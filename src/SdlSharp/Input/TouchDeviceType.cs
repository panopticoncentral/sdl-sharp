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
        Invalid = -1,

        /// <summary>
        /// Touch screen with window-relative coordinates,
        /// </summary>
        Direct,

        /// <summary>
        /// Trackpad with absolute device coordinates.
        /// </summary>
        IndirectAbsolute,

        /// <summary>
        /// Trackpad with screen cursor-relative coordinates.
        /// </summary>
        IndirectRelative
    }
}
