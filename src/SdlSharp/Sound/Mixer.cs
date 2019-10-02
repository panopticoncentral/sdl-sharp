using System;

// There are going to be unused fields in some of the interop structures
#pragma warning disable CS0169, RCS1213, IDE0051, IDE0052

namespace SdlSharp.Sound
{
    /// <summary>
    /// An audio mixer.
    /// </summary>
    public unsafe static class Mixer
    {
        /// <summary>
        /// The default frequency.
        /// </summary>
        public const int DefaultFrequency = 22050;

        /// <summary>
        /// The default format.
        /// </summary>
        public static AudioFormat DefaultAudioFormat { get; } = BitConverter.IsLittleEndian ? AudioFormat.Signed16BitLittleEndian : AudioFormat.Signed16BitBigEndian;

        /// <summary>
        /// The default number of channels;
        /// </summary>
        public const int DefaultChannels = 2;

        /// <summary>
        /// The maximum volume.
        /// </summary>
        public const int MaxVolume = Audio.MixMaxVolume;

        /// <summary>
        /// The mix channel that represents post effects.
        /// </summary>
        public const int MixChannelPost = -2;

        /// <summary>
        /// The environment variable that controls the max speed of the built-in effects.
        /// </summary>
        public const string EffectsMaxSpeedEnvironmentVariable = "MIX_EFFECTSMAXSPEED";

        private static Native.MixFunctionDelegate? s_postMixHook;
        private static Native.MixFunctionDelegate? s_playMusicHook;
        private static MusicFinishedDelegate? s_musicFinishedHook;
        private static Native.MusicChannelFinishedDelegate? s_channelFinishedHook;

        /// <summary>
        /// The user data of the current music hook.
        /// </summary>
        public static IntPtr PlayMusicHookData =>
            Native.Mix_GetMusicHookData();

        /// <summary>
        /// Synchro value for the mix.
        /// </summary>
        public static int SynchroValue
        {
            get => Native.Mix_GetSynchroValue();
            set => Native.CheckError(Native.Mix_SetSynchroValue(value));
        }

        /// <summary>
        /// The sound fonts for supported MIDI backends.
        /// </summary>
        public static string SoundFonts
        {
            get => Native.Mix_GetSoundFonts();
            set => Native.CheckError(Native.Mix_SetSoundFonts(value));
        }

        /// <summary>
        /// The fading status of the music.
        /// </summary>
        public static Fading Fading =>
            Native.Mix_FadingMusic();

        /// <summary>
        /// Whether the music is paused.
        /// </summary>
        public static bool Paused =>
            Native.Mix_PausedMusic();

        /// <summary>
        /// Whether the music is playing.
        /// </summary>
        public static bool Playing =>
            Native.Mix_PlayingMusic();

        /// <summary>
        /// Initializes the mixer.
        /// </summary>
        /// <param name="frequency">The frequency of the mixer.</param>
        /// <param name="format">The audio format of the mixer.</param>
        /// <param name="channels">The number of channels.</param>
        /// <param name="chunksize">The size of each output sample.</param>
        public static void OpenAudio(int frequency, AudioFormat format, int channels, int chunksize) =>
            Native.CheckError(Native.Mix_OpenAudio(frequency, format, channels, chunksize));

        /// <summary>
        /// Initializes the mixer.
        /// </summary>
        /// <param name="frequency">The frequency of the mixer.</param>
        /// <param name="format">The audio format of the mixer.</param>
        /// <param name="channels">The number of channels.</param>
        /// <param name="chunksize">The size of each output sample.</param>
        /// <param name="device">The audio device to use.</param>
        /// <param name="allowedChanges">Changes to the format that are allowed.</param>
        public static void OpenAudio(int frequency, AudioFormat format, int channels, int chunksize, string device, AudioAllowChange allowedChanges)
        {
            using var utf8Device = Utf8String.ToUtf8String(device);
            _ = Native.CheckError(Native.Mix_OpenAudioDevice(frequency, format, channels, chunksize, utf8Device, allowedChanges));
        }

        /// <summary>
        /// Closes the mixer.
        /// </summary>
        public static void CloseAudio() =>
            Native.Mix_CloseAudio();

