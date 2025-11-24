using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Pixels;
using static Sdl3Sharp.Native.Properties;
using static Sdl3Sharp.Native.Surface;

// We are intentionally exposing the P/Invoke calls so people can do low-level calls if needed
#pragma warning disable CA1401 // P/Invokes should not be visible

namespace Sdl3Sharp.Native;

/// <summary>
/// P/Invoke bindings for SDL_camera.h - Video capture for the SDL library.
/// This API lets apps read input from video sources, like webcams. Camera
/// devices can be enumerated, queried, and opened. Once opened, it will
/// provide SDL_Surface objects as new frames of video come in.
/// </summary>
public static unsafe partial class Camera
{
    /// <summary>
    /// A unique ID for a camera device for the time it is connected to the system,
    /// and is never reused for the lifetime of the application.
    /// If the device is disconnected and reconnected, it will get a new ID.
    /// The value 0 is an invalid ID.
    /// </summary>
    /// <param name="value">The camera ID value.</param>
    public readonly struct SDL_CameraID(uint value)
    {
        /// <summary>The underlying camera ID value.</summary>
        public readonly uint Value = value;

        /// <summary>Implicitly converts an SDL_CameraID to uint.</summary>
        /// <param name="id">The camera ID to convert.</param>
        public static implicit operator uint(SDL_CameraID id)
        {
            return id.Value;
        }

        /// <summary>Implicitly converts a uint to SDL_CameraID.</summary>
        /// <param name="value">The value to convert.</param>
        public static implicit operator SDL_CameraID(uint value)
        {
            return new(value);
        }
    }

    /// <summary>
    /// The opaque structure used to identify an opened SDL camera.
    /// </summary>
    public struct SDL_Camera
    {
    }

    /// <summary>
    /// The details of an output format for a camera device.
    /// Cameras often support multiple formats; each one will be encapsulated in this struct.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_CameraSpec
    {
        /// <summary>Frame format.</summary>
        public SDL_PixelFormat format;
        /// <summary>Frame colorspace.</summary>
        public SDL_Colorspace colorspace;
        /// <summary>Frame width.</summary>
        public int width;
        /// <summary>Frame height.</summary>
        public int height;
        /// <summary>Frame rate numerator ((num / denom) == FPS, (denom / num) == duration in seconds).</summary>
        public int framerate_numerator;
        /// <summary>Frame rate denominator ((num / denom) == FPS, (denom / num) == duration in seconds).</summary>
        public int framerate_denominator;
    }

    /// <summary>
    /// The position of camera in relation to system device.
    /// </summary>
    public enum SDL_CameraPosition
    {
        /// <summary>Unknown camera position.</summary>
        SDL_CAMERA_POSITION_UNKNOWN,
        /// <summary>Camera is on the front of the device (facing the user, for taking "selfies").</summary>
        SDL_CAMERA_POSITION_FRONT_FACING,
        /// <summary>Camera is on the back of the device (for filming in the direction the user is facing).</summary>
        SDL_CAMERA_POSITION_BACK_FACING
    }

    /// <summary>
    /// Use this function to get the number of built-in camera drivers.
    /// This function returns a hardcoded number. This never returns a negative
    /// value; if there are no drivers compiled into this build of SDL, this
    /// function returns zero.
    /// </summary>
    /// <returns>The number of built-in camera drivers.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_GetNumCameraDrivers();

    /// <summary>
    /// Use this function to get the name of a built in camera driver.
    /// The list of camera drivers is given in the order that they are normally
    /// initialized by default; the drivers that seem more reasonable to choose
    /// first are earlier in the list.
    /// </summary>
    /// <param name="index">The index of the camera driver; the value ranges from 0 to SDL_GetNumCameraDrivers() - 1.</param>
    /// <returns>The name of the camera driver at the requested index, or NULL if an invalid index was specified.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(Utf8StringMarshaller))]
    public static partial string? SDL_GetCameraDriver(int index);

    /// <summary>
    /// Get the name of the current camera driver.
    /// The names of drivers are all simple, low-ASCII identifiers, like "v4l2",
    /// "coremedia" or "android".
    /// </summary>
    /// <returns>The name of the current camera driver or NULL if no driver has been initialized.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(Utf8StringMarshaller))]
    public static partial string? SDL_GetCurrentCameraDriver();

    /// <summary>
    /// Get a list of currently connected camera devices.
    /// </summary>
    /// <param name="count">A pointer filled in with the number of cameras returned, may be NULL.</param>
    /// <returns>A 0 terminated array of camera instance IDs or NULL on failure; call SDL_GetError() for more information. This should be freed with SDL_free() when it is no longer needed.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_CameraID* SDL_GetCameras(int* count);

