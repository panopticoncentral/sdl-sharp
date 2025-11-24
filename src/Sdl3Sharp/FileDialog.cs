using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

using Sdl3Sharp.Graphics;

using static Sdl3Sharp.Native.Dialog;
using static Sdl3Sharp.Native.Video;

namespace Sdl3Sharp;

/// <summary>
/// Provides methods for displaying native file dialogs.
/// </summary>
/// <remarks>
/// <para>All dialog operations are asynchronous. The dialog will be displayed and control
/// will return immediately. The result will be provided through a callback or task completion.</para>
/// <para>On Linux, dialogs may require XDG Portals, which requires DBus, which requires an
/// event-handling loop. Apps that do not use SDL to handle events should add a call to
/// SDL_PumpEvents in their main loop.</para>
/// </remarks>
public static unsafe class FileDialog
{
    /// <summary>
    /// Displays a dialog that lets the user select a file to open.
    /// </summary>
    /// <param name="callback">A callback invoked when the user makes a selection or cancels the dialog.</param>
    /// <param name="window">The window that the dialog should be modal for, or null.</param>
    /// <param name="filters">A list of file filters, or null for no filtering.</param>
    /// <param name="defaultLocation">The default folder or file to start the dialog at, or null.</param>
    /// <param name="allowMultiple">If true, the user can select multiple files.</param>
    public static void ShowOpenFileDialog(
        Action<FileDialogResult> callback,
        Window? window = null,
        IReadOnlyList<DialogFileFilter>? filters = null,
        string? defaultLocation = null,
        bool allowMultiple = false)
    {
        ShowDialogCore(callback, window, filters, defaultLocation, allowMultiple, SDL_ShowOpenFileDialog);
    }

    /// <summary>
    /// Displays a dialog that lets the user choose a file to save.
    /// </summary>
    /// <param name="callback">A callback invoked when the user makes a selection or cancels the dialog.</param>
    /// <param name="window">The window that the dialog should be modal for, or null.</param>
    /// <param name="filters">A list of file filters, or null for no filtering.</param>
    /// <param name="defaultLocation">The default folder or file to start the dialog at, or null.</param>
    public static void ShowSaveFileDialog(
        Action<FileDialogResult> callback,
        Window? window = null,
        IReadOnlyList<DialogFileFilter>? filters = null,
        string? defaultLocation = null)
    {
        ShowDialogCore(callback, window, filters, defaultLocation, false, (cb, ud, wnd, f, nf, loc, _) =>
            SDL_ShowSaveFileDialog(cb, ud, wnd, f, nf, loc));
    }

    /// <summary>
    /// Displays a dialog that lets the user select a folder.
    /// </summary>
    /// <param name="callback">A callback invoked when the user makes a selection or cancels the dialog.</param>
    /// <param name="window">The window that the dialog should be modal for, or null.</param>
    /// <param name="defaultLocation">The default folder to start the dialog at, or null.</param>
    /// <param name="allowMultiple">If true, the user can select multiple folders.</param>
    public static void ShowOpenFolderDialog(
        Action<FileDialogResult> callback,
        Window? window = null,
        string? defaultLocation = null,
        bool allowMultiple = false)
    {
        var handle = GCHandle.Alloc(callback);

        SDL_ShowOpenFolderDialog(
            &DialogCallback,
            (nuint)(nint)GCHandle.ToIntPtr(handle),
            window == null ? null : window.Handle,
            defaultLocation,
            allowMultiple);
    }

    /// <summary>
    /// Displays a dialog that lets the user select a file to open.
    /// </summary>
    /// <param name="window">The window that the dialog should be modal for, or null.</param>
    /// <param name="filters">A list of file filters, or null for no filtering.</param>
    /// <param name="defaultLocation">The default folder or file to start the dialog at, or null.</param>
    /// <param name="allowMultiple">If true, the user can select multiple files.</param>
    /// <returns>A task that completes with the dialog result.</returns>
    public static Task<FileDialogResult> ShowOpenFileDialogAsync(
        Window? window = null,
        IReadOnlyList<DialogFileFilter>? filters = null,
        string? defaultLocation = null,
        bool allowMultiple = false)
    {
        var tcs = new TaskCompletionSource<FileDialogResult>();
        ShowOpenFileDialog(result => tcs.SetResult(result), window, filters, defaultLocation, allowMultiple);
        return tcs.Task;
    }

    /// <summary>
    /// Displays a dialog that lets the user choose a file to save.
    /// </summary>
    /// <param name="window">The window that the dialog should be modal for, or null.</param>
    /// <param name="filters">A list of file filters, or null for no filtering.</param>
    /// <param name="defaultLocation">The default folder or file to start the dialog at, or null.</param>
    /// <returns>A task that completes with the dialog result.</returns>
    public static Task<FileDialogResult> ShowSaveFileDialogAsync(
        Window? window = null,
        IReadOnlyList<DialogFileFilter>? filters = null,
        string? defaultLocation = null)
    {
        var tcs = new TaskCompletionSource<FileDialogResult>();
        ShowSaveFileDialog(result => tcs.SetResult(result), window, filters, defaultLocation);
        return tcs.Task;
    }