        /// <summary>
        /// Allocates a number of channels for mixing.
        /// </summary>
        /// <param name="channels">The number of channels.</param>
        /// <returns>The number of channels allocated.</returns>
        public static int AllocateChannels(int channels) =>
            Native.Mix_AllocateChannels(channels);

        /// <summary>
        /// Reserves a number of channels for mixing.
        /// </summary>
        /// <param name="channels">The number of channels.</param>
        /// <returns>The number of channels reserved.</returns>
        public static int ReserveChannels(int number) =>
            Native.Mix_ReserveChannels(number);

        /// <summary>
        /// Queries the current specification of the mixer.
        /// </summary>
        /// <param name="frequency">The frequency of the mixer.</param>
        /// <param name="format">The format of the mixer.</param>
        /// <param name="channels">The number of channels.</param>
        /// <returns>The number of times the mixer has been opened.</returns>
        public static int QuerySpec(out int frequency, out AudioFormat format, out int channels) =>
            Native.CheckErrorZero(Native.Mix_QuerySpec(out frequency, out format, out channels));

        /// <summary>
        /// Loads a music sample from storage.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <param name="shouldDispose">Whether the storage should be disposed when done.</param>
        /// <returns>The sample.</returns>
        public static MixChunk LoadWav(RWOps rwops, bool shouldDispose) =>
            MixChunk.PointerToInstanceNotNull(Native.Mix_LoadWAV_RW(rwops.Pointer, shouldDispose));

        /// <summary>
        /// Loads a music sample from a file.
        /// </summary>
        /// <param name="file">The file to load.</param>
        /// <returns>The sample.</returns>
        public static MixChunk LoadWav(string file) =>
            MixChunk.PointerToInstanceNotNull(Native.Mix_LoadWAV(file));

        /// <summary>
        /// Quickly loads a music sample, which must be in the correct format.
        /// </summary>
        /// <param name="mem">Pointer to the memory.</param>
        /// <returns>The sample.</returns>
        public static MixChunk QuickLoadWav(Span<byte> mem)
        {
            fixed (byte* memPointer = mem)
            {
                return MixChunk.PointerToInstanceNotNull(Native.Mix_QuickLoad_WAV(memPointer));
            }
        }

        /// <summary>
        /// Loads a music file.
        /// </summary>
        /// <param name="file">The file to load.</param>
        /// <returns>The music.</returns>
        public static MixMusic LoadMusic(string file) =>
            MixMusic.PointerToInstanceNotNull(Native.Mix_LoadMUS(file));

        /// <summary>
        /// Loads a music file from storage.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <param name="shouldDispose">Whether the storage should be disposed after loading.</param>
        /// <returns>The music.</returns>
        public static MixMusic LoadMusic(RWOps rwops, bool shouldDispose) =>
            MixMusic.PointerToInstanceNotNull(Native.Mix_LoadMUS_RW(rwops.Pointer, shouldDispose));

        /// <summary>
        /// Loads a music file from storage.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <param name="type">The type of the music.</param>
        /// <param name="shouldDispose">Whether the storage should be disposed after loading.</param>
        /// <returns>The music.</returns>
        public static MixMusic LoadMusic(RWOps rwops, MusicType type, bool shouldDispose) =>
            MixMusic.PointerToInstanceNotNull(Native.Mix_LoadMUSType_RW(rwops.Pointer, type, shouldDispose));

        /// <summary>
        /// Quickly loads a raw sample, must be in correct format.
        /// </summary>
        /// <param name="mem">The memory to load from.</param>
        /// <returns>The music sample.</returns>
        public static MixChunk QuickLoadRaw(Span<byte> mem)
        {
            fixed (byte* memPointer = mem)
            {
                return MixChunk.PointerToInstanceNotNull(Native.Mix_QuickLoad_RAW(memPointer, (uint)mem.Length));
            }
        }

        /// <summary>
        /// Sets a function to be called after the mix.
        /// </summary>
        /// <param name="function">The function to call.</param>
        /// <param name="userData">User data.</param>
        public static void SetPostMixHook(MixFunctionDelegate function, IntPtr userData)
        {
            s_postMixHook = new MixFunctionWrapper(function).MixFunction;
            Native.Mix_SetPostMix(s_postMixHook, userData);
        }

