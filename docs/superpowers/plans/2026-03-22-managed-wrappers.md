# Managed Wrappers Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Add idiomatic C# managed wrappers for all SDL3 subsystems that have native P/Invoke bindings but lack high-level wrappers.

**Architecture:** Each subsystem gets enums, value types, and either a handle wrapper (`sealed unsafe class : IDisposable`) or a static utility class, following the patterns established in the existing codebase. All wrappers live in `SdlSharp`, `SdlSharp.Graphics`, `SdlSharp.Input`, or `SdlSharp.Audio` namespaces.

**Tech Stack:** C# 13 / .NET 10.0, `LibraryImport`, unsafe pointers, `GCHandle` for callbacks.

**Spec:** `docs/superpowers/specs/2026-03-22-managed-wrappers-design.md`

---

### Task 1: Audio additions — WavData and AudioFormatInfo

**Files:**
- Create: `src/SdlSharp/Audio/WavData.cs`
- Create: `src/SdlSharp/Audio/AudioFormatInfo.cs`

- [ ] **Step 1: Create WavData record struct**

```csharp
// src/SdlSharp/Audio/WavData.cs
using System.Runtime.InteropServices;

using static SdlSharp.Native.Audio;
using static SdlSharp.Native.Common;

namespace SdlSharp.Audio;

/// <summary>
/// Holds audio data loaded from a WAV file.
/// </summary>
public readonly record struct WavData(AudioSpec Spec, byte[] Data)
{
    /// <summary>
    /// Loads audio data from a WAV file.
    /// </summary>
    /// <param name="path">Path to the WAV file.</param>
    /// <returns>The loaded audio spec and data.</returns>
    public static unsafe WavData Load(string path)
    {
        Native.SDL_AudioSpec spec;
        byte* audioBuf;
        uint audioLen;
        Check(SDL_LoadWAV(ToUtf8(path), &spec, &audioBuf, &audioLen));
        try
        {
            var data = new byte[audioLen];
            new Span<byte>(audioBuf, (int)audioLen).CopyTo(data);
            return new WavData(
                new AudioSpec((AudioFormat)spec.format, spec.channels, spec.freq),
                data);
        }
        finally
        {
            SDL_free(audioBuf);
        }
    }
}
```

- [ ] **Step 2: Create AudioFormatInfo static class**

```csharp
// src/SdlSharp/Audio/AudioFormatInfo.cs
using System.Runtime.InteropServices;

using static SdlSharp.Native.Audio;

namespace SdlSharp.Audio;

/// <summary>
/// Provides information about audio formats.
/// </summary>
public static unsafe class AudioFormatInfo
{
    /// <summary>
    /// Gets the human-readable name of an audio format.
    /// </summary>
    public static string? GetName(AudioFormat format) =>
        Marshal.PtrToStringUTF8((nint)SDL_GetAudioFormatName((Native.SDL_AudioFormat)format));
}
```

- [ ] **Step 3: Build and verify**

Run: `dotnet build src/SdlSharp/`
Expected: 0 errors, 0 warnings

- [ ] **Step 4: Commit**

```bash
git add src/SdlSharp/Audio/WavData.cs src/SdlSharp/Audio/AudioFormatInfo.cs
git commit -m "Add WavData and AudioFormatInfo managed wrappers"
```

---

### Task 2: DateTime/Time enums and SdlDateTime

**Files:**
- Create: `src/SdlSharp/DateFormat.cs`
- Create: `src/SdlSharp/TimeFormat.cs`
- Create: `src/SdlSharp/SdlDateTime.cs`

- [ ] **Step 1: Create DateFormat enum**

```csharp
// src/SdlSharp/DateFormat.cs
namespace SdlSharp;

/// <summary>
/// User's preferred date format.
/// </summary>
public enum DateFormat
{
    YearMonthDay = (int)Native.SDL_DateFormat.SDL_DATE_FORMAT_YYYYMMDD,
    DayMonthYear = (int)Native.SDL_DateFormat.SDL_DATE_FORMAT_DDMMYYYY,
    MonthDayYear = (int)Native.SDL_DateFormat.SDL_DATE_FORMAT_MMDDYYYY,
}
```

- [ ] **Step 2: Create TimeFormat enum**

```csharp
// src/SdlSharp/TimeFormat.cs
namespace SdlSharp;

/// <summary>
/// User's preferred time format.
/// </summary>
public enum TimeFormat
{
    TwentyFourHour = (int)Native.SDL_TimeFormat.SDL_TIME_FORMAT_24HR,
    TwelveHour = (int)Native.SDL_TimeFormat.SDL_TIME_FORMAT_12HR,
}
```

- [ ] **Step 3: Create SdlDateTime static class**

```csharp
// src/SdlSharp/SdlDateTime.cs
using static SdlSharp.Native.Common;
using static SdlSharp.Native.Time;

namespace SdlSharp;

/// <summary>
/// SDL date/time utilities bridging to .NET DateTime.
/// </summary>
public static unsafe class SdlDateTime
{
    /// <summary>
    /// Gets the user's preferred date format.
    /// </summary>
    public static DateFormat GetPreferredDateFormat()
    {
        Native.SDL_DateFormat fmt;
        Check(SDL_GetDateTimeLocalePreferences(&fmt, null));
        return (DateFormat)fmt;
    }

    /// <summary>
    /// Gets the user's preferred time format.
    /// </summary>
    public static TimeFormat GetPreferredTimeFormat()
    {
        Native.SDL_TimeFormat fmt;
        Check(SDL_GetDateTimeLocalePreferences(null, &fmt));
        return (TimeFormat)fmt;
    }

    /// <summary>
    /// Gets the current time as SDL nanosecond ticks since Unix epoch.
    /// </summary>
    public static long GetCurrentTime()
    {
        long ticks;
        Check(SDL_GetCurrentTime(&ticks));
        return ticks;
    }

    /// <summary>
    /// Converts an SDL time value (nanoseconds since Unix epoch) to a .NET DateTime.
    /// </summary>
    /// <param name="sdlTime">SDL time in nanoseconds.</param>
    /// <param name="localTime">If true, convert to local time; otherwise UTC.</param>
    public static DateTime ToDateTime(long sdlTime, bool localTime = false)
    {
        Native.SDL_DateTime dt;
        Check(SDL_TimeToDateTime(sdlTime, &dt, localTime));
        var result = new DateTime(dt.year, dt.month, dt.day, dt.hour, dt.minute, dt.second,
            dt.nanosecond / 1_000_000, localTime ? DateTimeKind.Local : DateTimeKind.Utc);
        return result;
    }

    /// <summary>
    /// Converts a .NET DateTime to an SDL time value (nanoseconds since Unix epoch).
    /// </summary>
    public static long FromDateTime(DateTime dateTime)
    {
        var dt = new Native.SDL_DateTime
        {
            year = dateTime.Year,
            month = dateTime.Month,
            day = dateTime.Day,
            hour = dateTime.Hour,
            minute = dateTime.Minute,
            second = dateTime.Second,
            nanosecond = (int)(dateTime.Ticks % TimeSpan.TicksPerSecond) * 100,
            day_of_week = (int)dateTime.DayOfWeek,
            utc_offset = dateTime.Kind == DateTimeKind.Local
                ? (int)TimeZoneInfo.Local.GetUtcOffset(dateTime).TotalSeconds
                : 0,
        };
        long ticks;
        Check(SDL_DateTimeToTime(&dt, &ticks));
        return ticks;
    }
}
```

