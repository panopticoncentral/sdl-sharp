# Phase 11: Final Surface Completion — Design

**Date:** 2026-07-03
**Scope:** TODO.md phase 11 — 11.1 haptic effect system, 11.2 tray, 11.3 small items, 11.4 documented skips for file-less headers. This closes out the phases 7–11 SDL3 surface-completion initiative.

## Goals

1. SDL_haptic.h reaches full accounting: 31/31 functions bound, zero skips.
2. SDL_tray.h reaches full accounting: 23/23 functions bound, zero skips.
3. Every 11.3 small item lands (or earns a settled documented-skip rationale where a platform/ABI constraint blocks it).
4. Every fully-deferred header has a code-side skip rationale in `src/SdlSharp/Native/Deferred.cs`.
5. TODO.md phases 7–11 initiative closes.

## Decisions (user-approved)

- **Haptic: typed effect records.** One public `readonly record struct` per effect kind converting internally to the native union — the GpuDescriptors precedent. Not a raw union mirror.
- **Tray: brief TrayDemo smoke.** Runtime proof via a ~3-second sample that shows a real menu-bar icon, mirrors the ImGuiDemo window-smoke precedent.
- **Skip docs: comment-only `Native/Deferred.cs`** for headers with no native file.

## 11.1 Haptic effect system

### Native layer (`src/SdlSharp/Native/Haptic.cs`)

Bind the 19 remaining functions (house rules: LibraryImport + CallConvCdecl, `[MarshalAs(UnmanagedType.U1)]` on bools):

`SDL_CreateHapticEffect` (returns int effect id, -1 on error), `SDL_DestroyHapticEffect`, `SDL_UpdateHapticEffect`, `SDL_RunHapticEffect`, `SDL_StopHapticEffect`, `SDL_StopHapticEffects`, `SDL_GetHapticEffectStatus`, `SDL_HapticEffectSupported`, `SDL_GetHapticFeatures`, `SDL_GetNumHapticAxes`, `SDL_GetMaxHapticEffects`, `SDL_GetMaxHapticEffectsPlaying`, `SDL_SetHapticGain`, `SDL_SetHapticAutocenter`, `SDL_PauseHaptic`, `SDL_ResumeHaptic`, `SDL_GetHapticFromID`, `SDL_IsJoystickHaptic`, `SDL_IsMouseHaptic`.

Structs (layouts verified against the header at plan time):

- `SDL_HapticDirection` — `byte type; fixed int dir[3];`
- `SDL_HapticConstant`, `SDL_HapticPeriodic`, `SDL_HapticCondition` (per-axis `fixed ushort/short [3]` fields), `SDL_HapticRamp`, `SDL_HapticLeftRight`, `SDL_HapticCustom` (`byte channels; ushort period; ushort samples; ushort* data;`)
- `SDL_HapticEffect` — `[StructLayout(LayoutKind.Explicit)]` union over `ushort type` + the six structs (same pattern as `SDL_Event`).

Constants: the `SDL_HAPTIC_*` defines (~25) as a `ushort`/`uint` const group in the native class — effect-type bits (CONSTANT, SINE, SQUARE, TRIANGLE, SAWTOOTHUP, SAWTOOTHDOWN, RAMP, SPRING, DAMPER, INERTIA, FRICTION, LEFTRIGHT, CUSTOM, GAIN, AUTOCENTER, STATUS, PAUSE), direction encodings (POLAR, CARTESIAN, SPHERICAL, STEERING_AXIS), and `SDL_HAPTIC_INFINITY`.

### Managed layer (`SdlSharp.Input`)

**Direction:** `HapticDirectionType` enum (Polar, Cartesian, Spherical, SteeringAxis — cast from native constants) and `readonly record struct HapticDirection(HapticDirectionType Type, int Dir0 = 0, int Dir1 = 0, int Dir2 = 0)` with an internal `ToNative()`.

**Effect records** (each with internal `ToNative()` producing a filled `SDL_HapticEffect`):

