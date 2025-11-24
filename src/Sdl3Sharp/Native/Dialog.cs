using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Properties;
using static Sdl3Sharp.Native.Video;

// We are intentionally exposing the P/Invoke calls so people can do low-level calls if needed
#pragma warning disable CA1401 // P/Invokes should not be visible

namespace Sdl3Sharp.Native;

/// <summary>
/// P/Invoke bindings for SDL_dialog.h - File dialog support.
/// </summary>
public static unsafe partial class Dialog
{
    /// <summary>
    /// An entry for filters for file dialogs.
    /// </summary>
    /// <remarks>
    /// <para><c>name</c> is a user-readable label for the filter (for example, "Office document").</para>
    /// <para><c>pattern</c> is a semicolon-separated list of file extensions (for example, "doc;docx").
    /// File extensions may only contain alphanumeric characters, hyphens, underscores and periods.
    /// Alternatively, the whole string can be a single asterisk ("*"), which serves as an "All files" filter.</para>
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_DialogFileFilter
    {
        /// <summary>
        /// A user-readable label for the filter (for example, "Office document").
        /// </summary>
        public byte* name;

        /// <summary>
        /// A semicolon-separated list of file extensions (for example, "doc;docx").
        /// </summary>
        public byte* pattern;
    }

    /// <summary>
    /// Various types of file dialogs.
    /// </summary>
    /// <remarks>
    /// This is used by <see cref="SDL_ShowFileDialogWithProperties"/> to decide what kind of
    /// dialog to present to the user.
    /// </remarks>
    public enum SDL_FileDialogType
    {
        /// <summary>
        /// Open file dialog.
        /// </summary>
        SDL_FILEDIALOG_OPENFILE,

        /// <summary>
        /// Save file dialog.
        /// </summary>
        SDL_FILEDIALOG_SAVEFILE,

        /// <summary>
        /// Open folder dialog.
        /// </summary>
        SDL_FILEDIALOG_OPENFOLDER
    }

    /// <summary>
    /// Property name for a pointer to a list of SDL_DialogFileFilter structs for file-based selections.
    /// </summary>
    public const string SDL_PROP_FILE_DIALOG_FILTERS_POINTER = "SDL.filedialog.filters";

    /// <summary>
    /// Property name for the number of filters in the array of filters.
    /// </summary>
    public const string SDL_PROP_FILE_DIALOG_NFILTERS_NUMBER = "SDL.filedialog.nfilters";

    /// <summary>
    /// Property name for the window that the dialog should be modal for.
    /// </summary>
    public const string SDL_PROP_FILE_DIALOG_WINDOW_POINTER = "SDL.filedialog.window";

    /// <summary>
    /// Property name for the default folder or file to start the dialog at.
    /// </summary>
    public const string SDL_PROP_FILE_DIALOG_LOCATION_STRING = "SDL.filedialog.location";

    /// <summary>
    /// Property name for a boolean indicating whether the user can select multiple entries.
    /// </summary>
    public const string SDL_PROP_FILE_DIALOG_MANY_BOOLEAN = "SDL.filedialog.many";

    /// <summary>
    /// Property name for the title of the dialog.
    /// </summary>
    public const string SDL_PROP_FILE_DIALOG_TITLE_STRING = "SDL.filedialog.title";

    /// <summary>
    /// Property name for the label of the accept button.
    /// </summary>
    public const string SDL_PROP_FILE_DIALOG_ACCEPT_STRING = "SDL.filedialog.accept";

    /// <summary>
    /// Property name for the label of the cancel button.
    /// </summary>
    public const string SDL_PROP_FILE_DIALOG_CANCEL_STRING = "SDL.filedialog.cancel";

