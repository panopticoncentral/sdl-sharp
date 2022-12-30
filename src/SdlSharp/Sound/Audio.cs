namespace SdlSharp.Sound
{
    /// <summary>
    /// Audio processing routines.
    /// </summary>
    public static unsafe class Audio
    {
        /// <summary>
        /// The maximum volume that can be used when mixing audio.
        /// </summary>
        public const int MixMaxVolume = 128;

        /// <summary>
        /// The audio drivers on the system.
        /// </summary>
        public static IReadOnlyList<string> Drivers => Native.GetIndexedCollection(i => Native.Utf8ToString(Native.SDL_GetAudioDriver(i))!, Native.SDL_GetNumAudioDrivers);

        /// <summary>
        /// The capture devices supported by the current driver.
        /// </summary>
        public static IReadOnlyList<(string name, AudioSpecification spec)> CaptureDevices => GetDevices(true);

        /// <summary>
        /// The default capture device for the current driver.
        /// </summary>
        public static (string name, AudioSpecification spec) DefaultCaptureDevice => GetDefaultDevice(true);

        /// <summary>
        /// The non-capture devices supported by the current drive.
        /// </summary>
        public static IReadOnlyList<(string name, AudioSpecification spec)> NonCaptureDevices => GetDevices(false);

        /// <summary>
        /// The default non-capture device for the current driver.
        /// </summary>
        public static (string name, AudioSpecification spec) DefaultNonCaptureDevice => GetDefaultDevice(false);

        /// <summary>
        /// The current audio driver in use.
        /// </summary>
        public static string CurrentDriver => Native.Utf8ToString(Native.SDL_GetCurrentAudioDriver())!;

        /// <summary>
        /// Event fired when an audio device is added to the system.
        /// </summary>
        public static event EventHandler<AudioDeviceAddedEventArgs>? Added;

        /// <summary>
        /// Event fired when an audio device is removed from the system.
        /// </summary>
        public static event EventHandler<AudioDeviceRemovedEventArgs>? Removed;

        /// <summary>
        /// Opens an audio device.
        /// </summary>
        /// <param name="desired">The desired audio specification.</param>
        /// <param name="source">An audio source.</param>
        /// <param name="obtained">The actual specifications for the device.</param>
        /// <returns>The audio device that was opened.</returns>
        public static AudioDevice Open(AudioSpecification desired, AudioSource? source, out AudioSpecification obtained) =>
            Open(null, false, desired, source, out obtained, AudioAllowChange.Any);

        /// <summary>
        /// Opens an audio device.
        /// </summary>
        /// <param name="desired">The desired audio specification.</param>
        /// <param name="obtained">The actual specifications for the device.</param>
        /// <returns>The audio device that was opened.</returns>
        public static AudioDevice Open(AudioSpecification desired, out AudioSpecification obtained) =>
            Open(null, false, desired, null, out obtained, AudioAllowChange.Any);

        /// <summary>
        /// Opens an audio device.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="isCapture">Whether the device is a capture device.</param>
        /// <param name="desired">The desired audio specification.</param>
        /// <param name="obtained">The actual specifications for the device.</param>
        /// <param name="allowedChanges">What changes can be made between the desired and actual specifications.</param>
        /// <returns>The audio device that was opened.</returns>
        public static AudioDevice Open(string? device, bool isCapture, AudioSpecification desired, out AudioSpecification obtained, AudioAllowChange allowedChanges) =>
            Open(device, isCapture, desired, null, out obtained, allowedChanges);

        /// <summary>
        /// Opens an audio device.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="isCapture">Whether the device is a capture device.</param>
        /// <param name="desired">The desired audio specification.</param>
        /// <param name="source">An audio source.</param>
        /// <param name="obtained">The actual specifications for the device.</param>
        /// <param name="allowedChanges">What changes can be made between the desired and actual specifications.</param>
        /// <returns>The audio device that was opened.</returns>
        public static AudioDevice Open(string? device, bool isCapture, AudioSpecification desired, AudioSource? source, out AudioSpecification obtained, AudioAllowChange allowedChanges)
        {
            var desiredNativeSpec = new Native.SDL_AudioSpec(
                desired.Frequency,
                desired.Format.Format,
                desired.Channels,
                desired.Samples,
                source != null ? (delegate* unmanaged[Cdecl]<nint, byte*, int, void>)&AudioSource.AudioCallback : null,
                source != null ? source.GetHashCode() : 0);

            Native.SDL_AudioSpec obtainedNativeSpec;

            fixed (byte* utf8Device = Native.StringToUtf8(device))
            {
                var audioDeviceId = Native.CheckError(Native.SDL_OpenAudioDevice(
                    utf8Device,
                    Native.BoolToInt(isCapture),
                    &desiredNativeSpec,
                    &obtainedNativeSpec,
                    (int)allowedChanges),
                    d => d.Id != 0);

                var audioDevice = new AudioDevice(audioDeviceId);
                obtained = new AudioSpecification(obtainedNativeSpec);

                return audioDevice;
            }
        }

        /// <summary>
        /// Loads a WAV file.
        /// </summary>
        /// <param name="rwops">The source of the data.</param>
        /// <param name="shouldFree">Whether the source should be freed when the audio is loaded.</param>
        /// <param name="spec">The audio specification of the loaded audio.</param>
        /// <returns>The loaded audio data.</returns>
        public static WavData LoadWav(RWOps rwops, bool shouldFree, out AudioSpecification spec)
        {
            Native.SDL_AudioSpec specNative;
            byte* buffer;
            uint bufferSize;

            _ = Native.CheckPointer(Native.SDL_LoadWAV_RW(rwops.ToNative(), Native.BoolToInt(shouldFree), &specNative, &buffer, &bufferSize));
            spec = new AudioSpecification(specNative);
            return new WavData(buffer, bufferSize);
        }

        /// <summary>
        /// Loads a WAV file.
        /// </summary>
        /// <param name="filename">The filename to load.</param>
        /// <param name="spec">The audio specification of the loaded audio.</param>
        /// <returns>The loaded audio data.</returns>
        public static WavData LoadWav(string filename, out AudioSpecification spec)
        {
            Native.SDL_AudioSpec specNative;
            byte* buffer;
            uint bufferSize;

            fixed (byte* ptr = Native.StringToUtf8(filename))
            {
                _ = Native.CheckPointer(Native.SDL_LoadWAV(ptr, &specNative, &buffer, &bufferSize));
            }
            spec = new AudioSpecification(specNative);
            return new WavData(buffer, bufferSize);
        }

        /// <summary>
        /// Converts audio from one format to another.
        /// </summary>
        /// <param name="source">The source audio data.</param>
        /// <param name="sourceFormat">The source audio format.</param>
        /// <param name="sourceChannels">The source audio channel count.</param>
        /// <param name="sourceRate">The source audio bitrate.</param>
        /// <param name="destinationFormat">The destination audio format.</param>
        /// <param name="destinationChannels">The destination audio channel count.</param>
        /// <param name="destinationRate">The destination audio bitrate.</param>
        /// <returns>The converted audio.</returns>
        public static Span<byte> Convert(Span<byte> source, AudioFormat sourceFormat, byte sourceChannels, int sourceRate, AudioFormat destinationFormat, byte destinationChannels, int destinationRate)
        {
            Native.SDL_AudioCVT audioConvert;
            if (!Native.CheckErrorBool(Native.SDL_BuildAudioCVT(&audioConvert, sourceFormat.Format, sourceChannels, sourceRate, destinationFormat.Format, destinationChannels, destinationRate)))
            {
                return source;
            }

            var buffer = new byte[source.Length * audioConvert.len_mult];
            source.CopyTo(buffer);

            fixed (byte* bufferPointer = buffer)
            {
                audioConvert.buf = bufferPointer;
                audioConvert.len = source.Length;
                _ = Native.CheckError(Native.SDL_ConvertAudio(&audioConvert));
            }

            return new Span<byte>(buffer, 0, audioConvert.len_cvt);
        }

        /// <summary>
        /// Mixes audio data.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="volume">The volume.</param>
        public static void Mix(Span<byte> source, Span<byte> destination, int volume)
        {
            if (destination.Length < source.Length)
            {
                throw new ArgumentException("Destination is too short.", nameof(destination));
            }

            fixed (byte* sourcePointer = source)
            {
                fixed (byte* destinationPointer = destination)
                {
                    Native.SDL_MixAudio(destinationPointer, sourcePointer, (uint)source.Length, volume);
                }
            }
        }

        /// <summary>
        /// Mixes audio data.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="format">The audio format.</param>
        /// <param name="volume">The volume.</param>
        public static void Mix(Span<byte> source, Span<byte> destination, AudioFormat format, int volume)
        {
            if (destination.Length < source.Length)
            {
                throw new ArgumentException("Destination is too short.", nameof(destination));
            }

            fixed (byte* sourcePointer = source)
            {
                fixed (byte* destinationPointer = destination)
                {
                    Native.SDL_MixAudioFormat(destinationPointer, sourcePointer, format.Format, (uint)source.Length, volume);
                }
            }
        }

        private static IReadOnlyList<(string name, AudioSpecification spec)> GetDevices(bool isCapture) => Native.GetIndexedCollection(i =>
        {
            var name = Native.Utf8ToString(Native.SDL_GetAudioDeviceName(i, Native.BoolToInt(isCapture)))!;
            Native.SDL_AudioSpec spec;
            _ = Native.CheckError(Native.SDL_GetAudioDeviceSpec(i, Native.BoolToInt(isCapture), &spec));
            return (name, new AudioSpecification(spec));
        }, () => Native.SDL_GetNumAudioDevices(Native.BoolToInt(isCapture)));

        private static (string name, AudioSpecification spec) GetDefaultDevice(bool isCapture)
        {
            byte* nameBuffer = null;
            Native.SDL_AudioSpec spec;

            _ = Native.CheckError(Native.SDL_GetDefaultAudioInfo(&nameBuffer, &spec, Native.BoolToInt(isCapture)));
            var name = Native.Utf8ToStringAndFree(nameBuffer);
            return (name!, new AudioSpecification(spec));
        }

        internal static void DispatchEvent(Native.SDL_Event e)
        {
            switch ((Native.SDL_EventType)e.type)
            {
                case Native.SDL_EventType.SDL_AUDIODEVICEADDED:
                    {
                        Added?.Invoke(null, new AudioDeviceAddedEventArgs(e.adevice));
                        break;
                    }

                case Native.SDL_EventType.SDL_AUDIODEVICEREMOVED:
                    {
                        Removed?.Invoke(null, new AudioDeviceRemovedEventArgs(e.adevice));
                        break;
                    }

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
