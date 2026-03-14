using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace SdlSharp.Native;

/// <summary>
/// SDL Timer ID.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly record struct SDL_TimerID(uint Value);

/// <summary>
/// Native bindings for SDL_timer.h — timing and timer utilities.
/// </summary>
[SuppressMessage("Interoperability", "CA1401:P/Invokes should not be visible")]
public static partial class Timer
{
    // Deferred: SDL_AddTimer, SDL_AddTimerNS, SDL_RemoveTimer (callback-based timers —
    // C# has System.Threading.Timer and System.Timers.Timer which are better suited
    // for managed code). Only wrapping the query/delay functions.

    /// <summary>Get the number of milliseconds since SDL library initialization.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetTicks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ulong SDL_GetTicks();

    /// <summary>Get the number of nanoseconds since SDL library initialization.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetTicksNS")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ulong SDL_GetTicksNS();

    /// <summary>Get the current value of the high resolution counter.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetPerformanceCounter")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ulong SDL_GetPerformanceCounter();

    /// <summary>Get the count per second of the high resolution counter.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetPerformanceFrequency")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ulong SDL_GetPerformanceFrequency();

    /// <summary>Wait a specified number of milliseconds before returning.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_Delay")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_Delay(uint ms);

    /// <summary>Wait a specified number of nanoseconds before returning.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_DelayNS")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_DelayNS(ulong ns);

    /// <summary>Wait a specified number of nanoseconds before returning (precise).</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_DelayPrecise")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_DelayPrecise(ulong ns);
}