- [ ] **Step 4: Build and verify**

Run: `dotnet build src/SdlSharp/`
Expected: 0 errors, 0 warnings

- [ ] **Step 5: Commit**

```bash
git add src/SdlSharp/DateFormat.cs src/SdlSharp/TimeFormat.cs src/SdlSharp/SdlDateTime.cs
git commit -m "Add DateFormat, TimeFormat enums and SdlDateTime utility"
```

---

### Task 3: Joystick enums and wrapper

**Files:**
- Create: `src/SdlSharp/Input/JoystickType.cs`
- Create: `src/SdlSharp/Input/JoystickConnectionState.cs`
- Create: `src/SdlSharp/Input/HatPosition.cs`
- Create: `src/SdlSharp/Input/Joystick.cs`

- [ ] **Step 1: Create JoystickType enum**

```csharp
// src/SdlSharp/Input/JoystickType.cs
namespace SdlSharp.Input;

/// <summary>
/// Common joystick types.
/// </summary>
public enum JoystickType
{
    Unknown = (int)Native.SDL_JoystickType.SDL_JOYSTICK_TYPE_UNKNOWN,
    Gamepad = (int)Native.SDL_JoystickType.SDL_JOYSTICK_TYPE_GAMEPAD,
    Wheel = (int)Native.SDL_JoystickType.SDL_JOYSTICK_TYPE_WHEEL,
    ArcadeStick = (int)Native.SDL_JoystickType.SDL_JOYSTICK_TYPE_ARCADE_STICK,
    FlightStick = (int)Native.SDL_JoystickType.SDL_JOYSTICK_TYPE_FLIGHT_STICK,
    DancePad = (int)Native.SDL_JoystickType.SDL_JOYSTICK_TYPE_DANCE_PAD,
    Guitar = (int)Native.SDL_JoystickType.SDL_JOYSTICK_TYPE_GUITAR,
    DrumKit = (int)Native.SDL_JoystickType.SDL_JOYSTICK_TYPE_DRUM_KIT,
    ArcadePad = (int)Native.SDL_JoystickType.SDL_JOYSTICK_TYPE_ARCADE_PAD,
    Throttle = (int)Native.SDL_JoystickType.SDL_JOYSTICK_TYPE_THROTTLE,
}
```

- [ ] **Step 2: Create JoystickConnectionState enum**

```csharp
// src/SdlSharp/Input/JoystickConnectionState.cs
namespace SdlSharp.Input;

/// <summary>
/// Possible connection states for a joystick device.
/// </summary>
public enum JoystickConnectionState
{
    Invalid = (int)Native.SDL_JoystickConnectionState.SDL_JOYSTICK_CONNECTION_INVALID,
    Unknown = (int)Native.SDL_JoystickConnectionState.SDL_JOYSTICK_CONNECTION_UNKNOWN,
    Wired = (int)Native.SDL_JoystickConnectionState.SDL_JOYSTICK_CONNECTION_WIRED,
    Wireless = (int)Native.SDL_JoystickConnectionState.SDL_JOYSTICK_CONNECTION_WIRELESS,
}
```

- [ ] **Step 3: Create HatPosition flags enum**

```csharp
// src/SdlSharp/Input/HatPosition.cs
namespace SdlSharp.Input;

/// <summary>
/// Joystick hat positions.
/// </summary>
[Flags]
public enum HatPosition : byte
{
    Centered = Native.Joystick.SDL_HAT_CENTERED,
    Up = Native.Joystick.SDL_HAT_UP,
    Right = Native.Joystick.SDL_HAT_RIGHT,
    Down = Native.Joystick.SDL_HAT_DOWN,
    Left = Native.Joystick.SDL_HAT_LEFT,
    RightUp = Native.Joystick.SDL_HAT_RIGHTUP,
    RightDown = Native.Joystick.SDL_HAT_RIGHTDOWN,
    LeftUp = Native.Joystick.SDL_HAT_LEFTUP,
    LeftDown = Native.Joystick.SDL_HAT_LEFTDOWN,
}
```

- [ ] **Step 4: Create Joystick handle wrapper**

```csharp
// src/SdlSharp/Input/Joystick.cs
using System.Runtime.InteropServices;

using static SdlSharp.Native.Common;
using static SdlSharp.Native.Joystick;

namespace SdlSharp.Input;

/// <summary>
/// Managed wrapper for an SDL joystick device.
/// </summary>
public sealed unsafe class Joystick : IDisposable
{
    private readonly bool _ownsHandle;

    internal Native.SDL_Joystick* Handle { get; private set; }

    internal Joystick(Native.SDL_Joystick* handle, bool ownsHandle = true)
    {
        Handle = handle;
        _ownsHandle = ownsHandle;
    }

    /// <summary>Opens a joystick by instance ID.</summary>
    public static Joystick Open(uint id) =>
        new(Check(SDL_OpenJoystick(new Native.SDL_JoystickID(id))));

    /// <summary>Gets the instance IDs of all connected joysticks.</summary>
    public static uint[] GetDevices()
    {
        int count;
        var ids = SDL_GetJoysticks(&count);
        if (ids == null) return [];
        try
        {
            var result = new uint[count];
            for (var i = 0; i < count; i++)
                result[i] = ids[i].Value;
            return result;
        }
        finally
        {
            SDL_free(ids);
        }
    }

    /// <summary>Gets the name of a joystick by instance ID.</summary>
    public static string? GetName(uint id) =>
        Marshal.PtrToStringUTF8((nint)SDL_GetJoystickNameForID(new Native.SDL_JoystickID(id)));

    /// <summary>Gets the type of a joystick by instance ID.</summary>
    public static JoystickType GetType(uint id) =>
        (JoystickType)SDL_GetJoystickTypeForID(new Native.SDL_JoystickID(id));

    /// <summary>Gets the name of this joystick.</summary>
    public string? Name => Marshal.PtrToStringUTF8((nint)SDL_GetJoystickName(Handle));

    /// <summary>Gets the type of this joystick.</summary>
    public JoystickType Type => (JoystickType)SDL_GetJoystickType(Handle);

    /// <summary>Gets the number of axes.</summary>
    public int NumAxes => SDL_GetNumJoystickAxes(Handle);

    /// <summary>Gets the number of trackballs.</summary>
    public int NumBalls => SDL_GetNumJoystickBalls(Handle);

    /// <summary>Gets the number of hats.</summary>
    public int NumHats => SDL_GetNumJoystickHats(Handle);

    /// <summary>Gets the number of buttons.</summary>
    public int NumButtons => SDL_GetNumJoystickButtons(Handle);

    /// <summary>Gets the current value of an axis.</summary>
    public short GetAxis(int index) => SDL_GetJoystickAxis(Handle, index);

    /// <summary>Gets the current position of a hat.</summary>
    public HatPosition GetHat(int index) => (HatPosition)SDL_GetJoystickHat(Handle, index);

    /// <summary>Gets the current state of a button.</summary>
    public bool GetButton(int index) => SDL_GetJoystickButton(Handle, index);

    /// <summary>Gets the connection state of this joystick.</summary>
    public JoystickConnectionState ConnectionState =>
        (JoystickConnectionState)SDL_GetJoystickConnectionState(Handle);

    /// <summary>Updates the state of all open joysticks.</summary>
    public static void Update() => SDL_UpdateJoysticks();

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_ownsHandle && Handle != null)
        {
            SDL_CloseJoystick(Handle);
            Handle = null;
        }
    }
}
```

- [ ] **Step 5: Build and verify**

