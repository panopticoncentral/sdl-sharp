namespace SdlSharp.Sound
{
    /// <summary>
    /// A mixer channels.
    /// </summary>
    public unsafe sealed class MixChannel : NativeStaticIndexBase<int, MixChannel>
    {
        /// <summary>
        /// The fading status of the channel.
        /// </summary>
        public Fading Fading =>
            Native.Mix_FadingChannel(Index);

        /// <summary>
        /// Whether the channel is paused.
        /// </summary>
        public bool Paused =>
            Native.Mix_Paused(Index);

        /// <summary>
        /// Whether the channel is playing.
        /// </summary>
        public bool Playing =>
            Native.Mix_Playing(Index);

        /// <summary>
        /// The last sample played (or playing) in the channel.
        /// </summary>
        public MixChunk? Chunk =>
            MixChunk.PointerToInstance(Native.Mix_GetChunk(Index));

        /// <summary>
        /// Gets a particular channel.
        /// </summary>
        /// <param name="channel">The channel index.</param>
        /// <returns>The channel.</returns>
        public static MixChannel Get(int channel) =>
            IndexToInstance(channel);

        /// <summary>
        /// Unregisters all effects from the channel.
        /// </summary>
        public void UnregisterAllEffects() =>
            Native.CheckErrorZero(Native.Mix_UnregisterAllEffects(Index));

        /// <summary>
        /// Sets the panning on the channel.
        /// </summary>
        /// <param name="left">Left panning.</param>
        /// <param name="right">Right panning.</param>
        public void SetPanning(byte left, byte right) =>
            Native.CheckErrorBool(Native.Mix_SetPanning(Index, left, right));

        /// <summary>
        /// Sets the position of the channel.
        /// </summary>
        /// <param name="angle">The angle of the channel.</param>
        /// <param name="distance">The distance.</param>
        public void SetPosition(short angle, byte distance) =>
            Native.CheckErrorBool(Native.Mix_SetPosition(Index, angle, distance));

        /// <summary>
        /// Sets the distance of the channel.
        /// </summary>
        /// <param name="distance">The distance.</param>
        public void SetDistance(byte distance) =>
            Native.CheckErrorBool(Native.Mix_SetDistance(Index, distance));

        /// <summary>
        /// Reverses the stereo of the channel.
        /// </summary>
        /// <param name="flip">The amount to flip.</param>
        public void SetReverseStereo(int flip) =>
            Native.CheckErrorBool(Native.Mix_SetReverseStereo(Index, flip));

        /// <summary>
        /// Sets the volume of the channel.
        /// </summary>
        /// <param name="volume">The volume.</param>
        /// <returns>The old volume.</returns>
        public int Volume(int volume) =>
            Native.Mix_Volume(Index, volume);

        /// <summary>
        /// Halts the channel.
        /// </summary>
        public void Halt() =>
            Native.Mix_HaltChannel(Index);

        /// <summary>
        /// Expires the channel after an interval.
        /// </summary>
        /// <param name="ticks">The number of milliseconds until the channel expires.</param>
        /// <returns>The number of channels set to expire.</returns>
        public int Expire(int ticks) =>
            Native.Mix_ExpireChannel(Index, ticks);

        /// <summary>
        /// Fades the channel out.
        /// </summary>
        /// <param name="ms">The fade out interval.</param>
        /// <returns>The number of channels set to fade out.</returns>
        public int FadeOut(int ms) =>
            Native.Mix_FadeOutChannel(Index, ms);

        /// <summary>
        /// Pauses the channel.
        /// </summary>
        public void Pause() =>
            Native.Mix_Pause(Index);

        /// <summary>
        /// Resumes the channel.
        /// </summary>
        public void Resume() =>
            Native.Mix_Resume(Index);

        /// <summary>
        /// Plays a sample on the channel.
        /// </summary>
        /// <param name="chunk">The sample.</param>
        /// <param name="loops">The number of times to play the sample.</param>
        /// <returns>The channel the sample is playing on.</returns>
        public MixChannel Play(MixChunk chunk, int loops) =>
            Get(Native.CheckError(Native.Mix_PlayChannel(Index, chunk.Pointer, loops)));

        /// <summary>
        /// Plays a sample on the channel.
        /// </summary>
        /// <param name="chunk">The sample.</param>
        /// <param name="loops">The number of times to play the sample.</param>
        /// <param name="ticks">The limit of time to play the sample.</param>
        /// <returns>The channel the sample is playing on.</returns>
        public MixChannel Play(MixChunk chunk, int loops, int ticks) =>
            Get(Native.CheckError(Native.Mix_PlayChannelTimed(Index, chunk.Pointer, loops, ticks)));

        /// <summary>
        /// Fades a sample into a channel.
        /// </summary>
        /// <param name="chunk">The sample.</param>
        /// <param name="loops">The number of times to play the sample.</param>
        /// <param name="ms">The length of the fade in.</param>
        /// <returns>The channel the sample is playing on.</returns>
        public MixChannel FadeIn(MixChunk chunk, int loops, int ms) =>
            Get(Native.CheckError(Native.Mix_FadeInChannel(Index, chunk.Pointer, loops, ms)));

        /// <summary>
        /// Fades a sample into a channel.
        /// </summary>
        /// <param name="chunk">The sample.</param>
        /// <param name="loops">The number of times to play the sample.</param>
        /// <param name="ms">The length of the fade in.</param>
        /// <param name="ticks">The limit of time to play the sample.</param>
        /// <returns>The channel the sample is playing on.</returns>
        public MixChannel FadeIn(MixChunk chunk, int loops, int ms, int ticks) =>
            Get(Native.CheckError(Native.Mix_FadeInChannelTimed(Index, chunk.Pointer, loops, ms, ticks)));
    }
}