    /// <summary>
    /// Displays a dialog that lets the user select a folder.
    /// </summary>
    /// <param name="window">The window that the dialog should be modal for, or null.</param>
    /// <param name="defaultLocation">The default folder to start the dialog at, or null.</param>
    /// <param name="allowMultiple">If true, the user can select multiple folders.</param>
    /// <returns>A task that completes with the dialog result.</returns>
    public static Task<FileDialogResult> ShowOpenFolderDialogAsync(
        Window? window = null,
        string? defaultLocation = null,
        bool allowMultiple = false)
    {
        var tcs = new TaskCompletionSource<FileDialogResult>();
        ShowOpenFolderDialog(result => tcs.SetResult(result), window, defaultLocation, allowMultiple);
        return tcs.Task;
    }

    private delegate void ShowDialogDelegate(
        delegate* unmanaged[Cdecl]<nuint, byte**, int, void> callback,
        nuint userdata,
        SDL_Window* window,
        SDL_DialogFileFilter* filters,
        int nfilters,
        string? defaultLocation,
        bool allowMany);

    private static void ShowDialogCore(
        Action<FileDialogResult> callback,
        Window? window,
        IReadOnlyList<DialogFileFilter>? filters,
        string? defaultLocation,
        bool allowMultiple,
        ShowDialogDelegate showDialog)
    {
        var callbackState = new DialogCallbackState(callback, filters);
        var handle = GCHandle.Alloc(callbackState);
        var windowHandle = window == null ? null : window.Handle;

        if (filters is { Count: > 0 })
        {
            fixed (SDL_DialogFileFilter* filtersPtr = callbackState.NativeFilters)
            {
                showDialog(
                    &DialogCallbackWithFilters,
                    (nuint)(nint)GCHandle.ToIntPtr(handle),
                    windowHandle,
                    filtersPtr,
                    filters.Count,
                    defaultLocation,
                    allowMultiple);
            }
        }
        else
        {
            showDialog(
                &DialogCallback,
                (nuint)(nint)GCHandle.ToIntPtr(handle),
                windowHandle,
                null,
                0,
                defaultLocation,
                allowMultiple);
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void DialogCallback(nuint userdata, byte** filelist, int filter)
    {
        var handle = GCHandle.FromIntPtr((nint)userdata);

        try
        {
            var files = ParseFileList(filelist);
            var result = new FileDialogResult(files, filter);

            if (handle.Target is Action<FileDialogResult> callback)
            {
                callback(result);
            }
        }
        finally
        {
            handle.Free();
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void DialogCallbackWithFilters(nuint userdata, byte** filelist, int filter)
    {
        var handle = GCHandle.FromIntPtr((nint)userdata);

        try
        {
            var files = ParseFileList(filelist);
            var result = new FileDialogResult(files, filter);

            if (handle.Target is DialogCallbackState state)
            {
                state.FreeNativeFilters();
                state.Callback(result);
            }
        }
        finally
        {
            handle.Free();
        }
    }

    private static string[] ParseFileList(byte** filelist)
    {
        if (filelist == null)
        {
            throw new SdlException();
        }

        if (*filelist == null)
        {
            return [];
        }

        var files = new List<string>();
        for (var i = 0; filelist[i] != null; i++)
        {
            var str = Marshal.PtrToStringUTF8((nint)filelist[i]);
            if (str != null)
            {
                files.Add(str);
            }
        }

        return [.. files];
    }

    private sealed class DialogCallbackState
    {
        public Action<FileDialogResult> Callback { get; }
        public SDL_DialogFileFilter[]? NativeFilters { get; }

        private readonly nint[]? _allocatedStrings;

        public DialogCallbackState(Action<FileDialogResult> callback, IReadOnlyList<DialogFileFilter>? filters)
        {
            Callback = callback;

            if (filters is { Count: > 0 })
            {
                NativeFilters = new SDL_DialogFileFilter[filters.Count];
                _allocatedStrings = new nint[filters.Count * 2];

                for (var i = 0; i < filters.Count; i++)
                {
                    var nameBytes = Encoding.UTF8.GetBytes(filters[i].Name + '\0');
                    var patternBytes = Encoding.UTF8.GetBytes(filters[i].Pattern + '\0');

                    var namePtr = Marshal.AllocHGlobal(nameBytes.Length);
                    var patternPtr = Marshal.AllocHGlobal(patternBytes.Length);

                    Marshal.Copy(nameBytes, 0, namePtr, nameBytes.Length);
                    Marshal.Copy(patternBytes, 0, patternPtr, patternBytes.Length);

                    _allocatedStrings[i * 2] = namePtr;
                    _allocatedStrings[i * 2 + 1] = patternPtr;

                    NativeFilters[i] = new SDL_DialogFileFilter
                    {
                        name = (byte*)namePtr,
                        pattern = (byte*)patternPtr
                    };
                }
            }
        }

        public void FreeNativeFilters()
        {
            if (_allocatedStrings != null)
            {
                foreach (var ptr in _allocatedStrings)
                {
                    if (ptr != 0)
                    {
                        Marshal.FreeHGlobal(ptr);
                    }
                }
            }
        }
    }
}
