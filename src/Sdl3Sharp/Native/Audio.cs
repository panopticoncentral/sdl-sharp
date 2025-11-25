using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.IOStream;
using static Sdl3Sharp.Native.Properties;

// We are intentionally exposing the P/Invoke calls so people can do low-level calls if needed
#pragma warning disable CA1401 // P/Invokes should not be visible

namespace Sdl3Sharp.Native;

/// <summary>
/// P/Invoke bindings for SDL_audio.h - Audio functionality for the SDL library.
/// </summary>
public static unsafe partial class Audio
{
    /// <summary>
    /// Mask of bits in an SDL_AudioFormat that contains the format bit size.
    /// </summary>
    public const int SDL_AUDIO_MASK_BITSIZE = 0xFF;

    /// <summary>
    /// Mask of bits in an SDL_AudioFormat that contain the floating point flag.
    /// </summary>
    public const int SDL_AUDIO_MASK_FLOAT = 1 << 8;

    /// <summary>
    /// Mask of bits in an SDL_AudioFormat that contain the bigendian flag.
    /// </summary>
    public const int SDL_AUDIO_MASK_BIG_ENDIAN = 1 << 12;

    /// <summary>
    /// Mask of bits in an SDL_AudioFormat that contain the signed data flag.
    /// </summary>
    public const int SDL_AUDIO_MASK_SIGNED = 1 << 15;

    /// <summary>
    /// Audio format.
    /// </summary>
    public enum SDL_AudioFormat : ushort
    {
        /// <summary>Unspecified audio format.</summary>
        SDL_AUDIO_UNKNOWN = 0x0000,

        /// <summary>Unsigned 8-bit samples.</summary>
        SDL_AUDIO_U8 = 0x0008,

        /// <summary>Signed 8-bit samples.</summary>
        SDL_AUDIO_S8 = 0x8008,

        /// <summary>Signed 16-bit samples (little-endian).</summary>
        SDL_AUDIO_S16LE = 0x8010,

        /// <summary>Signed 16-bit samples (big-endian).</summary>
        SDL_AUDIO_S16BE = 0x9010,

        /// <summary>32-bit integer samples (little-endian).</summary>
        SDL_AUDIO_S32LE = 0x8020,

        /// <summary>32-bit integer samples (big-endian).</summary>
        SDL_AUDIO_S32BE = 0x9020,

        /// <summary>32-bit floating point samples (little-endian).</summary>
        SDL_AUDIO_F32LE = 0x8120,

        /// <summary>32-bit floating point samples (big-endian).</summary>
        SDL_AUDIO_F32BE = 0x9120,

        /// <summary>Signed 16-bit samples in native byte order (little-endian on .NET).</summary>
        SDL_AUDIO_S16 = SDL_AUDIO_S16LE,

        /// <summary>32-bit integer samples in native byte order (little-endian on .NET).</summary>
        SDL_AUDIO_S32 = SDL_AUDIO_S32LE,

        /// <summary>32-bit floating point samples in native byte order (little-endian on .NET).</summary>
        SDL_AUDIO_F32 = SDL_AUDIO_F32LE
    }

    /// <summary>
    /// Retrieve the size, in bits, from an SDL_AudioFormat.
    /// </summary>
    /// <param name="format">An SDL_AudioFormat value.</param>
    /// <returns>Data size in bits.</returns>
    public static int SDL_AUDIO_BITSIZE(SDL_AudioFormat format) => (int)format & SDL_AUDIO_MASK_BITSIZE;

    /// <summary>
    /// Retrieve the size, in bytes, from an SDL_AudioFormat.
    /// </summary>
    /// <param name="format">An SDL_AudioFormat value.</param>
    /// <returns>Data size in bytes.</returns>
    public static int SDL_AUDIO_BYTESIZE(SDL_AudioFormat format) => SDL_AUDIO_BITSIZE(format) / 8;

    /// <summary>
    /// Determine if an SDL_AudioFormat represents floating point data.
    /// </summary>
    /// <param name="format">An SDL_AudioFormat value.</param>
    /// <returns>Non-zero if format is floating point, zero otherwise.</returns>
    public static int SDL_AUDIO_ISFLOAT(SDL_AudioFormat format) => (int)format & SDL_AUDIO_MASK_FLOAT;

