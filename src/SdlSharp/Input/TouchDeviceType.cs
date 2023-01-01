namespace SdlSharp.Input
{
    /// <summary>
    /// The type of a touch device.
    /// </summary>
    public enum TouchDeviceType
    {
        /// <summary>
        /// Not a valid touch device.
        /// </summary>
        Invalid = Native.SDL_TouchDeviceType.SDL_TOUCH_DEVICE_INVALID,

        /// <summary>
        /// Touch screen with window-relative coordinates,
        /// </summary>
        Direct = Native.SDL_TouchDeviceType.SDL_TOUCH_DEVICE_DIRECT,

        /// <summary>
        /// Trackpad with absolute device coordinates.
        /// </summary>
        IndirectAbsolute = Native.SDL_TouchDeviceType.SDL_TOUCH_DEVICE_INDIRECT_ABSOLUTE,

        /// <summary>
        /// Trackpad with screen cursor-relative coordinates.
        /// </summary>
        IndirectRelative = Native.SDL_TouchDeviceType.SDL_TOUCH_DEVICE_INDIRECT_RELATIVE
    }
}
