using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using static SdlSharp.Native.Common;
using static SdlSharp.Native.Log;

namespace SdlSharp;

/// <summary>
/// Provides access to SDL's logging system.
/// </summary>
public static unsafe class SdlLog
{
    // CallbackHandle to prevent the managed delegate from being collected while the native callback is active.
    private static CallbackHandle _callbackHandle;

    /// <summary>
    /// Sets the priority of all log categories.
    /// </summary>
    /// <param name="priority">The new priority for all categories.</param>
    public static void SetAllPriorities(LogPriority priority) =>
        SDL_SetLogPriorities((Native.SDL_LogPriority)priority);

    /// <summary>
    /// Sets the priority of a specific log category.
    /// </summary>
    /// <param name="category">The category to set.</param>
    /// <param name="priority">The new priority.</param>
    public static void SetPriority(LogCategory category, LogPriority priority) =>
        SDL_SetLogPriority((int)category, (Native.SDL_LogPriority)priority);

    /// <summary>
    /// Gets the priority of a specific log category.
    /// </summary>
    /// <param name="category">The category to query.</param>
    public static LogPriority GetPriority(LogCategory category) =>
        (LogPriority)SDL_GetLogPriority((int)category);

    /// <summary>
    /// Resets all log priorities to their default values.
    /// </summary>
    public static void ResetPriorities() => SDL_ResetLogPriorities();

    /// <summary>
    /// Sets a prefix string for log messages of a given priority.
    /// </summary>
    /// <param name="priority">The priority level to set the prefix for.</param>
    /// <param name="prefix">The prefix string, or null to reset to default.</param>
    public static void SetPriorityPrefix(LogPriority priority, string? prefix) =>
        Check(SDL_SetLogPriorityPrefix((Native.SDL_LogPriority)priority, ToUtf8(prefix)));

    /// <summary>
    /// Sets a managed callback to receive all SDL log output.
    /// </summary>
    /// <param name="callback">The callback to invoke, or null to reset to default.</param>
    public static void SetOutputFunction(Action<LogCategory, LogPriority, string?>? callback)
    {
        CallbackErrors.EnsureOutsideCallback();
        var previous = _callbackHandle;
        _callbackHandle = callback == null ? default : CallbackHandle.Alloc(callback);
        if (callback == null)
        {
            SDL_SetLogOutputFunction(SDL_GetDefaultLogOutputFunction(), null);
        }
        else SDL_SetLogOutputFunction(&NativeLogCallback, (void*)(nint)_callbackHandle);
        previous.Free();
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void NativeLogCallback(void* userdata, int category, Native.SDL_LogPriority priority, byte* message)
    {
        try
        {
            using var scope = CallbackErrors.Enter();
            if (CallbackHandle.FromIntPtr((nint)userdata).Target is Action<LogCategory, LogPriority, string?> callback)
                callback((LogCategory)category, (LogPriority)priority, Marshal.PtrToStringUTF8((nint)message));
        }
        catch (Exception error) { CallbackErrors.Capture(error); }
    }
}