    /// <summary>
    /// Determine if an SDL_AudioFormat represents bigendian data.
    /// </summary>
    /// <param name="format">An SDL_AudioFormat value.</param>
    /// <returns>Non-zero if format is bigendian, zero otherwise.</returns>
    public static int SDL_AUDIO_ISBIGENDIAN(SDL_AudioFormat format) => (int)format & SDL_AUDIO_MASK_BIG_ENDIAN;

    /// <summary>
    /// Determine if an SDL_AudioFormat represents littleendian data.
    /// </summary>
    /// <param name="format">An SDL_AudioFormat value.</param>
    /// <returns>Non-zero if format is littleendian, zero otherwise.</returns>
    public static int SDL_AUDIO_ISLITTLEENDIAN(SDL_AudioFormat format) => SDL_AUDIO_ISBIGENDIAN(format) == 0 ? 1 : 0;

    /// <summary>
    /// Determine if an SDL_AudioFormat represents signed data.
    /// </summary>
    /// <param name="format">An SDL_AudioFormat value.</param>
    /// <returns>Non-zero if format is signed, zero otherwise.</returns>
    public static int SDL_AUDIO_ISSIGNED(SDL_AudioFormat format) => (int)format & SDL_AUDIO_MASK_SIGNED;

    /// <summary>
    /// Determine if an SDL_AudioFormat represents integer data.
    /// </summary>
    /// <param name="format">An SDL_AudioFormat value.</param>
    /// <returns>Non-zero if format is integer, zero otherwise.</returns>
    public static int SDL_AUDIO_ISINT(SDL_AudioFormat format) => SDL_AUDIO_ISFLOAT(format) == 0 ? 1 : 0;

    /// <summary>
    /// Determine if an SDL_AudioFormat represents unsigned data.
    /// </summary>
    /// <param name="format">An SDL_AudioFormat value.</param>
    /// <returns>Non-zero if format is unsigned, zero otherwise.</returns>
    public static int SDL_AUDIO_ISUNSIGNED(SDL_AudioFormat format) => SDL_AUDIO_ISSIGNED(format) == 0 ? 1 : 0;

    /// <summary>
    /// SDL Audio Device instance IDs. Zero is used to signify an invalid/null device.
    /// </summary>
    /// <param name="value">The audio device ID value.</param>
    public readonly struct SDL_AudioDeviceID(uint value)
    {
        /// <summary>The underlying audio device ID value.</summary>
        public readonly uint Value = value;

        /// <summary>Implicitly converts an SDL_AudioDeviceID to uint.</summary>
        /// <param name="id">The audio device ID to convert.</param>
        public static implicit operator uint(SDL_AudioDeviceID id) => id.Value;

        /// <summary>Implicitly converts a uint to SDL_AudioDeviceID.</summary>
        /// <param name="value">The value to convert.</param>
        public static implicit operator SDL_AudioDeviceID(uint value) => new(value);
    }

    /// <summary>
    /// A value used to request a default playback audio device.
    /// </summary>
    public static readonly SDL_AudioDeviceID SDL_AUDIO_DEVICE_DEFAULT_PLAYBACK = 0xFFFFFFFFu;

    /// <summary>
    /// A value used to request a default recording audio device.
    /// </summary>
    public static readonly SDL_AudioDeviceID SDL_AUDIO_DEVICE_DEFAULT_RECORDING = 0xFFFFFFFEu;

    /// <summary>
    /// Format specifier for audio data.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_AudioSpec
    {
        /// <summary>Audio data format.</summary>
        public SDL_AudioFormat format;

        /// <summary>Number of channels: 1 mono, 2 stereo, etc.</summary>
        public int channels;

        /// <summary>Sample rate: sample frames per second.</summary>
        public int freq;
    }

    /// <summary>
    /// Calculate the size of each audio frame (in bytes) from an SDL_AudioSpec.
    /// </summary>
    /// <param name="spec">An SDL_AudioSpec to query.</param>
    /// <returns>The number of bytes used per sample frame.</returns>
    public static int SDL_AUDIO_FRAMESIZE(SDL_AudioSpec spec) => SDL_AUDIO_BYTESIZE(spec.format) * spec.channels;

