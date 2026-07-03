# Phase 8: Input Completion — Design

**Date:** 2026-07-03
**Phase:** 8 (TODO.md, "Phases 7–11: SDL3 Surface Completion")
**Problem:** Gamepad coverage is 15/73 functions, joystick 18/58, and the public
`Scancode`/`Keycode` enums are missing large member ranges — the biggest remaining
feature gaps by percentage. Source of truth for the missing-symbol lists:
`audit/sdl3-coverage-2026-07.json` (headers `SDL_gamepad.h`, `SDL_joystick.h`,
`SDL_scancode.h`, `SDL_keycode.h`).

## Conventions (apply throughout)

- Follow the existing `Input/Gamepad.cs` / `Input/Joystick.cs` shape: instance members
  on the opened handle class, statics for ID-based and global queries, raw `uint` for
  joystick instance IDs (matching `Open(uint)`/`GetDevices()`).
- `Common.Check` on every native call that reports failure; hardware-unsupported
  operations (rumble/LED on devices without them) surface as `SdlException` — same as
  the existing `Haptic` rumble API.
- Public enums/constants cast from native members; property-name constant classes
  follow the `GpuDeviceProperties` pattern (const strings referencing native consts).
- Native additions go in the existing `Native/Gamepad.cs` / `Native/Joystick.cs`
  (plus new `Native/Guid.cs`); every skipped symbol gets a skip comment.
- Zero-warning solution build after every task.

## 8.0 SdlGuid foundation

- New `src/SdlSharp/Native/Guid.cs`: `SDL_GUID` struct (16 bytes — two `ulong` fields
  for blittability and cheap equality), `SDL_GUIDToString(SDL_GUID, byte*, int)`,
  `SDL_StringToGUID(ReadOnlySpan<byte>)`.
- New public `readonly record struct SdlGuid` in the `SdlSharp` root namespace
  (shared by joystick and gamepad): two `ulong` fields (record equality is value
  equality), `ToString()` returning SDL's canonical 32-hex-char form via
  `SDL_GUIDToString` into a stackalloc buffer, `static SdlGuid Parse(string)` via
  `SDL_StringToGUID`, `IsZero` convenience property (SDL returns zero GUIDs for
  invalid IDs), internal `ToNative()`/`FromNative()`.
- Rationale for a custom type over `System.Guid`: SDL GUIDs are bus/vendor/product
  encodings whose canonical string form (used in gamecontrollerdb.txt) does not match
  RFC 4122 formatting or `System.Guid` byte order.

## 8.1 Gamepad completion

Bind and expose the missing `SDL_gamepad.h` surface (audit list, minus deferrals):

- **Rumble/LED** (instance): `Rumble(ushort lowFrequency, ushort highFrequency, TimeSpan duration)`,
  `RumbleTriggers(ushort left, ushort right, TimeSpan duration)`, `SetLed(Color color)`.
  Durations clamp to `uint` milliseconds.
