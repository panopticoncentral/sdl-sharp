using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace SdlSharp.Native;

/// <summary>
/// Sandbox environments detectable via SDL_GetSandbox.
/// </summary>
public enum SDL_Sandbox
{
    SDL_SANDBOX_NONE = 0,
    SDL_SANDBOX_UNKNOWN_CONTAINER,
    SDL_SANDBOX_FLATPAK,
    SDL_SANDBOX_SNAP,
    SDL_SANDBOX_MACOS,
}

/// <summary>
/// Native bindings for SDL_system.h. Named SdlSystem (not System) to avoid
/// colliding with the .NET System namespace.
/// </summary>
[SuppressMessage("Interoperability", "CA1401:P/Invokes should not be visible")]
public static partial class SdlSystem
{
    // Skipped: the rest of SDL_system.h is platform-specific interop —
    // Android (JNI activity/environment, external storage, permissions, toasts),
    // iOS (animation callbacks, event pump control, HomeKit/CoreBluetooth handles),
    // Windows (message hooks, D3D adapter queries), X11 (event hooks), GDK, and
    // Linux thread priority helpers. These target platform toolchains SdlSharp
    // does not abstract; call them via your own platform bindings if needed.

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_IsTablet")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_IsTablet();

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_IsTV")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_IsTV();

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetSandbox")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Sandbox SDL_GetSandbox();
}
