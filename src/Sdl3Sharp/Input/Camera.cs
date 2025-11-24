using static Sdl3Sharp.Native.Camera;
using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.StdInc;

namespace Sdl3Sharp.Input;

/// <summary>
/// Represents an opened SDL camera device for video capture.
/// This API lets apps read input from video sources, like webcams.
/// </summary>
public sealed unsafe class Camera : IDisposable
{
    private bool _disposed;
    private readonly bool _ownsHandle;

    /// <summary>
    /// Gets the underlying SDL_Camera pointer.
    /// </summary>
    public SDL_Camera* Handle { get; private set; }

    /// <summary>
    /// Gets the properties associated with this camera.
    /// </summary>
    public PropertyGroup Properties
    {
        get
        {
            ThrowIfDisposed();
            return new(CheckErrorZero(SDL_GetCameraProperties(Handle)), ownsHandle: false);
        }
    }

    /// <summary>
    /// Gets the descriptor for this camera.
    /// </summary>
    public CameraDescriptor Descriptor
    {
        get
        {
            ThrowIfDisposed();
            return new(CheckErrorZero(SDL_GetCameraID(Handle)));
        }
    }

    /// <summary>
    /// Gets the permission state for this camera.
    /// Cameras will not function until the user permits access to the hardware.
    /// </summary>
    public CameraPermissionState PermissionState
    {
        get
        {
            ThrowIfDisposed();
            return (CameraPermissionState)SDL_GetCameraPermissionState(Handle);
        }
    }

    /// <summary>
    /// Gets whether the camera has been approved for use.
    /// </summary>
    public bool IsApproved
    {
        get
        {
            ThrowIfDisposed();
            return SDL_GetCameraPermissionState(Handle) == 1;
        }
    }

    /// <summary>
    /// Gets the number of built-in camera drivers.
    /// </summary>
    public static int DriverCount => SDL_GetNumCameraDrivers();

    /// <summary>
    /// Gets the name of the current camera driver, or null if no driver has been initialized.
    /// </summary>
    public static string? CurrentDriver => SDL_GetCurrentCameraDriver();

    /// <summary>
    /// Gets a list of currently connected camera devices.
    /// </summary>
    /// <returns>An array of camera descriptors.</returns>
    public static CameraDescriptor[] GetCameras()
    {
        int count;
        SDL_CameraID* cameras = SDL_GetCameras(&count);

        if (cameras == null)
        {
            return [];
        }

        try
        {
            var result = new CameraDescriptor[count];
            for (var i = 0; i < count; i++)
            {
                result[i] = new CameraDescriptor(cameras[i]);
            }

            return result;
        }
        finally
        {
            SDL_free(cameras);
        }
    }

    /// <summary>
    /// Gets the name of a built-in camera driver by index.
    /// </summary>
    /// <param name="index">The driver index (0 to DriverCount - 1).</param>
    /// <returns>The name of the driver, or null if the index is invalid.</returns>
    public static string? GetDriverName(int index)
    {
        return SDL_GetCameraDriver(index);
    }

    /// <summary>
    /// Wraps an existing SDL_Camera pointer.
    /// </summary>
    /// <param name="handle">The SDL_Camera pointer to wrap.</param>
    /// <param name="ownsHandle">Whether this instance should close the camera when disposed.</param>
    internal Camera(SDL_Camera* handle, bool ownsHandle = true)
    {
        Handle = handle;
        _ownsHandle = ownsHandle;
    }

    /// <summary>
    /// Gets the spec that this camera is using when generating images.
    /// Note that this might not be the native format of the hardware, as SDL might
    /// be converting to this format behind the scenes.
    /// </summary>
    /// <returns>The camera specification, or null if not yet available (waiting for permission).</returns>
    public CameraSpec? GetFormat()
    {
        ThrowIfDisposed();

        SDL_CameraSpec spec;
        return !SDL_GetCameraFormat(Handle, &spec)
            ? null
            : new CameraSpec(
            new(spec.format),
            new(spec.colorspace),
            new(spec.width, spec.height),
            spec.framerate_numerator,
            spec.framerate_denominator);
    }

    /// <summary>
    /// Acquires a frame from the camera.
    /// This is a non-blocking API. If there is a frame available, it is returned.
    /// If no frame is available yet, null is returned.
    /// </summary>
    /// <returns>A camera frame if available, or null if no frame is ready yet.</returns>
    /// <remarks>
    /// After use, the frame must be released by disposing it. If you don't release frames,
    /// the system may stop providing more video!
    /// If you need to keep the frame data, use <see cref="CameraFrame.ToSurface"/> to create a copy.
    /// </remarks>
    public CameraFrame? AcquireFrame()
    {
        ThrowIfDisposed();

        ulong timestampNs;
        Native.Surface.SDL_Surface* surface = SDL_AcquireCameraFrame(Handle, &timestampNs);

        return surface == null ? null : new CameraFrame(Handle, surface, timestampNs);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        if (_ownsHandle && Handle != null)
        {
            SDL_CloseCamera(Handle);
            Handle = null;
        }

        _disposed = true;
    }

    private void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
    }
}