Run: `dotnet build src/SdlSharp/`
Expected: 0 errors, 0 warnings

- [ ] **Step 6: Commit**

```bash
git add src/SdlSharp/Input/JoystickType.cs src/SdlSharp/Input/JoystickConnectionState.cs \
  src/SdlSharp/Input/HatPosition.cs src/SdlSharp/Input/Joystick.cs
git commit -m "Add Joystick managed wrapper with enums"
```

---

### Task 4: Gamepad enums and wrapper

**Files:**
- Create: `src/SdlSharp/Input/GamepadType.cs`
- Create: `src/SdlSharp/Input/GamepadButton.cs`
- Create: `src/SdlSharp/Input/GamepadButtonLabel.cs`
- Create: `src/SdlSharp/Input/GamepadAxis.cs`
- Create: `src/SdlSharp/Input/Gamepad.cs`

- [ ] **Step 1: Create GamepadType enum**

```csharp
// src/SdlSharp/Input/GamepadType.cs
namespace SdlSharp.Input;

/// <summary>
/// Standard gamepad types.
/// </summary>
public enum GamepadType
{
    Unknown = (int)Native.SDL_GamepadType.SDL_GAMEPAD_TYPE_UNKNOWN,
    Standard = (int)Native.SDL_GamepadType.SDL_GAMEPAD_TYPE_STANDARD,
    Xbox360 = (int)Native.SDL_GamepadType.SDL_GAMEPAD_TYPE_XBOX360,
    XboxOne = (int)Native.SDL_GamepadType.SDL_GAMEPAD_TYPE_XBOXONE,
    PS3 = (int)Native.SDL_GamepadType.SDL_GAMEPAD_TYPE_PS3,
    PS4 = (int)Native.SDL_GamepadType.SDL_GAMEPAD_TYPE_PS4,
    PS5 = (int)Native.SDL_GamepadType.SDL_GAMEPAD_TYPE_PS5,
    NintendoSwitchPro = (int)Native.SDL_GamepadType.SDL_GAMEPAD_TYPE_NINTENDO_SWITCH_PRO,
    NintendoSwitchJoyConLeft = (int)Native.SDL_GamepadType.SDL_GAMEPAD_TYPE_NINTENDO_SWITCH_JOYCON_LEFT,
    NintendoSwitchJoyConRight = (int)Native.SDL_GamepadType.SDL_GAMEPAD_TYPE_NINTENDO_SWITCH_JOYCON_RIGHT,
    NintendoSwitchJoyConPair = (int)Native.SDL_GamepadType.SDL_GAMEPAD_TYPE_NINTENDO_SWITCH_JOYCON_PAIR,
    GameCube = (int)Native.SDL_GamepadType.SDL_GAMEPAD_TYPE_GAMECUBE,
}
```

- [ ] **Step 2: Create GamepadButton enum**

```csharp
// src/SdlSharp/Input/GamepadButton.cs
namespace SdlSharp.Input;

/// <summary>
/// Gamepad buttons.
/// </summary>
public enum GamepadButton
{
    Invalid = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_INVALID,
    South = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_SOUTH,
    East = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_EAST,
    West = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_WEST,
    North = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_NORTH,
    Back = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_BACK,
    Guide = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_GUIDE,
    Start = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_START,
    LeftStick = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_LEFT_STICK,
    RightStick = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_RIGHT_STICK,
    LeftShoulder = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_LEFT_SHOULDER,
    RightShoulder = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_RIGHT_SHOULDER,
    DpadUp = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_DPAD_UP,
    DpadDown = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_DPAD_DOWN,
    DpadLeft = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_DPAD_LEFT,
    DpadRight = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_DPAD_RIGHT,
    Misc1 = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_MISC1,
    RightPaddle1 = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_RIGHT_PADDLE1,
    LeftPaddle1 = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_LEFT_PADDLE1,
    RightPaddle2 = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_RIGHT_PADDLE2,
    LeftPaddle2 = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_LEFT_PADDLE2,
    Touchpad = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_TOUCHPAD,
    Misc2 = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_MISC2,
    Misc3 = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_MISC3,
    Misc4 = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_MISC4,
    Misc5 = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_MISC5,
    Misc6 = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_MISC6,
}
```

- [ ] **Step 3: Create GamepadButtonLabel enum**

```csharp
// src/SdlSharp/Input/GamepadButtonLabel.cs
namespace SdlSharp.Input;

/// <summary>
/// Gamepad button labels (platform-specific face button names).
/// </summary>
public enum GamepadButtonLabel
{
    Unknown = (int)Native.SDL_GamepadButtonLabel.SDL_GAMEPAD_BUTTON_LABEL_UNKNOWN,
    A = (int)Native.SDL_GamepadButtonLabel.SDL_GAMEPAD_BUTTON_LABEL_A,
    B = (int)Native.SDL_GamepadButtonLabel.SDL_GAMEPAD_BUTTON_LABEL_B,
    X = (int)Native.SDL_GamepadButtonLabel.SDL_GAMEPAD_BUTTON_LABEL_X,
    Y = (int)Native.SDL_GamepadButtonLabel.SDL_GAMEPAD_BUTTON_LABEL_Y,
    Cross = (int)Native.SDL_GamepadButtonLabel.SDL_GAMEPAD_BUTTON_LABEL_CROSS,
    Circle = (int)Native.SDL_GamepadButtonLabel.SDL_GAMEPAD_BUTTON_LABEL_CIRCLE,
    Square = (int)Native.SDL_GamepadButtonLabel.SDL_GAMEPAD_BUTTON_LABEL_SQUARE,
    Triangle = (int)Native.SDL_GamepadButtonLabel.SDL_GAMEPAD_BUTTON_LABEL_TRIANGLE,
}
```

- [ ] **Step 4: Create GamepadAxis enum**

```csharp
// src/SdlSharp/Input/GamepadAxis.cs
namespace SdlSharp.Input;

/// <summary>
/// Gamepad axes.
/// </summary>
public enum GamepadAxis
{
    Invalid = (int)Native.SDL_GamepadAxis.SDL_GAMEPAD_AXIS_INVALID,
    LeftX = (int)Native.SDL_GamepadAxis.SDL_GAMEPAD_AXIS_LEFTX,
    LeftY = (int)Native.SDL_GamepadAxis.SDL_GAMEPAD_AXIS_LEFTY,
    RightX = (int)Native.SDL_GamepadAxis.SDL_GAMEPAD_AXIS_RIGHTX,
    RightY = (int)Native.SDL_GamepadAxis.SDL_GAMEPAD_AXIS_RIGHTY,
    LeftTrigger = (int)Native.SDL_GamepadAxis.SDL_GAMEPAD_AXIS_LEFT_TRIGGER,
    RightTrigger = (int)Native.SDL_GamepadAxis.SDL_GAMEPAD_AXIS_RIGHT_TRIGGER,
}
```

- [ ] **Step 5: Create Gamepad handle wrapper**

