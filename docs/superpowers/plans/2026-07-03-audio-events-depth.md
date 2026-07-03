# Phase 10: Audio + Events Depth Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Bring SDL_audio.h and SDL_events.h to full accounting — pull-model audio callbacks, audio data utilities and format introspection, plus public event-queue operations, custom user events, watches/filters, and typed dispatch for every remaining event family.

**Architecture:** Native bindings (`SdlSharp.Native`) get the remaining function-pointer callbacks, data utilities, channel-map getters, event-queue-surgery functions, and three missing event structs. The managed layer follows established house patterns: single-delegate callback setters backed by per-slot `GCHandle` + `[UnmanagedCallersOnly]` trampolines (mirroring `Window.SetHitTest`), raw `nint` user-event payloads, and `readonly record struct` event args dispatched from a refactored `Application.DispatchOne`. Two headless samples prove the surface under `SDL_AUDIO_DRIVER=dummy`.

**Tech Stack:** C# / .NET 10, `[LibraryImport]` P/Invoke, SDL 3.4.2. No unit-test framework in this repo — verification is the zero-warning solution build plus runtime-assertion sample programs (the established phase-7-through-9 pattern).

**Spec:** `docs/superpowers/specs/2026-07-03-audio-events-depth-design.md`

**Conventions reminder (from CLAUDE.md):**
- Native layer: `[LibraryImport(Common.Sdl3, EntryPoint = "…")]` + `[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]`, `[return: MarshalAs(UnmanagedType.U1)]` on bool returns, `[MarshalAs(UnmanagedType.U1)]` on bool params, `ReadOnlySpan<byte>` for C-string inputs, `byte*` returns decoded via `Marshal.PtrToStringUTF8`. CS1591 suppressed here — XML docs optional.
- High-level layer: every public member gets `///` XML docs (build fails otherwise). No `SdlSharp.Native` types in public signatures. `readonly record struct` for value/event-arg types. Callbacks use `GCHandle` + static `[UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]` trampolines that swallow exceptions.
- Error checking: `Common.Check(bool)`, `Common.Check<T>(T*)`, `Common.CheckId(uint)` → throw `SdlException`.
- Build gate after every task: `dotnet build SdlSharp.slnx` → **0 Warnings, 0 Errors**. Stale LSP diagnostics are known ghosts in this repo; a fresh `dotnet build` is authoritative.

---

## Task 1: Native audio additions

**Files:**
- Modify: `src/SdlSharp/Native/Audio.cs`

Bind the 18 remaining wrappable audio functions, add the native `SDL_AUDIO_S16/S32/F32` alias enum members, and add two skip comments. The native callback delegate types are:
- `SDL_AudioStreamCallback` = `void(void* userdata, SDL_AudioStream* stream, int additional_amount, int total_amount)` → `delegate* unmanaged[Cdecl]<void*, SDL_AudioStream*, int, int, void>`
- `SDL_AudioPostmixCallback` = `void(void* userdata, const SDL_AudioSpec* spec, float* buffer, int buflen)` → `delegate* unmanaged[Cdecl]<void*, SDL_AudioSpec*, float*, int, void>`

- [ ] **Step 1: Add the S16/S32/F32 alias members to the native `SDL_AudioFormat` enum**

In `src/SdlSharp/Native/Audio.cs`, find the `SDL_AudioFormat` enum (members `SDL_AUDIO_F32BE = 0x9120` etc.). Add these three aliases after `SDL_AUDIO_F32BE`:

```csharp
    // Native byte-order aliases. SDL's header resolves these to the LE or BE
    // variant by compile-time byte order; every platform the redist ships for
    // (win-x64/arm64, osx, linux-x64/arm64) is little-endian.
    SDL_AUDIO_S16 = SDL_AUDIO_S16LE,
    SDL_AUDIO_S32 = SDL_AUDIO_S32LE,
    SDL_AUDIO_F32 = SDL_AUDIO_F32LE,
```

- [ ] **Step 2: Add the callback setter bindings**

Add inside the `Audio` partial class:

```csharp
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetAudioStreamGetCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetAudioStreamGetCallback(
        SDL_AudioStream* stream,
        delegate* unmanaged[Cdecl]<void*, SDL_AudioStream*, int, int, void> callback,
        void* userdata);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetAudioStreamPutCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetAudioStreamPutCallback(
        SDL_AudioStream* stream,
        delegate* unmanaged[Cdecl]<void*, SDL_AudioStream*, int, int, void> callback,
        void* userdata);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetAudioPostmixCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetAudioPostmixCallback(
        SDL_AudioDeviceID devid,
        delegate* unmanaged[Cdecl]<void*, SDL_AudioSpec*, float*, int, void> callback,
        void* userdata);
```

- [ ] **Step 3: Add lock/unlock, mix, convert, planar put, plural bind/unbind bindings**

```csharp
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_LockAudioStream")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_LockAudioStream(SDL_AudioStream* stream);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_UnlockAudioStream")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_UnlockAudioStream(SDL_AudioStream* stream);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_MixAudio")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_MixAudio(byte* dst, byte* src, SDL_AudioFormat format, uint len, float volume);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_ConvertAudioSamples")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_ConvertAudioSamples(
        SDL_AudioSpec* src_spec, byte* src_data, int src_len,
        SDL_AudioSpec* dst_spec, byte** dst_data, int* dst_len);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_PutAudioStreamPlanarData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_PutAudioStreamPlanarData(
        SDL_AudioStream* stream, void** channel_buffers, int num_channels, int num_samples);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_BindAudioStreams")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_BindAudioStreams(SDL_AudioDeviceID devid, SDL_AudioStream** streams, int num_streams);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_UnbindAudioStreams")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_UnbindAudioStreams(SDL_AudioStream** streams, int num_streams);
```

- [ ] **Step 4: Add silence, device-predicate, and channel-map bindings**

```csharp
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetSilenceValueForFormat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_GetSilenceValueForFormat(SDL_AudioFormat format);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_IsAudioDevicePhysical")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_IsAudioDevicePhysical(SDL_AudioDeviceID devid);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_IsAudioDevicePlayback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_IsAudioDevicePlayback(SDL_AudioDeviceID devid);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetAudioDeviceChannelMap")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial int* SDL_GetAudioDeviceChannelMap(SDL_AudioDeviceID devid, out int count);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetAudioStreamInputChannelMap")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial int* SDL_GetAudioStreamInputChannelMap(SDL_AudioStream* stream, out int count);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetAudioStreamOutputChannelMap")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial int* SDL_GetAudioStreamOutputChannelMap(SDL_AudioStream* stream, out int count);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetAudioStreamInputChannelMap")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetAudioStreamInputChannelMap(SDL_AudioStream* stream, int* chmap, int count);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetAudioStreamOutputChannelMap")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetAudioStreamOutputChannelMap(SDL_AudioStream* stream, int* chmap, int count);
```

- [ ] **Step 5: Add the skip comments**

Add near the other bindings (follow the house skip-comment pattern):

```csharp
    // Skipped SDL_LoadWAV_IO: IO-stream variant of SDL_LoadWAV. .NET has its own
    // stream abstractions; SDL_LoadWAV (path-based) is bound and covers the case.

    // Skipped SDL_PutAudioStreamDataNoCopy: the zero-copy put requires the caller
    // to keep the native buffer alive until an asynchronous completion callback
    // fires, which is incompatible with managed memory without unbounded pinning.
    // SDL_PutAudioStreamData / SDL_PutAudioStreamPlanarData cover the use case.
```

- [ ] **Step 6: Build**

Run: `dotnet build SdlSharp.slnx`
Expected: 0 Warnings, 0 Errors.

- [ ] **Step 7: Commit**

```bash
git add src/SdlSharp/Native/Audio.cs
git commit -m "Bind remaining SDL_audio.h functions and native format aliases"
```

---

## Task 2: Audio format introspection + AudioSpec.FrameSize + public format aliases

**Files:**
- Modify: `src/SdlSharp/Audio/AudioFormat.cs`
- Modify: `src/SdlSharp/Audio/AudioFormatInfo.cs`
- Modify: `src/SdlSharp/Audio/AudioSpec.cs`

SDL's format-inspection macros (from `SDL_audio.h`), for reference — `format` is the 16-bit `SDL_AudioFormat` value:
- `SDL_AUDIO_BITSIZE(x)` = `x & 0xFF`
- `SDL_AUDIO_BYTESIZE(x)` = `BITSIZE(x) / 8`
- `SDL_AUDIO_ISFLOAT(x)` = `x & 0x0100` (`SDL_AUDIO_MASK_FLOAT`)
- `SDL_AUDIO_ISBIGENDIAN(x)` = `x & 0x1000` (`SDL_AUDIO_MASK_BIG_ENDIAN`)
- `SDL_AUDIO_ISSIGNED(x)` = `x & 0x8000` (`SDL_AUDIO_MASK_SIGNED`)
- `SDL_AUDIO_ISINT(x)` = `!ISFLOAT(x)`, `SDL_AUDIO_ISLITTLEENDIAN(x)` = `!ISBIGENDIAN(x)`, `SDL_AUDIO_ISUNSIGNED(x)` = `!ISSIGNED(x)`

- [ ] **Step 1: Add the S16/S32/F32 public aliases to `AudioFormat`**

In `src/SdlSharp/Audio/AudioFormat.cs`, add after `F32BE`:

```csharp
    /// <summary>Signed 16-bit samples in native byte order (little-endian on all supported platforms).</summary>
    S16 = (ushort)Native.SDL_AudioFormat.SDL_AUDIO_S16,
    /// <summary>Signed 32-bit integer samples in native byte order (little-endian on all supported platforms).</summary>
    S32 = (ushort)Native.SDL_AudioFormat.SDL_AUDIO_S32,
    /// <summary>32-bit floating point samples in native byte order (little-endian on all supported platforms).</summary>
    F32 = (ushort)Native.SDL_AudioFormat.SDL_AUDIO_F32,
```

- [ ] **Step 2: Convert `AudioFormatInfo` to extension members and add introspection**

Replace the body of `src/SdlSharp/Audio/AudioFormatInfo.cs` with:

```csharp
using System.Runtime.InteropServices;

using static SdlSharp.Native.Audio;

namespace SdlSharp.Audio;

/// <summary>
/// Extension methods that inspect <see cref="AudioFormat"/> values.
/// </summary>
public static unsafe class AudioFormatInfo
{
    /// <summary>
    /// Gets the human-readable name of an audio format (for example "SDL_AUDIO_S16LE").
    /// </summary>
    /// <param name="format">The audio format.</param>
    /// <returns>The format name, or null if unavailable.</returns>
    public static string? GetName(this AudioFormat format) =>
        Marshal.PtrToStringUTF8((nint)SDL_GetAudioFormatName((Native.SDL_AudioFormat)format));

    /// <summary>Gets the size of a single sample in bits.</summary>
    /// <param name="format">The audio format.</param>
    public static int GetBitSize(this AudioFormat format) => (ushort)format & 0xFF;

    /// <summary>Gets the size of a single sample in bytes.</summary>
    /// <param name="format">The audio format.</param>
    public static int GetByteSize(this AudioFormat format) => format.GetBitSize() / 8;

    /// <summary>Gets whether the format holds floating-point samples.</summary>
    /// <param name="format">The audio format.</param>
    public static bool IsFloat(this AudioFormat format) => ((ushort)format & 0x0100) != 0;

    /// <summary>Gets whether the format holds integer samples.</summary>
    /// <param name="format">The audio format.</param>
    public static bool IsInt(this AudioFormat format) => !format.IsFloat();

    /// <summary>Gets whether the format stores samples big-endian.</summary>
    /// <param name="format">The audio format.</param>
    public static bool IsBigEndian(this AudioFormat format) => ((ushort)format & 0x1000) != 0;

    /// <summary>Gets whether the format stores samples little-endian.</summary>
    /// <param name="format">The audio format.</param>
    public static bool IsLittleEndian(this AudioFormat format) => !format.IsBigEndian();

    /// <summary>Gets whether the format holds signed samples.</summary>
    /// <param name="format">The audio format.</param>
    public static bool IsSigned(this AudioFormat format) => ((ushort)format & 0x8000) != 0;

    /// <summary>Gets whether the format holds unsigned samples.</summary>
    /// <param name="format">The audio format.</param>
    public static bool IsUnsigned(this AudioFormat format) => !format.IsSigned();

    /// <summary>
    /// Gets the byte value that represents silence for this format.
    /// </summary>
    /// <param name="format">The audio format.</param>
    public static int GetSilenceValue(this AudioFormat format) =>
        SDL_GetSilenceValueForFormat((Native.SDL_AudioFormat)format);
}
```

- [ ] **Step 3: Add `FrameSize` to `AudioSpec`**

`src/SdlSharp/Audio/AudioSpec.cs` currently reads:

```csharp
public readonly record struct AudioSpec(AudioFormat Format, int Channels, int Frequency);
```

Replace it with a body-bearing form that keeps the positional record and adds the computed property:

```csharp
namespace SdlSharp.Audio;

/// <summary>
/// Describes an audio data format: sample format, channel count, and sample rate.
/// </summary>
/// <param name="Format">The sample format.</param>
/// <param name="Channels">The number of channels (1 = mono, 2 = stereo, …).</param>
/// <param name="Frequency">The sample rate in Hz.</param>
public readonly record struct AudioSpec(AudioFormat Format, int Channels, int Frequency)
{
    /// <summary>
    /// Gets the size in bytes of one full sample frame (one sample per channel).
    /// </summary>
    public int FrameSize => Format.GetByteSize() * Channels;
}
```

(Preserve whatever existing doc comment / using lines the file already has; only the type body changes.)

- [ ] **Step 4: Build**

Run: `dotnet build SdlSharp.slnx`
Expected: 0 Warnings, 0 Errors. (Note: `GetName` moved from a static method to an extension method — the call site inside the library, if any, still compiles as `format.GetName()` or `AudioFormatInfo.GetName(format)`; grep `GetName(` under `src/` and `Samples/` to confirm no break.)