- `HapticConstantEffect(HapticDirection Direction, uint LengthMs, ushort DelayMs, short Level, ushort AttackLengthMs, ushort AttackLevel, ushort FadeLengthMs, ushort FadeLevel, ushort Button = 0, ushort IntervalMs = 0)`
- `HapticPeriodicEffect(HapticWaveform Waveform, HapticDirection Direction, uint LengthMs, ushort DelayMs, ushort PeriodMs, short Magnitude, short Offset, ushort Phase, envelope..., button/interval)` — `HapticWaveform` enum: Sine, Square, Triangle, SawtoothUp, SawtoothDown (maps to the native effect-type constant)
- `HapticConditionEffect(HapticConditionKind Kind, HapticDirection Direction, uint LengthMs, ushort DelayMs, per-axis triples: RightSat, LeftSat, RightCoeff, LeftCoeff, Deadband, Center, button/interval)` — `HapticConditionKind` enum: Spring, Damper, Inertia, Friction. Per-axis triples surface as three-element parameters (`(ushort X, ushort Y, ushort Z)` value tuples or explicit Axis0/1/2 fields — plan picks one shape and uses it consistently).
- `HapticRampEffect(...)` — start/end levels + envelope
- `HapticLeftRightEffect(uint LengthMs, ushort LargeMagnitude, ushort SmallMagnitude)`
- `HapticCustomEffect(HapticDirection Direction, byte Channels, ushort PeriodMs, ushort[] Data, envelope..., button/interval)` — `Data` length must equal samples × channels (samples derived); the array is pinned only for the duration of the native create/update call (SDL copies the data). Throws ArgumentException on length mismatch.

Exact field lists come from the header structs during planning; the rule: every native struct field is surfaced except `type` (implied by the record kind) and padding.

**`HapticEffect` handle class** (`sealed`): internal ctor takes the owning `Haptic` + effect id. Members: `Run(uint iterations = 1)`, `RunInfinite()` (SDL_HAPTIC_INFINITY), `Stop()`, `Update(<matching effect record>)` (same-type-only per SDL contract; documented), `IsRunning` (wraps `SDL_GetHapticEffectStatus`), `Dispose()` → `SDL_DestroyHapticEffect` — skipped when the owning `Haptic` is already disposed (GPU-resource precedent). `Haptic` gains `internal bool IsDisposed`.

**`Haptic` additions:** `Id` (uint), `static FromId(uint)` (non-owning, "new wrapper per access" caveat), `Features` (`HapticFeatures` flags enum over the effect-type + GAIN/AUTOCENTER/STATUS/PAUSE bits), `NumAxes`, `MaxEffects`, `MaxEffectsPlaying`, `EffectSupported(record)` overloads, `CreateEffect(record)` overloads returning `HapticEffect`, `StopAllEffects()`, `Pause()`/`Resume()`, `Gain` (set-only property or `SetGain(int)` 0–100), `Autocenter` (`SetAutocenter(int)` 0–100), `static bool IsMouseHaptic`. `Joystick` gains `IsHaptic` property (`SDL_IsJoystickHaptic`).

## 11.2 Tray

### Native layer (new `src/SdlSharp/Native/Tray.cs`)

All 23 functions; opaque `SDL_Tray`, `SDL_TrayMenu`, `SDL_TrayEntry`; `SDL_TrayEntryFlags` (uint flags: BUTTON, CHECKBOX, SUBMENU, DISABLED, CHECKED); callback param `delegate* unmanaged[Cdecl]<void*, SDL_TrayEntry*, void>`. String params as `ReadOnlySpan<byte>`; `SDL_GetTrayEntryLabel` returns `byte*`. `SDL_GetTrayEntries` returns `SDL_TrayEntry**` + out count (SDL-owned array — copied, not freed).

### Managed layer (root `SdlSharp` namespace, files `Tray.cs`, `TrayMenu.cs`, `TrayEntry.cs`, `TrayEntryFlags.cs`)

