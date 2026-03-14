using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace SdlSharp.Native;

// SDL_hints.h defines ~200 hint name constants. These are just string keys —
// callers can pass any hint name as a string. Only wrapping the get/set/reset
// functions, not the individual hint name constants.
// Deferred: SDL_AddHintCallback, SDL_RemoveHintCallback (callback-based hint
// change notification — rarely needed, can be added later).

/// <summary>
/// Hint priority levels.
/// </summary>
public enum SDL_HintPriority
{
    /// <summary>Low priority, used for default values.</summary>
    SDL_HINT_DEFAULT = 0,
    /// <summary>Medium priority.</summary>
    SDL_HINT_NORMAL = 1,
    /// <summary>High priority, overrides normal values.</summary>
    SDL_HINT_OVERRIDE = 2,
}

/// <summary>
/// Native bindings for SDL_hints.h — configuration hints.
/// </summary>
[SuppressMessage("Interoperability", "CA1401:P/Invokes should not be visible")]
public static partial class Hints
{
    /// <summary>Set a hint with a specific priority.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetHintWithPriority")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetHintWithPriority(ReadOnlySpan<byte> name, ReadOnlySpan<byte> value, SDL_HintPriority priority);

    /// <summary>Set a hint with normal priority.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetHint")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetHint(ReadOnlySpan<byte> name, ReadOnlySpan<byte> value);

    /// <summary>Reset a hint to the default value.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_ResetHint")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_ResetHint(ReadOnlySpan<byte> name);

    /// <summary>Reset all hints to the default values.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_ResetHints")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_ResetHints();

    /// <summary>Get the value of a hint.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetHint")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* SDL_GetHint(ReadOnlySpan<byte> name);

    /// <summary>Get the boolean value of a hint variable.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetHintBoolean")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetHintBoolean(ReadOnlySpan<byte> name, [MarshalAs(UnmanagedType.U1)] bool default_value);
}
