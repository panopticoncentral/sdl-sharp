using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SdlSharp.Native;

using static SdlSharp.Native.Common;
using static SdlSharp.Native.Dialog;

namespace SdlSharp.Graphics;

/// <summary>
/// File open/save/folder dialogs.
/// </summary>
public static unsafe class FileDialog
{
    // --- Async API ---

    /// <summary>Shows an "Open File" dialog asynchronously.</summary>
    public static Task<DialogResult> OpenFileAsync(
        Window? parent = null,
        DialogFileFilter[]? filters = null,
        string? defaultLocation = null,
        bool allowMany = false)
    {
        var tcs = new TaskCompletionSource<DialogResult>();
        OpenFile(parent, filters, defaultLocation, allowMany, result => tcs.SetResult(result));
        return tcs.Task;
    }

    /// <summary>Shows a "Save File" dialog asynchronously.</summary>
    public static Task<DialogResult> SaveFileAsync(
        Window? parent = null,
        DialogFileFilter[]? filters = null,
        string? defaultLocation = null)
    {
        var tcs = new TaskCompletionSource<DialogResult>();
        SaveFile(parent, filters, defaultLocation, result => tcs.SetResult(result));
        return tcs.Task;
    }

    /// <summary>Shows an "Open Folder" dialog asynchronously.</summary>
    public static Task<DialogResult> OpenFolderAsync(
        Window? parent = null,
        string? defaultLocation = null,
        bool allowMany = false)
    {
        var tcs = new TaskCompletionSource<DialogResult>();
        OpenFolder(parent, defaultLocation, allowMany, result => tcs.SetResult(result));
        return tcs.Task;
    }

    // --- Callback API ---

    /// <summary>Shows an "Open File" dialog with a callback.</summary>
    public static void OpenFile(
        Window? parent,
        DialogFileFilter[]? filters,
        string? defaultLocation,
        bool allowMany,
        Action<DialogResult> callback)
    {
        var handle = GCHandle.Alloc(callback);
        var nativeFilters = MarshalFilters(filters, out var pins);
        try
        {
            fixed (SDL_DialogFileFilter* filterPtr = nativeFilters)
            {
                SDL_ShowOpenFileDialog(
                    &DialogCallback,
                    (void*)(nint)handle,
                    parent != null ? parent.Handle : null,
                    filterPtr,
                    nativeFilters?.Length ?? 0,
                    ToUtf8(defaultLocation),
                    allowMany);
            }
        }
        finally
        {
            FreePins(pins);
        }
    }

    /// <summary>Shows a "Save File" dialog with a callback.</summary>
    public static void SaveFile(
        Window? parent,
        DialogFileFilter[]? filters,
        string? defaultLocation,
        Action<DialogResult> callback)
    {
        var handle = GCHandle.Alloc(callback);
        var nativeFilters = MarshalFilters(filters, out var pins);
        try
        {
            fixed (SDL_DialogFileFilter* filterPtr = nativeFilters)
            {
                SDL_ShowSaveFileDialog(
                    &DialogCallback,
                    (void*)(nint)handle,
                    parent != null ? parent.Handle : null,
                    filterPtr,
                    nativeFilters?.Length ?? 0,
                    ToUtf8(defaultLocation));
            }
        }
        finally
        {
            FreePins(pins);
        }
    }

    /// <summary>Shows an "Open Folder" dialog with a callback.</summary>
    public static void OpenFolder(
        Window? parent,
        string? defaultLocation,
        bool allowMany,
        Action<DialogResult> callback)
    {
        var handle = GCHandle.Alloc(callback);
        SDL_ShowOpenFolderDialog(
            &DialogCallback,
            (void*)(nint)handle,
            parent != null ? parent.Handle : null,
            ToUtf8(defaultLocation),
            allowMany);
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void DialogCallback(void* userdata, byte** filelist, int filter)
    {
        var handle = GCHandle.FromIntPtr((nint)userdata);
        var callback = (Action<DialogResult>)handle.Target!;
        try
        {
            string[]? files = null;
            if (filelist != null)
            {
                var list = new List<string>();
                for (var i = 0; filelist[i] != null; i++)
                {
                    var s = Marshal.PtrToStringUTF8((nint)filelist[i]);
                    if (s != null) list.Add(s);
                }
                files = list.Count > 0 ? list.ToArray() : null;
            }
            callback(new DialogResult(files, filter));
        }
        finally
        {
            handle.Free();
        }
    }

    private static SDL_DialogFileFilter[]? MarshalFilters(
        DialogFileFilter[]? filters, out GCHandle[] pins)
    {
        if (filters == null || filters.Length == 0)
        {
            pins = [];
            return null;
        }

        var native = new SDL_DialogFileFilter[filters.Length];
        pins = new GCHandle[filters.Length * 2];
        for (var i = 0; i < filters.Length; i++)
        {
            var nameBytes = ToUtf8(filters[i].Name)!;
            var patternBytes = ToUtf8(filters[i].Pattern)!;
            pins[i * 2] = GCHandle.Alloc(nameBytes, GCHandleType.Pinned);
            pins[i * 2 + 1] = GCHandle.Alloc(patternBytes, GCHandleType.Pinned);
            native[i].name = (byte*)pins[i * 2].AddrOfPinnedObject();
            native[i].pattern = (byte*)pins[i * 2 + 1].AddrOfPinnedObject();
        }
        return native;
    }

    private static void FreePins(GCHandle[] pins)
    {
        foreach (var pin in pins)
            if (pin.IsAllocated) pin.Free();
    }
}