    /// <summary>
    /// Displays a dialog that lets the user select a file on their filesystem.
    /// </summary>
    /// <remarks>
    /// <para>This is an asynchronous function; it will return immediately, and the result will be passed to the callback.</para>
    /// <para>The callback will be invoked with a null-terminated list of files the user chose.
    /// The list will be empty if the user canceled the dialog, and it will be NULL if an error occurred.</para>
    /// <para>Note that the callback may be called from a different thread than the one the function was invoked on.</para>
    /// <para>Depending on the platform, the user may be allowed to input paths that don't yet exist.</para>
    /// <para>On Linux, dialogs may require XDG Portals, which requires DBus, which requires an event-handling loop.
    /// Apps that do not use SDL to handle events should add a call to SDL_PumpEvents in their main loop.</para>
    /// </remarks>
    /// <param name="callback">A function pointer to be invoked when the user selects a file and accepts, or cancels the dialog, or an error occurs.</param>
    /// <param name="userdata">An optional pointer to pass extra data to the callback when it will be invoked.</param>
    /// <param name="window">The window that the dialog should be modal for, may be NULL. Not all platforms support this option.</param>
    /// <param name="filters">A list of filters, may be NULL. Not all platforms support this option. If non-NULL, it must remain valid at least until the callback is invoked.</param>
    /// <param name="nfilters">The number of filters. Ignored if filters is NULL.</param>
    /// <param name="default_location">The default folder or file to start the dialog at, may be NULL. Not all platforms support this option.</param>
    /// <param name="allow_many">If true, the user will be allowed to select multiple entries. Not all platforms support this option.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_ShowOpenFileDialog(
        delegate* unmanaged[Cdecl]<nuint, byte**, int, void> callback,
        nuint userdata,
        SDL_Window* window,
        SDL_DialogFileFilter* filters,
        int nfilters,
        [MarshalUsing(typeof(Utf8StringMarshaller))] string? default_location,
        [MarshalAs(UnmanagedType.U1)] bool allow_many);

    /// <summary>
    /// Displays a dialog that lets the user choose a new or existing file on their filesystem.
    /// </summary>
    /// <remarks>
    /// <para>This is an asynchronous function; it will return immediately, and the result will be passed to the callback.</para>
    /// <para>The callback will be invoked with a null-terminated list of files the user chose.
    /// The list will be empty if the user canceled the dialog, and it will be NULL if an error occurred.</para>
    /// <para>Note that the callback may be called from a different thread than the one the function was invoked on.</para>
    /// <para>The chosen file may or may not already exist.</para>
    /// <para>On Linux, dialogs may require XDG Portals, which requires DBus, which requires an event-handling loop.
    /// Apps that do not use SDL to handle events should add a call to SDL_PumpEvents in their main loop.</para>
    /// </remarks>
    /// <param name="callback">A function pointer to be invoked when the user selects a file and accepts, or cancels the dialog, or an error occurs.</param>
    /// <param name="userdata">An optional pointer to pass extra data to the callback when it will be invoked.</param>
    /// <param name="window">The window that the dialog should be modal for, may be NULL. Not all platforms support this option.</param>
    /// <param name="filters">A list of filters, may be NULL. Not all platforms support this option. If non-NULL, it must remain valid at least until the callback is invoked.</param>
    /// <param name="nfilters">The number of filters. Ignored if filters is NULL.</param>
    /// <param name="default_location">The default folder or file to start the dialog at, may be NULL. Not all platforms support this option.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_ShowSaveFileDialog(
        delegate* unmanaged[Cdecl]<nuint, byte**, int, void> callback,
        nuint userdata,
        SDL_Window* window,
        SDL_DialogFileFilter* filters,
        int nfilters,
        [MarshalUsing(typeof(Utf8StringMarshaller))] string? default_location);

    /// <summary>
    /// Displays a dialog that lets the user select a folder on their filesystem.
    /// </summary>
    /// <remarks>
    /// <para>This is an asynchronous function; it will return immediately, and the result will be passed to the callback.</para>
    /// <para>The callback will be invoked with a null-terminated list of files the user chose.
    /// The list will be empty if the user canceled the dialog, and it will be NULL if an error occurred.</para>
    /// <para>Note that the callback may be called from a different thread than the one the function was invoked on.</para>
    /// <para>Depending on the platform, the user may be allowed to input paths that don't yet exist.</para>
    /// <para>On Linux, dialogs may require XDG Portals, which requires DBus, which requires an event-handling loop.
    /// Apps that do not use SDL to handle events should add a call to SDL_PumpEvents in their main loop.</para>
    /// </remarks>
    /// <param name="callback">A function pointer to be invoked when the user selects a file and accepts, or cancels the dialog, or an error occurs.</param>
    /// <param name="userdata">An optional pointer to pass extra data to the callback when it will be invoked.</param>
    /// <param name="window">The window that the dialog should be modal for, may be NULL. Not all platforms support this option.</param>
    /// <param name="default_location">The default folder or file to start the dialog at, may be NULL. Not all platforms support this option.</param>
    /// <param name="allow_many">If true, the user will be allowed to select multiple entries. Not all platforms support this option.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_ShowOpenFolderDialog(
        delegate* unmanaged[Cdecl]<nuint, byte**, int, void> callback,
        nuint userdata,
        SDL_Window* window,
        [MarshalUsing(typeof(Utf8StringMarshaller))] string? default_location,
        [MarshalAs(UnmanagedType.U1)] bool allow_many);

    /// <summary>
    /// Create and launch a file dialog with the specified properties.
    /// </summary>
    /// <remarks>
    /// <para>These are the supported properties:</para>
    /// <list type="bullet">
    /// <item><description><see cref="SDL_PROP_FILE_DIALOG_FILTERS_POINTER"/>: a pointer to a list of SDL_DialogFileFilter structs.</description></item>
    /// <item><description><see cref="SDL_PROP_FILE_DIALOG_NFILTERS_NUMBER"/>: the number of filters in the array of filters.</description></item>
    /// <item><description><see cref="SDL_PROP_FILE_DIALOG_WINDOW_POINTER"/>: the window that the dialog should be modal for.</description></item>
    /// <item><description><see cref="SDL_PROP_FILE_DIALOG_LOCATION_STRING"/>: the default folder or file to start the dialog at.</description></item>
    /// <item><description><see cref="SDL_PROP_FILE_DIALOG_MANY_BOOLEAN"/>: true to allow the user to select more than one entry.</description></item>
    /// <item><description><see cref="SDL_PROP_FILE_DIALOG_TITLE_STRING"/>: the title for the dialog.</description></item>
    /// <item><description><see cref="SDL_PROP_FILE_DIALOG_ACCEPT_STRING"/>: the label that the accept button should have.</description></item>
    /// <item><description><see cref="SDL_PROP_FILE_DIALOG_CANCEL_STRING"/>: the label that the cancel button should have.</description></item>
    /// </list>
    /// <para>Note that each platform may or may not support any of the properties.</para>
    /// </remarks>
    /// <param name="type">The type of file dialog.</param>
    /// <param name="callback">A function pointer to be invoked when the user selects a file and accepts, or cancels the dialog, or an error occurs.</param>
    /// <param name="userdata">An optional pointer to pass extra data to the callback when it will be invoked.</param>
    /// <param name="props">The properties to use.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_ShowFileDialogWithProperties(
        SDL_FileDialogType type,
        delegate* unmanaged[Cdecl]<nuint, byte**, int, void> callback,
        nuint userdata,
        SDL_PropertiesID props);
}
