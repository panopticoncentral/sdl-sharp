using System.Collections.Generic;

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
        public static IReadOnlyList<string> Decoders => s_decoders ?? (s_decoders = new ItemCollection<string>(
            index => Native.CheckNotNull(Native.Mix_GetChunkDecoder(index)),
            Native.Mix_GetNumChunkDecoders));

        /// <inheritdoc/>
        public override void Dispose()
        {
            Native.Mix_FreeChunk(Pointer);
            base.Dispose();
        }

        /// <summary>
        /// Whether the decoder is available.
        /// </summary>
        /// <param name="decoder">The decoder.</param>
        /// <returns><c>true</c> if it is, <c>false</c> otherwise.</returns>
        public static bool HasDecoder(string decoder) =>
            Native.Mix_HasChunkDecoder(decoder);

        /// <summary>
        /// Sets the volume of the sample.
        /// </summary>
        /// <param name="volume">The volume.</param>
        /// <returns>The old volume.</returns>
        public int Volume(int volume) =>
            Native.Mix_VolumeChunk(Pointer, volume);
    }
}
