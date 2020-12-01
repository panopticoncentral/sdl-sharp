using System.Collections.Generic;

namespace SdlSharp.Sound
{
    /// <summary>
    /// Music that can be played through the mixer.
    /// </summary>
    public sealed unsafe class MixMusic : NativePointerBase<Native.Mix_Music, MixMusic>
    {
        private static ItemCollection<string>? s_decoders;

        /// <summary>
        /// The decoders for music.
        /// </summary>
        public static IReadOnlyList<string> Decoders => s_decoders ??= new ItemCollection<string>(
            index => SdlSharp.Native.CheckNotNull(SdlSharp.Native.Mix_GetMusicDecoder(index)),
            SdlSharp.Native.Mix_GetNumMusicDecoders);

        /// <summary>
        /// The type of the music.
        /// </summary>
        public MusicType Type =>
            SdlSharp.Native.Mix_GetMusicType(Native);

        /// <inheritdoc/>
        public override void Dispose()
        {
            SdlSharp.Native.Mix_FreeMusic(Native);
            base.Dispose();
        }

        /// <summary>
        /// Whether the decoder is available.
        /// </summary>
        /// <param name="decoder">The decoder.</param>
        /// <returns><c>true</c> if it is, <c>false</c> otherwise.</returns>
        public static bool HasDecoder(string decoder) =>
            SdlSharp.Native.Mix_HasMusicDecoder(decoder);

        /// <summary>
        /// Plays the music.
        /// </summary>
        /// <param name="loops">The number of times to repeat the music.</param>
        public void Play(int loops) =>
            SdlSharp.Native.CheckError(SdlSharp.Native.Mix_PlayMusic(Native, loops));

        /// <summary>
        /// Fades in the music.
        /// </summary>
        /// <param name="loops">The number of times to repeat the music.</param>
        /// <param name="ms">The length of the fade in.</param>
        public void FadeIn(int loops, int ms) =>
            SdlSharp.Native.CheckError(SdlSharp.Native.Mix_FadeInMusic(Native, loops, ms));

        /// <summary>
        /// Fades in the music.
        /// </summary>
        /// <param name="loops">The number of times to repeat the music.</param>
        /// <param name="ms">The length of the fade in.</param>
        /// <param name="position">The position to start.</param>
        public void FadeIn(int loops, int ms, double position) =>
            SdlSharp.Native.CheckError(SdlSharp.Native.Mix_FadeInMusicPos(Native, loops, ms, position));
    }
}