```csharp
// src/SdlSharp/Input/Gamepad.cs
using System.Runtime.InteropServices;

using static SdlSharp.Native.Common;
using static SdlSharp.Native.Gamepad;

namespace SdlSharp.Input;

/// <summary>
/// Managed wrapper for an SDL gamepad device.
/// </summary>
public sealed unsafe class Gamepad : IDisposable
{
    private readonly bool _ownsHandle;

    internal Native.SDL_Gamepad* Handle { get; private set; }

    internal Gamepad(Native.SDL_Gamepad* handle, bool ownsHandle = true)
    {
        Handle = handle;
        _ownsHandle = ownsHandle;
    }

    /// <summary>Opens a gamepad by joystick instance ID.</summary>
    public static Gamepad Open(uint id) =>
        new(Check(SDL_OpenGamepad(new Native.SDL_JoystickID(id))));

    /// <summary>Gets the joystick instance IDs of all connected gamepads.</summary>
    public static uint[] GetDevices()
    {
        int count;
        var ids = SDL_GetGamepads(&count);
        if (ids == null) return [];
        try
        {
            var result = new uint[count];
            for (var i = 0; i < count; i++)
                result[i] = ids[i].Value;
            return result;
        }
        finally
        {
            SDL_free(ids);
        }
    }

    /// <summary>Returns whether the given joystick instance ID is a gamepad.</summary>
    public static bool IsGamepad(uint joystickId) =>
        SDL_IsGamepad(new Native.SDL_JoystickID(joystickId));

    /// <summary>Gets the name of a gamepad by joystick instance ID.</summary>
    public static string? GetName(uint id) =>
        Marshal.PtrToStringUTF8((nint)SDL_GetGamepadNameForID(new Native.SDL_JoystickID(id)));

    /// <summary>Gets the type of a gamepad by joystick instance ID.</summary>
    public static GamepadType GetType(uint id) =>
        (GamepadType)SDL_GetGamepadTypeForID(new Native.SDL_JoystickID(id));

    /// <summary>Gets the name of this gamepad.</summary>
    public string? Name => Marshal.PtrToStringUTF8((nint)SDL_GetGamepadName(Handle));

    /// <summary>Gets the type of this gamepad.</summary>
    public GamepadType Type => (GamepadType)SDL_GetGamepadType(Handle);

    /// <summary>Gets the current value of an axis.</summary>
    public short GetAxis(GamepadAxis axis) =>
        SDL_GetGamepadAxis(Handle, (Native.SDL_GamepadAxis)axis);

    /// <summary>Gets the current state of a button.</summary>
    public bool GetButton(GamepadButton button) =>
        SDL_GetGamepadButton(Handle, (Native.SDL_GamepadButton)button);

    /// <summary>Gets the label for a button on this gamepad.</summary>
    public GamepadButtonLabel GetButtonLabel(GamepadButton button) =>
        (GamepadButtonLabel)SDL_GetGamepadButtonLabel(Handle, (Native.SDL_GamepadButton)button);

    /// <summary>Gets the connection state of this gamepad.</summary>
    public JoystickConnectionState ConnectionState =>
        (JoystickConnectionState)SDL_GetGamepadConnectionState(Handle);

    /// <summary>Updates the state of all open gamepads.</summary>
    public static void Update() => SDL_UpdateGamepads();

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_ownsHandle && Handle != null)
        {
            SDL_CloseGamepad(Handle);
            Handle = null;
        }
    }
}
```

- [ ] **Step 6: Build and verify**

Run: `dotnet build src/SdlSharp/`
Expected: 0 errors, 0 warnings

- [ ] **Step 7: Commit**

```bash
git add src/SdlSharp/Input/GamepadType.cs src/SdlSharp/Input/GamepadButton.cs \
  src/SdlSharp/Input/GamepadButtonLabel.cs src/SdlSharp/Input/GamepadAxis.cs \
  src/SdlSharp/Input/Gamepad.cs
git commit -m "Add Gamepad managed wrapper with enums"
```

---

### Task 5: Haptic wrapper

**Files:**
- Create: `src/SdlSharp/Input/Haptic.cs`

- [ ] **Step 1: Create Haptic handle wrapper**

```csharp
// src/SdlSharp/Input/Haptic.cs
using System.Runtime.InteropServices;

using static SdlSharp.Native.Common;
using static SdlSharp.Native.Haptic;

namespace SdlSharp.Input;

/// <summary>
/// Managed wrapper for an SDL haptic (force feedback) device.
/// </summary>
public sealed unsafe class Haptic : IDisposable
{
    private readonly bool _ownsHandle;

    internal Native.SDL_Haptic* Handle { get; private set; }

    internal Haptic(Native.SDL_Haptic* handle, bool ownsHandle = true)
    {
        Handle = handle;
        _ownsHandle = ownsHandle;
    }

    /// <summary>Opens a haptic device by instance ID.</summary>
    public static Haptic Open(uint id) =>
        new(Check(SDL_OpenHaptic(new Native.SDL_HapticID(id))));

    /// <summary>Opens the haptic device associated with the mouse.</summary>
    public static Haptic OpenFromMouse() =>
        new(Check(SDL_OpenHapticFromMouse()));

    /// <summary>Opens the haptic device associated with a joystick.</summary>
    public static Haptic OpenFromJoystick(Joystick joystick) =>
        new(Check(SDL_OpenHapticFromJoystick(joystick.Handle)));

    /// <summary>Gets the instance IDs of all connected haptic devices.</summary>
    public static uint[] GetDevices()
    {
        var ids = SDL_GetHaptics(out var count);
        if (ids == null) return [];
        try
        {
            var result = new uint[count];
            for (var i = 0; i < count; i++)
                result[i] = ids[i].Value;
            return result;
        }
        finally
        {
            SDL_free(ids);
        }
    }

    /// <summary>Gets the name of a haptic device by instance ID.</summary>
    public static string? GetName(uint id) =>
        Marshal.PtrToStringUTF8((nint)SDL_GetHapticNameForID(new Native.SDL_HapticID(id)));

    /// <summary>Gets the name of this haptic device.</summary>
    public string? Name => Marshal.PtrToStringUTF8((nint)SDL_GetHapticName(Handle));

    /// <summary>Gets whether simple rumble is supported on this device.</summary>
    public bool RumbleSupported => SDL_HapticRumbleSupported(Handle);

    /// <summary>Initializes the device for simple rumble playback.</summary>
    public void InitRumble() => Check(SDL_InitHapticRumble(Handle));

    /// <summary>Plays a simple rumble effect.</summary>
    /// <param name="strength">Strength from 0.0 to 1.0.</param>
    /// <param name="durationMs">Duration in milliseconds.</param>
    public void PlayRumble(float strength, uint durationMs) =>
        Check(SDL_PlayHapticRumble(Handle, strength, durationMs));

    /// <summary>Stops the simple rumble on this device.</summary>
    public void StopRumble() => Check(SDL_StopHapticRumble(Handle));

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_ownsHandle && Handle != null)
        {
            SDL_CloseHaptic(Handle);
            Handle = null;
        }
    }
}
```

- [ ] **Step 2: Build and verify**

Run: `dotnet build src/SdlSharp/`
Expected: 0 errors, 0 warnings

- [ ] **Step 3: Commit**

```bash
git add src/SdlSharp/Input/Haptic.cs
git commit -m "Add Haptic managed wrapper with rumble API"
```

---

### Task 6: Sensor enums and wrapper

**Files:**
- Create: `src/SdlSharp/Input/SensorType.cs`
- Create: `src/SdlSharp/Input/Sensor.cs`

- [ ] **Step 1: Create SensorType enum**

