using System.Collections.Concurrent;
using System.Runtime.ExceptionServices;

namespace SdlSharp;

// One state per native identity. Wrappers have their own disposed bit, but aliases
// share invalidation, leases and callback cleanup. Native-only callers must obey
// their own lifetime contracts; they cannot destroy objects behind this model.
internal sealed class NativeLifetime
{
    private static readonly Dictionary<(Type, nint), NativeLifetime> Registry = new();
    private readonly ConcurrentDictionary<NativeLifetime, byte> _children = new();
    private readonly ConcurrentQueue<Action> _cleanup = new();
    private readonly (Type, nint) _key;
    private readonly int _thread;
    internal object? Attachment;
    internal Action? Validate;
    internal bool HasOwners => _owners != 0;
    private int _owners;
    private int _leases;
    private int _alive = 1;
    private NativeLifetime? _parent;
    internal readonly object? Session = Application.CurrentSession;
    internal bool IsAlive => Volatile.Read(ref _alive) != 0 && (_parent?.IsAlive ?? true);
    internal static NativeLifetime Get(Type kind, nint handle, bool owns, bool threadAffine = false, NativeLifetime? parent = null)
    {
        if (handle == 0) throw new ArgumentException("A native handle cannot be null.");
        lock (Registry)
        {
            if (!Registry.TryGetValue((kind, handle), out var state) || !state.IsAlive)
                Registry[(kind, handle)] = state = new(kind, handle, threadAffine);
            // An independently opened reference must survive destruction of the
            // owner through which this identity was first observed as a borrow.
            if (owns && state._owners == 0 && parent == null && state._parent != null) state.SetParent(null);
            if (parent != null && state._parent == null && (owns || state._owners == 0)) state.SetParent(parent);
            if (owns) Interlocked.Increment(ref state._owners);
            return state;
        }
    }
    private NativeLifetime(Type kind, nint handle, bool threadAffine)
    {
        _key = (kind, handle);
        _thread = threadAffine ? Environment.CurrentManagedThreadId : 0;
    }
    internal void Check()
    {
        ObjectDisposedException.ThrowIf(!IsAlive, _key.Item1.Name);
        _parent?.Check();
        Validate?.Invoke();
        if (_thread != 0 && _thread != Environment.CurrentManagedThreadId)
            throw new InvalidOperationException("This SDL resource must be used on its creating thread.");
    }
    internal void SetParent(NativeLifetime? parent)
    {
        Check();
        if (parent == null) { _parent?._children.TryRemove(this, out _); _parent = null; return; }
        parent.Check();
        if (ReferenceEquals(parent, this)) return;
        for (var p = parent; p != null; p = p._parent)
            if (ReferenceEquals(p, this)) throw new InvalidOperationException("Cyclic SDL ownership.");
        _parent?._children.TryRemove(this, out _);
        _parent = parent;
        parent._children.TryAdd(this, 0);
    }
    internal void AddCleanup(Action cleanup) { Check(); _cleanup.Enqueue(cleanup); }
    internal IDisposable Retain()
    {
        Check(); Interlocked.Increment(ref _leases);
        return new SdlScope(() => Interlocked.Decrement(ref _leases), threadAffine: false);
    }
    private int _exclusive;
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000", Justification = "The lease is transferred to the returned scope or lifetime cleanup, which releases it exactly once.")]
    internal IDisposable RetainExclusive()
    {
        Check();
        if (Interlocked.CompareExchange(ref _exclusive, 1, 0) != 0) throw new InvalidOperationException("This resource is already mapped.");
        var lease = Retain();
        return new SdlScope(() => { lease.Dispose(); Volatile.Write(ref _exclusive, 0); });
    }
    internal void EnsureLeaseCountAtMost(int count)
    {
        Check();
        if (_leases > count) throw new InvalidOperationException("Release dependent operations before changing this resource.");
    }
    internal void InvalidateChildren(Type kind)
    {
        var states = _children.Keys.Where(child => child._key.Item1 == kind).ToArray();
        foreach (var state in states) state.EnsureCanDestroy();
        foreach (var state in states) state.Invalidate();
    }
    internal void EnsureCanDestroy()
    {
        Check(); CallbackErrors.EnsureOutsideCallback();
        if (_leases != 0) throw new InvalidOperationException("Release outstanding SDL leases or dependent resources first.");
        foreach (var child in _children.Keys.ToArray()) if (child.IsAlive) child.EnsureCanDestroy();
    }
    internal void Release(Action destroy)
    {
        if (!IsAlive) return;
        EnsureCanDestroy();
        destroy();
        if (Interlocked.Decrement(ref _owners) <= 0) Invalidate();
    }
    internal void Invalidate()
    {
        if (Interlocked.Exchange(ref _alive, 0) == 0) return;
        foreach (var child in _children.Keys.ToArray()) child.Invalidate();
        _children.Clear();
        _parent?._children.TryRemove(this, out _);
        while (_cleanup.TryDequeue(out var cleanup)) { try { cleanup(); } catch (Exception e) { CallbackErrors.Capture(e); } }
        lock (Registry) if (Registry.GetValueOrDefault(_key) == this) Registry.Remove(_key);
    }
    internal static void CheckSessionShutdown(object session)
    {
        lock (Registry)
            if (Registry.Values.Any(s => (s.Session == session || s.Session == null) && s.IsAlive && (s._owners != 0 || s._leases != 0)))
                throw new InvalidOperationException("Dispose SDL resources and registrations before disposing the application.");
    }
    internal static void EndSession(object session)
    {
        NativeLifetime[] states;
        lock (Registry) states = Registry.Values.Where(s => s.Session == session || s.Session == null).ToArray();
        foreach (var state in states) state.Invalidate();
    }
}

/// <summary>An idempotent SDL scope. Dispose on the thread that acquired it.</summary>
public sealed class SdlScope : IDisposable
{
    private Action? _release;
    private readonly int _thread;
    internal SdlScope(Action release, bool threadAffine = true) { _release = release; _thread = threadAffine ? Environment.CurrentManagedThreadId : 0; }
    /// <summary>Releases the scope once; aliases share the release state.</summary>
    public void Dispose()
    {
        if (_release == null) return;
        if (_thread != 0 && _thread != Environment.CurrentManagedThreadId) throw new InvalidOperationException("Dispose this scope on its acquiring thread.");
        var release = Interlocked.Exchange(ref _release, null);
        if (release == null) return;
        try { release(); }
        catch { Interlocked.CompareExchange(ref _release, release, null); throw; }
    }
}

internal static class CallbackErrors
{
    private static readonly ConcurrentQueue<ExceptionDispatchInfo> Errors = new();
    [ThreadStatic] private static int _depth;
    internal static void Capture(Exception e) => Errors.Enqueue(ExceptionDispatchInfo.Capture(e));
    internal static SdlScope Enter() { _depth++; return new(() => _depth--); }
    internal static void EnsureOutsideCallback()
    {
        if (_depth != 0) throw new InvalidOperationException("Native resources cannot be disposed from inside an SDL callback.");
    }
    internal static void ThrowPending()
    {
        if (Errors.TryDequeue(out var error)) error.Throw();
    }
}
