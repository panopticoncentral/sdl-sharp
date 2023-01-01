namespace SdlSharp.Sound
{
    /// <summary>
    /// An audio sample.
    /// </summary>
    public sealed unsafe class MixChunk : IDisposable
    {
        private readonly Native.Mix_Chunk* _chunk;

        /// <summary>
        /// The decoders for samples.
        /// </summary>
        public static IReadOnlyList<string> Decoders => Native.GetIndexedCollection(
            i => Native.Utf8ToString(Native.Mix_GetChunkDecoder(i))!,
            Native.Mix_GetNumChunkDecoders);

        internal MixChunk(Native.Mix_Chunk* chunk)
        {
            _chunk = chunk;
        }

        /// <inheritdoc/>
        public void Dispose() => Native.Mix_FreeChunk(_chunk);

        /// <summary>
        /// Loads a music sample from storage.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <param name="shouldDispose">Whether the storage should be disposed when done.</param>
        /// <returns>The sample.</returns>
        public static MixChunk LoadWav(RWOps rwops, bool shouldDispose) =>
            new(Native.Mix_LoadWAV_RW(rwops.ToNative(), Native.BoolToInt(shouldDispose)));

        /// <summary>
        /// Loads a music sample from a file.
        /// </summary>
        /// <param name="file">The file to load.</param>
        /// <returns>The sample.</returns>
        public static MixChunk LoadWav(string file)
        {
            fixed (byte* ptr = Native.StringToUtf8(file))
            {
                return new(Native.Mix_LoadWAV(ptr));
            }
        }

        /// <summary>
        /// Quickly loads a music sample, which must be in the correct format.
        /// </summary>
        /// <param name="mem">Pointer to the memory.</param>
        /// <returns>The sample.</returns>
        public static MixChunk QuickLoadWav(Span<byte> mem)
        {
            fixed (byte* memPointer = mem)
            {
                return new(Native.Mix_QuickLoad_WAV(memPointer));
            }
        }

        /// <summary>
        /// Quickly loads a raw sample, must be in correct format.
        /// </summary>
        /// <param name="mem">The memory to load from.</param>
        /// <returns>The music sample.</returns>
        public static MixChunk QuickLoadRaw(Span<byte> mem)
        {
            fixed (byte* memPointer = mem)
            {
                return new(Native.Mix_QuickLoad_RAW(memPointer, (uint)mem.Length));
            }
        }

        /// <summary>
        /// Whether the decoder is available.
        /// </summary>
        /// <param name="decoder">The decoder.</param>
        /// <returns><c>true</c> if it is, <c>false</c> otherwise.</returns>
        public static bool HasDecoder(string decoder)
        {
            fixed (byte* ptr = Native.StringToUtf8(decoder))
            {
                return Native.Mix_HasChunkDecoder(ptr);
            }
        }

        /// <summary>
        /// Sets the volume of the sample.
        /// </summary>
        /// <param name="volume">The volume.</param>
        /// <returns>The old volume.</returns>
        public int Volume(int volume) =>
            Native.Mix_VolumeChunk(_chunk, volume);

        internal Native.Mix_Chunk* ToNative() => _chunk;
    }
}