    /// <summary>
    /// The opaque handle that represents an audio stream.
    /// </summary>
    public struct SDL_AudioStream { }

    /// <summary>
    /// Use this function to get the number of built-in audio drivers.
    /// </summary>
    /// <returns>The number of built-in audio drivers.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_GetNumAudioDrivers();

    /// <summary>
    /// Use this function to get the name of a built in audio driver.
    /// </summary>
    /// <param name="index">The index of the audio driver; the value ranges from 0 to SDL_GetNumAudioDrivers() - 1.</param>
    /// <returns>The name of the audio driver at the requested index, or NULL if an invalid index was specified.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(Utf8StringMarshaller))]
    public static partial string? SDL_GetAudioDriver(int index);

    /// <summary>
    /// Get the name of the current audio driver.
    /// </summary>
    /// <returns>The name of the current audio driver or NULL if no driver has been initialized.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(Utf8StringMarshaller))]
    public static partial string? SDL_GetCurrentAudioDriver();

    /// <summary>
    /// Get a list of currently-connected audio playback devices.
    /// </summary>
    /// <param name="count">A pointer filled in with the number of devices returned, may be NULL.</param>
    /// <returns>A 0 terminated array of device instance IDs or NULL on error. This should be freed with SDL_free() when it is no longer needed.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_AudioDeviceID* SDL_GetAudioPlaybackDevices(int* count);

    /// <summary>
    /// Get a list of currently-connected audio recording devices.
    /// </summary>
    /// <param name="count">A pointer filled in with the number of devices returned, may be NULL.</param>
    /// <returns>A 0 terminated array of device instance IDs or NULL on error. This should be freed with SDL_free() when it is no longer needed.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_AudioDeviceID* SDL_GetAudioRecordingDevices(int* count);

    /// <summary>
    /// Get the human-readable name of a specific audio device.
    /// </summary>
    /// <param name="devid">The instance ID of the device to query.</param>
    /// <returns>The name of the audio device, or NULL on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(Utf8StringMarshaller))]
    public static partial string? SDL_GetAudioDeviceName(SDL_AudioDeviceID devid);

    /// <summary>
    /// Get the current audio format of a specific audio device.
    /// </summary>
    /// <param name="devid">The instance ID of the device to query.</param>
    /// <param name="spec">On return, will be filled with device details.</param>
    /// <param name="sample_frames">Pointer to store device buffer size, in sample frames. Can be NULL.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetAudioDeviceFormat(SDL_AudioDeviceID devid, SDL_AudioSpec* spec, int* sample_frames);

    /// <summary>
    /// Get the current channel map of an audio device.
    /// </summary>
    /// <param name="devid">The instance ID of the device to query.</param>
    /// <param name="count">On output, set to number of channels in the map. Can be NULL.</param>
    /// <returns>An array of the current channel mapping, or NULL if default. This should be freed with SDL_free() when it is no longer needed.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int* SDL_GetAudioDeviceChannelMap(SDL_AudioDeviceID devid, int* count);

    /// <summary>
    /// Open a specific audio device.
    /// </summary>
    /// <param name="devid">The device instance id to open, or SDL_AUDIO_DEVICE_DEFAULT_PLAYBACK or SDL_AUDIO_DEVICE_DEFAULT_RECORDING for the most reasonable default device.</param>
    /// <param name="spec">The requested device configuration. Can be NULL to use reasonable defaults.</param>
    /// <returns>The device ID on success or 0 on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_AudioDeviceID SDL_OpenAudioDevice(SDL_AudioDeviceID devid, SDL_AudioSpec* spec);

    /// <summary>
    /// Determine if an audio device is physical (instead of logical).
    /// </summary>
    /// <param name="devid">The device ID to query.</param>
    /// <returns>True if devid is a physical device, false if it is logical.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_IsAudioDevicePhysical(SDL_AudioDeviceID devid);

    /// <summary>
    /// Determine if an audio device is a playback device (instead of recording).
    /// </summary>
    /// <param name="devid">The device ID to query.</param>
    /// <returns>True if devid is a playback device, false if it is recording.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_IsAudioDevicePlayback(SDL_AudioDeviceID devid);

