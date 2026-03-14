using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace SdlSharp.Native;

// Deferred: SDL_ShowMessageBox (custom button/color scheme — complex struct hierarchy,
// rarely needed). Only wrapping SDL_ShowSimpleMessageBox for common use.

/// <summary>
/// Message box flags.
/// </summary>
[Flags]
public enum SDL_MessageBoxFlags : uint
{
    /// <summary>Error dialog.</summary>
    SDL_MESSAGEBOX_ERROR = 0x00000010u,
    /// <summary>Warning dialog.</summary>
    SDL_MESSAGEBOX_WARNING = 0x00000020u,
    /// <summary>Informational dialog.</summary>
    SDL_MESSAGEBOX_INFORMATION = 0x00000040u,
    /// <summary>Buttons placed left to right.</summary>
    SDL_MESSAGEBOX_BUTTONS_LEFT_TO_RIGHT = 0x00000080u,
    /// <summary>Buttons placed right to left.</summary>
    SDL_MESSAGEBOX_BUTTONS_RIGHT_TO_LEFT = 0x00000100u,
}

/// <summary>
/// Native bindings for SDL_messagebox.h — simple message boxes.
/// </summary>
[SuppressMessage("Interoperability", "CA1401:P/Invokes should not be visible")]
public static partial class MessageBox
{
    /// <summary>
    /// Display a simple modal message box.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_ShowSimpleMessageBox")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_ShowSimpleMessageBox(
        SDL_MessageBoxFlags flags,
        ReadOnlySpan<byte> title,
        ReadOnlySpan<byte> message,
        SDL_Window* window);
}
