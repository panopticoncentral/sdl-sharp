using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SdlSharp.Native;

public struct SDL_Storage;
[StructLayout(LayoutKind.Sequential)]
public unsafe struct SDL_StorageInterface
{
    public uint version;
    public delegate* unmanaged[Cdecl]<void*, byte> close;
    public delegate* unmanaged[Cdecl]<void*, byte> ready;
    public delegate* unmanaged[Cdecl]<void*, byte*, delegate* unmanaged[Cdecl]<void*, byte*, byte*, SDL_EnumerationResult>, void*, byte> enumerate;
    public delegate* unmanaged[Cdecl]<void*, byte*, SDL_PathInfo*, byte> info;
    public delegate* unmanaged[Cdecl]<void*, byte*, void*, ulong, byte> read_file;
    public delegate* unmanaged[Cdecl]<void*, byte*, void*, ulong, byte> write_file;
    public delegate* unmanaged[Cdecl]<void*, byte*, byte> mkdir;
    public delegate* unmanaged[Cdecl]<void*, byte*, byte> remove;
    public delegate* unmanaged[Cdecl]<void*, byte*, byte*, byte> rename;
    public delegate* unmanaged[Cdecl]<void*, byte*, byte*, byte> copy;
    public delegate* unmanaged[Cdecl]<void*, ulong> space_remaining;
}

public static unsafe partial class Storage
{
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_OpenTitleStorage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Storage* SDL_OpenTitleStorage(ReadOnlySpan<byte> path, SDL_PropertiesID props);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_OpenUserStorage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Storage* SDL_OpenUserStorage(ReadOnlySpan<byte> org, ReadOnlySpan<byte> app, SDL_PropertiesID props);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_OpenFileStorage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Storage* SDL_OpenFileStorage(ReadOnlySpan<byte> path);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_OpenStorage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Storage* SDL_OpenStorage(SDL_StorageInterface* iface, void* userdata);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_CloseStorage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_CloseStorage(SDL_Storage* storage);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_StorageReady")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_StorageReady(SDL_Storage* storage);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetStorageFileSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetStorageFileSize(SDL_Storage* storage, ReadOnlySpan<byte> path, out ulong length);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_ReadStorageFile")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_ReadStorageFile(SDL_Storage* storage, ReadOnlySpan<byte> path, void* destination, ulong length);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_WriteStorageFile")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_WriteStorageFile(SDL_Storage* storage, ReadOnlySpan<byte> path, void* source, ulong length);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_CreateStorageDirectory")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_CreateStorageDirectory(SDL_Storage* storage, ReadOnlySpan<byte> path);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_EnumerateStorageDirectory")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_EnumerateStorageDirectory(SDL_Storage* storage, ReadOnlySpan<byte> path, delegate* unmanaged[Cdecl]<void*, byte*, byte*, SDL_EnumerationResult> callback, void* userdata);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_RemoveStoragePath")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_RemoveStoragePath(SDL_Storage* storage, ReadOnlySpan<byte> path);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_RenameStoragePath")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_RenameStoragePath(SDL_Storage* storage, ReadOnlySpan<byte> oldpath, ReadOnlySpan<byte> newpath);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_CopyStorageFile")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_CopyStorageFile(SDL_Storage* storage, ReadOnlySpan<byte> oldpath, ReadOnlySpan<byte> newpath);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetStoragePathInfo")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetStoragePathInfo(SDL_Storage* storage, ReadOnlySpan<byte> path, SDL_PathInfo* info);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetStorageSpaceRemaining")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ulong SDL_GetStorageSpaceRemaining(SDL_Storage* storage);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GlobStorageDirectory")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte** SDL_GlobStorageDirectory(SDL_Storage* storage, ReadOnlySpan<byte> path, ReadOnlySpan<byte> pattern, SDL_GlobFlags flags, out int count);

}
