namespace SdlSharp.Sound
{
    /// <summary>
    /// The formats supported by the mixer.
    /// </summary>
    public enum MixerFormats
    {
        /// <summary>
        /// None.
        /// </summary>
        None = 0,

        /// <summary>
        /// FLAC
        /// </summary>
        Flac = Native.MIX_InitFlags.MIX_INIT_FLAC,

        /// <summary>
        /// MOD
        /// </summary>
        Mod = Native.MIX_InitFlags.MIX_INIT_MOD,

        /// <summary>
        /// MP3
        /// </summary>
        Mp3 = Native.MIX_InitFlags.MIX_INIT_MP3,

        /// <summary>
        /// OGG
        /// </summary>
        Ogg = Native.MIX_InitFlags.MIX_INIT_OGG,

        /// <summary>
        /// MID
        /// </summary>
        Mid = Native.MIX_InitFlags.MIX_INIT_MID,

        /// <summary>
        /// OPUS
        /// </summary>
        Opus = Native.MIX_InitFlags.MIX_INIT_OPUS
    }
}