    /// <summary>
    /// Get the list of native formats/sizes a camera supports.
    /// This returns a list of all formats and frame sizes that a specific camera
    /// can offer. This is useful if your app can accept a variety of image formats
    /// and sizes and so want to find the optimal spec that doesn't require conversion.
    /// </summary>
    /// <param name="instance_id">The camera device instance ID.</param>
    /// <param name="count">A pointer filled in with the number of elements in the list, may be NULL.</param>
    /// <returns>A NULL terminated array of pointers to SDL_CameraSpec or NULL on failure; call SDL_GetError() for more information. This is a single allocation that should be freed with SDL_free() when it is no longer needed.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_CameraSpec** SDL_GetCameraSupportedFormats(SDL_CameraID instance_id, int* count);

    /// <summary>
    /// Get the human-readable device name for a camera.
    /// </summary>
    /// <param name="instance_id">The camera device instance ID.</param>
    /// <returns>A human-readable device name or NULL on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(Utf8StringMarshaller))]
    public static partial string? SDL_GetCameraName(SDL_CameraID instance_id);

    /// <summary>
    /// Get the position of the camera in relation to the system.
    /// Most platforms will report UNKNOWN, but mobile devices, like phones, can
    /// often make a distinction between cameras on the front of the device (that
    /// points towards the user, for taking "selfies") and cameras on the back.
    /// </summary>
    /// <param name="instance_id">The camera device instance ID.</param>
    /// <returns>The position of the camera on the system hardware.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_CameraPosition SDL_GetCameraPosition(SDL_CameraID instance_id);

    /// <summary>
    /// Open a video recording device (a "camera").
    /// You can open the device with any reasonable spec, and if the hardware can't
    /// directly support it, it will convert data seamlessly to the requested format.
    /// </summary>
    /// <param name="instance_id">The camera device instance ID.</param>
    /// <param name="spec">The desired format for data the device will provide. Can be NULL.</param>
    /// <returns>An SDL_Camera object or NULL on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Camera* SDL_OpenCamera(SDL_CameraID instance_id, SDL_CameraSpec* spec);

    /// <summary>
    /// Query if camera access has been approved by the user.
    /// Cameras will not function between when the device is opened by the app and
    /// when the user permits access to the hardware.
    /// </summary>
    /// <param name="camera">The opened camera device to query.</param>
    /// <returns>-1 if user denied access to the camera, 1 if user approved access, 0 if no decision has been made yet.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_GetCameraPermissionState(SDL_Camera* camera);

    /// <summary>
    /// Get the instance ID of an opened camera.
    /// </summary>
    /// <param name="camera">An SDL_Camera to query.</param>
    /// <returns>The instance ID of the specified camera on success or 0 on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_CameraID SDL_GetCameraID(SDL_Camera* camera);

    /// <summary>
    /// Get the properties associated with an opened camera.
    /// </summary>
    /// <param name="camera">The SDL_Camera obtained from SDL_OpenCamera().</param>
    /// <returns>A valid property ID on success or 0 on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_PropertiesID SDL_GetCameraProperties(SDL_Camera* camera);

    /// <summary>
    /// Get the spec that a camera is using when generating images.
    /// Note that this might not be the native format of the hardware, as SDL might
    /// be converting to this format behind the scenes.
    /// </summary>
    /// <param name="camera">Opened camera device.</param>
    /// <param name="spec">The SDL_CameraSpec to be initialized by this function.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetCameraFormat(SDL_Camera* camera, SDL_CameraSpec* spec);

    /// <summary>
    /// Acquire a frame.
    /// The frame is a memory pointer to the image data, whose size and format are
    /// given by the spec requested when opening the device. This is a non blocking API.
    /// If there is a frame available, a non-NULL surface is returned, and timestampNS
    /// will be filled with a non-zero value. Note that an error case can also return NULL,
    /// but a NULL by itself is normal and just signifies that a new frame is not yet available.
    /// After use, the frame should be released with SDL_ReleaseCameraFrame().
    /// </summary>
    /// <param name="camera">Opened camera device.</param>
    /// <param name="timestampNS">A pointer filled in with the frame's timestamp, or 0 on error. Can be NULL.</param>
    /// <returns>A new frame of video on success, NULL if none is currently available.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Surface* SDL_AcquireCameraFrame(SDL_Camera* camera, ulong* timestampNS);

    /// <summary>
    /// Release a frame of video acquired from a camera.
    /// Let the back-end re-use the internal buffer for camera.
    /// This function must be called only on surface objects returned by SDL_AcquireCameraFrame().
    /// The app should not use the surface again after calling this function.
    /// </summary>
    /// <param name="camera">Opened camera device.</param>
    /// <param name="frame">The video frame surface to release.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_ReleaseCameraFrame(SDL_Camera* camera, SDL_Surface* frame);

    /// <summary>
    /// Use this function to shut down camera processing and close the camera device.
    /// </summary>
    /// <param name="camera">Opened camera device.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_CloseCamera(SDL_Camera* camera);
}
