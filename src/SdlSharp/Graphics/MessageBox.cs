using System.Runtime.InteropServices;

using static SdlSharp.Native.Common;
using static SdlSharp.Native.MessageBox;

namespace SdlSharp.Graphics;

/// <summary>
/// A button in a multi-button message box.
/// </summary>
/// <param name="Id">The value returned when this button is pressed.</param>
/// <param name="Text">The button label.</param>
/// <param name="IsReturnDefault">True if Return/Enter activates this button.</param>
/// <param name="IsEscapeDefault">True if Escape activates this button.</param>
public readonly record struct MessageBoxButton(
    int Id,
    string Text,
    bool IsReturnDefault = false,
    bool IsEscapeDefault = false);

/// <summary>
/// Optional colors for a message box (honored only by some platforms/backends).
/// </summary>
/// <param name="Background">Dialog background color.</param>
/// <param name="Text">Text color.</param>
/// <param name="ButtonBorder">Button border color.</param>
/// <param name="ButtonBackground">Button background color.</param>
/// <param name="ButtonSelected">Selected-button color.</param>
public readonly record struct MessageBoxColorScheme(
    Color Background,
    Color Text,
    Color ButtonBorder,
    Color ButtonBackground,
    Color ButtonSelected);

/// <summary>
/// Provides simple modal message box functionality.
/// </summary>
public static unsafe class MessageBox
{
    /// <summary>
    /// Shows a simple modal message box.
    /// </summary>
    /// <param name="type">The type of message box (error, warning, information).</param>
    /// <param name="title">The title of the message box.</param>
    /// <param name="message">The message text.</param>
    /// <param name="parent">Optional parent window, or null for no parent.</param>
    public static void Show(MessageBoxType type, string title, string message, Window? parent = null)
    {
        Native.SDL_Window* windowPtr = parent != null ? parent.Handle : null;
        Check(SDL_ShowSimpleMessageBox(
            (Native.SDL_MessageBoxFlags)type,
            ToUtf8(title),
            ToUtf8(message),
            windowPtr));
    }

    /// <summary>
    /// Shows a modal message box with custom buttons and returns the pressed button's ID.
    /// This blocks the calling thread and opens a native dialog — it cannot run headless.
    /// </summary>
    /// <param name="type">The type of message box (error, warning, information).</param>
    /// <param name="title">The title text.</param>
    /// <param name="message">The message text.</param>
    /// <param name="buttons">The buttons, in layout order.</param>
    /// <param name="parent">Optional parent window.</param>
    /// <param name="colorScheme">Optional color scheme (honored only by some backends).</param>
    /// <returns>The <see cref="MessageBoxButton.Id"/> of the pressed button, or -1 if the dialog was closed without a button.</returns>
    public static int Show(
        MessageBoxType type,
        string title,
        string message,
        MessageBoxButton[] buttons,
        Window? parent = null,
        MessageBoxColorScheme? colorScheme = null)
    {
        ArgumentNullException.ThrowIfNull(buttons);

        var textPins = new GCHandle[buttons.Length];
        var nativeButtons = new Native.SDL_MessageBoxButtonData[buttons.Length];
        try
        {
            for (var i = 0; i < buttons.Length; i++)
            {
                var utf8 = ToUtf8(buttons[i].Text)!;
                textPins[i] = GCHandle.Alloc(utf8, GCHandleType.Pinned);
                nativeButtons[i] = new Native.SDL_MessageBoxButtonData
                {
                    flags = (buttons[i].IsReturnDefault ? SDL_MESSAGEBOX_BUTTON_RETURNKEY_DEFAULT : 0u)
                          | (buttons[i].IsEscapeDefault ? SDL_MESSAGEBOX_BUTTON_ESCAPEKEY_DEFAULT : 0u),
                    buttonID = buttons[i].Id,
                    text = (byte*)textPins[i].AddrOfPinnedObject(),
                };
            }

            Native.SDL_MessageBoxColorScheme nativeScheme;
            Native.SDL_MessageBoxColorScheme* schemePtr = null;
            if (colorScheme is { } scheme)
            {
                nativeScheme = new Native.SDL_MessageBoxColorScheme
                {
                    background = ToNativeColor(scheme.Background),
                    text = ToNativeColor(scheme.Text),
                    button_border = ToNativeColor(scheme.ButtonBorder),
                    button_background = ToNativeColor(scheme.ButtonBackground),
                    button_selected = ToNativeColor(scheme.ButtonSelected),
                };
                schemePtr = &nativeScheme;
            }

            var titleUtf8 = ToUtf8(title)!;
            var messageUtf8 = ToUtf8(message)!;
            int buttonId;
            fixed (byte* titlePtr = titleUtf8)
            fixed (byte* messagePtr = messageUtf8)
            fixed (Native.SDL_MessageBoxButtonData* buttonsPtr = nativeButtons)
            {
                var data = new Native.SDL_MessageBoxData
                {
                    flags = (Native.SDL_MessageBoxFlags)type,
                    window = parent != null ? parent.Handle : null,
                    title = titlePtr,
                    message = messagePtr,
                    numbuttons = buttons.Length,
                    buttons = buttonsPtr,
                    colorScheme = schemePtr,
                };

                Check(SDL_ShowMessageBox(&data, &buttonId));
            }

            return buttonId;
        }
        finally
        {
            foreach (var pin in textPins)
            {
                if (pin.IsAllocated) pin.Free();
            }
        }
    }

    private static Native.SDL_MessageBoxColor ToNativeColor(Color color) =>
        new() { r = color.R, g = color.G, b = color.B };
}
