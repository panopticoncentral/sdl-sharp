using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace SdlSharp.Native;

// Minimal stub — only SDL_PumpEvents is bound here. The full SDL_events.h
// wrapping (SDL_Event union, SDL_PollEvent, SDL_WaitEvent, event filter, etc.)
// is deferred to Phase 3: Events + Input.

/// <summary>
/// Native bindings for SDL_events.h (stub — pump only).
/// </summary>
[SuppressMessage("Interoperability", "CA1401:P/Invokes should not be visible")]
public static partial class Events
{
    /// <summary>
    /// Gather events from input devices. Must be called on the main thread.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_PumpEvents")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_PumpEvents();
}