- [ ] **Step 5: Commit**

```bash
git add src/SdlSharp/Audio/AudioFormat.cs src/SdlSharp/Audio/AudioFormatInfo.cs src/SdlSharp/Audio/AudioSpec.cs
git commit -m "Add audio format introspection, native-order aliases, and AudioSpec.FrameSize"
```

---

## Task 3: AudioSamples static class (Mix + Convert)

**Files:**
- Create: `src/SdlSharp/Audio/AudioSamples.cs`

- [ ] **Step 1: Create the file**

```csharp
using static SdlSharp.Native.Audio;
using static SdlSharp.Native.Common;

namespace SdlSharp.Audio;

/// <summary>
/// Stateless helpers for mixing and converting raw audio sample buffers.
/// </summary>
public static unsafe class AudioSamples
{
    /// <summary>
    /// Mixes <paramref name="source"/> into <paramref name="destination"/> at the given volume,
    /// interpreting both buffers as samples of <paramref name="format"/>. The two spans must be
    /// the same length. Mixing more than two streams into the same destination may clip;
    /// use an <see cref="AudioStream"/> for general-purpose mixing.
    /// </summary>
    /// <param name="destination">The buffer mixed into, in place.</param>
    /// <param name="source">The buffer to add.</param>
    /// <param name="format">The sample format of both buffers.</param>
    /// <param name="volume">The mix volume, 0.0 (silence) to 1.0 (full); may exceed 1.0 to amplify.</param>
    /// <exception cref="ArgumentException">The spans differ in length.</exception>
    public static void Mix(Span<byte> destination, ReadOnlySpan<byte> source, AudioFormat format, float volume)
    {
        if (destination.Length != source.Length)
        {
            throw new ArgumentException("Destination and source must be the same length.", nameof(source));
        }

        fixed (byte* dst = destination)
        fixed (byte* src = source)
        {
            Check(SDL_MixAudio(dst, src, (Native.SDL_AudioFormat)format, (uint)source.Length, volume));
        }
    }

    /// <summary>
    /// Converts a complete audio buffer from one format to another. Suitable for whole,
    /// in-memory clips; for streaming conversion use <see cref="AudioStream"/>.
    /// </summary>
    /// <param name="sourceSpec">The format of <paramref name="sourceData"/>.</param>
    /// <param name="sourceData">The input audio bytes.</param>
    /// <param name="destinationSpec">The desired output format.</param>
    /// <returns>A newly allocated buffer holding the converted audio.</returns>
    public static byte[] Convert(AudioSpec sourceSpec, ReadOnlySpan<byte> sourceData, AudioSpec destinationSpec)
    {
        var srcSpec = ToNativeSpec(sourceSpec);
        var dstSpec = ToNativeSpec(destinationSpec);
        byte* dstData;
        int dstLen;

        fixed (byte* src = sourceData)
        {
            Check(SDL_ConvertAudioSamples(&srcSpec, src, sourceData.Length, &dstSpec, &dstData, &dstLen));
        }

        try
        {
            var result = new byte[dstLen];
            new ReadOnlySpan<byte>(dstData, dstLen).CopyTo(result);
            return result;
        }
        finally
        {
            SDL_free(dstData);
        }
    }

    private static Native.SDL_AudioSpec ToNativeSpec(AudioSpec spec) =>
        new()
        {
            format = (Native.SDL_AudioFormat)spec.Format,
            channels = spec.Channels,
            freq = spec.Frequency
        };
}
```

- [ ] **Step 2: Build**

Run: `dotnet build SdlSharp.slnx`
Expected: 0 Warnings, 0 Errors.

- [ ] **Step 3: Commit**

```bash
git add src/SdlSharp/Audio/AudioSamples.cs
git commit -m "Add AudioSamples.Mix and AudioSamples.Convert helpers"
```

---

## Task 4: AudioStream callbacks, lock scope, device, planar put, channel maps

**Files:**
- Modify: `src/SdlSharp/Audio/AudioStream.cs`
- Create: `src/SdlSharp/Audio/AudioStreamLock.cs`

- [ ] **Step 1: Create the lock scope type**

`src/SdlSharp/Audio/AudioStreamLock.cs`:

```csharp
using static SdlSharp.Native.Audio;

namespace SdlSharp.Audio;

/// <summary>
/// A scope that keeps an <see cref="AudioStream"/> locked against its audio-thread
/// callbacks. Obtain one with <see cref="AudioStream.Lock"/> and dispose it (for
/// example with <c>using</c>) to unlock. While held, the stream's get/put callbacks
/// will not run, so shared state can be updated safely.
/// </summary>
public readonly ref struct AudioStreamLock
{
    private readonly unsafe Native.SDL_AudioStream* _stream;

    internal unsafe AudioStreamLock(Native.SDL_AudioStream* stream) => _stream = stream;

    /// <summary>Unlocks the stream.</summary>
    public unsafe void Dispose() => SDL_UnlockAudioStream(_stream);
}
```

- [ ] **Step 2: Add callback delegate, holder, trampolines, GCHandle fields to `AudioStream`**

In `src/SdlSharp/Audio/AudioStream.cs`, add `using System.Runtime.CompilerServices;` and `using System.Runtime.InteropServices;` at the top (keep existing usings). Add a namespace-level delegate above the class:

```csharp
/// <summary>
/// A callback invoked when an audio stream needs more data (get) or has received
/// data (put). It runs on SDL's audio thread — keep it fast, avoid allocation and
/// blocking, and use <see cref="AudioStream.Lock"/> to guard shared state.
/// Exceptions that escape the callback are swallowed.
/// </summary>
/// <param name="stream">The stream that triggered the callback.</param>
/// <param name="additionalAmount">The amount, in bytes, of additional data the stream wants (get) or received (put); may be zero.</param>
/// <param name="totalAmount">The total amount, in bytes, currently queued/requested.</param>
public delegate void AudioStreamDataCallback(AudioStream stream, int additionalAmount, int totalAmount);
```

Inside the class, add fields near `_handle`:

```csharp
    private GCHandle _getCallbackHandle;
    private GCHandle _putCallbackHandle;

    private sealed record CallbackHolder(AudioStream Stream, AudioStreamDataCallback Callback);
```

- [ ] **Step 3: Add the trampolines and the setter methods**

```csharp
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void DataCallback(void* userdata, Native.SDL_AudioStream* stream, int additionalAmount, int totalAmount)
    {
        try
        {
            var holder = (CallbackHolder)GCHandle.FromIntPtr((nint)userdata).Target!;
            holder.Callback(holder.Stream, additionalAmount, totalAmount);
        }
        catch
        {
            // Must not cross the native boundary on the audio thread.
        }
    }

    /// <summary>
    /// Sets or clears (<c>null</c>) a callback that runs when the stream needs more data.
    /// The callback runs on SDL's audio thread. Setting a new callback replaces any
    /// previous one. See <see cref="AudioStreamDataCallback"/> for the threading contract.
    /// </summary>
    /// <param name="callback">The callback, or <c>null</c> to clear.</param>
    public void SetGetCallback(AudioStreamDataCallback? callback) =>
        SetCallback(callback, ref _getCallbackHandle, isGet: true);

    /// <summary>
    /// Sets or clears (<c>null</c>) a callback that runs after data has been added to the stream.
    /// The callback runs on SDL's audio thread. Setting a new callback replaces any
    /// previous one. See <see cref="AudioStreamDataCallback"/> for the threading contract.
    /// </summary>
    /// <param name="callback">The callback, or <c>null</c> to clear.</param>
    public void SetPutCallback(AudioStreamDataCallback? callback) =>
        SetCallback(callback, ref _putCallbackHandle, isGet: false);

    private void SetCallback(AudioStreamDataCallback? callback, ref GCHandle slot, bool isGet)
    {
        var previous = slot;

        if (callback == null)
        {
            if (isGet)
            {
                Check(SDL_SetAudioStreamGetCallback(Handle, null, null));
            }
            else
            {
                Check(SDL_SetAudioStreamPutCallback(Handle, null, null));
            }

            slot = default;
        }
        else
        {
            var newHandle = GCHandle.Alloc(new CallbackHolder(this, callback));
            var ok = isGet
                ? SDL_SetAudioStreamGetCallback(Handle, &DataCallback, (void*)GCHandle.ToIntPtr(newHandle))
                : SDL_SetAudioStreamPutCallback(Handle, &DataCallback, (void*)GCHandle.ToIntPtr(newHandle));

            if (!ok)
            {
                newHandle.Free();
                throw new SdlException();
            }

            slot = newHandle;
        }

        if (previous.IsAllocated)
        {
            previous.Free();
        }
    }
```

- [ ] **Step 4: Add `Lock`, `Device`, `PutPlanarData`, and channel-map members**

```csharp
    /// <summary>
    /// Locks the stream against its audio-thread callbacks and returns a scope that
    /// unlocks on dispose. Use <c>using var _ = stream.Lock();</c> to guard access to
    /// state shared with a get/put callback.
    /// </summary>
    /// <returns>A lock scope; dispose it to unlock.</returns>
    public AudioStreamLock Lock()
    {
        Check(SDL_LockAudioStream(Handle));
        return new AudioStreamLock(Handle);
    }

    /// <summary>
    /// Gets the audio device this stream is currently bound to, or <c>null</c> if it is
    /// unbound. The returned wrapper does not own the device; each access returns a new
    /// wrapper and wrappers are not equal to each other.
    /// </summary>
    public AudioDevice? Device
    {
        get
        {
            var id = SDL_GetAudioStreamDevice(Handle);
            return id.Value == 0 ? null : new AudioDevice(id, ownsHandle: false);
        }
    }

    /// <summary>
    /// Adds one buffer per channel of non-interleaved (planar) audio to the stream.
    /// Each channel buffer holds <paramref name="numSamples"/> samples laid out per the
    /// stream's source format.
    /// </summary>
    /// <param name="channels">One byte buffer per channel; all must be the same length.</param>
    /// <param name="numSamples">The number of samples per channel.</param>
    /// <exception cref="ArgumentException"><paramref name="channels"/> is empty.</exception>
    public void PutPlanarData(byte[][] channels, int numSamples)
    {
        if (channels.Length == 0)
        {
            throw new ArgumentException("At least one channel is required.", nameof(channels));
        }

        var handles = new GCHandle[channels.Length];
        var pointers = stackalloc void*[channels.Length];
        try
        {
            for (var i = 0; i < channels.Length; i++)
            {
                handles[i] = GCHandle.Alloc(channels[i], GCHandleType.Pinned);
                pointers[i] = (void*)handles[i].AddrOfPinnedObject();
            }

            Check(SDL_PutAudioStreamPlanarData(Handle, pointers, channels.Length, numSamples));
        }
        finally
        {
            foreach (var h in handles)
            {
                if (h.IsAllocated) h.Free();
            }
        }
    }

    /// <summary>Gets the current input channel map, or <c>null</c> for the default order.</summary>
    /// <returns>The channel map array, or <c>null</c>.</returns>
    public int[]? GetInputChannelMap() => ReadChannelMap(SDL_GetAudioStreamInputChannelMap(Handle, out var count), count);

    /// <summary>Gets the current output channel map, or <c>null</c> for the default order.</summary>
    /// <returns>The channel map array, or <c>null</c>.</returns>
    public int[]? GetOutputChannelMap() => ReadChannelMap(SDL_GetAudioStreamOutputChannelMap(Handle, out var count), count);

    /// <summary>Sets the input channel map, or passes <c>null</c> to reset to the default order.</summary>
    /// <param name="map">The channel map, or <c>null</c> to reset.</param>
    public void SetInputChannelMap(int[]? map) => SetChannelMap(map, input: true);

    /// <summary>Sets the output channel map, or passes <c>null</c> to reset to the default order.</summary>
    /// <param name="map">The channel map, or <c>null</c> to reset.</param>
    public void SetOutputChannelMap(int[]? map) => SetChannelMap(map, input: false);

    private void SetChannelMap(int[]? map, bool input)
    {
        fixed (int* ptr = map)
        {
            var count = map?.Length ?? 0;
            var ok = input
                ? SDL_SetAudioStreamInputChannelMap(Handle, ptr, count)
                : SDL_SetAudioStreamOutputChannelMap(Handle, ptr, count);
            Check(ok);
        }
    }

    private static int[]? ReadChannelMap(int* map, int count)
    {
        if (map == null) return null;
        try
        {
            var result = new int[count];
            new ReadOnlySpan<int>(map, count).CopyTo(result);
            return result;
        }
        finally
        {
            SDL_free(map);
        }
    }
```

(`SDL_free` comes from `static SdlSharp.Native.Common`, already imported; if not, add `using static SdlSharp.Native.Common;`.)

- [ ] **Step 5: Free callback handles in `Dispose`**

Change the existing `Dispose` to free the callback handles after destroying the stream:

```csharp
    /// <inheritdoc/>
    public void Dispose()
    {
        if (_ownsHandle && _handle != null)
        {
            SDL_DestroyAudioStream(_handle);
        }
        _handle = null;

        if (_getCallbackHandle.IsAllocated) _getCallbackHandle.Free();
        if (_putCallbackHandle.IsAllocated) _putCallbackHandle.Free();
    }
```

- [ ] **Step 6: Build**

Run: `dotnet build SdlSharp.slnx`
Expected: 0 Warnings, 0 Errors.

- [ ] **Step 7: Commit**

```bash
git add src/SdlSharp/Audio/AudioStream.cs src/SdlSharp/Audio/AudioStreamLock.cs
git commit -m "Add audio stream callbacks, lock scope, device, planar put, and channel maps"
```

---

## Task 5: AudioDevice postmix, plural bind/unbind, predicates, channel map

**Files:**
- Modify: `src/SdlSharp/Audio/AudioDevice.cs`

- [ ] **Step 1: Add usings and the postmix delegate**

