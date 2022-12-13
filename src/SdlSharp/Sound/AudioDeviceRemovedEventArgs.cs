namespace SdlSharp.Sound
{
    /// <summary>
    /// Event args for the audio device removed event
    /// </summary>
    public sealed class AudioDeviceRemovedEventArgs : SdlEventArgs
    {
        /// <summary>
        /// The device removed (if null, then the device removed was not open).
        /// </summary>
        public AudioDevice? Device { get; }

        /// <summary>
        /// Whether the device is a capture device.
        /// </summary>
        public bool IsCapture { get; }

        internal AudioDeviceRemovedEventArgs(Native.SDL_AudioDeviceEvent device) : base(device.timestamp)
        {
            Device = device.which == 0 ? null : new AudioDevice(new(device.which), null);
            IsCapture = device.iscapture != 0;
        }
    }
}
