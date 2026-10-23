using System.Runtime.InteropServices;
using static SdlSharp.ImGui.Native;
namespace SdlSharp.ImGui;

// A view retains this identity, never just an address. Reusing a native address cannot
// revive a disposed view. Frame views additionally capture a frame number.
internal sealed unsafe class Lifetime
{
    private static readonly Dictionary<nint, WeakReference<Lifetime>> Registry = [];
    private readonly int _thread = Environment.CurrentManagedThreadId;
    private readonly List<Lifetime> _children = [];
    private readonly List<Action> _cleanup = [];
    private readonly Action? _destroy;
    private readonly CpuHandle? _safeHandle;
    private readonly nint _pointer;
    internal readonly Context? Context;
    internal readonly Lifetime? Parent;
    internal bool Owned => _destroy != null || _safeHandle != null;
    internal readonly HashSet<nint> TextureReferences = [];
    internal readonly Dictionary<ulong, GpuTextureReference> GpuTextures = [];
    internal bool ReadOnly { get; set; }
    internal bool IsSnapshot { get; set; }
    internal bool Disposed { get; private set; }
    internal int Version { get; private set; }
    internal int FrameVersion { get; private set; }
    internal void AdvanceFrame() { FrameVersion++; foreach (var child in _children) child.AdvanceFrame(); }
    private readonly Dictionary<Lifetime, int> _retentionOwners = [];
    internal int Retainers { get; private set; }
    internal bool Alive => !Disposed && (Parent?.Alive ?? true);