    /// <summary>
    /// Use this function to pause audio playback on a specified device.
    /// </summary>
    /// <param name="devid">A device opened by SDL_OpenAudioDevice().</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_PauseAudioDevice(SDL_AudioDeviceID devid);

    /// <summary>
    /// Use this function to unpause audio playback on a specified device.
    /// </summary>
    /// <param name="devid">A device opened by SDL_OpenAudioDevice().</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_ResumeAudioDevice(SDL_AudioDeviceID devid);

    /// <summary>
    /// Use this function to query if an audio device is paused.
    /// </summary>
    /// <param name="devid">A device opened by SDL_OpenAudioDevice().</param>
    /// <returns>True if device is valid and paused, false otherwise.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_AudioDevicePaused(SDL_AudioDeviceID devid);

    /// <summary>
    /// Get the gain of an audio device.
    /// </summary>
    /// <param name="devid">The audio device to query.</param>
    /// <returns>The gain of the device or -1.0f on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float SDL_GetAudioDeviceGain(SDL_AudioDeviceID devid);

    /// <summary>
    /// Change the gain of an audio device.
    /// </summary>
    /// <param name="devid">The audio device on which to change gain.</param>
    /// <param name="gain">The gain. 1.0f is no change, 0.0f is silence.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetAudioDeviceGain(SDL_AudioDeviceID devid, float gain);

    /// <summary>
    /// Close a previously-opened audio device.
    /// </summary>
    /// <param name="devid">An audio device id previously returned by SDL_OpenAudioDevice().</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_CloseAudioDevice(SDL_AudioDeviceID devid);

    /// <summary>
    /// Bind a list of audio streams to an audio device.
    /// </summary>
    /// <param name="devid">An audio device to bind a stream to.</param>
    /// <param name="streams">An array of audio streams to bind.</param>
    /// <param name="num_streams">Number streams listed in the streams array.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_BindAudioStreams(SDL_AudioDeviceID devid, SDL_AudioStream** streams, int num_streams);

    /// <summary>
    /// Bind a single audio stream to an audio device.
    /// </summary>
    /// <param name="devid">An audio device to bind a stream to.</param>
    /// <param name="stream">An audio stream to bind to a device.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_BindAudioStream(SDL_AudioDeviceID devid, SDL_AudioStream* stream);

    /// <summary>
    /// Unbind a list of audio streams from their audio devices.
    /// </summary>
    /// <param name="streams">An array of audio streams to unbind. Can be NULL or contain NULL.</param>
    /// <param name="num_streams">Number streams listed in the streams array.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_UnbindAudioStreams(SDL_AudioStream** streams, int num_streams);

    /// <summary>
    /// Unbind a single audio stream from its audio device.
    /// </summary>
    /// <param name="stream">An audio stream to unbind from a device. Can be NULL.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_UnbindAudioStream(SDL_AudioStream* stream);

    /// <summary>
    /// Query an audio stream for its currently-bound device.
    /// </summary>
    /// <param name="stream">The audio stream to query.</param>
    /// <returns>The bound audio device, or 0 if not bound or invalid.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_AudioDeviceID SDL_GetAudioStreamDevice(SDL_AudioStream* stream);

    /// <summary>
    /// Create a new audio stream.
    /// </summary>
    /// <param name="src_spec">The format details of the input audio.</param>
    /// <param name="dst_spec">The format details of the output audio.</param>
    /// <returns>A new audio stream on success or NULL on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_AudioStream* SDL_CreateAudioStream(SDL_AudioSpec* src_spec, SDL_AudioSpec* dst_spec);

    /// <summary>
    /// Get the properties associated with an audio stream.
    /// </summary>
    /// <param name="stream">The SDL_AudioStream to query.</param>
    /// <returns>A valid property ID on success or 0 on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_PropertiesID SDL_GetAudioStreamProperties(SDL_AudioStream* stream);

    /// <summary>
    /// Query the current format of an audio stream.
    /// </summary>
    /// <param name="stream">The SDL_AudioStream to query.</param>
    /// <param name="src_spec">Where to store the input audio format; ignored if NULL.</param>
    /// <param name="dst_spec">Where to store the output audio format; ignored if NULL.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetAudioStreamFormat(SDL_AudioStream* stream, SDL_AudioSpec* src_spec, SDL_AudioSpec* dst_spec);

