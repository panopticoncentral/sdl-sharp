﻿namespace SdlSharp.Sound
{
    /// <summary>
    /// The status of an audio device.
    /// </summary>
    public enum AudioStatus
    {
        /// <summary>
        /// The audio device is stopped.
        /// </summary>
        Stopped = Native.SDL_AudioStatus.SDL_AUDIO_STOPPED,

        /// <summary>
        /// The audio device is playing.
        /// </summary>
        Playing = Native.SDL_AudioStatus.SDL_AUDIO_PLAYING,

        /// <summary>
        /// The audio device is paused;
        /// </summary>
        Paused = Native.SDL_AudioStatus.SDL_AUDIO_PAUSED
    }
}
