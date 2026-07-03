# Phase 10: Audio + Events Depth — Design

**Date:** 2026-07-03
**Scope:** TODO.md phase 10 — 10.1 (audio callbacks + utilities) and 10.2 (events depth: public queue ops, custom events, watches/filters, full typed dispatch).

## Goals

1. SDL_audio.h reaches full accounting: 56/58 functions bound, 2 documented skips.
2. SDL_events.h reaches full accounting: 19/20 functions bound, 1 documented skip.
3. Every event struct in SDL_events.h has a typed dispatch path on `Application`.
4. Pull-model audio (stream Get/Put callbacks) and postmix become usable from C#.

## Decisions (user-approved)

- **Audio callbacks: single-delegate setters.** `SetGetCallback`/`SetPutCallback`/`SetPostmixCallback`; setting replaces, null clears — mirrors SDL's single-slot semantics. Not C# multicast events.
- **Custom user events: raw `nint` payloads.** No GCHandle object payloads (events flushed/filtered before dispatch would silently leak handles).
- **Queue surgery: `SDL_FilterEvents` in, `SDL_PeepEvents` documented skip.** PeepEvents operates on caller-supplied `SDL_Event` arrays, which doesn't fit the `RawEvent` pointer model; Has/Flush/Wait/Push cover the practical uses.

## 10.1 Audio

### Native layer (`src/SdlSharp/Native/Audio.cs`)

Bind 18 functions (LibraryImport + CallConvCdecl per house rules; `[MarshalAs(UnmanagedType.U1)]` on bool returns/params):

| Function | Notes |
|---|---|
| `SDL_SetAudioStreamGetCallback` | takes `delegate* unmanaged[Cdecl]<void*, SDL_AudioStream*, int, int, void>` + userdata |
| `SDL_SetAudioStreamPutCallback` | same signature shape |
| `SDL_SetAudioPostmixCallback` | `delegate* unmanaged[Cdecl]<void*, SDL_AudioSpec*, float*, int, void>` + userdata |
| `SDL_LockAudioStream` / `SDL_UnlockAudioStream` | bool returns |
| `SDL_MixAudio` | `byte* dst, byte* src, SDL_AudioFormat, uint len, float volume` |
| `SDL_ConvertAudioSamples` | in-spec/in-data/in-len → out-spec/out-data/out-len (`byte**`, SDL-allocated, free with `SDL_free`) |
| `SDL_PutAudioStreamPlanarData` | `byte** channel_buffers, int num_channels, int num_samples` |
| `SDL_BindAudioStreams` / `SDL_UnbindAudioStreams` | array of `SDL_AudioStream*` + count |
| `SDL_GetSilenceValueForFormat` | returns int |
| `SDL_IsAudioDevicePhysical` / `SDL_IsAudioDevicePlayback` | bool, take device id |
| `SDL_GetAudioDeviceChannelMap` | returns `int*` (SDL-allocated; free with `SDL_free`; null = default order), out count |
| `SDL_GetAudioStreamInputChannelMap` / `SDL_GetAudioStreamOutputChannelMap` | same contract |
| `SDL_SetAudioStreamInputChannelMap` / `SDL_SetAudioStreamOutputChannelMap` | `int* chmap, int count`; null resets |

Documented skips (comment in Audio.cs, per house pattern):

- `SDL_LoadWAV_IO` — IO-stream variant; .NET has its own stream abstractions (consistent with all prior `_IO` skips).
- `SDL_PutAudioStreamDataNoCopy` — zero-copy put requires the caller to keep the native buffer alive until an asynchronous completion callback fires; incompatible with managed memory without unbounded pinning. `PutData`/`PutPlanarData` cover the use case.

Native `SDL_AudioFormat` enum: add `SDL_AUDIO_S16`, `SDL_AUDIO_S32`, `SDL_AUDIO_F32` alias members equal to the LE variants. SDL's header resolves these by compile-time byte order; every platform the redist ships for (win-x64/arm64, osx, linux-x64/arm64) is little-endian. Doc comment states this assumption.

### Managed layer

**`AudioFormat` enum** (`src/SdlSharp/Audio/AudioFormat.cs`): add `S16`, `S32`, `F32` aliases referencing the native alias members, same doc note.

**`AudioFormatInfo`** (`src/SdlSharp/Audio/AudioFormatInfo.cs`) — convert to extension members on `AudioFormat` (keep existing `GetName` shape; new members as `this AudioFormat` extensions):

