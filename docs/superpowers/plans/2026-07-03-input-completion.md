# Phase 8 Input Completion Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Complete the gamepad (15/73 → full), joystick (18/58 → full), and Scancode/Keycode public surfaces per the spec at `docs/superpowers/specs/2026-07-03-input-completion-design.md`.

**Architecture:** New `SdlGuid` foundation shared by both device classes; native bindings appended to the existing `Native/Gamepad.cs`/`Native/Joystick.cs` (new `Native/Guid.cs`); managed members follow the existing instance/static split in `Input/Gamepad.cs`/`Input/Joystick.cs`, which both gain `_ownsHandle` for the new non-owning `FromId`/`FromPlayerIndex` factories. Enum completion uses the EventType derivation-rules + parity-count method.

**Tech Stack:** C# 13 / .NET 10, LibraryImport P/Invoke.

**Source of truth for missing symbols:** `audit/sdl3-coverage-2026-07.json` (SDL_gamepad.h / SDL_joystick.h entries) and the headers `/Users/paulv/Projects/SDL/include/SDL3/SDL_gamepad.h` (73 functions) and `SDL_joystick.h` (58 functions). VERIFY every signature you write against the header — the audit lists what's missing; the header defines what's true.

**Binding boilerplate recipe (used by Tasks 2-3; shown once):** every new binding gets exactly the file's existing attribute triple:

```csharp
[LibraryImport(Common.Sdl3, EntryPoint = "SDL_ExactCName")]
[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
[return: MarshalAs(UnmanagedType.U1)]   // ONLY on bool returns
public static unsafe partial <C# signature>;
```

C string params → `ReadOnlySpan<byte>` (call sites use `Common.ToUtf8`); returned C strings → `byte*`; by-value SDL_GUID params/returns use the `SDL_GUID` struct from Task 1. `unsafe` only where pointers appear.

**Gates:** every task ends with `dotnet build SdlSharp.slnx` → `0 Warning(s), 0 Error(s)` (Native layer is CS1591-exempt; all public members need docs). Commit messages as given, NO Co-Authored-By.

---

### Task 1: SdlGuid foundation

**Files:**
- Create: `src/SdlSharp/Native/Guid.cs`
- Create: `src/SdlSharp/SdlGuid.cs`

- [ ] **Step 1.1: Create `src/SdlSharp/Native/Guid.cs`**

```csharp
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace SdlSharp.Native;

/// <summary>
/// A 128-bit device GUID (bus type, vendor, product encoding — not an RFC 4122 UUID).
/// Stored as two ulongs for blittability; layout-identical to the C Uint8 data[16].
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_GUID
{
    public ulong data0;
    public ulong data1;
}

/// <summary>
/// Native bindings for SDL_guid.h.
/// </summary>
public static partial class Guid
{
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GUIDToString")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_GUIDToString(SDL_GUID guid, byte* pszGUID, int cbGUID);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_StringToGUID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_GUID SDL_StringToGUID(ReadOnlySpan<byte> pchGUID);
}
```

Check the `[SuppressMessage("Interoperability", "CA1401...")]` attribute usage on sibling native classes (e.g. Native/Keyboard.cs) and add it to the `Guid` class if the codebase applies it uniformly.

- [ ] **Step 1.2: Create `src/SdlSharp/SdlGuid.cs`**

```csharp
using System.Text;
using static SdlSharp.Native.Common;
using static SdlSharp.Native.Guid;

namespace SdlSharp;

/// <summary>
/// A 128-bit SDL device GUID identifying a joystick or gamepad model
/// (an encoding of bus type, vendor, and product — not an RFC 4122 UUID).
/// Its canonical string form is the 32-character lowercase hex string used by
/// the SDL controller mapping database (gamecontrollerdb.txt).
/// </summary>
public readonly record struct SdlGuid
{
    private readonly ulong _data0;
    private readonly ulong _data1;

    internal SdlGuid(Native.SDL_GUID native)
    {
        _data0 = native.data0;
        _data1 = native.data1;
    }

    internal Native.SDL_GUID ToNative() => new() { data0 = _data0, data1 = _data1 };

    /// <summary>
    /// Gets whether this is the zero GUID (returned by SDL for invalid devices).
    /// </summary>
    public bool IsZero => _data0 == 0 && _data1 == 0;

    /// <summary>
    /// Parses a GUID from its canonical 32-character hex string form.
    /// </summary>
    /// <param name="text">The GUID string.</param>
    /// <returns>The parsed GUID (the zero GUID if the string is not parseable).</returns>
    public static SdlGuid Parse(string text) => new(SDL_StringToGUID(ToUtf8(text)));

    /// <summary>
    /// Returns the canonical 32-character lowercase hex string form.
    /// </summary>
    public override unsafe string ToString()
    {
        Span<byte> buffer = stackalloc byte[33];
        fixed (byte* p = buffer)
            SDL_GUIDToString(ToNative(), p, buffer.Length);
        var terminator = buffer.IndexOf((byte)0);
        return Encoding.ASCII.GetString(buffer[..(terminator < 0 ? buffer.Length : terminator)]);
    }
}
```

Note: `record struct` synthesizes value equality over the two private fields — that's the point of the record. If the compiler warns about unread fields or the synthesized `ToString` override conflicts (records synthesize `ToString`; an explicit override replaces it silently — verify it compiles with `sealed override` not required for structs), adjust minimally and note it.

- [ ] **Step 1.3: Build + commit**

Run: `dotnet build SdlSharp.slnx` → `0 Warning(s), 0 Error(s)`

```bash
git add src/SdlSharp/Native/Guid.cs src/SdlSharp/SdlGuid.cs
git commit -m "Add SDL_guid.h binding and public SdlGuid"
```

---

### Task 2: Native joystick additions

