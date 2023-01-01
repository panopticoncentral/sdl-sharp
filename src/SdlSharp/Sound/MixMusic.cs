using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SdlSharp.Sound
{
    /// <summary>
    /// Music that can be played through the mixer.
    /// </summary>
    public sealed unsafe class MixMusic : IDisposable
    {
        private readonly Native.Mix_Music* _music;

        private static MixHookCallback? s_musicHookCallback;
        private static MixHookFinishedCallback? s_musicHookFinishedCallback;

        /// <summary>
        /// The decoders for music.
        /// </summary>
        public static IReadOnlyList<string> Decoders => Native.GetIndexedCollection(
            i => Native.Utf8ToString(Native.Mix_GetMusicDecoder(i))!,
            Native.Mix_GetNumMusicDecoders);

        /// <summary>
        /// The type of the music.
        /// </summary>
        public MixMusicType Type => (MixMusicType)Native.Mix_GetMusicType(_music);

        /// <summary>
        /// The music's title, if any.
        /// </summary>
        public string? Title => Native.Utf8ToString(Native.Mix_GetMusicTitle(_music));

        /// <summary>
        /// The music's title tag, if any.
        /// </summary>
        public string? TitleTag => Native.Utf8ToString(Native.Mix_GetMusicTitleTag(_music));

        /// <summary>
        /// The music's artist tag, if any.
        /// </summary>
        public string? ArtistTag => Native.Utf8ToString(Native.Mix_GetMusicArtistTag(_music));

        /// <summary>
        /// The music's album tag, if any.
        /// </summary>
        public string? AlbumTag => Native.Utf8ToString(Native.Mix_GetMusicAlbumTag(_music));

        /// <summary>
        /// The music's copyright tag, if any.
        /// </summary>
        public string? CopyrightTag => Native.Utf8ToString(Native.Mix_GetMusicCopyrightTag(_music));

        /// <summary>
        /// The volume of the music.
        /// </summary>
        public int Volume => Native.Mix_GetMusicVolume(_music);

        /// <summary>
        /// The current position in the music stream.
        /// </summary>
        public double Position => Native.Mix_GetMusicPosition(_music);

        /// <summary>
        /// The duration of the music.
        /// </summary>
        public double Duration => Native.Mix_MusicDuration(_music);

        /// <summary>
        /// The loop start time position of music stream.
        /// </summary>
        public double LoopStartTime => Native.Mix_GetMusicLoopStartTime(_music);

        /// <summary>
        /// The loop end time position of music stream.
        /// </summary>
        public double LoopEndTime => Native.Mix_GetMusicLoopEndTime(_music);

        /// <summary>
        /// The loop length time of music stream.
        /// </summary>
        public double LoopLengthTime => Native.Mix_GetMusicLoopLengthTime(_music);

        /// <summary>
        /// The fading status of the music.
        /// </summary>
        public static MixFading Fading =>
            (MixFading)Native.Mix_FadingMusic();

        /// <summary>
        /// Whether the music is paused.
        /// </summary>
        public static bool Paused =>
            Native.Mix_PausedMusic() != 0;

        /// <summary>
        /// Whether the music is playing.
        /// </summary>
        public static bool Playing =>
            Native.Mix_PlayingMusic() != 0;

        internal MixMusic(Native.Mix_Music* music)
        {
            _music = music;
        }

        /// <summary>
        /// Loads a music file.
        /// </summary>
        /// <param name="file">The file to load.</param>
        /// <returns>The music.</returns>
        public static MixMusic Load(string file)
        {
            fixed (byte* ptr = Native.StringToUtf8(file))
            {
                return new(Native.Mix_LoadMUS(ptr));
            }
        }

        /// <summary>
        /// Loads a music file from storage.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <param name="shouldDispose">Whether the storage should be disposed after loading.</param>
        /// <returns>The music.</returns>
        public static MixMusic Load(RWOps rwops, bool shouldDispose) =>
            new(Native.Mix_LoadMUS_RW(rwops.ToNative(), Native.BoolToInt(shouldDispose)));

        /// <summary>
        /// Sets a function to be called to play music.
        /// </summary>
        /// <param name="function">The function to call.</param>
        public static void SetHook(MixHookCallback? function)
        {
            s_musicHookCallback = function;
            Native.Mix_HookMusic(s_musicHookCallback == null ? null : &HookCallback, 0);
        }

        /// <summary>
        /// Sets a function to be called when the music is finished.
        /// </summary>
        /// <param name="function">The function to call.</param>
        public static void SetFinishedHook(MixHookFinishedCallback function)
        {
            s_musicHookFinishedCallback = function;
            Native.Mix_HookMusicFinished(s_musicHookFinishedCallback == null ? null : &HookFinishedCallback);
        }

        /// <summary>
        /// Loads a music file from storage.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <param name="type">The type of the music.</param>
        /// <param name="shouldDispose">Whether the storage should be disposed after loading.</param>
        /// <returns>The music.</returns>
        public static MixMusic Load(RWOps rwops, MixMusicType type, bool shouldDispose) =>
            new(Native.Mix_LoadMUSType_RW(rwops.ToNative(), (Native.Mix_MusicType)type, Native.BoolToInt(shouldDispose)));

        /// <inheritdoc/>
        public void Dispose() => Native.Mix_FreeMusic(_music);

        /// <summary>
        /// Whether the decoder is available.
        /// </summary>
        /// <param name="decoder">The decoder.</param>
        /// <returns><c>true</c> if it is, <c>false</c> otherwise.</returns>
        public static bool HasDecoder(string decoder)
        {
            fixed (byte* ptr = Native.StringToUtf8(decoder))
            {
                return Native.Mix_HasMusicDecoder(ptr);
            }
        }

        /// <summary>
        /// Sets the music volume.
        /// </summary>
        /// <param name="volume">The volume.</param>
        /// <returns>The previous volume.</returns>
        public static int SetVolume(int volume) =>
            Native.Mix_VolumeMusic(volume);

        /// <summary>
        /// Halts music.
        /// </summary>
        public static void Halt() =>
            Native.CheckError(Native.Mix_HaltMusic());

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
            _ = Native.CheckError(Native.Mix_SetMusicPosition(position));

        /// <summary>
        /// Sets the command to use to play the music.
        /// </summary>
        /// <param name="command">The command string.</param>
        public static void SetCommand(string command)
        {
            fixed (byte* ptr = Native.StringToUtf8(command))
            {
                _ = Native.CheckError(Native.Mix_SetMusicCMD(ptr));
            }
        }

        /// <summary>
        /// Jump to a given order in MOD music.
        /// </summary>
        /// <param name="order">The order.</param>
        public static void ModJumpToOrder(int order) =>
            _ = Native.CheckError(Native.Mix_ModMusicJumpToOrder(order));

        /// <summary>
        /// Plays the music.
        /// </summary>
        /// <param name="loops">The number of times to repeat the music.</param>
        public void Play(int loops) =>
            Native.CheckError(Native.Mix_PlayMusic(_music, loops));

        /// <summary>
        /// Fades in the music.
        /// </summary>
        /// <param name="loops">The number of times to repeat the music.</param>
        /// <param name="ms">The length of the fade in.</param>
        public void FadeIn(int loops, int ms) =>
            Native.CheckError(Native.Mix_FadeInMusic(_music, loops, ms));

        /// <summary>
        /// Fades in the music.
        /// </summary>
        /// <param name="loops">The number of times to repeat the music.</param>
        /// <param name="ms">The length of the fade in.</param>
        /// <param name="position">The position to start.</param>
        public void FadeIn(int loops, int ms, double position) =>
            Native.CheckError(Native.Mix_FadeInMusicPos(_music, loops, ms, position));

        [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        internal static unsafe void HookCallback(nuint _, byte* buffer, int len) => s_musicHookCallback?.Invoke(new Span<byte>(buffer, len));

        [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        internal static unsafe void HookFinishedCallback() => s_musicHookFinishedCallback?.Invoke();

    }
}