```csharp
// src/SdlSharp/Input/SensorType.cs
namespace SdlSharp.Input;

/// <summary>
/// Types of sensors.
/// </summary>
public enum SensorType
{
    Invalid = (int)Native.SDL_SensorType.SDL_SENSOR_INVALID,
    Unknown = (int)Native.SDL_SensorType.SDL_SENSOR_UNKNOWN,
    Accelerometer = (int)Native.SDL_SensorType.SDL_SENSOR_ACCEL,
    Gyroscope = (int)Native.SDL_SensorType.SDL_SENSOR_GYRO,
    AccelerometerLeft = (int)Native.SDL_SensorType.SDL_SENSOR_ACCEL_L,
    GyroscopeLeft = (int)Native.SDL_SensorType.SDL_SENSOR_GYRO_L,
    AccelerometerRight = (int)Native.SDL_SensorType.SDL_SENSOR_ACCEL_R,
    GyroscopeRight = (int)Native.SDL_SensorType.SDL_SENSOR_GYRO_R,
}
```

- [ ] **Step 2: Create Sensor handle wrapper**

```csharp
// src/SdlSharp/Input/Sensor.cs
using System.Runtime.InteropServices;

using static SdlSharp.Native.Common;
using static SdlSharp.Native.Sensor;

namespace SdlSharp.Input;

/// <summary>
/// Managed wrapper for an SDL sensor device.
/// </summary>
public sealed unsafe class Sensor : IDisposable
{
    private readonly bool _ownsHandle;

    internal Native.SDL_Sensor* Handle { get; private set; }

    internal Sensor(Native.SDL_Sensor* handle, bool ownsHandle = true)
    {
        Handle = handle;
        _ownsHandle = ownsHandle;
    }

    /// <summary>Opens a sensor by instance ID.</summary>
    public static Sensor Open(uint id) =>
        new(Check(SDL_OpenSensor(id)));

    /// <summary>Gets the instance IDs of all connected sensors.</summary>
    public static uint[] GetDevices()
    {
        int count;
        var ids = SDL_GetSensors(&count);
        if (ids == null) return [];
        try
        {
            var result = new uint[count];
            for (var i = 0; i < count; i++)
                result[i] = ids[i];
            return result;
        }
        finally
        {
            SDL_free(ids);
        }
    }

    /// <summary>Gets the name of a sensor by instance ID.</summary>
    public static string? GetName(uint id) =>
        Marshal.PtrToStringUTF8((nint)SDL_GetSensorNameForID(id));

    /// <summary>Gets the type of a sensor by instance ID.</summary>
    public static SensorType GetType(uint id) =>
        (SensorType)SDL_GetSensorTypeForID(id);

    /// <summary>Gets the name of this sensor.</summary>
    public string? Name => Marshal.PtrToStringUTF8((nint)SDL_GetSensorName(Handle));

    /// <summary>Gets the type of this sensor.</summary>
    public SensorType Type => (SensorType)SDL_GetSensorType(Handle);

    /// <summary>Gets the non-portable type of this sensor.</summary>
    public int NonPortableType => SDL_GetSensorNonPortableType(Handle);

    /// <summary>Gets the current sensor data.</summary>
    /// <param name="data">A span to receive the sensor data values.</param>
    public void GetData(Span<float> data)
    {
        fixed (float* ptr = data)
            Check(SDL_GetSensorData(Handle, ptr, data.Length));
    }

    /// <summary>Gets the properties associated with this sensor.</summary>
    public PropertyGroup Properties =>
        new(CheckId(SDL_GetSensorProperties(Handle)), ownsHandle: false);

    /// <summary>Updates the state of all open sensors.</summary>
    public static void Update() => SDL_UpdateSensors();

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_ownsHandle && Handle != null)
        {
            SDL_CloseSensor(Handle);
            Handle = null;
        }
    }
}
```

- [ ] **Step 3: Build and verify**

Run: `dotnet build src/SdlSharp/`
Expected: 0 errors, 0 warnings

- [ ] **Step 4: Commit**

```bash
git add src/SdlSharp/Input/SensorType.cs src/SdlSharp/Input/Sensor.cs
git commit -m "Add Sensor managed wrapper with SensorType enum"
```

---

### Task 7: Touch and Pen wrappers

**Files:**
- Create: `src/SdlSharp/Input/TouchDeviceType.cs`
- Create: `src/SdlSharp/Input/Finger.cs`
- Create: `src/SdlSharp/Input/TouchDevice.cs`
- Create: `src/SdlSharp/Input/PenAxis.cs`
- Create: `src/SdlSharp/Input/PenDeviceType.cs`
- Create: `src/SdlSharp/Input/PenInput.cs`
- Create: `src/SdlSharp/Input/PenDevice.cs`

- [ ] **Step 1: Create TouchDeviceType enum**

```csharp
// src/SdlSharp/Input/TouchDeviceType.cs
namespace SdlSharp.Input;

/// <summary>
/// Types of touch devices.
/// </summary>
public enum TouchDeviceType
{
    Invalid = (int)Native.SDL_TouchDeviceType.SDL_TOUCH_DEVICE_INVALID,
    Direct = (int)Native.SDL_TouchDeviceType.SDL_TOUCH_DEVICE_DIRECT,
    IndirectAbsolute = (int)Native.SDL_TouchDeviceType.SDL_TOUCH_DEVICE_INDIRECT_ABSOLUTE,
    IndirectRelative = (int)Native.SDL_TouchDeviceType.SDL_TOUCH_DEVICE_INDIRECT_RELATIVE,
}
```

- [ ] **Step 2: Create Finger value type**

```csharp
// src/SdlSharp/Input/Finger.cs
namespace SdlSharp.Input;

/// <summary>
/// Data about a single touch finger.
/// </summary>
public readonly record struct Finger(long Id, float X, float Y, float Pressure);
```

- [ ] **Step 3: Create TouchDevice static class**

```csharp
// src/SdlSharp/Input/TouchDevice.cs
using System.Runtime.InteropServices;

using static SdlSharp.Native.Common;
using static SdlSharp.Native.Touch;

namespace SdlSharp.Input;

/// <summary>
/// Static methods for querying touch devices.
/// </summary>
public static unsafe class TouchDevice
{
    /// <summary>Gets the IDs of all connected touch devices.</summary>
    public static ulong[] GetDevices()
    {
        int count;
        var ids = SDL_GetTouchDevices(&count);
        if (ids == null) return [];
        try
        {
            var result = new ulong[count];
            for (var i = 0; i < count; i++)
                result[i] = ids[i];
            return result;
        }
        finally
        {
            SDL_free(ids);
        }
    }

    /// <summary>Gets the name of a touch device.</summary>
    public static string? GetName(ulong id) =>
        Marshal.PtrToStringUTF8((nint)SDL_GetTouchDeviceName(id));

    /// <summary>Gets the type of a touch device.</summary>
    public static TouchDeviceType GetType(ulong id) =>
        (TouchDeviceType)SDL_GetTouchDeviceType(id);

    /// <summary>Gets all active fingers on a touch device.</summary>
    public static Finger[] GetFingers(ulong id)
    {
        int count;
        var fingers = SDL_GetTouchFingers(id, &count);
        if (fingers == null) return [];
        try
        {
            var result = new Finger[count];
            for (var i = 0; i < count; i++)
            {
                var f = fingers[i];
                result[i] = new Finger((long)f->id, f->x, f->y, f->pressure);
            }
            return result;
        }
        finally
        {
            SDL_free(fingers);
        }
    }
}
```

- [ ] **Step 4: Create Pen enums**

