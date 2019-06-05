namespace SdlSharp.Sound
{
    /// <summary>
    /// The types of music files.
    /// </summary>
    public enum MusicType
    {
        /// <summary>
        /// None
        /// </summary>
        None,

        /// <summary>
        /// CMD
        /// </summary>
        Cmd,

        /// <summary>
        /// WAV
        /// </summary>
        Wav,

        /// <summary>
        /// MOD
        /// </summary>
        Mod,

        /// <summary>
        /// MID
        /// </summary>
        Mid,

        /// <summary>
        /// OGG
        /// </summary>
        Ogg,
        
        /// <summary>
        /// MP3
        /// </summary>
        Mp3,

        /// <summary>
        /// FLAC
        /// </summary>
        Flac = Mp3 + 2,

        /// <summary>
        /// OPUS
        /// </summary>
        Opus = Flac + 2
    }
}