- **`Tray`** — sealed, owning, `IDisposable`, house handle pattern. `static Create(Graphics.Surface? icon = null, string? tooltip = null)`, `SetIcon(Surface?)`, `SetTooltip(string?)`, `CreateMenu()` → `TrayMenu`, `Menu` (getter, null if none), `static void Update()` (`SDL_UpdateTrays`). Holds the **callback registry**: `Dictionary<nint, GCHandle>` keyed by entry pointer, lock-protected. `Dispose()` destroys the tray first, then frees every registry handle (destroy stops all callbacks).
- **`TrayMenu`** — non-owning view (internal ctor: menu pointer + owning `Tray`). `InsertEntry(int index, string? label, TrayEntryFlags flags)` → `TrayEntry` (index -1 appends; null label = separator; Check on null return unless label is null and SDL still returns an entry — follow header contract), `Entries` (`TrayEntry[]` snapshot), `ParentEntry` (null for root menu), `ParentTray`.
- **`TrayEntry`** — non-owning view (internal ctor: entry pointer + owning `Tray`). `Label` get/set (null = separator), `IsChecked` get/set, `IsEnabled` get/set, `CreateSubmenu()` / `Submenu` getter, `Click()`, `Remove()` (frees this entry's registry handle first, then `SDL_RemoveTrayEntry`; handles of a removed submenu's descendant entries remain in the registry until `Tray.Dispose` — memory-safe, documented), `Parent` (`TrayMenu`), `SetCallback(Action<TrayEntry>?)` — GCHandle trampoline per house pattern; the holder captures the owning `Tray` so the callback receives a fresh non-owning `TrayEntry`; setting replaces (frees old registry handle), null clears.
- Validity contract documented on all three types: menus/entries are owned by the tray and become invalid when the tray is disposed or the entry removed; wrappers are views, not lifetimes.
- Docs: tray APIs must be called on the main thread; callbacks fire during event processing.

## 11.3 Small items

| Item | Native | Managed |
|---|---|---|
| Version | New `Native/Version.cs`: `SDL_GetVersion` (int), `SDL_GetRevision` (`byte*`) | New static class `Sdl` (root ns): `Version` (System.Version via SDL_VERSIONNUM decode: major = v/1000000, minor = v/1000%1000, micro = v%1000), `Revision` (string?) |
| Hint priority | `SDL_SetHintWithPriority` already bound | `HintPriority` enum (Default/Normal/Override) + `SdlHints.Set(string name, string value, HintPriority priority)` overload |
| MessageBox | `SDL_ShowMessageBox` + `SDL_MessageBoxData`/`SDL_MessageBoxButtonData`/`SDL_MessageBoxColorScheme` structs in `Native/MessageBox.cs` | `Graphics.MessageBox.Show(Window? parent, string title, string message, MessageBoxButton[] buttons, MessageBoxFlags flags = ..., MessageBoxColorScheme? colorScheme = null)` → int (pressed button id; -1 if dismissed). `MessageBoxButton(int Id, string Text, bool IsDefaultReturn = false, bool IsDefaultEscape = false)` record; `MessageBoxColorScheme` record of five `Color`s. Not headless-runnable; build-only verification, doc note. |
| Property pointers | Bind `SDL_SetPointerProperty`; revise the existing skip comment (TODO decision supersedes it); `SDL_SetPointerPropertyWithCleanup` remains skipped (cleanup callback ties native lifetime to managed — not useful) | `PropertyGroup.SetPointer(string name, nint value)`, `GetPointer(string name, nint defaultValue = 0)` |
| Camera permission | `SDL_GetCameraPermissionState` already bound (orphan) | `CameraPermissionState` enum: Denied = -1, Waiting = 0, Approved = 1; `PermissionState` property on `Graphics.Camera` (`src/SdlSharp/Graphics/Camera.cs`) |
| Sensor polish | `SDL_GetSensorFromID` already bound (orphan) | `Sensor.Id` (uint), `static Sensor.FromId(uint)` (non-owning, caveat doc), `public const float StandardGravity = 9.80665f` |
| Touch constants | header macros | On the static `TouchDevice` class (`src/SdlSharp/Input/TouchDevice.cs`): `MouseId` (uint — the mouse `which` of mouse events synthesized from touch) and `MouseTouchDeviceId` (ulong — the touch-device id of touch events synthesized from mouse) |
| Filesystem | `SDL_CreateDirectory` already bound (orphan) | `SystemInfo.CreateDirectory(string path)` on the existing `src/SdlSharp/SystemInfo.cs` |
| System queries | Bind `SDL_IsTablet`, `SDL_IsTV`, `SDL_GetSandbox` | `SystemInfo.IsTablet`, `SystemInfo.IsTV`, `SystemInfo.Sandbox` + `SandboxEnvironment` enum (None/UnknownContainer/Flatpak/Snap/MacosSandbox) |
| Log emit | Bind `SDL_LogMessage` with fixed signature `(int category, SDL_LogPriority priority, ReadOnlySpan<byte> fmt, byte* message)` calling it only with `"%s"u8` | `SdlLog.Message(LogCategory, LogPriority, string)` + `Trace/Verbose/Debug/Info/Warn/Error/Critical(LogCategory, string)` convenience methods |