**Files:**
- Modify: `src/SdlSharp/Native/Joystick.cs`

- [ ] **Step 2.1: Bind the missing functions**

18 of 58 are bound (SDL_CloseJoystick, SDL_GetJoystickAxis/Button/Hat, SDL_GetJoystickConnectionState, SDL_GetJoystickFromID, SDL_GetJoystickID, SDL_GetJoystickName(+ForID), SDL_GetJoystickType(+ForID), SDL_GetJoysticks, SDL_GetNumJoystickAxes/Balls/Buttons/Hats, SDL_OpenJoystick, SDL_UpdateJoysticks). Add these, each per the boilerplate recipe, with the C# signature derived from the header (verify each against `SDL_joystick.h` before writing):

```csharp
public static partial bool SDL_HasJoystick();                                                    // bool return → MarshalAs
public static unsafe partial bool SDL_JoystickConnected(SDL_Joystick* joystick);
public static unsafe partial SDL_GUID SDL_GetJoystickGUID(SDL_Joystick* joystick);
public static partial SDL_GUID SDL_GetJoystickGUIDForID(uint instance_id);
public static unsafe partial ushort SDL_GetJoystickVendor(SDL_Joystick* joystick);
public static partial ushort SDL_GetJoystickVendorForID(uint instance_id);
public static unsafe partial ushort SDL_GetJoystickProduct(SDL_Joystick* joystick);
public static partial ushort SDL_GetJoystickProductForID(uint instance_id);
public static unsafe partial ushort SDL_GetJoystickProductVersion(SDL_Joystick* joystick);
public static partial ushort SDL_GetJoystickProductVersionForID(uint instance_id);
public static unsafe partial ushort SDL_GetJoystickFirmwareVersion(SDL_Joystick* joystick);
public static unsafe partial byte* SDL_GetJoystickSerial(SDL_Joystick* joystick);
public static unsafe partial byte* SDL_GetJoystickPath(SDL_Joystick* joystick);
public static partial byte* SDL_GetJoystickPathForID(uint instance_id);                          // unsafe
public static unsafe partial SDL_PropertiesID SDL_GetJoystickProperties(SDL_Joystick* joystick);
public static unsafe partial int SDL_GetJoystickPlayerIndex(SDL_Joystick* joystick);
public static unsafe partial bool SDL_SetJoystickPlayerIndex(SDL_Joystick* joystick, int player_index);
public static partial int SDL_GetJoystickPlayerIndexForID(uint instance_id);
public static unsafe partial SDL_Joystick* SDL_GetJoystickFromPlayerIndex(int player_index);
public static unsafe partial SDL_PowerState SDL_GetJoystickPowerInfo(SDL_Joystick* joystick, int* percent);
public static unsafe partial bool SDL_RumbleJoystick(SDL_Joystick* joystick, ushort low_frequency_rumble, ushort high_frequency_rumble, uint duration_ms);
public static unsafe partial bool SDL_RumbleJoystickTriggers(SDL_Joystick* joystick, ushort left_rumble, ushort right_rumble, uint duration_ms);
public static unsafe partial bool SDL_SetJoystickLED(SDL_Joystick* joystick, byte red, byte green, byte blue);
public static unsafe partial bool SDL_SendJoystickEffect(SDL_Joystick* joystick, void* data, int size);
public static partial void SDL_SetJoystickEventsEnabled([MarshalAs(UnmanagedType.U1)] bool enabled);
public static partial bool SDL_JoystickEventsEnabled();
public static unsafe partial bool SDL_GetJoystickBall(SDL_Joystick* joystick, int ball, int* dx, int* dy);
```

Notes: `SDL_PowerState` already exists in Native/Events.cs — verify the enum name and add a `using` or qualify if needed. Bool params also need `[MarshalAs(UnmanagedType.U1)]` on the parameter (check how existing bindings marshal bool params, e.g. SDL_SetWindowFullscreen in Native/Video.cs, and match).

- [ ] **Step 2.2: Add the CAP property constants + axis constants + skip comments**

```csharp
/// <summary>Property: true if this joystick has an LED with a single color.</summary>
public const string SDL_PROP_JOYSTICK_CAP_MONO_LED_BOOLEAN = "SDL.joystick.cap.mono_led";

/// <summary>Property: true if this joystick has an RGB LED.</summary>
public const string SDL_PROP_JOYSTICK_CAP_RGB_LED_BOOLEAN = "SDL.joystick.cap.rgb_led";

/// <summary>Property: true if this joystick has a player LED.</summary>
public const string SDL_PROP_JOYSTICK_CAP_PLAYER_LED_BOOLEAN = "SDL.joystick.cap.player_led";

/// <summary>Property: true if this joystick has left/right rumble.</summary>
public const string SDL_PROP_JOYSTICK_CAP_RUMBLE_BOOLEAN = "SDL.joystick.cap.rumble";

/// <summary>Property: true if this joystick has simple trigger rumble.</summary>
public const string SDL_PROP_JOYSTICK_CAP_TRIGGER_RUMBLE_BOOLEAN = "SDL.joystick.cap.trigger_rumble";

/// <summary>Minimum value an SDL joystick axis can report.</summary>
public const short SDL_JOYSTICK_AXIS_MIN = -32768;

/// <summary>Maximum value an SDL joystick axis can report.</summary>
public const short SDL_JOYSTICK_AXIS_MAX = 32767;
```

