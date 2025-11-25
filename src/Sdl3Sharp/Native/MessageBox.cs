using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Video;

// We are intentionally exposing the P/Invoke calls so people can do low-level calls if needed
#pragma warning disable CA1401 // P/Invokes should not be visible

namespace Sdl3Sharp.Native;

/// <summary>
/// P/Invoke bindings for SDL_messagebox.h - Simple message box API.
/// </summary>
/// <remarks>
/// SDL offers a simple message box API, which is useful for simple alerts,
/// such as informing the user when something fatal happens at startup without
/// the need to build a UI for it (or informing the user before your UI is ready).
/// These message boxes are native system dialogs where possible.
/// </remarks>
public static unsafe partial class MessageBox
{
    /// <summary>
    /// Message box flags.
    /// </summary>
    /// <remarks>
    /// If supported will display warning icon, etc.
    /// </remarks>
    public enum SDL_MessageBoxFlags : uint
    {
        /// <summary>
        /// Error dialog.
        /// </summary>
        SDL_MESSAGEBOX_ERROR = 0x00000010u,

        /// <summary>
        /// Warning dialog.
        /// </summary>
        SDL_MESSAGEBOX_WARNING = 0x00000020u,

        /// <summary>
        /// Informational dialog.
        /// </summary>
        SDL_MESSAGEBOX_INFORMATION = 0x00000040u,

        /// <summary>
        /// Buttons placed left to right.
        /// </summary>
        SDL_MESSAGEBOX_BUTTONS_LEFT_TO_RIGHT = 0x00000080u,

        /// <summary>
        /// Buttons placed right to left.
        /// </summary>
        SDL_MESSAGEBOX_BUTTONS_RIGHT_TO_LEFT = 0x00000100u
    }

    /// <summary>
    /// SDL_MessageBoxButtonData flags.
    /// </summary>
    public enum SDL_MessageBoxButtonFlags : uint
    {
        /// <summary>
        /// Marks the default button when return is hit.
        /// </summary>
        SDL_MESSAGEBOX_BUTTON_RETURNKEY_DEFAULT = 0x00000001u,

        /// <summary>
        /// Marks the default button when escape is hit.
        /// </summary>
        SDL_MESSAGEBOX_BUTTON_ESCAPEKEY_DEFAULT = 0x00000002u
    }

    /// <summary>
    /// Individual button data.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_MessageBoxButtonData
    {
        /// <summary>
        /// Button flags.
        /// </summary>
        public SDL_MessageBoxButtonFlags flags;

        /// <summary>
        /// User defined button id (value returned via SDL_ShowMessageBox).
        /// </summary>
        public int buttonID;

        /// <summary>
        /// The UTF-8 button text.
        /// </summary>
        public byte* text;
    }

    /// <summary>
    /// RGB value used in a message box color scheme.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_MessageBoxColor
    {
        /// <summary>
        /// Red component (0-255).
        /// </summary>
        public byte r;

        /// <summary>
        /// Green component (0-255).
        /// </summary>
        public byte g;

        /// <summary>
        /// Blue component (0-255).
        /// </summary>
        public byte b;
    }

    /// <summary>
    /// An enumeration of indices inside the colors array of SDL_MessageBoxColorScheme.
    /// </summary>
    public enum SDL_MessageBoxColorType
    {
        /// <summary>
        /// Background color.
        /// </summary>
        SDL_MESSAGEBOX_COLOR_BACKGROUND,

        /// <summary>
        /// Text color.
        /// </summary>
        SDL_MESSAGEBOX_COLOR_TEXT,

        /// <summary>
        /// Button border color.
        /// </summary>
        SDL_MESSAGEBOX_COLOR_BUTTON_BORDER,

        /// <summary>
        /// Button background color.
        /// </summary>
        SDL_MESSAGEBOX_COLOR_BUTTON_BACKGROUND,

        /// <summary>
        /// Selected button color.
        /// </summary>
        SDL_MESSAGEBOX_COLOR_BUTTON_SELECTED,

        /// <summary>
        /// Size of the colors array of SDL_MessageBoxColorScheme.
        /// </summary>
        SDL_MESSAGEBOX_COLOR_COUNT
    }