### Log-emit ABI contingency (explicit)

`SDL_LogMessage` is C-variadic. On macOS arm64 the C varargs ABI passes variadic arguments on the stack, while a fixed-signature P/Invoke passes the 4th argument in a register — the call may silently garble the string. The MiscOps sample MUST verify emit end-to-end: install a capture callback via `SdlLog.SetOutputFunction`, emit a known string, assert the captured text matches exactly. If this check fails on the target machine, REMOVE the emit binding and managed methods in the same phase and document the skip in `Native/Log.cs`: "SDL_LogMessage/SDL_Log* emit functions are C-variadic and not portably callable from .NET on arm64; use .NET logging and SDL_SetLogOutputFunction to bridge." Either outcome (working emit, or documented skip) is an acceptable completion of this item; shipping an unverified emit API is not.

## 11.4 Deferred.cs

New `src/SdlSharp/Native/Deferred.cs` containing only comments — one block per fully-skipped header with its settled rationale. Headers (verify the exact set against INVENTORY.md's deferred list at plan time): SDL_hidapi.h (raw HID access — niche, large surface, .NET has HidSharp ecosystem), SDL_metal.h / SDL_vulkan.h / SDL_egl.h / SDL_opengl*.h GL-header family (platform graphics-API interop headers; GL function loading is served by `Gl.GetProcAddress`), SDL_main.h + main-callbacks (managed apps own their entry point), SDL_stdinc.h / SDL_atomic.h / SDL_thread.h / SDL_mutex.h / SDL_rwlock.h etc. (.NET BCL equivalents), SDL_assert.h beyond what's bound, SDL_test\*.h (test harness, not part of the runtime API), SDL_intrin.h / SDL_endian.h / SDL_bits.h (compiler intrinsics; BCL equivalents), SDL_platform.h / SDL_platform_defines.h (compile-time macros). Each block: header name, one-sentence rationale, pointer to the .NET alternative where one exists.

## Verification

- **MiscOps sample** (headless, `Samples/MiscOps/`): version ≥ 3.4 and revision non-empty; hint set-with-priority then get round-trip; **log emit round-trip** (capture via output function, exact-match assert — the ABI gate); pointer property set/get round-trip (some sentinel nint); touch constants exact values; `Sensor.StandardGravity` value; `SystemInfo.IsTablet`/`IsTV` return false on desktop, `Sandbox` returns a valid enum member; `CreateDirectory` under the temp dir then `Directory.Exists`; haptic: `GetDevices()` returns without throwing (typically empty headless), `Haptic.IsMouseHaptic` no-crash. PASS/FAIL lines, non-zero exit on failure, registered in SdlSharp.slnx.
- **TrayDemo sample** (visible smoke, ~3 s): create tray with icon (small generated Surface) + tooltip; menu with button/checkbox/separator/submenu entries; entry callback wired; programmatic label/checked/enabled mutation + `Entries` snapshot + `Click()` invoking the callback (assert observed); remove an entry; dispose cleanly; auto-exit ≈3 s. A menu-bar icon appears briefly during the run (accepted). Registered in slnx; run once in the pipeline.
- MessageBox full form: build-only (modal dialog is not headless-safe); doc note on `Show`.
- Gates: zero-warning solution build; set-diffs — SDL_haptic.h 31/31 (0 skips), SDL_tray.h 23/23 (0 skips), and re-run for touched headers (hints/messagebox/properties/camera/sensor/touch/filesystem/system/log/version) to prove their new totals; orphan sweep over every newly-bound native; no-native-types gate on all new public surface; INVENTORY.md ✅ for all touched sections + a "fully deferred headers" section referencing Deferred.cs; TODO.md phase 11 checked off AND the phases 7–11 initiative marked complete; existing headless samples (InputInfo, SurfaceOps, AudioOps, EventQueueOps) regression-pass.

## Out of scope

- Any push of the `sdl3` branch (still user-deferred).
- New wrapping beyond the phase 11 list (post-initiative work like Span overloads for GPU void* APIs lives in separate chip sessions).
- ImGuiDemo interactive verification (remains a manual user step).
