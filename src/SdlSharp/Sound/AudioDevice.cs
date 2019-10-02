using System;
using System.Collections.Generic;

namespace SdlSharp.Sound
{
    /// <summary>
    /// An audio device.
    /// </summary>
    public unsafe sealed class AudioDevice : NativeStaticIndexBase<Native.SDL_AudioDeviceID, AudioDevice>, IDisposable
    {
        private List<Native.SDL_AudioCallback>? _callbacks;

        private List<Native.SDL_AudioCallback> Callbacks => _callbacks ?? (_callbacks = new List<Native.SDL_AudioCallback>());

        internal void AddAudioCallback(Native.SDL_AudioCallback? callback)
        {
            if (callback != null)
            {
                Callbacks.Add(callback);
            }
        }

        /// <summary>
        /// The status of the audio device.
        /// </summary>
        public AudioStatus Status =>
            Native.SDL_GetAudioDeviceStatus(Index);

        /// <summary>
        /// The size of the queued audio.
        /// </summary>
        public uint QueuedAudioSize =>
            Native.SDL_GetQueuedAudioSize(Index);

        /// <summary>
        /// Event fired when an audio device is added to the system.
        /// </summary>
        public static event EventHandler<AudioDeviceAddedEventArgs>? Added;

        /// <summary>
        /// Event fired when an audio device is removed from the system.
        /// </summary>
        public event EventHandler<SdlEventArgs>? Removed;

        /// <summary>
        ///  Pauses the audio device.
        /// </summary>
        public void Pause() =>
            Native.SDL_PauseAudioDevice(Index, true);

        /// <summary>
        /// Unpauses the audio device.
        /// </summary>
        public void Unpause() =>
            Native.SDL_PauseAudioDevice(Index, false);

        /// <summary>
        /// Queues audio for playback.
        /// </summary>
        /// <param name="audio">The audio data.</param>
        public void QueueAudio(Span<byte> audio)
        {
            fixed (byte* audioPointer = audio)
            {
                _ = Native.CheckError(Native.SDL_QueueAudio(Index, audioPointer, (uint)audio.Length));
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
                return Native.SDL_DequeueAudio(Index, audioPointer, (uint)audio.Length);
            }
        }

        /// <summary>
        /// Clears all queued audio from the device.
        /// </summary>
        public void ClearQueuedAudio() =>
            Native.SDL_ClearQueuedAudio(Index);

        /// <summary>
        /// Locks the device for multi-threaded access.
        /// </summary>
        public void Lock() =>
            Native.SDL_LockAudioDevice(Index);

        /// <summary>
        /// Unlocks the device.
        /// </summary>
        public void Unlock() =>
            Native.SDL_UnlockAudioDevice(Index);

        /// <summary>
        /// Closes the audio device.
        /// </summary>
        public void Dispose()
        {
            _callbacks = null;
            Native.SDL_CloseAudioDevice(Index);
        }

        internal static void DispatchEvent(Native.SDL_Event e)
        {
            switch (e.Type)
            {
                case Native.SDL_EventType.AudioDeviceAdded:
                    {
                        Added?.Invoke(null, new AudioDeviceAddedEventArgs(e.Adevice));
                        break;
                    }

                case Native.SDL_EventType.JoystickDeviceRemoved:
                    {
                        var device = IndexToInstance(new Native.SDL_AudioDeviceID(e.Adevice.Which));
                        device.Removed?.Invoke(device, new SdlEventArgs(e.Common));
                        break;
                    }

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
