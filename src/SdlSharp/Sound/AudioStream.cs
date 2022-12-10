namespace SdlSharp.Sound
{
    /// <summary>
    /// An audio stream that converts audio from one format to another.
    /// </summary>
#pragma warning disable CA1711 // Identifiers should not have incorrect suffix
    public unsafe struct AudioStream : IDisposable
#pragma warning restore CA1711 // Identifiers should not have incorrect suffix
    {
        private Native.SDL_AudioStream* _audioStream;

        private Native.SDL_AudioStream* Stream => _audioStream == null ? throw new InvalidOperationException() : _audioStream;

        /// <summary>
        /// The number of bytes available in the stream.
        /// </summary>
        public int Available => Native.SDL_AudioStreamAvailable(Stream);

        /// <summary>
        /// Creates a new audio stream.
        /// </summary>
        /// <param name="sourceFormat">The source audio format.</param>
        /// <param name="sourceChannels">The source audio channel count.</param>
        /// <param name="sourceRate">The source audio bitrate.</param>
        /// <param name="destinationFormat">The destination audio format.</param>
        /// <param name="destinationChannels">The destination audio channel count.</param>
        /// <param name="destinationRate">The destination audio bitrate.</param>
        /// <returns>The new audio stream.</returns>
        public AudioStream(AudioFormat sourceFormat, byte sourceChannels, int sourceRate, AudioFormat destinationFormat, byte destinationChannels, int destinationRate)
        {
            _audioStream = Native.CheckPointer(Native.SDL_NewAudioStream(sourceFormat.Format, sourceChannels, sourceRate, destinationFormat.Format, destinationChannels, destinationRate));
        }

        /// <summary>
        /// Puts data into the stream.
        /// </summary>
        /// <param name="data"></param>
        public void Put(Span<byte> data)
        {
            fixed (byte* dataPointer = data)
            {
                _ = Native.CheckError(Native.SDL_AudioStreamPut(Stream, dataPointer, data.Length));
            }
        }

        /// <summary>
        /// Gets data from the stream.
        /// </summary>
        /// <param name="data"></param>
        /// <returns>The number of bytes actually read.</returns>
        public int Get(Span<byte> data)
        {
            fixed (byte* dataPointer = data)
            {
                return Native.CheckError(Native.SDL_AudioStreamGet(Stream, dataPointer, data.Length));
            }
        }

        /// <summary>
        /// Flushes the audio stream.
        /// </summary>
        public void Flush() => Native.CheckError(Native.SDL_AudioStreamFlush(Stream));

        /// <summary>
        /// Clears any pending data in the audio stream.
        /// </summary>
        public void Clear() => Native.SDL_AudioStreamClear(Stream);

        /// <summary>
        /// Closes an audio stream.
        /// </summary>
        public void Dispose()
        {
            Native.SDL_FreeAudioStream(Stream);
            _audioStream = null;
        }
    }
}