Add `using System.Runtime.CompilerServices;` (keep existing `using System.Runtime.InteropServices;`). Add a namespace-level delegate above the class:

```csharp
/// <summary>
/// A callback invoked with the final mixed audio just before it is sent to a device.
/// It runs on SDL's audio thread — keep it fast and avoid blocking. The buffer is the
/// device's mix in 32-bit float and may be modified in place. Exceptions that escape
/// the callback are swallowed.
/// </summary>
/// <param name="spec">The format of the buffer (always 32-bit float samples).</param>
/// <param name="buffer">The audio samples, modifiable in place.</param>
public delegate void AudioPostmixCallback(in AudioSpec spec, Span<float> buffer);
```

- [ ] **Step 2: Add the postmix GCHandle field, holder, trampoline, and setter**

Inside the class, near `_id`:

```csharp
    private GCHandle _postmixHandle;

    private sealed record PostmixHolder(AudioPostmixCallback Callback);

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void PostmixCallback(void* userdata, Native.SDL_AudioSpec* spec, float* buffer, int buflen)
    {
        try
        {
            var holder = (PostmixHolder)GCHandle.FromIntPtr((nint)userdata).Target!;
            var managedSpec = new AudioSpec((AudioFormat)spec->format, spec->channels, spec->freq);
            var samples = new Span<float>(buffer, buflen / sizeof(float));
            holder.Callback(in managedSpec, samples);
        }
        catch
        {
            // Must not cross the native boundary on the audio thread.
        }
    }

    /// <summary>
    /// Sets or clears (<c>null</c>) a callback that runs with the device's final mixed
    /// audio just before playback. The callback runs on SDL's audio thread. Setting a
    /// new callback replaces any previous one. See <see cref="AudioPostmixCallback"/>.
    /// </summary>
    /// <param name="callback">The callback, or <c>null</c> to clear.</param>
    public void SetPostmixCallback(AudioPostmixCallback? callback)
    {
        var previous = _postmixHandle;

        if (callback == null)
        {
            Check(SDL_SetAudioPostmixCallback(Id, null, null));
            _postmixHandle = default;
        }
        else
        {
            var newHandle = GCHandle.Alloc(new PostmixHolder(callback));
            if (!SDL_SetAudioPostmixCallback(Id, &PostmixCallback, (void*)GCHandle.ToIntPtr(newHandle)))
            {
                newHandle.Free();
                throw new SdlException();
            }

            _postmixHandle = newHandle;
        }

        if (previous.IsAllocated)
        {
            previous.Free();
        }
    }
```

- [ ] **Step 3: Add plural bind/unbind, predicates, and channel map**

```csharp
    /// <summary>
    /// Binds one or more audio streams to this device in a single call.
    /// </summary>
    /// <param name="streams">The streams to bind.</param>
    public void Bind(params AudioStream[] streams)
    {
        if (streams.Length == 0) return;

        var pointers = stackalloc Native.SDL_AudioStream*[streams.Length];
        for (var i = 0; i < streams.Length; i++)
        {
            pointers[i] = streams[i].Handle;
        }

        Check(SDL_BindAudioStreams(Id, pointers, streams.Length));
    }

    /// <summary>
    /// Unbinds one or more audio streams from whatever devices they are bound to.
    /// </summary>
    /// <param name="streams">The streams to unbind.</param>
    public static void Unbind(params AudioStream[] streams)
    {
        if (streams.Length == 0) return;

        var pointers = stackalloc Native.SDL_AudioStream*[streams.Length];
        for (var i = 0; i < streams.Length; i++)
        {
            pointers[i] = streams[i].Handle;
        }

        SDL_UnbindAudioStreams(pointers, streams.Length);
    }

    /// <summary>Gets whether this device ID refers to a physical (not logical) device.</summary>
    public bool IsPhysical => SDL_IsAudioDevicePhysical(Id);

    /// <summary>Gets whether this device is a playback device (<c>false</c> for a recording device).</summary>
    public bool IsPlayback => SDL_IsAudioDevicePlayback(Id);

    /// <summary>
    /// Gets this device's channel map, or <c>null</c> for the default order.
    /// </summary>
    /// <returns>The channel map array, or <c>null</c>.</returns>
    public int[]? GetChannelMap()
    {
        var map = SDL_GetAudioDeviceChannelMap(Id, out var count);
        if (map == null) return null;
        try
        {
            var result = new int[count];
            new ReadOnlySpan<int>(map, count).CopyTo(result);
            return result;
        }
        finally
        {
            SDL_free(map);
        }
    }
```

Note the existing single-stream `Bind(AudioStream stream)` stays. The new `Bind(params AudioStream[])` is an overload; C# resolves `Bind(oneStream)` to the single-arg overload and `Bind(a, b)` to the params overload — no ambiguity.

- [ ] **Step 4: Free the postmix handle in `Dispose`**

```csharp
    /// <inheritdoc/>
    public void Dispose()
    {
        if (_ownsHandle && _id.Value != 0)
        {
            SDL_CloseAudioDevice(_id);
        }
        _id = default;

        if (_postmixHandle.IsAllocated) _postmixHandle.Free();
    }
```

- [ ] **Step 5: Build**

Run: `dotnet build SdlSharp.slnx`
Expected: 0 Warnings, 0 Errors.

- [ ] **Step 6: Commit**

```bash
git add src/SdlSharp/Audio/AudioDevice.cs
git commit -m "Add audio device postmix callback, plural bind/unbind, predicates, channel map"
```

---

## Task 6: Native events additions (7 functions + 3 structs + union members)

**Files:**
- Modify: `src/SdlSharp/Native/Events.cs`

The event-filter delegate type is `SDL_EventFilter` = `bool(void* userdata, SDL_Event* event)` → `delegate* unmanaged[Cdecl]<void*, SDL_Event*, byte>` (bool marshals as `byte` in function-pointer context).

- [ ] **Step 1: Add the three missing event structs**

Add near the other event structs (before the `SDL_Event` union). Note `SDL_AudioDeviceEvent`'s `which` is `SDL_AudioDeviceID` and `SDL_CameraDeviceEvent`'s `which` is `SDL_CameraID` — both are already defined (`Native/Audio.cs`, `Native/Camera.cs`) in the `SdlSharp.Native` namespace:

```csharp
/// <summary>
/// Audio device hotplug event structure.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_AudioDeviceEvent
{
    public SDL_EventType type;
    public uint reserved;
    public ulong timestamp;
    public SDL_AudioDeviceID which;
    public byte recording;
    public byte padding1;
    public byte padding2;
    public byte padding3;
}

/// <summary>
/// Camera device hotplug/permission event structure.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_CameraDeviceEvent
{
    public SDL_EventType type;
    public uint reserved;
    public ulong timestamp;
    public SDL_CameraID which;
}

/// <summary>
/// Keyboard IME candidates event structure.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct SDL_TextEditingCandidatesEvent
{
    public SDL_EventType type;
    public uint reserved;
    public ulong timestamp;
    public SDL_WindowID windowID;
    public byte** candidates;
    public int num_candidates;
    public int selected_candidate;
    public byte horizontal;
    public byte padding1;
    public byte padding2;
    public byte padding3;
}
```

- [ ] **Step 2: Add the three structs to the `SDL_Event` union**

In the `SDL_Event` explicit-layout struct, add after the existing `[FieldOffset(0)]` members:

```csharp
    [FieldOffset(0)] public SDL_AudioDeviceEvent adevice;
    [FieldOffset(0)] public SDL_CameraDeviceEvent cdevice;
    [FieldOffset(0)] public SDL_TextEditingCandidatesEvent editCandidates;
```

- [ ] **Step 3: Add the watch/filter/window/description bindings**

Inside the `Events` partial class:

```csharp
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_AddEventWatch")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_AddEventWatch(
        delegate* unmanaged[Cdecl]<void*, SDL_Event*, byte> filter, void* userdata);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_RemoveEventWatch")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_RemoveEventWatch(
        delegate* unmanaged[Cdecl]<void*, SDL_Event*, byte> filter, void* userdata);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetEventFilter")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_SetEventFilter(
        delegate* unmanaged[Cdecl]<void*, SDL_Event*, byte> filter, void* userdata);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_FilterEvents")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_FilterEvents(
        delegate* unmanaged[Cdecl]<void*, SDL_Event*, byte> filter, void* userdata);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetWindowFromEvent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Window* SDL_GetWindowFromEvent(SDL_Event* @event);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetEventDescription")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial int SDL_GetEventDescription(SDL_Event* @event, byte* buf, int buflen);
```

`SDL_Window*` is defined in `Native/Video.cs` (same `SdlSharp.Native` namespace) — no extra using needed inside the native layer. If the file lacks a reference, it resolves through the shared namespace.

- [ ] **Step 4: Add the skip comments**

Two SDL_events.h functions are documented skips. `SDL_GetEventFilter` is skipped rather than bound because a binding with no managed caller would be a native orphan (it fails the Task 13 orphan sweep), and the value it returns — a native function pointer + userdata — is only meaningful to whoever installed the filter. Add both comments near the bindings:

```csharp
    // Skipped SDL_PeepEvents (and its SDL_EventAction enum): bulk add/peek/get on a
    // caller-supplied SDL_Event array does not fit the transient-pointer RawEvent
    // model and would require a second managed event representation. The queue is
    // served by SDL_PollEvent/SDL_WaitEvent/SDL_PushEvent/SDL_Has*/SDL_Flush*.

    // Skipped SDL_GetEventFilter: it returns the currently-installed native filter
    // pointer + userdata, which is only meaningful to whoever installed it. The
    // managed Application owns the single filter slot via SetEventFilter, so a getter
    // would only expose an opaque native callback identity.
```

Net events accounting: of the 8 previously-unbound functions, 6 are bound (AddEventWatch, RemoveEventWatch, SetEventFilter, FilterEvents, GetWindowFromEvent, GetEventDescription) and 2 are documented skips (PeepEvents, GetEventFilter) → 18/20 bound.

- [ ] **Step 5: Build**

Run: `dotnet build SdlSharp.slnx`
Expected: 0 Warnings, 0 Errors.

- [ ] **Step 6: Commit**

```bash
git add src/SdlSharp/Native/Events.cs
git commit -m "Bind event watch/filter functions and add missing event structs"
```

---

## Task 7: Application queue operations + DispatchOne refactor

**Files:**
- Modify: `src/SdlSharp/Application.cs`

- [ ] **Step 1: Extract the per-event dispatch body into `DispatchOne`**

Refactor `DispatchEvents` so the `switch` body moves into a private `static bool DispatchOne(Native.SDL_Event* e)` that returns whether the event was a quit. `DispatchEvents` becomes the poll loop; `WaitDispatchEvent` (next step) reuses `DispatchOne`. Replace the current `DispatchEvents` method with:

```csharp
    /// <summary>
    /// Polls all pending events and dispatches them to the appropriate event handlers.
    /// Must be called on the main thread. Returns true if a quit event was received.
    /// </summary>
    /// <returns>True if a quit event was received.</returns>
    public static bool DispatchEvents()
    {
        Native.SDL_Event e;
        var gotQuit = false;

        while (SDL_PollEvent(&e))
        {
            if (DispatchOne(&e))
            {
                gotQuit = true;
            }
        }

        return gotQuit;
    }

    /// <summary>
    /// Waits for the next event (up to an optional timeout), then dispatches it through
    /// the same filter and typed-handler path as <see cref="DispatchEvents"/>.
    /// Must be called on the main thread.
    /// </summary>
    /// <param name="timeoutMilliseconds">Maximum time to wait, or -1 to wait indefinitely.</param>
    /// <returns>True if an event was dispatched, false if the wait timed out.</returns>
    public static bool WaitDispatchEvent(int timeoutMilliseconds = -1)
    {
        Native.SDL_Event e;
        var got = timeoutMilliseconds < 0
            ? SDL_WaitEvent(&e)
            : SDL_WaitEventTimeout(&e, timeoutMilliseconds);

        if (!got) return false;

        DispatchOne(&e);
        return true;
    }

    private static bool DispatchOne(Native.SDL_Event* ep)
    {
        ref var e = ref *ep;
        var gotQuit = false;

        RawEventFilter?.Invoke(new RawEvent((EventType)e.type, (nint)ep));

        switch ((Native.SDL_EventType)e.type)
        {
            case Native.SDL_EventType.SDL_EVENT_QUIT:
                gotQuit = true;
                Quit?.Invoke(new QuitEventArgs());
                break;

            case Native.SDL_EventType.SDL_EVENT_KEY_DOWN:
            case Native.SDL_EventType.SDL_EVENT_KEY_UP:
                var keyHandler = e.type == (uint)Native.SDL_EventType.SDL_EVENT_KEY_DOWN ? KeyDown : KeyUp;
                keyHandler?.Invoke(new KeyEventArgs(
                    e.key.windowID.Value,
                    (Scancode)e.key.scancode,
                    (Keycode)e.key.key,
                    (KeyModifiers)e.key.mod,
                    e.key.down != 0,
                    e.key.repeat != 0));
                break;

            case Native.SDL_EventType.SDL_EVENT_TEXT_INPUT:
                TextInput?.Invoke(new TextInputEventArgs(
                    e.text.windowID.Value,
                    Marshal.PtrToStringUTF8((nint)e.text.text)));
                break;

            case Native.SDL_EventType.SDL_EVENT_MOUSE_MOTION:
                MouseMotion?.Invoke(new MouseMotionEventArgs(
                    e.motion.windowID.Value,
                    e.motion.x, e.motion.y,
                    e.motion.xrel, e.motion.yrel,
                    e.motion.state));
                break;

            case Native.SDL_EventType.SDL_EVENT_MOUSE_BUTTON_DOWN:
            case Native.SDL_EventType.SDL_EVENT_MOUSE_BUTTON_UP:
                var btnHandler = e.type == (uint)Native.SDL_EventType.SDL_EVENT_MOUSE_BUTTON_DOWN ? MouseButtonDown : MouseButtonUp;
                btnHandler?.Invoke(new MouseButtonEventArgs(
                    e.button.windowID.Value,
                    (MouseButton)e.button.button,
                    e.button.down != 0,
                    e.button.clicks,
                    e.button.x, e.button.y));
                break;

            case Native.SDL_EventType.SDL_EVENT_MOUSE_WHEEL:
                MouseWheel?.Invoke(new MouseWheelEventArgs(
                    e.wheel.windowID.Value,
                    e.wheel.x, e.wheel.y,
                    e.wheel.mouse_x, e.wheel.mouse_y,
                    (MouseWheelDirection)e.wheel.direction));
                break;

            case >= Native.SDL_EventType.SDL_EVENT_WINDOW_FIRST and <= Native.SDL_EventType.SDL_EVENT_WINDOW_LAST:
                Window?.Invoke(new WindowEventArgs(
                    e.window.windowID.Value,
                    e.window.data1, e.window.data2));
                break;

            default:
                DispatchExtended(ref e);
                break;
        }

        return gotQuit;
    }
```