    /// <summary>
    /// Change the input and output formats of an audio stream.
    /// </summary>
    /// <param name="stream">The stream the format is being changed.</param>
    /// <param name="src_spec">The new format of the audio input; if NULL, it is not changed.</param>
    /// <param name="dst_spec">The new format of the audio output; if NULL, it is not changed.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetAudioStreamFormat(SDL_AudioStream* stream, SDL_AudioSpec* src_spec, SDL_AudioSpec* dst_spec);

    /// <summary>
    /// Get the frequency ratio of an audio stream.
    /// </summary>
    /// <param name="stream">The SDL_AudioStream to query.</param>
    /// <returns>The frequency ratio of the stream or 0.0 on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float SDL_GetAudioStreamFrequencyRatio(SDL_AudioStream* stream);

    /// <summary>
    /// Change the frequency ratio of an audio stream.
    /// </summary>
    /// <param name="stream">The stream the frequency ratio is being changed.</param>
    /// <param name="ratio">The frequency ratio. 1.0 is normal speed. Must be between 0.01 and 100.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetAudioStreamFrequencyRatio(SDL_AudioStream* stream, float ratio);

    /// <summary>
    /// Get the gain of an audio stream.
    /// </summary>
    /// <param name="stream">The SDL_AudioStream to query.</param>
    /// <returns>The gain of the stream or -1.0f on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float SDL_GetAudioStreamGain(SDL_AudioStream* stream);

    /// <summary>
    /// Change the gain of an audio stream.
    /// </summary>
    /// <param name="stream">The stream on which the gain is being changed.</param>
    /// <param name="gain">The gain. 1.0f is no change, 0.0f is silence.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetAudioStreamGain(SDL_AudioStream* stream, float gain);

    /// <summary>
    /// Get the current input channel map of an audio stream.
    /// </summary>
    /// <param name="stream">The SDL_AudioStream to query.</param>
    /// <param name="count">On output, set to number of channels in the map. Can be NULL.</param>
    /// <returns>An array of the current channel mapping, or NULL if default. This should be freed with SDL_free() when it is no longer needed.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int* SDL_GetAudioStreamInputChannelMap(SDL_AudioStream* stream, int* count);

    /// <summary>
    /// Get the current output channel map of an audio stream.
    /// </summary>
    /// <param name="stream">The SDL_AudioStream to query.</param>
    /// <param name="count">On output, set to number of channels in the map. Can be NULL.</param>
    /// <returns>An array of the current channel mapping, or NULL if default. This should be freed with SDL_free() when it is no longer needed.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int* SDL_GetAudioStreamOutputChannelMap(SDL_AudioStream* stream, int* count);

    /// <summary>
    /// Set the current input channel map of an audio stream.
    /// </summary>
    /// <param name="stream">The SDL_AudioStream to change.</param>
    /// <param name="chmap">The new channel map, NULL to reset to default.</param>
    /// <param name="count">The number of channels in the map.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetAudioStreamInputChannelMap(SDL_AudioStream* stream, int* chmap, int count);

    /// <summary>
    /// Set the current output channel map of an audio stream.
    /// </summary>
    /// <param name="stream">The SDL_AudioStream to change.</param>
    /// <param name="chmap">The new channel map, NULL to reset to default.</param>
    /// <param name="count">The number of channels in the map.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetAudioStreamOutputChannelMap(SDL_AudioStream* stream, int* chmap, int count);

    /// <summary>
    /// Add data to the stream.
    /// </summary>
    /// <param name="stream">The stream the audio data is being added to.</param>
    /// <param name="buf">A pointer to the audio data to add.</param>
    /// <param name="len">The number of bytes to write to the stream.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_PutAudioStreamData(SDL_AudioStream* stream, void* buf, int len);

    /// <summary>
    /// Get converted/resampled data from the stream.
    /// </summary>
    /// <param name="stream">The stream the audio is being requested from.</param>
    /// <param name="buf">A buffer to fill with audio data.</param>
    /// <param name="len">The maximum number of bytes to fill.</param>
    /// <returns>The number of bytes read from the stream or -1 on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_GetAudioStreamData(SDL_AudioStream* stream, void* buf, int len);

