namespace SdlSharp.Sound
{
    /// <summary>
    /// Indicates what aspects of an audio format can be changed when opening an audio device.
    /// </summary>
    [Flags]
    public enum AudioAllowChange
    {
        /// <summary>
        /// No changes allowed.
        /// </summary>
        None = 0x0,

        /// <summary>
        /// The frequency can be changed.
        /// </summary>
        Frequency = 0x1,

        /// <summary>
        /// The format can be changed.
        /// </summary>
        Format = 0x2,

        /// <summary>
        /// The number of channels can be changed.
        /// </summary>
        Channels = 0x4,

        /// <summary>
        /// The samples can be changed.
        /// </summary>
        Samples = 0x8,

        /// <summary>
        /// Any aspect can be changed.
        /// </summary>
        Any = Frequency | Format | Channels | Samples
    }
}