```csharp
// src/SdlSharp/Input/PenAxis.cs
namespace SdlSharp.Input;

/// <summary>
/// Pen axis indices.
/// </summary>
public enum PenAxis
{
    Pressure = (int)Native.SDL_PenAxis.SDL_PEN_AXIS_PRESSURE,
    XTilt = (int)Native.SDL_PenAxis.SDL_PEN_AXIS_XTILT,
    YTilt = (int)Native.SDL_PenAxis.SDL_PEN_AXIS_YTILT,
    Distance = (int)Native.SDL_PenAxis.SDL_PEN_AXIS_DISTANCE,
    Rotation = (int)Native.SDL_PenAxis.SDL_PEN_AXIS_ROTATION,
    Slider = (int)Native.SDL_PenAxis.SDL_PEN_AXIS_SLIDER,
    TangentialPressure = (int)Native.SDL_PenAxis.SDL_PEN_AXIS_TANGENTIAL_PRESSURE,
}
```

```csharp
// src/SdlSharp/Input/PenDeviceType.cs
namespace SdlSharp.Input;

/// <summary>
/// Types of pen devices.
/// </summary>
public enum PenDeviceType
{
    Invalid = (int)Native.SDL_PenDeviceType.SDL_PEN_DEVICE_TYPE_INVALID,
    Unknown = (int)Native.SDL_PenDeviceType.SDL_PEN_DEVICE_TYPE_UNKNOWN,
    Direct = (int)Native.SDL_PenDeviceType.SDL_PEN_DEVICE_TYPE_DIRECT,
    Indirect = (int)Native.SDL_PenDeviceType.SDL_PEN_DEVICE_TYPE_INDIRECT,
}
```

```csharp
// src/SdlSharp/Input/PenInput.cs
namespace SdlSharp.Input;

/// <summary>
/// Pen input flags.
/// </summary>
[Flags]
public enum PenInput : uint
{
    Down = Native.Pen.SDL_PEN_INPUT_DOWN,
    Button1 = Native.Pen.SDL_PEN_INPUT_BUTTON_1,
    Button2 = Native.Pen.SDL_PEN_INPUT_BUTTON_2,
    Button3 = Native.Pen.SDL_PEN_INPUT_BUTTON_3,
    Button4 = Native.Pen.SDL_PEN_INPUT_BUTTON_4,
    Button5 = Native.Pen.SDL_PEN_INPUT_BUTTON_5,
    EraserTip = Native.Pen.SDL_PEN_INPUT_ERASER_TIP,
    InProximity = Native.Pen.SDL_PEN_INPUT_IN_PROXIMITY,
}
```

- [ ] **Step 5: Create PenDevice static class**

```csharp
// src/SdlSharp/Input/PenDevice.cs
using static SdlSharp.Native.Pen;

namespace SdlSharp.Input;

/// <summary>
/// Static methods for querying pen devices.
/// </summary>
public static class PenDevice
{
    /// <summary>Gets the type of a pen device.</summary>
    public static PenDeviceType GetType(uint id) =>
        (PenDeviceType)SDL_GetPenDeviceType(id);
}
```

- [ ] **Step 6: Build and verify**

Run: `dotnet build src/SdlSharp/`
Expected: 0 errors, 0 warnings

- [ ] **Step 7: Commit**

```bash
git add src/SdlSharp/Input/TouchDeviceType.cs src/SdlSharp/Input/Finger.cs \
  src/SdlSharp/Input/TouchDevice.cs src/SdlSharp/Input/PenAxis.cs \
  src/SdlSharp/Input/PenDeviceType.cs src/SdlSharp/Input/PenInput.cs \
  src/SdlSharp/Input/PenDevice.cs
git commit -m "Add Touch and Pen managed wrappers with enums"
```

---

### Task 8: Camera enums, value types, and wrapper

**Files:**
- Create: `src/SdlSharp/Graphics/CameraPosition.cs`
- Create: `src/SdlSharp/Graphics/CameraSpec.cs`
- Create: `src/SdlSharp/Graphics/Camera.cs`

- [ ] **Step 1: Create CameraPosition enum**

```csharp
// src/SdlSharp/Graphics/CameraPosition.cs
namespace SdlSharp.Graphics;

/// <summary>
/// Camera position relative to the device.
/// </summary>
public enum CameraPosition
{
    Unknown = (int)Native.SDL_CameraPosition.SDL_CAMERA_POSITION_UNKNOWN,
    FrontFacing = (int)Native.SDL_CameraPosition.SDL_CAMERA_POSITION_FRONT_FACING,
    BackFacing = (int)Native.SDL_CameraPosition.SDL_CAMERA_POSITION_BACK_FACING,
}
```

- [ ] **Step 2: Create CameraSpec value type**

```csharp
// src/SdlSharp/Graphics/CameraSpec.cs
namespace SdlSharp.Graphics;

/// <summary>
/// Camera format and resolution specification.
/// </summary>
public readonly record struct CameraSpec(
    PixelFormat Format,
    Colorspace Colorspace,
    int Width,
    int Height,
    int FramerateNumerator,
    int FramerateDenominator);
```

- [ ] **Step 3: Create Camera handle wrapper**

```csharp
// src/SdlSharp/Graphics/Camera.cs
using System.Runtime.InteropServices;
using SdlSharp.Native;

using static SdlSharp.Native.Common;
using static SdlSharp.Native.Camera;

namespace SdlSharp.Graphics;

/// <summary>
/// Managed wrapper for an SDL camera device.
/// </summary>
public sealed unsafe class Camera : IDisposable
{
    private readonly bool _ownsHandle;

    internal SDL_Camera* Handle { get; private set; }

    internal Camera(SDL_Camera* handle, bool ownsHandle = true)
    {
        Handle = handle;
        _ownsHandle = ownsHandle;
    }

    /// <summary>Opens a camera device.</summary>
    /// <param name="id">The camera instance ID.</param>
    /// <param name="spec">Desired camera spec, or null for default.</param>
    public static Camera Open(uint id, CameraSpec? spec = null)
    {
        SDL_CameraSpec nativeSpec;
        SDL_CameraSpec* specPtr = null;
        if (spec is { } s)
        {
            nativeSpec = new SDL_CameraSpec
            {
                format = (SDL_PixelFormat)s.Format,
                colorspace = (SDL_Colorspace)s.Colorspace,
                width = s.Width,
                height = s.Height,
                framerate_numerator = s.FramerateNumerator,
                framerate_denominator = s.FramerateDenominator,
            };
            specPtr = &nativeSpec;
        }

        return new Camera(Check(SDL_OpenCamera(new SDL_CameraID(id), specPtr)));
    }

    /// <summary>Gets the instance IDs of all connected cameras.</summary>
    public static uint[] GetDevices()
    {
        var ids = SDL_GetCameras(out var count);
        if (ids == null) return [];
        try
        {
            var result = new uint[count];
            for (var i = 0; i < count; i++)
                result[i] = ids[i].Value;
            return result;
        }
        finally
        {
            SDL_free(ids);
        }
    }

    /// <summary>Gets the name of a camera by instance ID.</summary>
    public static string? GetName(uint id) =>
        Marshal.PtrToStringUTF8((nint)SDL_GetCameraName(new SDL_CameraID(id)));

    /// <summary>Gets the position of a camera by instance ID.</summary>
    public static CameraPosition GetPosition(uint id) =>
        (CameraPosition)SDL_GetCameraPosition(new SDL_CameraID(id));

    /// <summary>Gets the supported formats for a camera by instance ID.</summary>
    public static CameraSpec[] GetSupportedFormats(uint id)
    {
        var specs = SDL_GetCameraSupportedFormats(new SDL_CameraID(id), out var count);
        if (specs == null) return [];
        try
        {
            var result = new CameraSpec[count];
            for (var i = 0; i < count; i++)
                result[i] = MarshalSpec(specs[i]);
            return result;
        }
        finally
        {
            SDL_free(specs);
        }
    }

    /// <summary>Gets the names of all built-in camera drivers.</summary>
    public static string[] GetDrivers()
    {
        var count = SDL_GetNumCameraDrivers();
        var result = new string[count];
        for (var i = 0; i < count; i++)
            result[i] = Marshal.PtrToStringUTF8((nint)SDL_GetCameraDriver(i)) ?? "";
        return result;
    }

    /// <summary>Gets the name of the current camera driver.</summary>
    public static string? GetCurrentDriver() =>
        Marshal.PtrToStringUTF8((nint)SDL_GetCurrentCameraDriver());

    /// <summary>Gets the current format of this camera.</summary>
    public CameraSpec Format
    {
        get
        {
            SDL_CameraSpec spec;
            Check(SDL_GetCameraFormat(Handle, &spec));
            return MarshalSpec(&spec);
        }
    }

    /// <summary>Gets the permission state of this camera. 0 = pending, 1 = approved, -1 = denied.</summary>
    public int PermissionState => SDL_GetCameraPermissionState(Handle);

    /// <summary>Gets the properties associated with this camera.</summary>
    public PropertyGroup Properties =>
        new(CheckId(SDL_GetCameraProperties(Handle)), ownsHandle: false);

    /// <summary>
    /// Acquires a video frame from the camera.
    /// Returns null if no frame is available yet.
    /// The returned surface must be released with <see cref="ReleaseFrame"/>.
    /// </summary>
    /// <param name="timestampNS">Receives the frame timestamp in nanoseconds.</param>
    public Surface? AcquireFrame(out ulong timestampNS)
    {
        ulong ts;
        var surface = SDL_AcquireCameraFrame(Handle, &ts);
        timestampNS = ts;
        return surface == null ? null : new Surface(surface, ownsHandle: false);
    }

    /// <summary>
    /// Releases a frame previously acquired with <see cref="AcquireFrame"/>.
    /// </summary>
    public void ReleaseFrame(Surface frame) =>
        SDL_ReleaseCameraFrame(Handle, frame.Handle);

    private static CameraSpec MarshalSpec(SDL_CameraSpec* s) =>
        new((PixelFormat)s->format, (Colorspace)s->colorspace,
            s->width, s->height, s->framerate_numerator, s->framerate_denominator);

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_ownsHandle && Handle != null)
        {
            SDL_CloseCamera(Handle);
            Handle = null;
        }
    }
}
```