- `GetBitSize()`, `GetByteSize()` — SDL_AUDIO_BITSIZE / BYTESIZE macros (`format & 0xFF`, `bitsize / 8`)
- `IsFloat()`, `IsBigEndian()`, `IsLittleEndian()`, `IsSigned()`, `IsInt()`, `IsUnsigned()` — mask macros from SDL_audio.h
- `GetSilenceValue()` — wraps `SDL_GetSilenceValueForFormat`

**`AudioSpec`** (`src/SdlSharp/Audio/AudioSpec.cs`): add computed `int FrameSize => Format.GetByteSize() * Channels;` (SDL_AUDIO_FRAMESIZE macro).

**`AudioStream`** (`src/SdlSharp/Audio/AudioStream.cs`):

- `public delegate void AudioStreamDataCallback(AudioStream stream, int additionalAmount, int totalAmount);` (namespace-level, `SdlSharp.Audio`)
- `SetGetCallback(AudioStreamDataCallback? callback)` / `SetPutCallback(AudioStreamDataCallback? callback)` — one GCHandle slot per callback kind; the handle wraps a state object holding the `AudioStream` wrapper and the user delegate. Static `[UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]` trampoline recovers the state, invokes inside try/catch (exceptions swallowed — they cannot propagate across the SDL audio thread; documented). Replace: register new, then free old handle. Null: clear native callback, free handle. `Dispose()`: clear both handles after destroy. Docs on both methods carry the audio-thread warning (callback runs on SDL's audio thread; keep it fast; use `Lock()` for shared state).
- `Lock()` returns `AudioStreamLock` — a `public ref struct` with `Dispose()` calling `SDL_UnlockAudioStream` (Check on Lock, bare call on unlock-in-dispose). Enables `using var _ = stream.Lock();`.
- `Device` property → `AudioDevice?` — non-owning wrapper around `SDL_GetAudioStreamDevice` result; null when unbound (id 0). Doc caveat: each access returns a new wrapper; wrappers are not equal to each other.
- `PutPlanarData(byte[][] channels, int numSamples)` — matches the native `byte**`: pin each channel array, build a stackalloc pointer array, Check on result. Byte-based; callers lay out samples per the stream's source format. Throws ArgumentException if `channels.Length` doesn't match the source-spec channel count (SDL validates too, but the managed check gives a clearer error).
- `GetInputChannelMap()` / `GetOutputChannelMap()` → `int[]?` (null = default order; copy from SDL buffer then `SDL_free`)
- `SetInputChannelMap(int[]? map)` / `SetOutputChannelMap(int[]? map)` — null resets to default; Check on result.

**`AudioDevice`** (`src/SdlSharp/Audio/AudioDevice.cs`):

- `public delegate void AudioPostmixCallback(in AudioSpec spec, Span<float> buffer);`
- `SetPostmixCallback(AudioPostmixCallback? callback)` — same GCHandle/trampoline/replace/clear/Dispose lifecycle as stream callbacks; audio-thread warning; buffer is the device's final mix in float32.
- `Bind(params AudioStream[] streams)` — plural overload via `SDL_BindAudioStreams` (stackalloc pointer array; existing single-stream `Bind` stays); static `Unbind(params AudioStream[] streams)` via `SDL_UnbindAudioStreams` (complements the existing per-stream `AudioStream.Unbind()`).
- `IsPhysical` / `IsPlayback` properties (id-based, no-throw bools).
- `GetChannelMap()` → `int[]?` (same contract as stream maps).
- Non-owning support: constructor already takes an id; add internal non-owning path if not present (Dispose must not close a device it doesn't own) — follow the house `_ownsHandle` pattern.

**New `AudioSamples` static class** (`src/SdlSharp/Audio/AudioSamples.cs`):

- `Mix(Span<byte> destination, ReadOnlySpan<byte> source, AudioFormat format, float volume)` — lengths must match (throw ArgumentException otherwise); wraps `SDL_MixAudio`; volume 0.0–1.0 doc note.
- `Convert(AudioSpec sourceSpec, ReadOnlySpan<byte> sourceData, AudioSpec destinationSpec)` → `byte[]` — wraps `SDL_ConvertAudioSamples`; copies SDL-allocated output then `SDL_free`.

## 10.2 Events

### Native layer (`src/SdlSharp/Native/Events.cs`)

Bind 7 functions:

| Function | Notes |
|---|---|
| `SDL_AddEventWatch` / `SDL_RemoveEventWatch` | `delegate* unmanaged[Cdecl]<void*, SDL_Event*, byte>` + userdata; Add returns bool |
| `SDL_SetEventFilter` / `SDL_GetEventFilter` | same function-pointer shape; Get has out params |
| `SDL_FilterEvents` | one-shot sweep; removes events where callback returns false |
| `SDL_GetWindowFromEvent` | returns `SDL_Window*` (null OK) |
| `SDL_GetEventDescription` | `SDL_Event*, byte* buf, int buflen` → int (needed length) |

Documented skip: `SDL_PeepEvents` — bulk add/peek/get on caller-supplied `SDL_Event` arrays; doesn't fit the `RawEvent` transient-pointer model and would require a second managed event representation. `Has*/Flush*/Wait*/Push*` cover the practical uses. (Also skip the `SDL_EventAction` enum that exists only for PeepEvents.)

Event structs: audit Native/Events.cs against the header; add any missing structs — known gaps at design time: `SDL_TextEditingCandidatesEvent`, `SDL_CameraDeviceEvent`. The plan's accounting step must set-diff the header's typedef list and close every gap.

### Managed layer — `Application` (`src/SdlSharp/Application.cs`) + args files

**Queue operations (public statics):**

- `WaitDispatchEvent(int timeoutMilliseconds = -1)` → bool — blocks until one event arrives (−1 = infinite, wraps `SDL_WaitEvent`; otherwise `SDL_WaitEventTimeout`), runs it through the exact same path as `DispatchEvents` (RawEventFilter event, then typed dispatch), returns false on timeout. Refactor the per-event dispatch body out of `DispatchEvents` into a private `DispatchOne(SDL_Event*)` shared by both.
- `HasEvent(EventType)` / `HasEvents(EventType min, EventType max)` → bool
- `FlushEvent(EventType)` / `FlushEvents(EventType min, EventType max)`
- `SetEventEnabled(EventType, bool)` / `IsEventEnabled(EventType)` → bool

**Custom user events:**

- `RegisterEvents(int count)` → `EventType` — throws SdlException on exhaustion (SDL returns 0).
- `PushUserEvent(EventType type, int code, nint data1 = 0, nint data2 = 0)` → bool — builds an `SDL_Event` with `SDL_UserEvent` payload; returns false when a filter dropped it; throws on error (SDL_PushEvent's bool-false-with-error vs filtered distinction: SDL returns false for filtered with no error set — treat false as "filtered", check `SDL_GetError` emptiness is NOT reliable; document plainly: false = not queued).
- `public static event Action<UserEventArgs>? UserEvent;` — `UserEventArgs(EventType Type, ulong Timestamp, uint WindowId, int Code, nint Data1, nint Data2)`. Dispatch case: any type value ≥ `EventType.User`.
- No general `PushEvent(in RawEvent)`: a `RawEvent`'s pointer is only valid during its originating callback; re-injection is a use-after-free trap. Documented on `RawEvent`.

**Watches and filters:**

- `AddEventWatch(RawEventHandler watch)` / `RemoveEventWatch(RawEventHandler watch)` — delegate-keyed `Dictionary<RawEventHandler, GCHandle>` registry (static, lock-protected); the GCHandle is the SDL userdata, so each delegate registers with a distinct userdata and Remove passes the matching pair. Adding the same delegate twice throws ArgumentException. Watch return value to SDL is always true (SDL ignores it for watches). Docs: watches fire from the thread that pushes the event, possibly during `PumpEvents` or from SDL-internal threads (e.g., during window resize modal loops).
- `public delegate bool RawEventPredicate(in RawEvent e);`
- `SetEventFilter(RawEventPredicate? filter)` — single-slot semantics like audio callbacks (replace frees old handle; null clears). Doc must distinguish: this is SDL's queue-insertion-time filter (event dropped before it's ever queued, callback can run on any thread) vs the existing `RawEventFilter` event (dispatch-time observation on the main loop).
- `FilterEvents(RawEventPredicate predicate)` — one-shot sweep over the current queue; events where the predicate returns false are removed. GCHandle allocated and freed around the call.
- Trampolines: static `[UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]` returning `byte`; catch-all → return 1 (keep event) so a throwing user callback never drops events silently.

**`RawEvent` additions** (`src/SdlSharp/Input/RawEvent.cs`):

- `Window` property → `Graphics.Window?` — non-owning wrapper via `SDL_GetWindowFromEvent`; null when the event has no window. Same "new wrapper per access" caveat.
- `Description` property → `string` — `SDL_GetEventDescription` two-call pattern (query length, stackalloc/rent, decode UTF-8).

**Typed dispatch — full parity.** New `readonly record struct` args (one file per family under `src/SdlSharp/Input/`, display/render under matching namespaces per existing precedent) + `Application` static events + `DispatchOne` switch cases:

| Family | Events on Application | Args |
|---|---|---|
| Display | `Display` (single event; args carry EventType) | `DisplayEventArgs(EventType, ulong Timestamp, uint DisplayId, int Data1, int Data2)` |
| Keyboard device | `KeyboardAdded`, `KeyboardRemoved` | `KeyboardDeviceEventArgs` |
| Mouse device | `MouseAdded`, `MouseRemoved` | `MouseDeviceEventArgs` |
| Text editing | `TextEditing`, `TextEditingCandidates` | `TextEditingEventArgs` (string Text, int Start, int Length), `TextEditingCandidatesEventArgs` (string?[] Candidates, int SelectedCandidate, bool Horizontal) |
| Joystick | `JoystickAxisMotion`, `JoystickBallMotion`, `JoystickHatMotion`, `JoystickButtonDown/Up`, `JoystickAdded/Removed`, `JoystickUpdateComplete`, `JoystickBatteryUpdated` | one args record per struct (`JoyAxisEventArgs` etc.), joystick identified by instance id (uint) |
| Gamepad | `GamepadAxisMotion`, `GamepadButtonDown/Up`, `GamepadAdded/Removed/Remapped`, `GamepadSteamHandleUpdated`, `GamepadTouchpadDown/Motion/Up`, `GamepadSensorUpdate` | per-struct args; enums reuse existing `GamepadAxis`/`GamepadButton`/`SensorType` |
| Touch | `FingerDown/Up/Motion/Canceled`, `PinchBegin/Update/End` (confirmed in 3.4: event values 0x710–0x712, `SDL_PinchFingerEvent` already bound) | `TouchFingerEventArgs`; `PinchFingerEventArgs` |
| Pen | `PenProximityIn/Out`, `PenDown/Up`, `PenMotion`, `PenButtonDown/Up`, `PenAxisMotion` | per-struct args; reuses the existing public `PenAxis` enum (`src/SdlSharp/Input/PenAxis.cs`) |
| Drop | `DropBegin/File/Text/Position/Complete` | `DropEventArgs(uint WindowId, float X, float Y, string? Source, string? Data)` (strings marshalled per event type) |
| Clipboard | `ClipboardUpdate` | `ClipboardEventArgs(bool OwnerIsSelf, string?[] MimeTypes)` |
| Sensor | `SensorUpdate` | `SensorEventArgs(uint SensorId, float[] Data, ulong SensorTimestamp)` (fixed float[6] copied) |
| Camera device | `CameraAdded/Removed/Approved/Denied` | `CameraDeviceEventArgs(uint CameraId)` |
| Render | `RenderTargetsReset`, `RenderDeviceReset`, `RenderDeviceLost` | `RenderEventArgs(uint WindowId)` |
| User | `UserEvent` (above) | `UserEventArgs` |

Exact member shapes come from the header structs during planning; the rule is: no native types in args, ids stay raw uints (matching existing `WindowEventArgs` precedent), enums map to existing public enums. Existing nine typed events and `RawEventFilter` are untouched; `RawEventFilter` remains the catch-all for anything new SDL versions add.

## Verification

- **EventQueueOps sample** (headless, `Samples/EventQueueOps/`): register a custom event type; push with code+payload; `HasEvent` true; `WaitDispatchEvent` fires the typed `UserEvent`; `SetEventFilter` drops a second push (`HasEvent` false); watch observes a third push; `FlushEvents` clears; boolean checks printed, all must be true. Registered in `SdlSharp.slnx`.
- **AudioOps sample** (headless, `Samples/AudioOps/`, run with `SDL_AUDIO_DRIVER=dummy`): format introspection asserts (S16 → 16 bits/2 bytes/signed/int; F32 → float), `AudioSpec.FrameSize`, silence values, `AudioSamples.Convert` (S16 mono → F32 stereo, length check), `AudioSamples.Mix`, stream create + `SetPutCallback` + `PutData` → callback observed, `Lock()` scope, channel-map round-trip (null default OK), `AudioStream.Device` non-null after `OpenDevice`, postmix set/clear. Registered in `SdlSharp.slnx`.
- Gates: `dotnet build SdlSharp.slnx` zero warnings/errors; native-orphan sweep (every new native reachable from managed); per-file accounting proven by set-diff (audio 56/58 bound + 2 skip comments; events 19/20 + 1 skip comment); no `SdlSharp.Native` types in any new public signature; INVENTORY.md SDL_audio.h and SDL_events.h sections earn ✅ (every unwrapped row has settled rationale); TODO.md phase 10 checked off; existing samples (InputInfo, SurfaceOps, GpuInfo, ImGuiDemo smoke) still run.

## Out of scope

- Haptic effects, tray, small headers (phase 11).
- `SDL_main`-style app callbacks (long-standing documented skip).
- Any push of the `sdl3` branch (deferred by user).