    /// <summary>
    /// Get the number of converted/resampled bytes available.
    /// </summary>
    /// <param name="stream">The audio stream to query.</param>
    /// <returns>The number of converted/resampled bytes available or -1 on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_GetAudioStreamAvailable(SDL_AudioStream* stream);

    /// <summary>
    /// Get the number of bytes currently queued.
    /// </summary>
    /// <param name="stream">The audio stream to query.</param>
    /// <returns>The number of bytes queued or -1 on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_GetAudioStreamQueued(SDL_AudioStream* stream);

    /// <summary>
    /// Tell the stream that you're done sending data, and anything being buffered should be converted/resampled and made available immediately.
    /// </summary>
    /// <param name="stream">The audio stream to flush.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_FlushAudioStream(SDL_AudioStream* stream);

    /// <summary>
    /// Clear any pending data in the stream.
    /// </summary>
    /// <param name="stream">The audio stream to clear.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_ClearAudioStream(SDL_AudioStream* stream);

    /// <summary>
    /// Use this function to pause audio playback on the audio device associated with an audio stream.
    /// </summary>
    /// <param name="stream">The audio stream associated with the audio device to pause.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_PauseAudioStreamDevice(SDL_AudioStream* stream);

    /// <summary>
    /// Use this function to unpause audio playback on the audio device associated with an audio stream.
    /// </summary>
    /// <param name="stream">The audio stream associated with the audio device to resume.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_ResumeAudioStreamDevice(SDL_AudioStream* stream);

    /// <summary>
    /// Use this function to query if an audio device associated with a stream is paused.
    /// </summary>
    /// <param name="stream">The audio stream associated with the audio device to query.</param>
    /// <returns>True if device is valid and paused, false otherwise.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_AudioStreamDevicePaused(SDL_AudioStream* stream);

    /// <summary>
    /// Lock an audio stream for serialized access.
    /// </summary>
    /// <param name="stream">The audio stream to lock.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_LockAudioStream(SDL_AudioStream* stream);

    /// <summary>
    /// Unlock an audio stream for serialized access.
    /// </summary>
    /// <param name="stream">The audio stream to unlock.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_UnlockAudioStream(SDL_AudioStream* stream);

    /// <summary>
    /// Set a callback that runs when data is requested from an audio stream.
    /// </summary>
    /// <param name="stream">The audio stream to set the new callback on.</param>
    /// <param name="callback">The new callback function to call when data is requested from the stream.</param>
    /// <param name="userdata">An opaque pointer provided to the callback for its own personal use.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetAudioStreamGetCallback(SDL_AudioStream* stream, delegate* unmanaged[Cdecl]<nuint, SDL_AudioStream*, int, int, void> callback, nuint userdata);

    /// <summary>
    /// Set a callback that runs when data is added to an audio stream.
    /// </summary>
    /// <param name="stream">The audio stream to set the new callback on.</param>
    /// <param name="callback">The new callback function to call when data is added to the stream.</param>
    /// <param name="userdata">An opaque pointer provided to the callback for its own personal use.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetAudioStreamPutCallback(SDL_AudioStream* stream, delegate* unmanaged[Cdecl]<nuint, SDL_AudioStream*, int, int, void> callback, nuint userdata);

    /// <summary>
    /// Free an audio stream.
    /// </summary>
    /// <param name="stream">The audio stream to destroy.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_DestroyAudioStream(SDL_AudioStream* stream);

    /// <summary>
    /// Convenience function for straightforward audio init for the common case.
    /// </summary>
    /// <param name="devid">An audio device to open, or SDL_AUDIO_DEVICE_DEFAULT_PLAYBACK or SDL_AUDIO_DEVICE_DEFAULT_RECORDING.</param>
    /// <param name="spec">The audio stream's data format. Can be NULL.</param>
    /// <param name="callback">A callback where the app will provide new data for playback, or receive new data for recording. Can be NULL.</param>
    /// <param name="userdata">App-controlled pointer passed to callback. Can be NULL.</param>
    /// <returns>An audio stream on success, ready to use, or NULL on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_AudioStream* SDL_OpenAudioDeviceStream(SDL_AudioDeviceID devid, SDL_AudioSpec* spec, delegate* unmanaged[Cdecl]<nuint, SDL_AudioStream*, int, int, void> callback, nuint userdata);

