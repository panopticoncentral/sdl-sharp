using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SdlSharp.Sound
{
    /// <summary>
    /// A mixer channel.
    /// </summary>
    public sealed unsafe class MixChannel
    {
        private readonly int _index;

        private static MixChannelFinishedCallback? s_finishedCallback;

        /// <summary>
        /// The fading status of the channel.
        /// </summary>
        public MixFading Fading =>
            (MixFading)Native.Mix_FadingChannel(_index);

        /// <summary>
        /// Whether the channel is paused.
        /// </summary>
        public bool Paused =>
            Native.Mix_Paused(_index) != 0;

        /// <summary>
        /// Whether the channel is playing.
        /// </summary>
        public bool Playing =>
            Native.Mix_Playing(_index) != 0;

        /// <summary>
        /// The last sample played (or playing) in the channel.
        /// </summary>
        public MixChunk? Chunk =>
            new(Native.Mix_GetChunk(_index));

        internal MixChannel(int index)
        {
            _index = index;
        }

        /// <summary>
        /// Sets a function to be called when a channel finishes.
        /// </summary>
        /// <param name="function">The function to call.</param>
        public static void SetFinished(MixChannelFinishedCallback function)
        {
            s_finishedCallback = function;
            Native.Mix_ChannelFinished(s_finishedCallback == null ? null : &FinishedCallback);
        }

        /// <summary>
        /// Gets a particular channel.
        /// </summary>
        /// <param name="channel">The channel index.</param>
        /// <returns>The channel.</returns>
        public static MixChannel Get(int channel) =>
            new(channel);

        /// <summary>
        /// Allocates a number of channels for mixing.
        /// </summary>
        /// <param name="channels">The number of channels.</param>
        /// <returns>The number of channels allocated.</returns>
        public static int Allocate(int channels) =>
            Native.Mix_AllocateChannels(channels);

        /// <summary>
        /// Reserves a number of channels for mixing.
        /// </summary>
        /// <param name="number">The number of channels.</param>
        /// <returns>The number of channels reserved.</returns>
        public static int Reserve(int number) =>
            Native.Mix_ReserveChannels(number);

        /// <summary>
        /// Registers an effect on the channel.
        /// </summary>
        /// <param name="effect">The effect.</param>
        public void RegisterEffect(MixEffect effect) =>
            effect.Register(_index);

        /// <summary>
        /// Unregisters the effect on the channel.
        /// </summary>
        /// <param name="effect"></param>
        public void UnregisterEffect(MixEffect effect) =>
            effect.Unregister(_index);

        /// <summary>
        /// Unregisters all effects from the channel.
        /// </summary>
        public void UnregisterAllEffects() =>
            Native.CheckErrorZero(Native.Mix_UnregisterAllEffects(_index));

        /// <summary>
        /// Sets the panning on the channel.
        /// </summary>
        /// <param name="left">Left panning.</param>
        /// <param name="right">Right panning.</param>
        public void SetPanning(byte left, byte right) =>
            Native.CheckErrorBool(Native.Mix_SetPanning(_index, left, right));

        /// <summary>
        /// Sets the position of the channel.
        /// </summary>
        /// <param name="angle">The angle of the channel.</param>
        /// <param name="distance">The distance.</param>
        public void SetPosition(short angle, byte distance) =>
            Native.CheckErrorBool(Native.Mix_SetPosition(_index, angle, distance));

        /// <summary>
        /// Sets the distance of the channel.
        /// </summary>
        /// <param name="distance">The distance.</param>
        public void SetDistance(byte distance) =>
            Native.CheckErrorBool(Native.Mix_SetDistance(_index, distance));

        /// <summary>
        /// Reverses the stereo of the channel.
        /// </summary>
        /// <param name="flip">Whether to flip.</param>
        public void SetReverseStereo(bool flip) =>
            Native.CheckErrorBool(Native.Mix_SetReverseStereo(_index, Native.BoolToInt(flip)));

        /// <summary>
        /// Sets the volume of the channel.
        /// </summary>
        /// <param name="volume">The volume.</param>
        /// <returns>The old volume.</returns>
        public int Volume(int volume) =>
            Native.Mix_Volume(_index, volume);

        /// <summary>
        /// Halts the channel.
        /// </summary>
        public void Halt() =>
            Native.CheckError(Native.Mix_HaltChannel(_index));

        /// <summary>
        /// Expires the channel after an interval.
        /// </summary>
        /// <param name="ticks">The number of milliseconds until the channel expires.</param>
        /// <returns>The number of channels set to expire.</returns>
        public int Expire(int ticks) =>
            Native.Mix_ExpireChannel(_index, ticks);

        /// <summary>
        /// Fades the channel out.
        /// </summary>
        /// <param name="ms">The fade out interval.</param>
        /// <returns>The number of channels set to fade out.</returns>
        public int FadeOut(int ms) =>
            Native.Mix_FadeOutChannel(_index, ms);

        /// <summary>
        /// Pauses the channel.
        /// </summary>
        public void Pause() =>
            Native.Mix_Pause(_index);

        /// <summary>
        /// Resumes the channel.
        /// </summary>
        public void Resume() =>
            Native.Mix_Resume(_index);

        /// <summary>
        /// Plays a sample on the channel.
        /// </summary>
        /// <param name="chunk">The sample.</param>
        /// <param name="loops">The number of times to play the sample.</param>
        /// <returns>The channel the sample is playing on.</returns>
        public MixChannel Play(MixChunk chunk, int loops) =>
            Get(Native.CheckError(Native.Mix_PlayChannel(_index, chunk.ToNative(), loops)));

        /// <summary>
        /// Plays a sample on the channel.
        /// </summary>
        /// <param name="chunk">The sample.</param>
        /// <param name="loops">The number of times to play the sample.</param>
        /// <param name="ticks">The limit of time to play the sample.</param>
        /// <returns>The channel the sample is playing on.</returns>
        public MixChannel Play(MixChunk chunk, int loops, int ticks) =>
            Get(Native.CheckError(Native.Mix_PlayChannelTimed(_index, chunk.ToNative(), loops, ticks)));

        /// <summary>
        /// Fades a sample into a channel.
        /// </summary>
        /// <param name="chunk">The sample.</param>
        /// <param name="loops">The number of times to play the sample.</param>
        /// <param name="ms">The length of the fade in.</param>
        /// <returns>The channel the sample is playing on.</returns>
        public MixChannel FadeIn(MixChunk chunk, int loops, int ms) =>
            Get(Native.CheckError(Native.Mix_FadeInChannel(_index, chunk.ToNative(), loops, ms)));

        /// <summary>
        /// Fades a sample into a channel.
        /// </summary>
        /// <param name="chunk">The sample.</param>
        /// <param name="loops">The number of times to play the sample.</param>
        /// <param name="ms">The length of the fade in.</param>
        /// <param name="ticks">The limit of time to play the sample.</param>
        /// <returns>The channel the sample is playing on.</returns>
        public MixChannel FadeIn(MixChunk chunk, int loops, int ms, int ticks) =>
            Get(Native.CheckError(Native.Mix_FadeInChannelTimed(_index, chunk.ToNative(), loops, ms, ticks)));

        internal int ToNative() => _index;

        [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        internal static unsafe void FinishedCallback(int channel) => s_finishedCallback?.Invoke(new(channel));
    }
}
