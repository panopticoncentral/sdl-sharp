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
    public static bool Set(string name, string value) =>
        SDL_SetHint(ToUtf8(name), ToUtf8(value));

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
}
