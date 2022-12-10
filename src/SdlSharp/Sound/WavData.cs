namespace SdlSharp.Sound
{
    /// <summary>
    /// A WAV audio file.
    /// </summary>
    public unsafe struct WavData : IDisposable
    {
        private byte* _data;
        private uint _length;

        /// <summary>
        /// The audio data.
        /// </summary>
        public Span<byte> Data => new(_data, (int)_length);

        internal WavData(byte* data, uint length)
        {
            _data = data;
            _length = length;
        }

        /// <summary>
        /// Frees the audio data.
        /// </summary>
        public void Dispose()
        {
            if (_data != null)
            {
                Native.SDL_FreeWAV(_data);
                _data = null;
                _length = 0;
            }
        }

        /// <summary>
        /// Converts a WavData to a span of bytes.
        /// </summary>
        /// <param name="wavData">The wav file data.</param>
        public static implicit operator Span<byte>(WavData wavData) => wavData.Data;
    }
}
