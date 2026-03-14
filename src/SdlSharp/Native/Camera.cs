using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace SdlSharp.Native;

/// <summary>
/// SDL Camera instance IDs.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly record struct SDL_CameraID(uint Value);

/// <summary>
/// Opaque handle for a camera device.
/// </summary>
public struct SDL_Camera;

/// <summary>
/// Camera specification describing format and resolution.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_CameraSpec
{
    /// <summary>Pixel format.</summary>
    public SDL_PixelFormat format;
    /// <summary>Colorspace.</summary>
    public SDL_Colorspace colorspace;
    /// <summary>Width in pixels.</summary>
    public int width;
    /// <summary>Height in pixels.</summary>
    public int height;
    /// <summary>Frame rate numerator.</summary>
    public int framerate_numerator;
    /// <summary>Frame rate denominator.</summary>
    public int framerate_denominator;
}

/// <summary>
/// Camera position relative to the device.
/// </summary>
public enum SDL_CameraPosition
{
    /// <summary>Unknown position.</summary>
    SDL_CAMERA_POSITION_UNKNOWN = 0,
    /// <summary>Front-facing camera.</summary>
    SDL_CAMERA_POSITION_FRONT_FACING = 1,
    /// <summary>Back-facing camera.</summary>
    SDL_CAMERA_POSITION_BACK_FACING = 2,
}

/// <summary>
/// Native bindings for SDL_camera.h — camera input.
/// </summary>
[SuppressMessage("Interoperability", "CA1401:P/Invokes should not be visible")]
public static partial class Camera
{
    /// <summary>Get the number of built-in camera drivers.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetNumCameraDrivers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_GetNumCameraDrivers();

    /// <summary>Get the name of a built-in camera driver.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetCameraDriver")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* SDL_GetCameraDriver(int index);

    /// <summary>Get the name of the current camera driver.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetCurrentCameraDriver")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* SDL_GetCurrentCameraDriver();

    /// <summary>Get a list of currently connected camera devices.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetCameras")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_CameraID* SDL_GetCameras(out int count);

    /// <summary>Get the list of native formats/sizes a camera supports.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetCameraSupportedFormats")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_CameraSpec** SDL_GetCameraSupportedFormats(SDL_CameraID instance_id, out int count);

    /// <summary>Get the human-readable name of a camera.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetCameraName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* SDL_GetCameraName(SDL_CameraID instance_id);

    /// <summary>Get the position of a camera (front/back-facing).</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetCameraPosition")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_CameraPosition SDL_GetCameraPosition(SDL_CameraID instance_id);

    /// <summary>Open a camera device.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_OpenCamera")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Camera* SDL_OpenCamera(SDL_CameraID instance_id, SDL_CameraSpec* spec);

    /// <summary>Get the approval state of a camera device.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetCameraPermissionState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial int SDL_GetCameraPermissionState(SDL_Camera* camera);

    /// <summary>Get the instance ID of an opened camera.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetCameraID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_CameraID SDL_GetCameraID(SDL_Camera* camera);

    /// <summary>Get the properties associated with an opened camera.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetCameraProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_PropertiesID SDL_GetCameraProperties(SDL_Camera* camera);

    /// <summary>Get the spec that a camera is using when generating images.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetCameraFormat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GetCameraFormat(SDL_Camera* camera, SDL_CameraSpec* spec);

    /// <summary>Acquire a frame. Returns a surface that must be released with SDL_ReleaseCameraFrame.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_AcquireCameraFrame")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Surface* SDL_AcquireCameraFrame(SDL_Camera* camera, ulong* timestampNS);

    /// <summary>Release a frame acquired by SDL_AcquireCameraFrame.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_ReleaseCameraFrame")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_ReleaseCameraFrame(SDL_Camera* camera, SDL_Surface* frame);

    /// <summary>Shut down camera processing and close the camera device.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_CloseCamera")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_CloseCamera(SDL_Camera* camera);
}
