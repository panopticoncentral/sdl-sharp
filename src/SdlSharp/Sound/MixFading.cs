namespace SdlSharp.Sound
{
    /// <summary>
    /// The fading status of audio.
    /// </summary>
    public enum MixFading
    {
        /// <summary>
        /// No fading is going on.
        /// </summary>
        NoFading = Native.Mix_Fading.MIX_NO_FADING,

        /// <summary>
        /// The audio is fading in.
        /// </summary>
        FadingIn = Native.Mix_Fading.MIX_FADING_IN,

        /// <summary>
        /// The audio is fading out.
        /// </summary>
        FadingOut = Native.Mix_Fading.MIX_FADING_OUT
    }
}
