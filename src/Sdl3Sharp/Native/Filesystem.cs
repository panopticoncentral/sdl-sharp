using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

using static Sdl3Sharp.Native.Common;

// We are intentionally exposing the P/Invoke calls so people can do low-level calls if needed
#pragma warning disable CA1401 // P/Invokes should not be visible

namespace Sdl3Sharp.Native;

/// <summary>
/// P/Invoke bindings for SDL_filesystem.h - SDL filesystem abstraction.
/// </summary>
public static unsafe partial class Filesystem
{
    /// <summary>
    /// The type of the OS-provided default folder for a specific purpose.
    /// </summary>
    public enum SDL_Folder
    {
        /// <summary>The folder which contains all of the current user's data, preferences, and documents. It usually contains most of the other folders. If a requested folder does not exist, the home folder can be considered a safe fallback to store a user's documents.</summary>
        SDL_FOLDER_HOME,
        /// <summary>The folder of files that are displayed on the desktop. Note that the existence of a desktop folder does not guarantee that the system does show icons on its desktop; certain GNU/Linux distros with a graphical environment may not have desktop icons.</summary>
        SDL_FOLDER_DESKTOP,
        /// <summary>User document files, possibly application-specific. This is a good place to save a user's projects.</summary>
        SDL_FOLDER_DOCUMENTS,
        /// <summary>Standard folder for user files downloaded from the internet.</summary>
        SDL_FOLDER_DOWNLOADS,
        /// <summary>Music files that can be played using a standard music player (mp3, ogg...).</summary>
        SDL_FOLDER_MUSIC,
        /// <summary>Image files that can be displayed using a standard viewer (png, jpg...).</summary>
        SDL_FOLDER_PICTURES,
        /// <summary>Files that are meant to be shared with other users on the same computer.</summary>
        SDL_FOLDER_PUBLICSHARE,
        /// <summary>Save files for games.</summary>
        SDL_FOLDER_SAVEDGAMES,
        /// <summary>Application screenshots.</summary>
        SDL_FOLDER_SCREENSHOTS,
        /// <summary>Template files to be used when the user requests the desktop environment to create a new file in a certain folder, such as "New Text File.txt". Any file in the Templates folder can be used as a starting point for a new file.</summary>
        SDL_FOLDER_TEMPLATES,
        /// <summary>Video files that can be played using a standard video player (mp4, webm...).</summary>
        SDL_FOLDER_VIDEOS,
        /// <summary>Total number of types in this enum, not a folder type by itself.</summary>
        SDL_FOLDER_COUNT
    }

    /// <summary>
    /// Types of filesystem entries.
    /// </summary>
    public enum SDL_PathType
    {
        /// <summary>Path does not exist.</summary>
        SDL_PATHTYPE_NONE,
        /// <summary>A normal file.</summary>
        SDL_PATHTYPE_FILE,
        /// <summary>A directory.</summary>
        SDL_PATHTYPE_DIRECTORY,
        /// <summary>Something completely different like a device node (not a symlink, those are always followed).</summary>
        SDL_PATHTYPE_OTHER
    }

    /// <summary>
    /// Information about a path on the filesystem.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_PathInfo
    {
        /// <summary>The path type.</summary>
        public SDL_PathType type;
        /// <summary>The file size in bytes.</summary>
        public ulong size;
        /// <summary>The time when the path was created.</summary>
        public long create_time;
        /// <summary>The last time the path was modified.</summary>
        public long modify_time;
        /// <summary>The last time the path was read.</summary>
        public long access_time;
    }

    /// <summary>
    /// Possible results from an enumeration callback.
    /// </summary>
    public enum SDL_EnumerationResult
    {
        /// <summary>Value that requests that enumeration continue.</summary>
        SDL_ENUM_CONTINUE,
        /// <summary>Value that requests that enumeration stop, successfully.</summary>
        SDL_ENUM_SUCCESS,
        /// <summary>Value that requests that enumeration stop, as a failure.</summary>
        SDL_ENUM_FAILURE
    }

    /// <summary>
    /// Flags for path matching.
    /// </summary>
    public enum SDL_GlobFlags : uint
    {
        /// <summary>No flags set.</summary>
        SDL_GLOB_NONE = 0,
        /// <summary>Case-insensitive matching.</summary>
        SDL_GLOB_CASEINSENSITIVE = (1u << 0)
    }

