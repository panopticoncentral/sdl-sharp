# Managed Wrappers for Remaining Native Bindings

**Date**: 2026-03-22
**Status**: Approved
**Scope**: Add high-level C# managed wrappers for all SDL3 subsystems that have native P/Invoke bindings but lack managed wrappers.

## Overview

The native layer (`SdlSharp.Native`) has complete P/Invoke bindings for Camera, Dialog, Time, Haptic, Gamepad, Joystick, Touch, Pen, and Sensor. Audio has two deferred managed wrappers (LoadWAV, GetAudioFormatName). This spec covers adding idiomatic C# managed wrappers for all of them, plus sample projects.

## Namespace Assignment

| Subsystem | Namespace | Rationale |
|-----------|-----------|-----------|
| Camera | `SdlSharp.Graphics` | Produces visual frames/surfaces |
| FileDialog | `SdlSharp.Graphics` | Window-parented UI |
| Time/DateTime | `SdlSharp` | General utility |
| Audio additions | `SdlSharp.Audio` | Extends existing audio wrappers |
| Gamepad | `SdlSharp.Input` | Input device |
| Joystick | `SdlSharp.Input` | Input device |
| Haptic | `SdlSharp.Input` | Typically controller-associated |
| Sensor | `SdlSharp.Input` | Input device |
| Touch | `SdlSharp.Input` | Input device |
| Pen | `SdlSharp.Input` | Input device |

## New Enums (16 files)

All follow the CLAUDE.md pattern: public enum wrapping native enum, members expressed in terms of native members.

| Enum | File | Namespace | Wraps |
|------|------|-----------|-------|
| `CameraPosition` | `Graphics/CameraPosition.cs` | `SdlSharp.Graphics` | `SDL_CameraPosition` |
| `FileDialogType` | `Graphics/FileDialogType.cs` | `SdlSharp.Graphics` | `SDL_FileDialogType` |
| `DateFormat` | `DateFormat.cs` | `SdlSharp` | `SDL_DateFormat` |
| `TimeFormat` | `TimeFormat.cs` | `SdlSharp` | `SDL_TimeFormat` |
| `GamepadType` | `Input/GamepadType.cs` | `SdlSharp.Input` | `SDL_GamepadType` |
| `GamepadButton` | `Input/GamepadButton.cs` | `SdlSharp.Input` | `SDL_GamepadButton` |
| `GamepadButtonLabel` | `Input/GamepadButtonLabel.cs` | `SdlSharp.Input` | `SDL_GamepadButtonLabel` |
| `GamepadAxis` | `Input/GamepadAxis.cs` | `SdlSharp.Input` | `SDL_GamepadAxis` |
| `JoystickType` | `Input/JoystickType.cs` | `SdlSharp.Input` | `SDL_JoystickType` |
| `JoystickConnectionState` | `Input/JoystickConnectionState.cs` | `SdlSharp.Input` | `SDL_JoystickConnectionState` |
| `HatPosition` | `Input/HatPosition.cs` | `SdlSharp.Input` | `SDL_HAT_*` constants |
| `TouchDeviceType` | `Input/TouchDeviceType.cs` | `SdlSharp.Input` | `SDL_TouchDeviceType` |
| `PenAxis` | `Input/PenAxis.cs` | `SdlSharp.Input` | `SDL_PenAxis` |
| `PenDeviceType` | `Input/PenDeviceType.cs` | `SdlSharp.Input` | `SDL_PenDeviceType` |
| `PenInput` | `Input/PenInput.cs` | `SdlSharp.Input` | `SDL_PEN_INPUT_*` flags |
| `SensorType` | `Input/SensorType.cs` | `SdlSharp.Input` | `SDL_SensorType` |

## New Value Types (3 files)

All `readonly record struct`.

### CameraSpec (`SdlSharp.Graphics.CameraSpec`)

```csharp
public readonly record struct CameraSpec(
    PixelFormat Format, Colorspace Colorspace,
    int Width, int Height,
    int FrameratesNumerator, int FrameratesDenominator);
```

### DialogFileFilter (`SdlSharp.Graphics.DialogFileFilter`)

```csharp
public readonly record struct DialogFileFilter(string Name, string Pattern);
```

### DialogResult (`SdlSharp.Graphics.DialogResult`)

```csharp
public readonly record struct DialogResult(string[]? Files, int FilterIndex);
```

### Finger (`SdlSharp.Input.Finger`)

```csharp
public readonly record struct Finger(long Id, float X, float Y, float Pressure);
```

## Handle Wrappers (5 files)

All follow `sealed unsafe class : IDisposable` with `Handle` pointer, `_ownsHandle` flag, `Common.Check` error handling.

### Camera (`SdlSharp.Graphics.Camera`)

```
static Camera Open(uint id, CameraSpec spec)
static uint[] GetDevices()
static string? GetName(uint id)
static CameraPosition GetPosition(uint id)
static string[] GetDrivers()
static string? GetCurrentDriver()
CameraSpec Format { get; }
CameraSpec[] GetSupportedFormats()
int PermissionState { get; }
Surface? AcquireFrame(out ulong timestampNS)
void ReleaseFrame(Surface frame)
PropertyGroup Properties { get; }
Dispose() — calls SDL_CloseCamera
```

### Gamepad (`SdlSharp.Input.Gamepad`)

```
static Gamepad Open(uint id)
static uint[] GetDevices()
static bool IsGamepad(uint joystickId)
static string? GetName(uint id)
static GamepadType GetType(uint id)
string? Name { get; }
GamepadType Type { get; }
short GetAxis(GamepadAxis axis)
bool GetButton(GamepadButton button)
GamepadButtonLabel GetButtonLabel(GamepadButton button)
JoystickConnectionState ConnectionState { get; }
static void Update()
Dispose() — calls SDL_CloseGamepad
```

