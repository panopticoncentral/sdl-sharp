namespace SdlSharp.Sound
{
    /// <summary>
    /// An audio device.
    /// </summary>
    public sealed unsafe class AudioDevice : IDisposable
    {
        private readonly Native.SDL_AudioDeviceID _deviceId;

        /// <summary>
        /// The status of the audio device.
        /// </summary>
        public AudioStatus Status =>
            (AudioStatus)Native.SDL_GetAudioDeviceStatus(_deviceId);

        /// <summary>
        /// The size of the queued audio.
        /// </summary>
        public uint QueuedAudioSize =>
            Native.SDL_GetQueuedAudioSize(_deviceId);

        internal AudioDevice(Native.SDL_AudioDeviceID deviceId)
        {
            _deviceId = deviceId;
        }

        /// <summary>
        ///  Pauses the audio device.
        /// </summary>
        public void Pause() =>
            Native.SDL_PauseAudioDevice(_deviceId, Native.BoolToInt(true));

        /// <summary>
        /// Unpauses the audio device.
        /// </summary>
        public void Unpause() =>
            Native.SDL_PauseAudioDevice(_deviceId, Native.BoolToInt(false));

        /// <summary>
        /// Queues audio for playback.
        /// </summary>
        /// <param name="audio">The audio data.</param>
        public void QueueAudio(Span<byte> audio)
        {
            fixed (byte* audioPointer = audio)
            {
                _ = Native.CheckError(Native.SDL_QueueAudio(_deviceId, audioPointer, (uint)audio.Length));
            }
        }

        /// <summary>
        /// Dequeues audio from a capture device.
        /// </summary>
        /// <param name="audio">The buffer to dequeue into.</param>
        /// <returns>The number of bytes dequeued.</returns>
        public uint DequeueAudio(Span<byte> audio)
        {
            fixed (byte* audioPointer = audio)
            {
                return Native.SDL_DequeueAudio(_deviceId, audioPointer, (uint)audio.Length);
            }
        }

        /// <summary>
        /// Clears all queued audio from the device.
        /// </summary>
        public void ClearQueuedAudio() =>
            Native.SDL_ClearQueuedAudio(_deviceId);

        /// <summary>
        /// Locks the device for multi-threaded access.
        /// </summary>
        public void Lock() =>
            Native.SDL_LockAudioDevice(_deviceId);

        /// <summary>
        /// Unlocks the device.
        /// </summary>
        public void Unlock() =>
            Native.SDL_UnlockAudioDevice(_deviceId);

        /// <summary>
        /// Closes the audio device.
        /// </summary>
        public void Dispose() => Native.SDL_CloseAudioDevice(_deviceId);
    }
}
