using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SdlSharp.Sound
{
    /// <summary>
    /// An audio mixer.
    /// </summary>
    public static unsafe class Mixer
    {
        /// <summary>
        /// The default frequency.
        /// </summary>
        public const int DefaultFrequency = Native.MIX_DEFAULT_FREQUENCY;

        /// <summary>
        /// The default format.
        /// </summary>
        public static AudioFormat DefaultAudioFormat { get; } = new(Native.MIX_DEFAULT_FORMAT);

        /// <summary>
        /// The default number of channels;
        /// </summary>
        public const int DefaultChannels = Native.MIX_DEFAULT_CHANNELS;

        /// <summary>
        /// The maximum volume.
        /// </summary>
        public const int MaxVolume = Audio.MixMaxVolume;

        /// <summary>
        /// The mix channel that represents post effects.
        /// </summary>
        public const int MixChannelPost = Native.MIX_CHANNEL_POST;

        /// <summary>
        /// The environment variable that controls the max speed of the built-in effects.
        /// </summary>
        public const string EffectsMaxSpeedEnvironmentVariable = "MIX_EFFECTSMAXSPEED";

        private static MixHookCallback? s_postMixCallback;

        /// <summary>
        /// The sound fonts for supported MIDI backends.
        /// </summary>`
        public static string? SoundFonts
        {
            get => Native.Utf8ToString(Native.Mix_GetSoundFonts());
            set => Native.StringToUtf8Action(value, ptr => _ = Native.CheckError(Native.Mix_SetSoundFonts(ptr)));
        }

        /// <summary>
        /// The Timidity config file, if any.
        /// </summary>
        public static string? TimidityConfiguration
        {
            get => Native.Utf8ToString(Native.Mix_GetTimidityCfg());
            set => Native.StringToUtf8Action(value, ptr => _ = Native.CheckError(Native.Mix_SetTimidityCfg(ptr)));
        }

        /// <summary>
        /// Initializes the mixer.
        /// </summary>
        /// <param name="frequency">The frequency of the mixer.</param>
        /// <param name="format">The audio format of the mixer.</param>
        /// <param name="channels">The number of channels.</param>
        /// <param name="chunksize">The size of each output sample.</param>
        public static void OpenAudio(int frequency, AudioFormat format, int channels, int chunksize) =>
            Native.CheckError(Native.Mix_OpenAudio(frequency, format.Format.Value, channels, chunksize));

        /// <summary>
        /// Initializes the mixer.
        /// </summary>
        /// <param name="frequency">The frequency of the mixer.</param>
        /// <param name="format">The audio format of the mixer.</param>
        /// <param name="channels">The number of channels.</param>
        /// <param name="chunksize">The size of each output sample.</param>
        /// <param name="device">The audio device to use.</param>
        /// <param name="allowedChanges">Changes to the format that are allowed.</param>
        public static void OpenAudio(int frequency, AudioFormat format, int channels, int chunksize, string device, AudioAllowChange allowedChanges) => Native.StringToUtf8Action(device, ptr => _ = Native.CheckError(Native.Mix_OpenAudioDevice(frequency, format.Format.Value, channels, chunksize, ptr, (int)allowedChanges)));

        /// <summary>
        /// Closes the mixer.
        /// </summary>
        public static void CloseAudio() =>
            Native.Mix_CloseAudio();

        /// <summary>
        /// Queries the current specification of the mixer.
        /// </summary>
        /// <param name="frequency">The frequency of the mixer.</param>
        /// <param name="format">The format of the mixer.</param>
        /// <param name="channels">The number of channels.</param>
        /// <returns>The number of times the mixer has been opened.</returns>
        public static int QuerySpec(out int frequency, out AudioFormat format, out int channels)
        {
            int frequencyLocal;
            ushort formatLocal;
            int channelsLocal;
            var result = Native.CheckErrorZero(Native.Mix_QuerySpec(&frequencyLocal, &formatLocal, &channelsLocal));
            frequency = frequencyLocal;
            format = new(new(formatLocal));
            channels = channelsLocal;
            return result;
        }

        /// <summary>
        /// Sets a function to be called after the mix.
        /// </summary>
        /// <param name="function">The function to call.</param>
        public static void SetPostMixHook(MixHookCallback? function)
        {
            s_postMixCallback = function;
            Native.Mix_SetPostMix(s_postMixCallback == null ? null : &PostMixCallback, 0);
        }

        /// <summary>
        /// Sets the master volume.
        /// </summary>
        /// <param name="volume">The volume.</param>
        /// <returns>The previous volume.</returns>
        public static int SetMasterVolume(int volume) => Native.Mix_MasterVolume(volume);

        [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        internal static unsafe void PostMixCallback(nuint _, byte* buffer, int len) => s_postMixCallback?.Invoke(new Span<byte>(buffer, len));
    }
}