`DispatchExtended` is filled in by Task 10 (the remaining families). For this task, add a real but empty method body so the file compiles — do NOT use a `partial` method (a partial method with an explicit `private` accessibility modifier requires an implementing declaration, so a bare stub would not compile, and the class does not otherwise need to be `partial`):

```csharp
    // Filled in by Task 10 with the remaining typed event families.
    private static void DispatchExtended(ref Native.SDL_Event e)
    {
    }
```

Task 10 replaces this empty body with the full switch (it edits the body in place; the signature stays identical). The class stays `public sealed unsafe class Application` — no `partial` needed.

- [ ] **Step 2: Add the queue-state query/flush/enable methods**

Add to `Application`:

```csharp
    /// <summary>Gets whether at least one event of the given type is queued.</summary>
    /// <param name="type">The event type.</param>
    public static bool HasEvent(EventType type) => SDL_HasEvent((uint)type);

    /// <summary>Gets whether at least one event in the inclusive type range is queued.</summary>
    /// <param name="minType">The lowest event type.</param>
    /// <param name="maxType">The highest event type.</param>
    public static bool HasEvents(EventType minType, EventType maxType) =>
        SDL_HasEvents((uint)minType, (uint)maxType);

    /// <summary>Removes all queued events of the given type.</summary>
    /// <param name="type">The event type to flush.</param>
    public static void FlushEvent(EventType type) => SDL_FlushEvent((uint)type);

    /// <summary>Removes all queued events in the inclusive type range.</summary>
    /// <param name="minType">The lowest event type.</param>
    /// <param name="maxType">The highest event type.</param>
    public static void FlushEvents(EventType minType, EventType maxType) =>
        SDL_FlushEvents((uint)minType, (uint)maxType);

    /// <summary>Enables or disables processing of the given event type.</summary>
    /// <param name="type">The event type.</param>
    /// <param name="enabled">True to enable, false to disable.</param>
    public static void SetEventEnabled(EventType type, bool enabled) =>
        SDL_SetEventEnabled((uint)type, enabled);

    /// <summary>Gets whether the given event type is currently enabled.</summary>
    /// <param name="type">The event type.</param>
    public static bool IsEventEnabled(EventType type) => SDL_EventEnabled((uint)type);
```

Verify the native signatures exist (they do per exploration): `SDL_HasEvent(uint)`, `SDL_HasEvents(uint,uint)`, `SDL_FlushEvent(uint)`, `SDL_FlushEvents(uint,uint)`, `SDL_SetEventEnabled(uint,bool)`, `SDL_EventEnabled(uint)`. If `SDL_HasEvents` or `SDL_FlushEvent` (singular) is missing from `Native/Events.cs`, add the binding following the existing pattern:

```csharp
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_HasEvents")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HasEvents(uint minType, uint maxType);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_FlushEvent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_FlushEvent(uint type);
```

- [ ] **Step 3: Build**

Run: `dotnet build SdlSharp.slnx`
Expected: 0 Warnings, 0 Errors. `DispatchExtended` compiles as an empty partial (returns nothing, no cases yet).

- [ ] **Step 4: Commit**

```bash
git add src/SdlSharp/Application.cs src/SdlSharp/Native/Events.cs
git commit -m "Add event queue operations and refactor dispatch into DispatchOne"
```

---

## Task 8: Custom user events (RegisterEvents, PushUserEvent, UserEvent)

**Files:**
- Modify: `src/SdlSharp/Application.cs`
- Modify: `src/SdlSharp/Input/EventArgs.cs`

- [ ] **Step 1: Add `UserEventArgs`**

Append to `src/SdlSharp/Input/EventArgs.cs`:

```csharp
/// <summary>
/// Event data for user-defined events (types at or above <see cref="EventType.User"/>).
/// </summary>
/// <param name="Type">The registered event type.</param>
/// <param name="WindowId">The associated window ID, or 0 if none.</param>
/// <param name="Code">The user-defined event code.</param>
/// <param name="Data1">The first user-defined data pointer; the caller owns any pointed-to memory.</param>
/// <param name="Data2">The second user-defined data pointer; the caller owns any pointed-to memory.</param>
public readonly record struct UserEventArgs(
    EventType Type,
    uint WindowId,
    int Code,
    nint Data1,
    nint Data2);
```

- [ ] **Step 2: Add the `UserEvent` event, `RegisterEvents`, and `PushUserEvent`**

Add to `Application` (place the `UserEvent` declaration with the other `public static event` declarations):

```csharp
    /// <summary>Raised for user-defined events (see <see cref="RegisterEvents"/>).</summary>
    public static event Action<UserEventArgs>? UserEvent;

    /// <summary>
    /// Allocates a contiguous block of user event types for use with <see cref="PushUserEvent"/>.
    /// </summary>
    /// <param name="count">The number of event types to allocate.</param>
    /// <returns>The first allocated event type; subsequent types are consecutive.</returns>
    /// <exception cref="SdlException">No more user event types are available.</exception>
    public static EventType RegisterEvents(int count)
    {
        var first = SDL_RegisterEvents(count);
        if (first == 0)
        {
            throw new SdlException();
        }

        return (EventType)first;
    }

    /// <summary>
    /// Pushes a user-defined event onto the queue. Register the type with
    /// <see cref="RegisterEvents"/> first. Any memory referenced by <paramref name="data1"/>
    /// or <paramref name="data2"/> is owned by the caller and must outlive the event's dispatch.
    /// </summary>
    /// <param name="type">A type obtained from <see cref="RegisterEvents"/>.</param>
    /// <param name="code">A user-defined code.</param>
    /// <param name="data1">A user-defined pointer, or 0.</param>
    /// <param name="data2">A user-defined pointer, or 0.</param>
    /// <returns>True if the event was queued; false if a filter dropped it.</returns>
    public static bool PushUserEvent(EventType type, int code, nint data1 = 0, nint data2 = 0)
    {
        var e = new Native.SDL_Event
        {
            user = new Native.SDL_UserEvent
            {
                type = (uint)type,
                code = code,
                data1 = (void*)data1,
                data2 = (void*)data2,
            }
        };

        return SDL_PushEvent(&e);
    }
```

Note on the `false` return: `SDL_PushEvent` returns false both for a filter drop and for a genuine error; SDL sets an error message only in the latter. The design treats `false` as "not queued" without distinguishing, which matches the documented behavior. (`SDL_PushEvent` is already bound.)

- [ ] **Step 3: Dispatch `UserEvent` in `DispatchOne`**

In the `switch` inside `DispatchOne` (Task 7), the `default` branch calls `DispatchExtended`. Add the user-event case *before* the default, so registered user types dispatch directly:

```csharp
            case >= Native.SDL_EventType.SDL_EVENT_USER:
                UserEvent?.Invoke(new UserEventArgs(
                    (EventType)e.type,
                    e.user.windowID.Value,
                    e.user.code,
                    (nint)e.user.data1,
                    (nint)e.user.data2));
                break;
```

- [ ] **Step 4: Build**

Run: `dotnet build SdlSharp.slnx`
Expected: 0 Warnings, 0 Errors.

- [ ] **Step 5: Commit**

```bash
git add src/SdlSharp/Application.cs src/SdlSharp/Input/EventArgs.cs
git commit -m "Add custom user events: RegisterEvents, PushUserEvent, UserEvent dispatch"
```

---

## Task 9: Event watches, filters, and RawEvent.Window/Description

**Files:**
- Modify: `src/SdlSharp/Application.cs`
- Modify: `src/SdlSharp/Input/RawEvent.cs`

- [ ] **Step 1: Add `RawEventPredicate` delegate + watch/filter trampolines and registry**

Add to `Application`. The registry maps each managed delegate to its GCHandle so `RemoveEventWatch` can pass the same `(callback, userdata)` pair SDL was given:

```csharp
    /// <summary>
    /// A predicate applied to a raw event at queue-insertion time (see
    /// <see cref="SetEventFilter"/> and <see cref="FilterEvents"/>). Return true to keep
    /// the event, false to drop it. The event's <see cref="RawEvent.Pointer"/> is only
    /// valid during the call.
    /// </summary>
    /// <param name="e">The raw event.</param>
    /// <returns>True to keep the event, false to drop it.</returns>
    public delegate bool RawEventPredicate(in RawEvent e);

    private static readonly Dictionary<RawEventHandler, GCHandle> WatchHandles = new();
    private static GCHandle _eventFilterHandle;

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static byte WatchTrampoline(void* userdata, Native.SDL_Event* e)
    {
        try
        {
            var handler = (RawEventHandler)GCHandle.FromIntPtr((nint)userdata).Target!;
            handler(new RawEvent((EventType)e->type, (nint)e));
        }
        catch
        {
            // A throwing watch must not drop the event or cross the boundary.
        }

        return 1; // Watches ignore the return value; keep the event regardless.
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static byte FilterTrampoline(void* userdata, Native.SDL_Event* e)
    {
        try
        {
            var predicate = (RawEventPredicate)GCHandle.FromIntPtr((nint)userdata).Target!;
            return predicate(new RawEvent((EventType)e->type, (nint)e)) ? (byte)1 : (byte)0;
        }
        catch
        {
            return 1; // On error keep the event rather than silently dropping it.
        }
    }
```

- [ ] **Step 2: Add `AddEventWatch` / `RemoveEventWatch`**

```csharp
    /// <summary>
    /// Registers a callback invoked for every event as it is added to the queue. Watches
    /// may fire from the thread that pushes the event, including during
    /// <see cref="PumpEvents"/> or from SDL-internal threads. The same handler cannot be
    /// added twice.
    /// </summary>
    /// <param name="watch">The watch callback.</param>
    /// <exception cref="ArgumentException">The handler is already registered.</exception>
    public static void AddEventWatch(RawEventHandler watch)
    {
        lock (WatchHandles)
        {
            if (WatchHandles.ContainsKey(watch))
            {
                throw new ArgumentException("This handler is already registered as an event watch.", nameof(watch));
            }

            var handle = GCHandle.Alloc(watch);
            if (!SDL_AddEventWatch(&WatchTrampoline, (void*)GCHandle.ToIntPtr(handle)))
            {
                handle.Free();
                throw new SdlException();
            }

            WatchHandles[watch] = handle;
        }
    }

    /// <summary>
    /// Removes a watch previously registered with <see cref="AddEventWatch"/>. Does nothing
    /// if the handler was not registered.
    /// </summary>
    /// <param name="watch">The watch callback to remove.</param>
    public static void RemoveEventWatch(RawEventHandler watch)
    {
        lock (WatchHandles)
        {
            if (!WatchHandles.TryGetValue(watch, out var handle))
            {
                return;
            }

            SDL_RemoveEventWatch(&WatchTrampoline, (void*)GCHandle.ToIntPtr(handle));
            handle.Free();
            WatchHandles.Remove(watch);
        }
    }
```

- [ ] **Step 3: Add `SetEventFilter` and `FilterEvents`**

```csharp
    /// <summary>
    /// Sets or clears (<c>null</c>) the event filter. Unlike <see cref="RawEventFilter"/>
    /// (which observes events during dispatch on the main loop), this filter runs at the
    /// moment an event is added to the queue — possibly on another thread — and dropping
    /// an event here prevents it from ever being queued. Only one filter is installed at a
    /// time; setting a new one replaces the previous.
    /// </summary>
    /// <param name="filter">The filter predicate, or <c>null</c> to clear.</param>
    public static void SetEventFilter(RawEventPredicate? filter)
    {
        var previous = _eventFilterHandle;

        if (filter == null)
        {
            SDL_SetEventFilter(null, null);
            _eventFilterHandle = default;
        }
        else
        {
            var handle = GCHandle.Alloc(filter);
            SDL_SetEventFilter(&FilterTrampoline, (void*)GCHandle.ToIntPtr(handle));
            _eventFilterHandle = handle;
        }

        if (previous.IsAllocated)
        {
            previous.Free();
        }
    }

    /// <summary>
    /// Runs a predicate over every event currently in the queue, removing those for which
    /// it returns false. This is a one-shot sweep and does not install a persistent filter.
    /// </summary>
    /// <param name="predicate">The predicate; return false to remove an event.</param>
    public static void FilterEvents(RawEventPredicate predicate)
    {
        var handle = GCHandle.Alloc(predicate);
        try
        {
            SDL_FilterEvents(&FilterTrampoline, (void*)GCHandle.ToIntPtr(handle));
        }
        finally
        {
            handle.Free();
        }
    }
```

- [ ] **Step 4: Add `Window` and `Description` to `RawEvent`**

`RawEvent` is a `readonly record struct` with just `Type` and `Pointer`. Add computed properties that read through `Pointer`. Rewrite `src/SdlSharp/Input/RawEvent.cs`:

```csharp
using System.Runtime.InteropServices;

using static SdlSharp.Native.Events;

namespace SdlSharp.Input;

/// <summary>
/// A raw SDL event passed to <see cref="Application.RawEventFilter"/> before typed dispatch.
/// </summary>
/// <param name="Type">The event type.</param>
/// <param name="Pointer">
/// The address of the native SDL_Event. Intended for passing to external
/// backends (for example the Dear ImGui SDL3 platform backend). The pointer
/// targets a stack-local SDL_Event owned by <see cref="Application.DispatchEvents"/>,
/// which is overwritten when the next event is polled and is gone once dispatch
/// returns. Storing the pointer and dereferencing it later reads reused or freed
/// stack memory — use it only for the duration of the callback.
/// </param>
public readonly record struct RawEvent(EventType Type, nint Pointer)
{
    /// <summary>
    /// Gets the window associated with this event, or <c>null</c> if it has none. The
    /// returned wrapper does not own the window; each access returns a new wrapper and
    /// wrappers are not equal to each other. Only valid while <see cref="Pointer"/> is.
    /// </summary>
    public unsafe Graphics.Window? Window
    {
        get
        {
            var window = SDL_GetWindowFromEvent((Native.SDL_Event*)Pointer);
            return window == null ? null : new Graphics.Window(window, ownsHandle: false);
        }
    }

    /// <summary>
    /// Gets a human-readable description of this event, for logging and debugging. Only
    /// valid while <see cref="Pointer"/> is.
    /// </summary>
    public unsafe string Description
    {
        get
        {
            var e = (Native.SDL_Event*)Pointer;
            var length = SDL_GetEventDescription(e, null, 0);
            if (length <= 0) return string.Empty;

            Span<byte> buffer = length < 256 ? stackalloc byte[length + 1] : new byte[length + 1];
            fixed (byte* buf = buffer)
            {
                var written = SDL_GetEventDescription(e, buf, buffer.Length);
                return written <= 0 ? string.Empty : Marshal.PtrToStringUTF8((nint)buf) ?? string.Empty;
            }
        }
    }
}
```

Confirm the `Graphics.Window` constructor `internal Window(SDL_Window* handle, bool ownsHandle)` is accessible from `SdlSharp.Input` — it is `internal` in the same assembly, so yes. Confirm `SDL_GetEventDescription`'s length contract: it returns the number of bytes the description needs (excluding the null terminator), per the SDL header; passing `null, 0` queries the length. If SDL instead returns the written count, the two-call pattern still works because the second call passes a buffer sized to the queried length + 1.

- [ ] **Step 5: Free watch/filter handles on shutdown**

In `Application.Dispose`, before/after `SDL_Quit()`, release any dangling handles so a re-init doesn't leak (defensive; process usually exits anyway):

```csharp
    /// <inheritdoc/>
    public void Dispose()
    {
        SDL_SetEventFilter(null, null);
        if (_eventFilterHandle.IsAllocated) _eventFilterHandle.Free();
        _eventFilterHandle = default;

        lock (WatchHandles)
        {
            foreach (var (handler, handle) in WatchHandles)
            {
                SDL_RemoveEventWatch(&WatchTrampoline, (void*)GCHandle.ToIntPtr(handle));
                handle.Free();
            }

            WatchHandles.Clear();
        }

        SDL_Quit();
    }
```

- [ ] **Step 6: Build**

Run: `dotnet build SdlSharp.slnx`
Expected: 0 Warnings, 0 Errors.

- [ ] **Step 7: Commit**

```bash
git add src/SdlSharp/Application.cs src/SdlSharp/Input/RawEvent.cs
git commit -m "Add event watches, insertion-time filter, and RawEvent Window/Description"
```

---

## Task 10: Typed dispatch for all remaining event families

**Files:**
- Create: `src/SdlSharp/Input/DeviceEventArgs.cs`
- Create: `src/SdlSharp/Input/JoystickEventArgs.cs`
- Create: `src/SdlSharp/Input/GamepadEventArgs.cs`
- Create: `src/SdlSharp/Input/TouchEventArgs.cs`
- Create: `src/SdlSharp/Input/PenEventArgs.cs`
- Create: `src/SdlSharp/Input/DropEventArgs.cs`
- Create: `src/SdlSharp/Input/SensorEventArgs.cs`
- Create: `src/SdlSharp/Input/DisplayEventArgs.cs`
- Modify: `src/SdlSharp/Application.cs`

This task adds the remaining `readonly record struct` args, the `Application` events, and the `DispatchExtended` implementation. All event structs are in `Native/Events.cs` (explored above). Ids stay raw `uint`; enums map to existing public enums (`GamepadAxis`, `GamepadButton`, `HatPosition`, `SensorType`, `PenAxis`).

- [ ] **Step 1: Create device + display + text-editing args**

`src/SdlSharp/Input/DeviceEventArgs.cs`:

```csharp
namespace SdlSharp.Input;

/// <summary>Event data for keyboard/mouse device add/remove events.</summary>
/// <param name="Which">The instance ID of the device.</param>
public readonly record struct InputDeviceEventArgs(uint Which);

/// <summary>Event data for text-editing (IME composition) events.</summary>
/// <param name="WindowId">The window with keyboard focus.</param>
/// <param name="Text">The current composition text.</param>
/// <param name="Start">The start cursor position, in UTF-8 bytes.</param>
/// <param name="Length">The selection length, in UTF-8 bytes.</param>
public readonly record struct TextEditingEventArgs(uint WindowId, string? Text, int Start, int Length);

/// <summary>Event data for IME candidate-list events.</summary>
/// <param name="WindowId">The window with keyboard focus.</param>
/// <param name="Candidates">The candidate strings.</param>
/// <param name="SelectedCandidate">The index of the selected candidate, or -1 if none.</param>
/// <param name="Horizontal">True if the candidate list is horizontal.</param>
public readonly record struct TextEditingCandidatesEventArgs(
    uint WindowId, string?[] Candidates, int SelectedCandidate, bool Horizontal);
```

`src/SdlSharp/Input/DisplayEventArgs.cs`:

```csharp
namespace SdlSharp.Input;

/// <summary>Event data for display events (orientation, add/remove, moved, etc.).</summary>
/// <param name="Type">The specific display event type.</param>
/// <param name="DisplayId">The display instance ID.</param>
/// <param name="Data1">Event-dependent data.</param>
/// <param name="Data2">Event-dependent data.</param>
public readonly record struct DisplayEventArgs(EventType Type, uint DisplayId, int Data1, int Data2);
```

- [ ] **Step 2: Create joystick args**

`src/SdlSharp/Input/JoystickEventArgs.cs`:

```csharp
namespace SdlSharp.Input;

/// <summary>Event data for joystick axis motion.</summary>
/// <param name="Which">The joystick instance ID.</param>
/// <param name="Axis">The axis index.</param>
/// <param name="Value">The axis value (-32768 to 32767).</param>
public readonly record struct JoyAxisEventArgs(uint Which, byte Axis, short Value);

/// <summary>Event data for joystick trackball motion.</summary>
/// <param name="Which">The joystick instance ID.</param>
/// <param name="Ball">The trackball index.</param>
/// <param name="RelativeX">Relative X motion.</param>
/// <param name="RelativeY">Relative Y motion.</param>
public readonly record struct JoyBallEventArgs(uint Which, byte Ball, short RelativeX, short RelativeY);

/// <summary>Event data for joystick hat motion.</summary>
/// <param name="Which">The joystick instance ID.</param>
/// <param name="Hat">The hat index.</param>
/// <param name="Value">The hat position.</param>
public readonly record struct JoyHatEventArgs(uint Which, byte Hat, HatPosition Value);

/// <summary>Event data for joystick button events.</summary>
/// <param name="Which">The joystick instance ID.</param>
/// <param name="Button">The button index.</param>
/// <param name="IsDown">True if pressed.</param>
public readonly record struct JoyButtonEventArgs(uint Which, byte Button, bool IsDown);

/// <summary>Event data for joystick device add/remove/update events.</summary>
/// <param name="Which">The joystick instance ID.</param>
public readonly record struct JoyDeviceEventArgs(uint Which);

/// <summary>Event data for joystick battery-level changes.</summary>
/// <param name="Which">The joystick instance ID.</param>
/// <param name="State">The power state.</param>
/// <param name="Percent">The battery percentage (0-100), or -1 if unknown.</param>
public readonly record struct JoyBatteryEventArgs(uint Which, PowerState State, int Percent);
```

- [ ] **Step 3: Create gamepad args**

`src/SdlSharp/Input/GamepadEventArgs.cs`:

```csharp
namespace SdlSharp.Input;

/// <summary>Event data for gamepad axis motion.</summary>
/// <param name="Which">The joystick instance ID.</param>
/// <param name="Axis">The gamepad axis.</param>
/// <param name="Value">The axis value (-32768 to 32767).</param>
public readonly record struct GamepadAxisEventArgs(uint Which, GamepadAxis Axis, short Value);

/// <summary>Event data for gamepad button events.</summary>
/// <param name="Which">The joystick instance ID.</param>
/// <param name="Button">The gamepad button.</param>
/// <param name="IsDown">True if pressed.</param>
public readonly record struct GamepadButtonEventArgs(uint Which, GamepadButton Button, bool IsDown);

/// <summary>Event data for gamepad device add/remove/remap/update events.</summary>
/// <param name="Which">The joystick instance ID.</param>
public readonly record struct GamepadDeviceEventArgs(uint Which);

/// <summary>Event data for gamepad touchpad events.</summary>
/// <param name="Which">The joystick instance ID.</param>
/// <param name="Touchpad">The touchpad index.</param>
/// <param name="Finger">The finger index.</param>
/// <param name="X">Normalized X (0-1).</param>
/// <param name="Y">Normalized Y (0-1).</param>
/// <param name="Pressure">Normalized pressure (0-1).</param>
public readonly record struct GamepadTouchpadEventArgs(
    uint Which, int Touchpad, int Finger, float X, float Y, float Pressure);

/// <summary>Event data for gamepad sensor updates.</summary>
/// <param name="Which">The joystick instance ID.</param>
/// <param name="Sensor">The sensor type as reported by SDL.</param>
/// <param name="Data1">First sensor value.</param>
/// <param name="Data2">Second sensor value.</param>
/// <param name="Data3">Third sensor value.</param>
/// <param name="SensorTimestamp">The sensor reading timestamp, in nanoseconds.</param>
public readonly record struct GamepadSensorEventArgs(
    uint Which, int Sensor, float Data1, float Data2, float Data3, ulong SensorTimestamp);
```

- [ ] **Step 4: Create touch + pen + drop + sensor args**

`src/SdlSharp/Input/TouchEventArgs.cs`:

```csharp
namespace SdlSharp.Input;

/// <summary>Event data for touch finger events.</summary>
/// <param name="TouchId">The touch device ID.</param>
/// <param name="FingerId">The finger ID.</param>
/// <param name="X">Normalized X (0-1).</param>
/// <param name="Y">Normalized Y (0-1).</param>
/// <param name="DeltaX">Normalized X motion.</param>
/// <param name="DeltaY">Normalized Y motion.</param>
/// <param name="Pressure">Normalized pressure (0-1).</param>
/// <param name="WindowId">The window under the touch.</param>
public readonly record struct TouchFingerEventArgs(
    ulong TouchId, ulong FingerId, float X, float Y, float DeltaX, float DeltaY, float Pressure, uint WindowId);

/// <summary>Event data for pinch gesture events.</summary>
/// <param name="Scale">The relative pinch scale factor.</param>
/// <param name="WindowId">The window under the gesture.</param>
public readonly record struct PinchFingerEventArgs(float Scale, uint WindowId);
```

`src/SdlSharp/Input/PenEventArgs.cs`:

```csharp
namespace SdlSharp.Input;

/// <summary>Event data for pen proximity (enter/leave) events.</summary>
/// <param name="WindowId">The window with pen focus.</param>
/// <param name="Which">The pen instance ID.</param>
public readonly record struct PenProximityEventArgs(uint WindowId, uint Which);

/// <summary>Event data for pen motion events.</summary>
/// <param name="WindowId">The window with pen focus.</param>
/// <param name="Which">The pen instance ID.</param>
/// <param name="X">X coordinate relative to the window.</param>
/// <param name="Y">Y coordinate relative to the window.</param>
public readonly record struct PenMotionEventArgs(uint WindowId, uint Which, float X, float Y);

/// <summary>Event data for pen touch (down/up) events.</summary>
/// <param name="WindowId">The window with pen focus.</param>
/// <param name="Which">The pen instance ID.</param>
/// <param name="X">X coordinate relative to the window.</param>
/// <param name="Y">Y coordinate relative to the window.</param>
/// <param name="IsEraser">True if the eraser end is used.</param>
/// <param name="IsDown">True if the pen is touching.</param>
public readonly record struct PenTouchEventArgs(uint WindowId, uint Which, float X, float Y, bool IsEraser, bool IsDown);

/// <summary>Event data for pen button events.</summary>
/// <param name="WindowId">The window with pen focus.</param>
/// <param name="Which">The pen instance ID.</param>
/// <param name="X">X coordinate relative to the window.</param>
/// <param name="Y">Y coordinate relative to the window.</param>
/// <param name="Button">The pen button index.</param>
/// <param name="IsDown">True if pressed.</param>
public readonly record struct PenButtonEventArgs(uint WindowId, uint Which, float X, float Y, byte Button, bool IsDown);

/// <summary>Event data for pen axis events.</summary>
/// <param name="WindowId">The window with pen focus.</param>
/// <param name="Which">The pen instance ID.</param>
/// <param name="X">X coordinate relative to the window.</param>
/// <param name="Y">Y coordinate relative to the window.</param>
/// <param name="Axis">The axis that changed.</param>
/// <param name="Value">The new axis value.</param>
public readonly record struct PenAxisEventArgs(uint WindowId, uint Which, float X, float Y, PenAxis Axis, float Value);
```

`src/SdlSharp/Input/DropEventArgs.cs`:

```csharp
namespace SdlSharp.Input;

/// <summary>Event data for drag-and-drop events.</summary>
/// <param name="Type">The specific drop event type.</param>
/// <param name="WindowId">The window the drop targets.</param>
/// <param name="X">Drop X coordinate relative to the window (for position/complete).</param>
/// <param name="Y">Drop Y coordinate relative to the window (for position/complete).</param>
/// <param name="Source">The drag source application, or null.</param>
/// <param name="Data">The dropped file path or text, or null (for file/text events).</param>
public readonly record struct DropEventArgs(
    EventType Type, uint WindowId, float X, float Y, string? Source, string? Data);
```

`src/SdlSharp/Input/SensorEventArgs.cs`:

```csharp
namespace SdlSharp.Input;

/// <summary>Event data for sensor updates.</summary>
/// <param name="Which">The sensor instance ID.</param>
/// <param name="Data">Up to six sensor values.</param>
/// <param name="SensorTimestamp">The sensor reading timestamp, in nanoseconds.</param>
public readonly record struct SensorEventArgs(uint Which, float[] Data, ulong SensorTimestamp);

/// <summary>Event data for audio-device hotplug events.</summary>
/// <param name="Type">The specific audio device event type.</param>
/// <param name="Which">The audio device ID.</param>
/// <param name="IsRecording">True if a recording device, false if playback.</param>
public readonly record struct AudioDeviceEventArgs(EventType Type, uint Which, bool IsRecording);

/// <summary>Event data for camera-device hotplug/permission events.</summary>
/// <param name="Type">The specific camera device event type.</param>
/// <param name="Which">The camera device ID.</param>
public readonly record struct CameraDeviceEventArgs(EventType Type, uint Which);

/// <summary>Event data for render-target/device reset/lost events.</summary>
/// <param name="Type">The specific render event type.</param>
/// <param name="WindowId">The window whose renderer is affected.</param>
public readonly record struct RenderEventArgs(EventType Type, uint WindowId);

/// <summary>Event data for clipboard-update events.</summary>
/// <param name="OwnerIsSelf">True if this application owns the clipboard.</param>
/// <param name="MimeTypes">The available MIME types.</param>
public readonly record struct ClipboardEventArgs(bool OwnerIsSelf, string?[] MimeTypes);
```

- [ ] **Step 5: Declare the `Application` events for every family**

Add with the other `public static event` declarations in `Application`:

```csharp
    /// <summary>Raised for display events (orientation, connect/disconnect, moved, etc.).</summary>
    public static event Action<DisplayEventArgs>? Display;

    /// <summary>Raised when a keyboard is connected.</summary>
    public static event Action<InputDeviceEventArgs>? KeyboardAdded;
    /// <summary>Raised when a keyboard is disconnected.</summary>
    public static event Action<InputDeviceEventArgs>? KeyboardRemoved;
    /// <summary>Raised when a mouse is connected.</summary>
    public static event Action<InputDeviceEventArgs>? MouseAdded;
    /// <summary>Raised when a mouse is disconnected.</summary>
    public static event Action<InputDeviceEventArgs>? MouseRemoved;

    /// <summary>Raised while composing text through an IME.</summary>
    public static event Action<TextEditingEventArgs>? TextEditing;
    /// <summary>Raised when the IME candidate list changes.</summary>
    public static event Action<TextEditingCandidatesEventArgs>? TextEditingCandidates;

    /// <summary>Raised on joystick axis motion.</summary>
    public static event Action<JoyAxisEventArgs>? JoystickAxisMotion;
    /// <summary>Raised on joystick trackball motion.</summary>
    public static event Action<JoyBallEventArgs>? JoystickBallMotion;
    /// <summary>Raised on joystick hat motion.</summary>
    public static event Action<JoyHatEventArgs>? JoystickHatMotion;
    /// <summary>Raised when a joystick button is pressed.</summary>
    public static event Action<JoyButtonEventArgs>? JoystickButtonDown;
    /// <summary>Raised when a joystick button is released.</summary>
    public static event Action<JoyButtonEventArgs>? JoystickButtonUp;
    /// <summary>Raised when a joystick is connected.</summary>
    public static event Action<JoyDeviceEventArgs>? JoystickAdded;
    /// <summary>Raised when a joystick is disconnected.</summary>
    public static event Action<JoyDeviceEventArgs>? JoystickRemoved;
    /// <summary>Raised when a joystick finishes an update cycle.</summary>
    public static event Action<JoyDeviceEventArgs>? JoystickUpdateComplete;
    /// <summary>Raised when a joystick battery level changes.</summary>
    public static event Action<JoyBatteryEventArgs>? JoystickBatteryUpdated;

    /// <summary>Raised on gamepad axis motion.</summary>
    public static event Action<GamepadAxisEventArgs>? GamepadAxisMotion;
    /// <summary>Raised when a gamepad button is pressed.</summary>
    public static event Action<GamepadButtonEventArgs>? GamepadButtonDown;
    /// <summary>Raised when a gamepad button is released.</summary>
    public static event Action<GamepadButtonEventArgs>? GamepadButtonUp;
    /// <summary>Raised when a gamepad is connected.</summary>
    public static event Action<GamepadDeviceEventArgs>? GamepadAdded;
    /// <summary>Raised when a gamepad is disconnected.</summary>
    public static event Action<GamepadDeviceEventArgs>? GamepadRemoved;
    /// <summary>Raised when a gamepad mapping is updated.</summary>
    public static event Action<GamepadDeviceEventArgs>? GamepadRemapped;
    /// <summary>Raised when a gamepad finishes an update cycle.</summary>
    public static event Action<GamepadDeviceEventArgs>? GamepadUpdateComplete;
    /// <summary>Raised when a gamepad's Steam handle changes.</summary>
    public static event Action<GamepadDeviceEventArgs>? GamepadSteamHandleUpdated;
    /// <summary>Raised when a gamepad touchpad finger touches down.</summary>
    public static event Action<GamepadTouchpadEventArgs>? GamepadTouchpadDown;
    /// <summary>Raised when a gamepad touchpad finger moves.</summary>
    public static event Action<GamepadTouchpadEventArgs>? GamepadTouchpadMotion;
    /// <summary>Raised when a gamepad touchpad finger lifts.</summary>
    public static event Action<GamepadTouchpadEventArgs>? GamepadTouchpadUp;
    /// <summary>Raised on a gamepad sensor update.</summary>
    public static event Action<GamepadSensorEventArgs>? GamepadSensorUpdate;

    /// <summary>Raised when a finger touches down.</summary>
    public static event Action<TouchFingerEventArgs>? FingerDown;
    /// <summary>Raised when a finger lifts.</summary>
    public static event Action<TouchFingerEventArgs>? FingerUp;
    /// <summary>Raised when a finger moves.</summary>
    public static event Action<TouchFingerEventArgs>? FingerMotion;
    /// <summary>Raised when a finger touch is canceled.</summary>
    public static event Action<TouchFingerEventArgs>? FingerCanceled;
    /// <summary>Raised when a pinch gesture begins.</summary>
    public static event Action<PinchFingerEventArgs>? PinchBegin;
    /// <summary>Raised as a pinch gesture updates.</summary>
    public static event Action<PinchFingerEventArgs>? PinchUpdate;
    /// <summary>Raised when a pinch gesture ends.</summary>
    public static event Action<PinchFingerEventArgs>? PinchEnd;

    /// <summary>Raised when a pen enters proximity.</summary>
    public static event Action<PenProximityEventArgs>? PenProximityIn;
    /// <summary>Raised when a pen leaves proximity.</summary>
    public static event Action<PenProximityEventArgs>? PenProximityOut;
    /// <summary>Raised when a pen touches down.</summary>
    public static event Action<PenTouchEventArgs>? PenDown;
    /// <summary>Raised when a pen lifts.</summary>
    public static event Action<PenTouchEventArgs>? PenUp;
    /// <summary>Raised when a pen moves.</summary>
    public static event Action<PenMotionEventArgs>? PenMotion;
    /// <summary>Raised when a pen button is pressed.</summary>
    public static event Action<PenButtonEventArgs>? PenButtonDown;
    /// <summary>Raised when a pen button is released.</summary>
    public static event Action<PenButtonEventArgs>? PenButtonUp;
    /// <summary>Raised on a pen axis change.</summary>
    public static event Action<PenAxisEventArgs>? PenAxisMotion;

    /// <summary>Raised for each drop event (begin, file, text, position, complete).</summary>
    public static event Action<DropEventArgs>? Drop;

    /// <summary>Raised when the clipboard contents change.</summary>
    public static event Action<ClipboardEventArgs>? ClipboardUpdate;

    /// <summary>Raised on a sensor update.</summary>
    public static event Action<SensorEventArgs>? SensorUpdate;

    /// <summary>Raised on audio-device hotplug (added, removed, format changed).</summary>
    public static event Action<AudioDeviceEventArgs>? AudioDeviceEvent;

    /// <summary>Raised on camera-device hotplug/permission changes.</summary>
    public static event Action<CameraDeviceEventArgs>? CameraDeviceEvent;

    /// <summary>Raised on render target/device reset or loss.</summary>
    public static event Action<RenderEventArgs>? RenderEvent;
```

- [ ] **Step 6: Implement `DispatchExtended`**

Replace the empty `DispatchExtended` body added in Task 7 with the full switch below (keep the exact same signature `private static void DispatchExtended(ref Native.SDL_Event e)` — it is NOT a partial method). Write out `Native.SDL_EventType` in full (no alias). Note pen `pmotion`/`ptouch`/`pbutton`/`paxis`, gamepad `gsensor.data` fixed[3], sensor `data` fixed[6], drop strings via `Marshal.PtrToStringUTF8`, clipboard/candidate string arrays via a small local helper:

```csharp
    private static void DispatchExtended(ref Native.SDL_Event e)
    {
        switch ((Native.SDL_EventType)e.type)
        {
            case >= Native.SDL_EventType.SDL_EVENT_DISPLAY_FIRST and <= Native.SDL_EventType.SDL_EVENT_DISPLAY_LAST:
                Display?.Invoke(new DisplayEventArgs(
                    (EventType)e.type, e.display.displayID.Value, e.display.data1, e.display.data2));
                break;

            case Native.SDL_EventType.SDL_EVENT_KEYBOARD_ADDED:
                KeyboardAdded?.Invoke(new InputDeviceEventArgs(e.kdevice.which));
                break;
            case Native.SDL_EventType.SDL_EVENT_KEYBOARD_REMOVED:
                KeyboardRemoved?.Invoke(new InputDeviceEventArgs(e.kdevice.which));
                break;
            case Native.SDL_EventType.SDL_EVENT_MOUSE_ADDED:
                MouseAdded?.Invoke(new InputDeviceEventArgs(e.mdevice.which));
                break;
            case Native.SDL_EventType.SDL_EVENT_MOUSE_REMOVED:
                MouseRemoved?.Invoke(new InputDeviceEventArgs(e.mdevice.which));
                break;

            case Native.SDL_EventType.SDL_EVENT_TEXT_EDITING:
                TextEditing?.Invoke(new TextEditingEventArgs(
                    e.edit.windowID.Value,
                    Marshal.PtrToStringUTF8((nint)e.edit.text),
                    e.edit.start, e.edit.length));
                break;
            case Native.SDL_EventType.SDL_EVENT_TEXT_EDITING_CANDIDATES:
                TextEditingCandidates?.Invoke(new TextEditingCandidatesEventArgs(
                    e.editCandidates.windowID.Value,
                    ReadStringArray(e.editCandidates.candidates, e.editCandidates.num_candidates),
                    e.editCandidates.selected_candidate,
                    e.editCandidates.horizontal != 0));
                break;

            case Native.SDL_EventType.SDL_EVENT_JOYSTICK_AXIS_MOTION:
                JoystickAxisMotion?.Invoke(new JoyAxisEventArgs(e.jaxis.which.Value, e.jaxis.axis, e.jaxis.value));
                break;
            case Native.SDL_EventType.SDL_EVENT_JOYSTICK_BALL_MOTION:
                JoystickBallMotion?.Invoke(new JoyBallEventArgs(e.jball.which.Value, e.jball.ball, e.jball.xrel, e.jball.yrel));
                break;
            case Native.SDL_EventType.SDL_EVENT_JOYSTICK_HAT_MOTION:
                JoystickHatMotion?.Invoke(new JoyHatEventArgs(e.jhat.which.Value, e.jhat.hat, (HatPosition)e.jhat.value));
                break;
            case Native.SDL_EventType.SDL_EVENT_JOYSTICK_BUTTON_DOWN:
                JoystickButtonDown?.Invoke(new JoyButtonEventArgs(e.jbutton.which.Value, e.jbutton.button, e.jbutton.down != 0));
                break;
            case Native.SDL_EventType.SDL_EVENT_JOYSTICK_BUTTON_UP:
                JoystickButtonUp?.Invoke(new JoyButtonEventArgs(e.jbutton.which.Value, e.jbutton.button, e.jbutton.down != 0));
                break;
            case Native.SDL_EventType.SDL_EVENT_JOYSTICK_ADDED:
                JoystickAdded?.Invoke(new JoyDeviceEventArgs(e.jdevice.which.Value));
                break;
            case Native.SDL_EventType.SDL_EVENT_JOYSTICK_REMOVED:
                JoystickRemoved?.Invoke(new JoyDeviceEventArgs(e.jdevice.which.Value));
                break;
            case Native.SDL_EventType.SDL_EVENT_JOYSTICK_UPDATE_COMPLETE:
                JoystickUpdateComplete?.Invoke(new JoyDeviceEventArgs(e.jdevice.which.Value));
                break;
            case Native.SDL_EventType.SDL_EVENT_JOYSTICK_BATTERY_UPDATED:
                JoystickBatteryUpdated?.Invoke(new JoyBatteryEventArgs(
                    e.jbattery.which.Value, (PowerState)e.jbattery.state, e.jbattery.percent));
                break;

            case Native.SDL_EventType.SDL_EVENT_GAMEPAD_AXIS_MOTION:
                GamepadAxisMotion?.Invoke(new GamepadAxisEventArgs(e.gaxis.which.Value, (GamepadAxis)e.gaxis.axis, e.gaxis.value));
                break;
            case Native.SDL_EventType.SDL_EVENT_GAMEPAD_BUTTON_DOWN:
                GamepadButtonDown?.Invoke(new GamepadButtonEventArgs(e.gbutton.which.Value, (GamepadButton)e.gbutton.button, e.gbutton.down != 0));
                break;
            case Native.SDL_EventType.SDL_EVENT_GAMEPAD_BUTTON_UP:
                GamepadButtonUp?.Invoke(new GamepadButtonEventArgs(e.gbutton.which.Value, (GamepadButton)e.gbutton.button, e.gbutton.down != 0));
                break;
            case Native.SDL_EventType.SDL_EVENT_GAMEPAD_ADDED:
                GamepadAdded?.Invoke(new GamepadDeviceEventArgs(e.gdevice.which.Value));
                break;
            case Native.SDL_EventType.SDL_EVENT_GAMEPAD_REMOVED:
                GamepadRemoved?.Invoke(new GamepadDeviceEventArgs(e.gdevice.which.Value));
                break;
            case Native.SDL_EventType.SDL_EVENT_GAMEPAD_REMAPPED:
                GamepadRemapped?.Invoke(new GamepadDeviceEventArgs(e.gdevice.which.Value));
                break;
            case Native.SDL_EventType.SDL_EVENT_GAMEPAD_UPDATE_COMPLETE:
                GamepadUpdateComplete?.Invoke(new GamepadDeviceEventArgs(e.gdevice.which.Value));
                break;
            case Native.SDL_EventType.SDL_EVENT_GAMEPAD_STEAM_HANDLE_UPDATED:
                GamepadSteamHandleUpdated?.Invoke(new GamepadDeviceEventArgs(e.gdevice.which.Value));
                break;
            case Native.SDL_EventType.SDL_EVENT_GAMEPAD_TOUCHPAD_DOWN:
                GamepadTouchpadDown?.Invoke(ToTouchpadArgs(ref e));
                break;
            case Native.SDL_EventType.SDL_EVENT_GAMEPAD_TOUCHPAD_MOTION:
                GamepadTouchpadMotion?.Invoke(ToTouchpadArgs(ref e));
                break;
            case Native.SDL_EventType.SDL_EVENT_GAMEPAD_TOUCHPAD_UP:
                GamepadTouchpadUp?.Invoke(ToTouchpadArgs(ref e));
                break;
            case Native.SDL_EventType.SDL_EVENT_GAMEPAD_SENSOR_UPDATE:
                GamepadSensorUpdate?.Invoke(new GamepadSensorEventArgs(
                    e.gsensor.which.Value, e.gsensor.sensor,
                    e.gsensor.data[0], e.gsensor.data[1], e.gsensor.data[2],
                    e.gsensor.sensor_timestamp));
                break;

            case Native.SDL_EventType.SDL_EVENT_FINGER_DOWN:
                FingerDown?.Invoke(ToFingerArgs(ref e));
                break;
            case Native.SDL_EventType.SDL_EVENT_FINGER_UP:
                FingerUp?.Invoke(ToFingerArgs(ref e));
                break;
            case Native.SDL_EventType.SDL_EVENT_FINGER_MOTION:
                FingerMotion?.Invoke(ToFingerArgs(ref e));
                break;
            case Native.SDL_EventType.SDL_EVENT_FINGER_CANCELED:
                FingerCanceled?.Invoke(ToFingerArgs(ref e));
                break;
            case Native.SDL_EventType.SDL_EVENT_PINCH_BEGIN:
                PinchBegin?.Invoke(new PinchFingerEventArgs(e.pinch.scale, e.pinch.windowID.Value));
                break;
            case Native.SDL_EventType.SDL_EVENT_PINCH_UPDATE:
                PinchUpdate?.Invoke(new PinchFingerEventArgs(e.pinch.scale, e.pinch.windowID.Value));
                break;
            case Native.SDL_EventType.SDL_EVENT_PINCH_END:
                PinchEnd?.Invoke(new PinchFingerEventArgs(e.pinch.scale, e.pinch.windowID.Value));
                break;

            case Native.SDL_EventType.SDL_EVENT_PEN_PROXIMITY_IN:
                PenProximityIn?.Invoke(new PenProximityEventArgs(e.pproximity.windowID.Value, e.pproximity.which));
                break;
            case Native.SDL_EventType.SDL_EVENT_PEN_PROXIMITY_OUT:
                PenProximityOut?.Invoke(new PenProximityEventArgs(e.pproximity.windowID.Value, e.pproximity.which));
                break;
            case Native.SDL_EventType.SDL_EVENT_PEN_DOWN:
                PenDown?.Invoke(new PenTouchEventArgs(e.ptouch.windowID.Value, e.ptouch.which, e.ptouch.x, e.ptouch.y, e.ptouch.eraser != 0, e.ptouch.down != 0));
                break;
            case Native.SDL_EventType.SDL_EVENT_PEN_UP:
                PenUp?.Invoke(new PenTouchEventArgs(e.ptouch.windowID.Value, e.ptouch.which, e.ptouch.x, e.ptouch.y, e.ptouch.eraser != 0, e.ptouch.down != 0));
                break;
            case Native.SDL_EventType.SDL_EVENT_PEN_MOTION:
                PenMotion?.Invoke(new PenMotionEventArgs(e.pmotion.windowID.Value, e.pmotion.which, e.pmotion.x, e.pmotion.y));
                break;
            case Native.SDL_EventType.SDL_EVENT_PEN_BUTTON_DOWN:
                PenButtonDown?.Invoke(new PenButtonEventArgs(e.pbutton.windowID.Value, e.pbutton.which, e.pbutton.x, e.pbutton.y, e.pbutton.button, e.pbutton.down != 0));
                break;
            case Native.SDL_EventType.SDL_EVENT_PEN_BUTTON_UP:
                PenButtonUp?.Invoke(new PenButtonEventArgs(e.pbutton.windowID.Value, e.pbutton.which, e.pbutton.x, e.pbutton.y, e.pbutton.button, e.pbutton.down != 0));
                break;
            case Native.SDL_EventType.SDL_EVENT_PEN_AXIS:
                PenAxisMotion?.Invoke(new PenAxisEventArgs(e.paxis.windowID.Value, e.paxis.which, e.paxis.x, e.paxis.y, (PenAxis)e.paxis.axis, e.paxis.value));
                break;

            case Native.SDL_EventType.SDL_EVENT_DROP_FILE:
            case Native.SDL_EventType.SDL_EVENT_DROP_TEXT:
            case Native.SDL_EventType.SDL_EVENT_DROP_BEGIN:
            case Native.SDL_EventType.SDL_EVENT_DROP_COMPLETE:
            case Native.SDL_EventType.SDL_EVENT_DROP_POSITION:
                Drop?.Invoke(new DropEventArgs(
                    (EventType)e.type, e.drop.windowID.Value, e.drop.x, e.drop.y,
                    Marshal.PtrToStringUTF8((nint)e.drop.source),
                    Marshal.PtrToStringUTF8((nint)e.drop.data)));
                break;

            case Native.SDL_EventType.SDL_EVENT_CLIPBOARD_UPDATE:
                ClipboardUpdate?.Invoke(new ClipboardEventArgs(
                    e.clipboard.owner != 0,
                    ReadStringArray(e.clipboard.mime_types, e.clipboard.num_mime_types)));
                break;

            case Native.SDL_EventType.SDL_EVENT_SENSOR_UPDATE:
                SensorUpdate?.Invoke(ToSensorArgs(ref e));
                break;

            case >= Native.SDL_EventType.SDL_EVENT_AUDIO_DEVICE_ADDED and <= Native.SDL_EventType.SDL_EVENT_AUDIO_DEVICE_FORMAT_CHANGED:
                AudioDeviceEvent?.Invoke(new AudioDeviceEventArgs(
                    (EventType)e.type, e.adevice.which.Value, e.adevice.recording != 0));
                break;

            case >= Native.SDL_EventType.SDL_EVENT_CAMERA_DEVICE_ADDED and <= Native.SDL_EventType.SDL_EVENT_CAMERA_DEVICE_DENIED:
                CameraDeviceEvent?.Invoke(new CameraDeviceEventArgs((EventType)e.type, e.cdevice.which.Value));
                break;

            case >= Native.SDL_EventType.SDL_EVENT_RENDER_TARGETS_RESET and <= Native.SDL_EventType.SDL_EVENT_RENDER_DEVICE_LOST:
                RenderEvent?.Invoke(new RenderEventArgs((EventType)e.type, e.render.windowID.Value));
                break;
        }
    }

    private static GamepadTouchpadEventArgs ToTouchpadArgs(ref Native.SDL_Event e) =>
        new(e.gtouchpad.which.Value, e.gtouchpad.touchpad, e.gtouchpad.finger, e.gtouchpad.x, e.gtouchpad.y, e.gtouchpad.pressure);

    private static TouchFingerEventArgs ToFingerArgs(ref Native.SDL_Event e) =>
        new(e.tfinger.touchID, e.tfinger.fingerID, e.tfinger.x, e.tfinger.y, e.tfinger.dx, e.tfinger.dy, e.tfinger.pressure, e.tfinger.windowID.Value);

    private static SensorEventArgs ToSensorArgs(ref Native.SDL_Event e)
    {
        var data = new float[6];
        for (var i = 0; i < 6; i++)
        {
            data[i] = e.sensor.data[i];
        }

        return new SensorEventArgs(e.sensor.which, data, e.sensor.sensor_timestamp);
    }

    private static string?[] ReadStringArray(byte** array, int count)
    {
        if (array == null || count <= 0)
        {
            return [];
        }

        var result = new string?[count];
        for (var i = 0; i < count; i++)
        {
            result[i] = Marshal.PtrToStringUTF8((nint)array[i]);
        }

        return result;
    }
```

- [ ] **Step 7: Confirm the display/window range constants exist**

`DispatchExtended` references `SDL_EVENT_DISPLAY_FIRST`/`SDL_EVENT_DISPLAY_LAST`. Grep `Native/Events.cs` for them. SDL defines `SDL_EVENT_DISPLAY_FIRST = SDL_EVENT_DISPLAY_ORIENTATION` and `SDL_EVENT_DISPLAY_LAST = SDL_EVENT_DISPLAY_CONTENT_SCALE_CHANGED`. If those alias members are absent from the native enum, either add them or replace the range pattern with an explicit list of the display event members present in the enum (orientation, added, removed, moved, desktop-mode-changed, current-mode-changed, content-scale-changed). Do the same check for `SDL_EVENT_WINDOW_FIRST/LAST` (already used by the existing dispatch, so present).

- [ ] **Step 8: Build**

Run: `dotnet build SdlSharp.slnx`
Expected: 0 Warnings, 0 Errors. Every new public event and args type is documented.

- [ ] **Step 9: Commit**

```bash
git add src/SdlSharp/Application.cs src/SdlSharp/Input/DeviceEventArgs.cs src/SdlSharp/Input/DisplayEventArgs.cs src/SdlSharp/Input/JoystickEventArgs.cs src/SdlSharp/Input/GamepadEventArgs.cs src/SdlSharp/Input/TouchEventArgs.cs src/SdlSharp/Input/PenEventArgs.cs src/SdlSharp/Input/DropEventArgs.cs src/SdlSharp/Input/SensorEventArgs.cs
git commit -m "Add typed dispatch for all remaining event families"
```

---

## Task 11: AudioOps sample (headless audio proof)

**Files:**
- Create: `Samples/AudioOps/AudioOps.csproj`
- Create: `Samples/AudioOps/Program.cs`
- Modify: `SdlSharp.slnx`

- [ ] **Step 1: Create the csproj**

`Samples/AudioOps/AudioOps.csproj` (copy the SurfaceOps template exactly):

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net10.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <AllowUnsafeBlocks>true</AllowUnsafeBlocks>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="../../src/SdlSharp/SdlSharp.csproj" />
  </ItemGroup>

</Project>
```

- [ ] **Step 2: Write the sample**

`Samples/AudioOps/Program.cs` — headless checks that never require real hardware (dummy driver). Each check prints `PASS`/`FAIL`; a single non-zero exit code on any failure:

```csharp
using SdlSharp;
using SdlSharp.Audio;

Environment.SetEnvironmentVariable("SDL_AUDIO_DRIVER", "dummy");

using var app = new Application(InitFlags.Audio);

var failures = 0;

void Check(string name, bool ok)
{
    Console.WriteLine($"{(ok ? "PASS" : "FAIL")}  {name}");
    if (!ok) failures++;
}

// Format introspection.
Check("S16 bit size", AudioFormat.S16.GetBitSize() == 16);
Check("S16 byte size", AudioFormat.S16.GetByteSize() == 2);
Check("S16 is signed", AudioFormat.S16.IsSigned());
Check("S16 is int", AudioFormat.S16.IsInt());
Check("F32 is float", AudioFormat.F32.IsFloat());
Check("U8 is unsigned", AudioFormat.U8.IsUnsigned());
Check("S16 is little-endian", AudioFormat.S16.IsLittleEndian());
Check("format name non-empty", !string.IsNullOrEmpty(AudioFormat.S16.GetName()));

// FrameSize.
var stereoS16 = new AudioSpec(AudioFormat.S16, 2, 48000);
Check("stereo S16 frame size", stereoS16.FrameSize == 4);

// Silence values.
Check("U8 silence is 128", AudioFormat.U8.GetSilenceValue() == 128);
Check("S16 silence is 0", AudioFormat.S16.GetSilenceValue() == 0);

