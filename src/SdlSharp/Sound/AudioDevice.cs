using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SdlSharp.Sound
{
    /// <summary>
    /// An audio device.
    /// </summary>
    public unsafe struct AudioDevice : IDisposable
    {
        private static Dictionary<nint, AudioCallback>? s_audioCallbacks;

        private readonly Native.SDL_AudioDeviceID _deviceId;
        private readonly AudioCallback? _callback;

        private static Dictionary<nint, AudioCallback> AudioCallbacks => s_audioCallbacks ??= new();

        /// <summary>
        /// The status of the audio device.
        /// </summary>
        public AudioStatus Status =>
            Native.SDL_GetAudioDeviceStatus(_deviceId);

        /// <summary>
        /// The size of the queued audio.
        /// </summary>
        public uint QueuedAudioSize =>
            Native.SDL_GetQueuedAudioSize(_deviceId);

        internal AudioDevice(Native.SDL_AudioDeviceID deviceId, AudioCallback? callback)
        {
            _deviceId = deviceId;
            _callback = callback;

            if (callback != null)
            {
                AudioCallbacks[callback.GetHashCode()] = callback;
            }
        }

        /// <summary>
        ///  Pauses the audio device.
        /// </summary>
        public void Pause() =>
            Native.SDL_PauseAudioDevice(_deviceId, true);

        /// <summary>
        /// Unpauses the audio device.
        /// </summary>
        public void Unpause() =>
            Native.SDL_PauseAudioDevice(_deviceId, false);

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
        public void Dispose()
        {
            Native.SDL_CloseAudioDevice(_deviceId);

            if (_callback != null)
            {
                _ = AudioCallbacks.Remove(_callback.GetHashCode());
            }
        }

        internal static void AudioCallback(nint userData, byte* stream, int len)
        {
            if (AudioCallbacks.TryGetValue(userData, out var callback))
            {
                callback(new Span<byte>(stream, len));
            }
        }
    }
}
