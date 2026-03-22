namespace SdlSharp.Input;

/// <summary>
/// Types of pen devices.
/// </summary>
public enum PenDeviceType
{
    /// <summary>Invalid pen device type.</summary>
    Invalid = (int)Native.SDL_PenDeviceType.SDL_PEN_DEVICE_TYPE_INVALID,
    /// <summary>Unknown pen device type.</summary>
    Unknown = (int)Native.SDL_PenDeviceType.SDL_PEN_DEVICE_TYPE_UNKNOWN,
    /// <summary>Direct pen drawing on screen (e.g. Surface or iPad Pencil).</summary>
    Direct = (int)Native.SDL_PenDeviceType.SDL_PEN_DEVICE_TYPE_DIRECT,
    /// <summary>Indirect pen drawing on a separate tablet surface.</summary>
    Indirect = (int)Native.SDL_PenDeviceType.SDL_PEN_DEVICE_TYPE_INDIRECT,
}
