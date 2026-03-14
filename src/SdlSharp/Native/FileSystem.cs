using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace SdlSharp.Native;

// Deferred: SDL_EnumerateDirectory (callback-based enumeration), SDL_GlobDirectory
// (pattern matching), SDL_RemovePath, SDL_RenamePath, SDL_CopyFile, SDL_GetPathInfo
// (file operations — C# has System.IO which is more capable for these).
// Only wrapping path query functions that provide SDL-specific value.

/// <summary>
/// Known system folder types.
/// </summary>
public enum SDL_Folder
{
    /// <summary>The folder which contains all of the current user's data.</summary>
    SDL_FOLDER_HOME = 0,
    /// <summary>The folder of files on the desktop.</summary>
    SDL_FOLDER_DESKTOP = 1,
    /// <summary>User document files.</summary>
    SDL_FOLDER_DOCUMENTS = 2,
    /// <summary>Standard folder for user downloads.</summary>
    SDL_FOLDER_DOWNLOADS = 3,
    /// <summary>Music files.</summary>
    SDL_FOLDER_MUSIC = 4,
    /// <summary>Image files.</summary>
    SDL_FOLDER_PICTURES = 5,
    /// <summary>Files that are shared with other users.</summary>
    SDL_FOLDER_PUBLICSHARE = 6,
    /// <summary>Save files for games.</summary>
    SDL_FOLDER_SAVEDGAMES = 7,
    /// <summary>Application screenshots.</summary>
    SDL_FOLDER_SCREENSHOTS = 8,
    /// <summary>Template files.</summary>
    SDL_FOLDER_TEMPLATES = 9,
    /// <summary>Video files.</summary>
    SDL_FOLDER_VIDEOS = 10,
}

/// <summary>
/// Native bindings for SDL_filesystem.h — filesystem path utilities.
/// </summary>
[SuppressMessage("Interoperability", "CA1401:P/Invokes should not be visible")]
public static partial class FileSystem
{
    /// <summary>
    /// Get the directory where the application was run from. Returns SDL-owned string.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetBasePath")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* SDL_GetBasePath();

    /// <summary>
    /// Get the user-and-app-specific path where files can be written.
    /// The returned string must be freed with SDL_free.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetPrefPath")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* SDL_GetPrefPath(ReadOnlySpan<byte> org, ReadOnlySpan<byte> app);

    /// <summary>
    /// Finds the most suitable OS-provided folder for a given purpose. Returns SDL-owned string.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetUserFolder")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* SDL_GetUserFolder(SDL_Folder folder);

    /// <summary>
    /// Create a directory, and any missing parent directories.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_CreateDirectory")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_CreateDirectory(ReadOnlySpan<byte> path);

    /// <summary>
    /// Get the current working directory. The returned string must be freed with SDL_free.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetCurrentDirectory")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* SDL_GetCurrentDirectory();
}
