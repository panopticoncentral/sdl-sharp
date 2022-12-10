namespace SdlSharp.Sound
{
    /// <summary>
    /// Indicates what aspects of an audio format can be changed when opening an audio device.
    /// </summary>
    [Flags]
    public enum AudioAllowChange : uint
    {
        /// <summary>
        /// No changes allowed.
        /// </summary>
        None = 0,

        /// <summary>
        /// The frequency can be changed.
        /// </summary>
        Frequency = Native.SDL_AUDIO_ALLOW_FREQUENCY_CHANGE,

        /// <summary>
        /// The format can be changed.
        /// </summary>
        Format = Native.SDL_AUDIO_ALLOW_FORMAT_CHANGE,

        /// <summary>
        /// The number of channels can be changed.
        /// </summary>
        Channels = Native.SDL_AUDIO_ALLOW_CHANNELS_CHANGE,

        /// <summary>
        /// The samples can be changed.
        /// </summary>
        Samples = Native.SDL_AUDIO_ALLOW_SAMPLES_CHANGE,

        /// <summary>
        /// Any aspect can be changed.
        /// </summary>
        Any = Native.SDL_AUDIO_ALLOW_ANY_CHANGE
    }
}
