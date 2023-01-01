namespace SdlSharp.Sound
{
    /// <summary>
    /// The types of music files.
    /// </summary>
    public enum MixMusicType
    {
        /// <summary>
        /// None
        /// </summary>
        None = Native.Mix_MusicType.MUS_NONE,

        /// <summary>
        /// CMD
        /// </summary>
        Cmd = Native.Mix_MusicType.MUS_CMD,

        /// <summary>
        /// WAV
        /// </summary>
        Wav = Native.Mix_MusicType.MUS_WAV,

        /// <summary>
        /// MOD
        /// </summary>
        Mod = Native.Mix_MusicType.MUS_MOD,

        /// <summary>
        /// MID
        /// </summary>
        Mid = Native.Mix_MusicType.MUS_MID,

        /// <summary>
        /// OGG
        /// </summary>
        Ogg = Native.Mix_MusicType.MUS_OGG,

        /// <summary>
        /// MP3
        /// </summary>
        Mp3 = Native.Mix_MusicType.MUS_MP3,

        /// <summary>
        /// FLAC
        /// </summary>
        Flac = Native.Mix_MusicType.MUS_FLAC,

        /// <summary>
        /// OPUS
        /// </summary>
        Opus = Native.Mix_MusicType.MUS_OPUS
    }
}