    /// <summary>
    /// A set of colors to use for message box dialogs.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SDL_MessageBoxColorScheme
    {
        /// <summary>
        /// Fixed-size array of colors for the message box color scheme.
        /// </summary>
        public fixed byte colors[sizeof(SDL_MessageBoxColor) * (int)SDL_MessageBoxColorType.SDL_MESSAGEBOX_COLOR_COUNT];

        /// <summary>
        /// Gets or sets a color at the specified index.
        /// </summary>
        /// <param name="index">The color type index.</param>
        /// <returns>The color at the specified index.</returns>
        public SDL_MessageBoxColor this[SDL_MessageBoxColorType index]
        {
            get
            {
                fixed (byte* ptr = colors)
                {
                    return ((SDL_MessageBoxColor*)ptr)[(int)index];
                }
            }
            set
            {
                fixed (byte* ptr = colors)
                {
                    ((SDL_MessageBoxColor*)ptr)[(int)index] = value;
                }
            }
        }
    }

    /// <summary>
    /// MessageBox structure containing title, text, window, etc.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_MessageBoxData
    {
        /// <summary>
        /// Message box flags.
        /// </summary>
        public SDL_MessageBoxFlags flags;

        /// <summary>
        /// Parent window, can be NULL.
        /// </summary>
        public SDL_Window* window;

        /// <summary>
        /// UTF-8 title.
        /// </summary>
        public byte* title;

        /// <summary>
        /// UTF-8 message text.
        /// </summary>
        public byte* message;

        /// <summary>
        /// Number of buttons.
        /// </summary>
        public int numbuttons;

        /// <summary>
        /// Array of button data.
        /// </summary>
        public SDL_MessageBoxButtonData* buttons;

        /// <summary>
        /// SDL_MessageBoxColorScheme, can be NULL to use system settings.
        /// </summary>
        public SDL_MessageBoxColorScheme* colorScheme;
    }

    /// <summary>
    /// Create a modal message box.
    /// </summary>
    /// <remarks>
    /// <para>If your needs aren't complex, it might be easier to use <see cref="SDL_ShowSimpleMessageBox"/>.</para>
    /// <para>This function should be called on the thread that created the parent window, or on the main thread
    /// if the messagebox has no parent. It will block execution of that thread until the user clicks a button
    /// or closes the messagebox.</para>
    /// <para>This function may be called at any time, even before SDL_Init(). This makes it useful for reporting
    /// errors like a failure to create a renderer or OpenGL context.</para>
    /// <para>On X11, SDL rolls its own dialog box with X11 primitives instead of a formal toolkit like GTK+ or Qt.</para>
    /// <para>Note that if SDL_Init() would fail because there isn't any available video target, this function is
    /// likely to fail for the same reasons. If this is a concern, check the return value from this function and
    /// fall back to writing to stderr if you can.</para>
    /// </remarks>
    /// <param name="messageboxdata">The SDL_MessageBoxData structure with title, text and other options.</param>
    /// <param name="buttonid">The pointer to which user id of hit button should be copied.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_ShowMessageBox(SDL_MessageBoxData* messageboxdata, int* buttonid);

    /// <summary>
    /// Display a simple modal message box.
    /// </summary>
    /// <remarks>
    /// <para>If your needs aren't complex, this function is preferred over <see cref="SDL_ShowMessageBox"/>.</para>
    /// <para>This function should be called on the thread that created the parent window, or on the main thread
    /// if the messagebox has no parent. It will block execution of that thread until the user clicks a button
    /// or closes the messagebox.</para>
    /// <para>This function may be called at any time, even before SDL_Init(). This makes it useful for reporting
    /// errors like a failure to create a renderer or OpenGL context.</para>
    /// <para>On X11, SDL rolls its own dialog box with X11 primitives instead of a formal toolkit like GTK+ or Qt.</para>
    /// <para>Note that if SDL_Init() would fail because there isn't any available video target, this function is
    /// likely to fail for the same reasons. If this is a concern, check the return value from this function and
    /// fall back to writing to stderr if you can.</para>
    /// </remarks>
    /// <param name="flags">An SDL_MessageBoxFlags value.</param>
    /// <param name="title">UTF-8 title text.</param>
    /// <param name="message">UTF-8 message text.</param>
    /// <param name="window">The parent window, or NULL for no parent.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_ShowSimpleMessageBox(
        SDL_MessageBoxFlags flags,
        [MarshalUsing(typeof(Utf8StringMarshaller))] string title,
        [MarshalUsing(typeof(Utf8StringMarshaller))] string message,
        SDL_Window* window);
}
