using System.Runtime.InteropServices;

using static SdlSharp.Native.Common;
using static SdlSharp.Native.Hints;

namespace SdlSharp;

/// <summary>
/// Provides access to SDL configuration hints.
/// </summary>
public static unsafe class SdlHints
{
    /// <summary>
    /// Sets a hint with normal priority.
    /// </summary>
    /// <param name="name">The hint name.</param>
    /// <param name="value">The hint value.</param>
    /// <returns>true if the hint was set, false otherwise.</returns>
    public static bool Set(string name, string? value) =>
        SDL_SetHint(ToUtf8(name), ToUtf8(value));

    /// <summary>
    /// Sets a hint with a specific priority. Higher-priority values override lower ones.
    /// </summary>
    /// <param name="name">The hint name.</param>
    /// <param name="value">The value to set.</param>
    /// <param name="priority">The priority to set it with.</param>
    /// <returns>True if the hint was set.</returns>
    public static bool Set(string name, string? value, HintPriority priority) =>
        SDL_SetHintWithPriority(ToUtf8(name), ToUtf8(value), (Native.SDL_HintPriority)priority);

    /// <summary>
    /// Gets the value of a hint.
    /// </summary>
    /// <param name="name">The hint name.</param>
    /// <returns>The hint value, or null if not set.</returns>
    public static string? Get(string name) =>
        Marshal.PtrToStringUTF8((nint)SDL_GetHint(ToUtf8(name)));

    /// <summary>
    /// Gets the boolean value of a hint.
    /// </summary>
    /// <param name="name">The hint name.</param>
    /// <param name="defaultValue">The default value if the hint is not set.</param>
    public static bool GetBoolean(string name, bool defaultValue = false) =>
        SDL_GetHintBoolean(ToUtf8(name), defaultValue);

    /// <summary>
    /// Resets a hint to its default value.
    /// </summary>
    /// <param name="name">The hint name.</param>
    /// <returns>true if the hint was reset, false otherwise.</returns>
    public static bool Reset(string name) =>
        SDL_ResetHint(ToUtf8(name));

    /// <summary>
    /// Resets all hints to their default values.
    /// </summary>
    public static void ResetAll() => SDL_ResetHints();
    /// <summary>Observes hint changes, including an immediate notification of the current value. Dispose to unregister.</summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000", Justification = "The lease is transferred to the returned scope or lifetime cleanup, which releases it exactly once.")]
    public static IDisposable Observe(string name, Action<string?, string?> changed)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        var registration = CallbackHandle.Alloc(changed);
        var operation = Application.BeginOperation();
        try { Check(SDL_AddHintCallback(ToUtf8(name), &HintChanged, (void*)(nint)registration)); }
        catch { registration.Free(); operation.Dispose(); throw; }
        return new SdlScope(() =>
        {
            CallbackErrors.EnsureOutsideCallback();
            SDL_RemoveHintCallback(ToUtf8(name), &HintChanged, (void*)(nint)registration);
            registration.Free(); operation.Dispose();
        });
    }
    [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static void HintChanged(void* data, byte* name, byte* previous, byte* current)
    {
        try
        {
            using var scope = CallbackErrors.Enter();
            if (CallbackHandle.FromIntPtr((nint)data).Target is Action<string?, string?> changed)
                changed(Marshal.PtrToStringUTF8((nint)previous), Marshal.PtrToStringUTF8((nint)current));
        }
        catch (Exception error) { CallbackErrors.Capture(error); }
    }
}
