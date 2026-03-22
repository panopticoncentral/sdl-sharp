using System.Runtime.InteropServices;
using SdlSharp.Native;

using static SdlSharp.Native.Common;
using static SdlSharp.Native.Camera;

namespace SdlSharp.Graphics;

/// <summary>
/// Managed wrapper for an SDL camera device.
/// </summary>
public sealed unsafe class Camera : IDisposable
{
    private readonly bool _ownsHandle;

    internal SDL_Camera* Handle { get; private set; }

    internal Camera(SDL_Camera* handle, bool ownsHandle = true)
    {
        Handle = handle;
        _ownsHandle = ownsHandle;
    }

    /// <summary>Opens a camera device.</summary>
    /// <param name="id">The camera instance ID.</param>
    /// <param name="spec">Desired camera spec, or null for default.</param>
    public static Camera Open(uint id, CameraSpec? spec = null)
    {
        SDL_CameraSpec nativeSpec;
        SDL_CameraSpec* specPtr = null;
        if (spec is { } s)
        {
            nativeSpec = new SDL_CameraSpec
            {
                format = (SDL_PixelFormat)s.Format,
                colorspace = (SDL_Colorspace)s.Colorspace,
                width = s.Width,
                height = s.Height,
                framerate_numerator = s.FramerateNumerator,
                framerate_denominator = s.FramerateDenominator,
            };
            specPtr = &nativeSpec;
        }

        return new Camera(Check(SDL_OpenCamera(new SDL_CameraID(id), specPtr)));
    }

    /// <summary>Gets the instance IDs of all connected cameras.</summary>
    public static uint[] GetDevices()
    {
        var ids = SDL_GetCameras(out var count);
        if (ids == null) return [];
        try
        {
            var result = new uint[count];
            for (var i = 0; i < count; i++)
                result[i] = ids[i].Value;
            return result;
        }
        finally
        {
            SDL_free(ids);
        }
    }

    /// <summary>Gets the name of a camera by instance ID.</summary>
    public static string? GetName(uint id) =>
        Marshal.PtrToStringUTF8((nint)SDL_GetCameraName(new SDL_CameraID(id)));

    /// <summary>Gets the position of a camera by instance ID.</summary>
    public static CameraPosition GetPosition(uint id) =>
        (CameraPosition)SDL_GetCameraPosition(new SDL_CameraID(id));

    /// <summary>Gets the supported formats for a camera by instance ID.</summary>
    public static CameraSpec[] GetSupportedFormats(uint id)
    {
        var specs = SDL_GetCameraSupportedFormats(new SDL_CameraID(id), out var count);
        if (specs == null) return [];
        try
        {
            var result = new CameraSpec[count];
            for (var i = 0; i < count; i++)
                result[i] = MarshalSpec(specs[i]);
            return result;
        }
        finally
        {
            SDL_free(specs);
        }
    }

    /// <summary>Gets the names of all built-in camera drivers.</summary>
    public static string[] GetDrivers()
    {
        var count = SDL_GetNumCameraDrivers();
        var result = new string[count];
        for (var i = 0; i < count; i++)
            result[i] = Marshal.PtrToStringUTF8((nint)SDL_GetCameraDriver(i)) ?? "";
        return result;
    }

    /// <summary>Gets the name of the current camera driver.</summary>
    public static string? GetCurrentDriver() =>
        Marshal.PtrToStringUTF8((nint)SDL_GetCurrentCameraDriver());

    /// <summary>Gets the current format of this camera.</summary>
    public CameraSpec Format
    {
        get
        {
            SDL_CameraSpec spec;
            Check(SDL_GetCameraFormat(Handle, &spec));
            return MarshalSpec(&spec);
        }
    }

    /// <summary>Gets the permission state of this camera. 0 = pending, 1 = approved, -1 = denied.</summary>
    public int PermissionState => SDL_GetCameraPermissionState(Handle);

    /// <summary>Gets the properties associated with this camera.</summary>
    public PropertyGroup Properties =>
        new(CheckId(SDL_GetCameraProperties(Handle)), ownsHandle: false);

    /// <summary>
    /// Acquires a video frame from the camera.
    /// Returns null if no frame is available yet.
    /// The returned surface must be released with <see cref="ReleaseFrame"/>.
    /// </summary>
    /// <param name="timestampNS">Receives the frame timestamp in nanoseconds.</param>
    public Surface? AcquireFrame(out ulong timestampNS)
    {
        ulong ts;
        var surface = SDL_AcquireCameraFrame(Handle, &ts);
        timestampNS = ts;
        return surface == null ? null : new Surface(surface, ownsHandle: false);
    }

    /// <summary>
    /// Releases a frame previously acquired with <see cref="AcquireFrame"/>.
    /// </summary>
    public void ReleaseFrame(Surface frame) =>
        SDL_ReleaseCameraFrame(Handle, frame.Handle);

    private static CameraSpec MarshalSpec(SDL_CameraSpec* s) =>
        new((PixelFormat)s->format, (Colorspace)s->colorspace,
            s->width, s->height, s->framerate_numerator, s->framerate_denominator);

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_ownsHandle && Handle != null)
        {
            SDL_CloseCamera(Handle);
            Handle = null;
        }
    }
}
