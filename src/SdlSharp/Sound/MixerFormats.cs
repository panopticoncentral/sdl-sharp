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
        None = 0x0,

        /// <summary>
        /// FLAC
        /// </summary>
        Flac = 0x1,

        /// <summary>
        /// MOD
        /// </summary>
        Mod = 0x2,

        /// <summary>
        /// MP3
        /// </summary>
        Mp3 = 0x8,
        
        /// <summary>
        /// OGG
        /// </summary>
        Ogg = 0x10,
        
        /// <summary>
        /// MID
        /// </summary>
        Mid = 0x20,

        /// <summary>
        /// OPUS
        /// </summary>
        Opus = 0x40
    }
}