- [ ] **Step 4: Build and verify**

Run: `dotnet build src/SdlSharp/`
Expected: 0 errors, 0 warnings

- [ ] **Step 5: Commit**

```bash
git add src/SdlSharp/Graphics/CameraPosition.cs src/SdlSharp/Graphics/CameraSpec.cs \
  src/SdlSharp/Graphics/Camera.cs
git commit -m "Add Camera managed wrapper with CameraSpec and CameraPosition"
```

---

### Task 9: FileDialog enums, value types, and wrapper

**Files:**
- Create: `src/SdlSharp/Graphics/FileDialogType.cs`
- Create: `src/SdlSharp/Graphics/DialogFileFilter.cs`
- Create: `src/SdlSharp/Graphics/DialogResult.cs`
- Create: `src/SdlSharp/Graphics/FileDialog.cs`

- [ ] **Step 1: Create FileDialogType enum**

```csharp
// src/SdlSharp/Graphics/FileDialogType.cs
namespace SdlSharp.Graphics;

/// <summary>
/// File dialog type.
/// </summary>
public enum FileDialogType
{
    OpenFile = (int)Native.SDL_FileDialogType.SDL_FILEDIALOG_OPENFILE,
    SaveFile = (int)Native.SDL_FileDialogType.SDL_FILEDIALOG_SAVEFILE,
    OpenFolder = (int)Native.SDL_FileDialogType.SDL_FILEDIALOG_OPENFOLDER,
}
```

- [ ] **Step 2: Create DialogFileFilter and DialogResult value types**

```csharp
// src/SdlSharp/Graphics/DialogFileFilter.cs
namespace SdlSharp.Graphics;

/// <summary>
/// A file filter for dialog operations.
/// </summary>
/// <param name="Name">Human-readable label (e.g. "Image files").</param>
/// <param name="Pattern">Semicolon-separated extensions (e.g. "png;jpg;bmp").</param>
public readonly record struct DialogFileFilter(string Name, string Pattern);
```

```csharp
// src/SdlSharp/Graphics/DialogResult.cs
namespace SdlSharp.Graphics;

/// <summary>
/// Result from a file dialog operation.
/// </summary>
/// <param name="Files">Selected file paths, or null if cancelled.</param>
/// <param name="FilterIndex">Index of the filter the user selected.</param>
public readonly record struct DialogResult(string[]? Files, int FilterIndex);
```

- [ ] **Step 3: Create FileDialog static class**

