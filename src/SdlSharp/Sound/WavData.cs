namespace SdlSharp.Sound
{
    /// <summary>
    /// A WAV audio file.
    /// </summary>
    public sealed unsafe class WavData : IDisposable
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
    }
}
