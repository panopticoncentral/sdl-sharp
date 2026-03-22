namespace SdlSharp.Input;

/// <summary>
/// Types of touch devices.
/// </summary>
public enum TouchDeviceType
{
    /// <summary>Invalid touch device type.</summary>
    Invalid = (int)Native.SDL_TouchDeviceType.SDL_TOUCH_DEVICE_INVALID,
    /// <summary>Touch screen with window-relative coordinates (e.g. phone screen).</summary>
    Direct = (int)Native.SDL_TouchDeviceType.SDL_TOUCH_DEVICE_DIRECT,
    /// <summary>Trackpad with absolute device coordinates (e.g. Apple Magic Trackpad).</summary>
    IndirectAbsolute = (int)Native.SDL_TouchDeviceType.SDL_TOUCH_DEVICE_INDIRECT_ABSOLUTE,
    /// <summary>Trackpad with relative device coordinates (e.g. laptop trackpad).</summary>
    IndirectRelative = (int)Native.SDL_TouchDeviceType.SDL_TOUCH_DEVICE_INDIRECT_RELATIVE,
}
