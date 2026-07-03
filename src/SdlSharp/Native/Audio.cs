using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace SdlSharp.Native;

// Deferred: SDL_LoadWAV_IO (needs SDL_IOStream — see skip comment below),
// SDL_PutAudioStreamDataNoCopy (advanced zero-copy — see skip comment below).

/// <summary>
/// Audio data format values.
/// </summary>
public enum SDL_AudioFormat : ushort
{
    /// <summary>Unspecified audio format.</summary>
    SDL_AUDIO_UNKNOWN = 0x0000,
    /// <summary>Unsigned 8-bit samples.</summary>
    SDL_AUDIO_U8 = 0x0008,
    /// <summary>Signed 8-bit samples.</summary>
    SDL_AUDIO_S8 = 0x8008,
    /// <summary>Signed 16-bit samples, little-endian.</summary>
    SDL_AUDIO_S16LE = 0x8010,
    /// <summary>Signed 16-bit samples, big-endian.</summary>
    SDL_AUDIO_S16BE = 0x9010,
    /// <summary>Signed 32-bit integer samples, little-endian.</summary>
    SDL_AUDIO_S32LE = 0x8020,
    /// <summary>Signed 32-bit integer samples, big-endian.</summary>
    SDL_AUDIO_S32BE = 0x9020,
    /// <summary>32-bit floating point samples, little-endian.</summary>
    SDL_AUDIO_F32LE = 0x8120,
    /// <summary>32-bit floating point samples, big-endian.</summary>
    SDL_AUDIO_F32BE = 0x9120,

    // Native byte-order aliases. SDL's header resolves these to the LE or BE
    // variant by compile-time byte order; every platform the redist ships for
    // (win-x64/arm64, osx, linux-x64/arm64) is little-endian.
    SDL_AUDIO_S16 = SDL_AUDIO_S16LE,
    SDL_AUDIO_S32 = SDL_AUDIO_S32LE,
    SDL_AUDIO_F32 = SDL_AUDIO_F32LE,
}

/// <summary>
/// SDL Audio Device instance IDs.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly record struct SDL_AudioDeviceID(uint Value);

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
/// Opaque handle for an audio stream.
/// </summary>
public struct SDL_AudioStream;

/// <summary>
/// Native bindings for SDL_audio.h — audio device and stream management.
/// </summary>
[SuppressMessage("Interoperability", "CA1401:P/Invokes should not be visible")]
public static partial class Audio
{
    /// <summary>Default playback device ID.</summary>
    public static readonly SDL_AudioDeviceID SDL_AUDIO_DEVICE_DEFAULT_PLAYBACK = new(0xFFFFFFFFu);

    /// <summary>Default recording device ID.</summary>
    public static readonly SDL_AudioDeviceID SDL_AUDIO_DEVICE_DEFAULT_RECORDING = new(0xFFFFFFFEu);

    /// <summary>Auto-cleanup property for audio streams.</summary>
    public static ReadOnlySpan<byte> SDL_PROP_AUDIOSTREAM_AUTO_CLEANUP_BOOLEAN => "SDL.audiostream.auto_cleanup"u8;

    // --- Audio drivers ---

    /// <summary>Get the number of built-in audio drivers.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetNumAudioDrivers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_GetNumAudioDrivers();

    /// <summary>Get the name of a built-in audio driver.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetAudioDriver")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* SDL_GetAudioDriver(int index);

    /// <summary>Get the name of the current audio driver.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetCurrentAudioDriver")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* SDL_GetCurrentAudioDriver();

    // --- Device enumeration ---

    /// <summary>Get a list of currently-connected audio playback devices.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetAudioPlaybackDevices")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_AudioDeviceID* SDL_GetAudioPlaybackDevices(out int count);

    /// <summary>Get a list of currently-connected audio recording devices.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetAudioRecordingDevices")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_AudioDeviceID* SDL_GetAudioRecordingDevices(out int count);

    /// <summary>Get the human-readable name of an audio device.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetAudioDeviceName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* SDL_GetAudioDeviceName(SDL_AudioDeviceID devid);

    /// <summary>Get the current audio format of a specific audio device.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetAudioDeviceFormat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GetAudioDeviceFormat(SDL_AudioDeviceID devid, SDL_AudioSpec* spec, int* sample_frames);

    /// <summary>Get the current channel map of an audio device.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetAudioDeviceChannelMap")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial int* SDL_GetAudioDeviceChannelMap(SDL_AudioDeviceID devid, out int count);

    /// <summary>Query whether an audio device ID is a physical (not logical) device.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_IsAudioDevicePhysical")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_IsAudioDevicePhysical(SDL_AudioDeviceID devid);

    /// <summary>Query whether an audio device ID is a playback device.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_IsAudioDevicePlayback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_IsAudioDevicePlayback(SDL_AudioDeviceID devid);