### Joystick (`SdlSharp.Input.Joystick`)

```
static Joystick Open(uint id)
static uint[] GetDevices()
static string? GetName(uint id)
static JoystickType GetType(uint id)
string? Name { get; }
JoystickType Type { get; }
int NumAxes { get; }
int NumButtons { get; }
int NumHats { get; }
int NumBalls { get; }
short GetAxis(int index)
bool GetButton(int index)
HatPosition GetHat(int index)
JoystickConnectionState ConnectionState { get; }
static void Update()
Dispose() — calls SDL_CloseJoystick
```

### Haptic (`SdlSharp.Input.Haptic`)

```
static Haptic Open(uint id)
static Haptic OpenFromMouse()
static Haptic OpenFromJoystick(Joystick joystick)
static uint[] GetDevices()
static string? GetName(uint id)
string? Name { get; }
bool RumbleSupported { get; }
void InitRumble()
void PlayRumble(float strength, uint durationMs)
void StopRumble()
Dispose() — calls SDL_CloseHaptic
```

### Sensor (`SdlSharp.Input.Sensor`)

```
static Sensor Open(uint id)
static uint[] GetDevices()
static string? GetName(uint id)
static SensorType GetType(uint id)
string? Name { get; }
SensorType Type { get; }
int NonPortableType { get; }
void GetData(Span<float> data)
PropertyGroup Properties { get; }
static void Update()
Dispose() — calls SDL_CloseSensor
```

## Static Utility Classes (3 files)

### TouchDevice (`SdlSharp.Input.TouchDevice`)

Static class (SDL has no open/close for touch devices).

```
static uint[] GetDevices()
static string? GetName(uint id)
static TouchDeviceType GetType(uint id)
static Finger[] GetFingers(uint id)
```

### PenDevice (`SdlSharp.Input.PenDevice`)

Static class, minimal API.

```
static PenDeviceType GetType(uint id)
```

### SdlDateTime (`SdlSharp.SdlDateTime`)

Static class bridging SDL time to .NET DateTime.

```
static DateFormat GetPreferredDateFormat()
static TimeFormat GetPreferredTimeFormat()
static long GetCurrentTime()
static DateTime ToDateTime(long sdlTime)
static long FromDateTime(DateTime dt)
```

Converts between SDL nanosecond timestamps and .NET `DateTime` internally via `SDL_TimeToDateTime`/`SDL_DateTimeToTime` and the `SDL_DateTime` struct.

## FileDialog (`SdlSharp.Graphics.FileDialog`)

Static class with both async (Task-based) and callback APIs.

### Async API (primary)

```csharp
static Task<DialogResult> OpenFileAsync(Window? parent, DialogFileFilter[]? filters,
    string? defaultLocation, bool allowMany = false)
static Task<DialogResult> SaveFileAsync(Window? parent, DialogFileFilter[]? filters,
    string? defaultLocation)
static Task<DialogResult> OpenFolderAsync(Window? parent,
    string? defaultLocation, bool allowMany = false)
```

Internally uses `TaskCompletionSource<DialogResult>` + `[UnmanagedCallersOnly]` callback + `GCHandle` for userdata. The callback marshals the result filelist and filter index, sets the TCS result, and frees the GCHandle.

### Callback API

```csharp
static void OpenFile(Window? parent, DialogFileFilter[]? filters,
    string? defaultLocation, bool allowMany, Action<DialogResult> callback)
static void SaveFile(Window? parent, DialogFileFilter[]? filters,
    string? defaultLocation, Action<DialogResult> callback)
static void OpenFolder(Window? parent, string? defaultLocation,
    bool allowMany, Action<DialogResult> callback)
```

## Audio Additions (2 files)

### WavData (`SdlSharp.Audio.WavData`)

```csharp
public readonly record struct WavData(AudioSpec Spec, byte[] Data)
{
    public static WavData Load(string path);
}
```

`Load` calls `SDL_LoadWAV`, copies the buffer to managed memory, frees the SDL buffer via `SDL_free`, and returns the struct.

### AudioFormatInfo (`SdlSharp.Audio.AudioFormatInfo`) (new static class)

Since `AudioFormat` is an enum, add a companion static class:

```csharp
public static class AudioFormatInfo
{
    public static string? GetName(AudioFormat format);
}
```

Calls `SDL_GetAudioFormatName` and marshals the returned string.

## Samples

New or extended sample projects in `Samples/`:

- **CameraDemo** — enumerate camera devices, open a camera, acquire and display frames in a window
- **FileDialogDemo** — demonstrate open/save file dialogs with filters
- **GamepadDemo** — open a gamepad, display axes/buttons in real-time
- **HapticDemo** — detect haptic devices and play rumble effects

## File Summary

| Category | Count | Files |
|----------|-------|-------|
| Enums | 16 | New files in `src/SdlSharp/` and `src/SdlSharp/Input/` and `src/SdlSharp/Graphics/` |
| Value types | 4 | `CameraSpec.cs`, `DialogFileFilter.cs`, `DialogResult.cs`, `Finger.cs` |
| Handle wrappers | 5 | `Camera.cs`, `Gamepad.cs`, `Joystick.cs`, `Haptic.cs`, `Sensor.cs` |
| Static classes | 4 | `FileDialog.cs`, `TouchDevice.cs`, `PenDevice.cs`, `SdlDateTime.cs` |
| Audio additions | 2 | `WavData.cs`, `AudioFormatInfo.cs` |
| Samples | 4 | New sample projects |
| **Total** | **31 new files + 4 samples** | |

Post-implementation: update `INVENTORY.md` and `TODO.md` to reflect completion.