```csharp
// src/SdlSharp/Graphics/FileDialog.cs
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SdlSharp.Native;

using static SdlSharp.Native.Common;
using static SdlSharp.Native.Dialog;

namespace SdlSharp.Graphics;

/// <summary>
/// File open/save/folder dialogs.
/// </summary>
public static unsafe class FileDialog
{
    // --- Async API ---

    /// <summary>Shows an "Open File" dialog asynchronously.</summary>
    public static Task<DialogResult> OpenFileAsync(
        Window? parent = null,
        DialogFileFilter[]? filters = null,
        string? defaultLocation = null,
        bool allowMany = false)
    {
        var tcs = new TaskCompletionSource<DialogResult>();
        OpenFile(parent, filters, defaultLocation, allowMany, result => tcs.SetResult(result));
        return tcs.Task;
    }

    /// <summary>Shows a "Save File" dialog asynchronously.</summary>
    public static Task<DialogResult> SaveFileAsync(
        Window? parent = null,
        DialogFileFilter[]? filters = null,
        string? defaultLocation = null)
    {
        var tcs = new TaskCompletionSource<DialogResult>();
        SaveFile(parent, filters, defaultLocation, result => tcs.SetResult(result));
        return tcs.Task;
    }

    /// <summary>Shows an "Open Folder" dialog asynchronously.</summary>
    public static Task<DialogResult> OpenFolderAsync(
        Window? parent = null,
        string? defaultLocation = null,
        bool allowMany = false)
    {
        var tcs = new TaskCompletionSource<DialogResult>();
        OpenFolder(parent, defaultLocation, allowMany, result => tcs.SetResult(result));
        return tcs.Task;
    }

    // --- Callback API ---

    /// <summary>Shows an "Open File" dialog with a callback.</summary>
    public static void OpenFile(
        Window? parent,
        DialogFileFilter[]? filters,
        string? defaultLocation,
        bool allowMany,
        Action<DialogResult> callback)
    {
        var handle = GCHandle.Alloc(callback);
        var nativeFilters = MarshalFilters(filters, out var pins);
        try
        {
            fixed (SDL_DialogFileFilter* filterPtr = nativeFilters)
            {
                SDL_ShowOpenFileDialog(
                    &DialogCallback,
                    (void*)(nint)handle,
                    parent != null ? parent.Handle : null,
                    filterPtr,
                    nativeFilters?.Length ?? 0,
                    ToUtf8(defaultLocation),
                    allowMany);
            }
        }
        finally
        {
            FreePins(pins);
        }
    }

    /// <summary>Shows a "Save File" dialog with a callback.</summary>
    public static void SaveFile(
        Window? parent,
        DialogFileFilter[]? filters,
        string? defaultLocation,
        Action<DialogResult> callback)
    {
        var handle = GCHandle.Alloc(callback);
        var nativeFilters = MarshalFilters(filters, out var pins);
        try
        {
            fixed (SDL_DialogFileFilter* filterPtr = nativeFilters)
            {
                SDL_ShowSaveFileDialog(
                    &DialogCallback,
                    (void*)(nint)handle,
                    parent != null ? parent.Handle : null,
                    filterPtr,
                    nativeFilters?.Length ?? 0,
                    ToUtf8(defaultLocation));
            }
        }
        finally
        {
            FreePins(pins);
        }
    }

    /// <summary>Shows an "Open Folder" dialog with a callback.</summary>
    public static void OpenFolder(
        Window? parent,
        string? defaultLocation,
        bool allowMany,
        Action<DialogResult> callback)
    {
        var handle = GCHandle.Alloc(callback);
        SDL_ShowOpenFolderDialog(
            &DialogCallback,
            (void*)(nint)handle,
            parent != null ? parent.Handle : null,
            ToUtf8(defaultLocation),
            allowMany);
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void DialogCallback(void* userdata, byte** filelist, int filter)
    {
        var handle = GCHandle.FromIntPtr((nint)userdata);
        var callback = (Action<DialogResult>)handle.Target!;
        try
        {
            string[]? files = null;
            if (filelist != null)
            {
                var list = new List<string>();
                for (var i = 0; filelist[i] != null; i++)
                {
                    var s = Marshal.PtrToStringUTF8((nint)filelist[i]);
                    if (s != null) list.Add(s);
                }
                files = list.Count > 0 ? list.ToArray() : null;
            }
            callback(new DialogResult(files, filter));
        }
        finally
        {
            handle.Free();
        }
    }

    private static SDL_DialogFileFilter[]? MarshalFilters(
        DialogFileFilter[]? filters, out GCHandle[] pins)
    {
        if (filters == null || filters.Length == 0)
        {
            pins = [];
            return null;
        }

        var native = new SDL_DialogFileFilter[filters.Length];
        pins = new GCHandle[filters.Length * 2];
        for (var i = 0; i < filters.Length; i++)
        {
            var nameBytes = ToUtf8(filters[i].Name)!;
            var patternBytes = ToUtf8(filters[i].Pattern)!;
            pins[i * 2] = GCHandle.Alloc(nameBytes, GCHandleType.Pinned);
            pins[i * 2 + 1] = GCHandle.Alloc(patternBytes, GCHandleType.Pinned);
            native[i].name = (byte*)pins[i * 2].AddrOfPinnedObject();
            native[i].pattern = (byte*)pins[i * 2 + 1].AddrOfPinnedObject();
        }
        return native;
    }

    private static void FreePins(GCHandle[] pins)
    {
        foreach (var pin in pins)
            if (pin.IsAllocated) pin.Free();
    }
}
```

- [ ] **Step 4: Build and verify**

Run: `dotnet build src/SdlSharp/`
Expected: 0 errors, 0 warnings

- [ ] **Step 5: Commit**

```bash
git add src/SdlSharp/Graphics/FileDialogType.cs src/SdlSharp/Graphics/DialogFileFilter.cs \
  src/SdlSharp/Graphics/DialogResult.cs src/SdlSharp/Graphics/FileDialog.cs
git commit -m "Add FileDialog managed wrapper with async and callback APIs"
```

---

### Task 10: Update INVENTORY.md and TODO.md

**Files:**
- Modify: `INVENTORY.md`
- Modify: `TODO.md`

- [ ] **Step 1: Update INVENTORY.md**

For each subsystem that now has a managed wrapper, update the "High-level" column from "-" to the wrapper class name. Sections to update:

- `SDL_audio.h`: `SDL_LoadWAV` → `WavData.Load`, `SDL_GetAudioFormatName` → `AudioFormatInfo.GetName`
- `SDL_camera.h`: All camera functions → `Camera` class
- `SDL_dialog.h`: All dialog functions → `FileDialog` class
- `SDL_gamepad.h`: All gamepad functions → `Gamepad` class
- `SDL_haptic.h`: All haptic functions → `Haptic` class
- `SDL_joystick.h`: All joystick functions → `Joystick` class
- `SDL_pen.h`: `SDL_GetPenDeviceType` → `PenDevice.GetType`
- `SDL_sensor.h`: All sensor functions → `Sensor` class
- `SDL_time.h`: All time functions → `SdlDateTime` class
- `SDL_touch.h`: All touch functions → `TouchDevice` class

- [ ] **Step 2: Update TODO.md**

Add a new completed section summarizing what was wrapped. Update the deferred input wrappers list to reflect that Gamepad, Joystick, Touch, Pen, and Sensor are now done.

- [ ] **Step 3: Build full solution**

Run: `dotnet build`
Expected: 0 errors, 0 warnings for entire solution

- [ ] **Step 4: Commit**

```bash
git add INVENTORY.md TODO.md
git commit -m "Update INVENTORY.md and TODO.md with new managed wrappers"
```

---

### Task 11: Sample projects

**Files:**
- Create: `Samples/CameraDemo/CameraDemo.csproj`
- Create: `Samples/CameraDemo/Program.cs`
- Create: `Samples/FileDialogDemo/FileDialogDemo.csproj`
- Create: `Samples/FileDialogDemo/Program.cs`
- Create: `Samples/GamepadDemo/GamepadDemo.csproj`
- Create: `Samples/GamepadDemo/Program.cs`
- Create: `Samples/HapticDemo/HapticDemo.csproj`
- Create: `Samples/HapticDemo/Program.cs`

- [ ] **Step 1: Check existing sample structure**

Read an existing sample's `.csproj` and `Program.cs` for the pattern (e.g., `Samples/ImGuiDemo/`).

- [ ] **Step 2: Create CameraDemo sample**

A simple program that:
- Initializes SDL with video+camera
- Enumerates camera devices and prints names
- Opens the first camera
- Waits for permission
- Acquires frames in a loop and blits to a window
- Runs until quit

- [ ] **Step 3: Create FileDialogDemo sample**

A simple program that:
- Initializes SDL with video
- Creates a window
- Opens a file dialog with filters (e.g., images, text files)
- Prints selected files to console
- Demonstrates both async and callback APIs

- [ ] **Step 4: Create GamepadDemo sample**

A simple program that:
- Initializes SDL with gamepad subsystem
- Polls for gamepad connection
- Opens the first gamepad
- Displays axis/button state in a text render loop (or console output)
- Runs until quit

- [ ] **Step 5: Create HapticDemo sample**

A simple program that:
- Initializes SDL with joystick/haptic subsystems
- Enumerates haptic devices
- Opens the first haptic device (or from mouse)
- Initializes rumble and plays a test pattern
- Runs until quit

- [ ] **Step 6: Build samples**

Run: `dotnet build Samples/CameraDemo/ && dotnet build Samples/FileDialogDemo/ && dotnet build Samples/GamepadDemo/ && dotnet build Samples/HapticDemo/`
Expected: 0 errors, 0 warnings

- [ ] **Step 7: Commit**

```bash
git add Samples/CameraDemo/ Samples/FileDialogDemo/ Samples/GamepadDemo/ Samples/HapticDemo/
git commit -m "Add CameraDemo, FileDialogDemo, GamepadDemo, and HapticDemo samples"
```
