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
    private sealed record Pending(Action<DialogResult> Callback, IDisposable? ParentLease, IDisposable Operation, FilterBuffer Filters);
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000", Justification = "The lease is transferred to the returned scope or lifetime cleanup, which releases it exactly once.")]
    private static CallbackHandle Allocate(Window? parent, Action<DialogResult> callback, DialogFileFilter[]? filters = null)
    {
        ArgumentNullException.ThrowIfNull(callback);
        if (parent != null) _ = parent.Handle;
        var buffer = new FilterBuffer(filters);
        IDisposable? lease = null;
        IDisposable? operation = null;
        try
        {
            lease = parent?.Lifetime.Retain();
            operation = Application.BeginOperation();
            return CallbackHandle.Alloc(new Pending(callback, lease, operation, buffer));
        }
        catch { buffer.Dispose(); lease?.Dispose(); operation?.Dispose(); throw; }
    }
    private static void Release(CallbackHandle handle)
    {
        if (handle.Target is Pending pending)
        {
            handle.Free();
            pending.Filters.Dispose();
            pending.ParentLease?.Dispose();
            pending.Operation.Dispose();
        }
    }
    // --- Async API ---

    /// <summary>Shows an "Open File" dialog asynchronously.</summary>
    public static Task<DialogResult> OpenFileAsync(
        Window? parent = null,
        DialogFileFilter[]? filters = null,
        string? defaultLocation = null,
        bool allowMany = false)
    {
        var tcs = new TaskCompletionSource<DialogResult>(TaskCreationOptions.RunContinuationsAsynchronously);
        OpenFile(parent, filters, defaultLocation, allowMany, result => { if (result.Error is { } error) tcs.TrySetException(error); else tcs.TrySetResult(result); });
        return tcs.Task;
    }

    /// <summary>Shows a "Save File" dialog asynchronously.</summary>
    public static Task<DialogResult> SaveFileAsync(
        Window? parent = null,
        DialogFileFilter[]? filters = null,
        string? defaultLocation = null)
    {
        var tcs = new TaskCompletionSource<DialogResult>(TaskCreationOptions.RunContinuationsAsynchronously);
        SaveFile(parent, filters, defaultLocation, result => { if (result.Error is { } error) tcs.TrySetException(error); else tcs.TrySetResult(result); });
        return tcs.Task;
    }

    /// <summary>Shows an "Open Folder" dialog asynchronously.</summary>
    public static Task<DialogResult> OpenFolderAsync(
        Window? parent = null,
        string? defaultLocation = null,
        bool allowMany = false)
    {
        var tcs = new TaskCompletionSource<DialogResult>(TaskCreationOptions.RunContinuationsAsynchronously);
        OpenFolder(parent, defaultLocation, allowMany, result => { if (result.Error is { } error) tcs.TrySetException(error); else tcs.TrySetResult(result); });
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
        if (parent != null) _ = parent.Handle;
        var handle = Allocate(parent, callback, filters);
        var buffer = ((Pending)handle.Target!).Filters;
        try
        {
            {
                SDL_ShowOpenFileDialog(
                    &DialogCallback,
                    (void*)(nint)handle,
                    parent != null ? parent.Handle : null,
                    buffer.Pointer,
                    buffer.Count,
                    ToUtf8(defaultLocation),
                    allowMany);
            }
        }
        catch { Release(handle); throw; }
    }

    /// <summary>Shows a "Save File" dialog with a callback.</summary>
    public static void SaveFile(
        Window? parent,
        DialogFileFilter[]? filters,
        string? defaultLocation,
        Action<DialogResult> callback)
    {
        if (parent != null) _ = parent.Handle;
        var handle = Allocate(parent, callback, filters);
        var buffer = ((Pending)handle.Target!).Filters;
        try
        {
            {
                SDL_ShowSaveFileDialog(
                    &DialogCallback,
                    (void*)(nint)handle,
                    parent != null ? parent.Handle : null,
                    buffer.Pointer,
                    buffer.Count,
                    ToUtf8(defaultLocation));
            }
        }
        catch { Release(handle); throw; }
    }

    /// <summary>Shows an "Open Folder" dialog with a callback.</summary>
    public static void OpenFolder(
        Window? parent,
        string? defaultLocation,
        bool allowMany,
        Action<DialogResult> callback)
    {
        var handle = Allocate(parent, callback);
        try { SDL_ShowOpenFolderDialog(
            &DialogCallback,
            (void*)(nint)handle,
            parent != null ? parent.Handle : null,
            ToUtf8(defaultLocation),
            allowMany); }
        catch { Release(handle); throw; }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void DialogCallback(void* userdata, byte** filelist, int filter)
    {
        var handle = CallbackHandle.FromIntPtr((nint)userdata);
        if (handle.Target is not Pending pending) return;
        var callback = pending.Callback;
        try
        {
            using var scope = CallbackErrors.Enter();
            if (filelist == null) { callback(new DialogResult(null, filter) { Error = new SdlException() }); return; }
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
        catch (Exception error) { CallbackErrors.Capture(error); }
        finally { Release(handle); }
    }

    // SDL retains both the array and its UTF-8 strings until completion.
    internal sealed class FilterBuffer : IDisposable
    {
        private GCHandle[]? _pins;
        private readonly nint _pointer;
        internal int Count { get; }
        internal SDL_DialogFileFilter* Pointer
        {
            get { ObjectDisposedException.ThrowIf(_pins == null, this); return (SDL_DialogFileFilter*)_pointer; }
        }
        internal FilterBuffer(DialogFileFilter[]? filters)
        {
            Count = filters?.Length ?? 0;
            _pins = new GCHandle[checked(Count * 2 + 1)];
            if (Count == 0) return;
            try
            {
                var native = new SDL_DialogFileFilter[Count];
                for (var i = 0; i < Count; i++)
                {
                    ArgumentNullException.ThrowIfNull(filters![i].Name);
                    ArgumentNullException.ThrowIfNull(filters[i].Pattern);
                    _pins[i * 2] = GCHandle.Alloc(ToUtf8(filters[i].Name)!, GCHandleType.Pinned);
                    _pins[i * 2 + 1] = GCHandle.Alloc(ToUtf8(filters[i].Pattern)!, GCHandleType.Pinned);
                    native[i].name = (byte*)_pins[i * 2].AddrOfPinnedObject();
                    native[i].pattern = (byte*)_pins[i * 2 + 1].AddrOfPinnedObject();
                }
                _pins[^1] = GCHandle.Alloc(native, GCHandleType.Pinned);
                _pointer = _pins[^1].AddrOfPinnedObject();
            }
            catch { Dispose(); throw; }
        }
        public void Dispose()
        {
            var pins = Interlocked.Exchange(ref _pins, null);
            if (pins == null) return;
            foreach (var pin in pins) if (pin.IsAllocated) pin.Free();
        }
    }
    /// <summary>Shows a dialog configured through SDL properties, with managed filters retained until completion.</summary>
    /// <remarks>Pass filters through <paramref name="filters"/>; raw filter pointers in the properties are rejected.</remarks>
    public static Task<DialogResult> ShowAsync(FileDialogType type, PropertyGroup properties, DialogFileFilter[]? filters = null)
    {
        if (properties.GetPointer("SDL.filedialog.filters") != 0)
            throw new ArgumentException("Use the managed filters argument instead of a raw filter pointer.", nameof(properties));
        using var configuration = new PropertyGroup();
        properties.CopyTo(configuration);
        var completion = new TaskCompletionSource<DialogResult>(TaskCreationOptions.RunContinuationsAsynchronously);
        var parentPointer = properties.GetPointer("SDL.filedialog.window");
        using var parent = parentPointer == 0 ? null : new Window((SDL_Window*)parentPointer, false);
        var handle = Allocate(parent, result => { if (result.Error is { } error) completion.TrySetException(error); else completion.TrySetResult(result); }, filters);
        try
        {
            var buffer = ((Pending)handle.Target!).Filters;
            configuration.SetPointer("SDL.filedialog.filters", (nint)buffer.Pointer);
            configuration.SetNumber("SDL.filedialog.nfilters", buffer.Count);
            SDL_ShowFileDialogWithProperties((SDL_FileDialogType)type, &DialogCallback, (void*)(nint)handle, configuration.Id);
        }
        catch { Release(handle); throw; }
        return completion.Task;
    }
}
