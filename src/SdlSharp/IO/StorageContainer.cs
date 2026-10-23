using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using SdlSharp.Native;
using static SdlSharp.Native.Common;
using static SdlSharp.Native.Storage;

namespace SdlSharp.IO;

/// <summary>An owned SDL title, user, local, or custom storage container.</summary>
/// <remarks>Use relative paths with '/' separators and no '.' or '..' components. Operations are synchronous and serialized. Poll <see cref="IsReady"/> while processing events before accessing a backend that mounts asynchronously. Dispose user storage promptly after a save batch so cloud backends can flush. No native handles or lazy iterators escape the container.</remarks>
public sealed unsafe class StorageContainer : IDisposable
{
    private readonly object _gate = new();
    private SDL_Storage* _handle;
    private readonly StorageBridge? _bridge;
    private bool _active;
    internal readonly NativeLifetime Lifetime;
    private StorageContainer(SDL_Storage* handle, StorageBridge? bridge = null)
    {
        Lifetime = NativeLifetime.Get(typeof(StorageContainer), (nint)handle, true);
        _handle = handle;
        _bridge = bridge;
    }
    private SDL_Storage* Handle
    {
        get { ObjectDisposedException.ThrowIf(_handle == null, this); Lifetime.Check(); return _handle; }
    }
    private SdlScope Enter()
    {
        _ = Handle;
        if (_active) throw new InvalidOperationException("A storage callback cannot reenter its container.");
        _active = true;
        return new SdlScope(() => _active = false, threadAffine: false);
    }
    private void CheckOperation(bool success)
    {
        _bridge?.ThrowPending();
        Check(success);
    }
    internal static byte[] PathBytes(string path, bool rootAllowed = false)
    {
        ArgumentNullException.ThrowIfNull(path);
        if ((!rootAllowed && path.Length == 0) || path.StartsWith('/') || path.Contains('\\') || path.Contains('\0') || path.Split('/').Any(part => part is "." or ".."))
            throw new ArgumentException("Use a relative storage path with '/' separators and no null, '.' or '..' components.", nameof(path));
        return new System.Text.UTF8Encoding(false, true).GetBytes(path + '\0');
    }
    private static byte[]? NativePath(string? path)
    {
        if (path?.Contains('\0') == true) throw new ArgumentException("Embedded null characters are not allowed.", nameof(path));
        return ToUtf8(path);
    }

    /// <summary>Opens read-only application content, optionally overriding its native filesystem root.</summary>
    public static StorageContainer OpenTitle(string? rootPath = null, PropertyGroup? properties = null) =>
        new(Check(SDL_OpenTitleStorage(NativePath(rootPath), properties?.Id ?? default)));
    /// <summary>Opens a user's writable save-data container. Dispose promptly to flush the batch.</summary>
    public static StorageContainer OpenUser(string organization, string application, PropertyGroup? properties = null)
    {
        ArgumentException.ThrowIfNullOrEmpty(organization);
        ArgumentException.ThrowIfNullOrEmpty(application);
        return new(Check(SDL_OpenUserStorage(NativePath(organization), NativePath(application), properties?.Id ?? default)));
    }
    /// <summary>Opens local filesystem storage for tools/development, rooted at a native filesystem path.</summary>
    public static StorageContainer OpenLocal(string rootPath)
    {
        ArgumentException.ThrowIfNullOrEmpty(rootPath);
        return new(Check(SDL_OpenFileStorage(NativePath(rootPath))));
    }
    /// <summary>Opens a custom managed backend. Ownership transfers only on success; leaveOpen keeps the backend caller-owned.</summary>
    public static StorageContainer Open(StorageBackend backend, bool leaveOpen = false)
    {
        ArgumentNullException.ThrowIfNull(backend);
        var bridge = new StorageBridge(backend, leaveOpen);
        try
        {
            var callbacks = bridge.Interface;
            var pointer = Check(SDL_OpenStorage(&callbacks, bridge.UserData));
            try
            {
                var container = new StorageContainer(pointer, bridge);
                bridge.CommitOwnership();
                return container;
            }
            catch { SDL_CloseStorage(pointer); throw; }
        }
        catch { bridge.Release(); throw; }
    }

