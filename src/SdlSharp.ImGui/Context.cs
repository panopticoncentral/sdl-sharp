using static SdlSharp.ImGui.Native;
namespace SdlSharp.ImGui;

/// <summary>Owns an ImGui context and its resources on one thread. Dispose deterministically.</summary>
public sealed unsafe class Context : IDisposable
{
    private static readonly Dictionary<nint, WeakReference<Context>> Contexts = [];
    private IGSharp_Context* _handle;
    private int _callbackDepth;
    internal readonly Dictionary<nint, Stack<StateStorageScope>> StorageScopes = [];
    internal readonly Stack<nint> TableHostWindows = [];
    internal nint? DragSourceHostWindow;
    internal void EnsureStorageRestored()
    {
        CheckCurrent();
        if (StorageScopes.Count != 0) throw new InvalidOperationException("Dispose state-storage scopes before ending or starting a frame.");
    }
    internal void EnsureWindowStorageRestored()
    {
        CheckCurrent();
        if (StorageScopes.ContainsKey((nint)IGSharp_GetWindowDrawList())) throw new InvalidOperationException("Dispose this window's state-storage scopes before ending it.");
    }
    internal void CheckOutsideCallback() { if (_callbackDepth != 0) throw new InvalidOperationException("This operation is not allowed during a native callback."); }
    internal IDisposable EnterCallback() { CheckCurrent(); _callbackDepth++; return new ReleaseAction(() => _callbackDepth--); }
    private readonly int _thread = Environment.CurrentManagedThreadId;
    internal Lifetime Lifetime { get; }
    internal IGSharp_Context* Handle { get { CheckAccess(); return _handle; } }
    private Context(IGSharp_Context* handle)
    {
        _handle = handle;
        Lifetime = new((nint)handle, this);
        lock (Contexts) Contexts[(nint)handle] = new(this);
    }
    /// <summary>Creates and activates a context with its own font atlas.</summary>
    public static Context Create() => CreateCore(null);
    /// <summary>Creates a context retaining a shared, owned font atlas until context disposal.</summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000", Justification = "The lease is transferred to the context lifetime, or disposed in the catch path.")]
    public static Context Create(OwnedFontAtlas sharedAtlas)
    {
        ArgumentNullException.ThrowIfNull(sharedAtlas);
        var atlas = sharedAtlas.Atlas;
        var retention = atlas.Lifetime.Retain();
        try { var context = CreateCore(atlas.Handle); context.Lifetime.AddCleanup(retention.Dispose); return context; }
        catch { retention.Dispose(); throw; }
    }
    private static Context CreateCore(IGSharp_FontAtlas* atlas)
    {
        if (TryGetCurrent() is { _callbackDepth: > 0 }) throw new InvalidOperationException("A native callback cannot create another context.");
        ValidateAbi(); var handle = IGSharp_CreateContext(atlas);
        if (handle == null) throw new OutOfMemoryException();
        IGSharp_SetCurrentContext(handle);
        var context = new Context(handle);
        if (atlas == null) _ = new Lifetime((nint)IGSharp_GetIO()->Fonts, context, context.Lifetime);
        return context;
    }
    /// <summary>The active managed context; throws when none is active.</summary>
    public static Context Current => TryGetCurrent() ?? throw new InvalidOperationException("No managed ImGui context is current.");
    internal static Context? TryGetCurrent()
    {
        var pointer = (nint)IGSharp_GetCurrentContext();
        lock (Contexts) return Contexts.TryGetValue(pointer, out var weak) && weak.TryGetTarget(out var context) ? context : null;
    }
    /// <summary>Whether this context has been disposed.</summary>
    public bool IsDisposed => _handle == null;
    /// <summary>Whether this context is currently active.</summary>
    public bool IsCurrent => _handle != null && IGSharp_GetCurrentContext() == _handle;
    internal void CheckAccess()
    {
        ObjectDisposedException.ThrowIf(_handle == null, this);
        if (_thread != Environment.CurrentManagedThreadId) throw new InvalidOperationException("Use this context on its creating thread.");
    }
    internal void CheckCurrent()
    {
        CheckAccess();
        if (!IsCurrent) throw new InvalidOperationException("The resource's ImGui context is not current. Use Context.Enter().");
    }
    /// <summary>Makes this context current.</summary>
    public void MakeCurrent() { CheckAccess(); if (TryGetCurrent() is { _callbackDepth: > 0 } active && !ReferenceEquals(active, this)) throw new InvalidOperationException("A native callback cannot switch contexts."); IGSharp_SetCurrentContext(_handle); }
    /// <summary>Activates this context until the returned scope is disposed, then restores the previous context.</summary>
    public IDisposable Enter()
    {
        CheckAccess(); var previous = TryGetCurrent(); MakeCurrent();
        return new ReleaseAction(() => { if (_thread != Environment.CurrentManagedThreadId) throw new InvalidOperationException("Dispose a context scope on its creating thread."); if (previous is { IsDisposed: false }) previous.MakeCurrent(); else { TryGetCurrent()?.CheckOutsideCallback(); IGSharp_SetCurrentContext(null); } });
    }
    /// <summary>Starts a headless/custom-backend frame after IO has been configured.</summary>
    public void NewFrame() { CheckCurrent(); CheckOutsideCallback(); EnsureStorageRestored(); ImGui.ReleaseFrameCallbacks(); Lifetime.AdvanceFrame();
        SdlSharp.ImGui.Lifetime.Capture((nint)IGSharp_GetIO()->Fonts).AdvanceFrame(); IGSharp_NewFrame(); }
    /// <summary>Disposes owned children before destroying the context, restoring the previous context when possible.</summary>
    public void Dispose()
    {
        if (_handle == null) return;
        CheckAccess(); if (_callbackDepth != 0) throw new InvalidOperationException("A context cannot be disposed from inside a native callback.");
        var previous = TryGetCurrent(); MakeCurrent(); var pointer = (nint)_handle;
        try
        {
            Lifetime.EnsureContextCanDispose();
            DrawList.ReleaseFrame(pointer);
            Lifetime.Dispose();
            IGSharp_DestroyContext(_handle); _handle = null;
            PlatformIO.ReleaseContext(pointer); Io.ReleaseContext(pointer); ImGui.ReleaseContext(pointer); DrawList.ReleaseContext(pointer);
            lock (Contexts) Contexts.Remove(pointer);
        }
        finally { IGSharp_SetCurrentContext(previous is { IsDisposed: false } ? previous._handle : null); }
    }
}