    /// <summary>
    /// Set a callback that fires when data is about to be fed to an audio device.
    /// </summary>
    /// <param name="devid">The ID of an opened audio device.</param>
    /// <param name="callback">A callback function to be called. Can be NULL.</param>
    /// <param name="userdata">App-controlled pointer passed to callback. Can be NULL.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetAudioPostmixCallback(SDL_AudioDeviceID devid, delegate* unmanaged[Cdecl]<nuint, SDL_AudioSpec*, float*, int, void> callback, nuint userdata);

    /// <summary>
    /// Load the audio data of a WAVE file into memory.
    /// </summary>
    /// <param name="src">The data source for the WAVE data.</param>
    /// <param name="closeio">If true, calls SDL_CloseIO() on src before returning, even in the case of an error.</param>
    /// <param name="spec">A pointer to an SDL_AudioSpec that will be set to the WAVE data's format details on successful return.</param>
    /// <param name="audio_buf">A pointer filled with the audio data, allocated by the function.</param>
    /// <param name="audio_len">A pointer filled with the length of the audio data buffer in bytes.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_LoadWAV_IO(SDL_IOStream* src, [MarshalAs(UnmanagedType.U1)] bool closeio, SDL_AudioSpec* spec, byte** audio_buf, uint* audio_len);

    /// <summary>
    /// Loads a WAV from a file path.
    /// </summary>
    /// <param name="path">The file path of the WAV file to open.</param>
    /// <param name="spec">A pointer to an SDL_AudioSpec that will be set to the WAVE data's format details on successful return.</param>
    /// <param name="audio_buf">A pointer filled with the audio data, allocated by the function.</param>
    /// <param name="audio_len">A pointer filled with the length of the audio data buffer in bytes.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_LoadWAV([MarshalUsing(typeof(Utf8StringMarshaller))] string path, SDL_AudioSpec* spec, byte** audio_buf, uint* audio_len);

    /// <summary>
    /// Mix audio data in a specified format.
    /// </summary>
    /// <param name="dst">The destination for the mixed audio.</param>
    /// <param name="src">The source audio buffer to be mixed.</param>
    /// <param name="format">The SDL_AudioFormat structure representing the desired audio format.</param>
    /// <param name="len">The length of the audio buffer in bytes.</param>
    /// <param name="volume">Ranges from 0.0 - 1.0, and should be set to 1.0 for full audio volume.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_MixAudio(byte* dst, byte* src, SDL_AudioFormat format, uint len, float volume);

    /// <summary>
    /// Convert some audio data of one format to another format.
    /// </summary>
    /// <param name="src_spec">The format details of the input audio.</param>
    /// <param name="src_data">The audio data to be converted.</param>
    /// <param name="src_len">The len of src_data.</param>
    /// <param name="dst_spec">The format details of the output audio.</param>
    /// <param name="dst_data">Will be filled with a pointer to converted audio data, which should be freed with SDL_free(). On error, it will be NULL.</param>
    /// <param name="dst_len">Will be filled with the len of dst_data.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_ConvertAudioSamples(SDL_AudioSpec* src_spec, byte* src_data, int src_len, SDL_AudioSpec* dst_spec, byte** dst_data, int* dst_len);

    /// <summary>
    /// Get the human readable name of an audio format.
    /// </summary>
    /// <param name="format">The audio format to query.</param>
    /// <returns>The human readable name of the specified audio format or "SDL_AUDIO_UNKNOWN" if the format isn't recognized.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(Utf8StringMarshaller))]
    public static partial string SDL_GetAudioFormatName(SDL_AudioFormat format);

    /// <summary>
    /// Get the appropriate memset value for silencing an audio format.
    /// </summary>
    /// <param name="format">The audio data format to query.</param>
    /// <returns>A byte value that can be passed to memset.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_GetSilenceValueForFormat(SDL_AudioFormat format);
}