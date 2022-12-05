namespace SdlSharp.Sound
{
    /// <summary>
    /// Event args for the audio device added event
    /// </summary>
    public sealed unsafe class AudioDeviceAddedEventArgs : SdlEventArgs
    {
        /// <summary>
        /// The device added.
        /// </summary>
        public string DeviceName { get; }

        /// <summary>
        /// Whether the device is a capture device.
        /// </summary>
        public bool IsCapture { get; }

        internal AudioDeviceAddedEventArgs(Native.SDL_AudioDeviceEvent device) : base(device.Timestamp)
        {
            DeviceName = Native.Utf8ToString(Native.SDL_GetAudioDeviceName((int)device.Which, Native.BoolToInt(IsCapture)))!;
            IsCapture = device.IsCapture;
        }
    }
}
