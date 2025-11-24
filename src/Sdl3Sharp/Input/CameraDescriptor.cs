using static Sdl3Sharp.Native.Camera;
using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.StdInc;

namespace Sdl3Sharp.Input;

/// <summary>
/// Describes a camera that is connected to the system but not yet opened.
/// Use <see cref="Open()"/> to open the camera for capturing video.
/// </summary>
/// <param name="id">The underlying SDL camera ID.</param>
public readonly unsafe struct CameraDescriptor(SDL_CameraID id)
{
    /// <summary>
    /// Gets the underlying SDL camera ID.
    /// </summary>
    public SDL_CameraID Id { get; } = id;

    /// <summary>
    /// Gets the human-readable device name for this camera.
    /// </summary>
    public string Name => CheckErrorNull(SDL_GetCameraName(Id));

    /// <summary>
    /// Gets the position of the camera in relation to the system.
    /// Most platforms will report Unknown, but mobile devices can often distinguish
    /// between front-facing (selfie) and back-facing cameras.
    /// </summary>
    public CameraPosition Position => (CameraPosition)SDL_GetCameraPosition(Id);

    /// <summary>
    /// Gets the list of native formats/sizes this camera supports.
    /// This returns a list of all formats and frame sizes that the camera can offer.
    /// </summary>
    /// <returns>An array of supported camera specifications.</returns>
    public CameraSpec[] GetSupportedFormats()
    {
        int count;
        SDL_CameraSpec** specs = SDL_GetCameraSupportedFormats(Id, &count);

        if (specs == null)
        {
            return [];
        }

        try
        {
            var result = new CameraSpec[count];
            for (var i = 0; i < count; i++)
            {
                SDL_CameraSpec* spec = specs[i];
                result[i] = new CameraSpec(
                    new(spec->format),
                    new(spec->colorspace),
                    new(spec->width, spec->height),
                    spec->framerate_numerator,
                    spec->framerate_denominator);
            }

            return result;
        }
        finally
        {
            SDL_free(specs);
        }
    }

    /// <summary>
    /// Opens this camera for use with the native format.
    /// SDL will choose a native format for you.
    /// </summary>
    /// <returns>A new Camera instance.</returns>
    public Camera Open()
    {
        return new(CheckErrorPointer(SDL_OpenCamera(Id, null)), ownsHandle: true);
    }

    /// <summary>
    /// Opens this camera for use with a specific format.
    /// If the hardware can't directly support the format, it will convert data seamlessly.
    /// </summary>
    /// <param name="spec">The desired format for data the device will provide.</param>
    /// <returns>A new Camera instance.</returns>
    public Camera Open(CameraSpec spec)
    {
        var nativeSpec = new SDL_CameraSpec
        {
            format = spec.Format.Format,
            colorspace = spec.Colorspace.Value,
            width = spec.Size.Width,
            height = spec.Size.Height,
            framerate_numerator = spec.FramerateNumerator,
            framerate_denominator = spec.FramerateDenominator
        };

        return new(CheckErrorPointer(SDL_OpenCamera(Id, &nativeSpec)), ownsHandle: true);
    }

    /// <summary>
    /// Implicitly converts an SDL_CameraID to a CameraDescriptor.
    /// </summary>
    /// <param name="id">The camera ID to convert.</param>
    public static implicit operator CameraDescriptor(SDL_CameraID id)
    {
        return new(id);
    }

    /// <summary>
    /// Implicitly converts a CameraDescriptor to an SDL_CameraID.
    /// </summary>
    /// <param name="descriptor">The descriptor to convert.</param>
    public static implicit operator SDL_CameraID(CameraDescriptor descriptor)
    {
        return descriptor.Id;
    }
}