    /// <summary>Gets whether the storage is ready without spinning or blocking the event loop.</summary>
    public bool IsReady
    {
        get { lock (_gate) { using var call = Enter(); var result = SDL_StorageReady(Handle); _bridge?.ThrowPending(); return result; } }
    }
    /// <summary>Gets available bytes; some backends return <see cref="ulong.MaxValue"/> when capacity is unknown.</summary>
    public ulong SpaceRemaining
    {
        get { lock (_gate) { using var call = Enter(); var result = SDL_GetStorageSpaceRemaining(Handle); _bridge?.ThrowPending(); return result; } }
    }
    /// <summary>Gets a file's size in bytes.</summary>
    public ulong GetFileSize(string path)
    {
        lock (_gate) { using var call = Enter(); CheckOperation(SDL_GetStorageFileSize(Handle, PathBytes(path), out var size)); return size; }
    }
    /// <summary>Reads a whole file into an exact-sized buffer; a mismatched length is rejected before reading.</summary>
    public void ReadFile(string path, Span<byte> destination)
    {
        lock (_gate)
        {
            using var call = Enter();
            var encoded = PathBytes(path);
            CheckOperation(SDL_GetStorageFileSize(Handle, encoded, out var size));
            if (size != (ulong)destination.Length) throw new ArgumentException("The destination length must equal the file size.", nameof(destination));
            byte empty = 0;
            fixed (byte* data = destination) CheckOperation(SDL_ReadStorageFile(Handle, encoded, data == null ? &empty : data, size));
        }
    }
    /// <summary>Reads an entire file into a detached byte array. Files exceeding the maximum managed array length are rejected.</summary>
    public byte[] ReadAllBytes(string path)
    {
        lock (_gate)
        {
            using var call = Enter();
            var encoded = PathBytes(path);
            CheckOperation(SDL_GetStorageFileSize(Handle, encoded, out var size));
            if (size > (ulong)Array.MaxLength) throw new IOException("The file is too large for a managed byte array.");
            var result = new byte[(int)size];
            byte empty = 0;
            fixed (byte* data = result) CheckOperation(SDL_ReadStorageFile(Handle, encoded, data == null ? &empty : data, size));
            return result;
        }
    }
    /// <summary>Writes an entire file from a temporary span.</summary>
    public void WriteAllBytes(string path, ReadOnlySpan<byte> source)
    {
        lock (_gate)
        {
            using var call = Enter();
            var encoded = PathBytes(path);
            byte empty = 0;
            fixed (byte* data = source) CheckOperation(SDL_WriteStorageFile(Handle, encoded, data == null ? &empty : data, (ulong)source.Length));
        }
    }
    /// <summary>Creates a directory.</summary>
    public void CreateDirectory(string path)
    {
        lock (_gate) { using var call = Enter(); CheckOperation(SDL_CreateStorageDirectory(Handle, PathBytes(path))); }
    }
    /// <summary>Removes a file or empty directory.</summary>
    public void Remove(string path)
    {
        lock (_gate) { using var call = Enter(); CheckOperation(SDL_RemoveStoragePath(Handle, PathBytes(path))); }
    }
    /// <summary>Renames a path, replacing the destination when supported.</summary>
    public void Rename(string oldPath, string newPath)
    {
        lock (_gate) { using var call = Enter(); CheckOperation(SDL_RenameStoragePath(Handle, PathBytes(oldPath), PathBytes(newPath))); }
    }
    /// <summary>Copies a file, replacing the destination when supported. The operation is not atomic.</summary>
    public void CopyFile(string oldPath, string newPath)
    {
        lock (_gate) { using var call = Enter(); CheckOperation(SDL_CopyStorageFile(Handle, PathBytes(oldPath), PathBytes(newPath))); }
    }
    /// <summary>Gets copied path metadata, throwing on native failure.</summary>
    public StoragePathInfo GetPathInfo(string path)
    {
        lock (_gate) { using var call = Enter(); SDL_PathInfo info; CheckOperation(SDL_GetStoragePathInfo(Handle, PathBytes(path, true), &info)); return StoragePathInfo.FromNative(info); }
    }
    /// <summary>Checks whether a path exists. Native access errors also return false; use GetPathInfo to obtain an error. Exceptions from custom managed backends are rethrown.</summary>
    public bool Exists(string path)
    {
        lock (_gate) { using var call = Enter(); var result = SDL_GetStoragePathInfo(Handle, PathBytes(path, true), null); _bridge?.ThrowPending(); return result; }
    }

