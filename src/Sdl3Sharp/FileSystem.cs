using System.Runtime.InteropServices;

using static Sdl3Sharp.Native.Filesystem;
using static Sdl3Sharp.Native.StdInc;
using static Sdl3Sharp.Native.Common;

namespace Sdl3Sharp;

/// <summary>
/// Provides methods for examining and manipulating the system's filesystem.
/// </summary>
public static unsafe class FileSystem
{
    /// <summary>
    /// Gets the directory where the application was run from.
    /// SDL caches the result of this call internally, but the first call to this
    /// method is not necessarily fast, so plan accordingly.
    /// The returned path is guaranteed to end with a path separator.
    /// </summary>
    /// <returns>An absolute path to the application data directory.</returns>
    /// <exception cref="SdlException">Thrown if the path cannot be determined.</exception>
    public static string GetBasePath()
    {
        var ptr = CheckErrorPointer(SDL_GetBasePath());
        return Marshal.PtrToStringUTF8((nint)ptr) ?? string.Empty;
    }

    /// <summary>
    /// Get the user-and-app-specific path where files can be written.
    /// This is meant to be where users can write personal files (preferences and save games, etc)
    /// that are specific to your application. This directory is unique per user, per application.
    /// This method will decide the appropriate location in the native filesystem, create the
    /// directory if necessary, and return a string of the absolute path to the directory.
    /// The returned path is guaranteed to end with a path separator.
    /// </summary>
    /// <param name="org">The name of your organization.</param>
    /// <param name="app">The name of your application.</param>
    /// <returns>The user directory in platform-dependent notation.</returns>
    /// <exception cref="SdlException">Thrown if there's a problem (creating directory failed, etc.).</exception>
    public static string GetPrefPath(string org, string app)
    {
        var ptr = CheckErrorPointer(SDL_GetPrefPath(org, app));
        try
        {
            return Marshal.PtrToStringUTF8((nint)ptr) ?? string.Empty;
        }
        finally
        {
            SDL_free(ptr);
        }
    }

    /// <summary>
    /// Finds the most suitable user folder for a specific purpose.
    /// Many OSes provide certain standard folders for certain purposes, such as storing pictures,
    /// music or videos for a certain user. This method gives the path for many of those special locations.
    /// The returned path is guaranteed to end with a path separator.
    /// </summary>
    /// <param name="folder">The type of folder to find.</param>
    /// <returns>The full path to the folder.</returns>
    /// <exception cref="SdlException">Thrown if an error happened.</exception>
    public static string GetUserFolder(Folder folder)
    {
        var ptr = CheckErrorPointer(SDL_GetUserFolder((SDL_Folder)folder));
        return Marshal.PtrToStringUTF8((nint)ptr) ?? string.Empty;
    }

    /// <summary>
    /// Create a directory, and any missing parent directories.
    /// This reports success if path already exists as a directory.
    /// If parent directories are missing, it will also create them. Note that if
    /// this fails, it will not remove any parent directories it already made.
    /// </summary>
    /// <param name="path">The path of the directory to create.</param>
    /// <exception cref="SdlException">Thrown on failure.</exception>
    public static void CreateDirectory(string path)
    {
        CheckErrorBool(SDL_CreateDirectory(path));
    }