        /// <summary>
        /// Sets a function to be called to play music.
        /// </summary>
        /// <param name="function">The function to call.</param>
        /// <param name="userData">User data.</param>
        public static void SetPlayMusicHook(MixFunctionDelegate function, IntPtr userData)
        {
            s_playMusicHook = new MixFunctionWrapper(function).MixFunction;
            Native.Mix_HookMusic(s_playMusicHook, userData);
        }

        /// <summary>
        /// Sets a function to be called when the music is finished.
        /// </summary>
        /// <param name="function">The function to call.</param>
        public static void SetMusicFinishedHook(MusicFinishedDelegate function)
        {
            s_musicFinishedHook = function;
            Native.Mix_HookMusicFinished(function);
        }

        /// <summary>
        /// Sets a function to be called when a channel finishes.
        /// </summary>
        /// <param name="function">The function to call.</param>
        public static void SetChannelFinishedHook(MusicChannelFinishedDelegate function)
        {
            s_channelFinishedHook = new MusicChannelFinishedWrapper(function).MusicChannelFinished;
            Native.Mix_ChannelFinished(s_channelFinishedHook);
        }

        /// <summary>
        /// Sets the mixer volume.
        /// </summary>
        /// <param name="volume">The volume.</param>
        /// <returns>The previous volume.</returns>
        public static int Volume(int volume) =>
            Native.Mix_VolumeMusic(volume);

        /// <summary>
        /// Halts music.
        /// </summary>
        public static void Halt() =>
            Native.Mix_HaltMusic();

        /// <summary>
        /// Fades out music.
        /// </summary>
        /// <param name="ms">The length of the fade.</param>
        /// <returns>Whether the fade was successful.</returns>
        public static bool FadeOut(int ms) =>
            Native.Mix_FadeOutMusic(ms);

        /// <summary>
        /// Pauses the music.
        /// </summary>
        public static void Pause() =>
            Native.Mix_PauseMusic();

        /// <summary>
        /// Resumes the music.
        /// </summary>
        public static void Resume() =>
            Native.Mix_ResumeMusic();

        /// <summary>
        /// Rewinds the music to the beginning.
        /// </summary>
        public static void Rewind() =>
            Native.Mix_RewindMusic();

        /// <summary>
        /// Sets the music to the specified position.
        /// </summary>
        /// <param name="position">The position.</param>
        public static void SetPosition(double position) =>
            Native.CheckError(Native.Mix_SetMusicPosition(position));

        /// <summary>
        /// Sets the command to use to play the music.
        /// </summary>
        /// <param name="command">The command string.</param>
        public static void SetCommand(string command) =>
            Native.CheckError(Native.Mix_SetMusicCMD(command));

        /// <summary>
        /// Calls a function for each sound font.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <param name="data">User data.</param>
        /// <returns>Whether all the callbacks succeeded.</returns>
        public static bool EachSoundFont(Func<string, IntPtr, bool> callback, IntPtr data)
        {
            var wrapper = new EachWrapper(callback);
            return Native.Mix_EachSoundFont(wrapper.Callback, data);
        }

        private sealed class EachWrapper
        {
            private readonly Func<string, IntPtr, bool> _callback;

            public EachWrapper(Func<string, IntPtr, bool> callback)
            {
                _callback = callback;
            }

            public bool Callback(string s, IntPtr data) =>
                _callback(s, data);
        }

        private sealed class MusicChannelFinishedWrapper
        {
            private readonly MusicChannelFinishedDelegate _finishedDelegate;

            public void MusicChannelFinished(int channel)
            {
                _finishedDelegate(MixChannel.Get(channel));
            }

            public MusicChannelFinishedWrapper(MusicChannelFinishedDelegate finishedDelegate)
            {
                _finishedDelegate = finishedDelegate;
            }
        }

        private sealed class MixFunctionWrapper
        {
            private readonly MixFunctionDelegate _mixDelegate;

            public void MixFunction(IntPtr udata, IntPtr stream, int len)
            {
                _mixDelegate(new Span<byte>((void*)stream, len), udata);
            }

            public MixFunctionWrapper(MixFunctionDelegate mixDelegate)
            {
                _mixDelegate = mixDelegate;
            }
        }
    }
}