    private sealed class Visitor(Func<string, bool> callback)
    {
        internal readonly Func<string, bool> Callback = callback;
        internal ExceptionDispatchInfo? Error;
    }
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static SDL_EnumerationResult Visit(void* userdata, byte* directory, byte* filename)
    {
        var state = (Visitor)CallbackHandle.FromIntPtr((nint)userdata).Target!;
        try
        {
            using var callback = CallbackErrors.Enter();
            return state.Callback((Marshal.PtrToStringUTF8((nint)directory) ?? string.Empty) + Marshal.PtrToStringUTF8((nint)filename))
                ? SDL_EnumerationResult.SDL_ENUM_CONTINUE : SDL_EnumerationResult.SDL_ENUM_SUCCESS;
        }
        catch (Exception error) { state.Error ??= ExceptionDispatchInfo.Capture(error); return SDL_EnumerationResult.SDL_ENUM_FAILURE; }
    }
    /// <summary>Visits copied relative entry paths synchronously; return false to stop successfully. Exceptions are rethrown after the native call returns.</summary>
    public void VisitEntries(Func<string, bool> visitor, string directory = "")
    {
        ArgumentNullException.ThrowIfNull(visitor);
        lock (_gate)
        {
            using var call = Enter();
            var path = PathBytes(directory, true);
            var state = new Visitor(visitor);
            var token = CallbackHandle.Alloc(state);
            try
            {
                var result = SDL_EnumerateStorageDirectory(Handle, path, &Visit, (void*)(nint)token);
                var backendError = _bridge?.TakePending();
                state.Error?.Throw();
                backendError?.Throw();
                Check(result);
            }
            finally { token.Free(); }
        }
    }
    /// <summary>Returns an eager, detached list of immediate entry paths.</summary>
    public string[] GetEntries(string directory = "")
    {
        var result = new List<string>();
        VisitEntries(path => { result.Add(path); return true; }, directory);
        return result.ToArray();
    }
    /// <summary>Returns copied paths matching SDL's '*' and '?' glob syntax; wildcards do not match '/'. A null pattern matches all entries.</summary>
    public string[] Glob(string? pattern = null, string directory = "", bool ignoreCase = false)
    {
        lock (_gate)
        {
            using var call = Enter();
            var result = SDL_GlobStorageDirectory(Handle, PathBytes(directory, true), NativePath(pattern), ignoreCase ? SDL_GlobFlags.SDL_GLOB_CASEINSENSITIVE : 0, out var count);
            try
            {
                _bridge?.ThrowPending();
                if (result == null) throw new SdlException();
                var paths = new string[count];
                for (var i = 0; i < count; i++) paths[i] = Marshal.PtrToStringUTF8((nint)result[i])!;
                return paths;
            }
            finally { SDL_free(result); }
        }
    }

    /// <summary>Closes and flushes once. Even when flushing throws, the native container is freed and subsequent disposal is a no-op.</summary>
    public void Dispose()
    {
        lock (_gate)
        {
            if (_handle == null) return;
            if (_active) throw new InvalidOperationException("A storage callback cannot close its container.");
            SdlException? failure = null;
            Lifetime.Release(() => { if (!SDL_CloseStorage(_handle)) failure = new SdlException(); });
            _handle = null;
            _bridge?.Release();
            _bridge?.ThrowPending();
            if (failure != null) throw failure;
        }
    }
}