    /// <summary>
    /// Get the directory where the application was run from.
    /// </summary>
    /// <returns>An absolute path in UTF-8 encoding to the application data directory. NULL will be returned on error or when the platform doesn't implement this functionality. This is a cached string owned by SDL and should NOT be freed.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte* SDL_GetBasePath();

    /// <summary>
    /// Get the user-and-app-specific path where files can be written.
    /// </summary>
    /// <param name="org">The name of your organization.</param>
    /// <param name="app">The name of your application.</param>
    /// <returns>A UTF-8 string of the user directory in platform-dependent notation. NULL if there's a problem (creating directory failed, etc.). This should be freed with SDL_free() when it is no longer needed.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte* SDL_GetPrefPath([MarshalUsing(typeof(Utf8StringMarshaller))] string org, [MarshalUsing(typeof(Utf8StringMarshaller))] string app);

    /// <summary>
    /// Finds the most suitable user folder for a specific purpose.
    /// </summary>
    /// <param name="folder">The type of folder to find.</param>
    /// <returns>Either a null-terminated C string containing the full path to the folder, or NULL if an error happened. This string is owned by SDL and should NOT be freed.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte* SDL_GetUserFolder(SDL_Folder folder);

    /// <summary>
    /// Create a directory, and any missing parent directories.
    /// </summary>
    /// <param name="path">The path of the directory to create.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_CreateDirectory([MarshalUsing(typeof(Utf8StringMarshaller))] string path);

    /// <summary>
    /// Enumerate a directory through a callback function.
    /// </summary>
    /// <param name="path">The path of the directory to enumerate.</param>
    /// <param name="callback">A function that is called for each entry in the directory.</param>
    /// <param name="userdata">A pointer that is passed to callback.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_EnumerateDirectory([MarshalUsing(typeof(Utf8StringMarshaller))] string path, delegate* unmanaged[Cdecl]<nuint, byte*, byte*, SDL_EnumerationResult> callback, nuint userdata);

    /// <summary>
    /// Remove a file or an empty directory.
    /// </summary>
    /// <param name="path">The path to remove from the filesystem.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_RemovePath([MarshalUsing(typeof(Utf8StringMarshaller))] string path);

    /// <summary>
    /// Rename a file or directory.
    /// </summary>
    /// <param name="oldpath">The old path.</param>
    /// <param name="newpath">The new path.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_RenamePath([MarshalUsing(typeof(Utf8StringMarshaller))] string oldpath, [MarshalUsing(typeof(Utf8StringMarshaller))] string newpath);

    /// <summary>
    /// Copy a file.
    /// </summary>
    /// <param name="oldpath">The old path.</param>
    /// <param name="newpath">The new path.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_CopyFile([MarshalUsing(typeof(Utf8StringMarshaller))] string oldpath, [MarshalUsing(typeof(Utf8StringMarshaller))] string newpath);

    /// <summary>
    /// Get information about a filesystem path.
    /// </summary>
    /// <param name="path">The path to query.</param>
    /// <param name="info">A pointer filled in with information about the path, or NULL to check for the existence of a file.</param>
    /// <returns>True on success or false if the file doesn't exist, or another failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetPathInfo([MarshalUsing(typeof(Utf8StringMarshaller))] string path, SDL_PathInfo* info);

    /// <summary>
    /// Enumerate a directory tree, filtered by pattern, and return a list.
    /// </summary>
    /// <param name="path">The path of the directory to enumerate.</param>
    /// <param name="pattern">The pattern that files in the directory must match. Can be NULL.</param>
    /// <param name="flags">SDL_GLOB_* bitflags that affect this search.</param>
    /// <param name="count">On return, will be set to the number of items in the returned array. Can be NULL.</param>
    /// <returns>An array of strings on success or NULL on failure. This is a single allocation that should be freed with SDL_free() when it is no longer needed.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte** SDL_GlobDirectory([MarshalUsing(typeof(Utf8StringMarshaller))] string path, [MarshalUsing(typeof(Utf8StringMarshaller))] string? pattern, SDL_GlobFlags flags, int* count);

    /// <summary>
    /// Get what the system believes is the "current working directory."
    /// </summary>
    /// <returns>A UTF-8 string of the current working directory in platform-dependent notation. NULL if there's a problem. This should be freed with SDL_free() when it is no longer needed.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte* SDL_GetCurrentDirectory();
}
