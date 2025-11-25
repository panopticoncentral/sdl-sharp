using System.Runtime.InteropServices;
using System.Text;

using Sdl3Sharp.Graphics;
using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.MessageBox;

namespace Sdl3Sharp;

/// <summary>
/// Provides methods for displaying native message boxes.
/// </summary>
/// <remarks>
/// <para>SDL offers a simple message box API, which is useful for simple alerts,
/// such as informing the user when something fatal happens at startup without
/// the need to build a UI for it (or informing the user before your UI is ready).</para>
/// <para>These message boxes are native system dialogs where possible.</para>
/// <para>Message box functions may be called at any time, even before SDL initialization,
/// making them useful for reporting errors during startup.</para>
/// </remarks>
public static unsafe class MessageBox
{
    /// <summary>
    /// Displays a simple modal message box with an OK button.
    /// </summary>
    /// <param name="flags">Flags controlling the appearance of the message box.</param>
    /// <param name="title">The title of the message box.</param>
    /// <param name="message">The message text to display.</param>
    /// <param name="window">The parent window, or null for no parent.</param>
    /// <remarks>
    /// <para>This function will block execution until the user clicks OK or closes the message box.</para>
    /// <para>This function may be called at any time, even before SDL initialization.</para>
    /// <para>On X11, SDL rolls its own dialog box with X11 primitives instead of a formal toolkit.</para>
    /// </remarks>
    public static void Show(MessageBoxFlags flags, string title, string message, Window? window = null)
    {
        CheckErrorBool(SDL_ShowSimpleMessageBox(
            (SDL_MessageBoxFlags)flags,
            title,
            message,
            window?.Handle));
    }

    /// <summary>
    /// Displays a simple informational message box with an OK button.
    /// </summary>
    /// <param name="title">The title of the message box.</param>
    /// <param name="message">The message text to display.</param>
    /// <param name="window">The parent window, or null for no parent.</param>
    public static void ShowInformation(string title, string message, Window? window = null)
    {
        Show(MessageBoxFlags.Information, title, message, window);
    }

    /// <summary>
    /// Displays a simple warning message box with an OK button.
    /// </summary>
    /// <param name="title">The title of the message box.</param>
    /// <param name="message">The message text to display.</param>
    /// <param name="window">The parent window, or null for no parent.</param>
    public static void ShowWarning(string title, string message, Window? window = null)
    {
        Show(MessageBoxFlags.Warning, title, message, window);
    }

    /// <summary>
    /// Displays a simple error message box with an OK button.
    /// </summary>
    /// <param name="title">The title of the message box.</param>
    /// <param name="message">The message text to display.</param>
    /// <param name="window">The parent window, or null for no parent.</param>
    public static void ShowError(string title, string message, Window? window = null)
    {
        Show(MessageBoxFlags.Error, title, message, window);
    }

    /// <summary>
    /// Displays a customizable modal message box with multiple buttons.
    /// </summary>
    /// <param name="flags">Flags controlling the appearance of the message box.</param>
    /// <param name="title">The title of the message box.</param>
    /// <param name="message">The message text to display.</param>
    /// <param name="buttons">The buttons to display in the message box.</param>
    /// <param name="window">The parent window, or null for no parent.</param>
    /// <param name="colorScheme">The color scheme to use, or null for system defaults.</param>
    /// <returns>The ID of the button that was clicked.</returns>
    /// <remarks>
    /// <para>This function will block execution until the user clicks a button or closes the message box.</para>
    /// <para>This function may be called at any time, even before SDL initialization.</para>
    /// <para>On X11, SDL rolls its own dialog box with X11 primitives instead of a formal toolkit.</para>
    /// </remarks>
    public static int ShowCustom(
        MessageBoxFlags flags,
        string title,
        string message,
        IReadOnlyList<MessageBoxButton> buttons,
        Window? window = null,
        MessageBoxColorScheme? colorScheme = null)
    {
        ArgumentNullException.ThrowIfNull(buttons);

        if (buttons.Count == 0)
        {
            throw new ArgumentException("At least one button must be provided.", nameof(buttons));
        }

        var nativeButtons = CreateNativeButtons(buttons, out var allocatedStrings);
        var nativeColorScheme = CreateNativeColorScheme(colorScheme);

        try
        {
            var messageBoxData = new SDL_MessageBoxData
            {
                flags = (SDL_MessageBoxFlags)flags,
                window = window?.Handle,
                title = null,
                message = null,
                numbuttons = buttons.Count,
                buttons = nativeButtons,
                colorScheme = nativeColorScheme
            };

            var titleBytes = Encoding.UTF8.GetBytes(title + '\0');
            var messageBytes = Encoding.UTF8.GetBytes(message + '\0');

            fixed (byte* titlePtr = titleBytes)
            fixed (byte* messagePtr = messageBytes)
            {
                messageBoxData.title = titlePtr;
                messageBoxData.message = messagePtr;

                int buttonId = 0;
                CheckErrorBool(SDL_ShowMessageBox(&messageBoxData, &buttonId));
                return buttonId;
            }
        }
        finally
        {
            FreeNativeButtons(nativeButtons, allocatedStrings);
            FreeNativeColorScheme(nativeColorScheme);
        }
    }

    private static SDL_MessageBoxButtonData* CreateNativeButtons(
        IReadOnlyList<MessageBoxButton> buttons,
        out nint[] allocatedStrings)
    {
        var nativeButtons = (SDL_MessageBoxButtonData*)Marshal.AllocHGlobal(
            sizeof(SDL_MessageBoxButtonData) * buttons.Count);

        allocatedStrings = new nint[buttons.Count];

        for (var i = 0; i < buttons.Count; i++)
        {
            var textBytes = Encoding.UTF8.GetBytes(buttons[i].Text + '\0');
            var textPtr = Marshal.AllocHGlobal(textBytes.Length);
            Marshal.Copy(textBytes, 0, textPtr, textBytes.Length);

            allocatedStrings[i] = textPtr;

            nativeButtons[i] = new SDL_MessageBoxButtonData
            {
                flags = (SDL_MessageBoxButtonFlags)buttons[i].Flags,
                buttonID = buttons[i].ButtonId,
                text = (byte*)textPtr
            };
        }

        return nativeButtons;
    }

    private static void FreeNativeButtons(SDL_MessageBoxButtonData* nativeButtons, nint[] allocatedStrings)
    {
        if (nativeButtons != null)
        {
            Marshal.FreeHGlobal((nint)nativeButtons);
        }

        foreach (var ptr in allocatedStrings)
        {
            if (ptr != 0)
            {
                Marshal.FreeHGlobal(ptr);
            }
        }
    }

    private static SDL_MessageBoxColorScheme* CreateNativeColorScheme(MessageBoxColorScheme? colorScheme)
    {
        if (colorScheme == null)
        {
            return null;
        }

        var nativeScheme = (SDL_MessageBoxColorScheme*)Marshal.AllocHGlobal(sizeof(SDL_MessageBoxColorScheme));
        var colors = colorScheme.GetColors();

        for (var i = 0; i < colors.Length; i++)
        {
            (*nativeScheme)[(SDL_MessageBoxColorType)i] = new SDL_MessageBoxColor
            {
                r = colors[i].R,
                g = colors[i].G,
                b = colors[i].B
            };
        }

        return nativeScheme;
    }

    private static void FreeNativeColorScheme(SDL_MessageBoxColorScheme* nativeScheme)
    {
        if (nativeScheme != null)
        {
            Marshal.FreeHGlobal((nint)nativeScheme);
        }
    }
}
