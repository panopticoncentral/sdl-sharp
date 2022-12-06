namespace SdlSharp.Sound
{
    /// <summary>
    /// Audio processing routines.
    /// </summary>
    public static unsafe class Audio
    {
        //private static ItemCollection<string>? s_captureDevices;
        //private static ItemCollection<string>? s_nonCaptureDevices;

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
        /// <param name="frequency">The desired frequency.</param>
        /// <param name="format">The desired format.</param>
        /// <param name="channels">The desired number of channels.</param>
        /// <param name="samples">The desired number of samples.</param>
        /// <param name="callback">A data callback.</param>
        /// <param name="obtained">The actual specifications for the device.</param>
        /// <returns>The audio device that was opened.</returns>
        public static AudioDevice Open(int frequency, AudioFormat format, byte channels, ushort samples, AudioCallback? callback, out AudioSpecification obtained)
        {
            var desiredNativeSpec = new Native.SDL_AudioSpec(
                frequency,
                format.Format,
                channels,
                samples,
                callback != null ? (delegate* unmanaged[Cdecl]<nint, byte*, int, void>)&AudioDevice.AudioCallback : null,
                callback != null ? callback.GetHashCode() : 0);

            Native.SDL_AudioSpec obtainedNativeSpec;
            _ = Native.CheckError(Native.SDL_OpenAudio(&desiredNativeSpec, &obtainedNativeSpec));

            var audioDevice = new AudioDevice(new(1), callback);

            obtained = new AudioSpecification(obtainedNativeSpec);

            return audioDevice;
        }

        /// <summary>
        /// Opens an audio device.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="isCapture">Whether the device is a capture device.</param>
        /// <param name="frequency">The desired frequency.</param>
        /// <param name="format">The desired format.</param>
        /// <param name="channels">The desired number of channels.</param>
        /// <param name="samples">The desired number of samples.</param>
        /// <param name="callback">A data callback.</param>
        /// <param name="obtained">The actual specifications for the device.</param>
        /// <param name="allowedChanges">What changes can be made between the desired and actual specifications.</param>
        /// <returns>The audio device that was opened.</returns>
        public static AudioDevice Open(string? device, bool isCapture, int frequency, AudioFormat format, byte channels, ushort samples, AudioCallback? callback, out AudioSpecification obtained, AudioAllowChange allowedChanges)
        {
            using var utf8Device = Utf8String.ToUtf8String(device);

            var desiredNativeSpec = new Native.SDL_AudioSpec(
                frequency,
                format.Format,
                channels,
                samples,
                callback != null ? (delegate* unmanaged[Cdecl]<nint, byte*, int, void>)&AudioDevice.AudioCallback : null,
                callback != null ? callback.GetHashCode() : 0);

            var audioDeviceId = Native.CheckErrorZero(Native.SDL_OpenAudioDevice(
                utf8Device,
                isCapture,
                in desiredNativeSpec,
                out var obtainedNativeSpec,
                allowedChanges));

            var audioDevice = new AudioDevice(new(audioDeviceId), callback);

            obtained = new AudioSpecification(obtainedNativeSpec);

            return audioDevice;
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
            _ = Native.CheckPointer(Native.SDL_LoadWAV_RW(rwops.Native, shouldFree, out var specNative, out var buffer, out var bufferSize));
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
            _ = Native.CheckPointer(Native.SDL_LoadWAV(filename, out var specNative, out var buffer, out var bufferSize));
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
            if (!Native.CheckErrorBool(Native.SDL_BuildAudioCVT(out var audioConvert, sourceFormat, sourceChannels, sourceRate, destinationFormat, destinationChannels, destinationRate)))
            {
                return source;
            }

            var buffer = new byte[source.Length * audioConvert.len_mult];
            source.CopyTo(buffer);

            fixed (byte* bufferPointer = buffer)
            {
                audioConvert.buf = bufferPointer;
                audioConvert.len = source.Length;
                _ = Native.CheckError(Native.SDL_ConvertAudio(ref audioConvert));
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
                    Native.SDL_MixAudioFormat(destinationPointer, sourcePointer, format, (uint)source.Length, volume);
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
            using var utf8Name = new Utf8String(nameBuffer);
            return (utf8Name.ToString()!, new AudioSpecification(spec));
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

                case Native.SDL_EventType.AudioDeviceRemoved:
                    {
                        Removed?.Invoke(null, new AudioDeviceRemovedEventArgs(e.Adevice));
                        break;
                    }

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