- **Mapping database**: static `AddMapping(string mapping)` (returns bool — true if
  new, false if updated existing, per SDL's 1/0 return; -1 → SdlException),
  static `AddMappingsFromFile(string path)` (returns count added),
  instance `GetMapping()` (string?, SDL-allocated → copy + SDL_free),
  static `GetMappingForGuid(SdlGuid)` (same free semantics),
  static `SetMapping(uint joystickId, string? mapping)`.
  (`SDL_AddGamepadMappingsFromIO` and `SDL_GetGamepadMappings` enumeration stay
  deferred: IO-stream variant per project policy; the enumerator is niche — skip
  comments required.)
- **Identity/correlation**: instance `Id` (uint), static `Gamepad? FromId(uint)`
  (non-owning wrapper — the class gains `_ownsHandle` like Cursor), instance
  `Connected`, static `bool HasGamepad`, instance `Guid` (via joystick GUID of the
  underlying `SDL_GetGamepadJoystick` — expose instance `Joystick GetJoystick()` as
  non-owning too), `Vendor`/`Product`/`ProductVersion`/`FirmwareVersion` (ushort),
  `Serial` (string?), `Path` (string?), `SteamHandle` (ulong), plus the per-ID
  statics SDL provides (`GetGuidForId`, `GetVendorForId`, `GetProductForId`,
  `GetProductVersionForId`, `GetPathForId`, `GetTypeForId` exists already as GetType,
  `GetMappingForId`).
- **Properties**: instance `Properties` (non-owning `PropertyGroup`) + static class
  `GamepadProperties` with the `SDL_PROP_GAMEPAD_CAP_*` const strings (native consts
  added alongside).
- **Player index**: instance `PlayerIndex` { get; set; } (set −1 clears, per SDL),
  static `Gamepad? FromPlayerIndex(int)`, static `int GetPlayerIndexForId(uint)`.
- **Sensors**: `HasSensor(SensorType)`, `SetSensorEnabled(SensorType, bool)`,
  `IsSensorEnabled(SensorType)`, `GetSensorData(SensorType, Span<float>)`,
  `GetSensorDataRate(SensorType)` — reusing the public `SensorType` enum.
- **Power**: `PowerState GetPowerInfo(out int percent)`.
- **Capability/misc**: `HasAxis(GamepadAxis)`, `HasButton(GamepadButton)`,
  static axis/button string round-trips (`GetAxisFromString`, `GetStringForAxis`,
  `GetButtonFromString`, `GetStringForButton`), static `SetEventsEnabled(bool)` /
  `EventsEnabled`, touchpads (`NumTouchpads`, `GetNumTouchpadFingers(int)`,
  `GetTouchpadFinger(int touchpad, int finger, out bool down, out float x, out float y, out float pressure)`),
  and the real-type queries: instance `RealType` property wrapping
  `SDL_GetRealGamepadType` (the hardware type ignoring mapping overrides, vs the
  existing `Type` property) and static `GetRealType(uint id)` wrapping
  `SDL_GetRealGamepadTypeForID`.
- **Deferred with skip comments**: `SDL_GetGamepadAppleSFSymbols*` (Apple-specific),
  `SDL_AddGamepadMappingsFromIO` (IO streams), `SDL_GetGamepadMappings` (niche
  enumerator), `SDL_GetGamepadBindings` (low-level binding introspection, niche).

## 8.2 Joystick completion

Same treatment for `SDL_joystick.h` (audit list, minus deferrals):

- **Rumble/LED/effects** (instance): `Rumble(ushort, ushort, TimeSpan)`,
  `RumbleTriggers(ushort, ushort, TimeSpan)`, `SetLed(Color)`,
  `SendEffect(ReadOnlySpan<byte>)`.
- **Identity**: `Id`, static `Joystick? FromId(uint)` (non-owning; class gains
  `_ownsHandle`), `Connected`, static `bool HasJoystick`, `Guid` (SdlGuid), static
  `GetGuidForId(uint)`, `Vendor`/`Product`/`ProductVersion`/`FirmwareVersion`,
  `Serial`, `Path`, plus per-ID statics (`GetVendorForId` etc.).
- **Properties**: instance `Properties` (non-owning) + `JoystickProperties` const
  class for the `SDL_PROP_JOYSTICK_CAP_*` strings.
- **Player index**: `PlayerIndex` get/set, static `FromPlayerIndex(int)`, static
  `GetPlayerIndexForId(uint)`.
- **Power**: `PowerState GetPowerInfo(out int percent)`.
- **Misc**: static `SetEventsEnabled(bool)`/`EventsEnabled`, `GetBall(int, out int dx, out int dy)`,
  `public const short AxisMin = -32768` / `AxisMax = 32767` (mirroring
  SDL_JOYSTICK_AXIS_MIN/MAX).
- **Deferred with skip comments**: the virtual-joystick suite
  (`SDL_AttachVirtualJoystick` + descriptor struct + callbacks + Set*Virtual*),
  `SDL_LockJoysticks`/`SDL_UnlockJoysticks` (internal-locking niche),
  `SDL_GetJoystickGUIDInfo` (decoder — revisit if requested).

## 8.3 Scancode/Keycode completion

- Extend public `Input/Scancode.cs` to full parity with `Native.SDL_Scancode`
  (~144 missing members: NonUsBackslash, NonUsHash, F13–F24, media/volume/AC keys,
  international/lang keys, keypad extended block, modifier keys, sentinels) using the
  EventType derivation-rules + parity-count approach.
- Extend public `Input/Keycode.cs` to full parity with `Native.SDL_Keycode`
  (keypad, punctuation, F13–F24, media/AC, extended/left-right modifier keys).
- Add `Level5` to `KeyModifiers` (SDL_KMOD_LEVEL5); verify no other SDL_Keymod
  members are missing while there.

## Error handling

`Common.Check` throughout; SDL-allocated returned strings (mapping queries) are
copied to managed strings then freed with `SDL_free` (bind it in Native/Common.cs or
wherever the codebase already has it — verify; add if missing with a skip-comment
note explaining its role).

## Verification

1. Zero-warning `dotnet build SdlSharp.slnx` per task.
2. Grep gates: no native types in public signatures across `Input/` (body calls and
   native-const initializers excepted).
3. Enum parity counts for Scancode/Keycode/KeyModifiers vs native (EventType-style
   set-diff).
4. Device-free runtime check in the final task: a transient console invocation (via
   `dotnet run` on a scratch sample or `GpuInfo`-style inline check) calling
   `Gamepad.HasGamepad`, `Gamepad.GetDevices()`, `Joystick.GetDevices()`,
   `Gamepad.AddMapping(<known-good mapping string>)`, and `SdlGuid.Parse` round-trip —
   exercises the new bindings without hardware. ImGuiDemo smoke test unchanged.
5. INVENTORY.md: SDL_gamepad.h, SDL_joystick.h, SDL_guid.h, SDL_scancode.h,
   SDL_keycode.h sections updated (managed wrapper names grep-verified); the false-✅
   history of these sections makes accuracy here the point. TODO.md 8.1–8.3 checked
   off at the end.
