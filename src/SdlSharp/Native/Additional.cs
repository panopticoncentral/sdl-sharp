using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
namespace SdlSharp.Native;

public enum SDL_IOStatus { SDL_IO_STATUS_READY, SDL_IO_STATUS_ERROR, SDL_IO_STATUS_EOF, SDL_IO_STATUS_NOT_READY, SDL_IO_STATUS_READONLY, SDL_IO_STATUS_WRITEONLY }
public enum SDL_IOWhence { SDL_IO_SEEK_SET, SDL_IO_SEEK_CUR, SDL_IO_SEEK_END }


/// <summary>Opaque SDL stream.</summary>
public struct SDL_IOStream;
/// <summary>SDL stream callback interface.</summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct SDL_IOStreamInterface
{
    public uint version;
    public delegate* unmanaged[Cdecl]<void*, long> size;
    public delegate* unmanaged[Cdecl]<void*, long, SDL_IOWhence, long> seek;
    public delegate* unmanaged[Cdecl]<void*, void*, nuint, SDL_IOStatus*, nuint> read;
    public delegate* unmanaged[Cdecl]<void*, void*, nuint, SDL_IOStatus*, nuint> write;
    public delegate* unmanaged[Cdecl]<void*, SDL_IOStatus*, byte> flush;
    public delegate* unmanaged[Cdecl]<void*, byte> close;
}

/// <summary>Additional SDL bindings.</summary>
public static partial class IOStream
{
    /// <summary>Calls SDL_OpenIO.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_OpenIO")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_IOStream* SDL_OpenIO(SDL_IOStreamInterface* iface, void* userdata);
    /// <summary>Calls SDL_CloseIO.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_CloseIO")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_CloseIO(SDL_IOStream* stream);
}

/// <summary>Additional SDL bindings.</summary>
public static partial class Surface
{
    /// <summary>Calls SDL_LoadSurface_IO.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_LoadSurface_IO")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Surface* SDL_LoadSurface_IO(SDL_IOStream* stream, [MarshalAs(UnmanagedType.U1)] bool closeio);
    /// <summary>Calls SDL_LoadBMP_IO.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_LoadBMP_IO")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Surface* SDL_LoadBMP_IO(SDL_IOStream* stream, [MarshalAs(UnmanagedType.U1)] bool closeio);
    /// <summary>Calls SDL_LoadPNG_IO.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_LoadPNG_IO")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Surface* SDL_LoadPNG_IO(SDL_IOStream* stream, [MarshalAs(UnmanagedType.U1)] bool closeio);
    /// <summary>Calls SDL_SaveBMP_IO.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SaveBMP_IO")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SaveBMP_IO(SDL_Surface* surface, SDL_IOStream* stream, [MarshalAs(UnmanagedType.U1)] bool closeio);
    /// <summary>Calls SDL_SavePNG_IO.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SavePNG_IO")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SavePNG_IO(SDL_Surface* surface, SDL_IOStream* stream, [MarshalAs(UnmanagedType.U1)] bool closeio);
}

/// <summary>Additional SDL bindings.</summary>
public static partial class Audio
{
    /// <summary>Calls SDL_LoadWAV_IO.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_LoadWAV_IO")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_LoadWAV_IO(SDL_IOStream* stream, [MarshalAs(UnmanagedType.U1)] bool closeio, SDL_AudioSpec* spec, byte** data, uint* length);
    /// <summary>Calls SDL_PutAudioStreamDataNoCopy.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_PutAudioStreamDataNoCopy")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_PutAudioStreamDataNoCopy(SDL_AudioStream* stream, void* buffer, int length, delegate* unmanaged[Cdecl]<void*, void*, int, void> callback, void* userdata);
}

/// <summary>Additional SDL bindings.</summary>
public static partial class Gamepad
{
    /// <summary>Calls SDL_AddGamepadMappingsFromIO.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_AddGamepadMappingsFromIO")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial int SDL_AddGamepadMappingsFromIO(SDL_IOStream* stream, [MarshalAs(UnmanagedType.U1)] bool closeio);
    /// <summary>Calls SDL_GetGamepadMappings.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetGamepadMappings")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte** SDL_GetGamepadMappings(int* count);
}

/// <summary>Additional SDL bindings.</summary>
public static partial class Clipboard
{
    /// <summary>Calls SDL_GetPrimarySelectionText.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetPrimarySelectionText")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* SDL_GetPrimarySelectionText();
    /// <summary>Calls SDL_SetPrimarySelectionText.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetPrimarySelectionText")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetPrimarySelectionText(ReadOnlySpan<byte> text);
    /// <summary>Calls SDL_HasPrimarySelectionText.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_HasPrimarySelectionText")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_HasPrimarySelectionText();
}

/// <summary>Additional SDL bindings.</summary>
public static partial class Hints
{
    /// <summary>Calls SDL_AddHintCallback.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_AddHintCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_AddHintCallback(ReadOnlySpan<byte> name, delegate* unmanaged[Cdecl]<void*, byte*, byte*, byte*, void> callback, void* userdata);
    /// <summary>Calls SDL_RemoveHintCallback.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_RemoveHintCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_RemoveHintCallback(ReadOnlySpan<byte> name, delegate* unmanaged[Cdecl]<void*, byte*, byte*, byte*, void> callback, void* userdata);
}

/// <summary>Additional SDL bindings.</summary>
public static partial class Properties
{
    /// <summary>Calls SDL_SetPointerPropertyWithCleanup.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetPointerPropertyWithCleanup")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetPointerPropertyWithCleanup(SDL_PropertiesID id, ReadOnlySpan<byte> name, void* value, delegate* unmanaged[Cdecl]<void*, void*, void> cleanup, void* userdata);
}

/// <summary>Additional SDL bindings.</summary>
public static partial class Video
{
    /// <summary>Calls SDL_GetWindowICCProfile.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetWindowICCProfile")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void* SDL_GetWindowICCProfile(SDL_Window* window, nuint* size);
}

/// <summary>Additional SDL bindings.</summary>
public static partial class Gpu
{
    /// <summary>Calls SDL_GPUSupportsProperties.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GPUSupportsProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GPUSupportsProperties(SDL_PropertiesID properties);
    /// <summary>Calls SDL_GetGPUTextureFormatFromPixelFormat.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetGPUTextureFormatFromPixelFormat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_GPUTextureFormat SDL_GetGPUTextureFormatFromPixelFormat(SDL_PixelFormat format);
    /// <summary>Calls SDL_GetPixelFormatFromGPUTextureFormat.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetPixelFormatFromGPUTextureFormat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_PixelFormat SDL_GetPixelFormatFromGPUTextureFormat(SDL_GPUTextureFormat format);
}

/// <summary>Additional SDL bindings.</summary>
public static partial class Dialog
{
    /// <summary>Calls SDL_ShowFileDialogWithProperties.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_ShowFileDialogWithProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_ShowFileDialogWithProperties(SDL_FileDialogType type, delegate* unmanaged[Cdecl]<void*, byte**, int, void> callback, void* userdata, SDL_PropertiesID properties);
}
