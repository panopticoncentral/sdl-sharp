namespace SdlSharp.Sound
{
    /// <summary>
    /// An audio sample.
    /// </summary>
    public sealed unsafe class MixChunk : NativePointerBase<Native.Mix_Chunk, MixChunk>
    {
        private static ItemCollection<string>? s_decoders;

        /// <summary>
        /// The decoders for samples.
        /// </summary>
        public static IReadOnlyList<string> Decoders => s_decoders ??= new ItemCollection<string>(
            index => SdlSharp.Native.CheckNotNull(SdlSharp.Native.Mix_GetChunkDecoder(index)),
            SdlSharp.Native.Mix_GetNumChunkDecoders);

        /// <inheritdoc/>
        public override void Dispose()
        {
            SdlSharp.Native.Mix_FreeChunk(Native);
            base.Dispose();
        }

        /// <summary>
        /// Whether the decoder is available.
        /// </summary>
        /// <param name="decoder">The decoder.</param>
        /// <returns><c>true</c> if it is, <c>false</c> otherwise.</returns>
        public static bool HasDecoder(string decoder) =>
            SdlSharp.Native.Mix_HasChunkDecoder(decoder);

        /// <summary>
        /// Sets the volume of the sample.
        /// </summary>
        /// <param name="volume">The volume.</param>
        /// <returns>The old volume.</returns>
        public int Volume(int volume) =>
            SdlSharp.Native.Mix_VolumeChunk(Native, volume);
    }
}