    // --- Device lifecycle ---

    /// <summary>Open a specific audio device.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_OpenAudioDevice")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_AudioDeviceID SDL_OpenAudioDevice(SDL_AudioDeviceID devid, SDL_AudioSpec* spec);

    /// <summary>Close a previously-opened audio device.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_CloseAudioDevice")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_CloseAudioDevice(SDL_AudioDeviceID devid);

    // --- Device control ---

    /// <summary>Pause audio playback on a specified device.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_PauseAudioDevice")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_PauseAudioDevice(SDL_AudioDeviceID devid);

    /// <summary>Unpause audio playback on a specified device.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_ResumeAudioDevice")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_ResumeAudioDevice(SDL_AudioDeviceID devid);

    /// <summary>Query if an audio device is paused.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_AudioDevicePaused")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_AudioDevicePaused(SDL_AudioDeviceID devid);

    /// <summary>Get the gain of an audio device.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetAudioDeviceGain")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float SDL_GetAudioDeviceGain(SDL_AudioDeviceID devid);

    /// <summary>Change the gain of an audio device.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetAudioDeviceGain")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetAudioDeviceGain(SDL_AudioDeviceID devid, float gain);

    /// <summary>Set a callback that runs with a device's final mixed audio just before playback.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetAudioPostmixCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetAudioPostmixCallback(
        SDL_AudioDeviceID devid,
        delegate* unmanaged[Cdecl]<void*, SDL_AudioSpec*, float*, int, void> callback,
        void* userdata);

    // --- Stream binding ---

    /// <summary>Bind a single audio stream to an audio device.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_BindAudioStream")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_BindAudioStream(SDL_AudioDeviceID devid, SDL_AudioStream* stream);

    /// <summary>Unbind a single audio stream from its audio device.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_UnbindAudioStream")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_UnbindAudioStream(SDL_AudioStream* stream);

    /// <summary>Bind a list of audio streams to an audio device.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_BindAudioStreams")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_BindAudioStreams(SDL_AudioDeviceID devid, SDL_AudioStream** streams, int num_streams);

    /// <summary>Unbind a list of audio streams from their audio devices.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_UnbindAudioStreams")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_UnbindAudioStreams(SDL_AudioStream** streams, int num_streams);

    /// <summary>Query an audio stream for its currently-bound device.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetAudioStreamDevice")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_AudioDeviceID SDL_GetAudioStreamDevice(SDL_AudioStream* stream);

    // --- Stream lifecycle ---

    /// <summary>Create a new audio stream.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_CreateAudioStream")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_AudioStream* SDL_CreateAudioStream(SDL_AudioSpec* src_spec, SDL_AudioSpec* dst_spec);

    /// <summary>Destroy an audio stream.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_DestroyAudioStream")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_DestroyAudioStream(SDL_AudioStream* stream);

    /// <summary>Convenience function: open device, create stream, and bind together.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_OpenAudioDeviceStream")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_AudioStream* SDL_OpenAudioDeviceStream(SDL_AudioDeviceID devid, SDL_AudioSpec* spec, delegate* unmanaged[Cdecl]<void*, SDL_AudioStream*, int, int, void> callback, void* userdata);

    // --- Stream properties ---

    /// <summary>Get the properties associated with an audio stream.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetAudioStreamProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_PropertiesID SDL_GetAudioStreamProperties(SDL_AudioStream* stream);

    /// <summary>Query the current format of an audio stream.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetAudioStreamFormat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GetAudioStreamFormat(SDL_AudioStream* stream, SDL_AudioSpec* src_spec, SDL_AudioSpec* dst_spec);

    /// <summary>Change the input and output formats of an audio stream.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetAudioStreamFormat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetAudioStreamFormat(SDL_AudioStream* stream, SDL_AudioSpec* src_spec, SDL_AudioSpec* dst_spec);

    /// <summary>Get the frequency ratio of an audio stream.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetAudioStreamFrequencyRatio")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial float SDL_GetAudioStreamFrequencyRatio(SDL_AudioStream* stream);

    /// <summary>Change the frequency ratio of an audio stream.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetAudioStreamFrequencyRatio")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetAudioStreamFrequencyRatio(SDL_AudioStream* stream, float ratio);

    /// <summary>Get the gain of an audio stream.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetAudioStreamGain")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial float SDL_GetAudioStreamGain(SDL_AudioStream* stream);

    /// <summary>Change the gain of an audio stream.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetAudioStreamGain")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetAudioStreamGain(SDL_AudioStream* stream, float gain);

    /// <summary>Get the current input channel map of an audio stream.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetAudioStreamInputChannelMap")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial int* SDL_GetAudioStreamInputChannelMap(SDL_AudioStream* stream, out int count);

    /// <summary>Get the current output channel map of an audio stream.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetAudioStreamOutputChannelMap")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial int* SDL_GetAudioStreamOutputChannelMap(SDL_AudioStream* stream, out int count);

    /// <summary>Set the input channel map of an audio stream.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetAudioStreamInputChannelMap")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetAudioStreamInputChannelMap(SDL_AudioStream* stream, int* chmap, int count);

    /// <summary>Set the output channel map of an audio stream.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetAudioStreamOutputChannelMap")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetAudioStreamOutputChannelMap(SDL_AudioStream* stream, int* chmap, int count);

    /// <summary>Set a callback that runs when data is requested from an audio stream.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetAudioStreamGetCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetAudioStreamGetCallback(
        SDL_AudioStream* stream,
        delegate* unmanaged[Cdecl]<void*, SDL_AudioStream*, int, int, void> callback,
        void* userdata);

    /// <summary>Set a callback that runs when data is added to an audio stream.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetAudioStreamPutCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetAudioStreamPutCallback(
        SDL_AudioStream* stream,
        delegate* unmanaged[Cdecl]<void*, SDL_AudioStream*, int, int, void> callback,
        void* userdata);

    /// <summary>Lock an audio stream for serialized access.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_LockAudioStream")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_LockAudioStream(SDL_AudioStream* stream);

    /// <summary>Unlock an audio stream for serialized access.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_UnlockAudioStream")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_UnlockAudioStream(SDL_AudioStream* stream);

    // --- Stream data ---

    /// <summary>Add data to the stream.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_PutAudioStreamData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_PutAudioStreamData(SDL_AudioStream* stream, void* buf, int len);

    // Skipped SDL_PutAudioStreamDataNoCopy: the zero-copy put requires the caller
    // to keep the native buffer alive until an asynchronous completion callback
    // fires, which is incompatible with managed memory without unbounded pinning.
    // SDL_PutAudioStreamData / SDL_PutAudioStreamPlanarData cover the use case.

    /// <summary>Add data to the stream, given non-interleaved (planar) channel buffers.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_PutAudioStreamPlanarData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_PutAudioStreamPlanarData(
        SDL_AudioStream* stream, void** channel_buffers, int num_channels, int num_samples);

    /// <summary>Get converted/resampled data from the stream.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetAudioStreamData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial int SDL_GetAudioStreamData(SDL_AudioStream* stream, void* buf, int len);

    /// <summary>Get the number of converted/resampled bytes available.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetAudioStreamAvailable")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial int SDL_GetAudioStreamAvailable(SDL_AudioStream* stream);

    /// <summary>Get the number of bytes currently queued.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetAudioStreamQueued")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial int SDL_GetAudioStreamQueued(SDL_AudioStream* stream);

    /// <summary>Flush the stream — convert/resample all buffered data immediately.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_FlushAudioStream")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_FlushAudioStream(SDL_AudioStream* stream);

    /// <summary>Clear any pending data in the stream.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_ClearAudioStream")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_ClearAudioStream(SDL_AudioStream* stream);

    // --- Stream device control ---

    /// <summary>Pause the audio device associated with a stream.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_PauseAudioStreamDevice")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_PauseAudioStreamDevice(SDL_AudioStream* stream);

    /// <summary>Resume the audio device associated with a stream.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_ResumeAudioStreamDevice")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_ResumeAudioStreamDevice(SDL_AudioStream* stream);

    /// <summary>Query if the audio device associated with a stream is paused.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_AudioStreamDevicePaused")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_AudioStreamDevicePaused(SDL_AudioStream* stream);

    // --- WAV loading ---

    /// <summary>Load the audio data of a WAVE file into memory.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_LoadWAV")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_LoadWAV(ReadOnlySpan<byte> path, SDL_AudioSpec* spec, byte** audio_buf, uint* audio_len);

    // Skipped SDL_LoadWAV_IO: IO-stream variant of SDL_LoadWAV. .NET has its own
    // stream abstractions; SDL_LoadWAV (path-based) is bound and covers the case.

    // --- Format info ---

    /// <summary>Get the human readable name of an audio format.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetAudioFormatName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* SDL_GetAudioFormatName(SDL_AudioFormat format);

    /// <summary>Get the appropriate memset value for silence in a given audio format.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetSilenceValueForFormat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_GetSilenceValueForFormat(SDL_AudioFormat format);

    // --- Mixing and conversion ---

    /// <summary>Mix audio data in a specified format.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_MixAudio")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_MixAudio(byte* dst, byte* src, SDL_AudioFormat format, uint len, float volume);

    /// <summary>Convert samples from one format to another.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_ConvertAudioSamples")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_ConvertAudioSamples(
        SDL_AudioSpec* src_spec, byte* src_data, int src_len,
        SDL_AudioSpec* dst_spec, byte** dst_data, int* dst_len);
}
