namespace SdlSharp.Sound
{
    /// <summary>
    /// Event args for the audio device added event
    /// </summary>
    public sealed class AudioDeviceAddedEventArgs : SdlEventArgs
    {
        private readonly int _index;

        /// <summary>
        /// The device added.
        /// </summary>
        public string Device => Native.SDL_GetAudioDeviceName(_index, IsCapture).ToString()!;

        /// <summary>
        /// Whether the device is a capture device.
        /// </summary>
        public bool IsCapture { get; }

        internal AudioDeviceAddedEventArgs(Native.SDL_AudioDeviceEvent device) : base(device.Timestamp)
        {
            _index = (int)device.Which;
            IsCapture = device.IsCapture;
        }
    }
}
