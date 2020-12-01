using System;
using System.Collections.Generic;

namespace SdlSharp.Sound
{
    /// <summary>
    /// Audio processing routines.
    /// </summary>
    public static unsafe class Audio
    {
        private static ItemCollection<string>? s_drivers;
        private static ItemCollection<string>? s_captureDevices;
        private static ItemCollection<string>? s_nonCaptureDevices;

        /// <summary>
        /// The maximum volume that can be used when mixing audio.
        /// </summary>
        public const int MixMaxVolume = 128;

        /// <summary>
        /// The audio drivers on the system.
        /// </summary>
        public static IReadOnlyList<string> Drivers => s_drivers ??= new ItemCollection<string>(Native.SDL_GetAudioDriver, Native.SDL_GetNumAudioDrivers);

        /// <summary>
        /// The capture devices supported by the current driver.
        /// </summary>
        public static IReadOnlyList<string> CaptureDevices => s_captureDevices ??= new ItemCollection<string>(
            index => Native.CheckNotNull(Native.SDL_GetAudioDeviceName(index, true).ToString()),
            () => Native.SDL_GetNumAudioDevices(true));

        /// <summary>
        /// The non-capture devices supported by the current drive.
        /// </summary>
        public static IReadOnlyList<string> NonCaptureDevices => s_nonCaptureDevices ??= new ItemCollection<string>(
            index => Native.CheckNotNull(Native.SDL_GetAudioDeviceName(index, false).ToString()),
            () => Native.SDL_GetNumAudioDevices(false));

        /// <summary>
        /// The current audio driver in use.
        /// </summary>
        public static string CurrentDriver =>
            Native.SDL_GetCurrentAudioDriver();

        /// <summary>
        /// Opens an audio device.
        /// </summary>
        /// <param name="desired">The desired specifications for the device.</param>
        /// <param name="obtained">The actual specifications for the device.</param>
        /// <returns>The audio device that was opened.</returns>
        public static AudioDevice Open(in AudioSpec desired, out AudioSpec obtained)
        {
            var desiredNativeSpec = desired.ToNative();
            var audioDeviceId = Native.SDL_OpenAudio(in desiredNativeSpec, out var obtainedNativeSpec);

            if (audioDeviceId.Id == 0)
            {
                throw new SdlException();
            }

            var audioDevice = AudioDevice.IndexToInstance(audioDeviceId);
            obtained = new AudioSpec(obtainedNativeSpec);

            // We have to save the callback to prevent collection
            audioDevice.AddAudioCallback(desiredNativeSpec.Callback);
            return audioDevice;
        }

        /// <summary>
        /// Opens an audio device.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="isCapture">Whether the device is a capture device.</param>
        /// <param name="desired">The desired specifications for the device.</param>
        /// <param name="obtained">The actual specifications for the device.</param>
        /// <param name="allowedChanges">What changes can be made between the desired and actual specifications.</param>
        /// <returns>The audio device that was opened.</returns>
        public static AudioDevice Open(string? device, bool isCapture, in AudioSpec desired, out AudioSpec obtained, AudioAllowChange allowedChanges)
        {
            using var utf8Device = Utf8String.ToUtf8String(device);
            var desiredNativeSpec = desired.ToNative();
            var audioDeviceId = Native.SDL_OpenAudioDevice(utf8Device, isCapture, in desiredNativeSpec, out var obtainedNativeSpec, allowedChanges);

            if (audioDeviceId.Id == 0)
            {
                throw new SdlException();
            }

            var audioDevice = AudioDevice.IndexToInstance(audioDeviceId);
            obtained = new AudioSpec(obtainedNativeSpec);

            // We have to save the callback to prevent collection
            audioDevice.AddAudioCallback(desiredNativeSpec.Callback);
            return audioDevice;
        }

        /// <summary>
        /// Loads a WAV file.
        /// </summary>
        /// <param name="rwops">The source of the data.</param>
        /// <param name="shouldFree">Whether the source should be freed when the audio is loaded.</param>
        /// <param name="spec">The audio specification of the loaded audio.</param>
        /// <returns>The loaded audio data.</returns>
        public static WavData LoadWav(RWOps rwops, bool shouldFree, out AudioSpec spec)
        {
            _ = Native.CheckPointer(Native.SDL_LoadWAV_RW(rwops.Native, shouldFree, out var specNative, out var buffer, out var bufferSize));
            spec = new AudioSpec(specNative);
            return new WavData(buffer, bufferSize);
        }

        /// <summary>
        /// Loads a WAV file.
        /// </summary>
        /// <param name="filename">The filename to load.</param>
        /// <param name="spec">The audio specification of the loaded audio.</param>
        /// <returns>The loaded audio data.</returns>
        public static WavData LoadWav(string filename, out AudioSpec spec)
        {
            _ = Native.CheckPointer(Native.SDL_LoadWAV(filename, out var specNative, out var buffer, out var bufferSize));
            spec = new AudioSpec(specNative);
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

            var buffer = new byte[source.Length * audioConvert.LengthMultiplier];
            source.CopyTo(buffer);

            fixed (byte* bufferPointer = buffer)
            {
                audioConvert.Buffer = bufferPointer;
                audioConvert.Length = source.Length;
                _ = Native.CheckError(Native.SDL_ConvertAudio(ref audioConvert));
            }

            return new Span<byte>(buffer, 0, audioConvert.ConvertedLength);
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
    }
}