    internal Lifetime(nint pointer, Context? context, Lifetime? parent = null, Action? destroy = null, bool threadIndependent = false)
    {
        _pointer = pointer; Context = context; Parent = parent;
        if (threadIndependent && destroy != null) _safeHandle = new CpuHandle(pointer, destroy);
        else _destroy = destroy;
        parent?._children.Add(this);
        if (pointer != 0) lock (Registry) Registry[pointer] = new(this);
    }
    internal static Lifetime Capture(nint pointer, Lifetime? parent = null)
    {
        lock (Registry)
            if (Registry.TryGetValue(pointer, out var weak) && weak.TryGetTarget(out var state) && state.Alive) return state;
        var context = parent?.Context ?? SdlSharp.ImGui.Context.TryGetCurrent();
        return new(pointer, parent == null ? context : parent.Context, parent ?? context?.Lifetime);
    }
    internal static Lifetime Own(nint pointer, Action destroy, bool threadIndependent = false)
    {
        if (pointer == 0) throw new OutOfMemoryException();
        var context = SdlSharp.ImGui.Context.TryGetCurrent();
        return new(pointer, context, context?.Lifetime, destroy, threadIndependent);
    }
    internal void Check()
    {
        ObjectDisposedException.ThrowIf(!Alive, "ImGui resource");
        if (_thread != Environment.CurrentManagedThreadId) throw new InvalidOperationException("ImGui resources must be used on their creating thread.");
        Context?.CheckCurrent();
    }
    internal NativeCallLease Borrow() { Check(); return new(_safeHandle, this); }
    internal void AddCleanup(Action cleanup) { Check(); _cleanup.Add(cleanup); }
    internal IDisposable Retain()
    {
        Check(); Retainers++;
        return new ReleaseAction(() => Retainers--);
    }
    // Internal leases are released by their owner during context teardown. Leases
    // without an owner (scopes, callbacks, external command buffers) must block it.
    internal IDisposable RetainFor(Lifetime owner)
    {
        Check(); owner.Check(); Retainers++;
        _retentionOwners[owner] = _retentionOwners.GetValueOrDefault(owner) + 1;
        return new ReleaseAction(() =>
        {
            Retainers--;
            if (--_retentionOwners[owner] == 0) _retentionOwners.Remove(owner);
        });
    }
    internal void EnsureContextCanDispose()
    {
        Check(); Context?.CheckOutsideCallback();
        var tree = new HashSet<Lifetime>();
        Collect(this);
        foreach (var item in tree)
            if (item.Retainers != item._retentionOwners.Where(pair => tree.Contains(pair.Key)).Sum(pair => pair.Value))
                throw new InvalidOperationException("Release outstanding scopes, callbacks, and external submissions before disposing this context.");
        void Collect(Lifetime item) { tree.Add(item); foreach (var child in item._children) Collect(child); }
    }
    internal void ReleaseAttachments() { Check(); foreach (var cleanup in _cleanup.AsEnumerable().Reverse()) cleanup(); _cleanup.Clear(); }
    internal void InvalidateGrandchildren() { Check(); foreach (var child in _children.ToArray()) child.InvalidateChildren(); }
    internal void InvalidateViews() { Check(); Version++; }
    internal void EnsureUnretained()
    {
        Check(); Context?.CheckOutsideCallback();
        if (Retainers != 0 || _children.Any(c => c.Retainers != 0)) throw new InvalidOperationException("A context or render container still retains this resource.");
    }
    internal void InvalidateChildren()
    {
        Check();
        while (_children.Count != 0)
        {
            var child = _children.LastOrDefault(c => c.Retainers == 0);
            if (child == null) throw new InvalidOperationException("Resources are retained by an owner outside this context.");
            child.Dispose();
        }
        _children.Clear();
    }
    internal void Dispose()
    {
        if (Disposed) return;
        Check(); Context?.CheckOutsideCallback();
        if (Retainers != 0) throw new InvalidOperationException("The resource is retained by a snapshot or container. Dispose that owner first.");
        InvalidateChildren();
        // Native destruction can use attached source buffers and callback registrations.
        _destroy?.Invoke(); _safeHandle?.Dispose();
        foreach (var cleanup in _cleanup.AsEnumerable().Reverse()) cleanup();
        _cleanup.Clear(); Disposed = true;
        Parent?._children.Remove(this);
        if (_pointer != 0) lock (Registry)
            if (Registry.TryGetValue(_pointer, out var weak) && weak.TryGetTarget(out var value) && ReferenceEquals(value, this)) Registry.Remove(_pointer);
    }
    private sealed class CpuHandle(nint pointer, Action destroy) : SafeHandle(pointer, true)
    {
        public override bool IsInvalid => handle == 0;
        protected override bool ReleaseHandle() { destroy(); handle = 0; return true; }
    }
}
internal sealed class ReleaseAction(Action release) : IDisposable
{
    private Action? _release = release;
    private readonly object _sync = new();
    public void Dispose() { lock (_sync) { if (_release == null) return; _release(); _release = null; } }
}
internal readonly unsafe struct NativeView<T> where T : unmanaged
{
    private readonly T* _pointer;
    internal readonly Lifetime? Lifetime;
    private readonly int _frame;
    private readonly int _frameVersion;
    private readonly Context? _frameContext;
    private readonly int _version;
    internal NativeView(T* pointer, Lifetime? lifetime = null, bool frame = false)
    {
        _pointer = pointer; Lifetime = pointer == null ? null : lifetime ?? SdlSharp.ImGui.Lifetime.Capture((nint)pointer);
        _frameContext = frame ? SdlSharp.ImGui.Context.TryGetCurrent() : null;
        _frame = frame && pointer != null && _frameContext != null ? IGSharp_GetFrameCount() : -1;
        _frameVersion = frame && Lifetime != null ? Lifetime.FrameVersion : -1;
        _version = Lifetime?.Version ?? 0;
    }
    internal bool IsValid => _pointer != null && Lifetime is { Alive: true } && Lifetime.Version == _version && (_frameVersion < 0 || Lifetime.FrameVersion == _frameVersion) && (_frame < 0 || (_frameContext?.IsCurrent == true && IGSharp_GetFrameCount() == _frame));
    internal T* Handle
    {
        get
        {
            if (_pointer == null) throw new InvalidOperationException("The ImGui view is empty.");
            Lifetime!.Check();
            if (!IsValid) throw new InvalidOperationException("The ImGui view expired after a frame change or mutation.");
            return _pointer;
        }
    }
}

// Keep SafeHandles alive and pin their release count for the full unmanaged call,
// including calls made through a temporary managed owner.
internal readonly struct NativeCallLease : IDisposable
{
    private readonly SafeHandle? _handle;
    private readonly object? _owner;
    internal NativeCallLease(SafeHandle? handle, object? owner = null)
    {
        _handle = handle; _owner = owner;
        if (handle != null) { var acquired = false; handle.DangerousAddRef(ref acquired); }
    }
    public void Dispose() { _handle?.DangerousRelease(); GC.KeepAlive(_owner); }
}