    /// <summary>
    /// Enumerate a directory through a callback function.
    /// This method provides every directory entry through an app-provided callback,
    /// called once for each directory entry, until all results have been provided or
    /// the callback returns false to stop enumeration.
    /// </summary>
    /// <param name="path">The path of the directory to enumerate.</param>
    /// <param name="callback">A function that is called for each entry in the directory. Return true to continue enumeration, false to stop.</param>
    /// <exception cref="SdlException">Thrown if there was a system problem in general, or if a callback returns false.</exception>
    public static void EnumerateDirectory(string path, Func<string, string, bool> callback)
    {
        var handle = GCHandle.Alloc(callback);
        try
        {
            var result = SDL_EnumerateDirectory(path, &EnumerateDirectoryCallback, (nuint)GCHandle.ToIntPtr(handle));
            CheckErrorBool(result);
        }
        finally
        {
            handle.Free();
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static SDL_EnumerationResult EnumerateDirectoryCallback(nuint userdata, byte* dirname, byte* fname)
    {
        try
        {
            var handle = GCHandle.FromIntPtr((nint)userdata);
            var callback = (Func<string, string, bool>)handle.Target!;
            var dirnameStr = Marshal.PtrToStringUTF8((nint)dirname) ?? string.Empty;
            var fnameStr = Marshal.PtrToStringUTF8((nint)fname) ?? string.Empty;
            return callback(dirnameStr, fnameStr) ? SDL_EnumerationResult.SDL_ENUM_CONTINUE : SDL_EnumerationResult.SDL_ENUM_SUCCESS;
        }
        catch
        {
            return SDL_EnumerationResult.SDL_ENUM_FAILURE;
        }
    }

    /// <summary>
    /// Remove a file or an empty directory.
    /// Directories that are not empty will fail; this method will not recursively delete directory trees.
    /// </summary>
    /// <param name="path">The path to remove from the filesystem.</param>
    /// <exception cref="SdlException">Thrown on failure.</exception>
    public static void RemovePath(string path)
    {
        CheckErrorBool(SDL_RemovePath(path));
    }

    /// <summary>
    /// Rename a file or directory.
    /// If the file at newpath already exists, it will be replaced.
    /// Note that this will not copy files across filesystems/drives/volumes, as that is
    /// a much more complicated (and possibly time-consuming) operation.
    /// </summary>
    /// <param name="oldPath">The old path.</param>
    /// <param name="newPath">The new path.</param>
    /// <exception cref="SdlException">Thrown on failure.</exception>
    public static void RenamePath(string oldPath, string newPath)
    {
        CheckErrorBool(SDL_RenamePath(oldPath, newPath));
    }

    /// <summary>
    /// Copy a file.
    /// If the file at newpath already exists, it will be overwritten with the contents
    /// of the file at oldpath.
    /// This method will block until the copy is complete, which might be a significant
    /// time for large files on slow disks.
    /// </summary>
    /// <param name="oldPath">The old path.</param>
    /// <param name="newPath">The new path.</param>
    /// <exception cref="SdlException">Thrown on failure.</exception>
    public static void CopyFile(string oldPath, string newPath)
    {
        CheckErrorBool(SDL_CopyFile(oldPath, newPath));
    }

    /// <summary>
    /// Get information about a filesystem path.
    /// </summary>
    /// <param name="path">The path to query.</param>
    /// <returns>Information about the path.</returns>
    /// <exception cref="SdlException">Thrown if the file doesn't exist, or another failure.</exception>
    public static PathInfo GetPathInfo(string path)
    {
        Native.Filesystem.SDL_PathInfo info;
        CheckErrorBool(SDL_GetPathInfo(path, &info));
        return new PathInfo(info);
    }

    /// <summary>
    /// Check if a path exists.
    /// </summary>
    /// <param name="path">The path to check.</param>
    /// <returns>True if the path exists, false otherwise.</returns>
    public static bool PathExists(string path)
    {
        return SDL_GetPathInfo(path, null);
    }

    /// <summary>
    /// Enumerate a directory tree, filtered by pattern, and return a list.
    /// Files are filtered out if they don't match the string in pattern, which may contain
    /// wildcard characters '*' (match everything) and '?' (match one character). If pattern
    /// is null, no filtering is done and all results are returned. Subdirectories are permitted,
    /// and are specified with a path separator of '/'. Wildcard characters '*' and '?' never
    /// match a path separator.
    /// </summary>
    /// <param name="path">The path of the directory to enumerate.</param>
    /// <param name="pattern">The pattern that files in the directory must match. Can be null.</param>
    /// <param name="flags">Flags that affect this search.</param>
    /// <returns>An array of file paths.</returns>
    /// <exception cref="SdlException">Thrown on failure.</exception>
    public static string[] GlobDirectory(string path, string? pattern = null, GlobFlags flags = GlobFlags.None)
    {
        int count;
        var ptr = CheckErrorPointer(SDL_GlobDirectory(path, pattern, (SDL_GlobFlags)flags, &count));
        try
        {
            var result = new string[count];
            for (var i = 0; i < count; i++)
            {
                result[i] = Marshal.PtrToStringUTF8((nint)ptr[i]) ?? string.Empty;
            }
            return result;
        }
        finally
        {
            SDL_free(ptr);
        }
    }

    /// <summary>
    /// Get what the system believes is the "current working directory."
    /// For systems without a concept of a current working directory, this will still
    /// attempt to provide something reasonable.
    /// The returned path is guaranteed to end with a path separator.
    /// </summary>
    /// <returns>The current working directory in platform-dependent notation.</returns>
    /// <exception cref="SdlException">Thrown if there's a problem.</exception>
    public static string GetCurrentDirectory()
    {
        var ptr = CheckErrorPointer(SDL_GetCurrentDirectory());
        try
        {
            return Marshal.PtrToStringUTF8((nint)ptr) ?? string.Empty;
        }
        finally
        {
            SDL_free(ptr);
        }
    }
}