// Convert: 4 mono S16 samples -> stereo F32.
var srcSpec = new AudioSpec(AudioFormat.S16, 1, 48000);
var dstSpec = new AudioSpec(AudioFormat.F32, 2, 48000);
var srcBytes = new byte[4 * 2]; // 4 samples * 2 bytes
var converted = AudioSamples.Convert(srcSpec, srcBytes, dstSpec);
// 4 frames * 2 channels * 4 bytes = 32 bytes expected.
Check("convert output length", converted.Length == 32);

// Mix: silence into silence stays silence.
var dst = new byte[8];
var add = new byte[8];
AudioSamples.Mix(dst, add, AudioFormat.S16, 1.0f);
Check("mix silence stays zero", Array.TrueForAll(dst, b => b == 0));

// Stream put callback fires on PutData.
using var stream = AudioStream.Create(srcSpec, srcSpec);
var putCallbackFired = false;
stream.SetPutCallback((s, additional, total) => putCallbackFired = true);
stream.PutData(new byte[16]);
Check("put callback fired", putCallbackFired);

// Lock scope round-trips.
var locked = false;
using (stream.Lock())
{
    locked = true;
}
Check("lock scope", locked);

// Channel map default is null.
Check("input channel map default null", stream.GetInputChannelMap() is null);

// Clearing the callback works.
stream.SetPutCallback(null);
Check("put callback cleared", true);

// Device from OpenDevice is non-null; postmix set/clear.
using var deviceStream = AudioStream.OpenDevice(playback: true);
var device = deviceStream.Device;
Check("stream device non-null", device is not null);
if (device is not null)
{
    device.SetPostmixCallback((in AudioSpec spec, Span<float> buffer) => { });
    device.SetPostmixCallback(null);
    Check("postmix set/clear", true);
    Check("device is playback", device.IsPlayback);
}

Console.WriteLine(failures == 0 ? "ALL PASS" : $"{failures} FAILURES");
return failures == 0 ? 0 : 1;
```

- [ ] **Step 3: Register in the solution**

Add to `SdlSharp.slnx` after the last `<Project Path="Samples/…">` line:

```xml
    <Project Path="Samples/AudioOps/AudioOps.csproj" />
```

- [ ] **Step 4: Build and run**

Run: `dotnet build SdlSharp.slnx`
Expected: 0 Warnings, 0 Errors.

Run: `dotnet run --project Samples/AudioOps/AudioOps.csproj`
Expected: every line prints `PASS`, final line `ALL PASS`, exit code 0. If the native SDL library isn't on the sample's runtime path, follow the same launch approach the existing SurfaceOps sample uses (it already resolves the redist native lib); mirror any `runtimeconfig`/native-copy the other samples rely on. If a check legitimately can't run under the dummy driver (for example `Device` being null), investigate before weakening the assertion — the dummy driver does provide a logical playback device.

- [ ] **Step 5: Commit**

```bash
git add Samples/AudioOps/ SdlSharp.slnx
git commit -m "Add AudioOps headless sample proving phase 10 audio surface"
```

---

## Task 12: EventQueueOps sample (headless events proof)

**Files:**
- Create: `Samples/EventQueueOps/EventQueueOps.csproj`
- Create: `Samples/EventQueueOps/Program.cs`
- Modify: `SdlSharp.slnx`

- [ ] **Step 1: Create the csproj** (same template as Task 11, name `EventQueueOps`).

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net10.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <AllowUnsafeBlocks>true</AllowUnsafeBlocks>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="../../src/SdlSharp/SdlSharp.csproj" />
  </ItemGroup>

</Project>
```

- [ ] **Step 2: Write the sample**

`Samples/EventQueueOps/Program.cs` — exercises register/push/has/wait-dispatch, filter drop, watch, flush. Runs headless (events subsystem needs no display):

```csharp
using SdlSharp;
using SdlSharp.Input;

using var app = new Application(InitFlags.Events);

var failures = 0;

void Check(string name, bool ok)
{
    Console.WriteLine($"{(ok ? "PASS" : "FAIL")}  {name}");
    if (!ok) failures++;
}

// Register a custom event type.
var myEvent = Application.RegisterEvents(1);
Check("registered type >= User", myEvent >= EventType.User);

// Push one and confirm it queues.
var pushed = Application.PushUserEvent(myEvent, code: 42, data1: 7, data2: 9);
Check("push succeeded", pushed);
Check("HasEvent true after push", Application.HasEvent(myEvent));

// WaitDispatchEvent should fire the typed UserEvent handler.
UserEventArgs? received = null;
Application.UserEvent += args => received = args;
var dispatched = Application.WaitDispatchEvent(1000);
Check("wait dispatched an event", dispatched);
Check("user event received", received is { Code: 42, Data1: 7, Data2: 9 });
Check("queue empty after dispatch", !Application.HasEvent(myEvent));

// A filter that drops our event type prevents queueing.
Application.SetEventFilter((in RawEvent e) => e.Type != myEvent);
var pushedWhileFiltered = Application.PushUserEvent(myEvent, code: 1);
Check("filtered push not queued", !pushedWhileFiltered || !Application.HasEvent(myEvent));
Application.SetEventFilter(null);

// A watch observes a pushed event.
var watched = false;
void Watch(in RawEvent e)
{
    if (e.Type == myEvent) watched = true;
}
Application.AddEventWatch(Watch);
Application.PushUserEvent(myEvent, code: 2);
Check("watch observed push", watched);
Application.RemoveEventWatch(Watch);

// Flush clears remaining events of the type.
Application.PushUserEvent(myEvent, code: 3);
Application.FlushEvent(myEvent);
Check("flush cleared queue", !Application.HasEvent(myEvent));

// Enable/disable round-trips.
Application.SetEventEnabled(myEvent, false);
Check("event disabled", !Application.IsEventEnabled(myEvent));
Application.SetEventEnabled(myEvent, true);
Check("event enabled", Application.IsEventEnabled(myEvent));

Console.WriteLine(failures == 0 ? "ALL PASS" : $"{failures} FAILURES");
return failures == 0 ? 0 : 1;
```

Note on the filter check: SDL's insertion-time filter for user events pushed on the same thread runs synchronously inside `SDL_PushEvent`, so a dropped event returns `false` from `PushUserEvent` and never queues — the assertion tolerates either signal.

- [ ] **Step 3: Register in the solution**

```xml
    <Project Path="Samples/EventQueueOps/EventQueueOps.csproj" />
```

- [ ] **Step 4: Build and run**

Run: `dotnet build SdlSharp.slnx`
Expected: 0 Warnings, 0 Errors.

Run: `dotnet run --project Samples/EventQueueOps/EventQueueOps.csproj`
Expected: every line `PASS`, final `ALL PASS`, exit 0.

- [ ] **Step 5: Commit**

```bash
git add Samples/EventQueueOps/ SdlSharp.slnx
git commit -m "Add EventQueueOps headless sample proving phase 10 events surface"
```

---

## Task 13: Accounting, INVENTORY.md, TODO.md close-out

**Files:**
- Modify: `INVENTORY.md`
- Modify: `TODO.md`

- [ ] **Step 1: Prove per-file accounting via set-diff**

For audio, regenerate the header/bound lists and confirm every header function is either bound or has a skip comment:

```bash
awk '/^extern SDL_DECLSPEC/{s=$0; while (s !~ /;/) {getline l; s=s" "l} print s}' ../SDL/include/SDL3/SDL_audio.h | grep -o 'SDLCALL SDL_[A-Za-z0-9_]*' | sed 's/SDLCALL //' | sort -u > /tmp/audio_hdr.txt
grep -o 'EntryPoint = "[^"]*"' src/SdlSharp/Native/Audio.cs | sed 's/EntryPoint = "//;s/"//' | sort -u > /tmp/audio_bound.txt
comm -23 /tmp/audio_hdr.txt /tmp/audio_bound.txt
```

Expected remaining (unbound): exactly `SDL_LoadWAV_IO` and `SDL_PutAudioStreamDataNoCopy` — both must have skip comments in `Audio.cs`. Any other name means a binding was missed; add it.

Repeat for events:

```bash
awk '/^extern SDL_DECLSPEC/{s=$0; while (s !~ /;/) {getline l; s=s" "l} print s}' ../SDL/include/SDL3/SDL_events.h | grep -o 'SDLCALL SDL_[A-Za-z0-9_]*' | sed 's/SDLCALL //' | sort -u > /tmp/events_hdr.txt
grep -o 'EntryPoint = "[^"]*"' src/SdlSharp/Native/Events.cs | sed 's/EntryPoint = "//;s/"//' | sort -u > /tmp/events_bound.txt
comm -23 /tmp/events_hdr.txt /tmp/events_bound.txt
```

Expected remaining (unbound): exactly `SDL_PeepEvents` and `SDL_GetEventFilter` — both must have skip comments. Any other name is a miss.

- [ ] **Step 2: Native-orphan sweep**

Confirm every newly-bound native function is reachable from the managed layer. For each new entry point, grep `src/SdlSharp` (excluding `Native/`) for a call:

```bash
for fn in SDL_SetAudioStreamGetCallback SDL_SetAudioStreamPutCallback SDL_SetAudioPostmixCallback SDL_LockAudioStream SDL_UnlockAudioStream SDL_MixAudio SDL_ConvertAudioSamples SDL_PutAudioStreamPlanarData SDL_BindAudioStreams SDL_UnbindAudioStreams SDL_GetSilenceValueForFormat SDL_IsAudioDevicePhysical SDL_IsAudioDevicePlayback SDL_GetAudioDeviceChannelMap SDL_GetAudioStreamInputChannelMap SDL_GetAudioStreamOutputChannelMap SDL_SetAudioStreamInputChannelMap SDL_SetAudioStreamOutputChannelMap SDL_AddEventWatch SDL_RemoveEventWatch SDL_SetEventFilter SDL_FilterEvents SDL_GetWindowFromEvent SDL_GetEventDescription; do
  n=$(grep -rl --include=*.cs "$fn" src/SdlSharp | grep -v '/Native/' | wc -l)
  echo "$fn: $n managed references"
done
```

Expected: every function reports ≥ 1 managed reference. Zero means an orphan — wire it up.

- [ ] **Step 3: No-native-types gate**

Confirm no `SdlSharp.Native` type leaked into a public signature in the new code:

```bash
grep -rn "Native\." src/SdlSharp/Audio src/SdlSharp/Input/RawEvent.cs src/SdlSharp/Input/EventArgs.cs src/SdlSharp/Application.cs | grep -i "public " | grep -v "// " || echo "no public leaks"
```

Expected: `no public leaks` (internal `Native.` casts inside method bodies are fine; only public parameter/return/property types matter — eyeball any hits).

- [ ] **Step 4: Update `INVENTORY.md`**

Update the SDL_audio.h and SDL_events.h sections to reflect the new counts and mark them ✅. For SDL_audio.h: bound count rises by 18 to 56/58 with the two documented skips (`SDL_LoadWAV_IO`, `SDL_PutAudioStreamDataNoCopy`). For SDL_events.h: 6 newly bound + 3 new structs, leaving `SDL_PeepEvents` and `SDL_GetEventFilter` as documented skips. Each previously-unwrapped row that's now bound moves to bound; each remaining unwrapped row must carry its settled rationale (no bare "deferred"). Only mark a section ✅ when every unwrapped row has a settled rationale. Match the exact table format already used in the file (read the surrounding sections first to mirror columns and checkmark placement).

- [ ] **Step 5: Update `TODO.md`**

Check off phase 10 (10.1 and 10.2) in the "Phases 7–11: SDL3 Surface Completion" section. Leave phase 11 items unchecked.

- [ ] **Step 6: Verify existing samples still run**

Run the previously-built headless samples to confirm no regression:

```bash
dotnet run --project Samples/InputInfo/InputInfo.csproj
dotnet run --project Samples/SurfaceOps/SurfaceOps.csproj
```

Expected: both complete with their prior success output (all checks pass). The interactive ImGuiDemo smoke test remains a manual/user step and is not required here.

- [ ] **Step 7: Final build gate**

Run: `dotnet build SdlSharp.slnx`
Expected: 0 Warnings, 0 Errors.

- [ ] **Step 8: Commit**

```bash
git add INVENTORY.md TODO.md
git commit -m "Record phase 10 audio and events surface in INVENTORY.md; complete phase 10"
```

---

## Self-Review Notes (for the executor)

- **Union field names:** `DispatchExtended` reads `e.kdevice`, `e.mdevice`, `e.edit`, `e.editCandidates`, `e.jaxis/jball/jhat/jbutton/jdevice/jbattery`, `e.gaxis/gbutton/gdevice/gtouchpad/gsensor`, `e.tfinger`, `e.pinch`, `e.pproximity/ptouch/pmotion/pbutton/paxis`, `e.drop`, `e.clipboard`, `e.sensor`, `e.adevice`, `e.cdevice`, `e.render`, `e.user`. All except the three added in Task 6 (`adevice`, `cdevice`, `editCandidates`) already exist in the `SDL_Event` union (verified against `Native/Events.cs`). Task 6 adds those three union members — do not skip that step.
- **Enum member names:** verify the exact spelling of `SDL_EVENT_MOUSE_REMOVED`, `SDL_EVENT_JOYSTICK_BALL_MOTION`, and the display FIRST/LAST aliases in the native enum before relying on them (Task 10 Step 7). The native `SDL_EventType` enum in `Native/Events.cs` is the source of truth.
- **`GamepadSensorEvent.data` is `fixed float[3]`; `SensorEvent.data` is `fixed float[6]`** — indexing requires the enclosing method to be in an `unsafe` context (the class already is `unsafe`).
- **`AudioStreamLock` is a `ref struct`** so it can't be boxed or stored on the heap, which is correct for a scoped lock. It intentionally has no owner/dispose-guard beyond calling unlock.
- **Partial method:** `Application` must be `partial` for the `DispatchExtended` split (Task 7 changes the class declaration). If the executor inlines Task 10's cases into `DispatchOne` instead, the `partial` keyword and the stub are unnecessary — either structure is acceptable as long as the build is clean and every family dispatches.
