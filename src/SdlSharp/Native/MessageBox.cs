using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace SdlSharp.Native;

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

/// <summary>Data for a single message box button.</summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct SDL_MessageBoxButtonData
{
    /// <summary>Button flags (return/escape default).</summary>
    public uint flags;
    /// <summary>The value returned when this button is pressed.</summary>
    public int buttonID;
    /// <summary>The button label.</summary>
    public byte* text;
}

/// <summary>A color used in a message box color scheme.</summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_MessageBoxColor
{
    /// <summary>Red component.</summary>
    public byte r;
    /// <summary>Green component.</summary>
    public byte g;
    /// <summary>Blue component.</summary>
    public byte b;
}

/// <summary>A full message box color scheme (five colors, in SDL_MessageBoxColorType order).</summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_MessageBoxColorScheme
{
    /// <summary>Dialog background color.</summary>
    public SDL_MessageBoxColor background;
    /// <summary>Text color.</summary>
    public SDL_MessageBoxColor text;
    /// <summary>Button border color.</summary>
    public SDL_MessageBoxColor button_border;
    /// <summary>Button background color.</summary>
    public SDL_MessageBoxColor button_background;
    /// <summary>Selected-button color.</summary>
    public SDL_MessageBoxColor button_selected;
}

/// <summary>Full message box description.</summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct SDL_MessageBoxData
{
    /// <summary>Message box flags.</summary>
    public SDL_MessageBoxFlags flags;
    /// <summary>Parent window, or null for no parent.</summary>
    public SDL_Window* window;
    /// <summary>The title text.</summary>
    public byte* title;
    /// <summary>The message text.</summary>
    public byte* message;
    /// <summary>Number of buttons.</summary>
    public int numbuttons;
    /// <summary>The buttons, in layout order.</summary>
    public SDL_MessageBoxButtonData* buttons;
    /// <summary>Optional color scheme, or null for the default.</summary>
    public SDL_MessageBoxColorScheme* colorScheme;
}

/// <summary>
/// Native bindings for SDL_messagebox.h — message boxes.
/// </summary>
[SuppressMessage("Interoperability", "CA1401:P/Invokes should not be visible")]
public static partial class MessageBox
{
    /// <summary>Return/Enter activates this button.</summary>
    public const uint SDL_MESSAGEBOX_BUTTON_RETURNKEY_DEFAULT = 0x00000001u;
    /// <summary>Escape activates this button.</summary>
    public const uint SDL_MESSAGEBOX_BUTTON_ESCAPEKEY_DEFAULT = 0x00000002u;

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

    /// <summary>
    /// Display a modal message box with custom buttons and an optional color scheme.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_ShowMessageBox")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_ShowMessageBox(SDL_MessageBoxData* messageboxdata, int* buttonid);
}
