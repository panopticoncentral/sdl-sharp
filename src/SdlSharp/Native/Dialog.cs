using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace SdlSharp.Native;

/// <summary>
/// A filter for file dialog operations.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct SDL_DialogFileFilter
{
    /// <summary>Human-readable label for the filter (e.g. "Image files").</summary>
    public byte* name;
    /// <summary>Semicolon-separated list of extensions (e.g. "png;jpg;bmp").</summary>
    public byte* pattern;
}

/// <summary>
/// File dialog type.
/// </summary>
public enum SDL_FileDialogType
{
    /// <summary>Open file dialog.</summary>
    SDL_FILEDIALOG_OPENFILE = 0,
    /// <summary>Save file dialog.</summary>
    SDL_FILEDIALOG_SAVEFILE = 1,
    /// <summary>Open folder dialog.</summary>
    SDL_FILEDIALOG_OPENFOLDER = 2,
}

/// <summary>
/// Native bindings for SDL_dialog.h — file and folder dialogs.
/// </summary>
[SuppressMessage("Interoperability", "CA1401:P/Invokes should not be visible")]
public static partial class Dialog
{
    // Deferred: SDL_ShowFileDialogWithProperties (property-based variant — rarely needed,
    // the specific open/save/folder functions cover common cases).

    /// <summary>Display an "Open File" dialog.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_ShowOpenFileDialog")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_ShowOpenFileDialog(
        delegate* unmanaged[Cdecl]<void*, byte**, int, void> callback,
        void* userdata,
        SDL_Window* window,
        SDL_DialogFileFilter* filters,
        int nfilters,
        ReadOnlySpan<byte> default_location,
        [MarshalAs(UnmanagedType.U1)] bool allow_many);

    /// <summary>Display a "Save File" dialog.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_ShowSaveFileDialog")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_ShowSaveFileDialog(
        delegate* unmanaged[Cdecl]<void*, byte**, int, void> callback,
        void* userdata,
        SDL_Window* window,
        SDL_DialogFileFilter* filters,
        int nfilters,
        ReadOnlySpan<byte> default_location);

    /// <summary>Display an "Open Folder" dialog.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_ShowOpenFolderDialog")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_ShowOpenFolderDialog(
        delegate* unmanaged[Cdecl]<void*, byte**, int, void> callback,
        void* userdata,
        SDL_Window* window,
        ReadOnlySpan<byte> default_location,
        [MarshalAs(UnmanagedType.U1)] bool allow_many);
}
