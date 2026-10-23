using System.Collections.Concurrent;

namespace SdlSharp;

internal readonly struct CallbackHandle
{
    private static long _next;
    private static readonly ConcurrentDictionary<nint, object> Targets = new();
    internal static int Count => Targets.Count;
    private readonly nint _token;
    private CallbackHandle(nint token) => _token = token;
    internal static CallbackHandle Alloc(object target)
    {
        ArgumentNullException.ThrowIfNull(target);
        var token = checked((nint)Interlocked.Increment(ref _next));
        Targets[token] = target;
        return new(token);
    }
    internal static CallbackHandle FromIntPtr(nint token) => new(token);
    internal static nint ToIntPtr(CallbackHandle handle) => handle._token;
    public static explicit operator nint(CallbackHandle handle) => handle._token;
    internal object? Target => Targets.GetValueOrDefault(_token);
    internal bool IsAllocated => Targets.ContainsKey(_token);
    internal void Free() => Targets.TryRemove(_token, out _);
}