(Verify the five string values byte-exact against SDL_joystick.h lines ~772-776 and the axis constants against the header's #defines.) Update the file's header skip comment to enumerate what remains deliberately unbound: the virtual-joystick suite (`SDL_AttachVirtualJoystick`, `SDL_DetachVirtualJoystick`, `SDL_IsJoystickVirtual`, `SDL_SetJoystickVirtual*`, `SDL_VirtualJoystickDesc` + callbacks), `SDL_LockJoysticks`/`SDL_UnlockJoysticks`, `SDL_GetJoystickGUIDInfo`, and `SDL_JOYSTICK_AXIS_MIN`-adjacent macro machinery if any. Count check: bound + skipped must account for all 58 header functions — state the arithmetic in your report.

- [ ] **Step 2.3: Build + commit**

Run: `dotnet build SdlSharp.slnx` → `0 Warning(s), 0 Error(s)`

```bash
git add src/SdlSharp/Native/Joystick.cs
git commit -m "Bind remaining joystick functions, CAP properties, axis constants"
```

---

### Task 3: Native gamepad additions

**Files:**
- Modify: `src/SdlSharp/Native/Gamepad.cs`

- [ ] **Step 3.1: Bind the missing functions**

15 of 73 are bound (list in Task 2 preamble style — run the EntryPoint grep to confirm). Add, per the boilerplate recipe (verify each signature against `SDL_gamepad.h`):

```csharp
public static partial bool SDL_HasGamepad();
public static unsafe partial bool SDL_GamepadConnected(SDL_Gamepad* gamepad);
public static unsafe partial uint SDL_GetGamepadID(SDL_Gamepad* gamepad);
public static partial int SDL_AddGamepadMapping(ReadOnlySpan<byte> mapping);
public static partial int SDL_AddGamepadMappingsFromFile(ReadOnlySpan<byte> file);
public static unsafe partial byte* SDL_GetGamepadMapping(SDL_Gamepad* gamepad);            // caller frees via SDL_free
public static unsafe partial byte* SDL_GetGamepadMappingForGUID(SDL_GUID guid);            // caller frees
public static unsafe partial byte* SDL_GetGamepadMappingForID(uint instance_id);           // caller frees
public static partial bool SDL_SetGamepadMapping(uint instance_id, ReadOnlySpan<byte> mapping);
public static partial SDL_GUID SDL_GetGamepadGUIDForID(uint instance_id);
public static unsafe partial ushort SDL_GetGamepadVendor(SDL_Gamepad* gamepad);
public static partial ushort SDL_GetGamepadVendorForID(uint instance_id);
public static unsafe partial ushort SDL_GetGamepadProduct(SDL_Gamepad* gamepad);
public static partial ushort SDL_GetGamepadProductForID(uint instance_id);
public static unsafe partial ushort SDL_GetGamepadProductVersion(SDL_Gamepad* gamepad);
public static partial ushort SDL_GetGamepadProductVersionForID(uint instance_id);
public static unsafe partial ushort SDL_GetGamepadFirmwareVersion(SDL_Gamepad* gamepad);
public static unsafe partial byte* SDL_GetGamepadSerial(SDL_Gamepad* gamepad);
public static unsafe partial byte* SDL_GetGamepadPath(SDL_Gamepad* gamepad);
public static partial byte* SDL_GetGamepadPathForID(uint instance_id);                     // unsafe
public static unsafe partial ulong SDL_GetGamepadSteamHandle(SDL_Gamepad* gamepad);
public static unsafe partial SDL_PropertiesID SDL_GetGamepadProperties(SDL_Gamepad* gamepad);
public static unsafe partial SDL_GamepadType SDL_GetRealGamepadType(SDL_Gamepad* gamepad);
public static partial SDL_GamepadType SDL_GetRealGamepadTypeForID(uint instance_id);
public static unsafe partial int SDL_GetGamepadPlayerIndex(SDL_Gamepad* gamepad);
public static unsafe partial bool SDL_SetGamepadPlayerIndex(SDL_Gamepad* gamepad, int player_index);
public static partial int SDL_GetGamepadPlayerIndexForID(uint instance_id);
public static unsafe partial SDL_Gamepad* SDL_GetGamepadFromPlayerIndex(int player_index);
public static unsafe partial SDL_PowerState SDL_GetGamepadPowerInfo(SDL_Gamepad* gamepad, int* percent);
public static unsafe partial bool SDL_RumbleGamepad(SDL_Gamepad* gamepad, ushort low_frequency_rumble, ushort high_frequency_rumble, uint duration_ms);
public static unsafe partial bool SDL_RumbleGamepadTriggers(SDL_Gamepad* gamepad, ushort left_rumble, ushort right_rumble, uint duration_ms);
public static unsafe partial bool SDL_SetGamepadLED(SDL_Gamepad* gamepad, byte red, byte green, byte blue);
public static unsafe partial bool SDL_SendGamepadEffect(SDL_Gamepad* gamepad, void* data, int size);
public static unsafe partial bool SDL_GamepadHasAxis(SDL_Gamepad* gamepad, SDL_GamepadAxis axis);
public static unsafe partial bool SDL_GamepadHasButton(SDL_Gamepad* gamepad, SDL_GamepadButton button);
public static partial SDL_GamepadAxis SDL_GetGamepadAxisFromString(ReadOnlySpan<byte> str);
public static unsafe partial byte* SDL_GetGamepadStringForAxis(SDL_GamepadAxis axis);       // SDL-owned, no free
public static partial SDL_GamepadButton SDL_GetGamepadButtonFromString(ReadOnlySpan<byte> str);
public static unsafe partial byte* SDL_GetGamepadStringForButton(SDL_GamepadButton button); // SDL-owned, no free
public static partial void SDL_SetGamepadEventsEnabled([MarshalAs(UnmanagedType.U1)] bool enabled);
public static partial bool SDL_GamepadEventsEnabled();
public static unsafe partial int SDL_GetNumGamepadTouchpads(SDL_Gamepad* gamepad);
public static unsafe partial int SDL_GetNumGamepadTouchpadFingers(SDL_Gamepad* gamepad, int touchpad);
public static unsafe partial bool SDL_GetGamepadTouchpadFinger(SDL_Gamepad* gamepad, int touchpad, int finger, byte* down, float* x, float* y, float* pressure);   // C bool out-param marshalled as byte* (1-byte stdbool ABI); managed layer converts
public static unsafe partial bool SDL_GamepadHasSensor(SDL_Gamepad* gamepad, SDL_SensorType type);
public static unsafe partial bool SDL_SetGamepadSensorEnabled(SDL_Gamepad* gamepad, SDL_SensorType type, [MarshalAs(UnmanagedType.U1)] bool enabled);
public static unsafe partial bool SDL_GamepadSensorEnabled(SDL_Gamepad* gamepad, SDL_SensorType type);
public static unsafe partial float SDL_GetGamepadSensorDataRate(SDL_Gamepad* gamepad, SDL_SensorType type);
public static unsafe partial bool SDL_GetGamepadSensorData(SDL_Gamepad* gamepad, SDL_SensorType type, float* data, int num_values);
```

TouchpadFinger's `byte* down` is deliberate (C bool out-param, 1-byte stdbool ABI; the managed layer converts) — but first check whether the codebase already marshals bool-pointer out-params some other way (grep `bool\*` in Native/) and match any existing precedent instead, adjusting Task 5's `GetTouchpadFinger` body to agree. `SDL_SensorType` lives in Native/Sensor.cs; `SDL_GUID` from Task 1; add usings as needed.

- [ ] **Step 3.2: Add CAP aliases + skip comments**

```csharp
/// <summary>Property: true if this gamepad has an LED with a single color (alias of the joystick CAP property).</summary>
public const string SDL_PROP_GAMEPAD_CAP_MONO_LED_BOOLEAN = Joystick.SDL_PROP_JOYSTICK_CAP_MONO_LED_BOOLEAN;
/// <summary>Property: true if this gamepad has an RGB LED.</summary>
public const string SDL_PROP_GAMEPAD_CAP_RGB_LED_BOOLEAN = Joystick.SDL_PROP_JOYSTICK_CAP_RGB_LED_BOOLEAN;
/// <summary>Property: true if this gamepad has a player LED.</summary>
public const string SDL_PROP_GAMEPAD_CAP_PLAYER_LED_BOOLEAN = Joystick.SDL_PROP_JOYSTICK_CAP_PLAYER_LED_BOOLEAN;
/// <summary>Property: true if this gamepad has left/right rumble.</summary>
public const string SDL_PROP_GAMEPAD_CAP_RUMBLE_BOOLEAN = Joystick.SDL_PROP_JOYSTICK_CAP_RUMBLE_BOOLEAN;
/// <summary>Property: true if this gamepad has simple trigger rumble.</summary>
public const string SDL_PROP_GAMEPAD_CAP_TRIGGER_RUMBLE_BOOLEAN = Joystick.SDL_PROP_JOYSTICK_CAP_TRIGGER_RUMBLE_BOOLEAN;
```

(The header defines the gamepad CAPs as aliases of the joystick CAPs — SDL_gamepad.h lines 813-817; referencing the Joystick consts keeps one source of truth.) Update the file-header skip comment to enumerate the deliberate skips: `SDL_GetGamepadAppleSFSymbols*` (2 functions, Apple-specific), `SDL_AddGamepadMappingsFromIO` (IO streams — project policy), `SDL_GetGamepadMappings` (niche enumerator), `SDL_GetGamepadBindings` + `SDL_GamepadBinding` struct (low-level binding introspection, niche), `SDL_GetGamepadTypeFromString`/`SDL_GetGamepadStringForType` IF the audit lists them as missing — check the audit entry and either bind them (they're trivial string round-trips, matching the axis/button ones — preferred) or skip-comment them. Count check: bound + skipped must account for all 73 — state the arithmetic.

- [ ] **Step 3.3: Build + commit**

Run: `dotnet build SdlSharp.slnx` → `0 Warning(s), 0 Error(s)`

```bash
git add src/SdlSharp/Native/Gamepad.cs
git commit -m "Bind remaining gamepad functions, mapping database, CAP aliases"
```

---

### Task 4: Managed joystick surface

**Files:**
- Modify: `src/SdlSharp/Input/Joystick.cs`
- Create: `src/SdlSharp/Input/JoystickProperties.cs`

- [ ] **Step 4.1: Add `_ownsHandle` + new members to `Input/Joystick.cs`**

First read the current file. Add `private readonly bool _ownsHandle;` + `bool ownsHandle = true` ctor param, gate `SDL_CloseJoystick` in Dispose on it (Handle still always nulled) — the exact GpuTexture/Cursor pattern. Then add (full code; adjust `using static SdlSharp.Native.Common;`/usings as needed; every member documented):

```csharp
/// <summary>Gets whether any joystick is currently connected.</summary>
public static bool HasJoystick => SDL_HasJoystick();

/// <summary>Gets the instance ID of this joystick.</summary>
public uint Id => CheckId(SDL_GetJoystickID(Handle).Value);   // check the native return type — SDL_JoystickID typed id; unwrap consistently with GetDevices()

/// <summary>Gets the joystick associated with an instance ID, or null if none. The returned wrapper does not own the handle.</summary>
public static Joystick? FromId(uint id)
{
    var handle = SDL_GetJoystickFromID(id);
    return handle == null ? null : new Joystick(handle, ownsHandle: false);
}

/// <summary>Gets the joystick associated with a player index, or null. The returned wrapper does not own the handle.</summary>
public static Joystick? FromPlayerIndex(int playerIndex)
{
    var handle = SDL_GetJoystickFromPlayerIndex(playerIndex);
    return handle == null ? null : new Joystick(handle, ownsHandle: false);
}

/// <summary>Gets whether this joystick is still connected.</summary>
public bool Connected => SDL_JoystickConnected(Handle);

/// <summary>Gets the GUID identifying this joystick model.</summary>
public SdlGuid Guid => new(SDL_GetJoystickGUID(Handle));

/// <summary>Gets the GUID for a joystick instance ID.</summary>
public static SdlGuid GetGuidForId(uint id) => new(SDL_GetJoystickGUIDForID(id));

/// <summary>Gets the USB vendor ID, or 0 if unavailable.</summary>
public ushort Vendor => SDL_GetJoystickVendor(Handle);

/// <summary>Gets the USB product ID, or 0 if unavailable.</summary>
public ushort Product => SDL_GetJoystickProduct(Handle);

/// <summary>Gets the product version, or 0 if unavailable.</summary>
public ushort ProductVersion => SDL_GetJoystickProductVersion(Handle);

/// <summary>Gets the firmware version, or 0 if unavailable.</summary>
public ushort FirmwareVersion => SDL_GetJoystickFirmwareVersion(Handle);

/// <summary>Gets the serial number, or null if unavailable.</summary>
public string? Serial => Marshal.PtrToStringUTF8((nint)SDL_GetJoystickSerial(Handle));

/// <summary>Gets the implementation-dependent path, or null if unavailable.</summary>
public string? Path => Marshal.PtrToStringUTF8((nint)SDL_GetJoystickPath(Handle));

/// <summary>Gets the properties of this joystick (capability flags — names in <see cref="JoystickProperties"/>). Owned by SDL.</summary>
public PropertyGroup Properties => new(SDL_GetJoystickProperties(Handle), ownsHandle: false);

/// <summary>Gets or sets the player index, or -1 when unset. Setting -1 clears it.</summary>
public int PlayerIndex
{
    get => SDL_GetJoystickPlayerIndex(Handle);
    set => Check(SDL_SetJoystickPlayerIndex(Handle, value));
}

/// <summary>Gets the player index for an instance ID, or -1.</summary>
public static int GetPlayerIndexForId(uint id) => SDL_GetJoystickPlayerIndexForID(id);

/// <summary>Gets the battery state. </summary>
/// <param name="percent">Receives the charge percentage, or -1 if unknown.</param>
/// <returns>The power state.</returns>
public PowerState GetPowerInfo(out int percent)
{
    int p;
    var state = SDL_GetJoystickPowerInfo(Handle, &p);
    percent = p;
    return (PowerState)state;
}

/// <summary>Rumbles the joystick. Zero intensities stop rumbling.</summary>
/// <param name="lowFrequency">Low-frequency (left) motor intensity.</param>
/// <param name="highFrequency">High-frequency (right) motor intensity.</param>
/// <param name="duration">How long to rumble.</param>
public void Rumble(ushort lowFrequency, ushort highFrequency, TimeSpan duration) =>
    Check(SDL_RumbleJoystick(Handle, lowFrequency, highFrequency, ToMilliseconds(duration)));

/// <summary>Rumbles the triggers. Not all devices support trigger rumble.</summary>
public void RumbleTriggers(ushort left, ushort right, TimeSpan duration) =>
    Check(SDL_RumbleJoystickTriggers(Handle, left, right, ToMilliseconds(duration)));

/// <summary>Sets the LED color. Alpha is ignored.</summary>
public void SetLed(Color color) => Check(SDL_SetJoystickLED(Handle, color.R, color.G, color.B));

/// <summary>Sends a device-specific effect packet.</summary>
public void SendEffect(ReadOnlySpan<byte> data)
{
    fixed (byte* p = data)
        Check(SDL_SendJoystickEffect(Handle, p, data.Length));
}

/// <summary>Gets a trackball's movement delta since the last poll.</summary>
public void GetBall(int ball, out int deltaX, out int deltaY)
{
    int dx, dy;
    Check(SDL_GetJoystickBall(Handle, ball, &dx, &dy));
    deltaX = dx;
    deltaY = dy;
}

/// <summary>Enables or disables joystick event polling delivery.</summary>
public static void SetEventsEnabled(bool enabled) => SDL_SetJoystickEventsEnabled(enabled);

/// <summary>Gets whether joystick events are enabled.</summary>
public static bool EventsEnabled => SDL_JoystickEventsEnabled();

/// <summary>Minimum value a joystick axis reports.</summary>
public const short AxisMin = Native.Joystick.SDL_JOYSTICK_AXIS_MIN;

/// <summary>Maximum value a joystick axis reports.</summary>
public const short AxisMax = Native.Joystick.SDL_JOYSTICK_AXIS_MAX;

private static uint ToMilliseconds(TimeSpan duration) =>
    (uint)Math.Clamp(duration.TotalMilliseconds, 0, uint.MaxValue);
```

Adjust the `Id` line to the actual native return (if `SDL_GetJoystickID` returns a typed `SDL_JoystickID`, use its `.Value` like `GetDevices()` does; `CheckId` if the codebase treats 0 as failure — look at how the existing code validates instance IDs and match). `Color` is `SdlSharp.Graphics.Color` — add the using. `PowerState` is the existing public enum.

- [ ] **Step 4.2: Create `src/SdlSharp/Input/JoystickProperties.cs`**

```csharp
namespace SdlSharp.Input;

/// <summary>
/// Property names for <see cref="Joystick.Properties"/> capability queries.
/// </summary>
public static class JoystickProperties
{
    /// <summary>True if the joystick has an LED with a single color (boolean).</summary>
    public const string CapMonoLed = Native.Joystick.SDL_PROP_JOYSTICK_CAP_MONO_LED_BOOLEAN;
    /// <summary>True if the joystick has an RGB LED (boolean).</summary>
    public const string CapRgbLed = Native.Joystick.SDL_PROP_JOYSTICK_CAP_RGB_LED_BOOLEAN;
    /// <summary>True if the joystick has a player LED (boolean).</summary>
    public const string CapPlayerLed = Native.Joystick.SDL_PROP_JOYSTICK_CAP_PLAYER_LED_BOOLEAN;
    /// <summary>True if the joystick has left/right rumble (boolean).</summary>
    public const string CapRumble = Native.Joystick.SDL_PROP_JOYSTICK_CAP_RUMBLE_BOOLEAN;
    /// <summary>True if the joystick has simple trigger rumble (boolean).</summary>
    public const string CapTriggerRumble = Native.Joystick.SDL_PROP_JOYSTICK_CAP_TRIGGER_RUMBLE_BOOLEAN;
}
```

- [ ] **Step 4.3: Build + commit**

Run: `dotnet build SdlSharp.slnx` → `0 Warning(s), 0 Error(s)`

```bash
git add src/SdlSharp/Input/Joystick.cs src/SdlSharp/Input/JoystickProperties.cs
git commit -m "Complete managed joystick surface: rumble, LED, identity, power, player index"
```

---

### Task 5: Managed gamepad surface

**Files:**
- Modify: `src/SdlSharp/Input/Gamepad.cs`
- Create: `src/SdlSharp/Input/GamepadProperties.cs`

- [ ] **Step 5.1: Add `_ownsHandle` + new members to `Input/Gamepad.cs`**

Same `_ownsHandle` retrofit as Task 4. Then add (same doc/`Check` discipline; usings as needed). The joystick-parallel members are identical in shape to Task 4's — write them out concretely for: `HasGamepad` (static), `Id`, `FromId`, `FromPlayerIndex`, `Connected`, `Vendor`, `Product`, `ProductVersion`, `FirmwareVersion`, `Serial`, `Path`, `Properties` (→ `GamepadProperties` cref), `PlayerIndex` get/set, `GetPlayerIndexForId`, `GetPowerInfo`, `Rumble`, `RumbleTriggers`, `SetLed`, `SendEffect`, `SetEventsEnabled`/`EventsEnabled`, and the private `ToMilliseconds` helper — substituting the Gamepad natives (`SDL_GamepadConnected`, `SDL_RumbleGamepad`, …). Gamepad-specific members:

```csharp
/// <summary>Gets the GUID of the underlying joystick device.</summary>
public SdlGuid Guid => new(SDL_GetGamepadGUIDForID(Id));

/// <summary>Gets the GUID for a gamepad instance ID.</summary>
public static SdlGuid GetGuidForId(uint id) => new(SDL_GetGamepadGUIDForID(id));

/// <summary>Gets the underlying joystick as a non-owning wrapper.</summary>
public Joystick GetJoystick() => new(Check(SDL_GetGamepadJoystick(Handle)), ownsHandle: false);

/// <summary>Gets the Steam Input handle, or 0 if unavailable.</summary>
public ulong SteamHandle => SDL_GetGamepadSteamHandle(Handle);

/// <summary>Gets the hardware type, ignoring any mapping override (contrast <see cref="Type"/>).</summary>
public GamepadType RealType => (GamepadType)SDL_GetRealGamepadType(Handle);

/// <summary>Gets the hardware type for an instance ID, ignoring mapping overrides.</summary>
public static GamepadType GetRealType(uint id) => (GamepadType)SDL_GetRealGamepadTypeForID(id);

/// <summary>Adds a controller mapping string.</summary>
/// <returns>True if a new mapping was added, false if an existing one was updated.</returns>
public static bool AddMapping(string mapping)
{
    var result = SDL_AddGamepadMapping(ToUtf8(mapping));
    if (result < 0) throw new SdlException();
    return result == 1;
}

/// <summary>Loads controller mappings from a file (e.g. gamecontrollerdb.txt).</summary>
/// <returns>The number of mappings added.</returns>
public static int AddMappingsFromFile(string path)
{
    var result = SDL_AddGamepadMappingsFromFile(ToUtf8(path));
    if (result < 0) throw new SdlException();
    return result;
}

/// <summary>Gets this gamepad's mapping string, or null if none.</summary>
public string? GetMapping()
{
    var native = SDL_GetGamepadMapping(Handle);
    if (native == null) return null;
    var result = Marshal.PtrToStringUTF8((nint)native);
    SDL_free(native);
    return result;
}

/// <summary>Gets the mapping string for a device GUID, or null if none.</summary>
public static string? GetMappingForGuid(SdlGuid guid)
{
    var native = SDL_GetGamepadMappingForGUID(guid.ToNative());
    if (native == null) return null;
    var result = Marshal.PtrToStringUTF8((nint)native);
    SDL_free(native);
    return result;
}

/// <summary>Sets or clears (null) the mapping for a joystick instance ID.</summary>
public static void SetMapping(uint joystickId, string? mapping) =>
    Check(SDL_SetGamepadMapping(joystickId, ToUtf8(mapping)));

/// <summary>Gets whether this gamepad reports the given axis.</summary>
public bool HasAxis(GamepadAxis axis) => SDL_GamepadHasAxis(Handle, (SDL_GamepadAxis)axis);

/// <summary>Gets whether this gamepad reports the given button.</summary>
public bool HasButton(GamepadButton button) => SDL_GamepadHasButton(Handle, (SDL_GamepadButton)button);

/// <summary>Parses an axis from its mapping-string name, or Invalid.</summary>
public static GamepadAxis GetAxisFromString(string name) => (GamepadAxis)SDL_GetGamepadAxisFromString(ToUtf8(name));

/// <summary>Gets the mapping-string name for an axis, or null.</summary>
public static string? GetStringForAxis(GamepadAxis axis) =>
    Marshal.PtrToStringUTF8((nint)SDL_GetGamepadStringForAxis((SDL_GamepadAxis)axis));

/// <summary>Parses a button from its mapping-string name, or Invalid.</summary>
public static GamepadButton GetButtonFromString(string name) => (GamepadButton)SDL_GetGamepadButtonFromString(ToUtf8(name));

/// <summary>Gets the mapping-string name for a button, or null.</summary>
public static string? GetStringForButton(GamepadButton button) =>
    Marshal.PtrToStringUTF8((nint)SDL_GetGamepadStringForButton((SDL_GamepadButton)button));

/// <summary>Gets the number of touchpads on this gamepad.</summary>
public int NumTouchpads => SDL_GetNumGamepadTouchpads(Handle);

/// <summary>Gets the number of simultaneous fingers a touchpad supports.</summary>
public int GetNumTouchpadFingers(int touchpad) => SDL_GetNumGamepadTouchpadFingers(Handle, touchpad);

/// <summary>Gets the state of a finger on a touchpad.</summary>
public void GetTouchpadFinger(int touchpad, int finger, out bool down, out float x, out float y, out float pressure)
{
    byte d;
    float fx, fy, fp;
    Check(SDL_GetGamepadTouchpadFinger(Handle, touchpad, finger, &d, &fx, &fy, &fp));
    down = d != 0;
    x = fx;
    y = fy;
    pressure = fp;
}

/// <summary>Gets whether this gamepad has the given sensor.</summary>
public bool HasSensor(SensorType type) => SDL_GamepadHasSensor(Handle, (SDL_SensorType)type);

/// <summary>Enables or disables a sensor.</summary>
public void SetSensorEnabled(SensorType type, bool enabled) => Check(SDL_SetGamepadSensorEnabled(Handle, (SDL_SensorType)type, enabled));

/// <summary>Gets whether a sensor is enabled.</summary>
public bool IsSensorEnabled(SensorType type) => SDL_GamepadSensorEnabled(Handle, (SDL_SensorType)type);

/// <summary>Gets a sensor's data rate in events per second, or 0.</summary>
public float GetSensorDataRate(SensorType type) => SDL_GetGamepadSensorDataRate(Handle, (SDL_SensorType)type);

/// <summary>Reads the current sensor state into the given buffer.</summary>
public void GetSensorData(SensorType type, Span<float> data)
{
    fixed (float* p = data)
        Check(SDL_GetGamepadSensorData(Handle, (SDL_SensorType)type, p, data.Length));
}
```

Fill in `GetTouchpadFinger`'s body per the Task 3 marshalling choice (byte* → `down = d != 0`, or bool* passthrough) — the `...` above is the ONLY intentionally-deferred body in this plan and MUST be completed against Task 3's actual binding. Type/GetType naming: the existing class has instance `Type` and static `GetType(uint)` — keep them; add the Real pair as shown.

- [ ] **Step 5.2: Create `src/SdlSharp/Input/GamepadProperties.cs`**

Same shape as JoystickProperties, referencing the `Native.Gamepad.SDL_PROP_GAMEPAD_CAP_*` aliases, doc summaries saying "gamepad".

- [ ] **Step 5.3: Build + commit**

Run: `dotnet build SdlSharp.slnx` → `0 Warning(s), 0 Error(s)`

```bash
git add src/SdlSharp/Input/Gamepad.cs src/SdlSharp/Input/GamepadProperties.cs
git commit -m "Complete managed gamepad surface: mappings, rumble, sensors, touchpads, identity"
```

---

### Task 6: Scancode completion

**Files:**
- Modify: `src/SdlSharp/Input/Scancode.cs`

- [ ] **Step 6.1: Extend to full parity with `Native.SDL_Scancode`**

Current public enum has 105 members; native has 249. Apply the EventType method: for every native member absent from the public enum, add `<PascalCase> = (int)Native.SDL_Scancode.SDL_SCANCODE_<NAME>,` in native declaration order, one-line summary each, preserving/adding the native file's section grouping. Derivation rules: strip `SDL_SCANCODE_`, PascalCase tokens (`NONUSBACKSLASH` → `NonUsBackslash`, `NONUSHASH` → `NonUsHash`, `KP_MEMSTORE` → `KpMemStore` — follow the EXISTING members' conventions for the KP_ prefix and digits: read how the current file names `KP_1` etc. and stay consistent; `AC_BACK` → `AcBack`; `INTERNATIONAL1` → `International1`; `LANG1` → `Lang1`; `MODE` → `Mode`; `COUNT` → `Count`). Where an existing public member already covers a native member, do not rename it.

- [ ] **Step 6.2: Parity check**

Set-diff the native enum's member tokens against the public enum's cast targets (EventType-style script). Expected: 249/249 (or state exactly which sentinels you omitted and why — prefer including `Count`).

- [ ] **Step 6.3: Build + commit**

Run: `dotnet build SdlSharp.slnx` → `0 Warning(s), 0 Error(s)`

```bash
git add src/SdlSharp/Input/Scancode.cs
git commit -m "Complete public Scancode enum to full native parity"
```

---

### Task 7: Keycode completion + KeyModifiers.Level5

**Files:**
- Modify: `src/SdlSharp/Input/Keycode.cs`

- [ ] **Step 7.1: Extend `Keycode` to full parity with `Native.SDL_Keycode`**

Native has 256 `SDLK_*` members; public has ~86. Same method as Task 6: follow the existing public members' naming conventions exactly (read them first — e.g. how digits, punctuation, and `SDLK_KP_*` are currently named), derive the rest mechanically, native order, one-line docs, `(uint)` or `(int)` cast matching the file's existing cast style.

- [ ] **Step 7.2: Complete `KeyModifiers`**

Add `Level5 = (ushort)Native.SDL_Keymod.SDL_KMOD_LEVEL5,` (adjust cast to the enum's underlying type — read the existing members). Set-diff `SDL_Keymod` members vs `KeyModifiers` and add anything else missing (report what you found).

- [ ] **Step 7.3: Parity check + build + commit**

Parity script for both enums; then `dotnet build SdlSharp.slnx` → clean.

```bash
git add src/SdlSharp/Input/Keycode.cs
git commit -m "Complete public Keycode enum and KeyModifiers"
```

---

### Task 8: Device-free runtime check

**Files:**
- Create: `Samples/InputInfo/InputInfo.csproj`
- Create: `Samples/InputInfo/Program.cs`

- [ ] **Step 8.1: Create a small console sample**

Copy `Samples/GpuInfo/GpuInfo.csproj` as `Samples/InputInfo/InputInfo.csproj` (adjust the assembly name if the csproj states one) and add it to `SdlSharp.slnx` the same way GpuInfo is registered (inspect the slnx format first). `Program.cs`:

```csharp
// InputInfo sample — exercises the phase 8 input surface without requiring hardware.
// Exercises: Gamepad.HasGamepad, device enumeration, mapping database, SdlGuid round-trip,
// Joystick/Gamepad events-enabled toggles, axis/button string round-trips.

using SdlSharp;
using SdlSharp.Input;

using var app = new Application(InitFlags.Joystick | InitFlags.Gamepad);

Console.WriteLine($"Has gamepad: {Gamepad.HasGamepad}");
Console.WriteLine($"Has joystick: {Joystick.HasJoystick}");
Console.WriteLine($"Gamepads: {Gamepad.GetDevices().Length}");
Console.WriteLine($"Joysticks: {Joystick.GetDevices().Length}");

// Mapping database: add a syntactically valid mapping for a fake device.
var added = Gamepad.AddMapping(
    "00000000f00dcafe0000000000000000,Phase8 Test Pad,a:b0,b:b1,back:b6,start:b7,leftshoulder:b4,rightshoulder:b5,dpup:h0.1,dpdown:h0.4,dpleft:h0.8,dpright:h0.2,leftx:a0,lefty:a1");
Console.WriteLine($"Mapping added as new: {added}");

// SdlGuid round-trip through SDL's parser/formatter.
var guid = SdlGuid.Parse("00000000f00dcafe0000000000000000");
Console.WriteLine($"Guid round-trip: {guid} (zero: {guid.IsZero})");
var mapping = Gamepad.GetMappingForGuid(guid);
Console.WriteLine($"Mapping lookup: {(mapping != null ? "found" : "missing")}");

// String round-trips.
Console.WriteLine($"Axis leftx -> {Gamepad.GetAxisFromString("leftx")} -> {Gamepad.GetStringForAxis(GamepadAxis.LeftX)}");
Console.WriteLine($"Button a -> {Gamepad.GetButtonFromString("a")} -> {Gamepad.GetStringForButton(GamepadButton.South)}");

// Events-enabled toggles.
Gamepad.SetEventsEnabled(true);
Joystick.SetEventsEnabled(true);
Console.WriteLine($"Events enabled: gamepad={Gamepad.EventsEnabled} joystick={Joystick.EventsEnabled}");

Console.WriteLine("Done.");
```

Adjust to the actual public API: check `Application`'s constructor/init pattern in `Samples/GpuInfo/Program.cs` and `InitFlags` member names; check `GamepadButton.South` vs `A` naming in the existing enum; if `GetMappingForGuid` legitimately returns null here, print it without failing. The sample must exit 0.

- [ ] **Step 8.2: Run it + ImGuiDemo smoke**

Run: `dotnet run --project Samples/InputInfo` → prints all lines, exit 0. Expected: mapping added true, guid round-trip echoes the input string, mapping lookup "found", axis/button round-trips echo. Then the standard ImGuiDemo 10s background smoke test (unchanged code path, regression check only).

- [ ] **Step 8.3: Commit**

```bash
git add Samples/InputInfo/ SdlSharp.slnx
git commit -m "Add InputInfo sample exercising the phase 8 input surface"
```

---

### Task 9: INVENTORY.md + TODO.md close-out

**Files:**
- Modify: `INVENTORY.md` (sections: `## SDL_gamepad.h`, `## SDL_joystick.h`, `## SDL_guid.h`, `## SDL_scancode.h`, `## SDL_keycode.h`)
- Modify: `TODO.md` (8.1, 8.2, 8.3 checkboxes)

- [ ] **Step 9.1: Update the five INVENTORY sections**

For every symbol bound/exposed in Tasks 1-7, fill the Native/Managed cells with grep-verified names; flip stale "deferred" notes; the deliberately-skipped symbols keep/gain rows with their rationale (virtual joystick suite → niche; Apple SF symbols → platform; IO-stream mapping loader → .NET; mappings enumerator + bindings introspection → niche). SDL_guid.h section: `SDL_GUID` → `Guid.SDL_GUID` / `SdlGuid`, both functions → `SdlGuid.ToString`/`SdlGuid.Parse`. Scancode/keycode sections: update coverage notes to full parity. Consider whether `## SDL_gamepad.h` / `## SDL_joystick.h` / `## SDL_guid.h` now earn the ✅ marker under the file's stated rule (every unwrapped row has a settled rationale) — apply it if and only if the rule is met.

- [ ] **Step 9.2: TODO.md**

Flip `- [ ] 8.1`, `- [ ] 8.2`, `- [ ] 8.3` to `- [x]`, text intact.

- [ ] **Step 9.3: Final build + commit**

Run: `dotnet build SdlSharp.slnx --no-incremental` → `0 Warning(s), 0 Error(s)`

```bash
git add INVENTORY.md TODO.md
git commit -m "Record phase 8 input surface in INVENTORY.md; complete phase 8"
```
