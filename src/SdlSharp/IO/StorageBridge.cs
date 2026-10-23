using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using SdlSharp.Native;
using static SdlSharp.Native.Common;

namespace SdlSharp.IO;

// SDL invokes these callbacks synchronously. The container serializes calls and
// rethrows the captured exception only after returning across the native boundary.
internal sealed unsafe class StorageBridge
{
    private readonly StorageBackend _backend;
    private readonly bool _leaveOpen;
    private readonly CallbackHandle _token;
    private ExceptionDispatchInfo? _error;
    private bool _released;
    private bool _ownsBackend;
    internal void CommitOwnership() => _ownsBackend = !_leaveOpen;
    internal StorageBridge(StorageBackend backend, bool leaveOpen)
    {
        _backend = backend;
        _leaveOpen = leaveOpen;
        backend.Attach();
        try { _token = CallbackHandle.Alloc(this); }
        catch { backend.Detach(); throw; }
    }
    internal void* UserData => (void*)(nint)_token;
    internal SDL_StorageInterface Interface => new()
    {
        version = (uint)sizeof(SDL_StorageInterface), close = &Close, ready = &Ready, enumerate = &Enumerate,
        info = &Info, read_file = &Read, write_file = &Write, mkdir = &CreateDirectory,
        remove = &Remove, rename = &Rename, copy = &Copy, space_remaining = &SpaceRemaining
    };
    internal ExceptionDispatchInfo? TakePending()
    {
        var error = _error;
        _error = null;
        if (error != null) Native.Error.SDL_ClearError();
        return error;
    }
    internal void ThrowPending() => TakePending()?.Throw();
    internal void Release()
    {
        if (_released) return;
        _released = true;
        _token.Free();
        _backend.Detach();
    }
    private static StorageBridge State(void* data) => (StorageBridge)CallbackHandle.FromIntPtr((nint)data).Target!;
    private static string Path(byte* path) => Marshal.PtrToStringUTF8((nint)path) ?? string.Empty;
    private static byte Fail(void* data, Exception error)
    {
        if (CallbackHandle.FromIntPtr((nint)data).Target is StorageBridge state) state._error ??= ExceptionDispatchInfo.Capture(error);
        else CallbackErrors.Capture(error);
        Native.Error.SDL_SetError("Managed storage backend failed"u8);
        return 0;
    }
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static byte Close(void* data)
    {
        try { using var scope = CallbackErrors.Enter(); var state = State(data); if (state._ownsBackend) state._backend.CloseFromContainer(); return 1; }
        catch (Exception error) { return Fail(data, error); }
    }
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static byte Ready(void* data)
    {
        try { using var scope = CallbackErrors.Enter(); return State(data)._backend.IsReady ? (byte)1 : (byte)0; }
        catch (Exception error) { return Fail(data, error); }
    }
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static byte Info(void* data, byte* path, SDL_PathInfo* info)
    {
        try
        {
            using var scope = CallbackErrors.Enter();
            var value = State(data)._backend.GetPathInfo(Path(path));
            if (!Enum.IsDefined(value.Type)) throw new InvalidDataException("Unknown storage entry type.");
            if (value.Type == StoragePathType.None) { Native.Error.SDL_SetError("Storage path does not exist"u8); return 0; }
            if (info != null) *info = value.ToNative();
            return 1;
        }
        catch (Exception error) { return Fail(data, error); }
    }
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static byte Read(void* data, byte* path, void* destination, ulong length)
    {
        try
        {
            using var scope = CallbackErrors.Enter();
            State(data)._backend.ReadFile(Path(path), new Span<byte>(destination, checked((int)length)));
            return 1;
        }
        catch (Exception error) { return Fail(data, error); }
    }
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static byte Write(void* data, byte* path, void* source, ulong length)
    {
        try
        {
            using var scope = CallbackErrors.Enter();
            State(data)._backend.WriteFile(Path(path), new ReadOnlySpan<byte>(source, checked((int)length)));
            return 1;
        }
        catch (Exception error) { return Fail(data, error); }
    }
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static byte CreateDirectory(void* data, byte* path)
    {
        try { using var scope = CallbackErrors.Enter(); State(data)._backend.CreateDirectory(Path(path)); return 1; }
        catch (Exception error) { return Fail(data, error); }
    }
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static byte Remove(void* data, byte* path)
    {
        try { using var scope = CallbackErrors.Enter(); State(data)._backend.Remove(Path(path)); return 1; }
        catch (Exception error) { return Fail(data, error); }
    }
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static byte Rename(void* data, byte* oldPath, byte* newPath)
    {
        try { using var scope = CallbackErrors.Enter(); State(data)._backend.Rename(Path(oldPath), Path(newPath)); return 1; }
        catch (Exception error) { return Fail(data, error); }
    }
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static byte Copy(void* data, byte* oldPath, byte* newPath)
    {
        try { using var scope = CallbackErrors.Enter(); State(data)._backend.CopyFile(Path(oldPath), Path(newPath)); return 1; }
        catch (Exception error) { return Fail(data, error); }
    }
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static ulong SpaceRemaining(void* data)
    {
        try { using var scope = CallbackErrors.Enter(); return State(data)._backend.SpaceRemaining; }
        catch (Exception error) { Fail(data, error); return 0; }
    }
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static byte Enumerate(void* data, byte* path,
        delegate* unmanaged[Cdecl]<void*, byte*, byte*, SDL_EnumerationResult> callback, void* callbackData)
    {
        try
        {
            using var scope = CallbackErrors.Enter();
            var directory = Path(path);
            var prefix = directory.Length == 0 || directory.EndsWith('/') ? directory : directory + '/';
            fixed (byte* nativeDirectory = ToUtf8(prefix))
            foreach (var name in State(data)._backend.EnumerateDirectory(directory))
            {
                // Backends cannot accidentally feed a glob traversal a parent or nested path.
                if (string.IsNullOrEmpty(name) || name.Contains('/') || name.Contains('\\')) throw new InvalidDataException("Storage enumeration must return immediate child names.");
                var encoded = StorageContainer.PathBytes(name);
                SDL_EnumerationResult result;
                fixed (byte* filename = encoded) result = callback(callbackData, nativeDirectory, filename);
                if (result == SDL_EnumerationResult.SDL_ENUM_SUCCESS) return 1;
                if (result != SDL_EnumerationResult.SDL_ENUM_CONTINUE) return 0;
            }
            return 1;
        }
        catch (Exception error) { return Fail(data, error); }
    }
}
