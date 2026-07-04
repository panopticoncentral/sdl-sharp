# Phase 11: Final Surface Completion Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Complete the SDL3 surface: full haptic effect system (31/31), full tray wrap (23/23), all 11.3 small items, and settled skip documentation for every remaining header — closing the phases 7–11 initiative.

**Architecture:** Native bindings follow the house LibraryImport pattern; the haptic effect union mirrors the `SDL_Event` explicit-layout precedent with typed managed records converting via internal `ToNative()` (GpuDescriptors precedent); tray uses non-owning view wrappers over tray-owned entries with a per-tray GCHandle callback registry; small items mostly un-orphan already-bound natives. Verification: headless MiscOps sample (including the log-emit ABI gate) + a ~3s visible TrayDemo smoke.

**Tech Stack:** C# / .NET 10, `[LibraryImport]` P/Invoke, SDL 3.4.2. No unit-test framework — verification is the zero-warning build plus runtime-assertion samples (established phases 7–10 pattern).

**Spec:** `docs/superpowers/specs/2026-07-03-final-surface-completion-design.md`

**Conventions reminder (CLAUDE.md):** `[LibraryImport(Common.Sdl3, EntryPoint = "…")]` + `[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]`; `[return: MarshalAs(UnmanagedType.U1)]` on bool returns, `[MarshalAs(UnmanagedType.U1)]` on bool params; `ReadOnlySpan<byte>` string inputs via `Common.ToUtf8`; `byte*` string returns via `Marshal.PtrToStringUTF8`; XML docs mandatory on all public high-level members (CS1591 = build error); no `SdlSharp.Native` types in public signatures; `Common.Check`/`CheckId` → `SdlException`. Build gate after every task: `dotnet build SdlSharp.slnx` → **0 Warnings, 0 Errors** (stale LSP diagnostics are known ghosts; fresh build is authoritative). Commits have NO Co-Authored-By line. Never push.

---

## Task 1: Native haptic completion (structs, union, constants, 19 functions)

**Files:**
- Modify: `src/SdlSharp/Native/Haptic.cs`

Header facts (verified against `/Users/paulv/Projects/SDL/include/SDL3/SDL_haptic.h`):
- `typedef Uint16 SDL_HapticEffectType;` `typedef Uint8 SDL_HapticDirectionType;` `typedef int SDL_HapticEffectID;` (−1 = error from `SDL_CreateHapticEffect`)
- Effect-type bits: CONSTANT 1u<<0, SINE 1u<<1, SQUARE 1u<<2, TRIANGLE 1u<<3, SAWTOOTHUP 1u<<4, SAWTOOTHDOWN 1u<<5, RAMP 1u<<6, SPRING 1u<<7, DAMPER 1u<<8, INERTIA 1u<<9, FRICTION 1u<<10, LEFTRIGHT 1u<<11, CUSTOM 1u<<15; feature bits GAIN 1u<<16, AUTOCENTER 1u<<17, STATUS 1u<<18, PAUSE 1u<<19; directions POLAR 0, CARTESIAN 1, SPHERICAL 2, STEERING_AXIS 3; `SDL_HAPTIC_INFINITY` 4294967295U.

- [ ] **Step 1: Remove the stale "Deferred:" comment block** at the top of `Haptic.cs` (lines ~8–16, the block listing the effect system as deferred) — everything it lists is bound by this task.

- [ ] **Step 2: Add the ID typedef, constants, and structs** (before the `Haptic` partial class, alongside `SDL_HapticID`):

```csharp
/// <summary>
/// ID for a haptic effect uploaded to a device (-1 indicates failure).
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly record struct SDL_HapticEffectID(int Value);

/// <summary>
/// Direction encoding and axis values for a haptic effect.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct SDL_HapticDirection
{
    public byte type;
    public fixed int dir[3];
}

/// <summary>
/// Constant-force haptic effect.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_HapticConstant
{
    public ushort type;
    public SDL_HapticDirection direction;
    public uint length;
    public ushort delay;
    public ushort button;
    public ushort interval;
    public short level;
    public ushort attack_length;
    public ushort attack_level;
    public ushort fade_length;
    public ushort fade_level;
}

/// <summary>
/// Periodic (waveform) haptic effect.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_HapticPeriodic
{
    public ushort type;
    public SDL_HapticDirection direction;
    public uint length;
    public ushort delay;
    public ushort button;
    public ushort interval;
    public ushort period;
    public short magnitude;
    public short offset;
    public ushort phase;
    public ushort attack_length;
    public ushort attack_level;
    public ushort fade_length;
    public ushort fade_level;
}

/// <summary>
/// Axis-condition haptic effect (spring/damper/inertia/friction).
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct SDL_HapticCondition
{
    public ushort type;
    public SDL_HapticDirection direction;
    public uint length;
    public ushort delay;
    public ushort button;
    public ushort interval;
    public fixed ushort right_sat[3];
    public fixed ushort left_sat[3];
    public fixed short right_coeff[3];
    public fixed short left_coeff[3];
    public fixed ushort deadband[3];
    public fixed short center[3];
}

/// <summary>
/// Ramp haptic effect.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_HapticRamp
{
    public ushort type;
    public SDL_HapticDirection direction;
    public uint length;
    public ushort delay;
    public ushort button;
    public ushort interval;
    public short start;
    public short end;
    public ushort attack_length;
    public ushort attack_level;
    public ushort fade_length;
    public ushort fade_level;
}

/// <summary>
/// Left/right motor rumble haptic effect.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_HapticLeftRight
{
    public ushort type;
    public uint length;
    public ushort large_magnitude;
    public ushort small_magnitude;
}

/// <summary>
/// Custom-waveform haptic effect.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct SDL_HapticCustom
{
    public ushort type;
    public SDL_HapticDirection direction;
    public uint length;
    public ushort delay;
    public ushort button;
    public ushort interval;
    public byte channels;
    public ushort period;
    public ushort samples;
    public ushort* data;
    public ushort attack_length;
    public ushort attack_level;
    public ushort fade_length;
    public ushort fade_level;
}

/// <summary>
/// The generic haptic effect union (discriminated by type).
/// </summary>
[StructLayout(LayoutKind.Explicit)]
public struct SDL_HapticEffect
{
    [FieldOffset(0)] public ushort type;
    [FieldOffset(0)] public SDL_HapticConstant constant;
    [FieldOffset(0)] public SDL_HapticPeriodic periodic;
    [FieldOffset(0)] public SDL_HapticCondition condition;
    [FieldOffset(0)] public SDL_HapticRamp ramp;
    [FieldOffset(0)] public SDL_HapticLeftRight leftright;
    [FieldOffset(0)] public SDL_HapticCustom custom;
}
```

And inside the `Haptic` partial class, the constants:

```csharp
    public const uint SDL_HAPTIC_CONSTANT = 1u << 0;
    public const uint SDL_HAPTIC_SINE = 1u << 1;
    public const uint SDL_HAPTIC_SQUARE = 1u << 2;
    public const uint SDL_HAPTIC_TRIANGLE = 1u << 3;
    public const uint SDL_HAPTIC_SAWTOOTHUP = 1u << 4;
    public const uint SDL_HAPTIC_SAWTOOTHDOWN = 1u << 5;
    public const uint SDL_HAPTIC_RAMP = 1u << 6;
    public const uint SDL_HAPTIC_SPRING = 1u << 7;
    public const uint SDL_HAPTIC_DAMPER = 1u << 8;
    public const uint SDL_HAPTIC_INERTIA = 1u << 9;
    public const uint SDL_HAPTIC_FRICTION = 1u << 10;
    public const uint SDL_HAPTIC_LEFTRIGHT = 1u << 11;
    public const uint SDL_HAPTIC_CUSTOM = 1u << 15;
    public const uint SDL_HAPTIC_GAIN = 1u << 16;
    public const uint SDL_HAPTIC_AUTOCENTER = 1u << 17;
    public const uint SDL_HAPTIC_STATUS = 1u << 18;
    public const uint SDL_HAPTIC_PAUSE = 1u << 19;
    public const byte SDL_HAPTIC_POLAR = 0;
    public const byte SDL_HAPTIC_CARTESIAN = 1;
    public const byte SDL_HAPTIC_SPHERICAL = 2;
    public const byte SDL_HAPTIC_STEERING_AXIS = 3;
    public const uint SDL_HAPTIC_INFINITY = 4294967295u;
```

- [ ] **Step 3: Bind the 19 functions** (inside the partial class):

```csharp
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_CreateHapticEffect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_HapticEffectID SDL_CreateHapticEffect(SDL_Haptic* haptic, SDL_HapticEffect* effect);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_DestroyHapticEffect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_DestroyHapticEffect(SDL_Haptic* haptic, SDL_HapticEffectID effect);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_UpdateHapticEffect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_UpdateHapticEffect(SDL_Haptic* haptic, SDL_HapticEffectID effect, SDL_HapticEffect* data);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_RunHapticEffect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_RunHapticEffect(SDL_Haptic* haptic, SDL_HapticEffectID effect, uint iterations);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_StopHapticEffect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_StopHapticEffect(SDL_Haptic* haptic, SDL_HapticEffectID effect);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_StopHapticEffects")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_StopHapticEffects(SDL_Haptic* haptic);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetHapticEffectStatus")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GetHapticEffectStatus(SDL_Haptic* haptic, SDL_HapticEffectID effect);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_HapticEffectSupported")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_HapticEffectSupported(SDL_Haptic* haptic, SDL_HapticEffect* effect);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetHapticFeatures")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial uint SDL_GetHapticFeatures(SDL_Haptic* haptic);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetNumHapticAxes")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial int SDL_GetNumHapticAxes(SDL_Haptic* haptic);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetMaxHapticEffects")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial int SDL_GetMaxHapticEffects(SDL_Haptic* haptic);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetMaxHapticEffectsPlaying")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial int SDL_GetMaxHapticEffectsPlaying(SDL_Haptic* haptic);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetHapticGain")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetHapticGain(SDL_Haptic* haptic, int gain);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetHapticAutocenter")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetHapticAutocenter(SDL_Haptic* haptic, int autocenter);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_PauseHaptic")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_PauseHaptic(SDL_Haptic* haptic);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_ResumeHaptic")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_ResumeHaptic(SDL_Haptic* haptic);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetHapticFromID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Haptic* SDL_GetHapticFromID(SDL_HapticID instance_id);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_IsJoystickHaptic")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_IsJoystickHaptic(SDL_Joystick* joystick);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_IsMouseHaptic")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_IsMouseHaptic();
```

(`SDL_Joystick` is defined in `Native/Joystick.cs`, same namespace — no using needed.)

- [ ] **Step 4: Build** — `dotnet build SdlSharp.slnx` → 0 Warnings, 0 Errors.

- [ ] **Step 5: Commit**

```bash
git add src/SdlSharp/Native/Haptic.cs
git commit -m "Bind full haptic effect system: structs, union, constants, 19 functions"
```

---

## Task 2: Managed haptic effect records and enums

**Files:**
- Create: `src/SdlSharp/Input/HapticEffects.cs` (enums + direction + the six effect records)

- [ ] **Step 1: Create the file** with this exact content:

```csharp
using static SdlSharp.Native.Haptic;

namespace SdlSharp.Input;

/// <summary>
/// How a <see cref="HapticDirection"/> encodes its axis values.
/// </summary>
public enum HapticDirectionType : byte
{
    /// <summary>Direction as a polar angle in hundredths of a degree (Dir0).</summary>
    Polar = SDL_HAPTIC_POLAR,
    /// <summary>Direction as X/Y/Z cartesian axis values (Dir0..Dir2).</summary>
    Cartesian = SDL_HAPTIC_CARTESIAN,
    /// <summary>Direction as two spherical rotations in hundredths of a degree (Dir0, Dir1).</summary>
    Spherical = SDL_HAPTIC_SPHERICAL,
    /// <summary>Effect applies along the steering axis (wheels); direction values unused.</summary>
    SteeringAxis = SDL_HAPTIC_STEERING_AXIS,
}

/// <summary>
/// The waveform of a <see cref="HapticPeriodicEffect"/>.
/// </summary>
public enum HapticWaveform : ushort
{
    /// <summary>Sine wave.</summary>
    Sine = (ushort)SDL_HAPTIC_SINE,
    /// <summary>Square wave.</summary>
    Square = (ushort)SDL_HAPTIC_SQUARE,
    /// <summary>Triangle wave.</summary>
    Triangle = (ushort)SDL_HAPTIC_TRIANGLE,
    /// <summary>Rising sawtooth wave.</summary>
    SawtoothUp = (ushort)SDL_HAPTIC_SAWTOOTHUP,
    /// <summary>Falling sawtooth wave.</summary>
    SawtoothDown = (ushort)SDL_HAPTIC_SAWTOOTHDOWN,
}

/// <summary>
/// The kind of a <see cref="HapticConditionEffect"/>.
/// </summary>
public enum HapticConditionKind : ushort
{
    /// <summary>Force resists position displacement.</summary>
    Spring = (ushort)SDL_HAPTIC_SPRING,
    /// <summary>Force resists velocity.</summary>
    Damper = (ushort)SDL_HAPTIC_DAMPER,
    /// <summary>Force resists acceleration.</summary>
    Inertia = (ushort)SDL_HAPTIC_INERTIA,
    /// <summary>Force resists any movement.</summary>
    Friction = (ushort)SDL_HAPTIC_FRICTION,
}

/// <summary>
/// Capabilities of a haptic device, as reported by <see cref="Haptic.Features"/>.
/// </summary>
[Flags]
public enum HapticFeatures : uint
{
    /// <summary>No features.</summary>
    None = 0,
    /// <summary>Constant-force effects.</summary>
    Constant = SDL_HAPTIC_CONSTANT,
    /// <summary>Sine-wave periodic effects.</summary>
    Sine = SDL_HAPTIC_SINE,
    /// <summary>Square-wave periodic effects.</summary>
    Square = SDL_HAPTIC_SQUARE,
    /// <summary>Triangle-wave periodic effects.</summary>
    Triangle = SDL_HAPTIC_TRIANGLE,
    /// <summary>Rising-sawtooth periodic effects.</summary>
    SawtoothUp = SDL_HAPTIC_SAWTOOTHUP,
    /// <summary>Falling-sawtooth periodic effects.</summary>
    SawtoothDown = SDL_HAPTIC_SAWTOOTHDOWN,
    /// <summary>Ramp effects.</summary>
    Ramp = SDL_HAPTIC_RAMP,
    /// <summary>Spring condition effects.</summary>
    Spring = SDL_HAPTIC_SPRING,
    /// <summary>Damper condition effects.</summary>
    Damper = SDL_HAPTIC_DAMPER,
    /// <summary>Inertia condition effects.</summary>
    Inertia = SDL_HAPTIC_INERTIA,
    /// <summary>Friction condition effects.</summary>
    Friction = SDL_HAPTIC_FRICTION,
    /// <summary>Left/right motor rumble effects.</summary>
    LeftRight = SDL_HAPTIC_LEFTRIGHT,
    /// <summary>Custom-waveform effects.</summary>
    Custom = SDL_HAPTIC_CUSTOM,
    /// <summary>Global gain control.</summary>
    Gain = SDL_HAPTIC_GAIN,
    /// <summary>Autocenter control.</summary>
    Autocenter = SDL_HAPTIC_AUTOCENTER,
    /// <summary>Effect status queries.</summary>
    Status = SDL_HAPTIC_STATUS,
    /// <summary>Pausing effect playback.</summary>
    Pause = SDL_HAPTIC_PAUSE,
}

/// <summary>
/// The direction of a haptic effect.
/// </summary>
/// <param name="Type">How the axis values are encoded.</param>
/// <param name="Dir0">First encoded value (meaning depends on <paramref name="Type"/>).</param>
/// <param name="Dir1">Second encoded value.</param>
/// <param name="Dir2">Third encoded value.</param>
public readonly record struct HapticDirection(HapticDirectionType Type, int Dir0 = 0, int Dir1 = 0, int Dir2 = 0)
{
    internal unsafe Native.SDL_HapticDirection ToNative()
    {
        var d = new Native.SDL_HapticDirection { type = (byte)Type };
        d.dir[0] = Dir0;
        d.dir[1] = Dir1;
        d.dir[2] = Dir2;
        return d;
    }
}

/// <summary>
/// A constant-force haptic effect.
/// </summary>
/// <param name="Direction">The direction of the force.</param>
/// <param name="LengthMs">Effect duration in milliseconds, or <see cref="HapticEffect.Infinity"/>.</param>
/// <param name="DelayMs">Delay before starting, in milliseconds.</param>
/// <param name="Level">The force strength (-32768 to 32767).</param>
/// <param name="AttackLengthMs">Envelope attack duration in milliseconds.</param>
/// <param name="AttackLevel">Envelope level at the start of the attack.</param>
/// <param name="FadeLengthMs">Envelope fade duration in milliseconds.</param>
/// <param name="FadeLevel">Envelope level at the end of the fade.</param>
/// <param name="Button">Button that triggers the effect, or 0.</param>
/// <param name="IntervalMs">Minimum interval between triggers, in milliseconds.</param>
public readonly record struct HapticConstantEffect(
    HapticDirection Direction,
    uint LengthMs,
    ushort DelayMs,
    short Level,
    ushort AttackLengthMs = 0,
    ushort AttackLevel = 0,
    ushort FadeLengthMs = 0,
    ushort FadeLevel = 0,
    ushort Button = 0,
    ushort IntervalMs = 0)
{
    internal unsafe Native.SDL_HapticEffect ToNative() => new()
    {
        constant = new Native.SDL_HapticConstant
        {
            type = (ushort)SDL_HAPTIC_CONSTANT,
            direction = Direction.ToNative(),
            length = LengthMs,
            delay = DelayMs,
            button = Button,
            interval = IntervalMs,
            level = Level,
            attack_length = AttackLengthMs,
            attack_level = AttackLevel,
            fade_length = FadeLengthMs,
            fade_level = FadeLevel,
        }
    };
}

/// <summary>
/// A periodic (waveform) haptic effect.
/// </summary>
/// <param name="Waveform">The wave shape.</param>
/// <param name="Direction">The direction of the force.</param>
/// <param name="LengthMs">Effect duration in milliseconds, or <see cref="HapticEffect.Infinity"/>.</param>
/// <param name="DelayMs">Delay before starting, in milliseconds.</param>
/// <param name="PeriodMs">Wave period in milliseconds.</param>
/// <param name="Magnitude">Peak force (-32768 to 32767).</param>
/// <param name="Offset">Mean force offset.</param>
/// <param name="Phase">Phase shift in hundredths of a degree.</param>
/// <param name="AttackLengthMs">Envelope attack duration in milliseconds.</param>
/// <param name="AttackLevel">Envelope level at the start of the attack.</param>
/// <param name="FadeLengthMs">Envelope fade duration in milliseconds.</param>
/// <param name="FadeLevel">Envelope level at the end of the fade.</param>
/// <param name="Button">Button that triggers the effect, or 0.</param>
/// <param name="IntervalMs">Minimum interval between triggers, in milliseconds.</param>
public readonly record struct HapticPeriodicEffect(
    HapticWaveform Waveform,
    HapticDirection Direction,
    uint LengthMs,
    ushort DelayMs,
    ushort PeriodMs,
    short Magnitude,
    short Offset = 0,
    ushort Phase = 0,
    ushort AttackLengthMs = 0,
    ushort AttackLevel = 0,
    ushort FadeLengthMs = 0,
    ushort FadeLevel = 0,
    ushort Button = 0,
    ushort IntervalMs = 0)
{
    internal unsafe Native.SDL_HapticEffect ToNative() => new()
    {
        periodic = new Native.SDL_HapticPeriodic
        {
            type = (ushort)Waveform,
            direction = Direction.ToNative(),
            length = LengthMs,
            delay = DelayMs,
            button = Button,
            interval = IntervalMs,
            period = PeriodMs,
            magnitude = Magnitude,
            offset = Offset,
            phase = Phase,
            attack_length = AttackLengthMs,
            attack_level = AttackLevel,
            fade_length = FadeLengthMs,
            fade_level = FadeLevel,
        }
    };
}

/// <summary>
/// An axis-condition haptic effect (spring, damper, inertia, or friction).
/// Per-axis values are (X, Y, Z) triples; unused axes are ignored by the device.
/// </summary>
/// <param name="Kind">The condition kind.</param>
/// <param name="Direction">The direction (most condition effects ignore it; per SDL docs).</param>
/// <param name="LengthMs">Effect duration in milliseconds, or <see cref="HapticEffect.Infinity"/>.</param>
/// <param name="DelayMs">Delay before starting, in milliseconds.</param>
/// <param name="RightSaturation">Force level when the joystick is fully to the positive side, per axis.</param>
/// <param name="LeftSaturation">Force level when the joystick is fully to the negative side, per axis.</param>
/// <param name="RightCoefficient">How fast force grows toward the positive side, per axis.</param>
/// <param name="LeftCoefficient">How fast force grows toward the negative side, per axis.</param>
/// <param name="Deadband">Size of the zero-force dead zone, per axis.</param>
/// <param name="Center">Position of the dead zone, per axis.</param>
/// <param name="Button">Button that triggers the effect, or 0.</param>
/// <param name="IntervalMs">Minimum interval between triggers, in milliseconds.</param>
public readonly record struct HapticConditionEffect(
    HapticConditionKind Kind,
    HapticDirection Direction,
    uint LengthMs,
    ushort DelayMs,
    (ushort X, ushort Y, ushort Z) RightSaturation,
    (ushort X, ushort Y, ushort Z) LeftSaturation,
    (short X, short Y, short Z) RightCoefficient,
    (short X, short Y, short Z) LeftCoefficient,
    (ushort X, ushort Y, ushort Z) Deadband = default,
    (short X, short Y, short Z) Center = default,
    ushort Button = 0,
    ushort IntervalMs = 0)
{
    internal unsafe Native.SDL_HapticEffect ToNative()
    {
        var e = new Native.SDL_HapticEffect();
        ref var c = ref e.condition;
        c.type = (ushort)Kind;
        c.direction = Direction.ToNative();
        c.length = LengthMs;
        c.delay = DelayMs;
        c.button = Button;
        c.interval = IntervalMs;
        c.right_sat[0] = RightSaturation.X; c.right_sat[1] = RightSaturation.Y; c.right_sat[2] = RightSaturation.Z;
        c.left_sat[0] = LeftSaturation.X; c.left_sat[1] = LeftSaturation.Y; c.left_sat[2] = LeftSaturation.Z;
        c.right_coeff[0] = RightCoefficient.X; c.right_coeff[1] = RightCoefficient.Y; c.right_coeff[2] = RightCoefficient.Z;
        c.left_coeff[0] = LeftCoefficient.X; c.left_coeff[1] = LeftCoefficient.Y; c.left_coeff[2] = LeftCoefficient.Z;
        c.deadband[0] = Deadband.X; c.deadband[1] = Deadband.Y; c.deadband[2] = Deadband.Z;
        c.center[0] = Center.X; c.center[1] = Center.Y; c.center[2] = Center.Z;
        return e;
    }
}

/// <summary>
/// A ramp haptic effect: force sweeps linearly from a start to an end level.
/// </summary>
/// <param name="Direction">The direction of the force.</param>
/// <param name="LengthMs">Effect duration in milliseconds (ramps cannot be infinite).</param>
/// <param name="DelayMs">Delay before starting, in milliseconds.</param>
/// <param name="Start">Force at the beginning (-32768 to 32767).</param>
/// <param name="End">Force at the end.</param>
/// <param name="AttackLengthMs">Envelope attack duration in milliseconds.</param>
/// <param name="AttackLevel">Envelope level at the start of the attack.</param>
/// <param name="FadeLengthMs">Envelope fade duration in milliseconds.</param>
/// <param name="FadeLevel">Envelope level at the end of the fade.</param>
/// <param name="Button">Button that triggers the effect, or 0.</param>
/// <param name="IntervalMs">Minimum interval between triggers, in milliseconds.</param>
public readonly record struct HapticRampEffect(
    HapticDirection Direction,
    uint LengthMs,
    ushort DelayMs,
    short Start,
    short End,
    ushort AttackLengthMs = 0,
    ushort AttackLevel = 0,
    ushort FadeLengthMs = 0,
    ushort FadeLevel = 0,
    ushort Button = 0,
    ushort IntervalMs = 0)
{
    internal unsafe Native.SDL_HapticEffect ToNative() => new()
    {
        ramp = new Native.SDL_HapticRamp
        {
            type = (ushort)SDL_HAPTIC_RAMP,
            direction = Direction.ToNative(),
            length = LengthMs,
            delay = DelayMs,
            button = Button,
            interval = IntervalMs,
            start = Start,
            end = End,
            attack_length = AttackLengthMs,
            attack_level = AttackLevel,
            fade_length = FadeLengthMs,
            fade_level = FadeLevel,
        }
    };
}

/// <summary>
/// A left/right motor rumble effect (the simple dual-motor model).
/// </summary>
/// <param name="LengthMs">Effect duration in milliseconds, or <see cref="HapticEffect.Infinity"/>.</param>
/// <param name="LargeMagnitude">Strength of the large (low-frequency) motor.</param>
/// <param name="SmallMagnitude">Strength of the small (high-frequency) motor.</param>
public readonly record struct HapticLeftRightEffect(
    uint LengthMs,
    ushort LargeMagnitude,
    ushort SmallMagnitude)
{
    internal Native.SDL_HapticEffect ToNative() => new()
    {
        leftright = new Native.SDL_HapticLeftRight
        {
            type = (ushort)SDL_HAPTIC_LEFTRIGHT,
            length = LengthMs,
            large_magnitude = LargeMagnitude,
            small_magnitude = SmallMagnitude,
        }
    };
}

/// <summary>
/// A custom-waveform haptic effect. <see cref="Data"/> holds interleaved samples for
/// <see cref="Channels"/> channels; its length must be a multiple of the channel count.
/// SDL copies the data during create/update, so the array is only pinned for that call.
/// </summary>
/// <param name="Direction">The direction of the force.</param>
/// <param name="Channels">Number of channels (axes) the samples drive.</param>
/// <param name="PeriodMs">Sample period in milliseconds.</param>
/// <param name="Data">Interleaved sample data; length must be a multiple of <paramref name="Channels"/>.</param>
/// <param name="LengthMs">Effect duration in milliseconds, or <see cref="HapticEffect.Infinity"/>.</param>
/// <param name="DelayMs">Delay before starting, in milliseconds.</param>
/// <param name="AttackLengthMs">Envelope attack duration in milliseconds.</param>
/// <param name="AttackLevel">Envelope level at the start of the attack.</param>
/// <param name="FadeLengthMs">Envelope fade duration in milliseconds.</param>
/// <param name="FadeLevel">Envelope level at the end of the fade.</param>
/// <param name="Button">Button that triggers the effect, or 0.</param>
/// <param name="IntervalMs">Minimum interval between triggers, in milliseconds.</param>
public readonly record struct HapticCustomEffect(
    HapticDirection Direction,
    byte Channels,
    ushort PeriodMs,
    ushort[] Data,
    uint LengthMs,
    ushort DelayMs = 0,
    ushort AttackLengthMs = 0,
    ushort AttackLevel = 0,
    ushort FadeLengthMs = 0,
    ushort FadeLevel = 0,
    ushort Button = 0,
    ushort IntervalMs = 0)
{
    internal unsafe Native.SDL_HapticEffect ToNative(ushort* pinnedData)
    {
        if (Channels == 0 || Data == null || Data.Length == 0 || Data.Length % Channels != 0)
        {
            throw new ArgumentException("Data length must be a positive multiple of the channel count.");
        }

        return new Native.SDL_HapticEffect
        {
            custom = new Native.SDL_HapticCustom
            {
                type = (ushort)SDL_HAPTIC_CUSTOM,
                direction = Direction.ToNative(),
                length = LengthMs,
                delay = DelayMs,
                button = Button,
                interval = IntervalMs,
                channels = Channels,
                period = PeriodMs,
                samples = (ushort)(Data.Length / Channels),
                data = pinnedData,
                attack_length = AttackLengthMs,
                attack_level = AttackLevel,
                fade_length = FadeLengthMs,
                fade_level = FadeLevel,
            }
        };
    }
}
```

Note: `unsafe` methods on record structs require `<AllowUnsafeBlocks>` (already on) — no `unsafe` modifier needed on the record itself, only on the method. Fixed-buffer element access (`d.dir[0]`, `c.right_sat[0]`) is legal inside an `unsafe` method on a local/ref of the containing struct.

- [ ] **Step 2: Build** — 0 Warnings, 0 Errors. (`HapticEffect.Infinity` crefs will not resolve until Task 3 — if the build flags CS1574 as a WARNING here, temporarily expect it and confirm Task 3 clears it; alternatively implement Tasks 2+3 in one commit. Preferred: keep one commit per task but build Tasks 2 and 3 together before committing Task 2 — the executor may reorder the two builds so the gate holds at each commit. Simplest compliant route: write Task 3's `HapticEffect` class FIRST within this same task if the cref warning appears, then commit both files in Task 2's commit and make Task 3 a no-op verification. Decide by what keeps every commit at 0 warnings.)

- [ ] **Step 3: Commit**

```bash
git add src/SdlSharp/Input/HapticEffects.cs
git commit -m "Add managed haptic effect records, direction, and capability enums"
```

---

## Task 3: HapticEffect handle + Haptic/Joystick additions

**Files:**
- Create: `src/SdlSharp/Input/HapticEffect.cs`
- Modify: `src/SdlSharp/Input/Haptic.cs`
- Modify: `src/SdlSharp/Input/Joystick.cs`

- [ ] **Step 1: Create `HapticEffect.cs`:**

```csharp
using static SdlSharp.Native.Common;
using static SdlSharp.Native.Haptic;

namespace SdlSharp.Input;

/// <summary>
/// A haptic effect uploaded to a device. Created via <see cref="Haptic.CreateEffect(in HapticConstantEffect)"/>
/// and overloads. Dispose to free the effect slot; effects become invalid when the
/// owning <see cref="Haptic"/> device is disposed.
/// </summary>
public sealed unsafe class HapticEffect : IDisposable
{
    /// <summary>Pass as an iteration count or effect length to repeat/run forever.</summary>
    public const uint Infinity = SDL_HAPTIC_INFINITY;

    private readonly Haptic _device;
    private int _id;

    internal HapticEffect(Haptic device, int id)
    {
        _device = device;
        _id = id;
    }

    private Native.SDL_HapticEffectID Id
    {
        get
        {
            ObjectDisposedException.ThrowIf(_id < 0, this);
            return new Native.SDL_HapticEffectID(_id);
        }
    }

    /// <summary>
    /// Runs the effect.
    /// </summary>
    /// <param name="iterations">How many times to repeat, or <see cref="Infinity"/> to repeat until stopped.</param>
    public void Run(uint iterations = 1) => Check(SDL_RunHapticEffect(_device.Handle, Id, iterations));

    /// <summary>Stops playback of this effect.</summary>
    public void Stop() => Check(SDL_StopHapticEffect(_device.Handle, Id));

    /// <summary>
    /// Gets whether the effect is currently playing. Requires the device to support
    /// <see cref="HapticFeatures.Status"/>; returns false (with no exception) when
    /// unsupported or on error.
    /// </summary>
    public bool IsRunning => SDL_GetHapticEffectStatus(_device.Handle, Id);

    /// <summary>Updates this effect in place. The effect kind cannot change (SDL contract).</summary>
    /// <param name="effect">The new parameters.</param>
    public void Update(in HapticConstantEffect effect)
    {
        var native = effect.ToNative();
        Check(SDL_UpdateHapticEffect(_device.Handle, Id, &native));
    }

    /// <summary>Updates this effect in place. The effect kind (waveform family) cannot change (SDL contract).</summary>
    /// <param name="effect">The new parameters.</param>
    public void Update(in HapticPeriodicEffect effect)
    {
        var native = effect.ToNative();
        Check(SDL_UpdateHapticEffect(_device.Handle, Id, &native));
    }

    /// <summary>Updates this effect in place. The effect kind cannot change (SDL contract).</summary>
    /// <param name="effect">The new parameters.</param>
    public void Update(in HapticConditionEffect effect)
    {
        var native = effect.ToNative();
        Check(SDL_UpdateHapticEffect(_device.Handle, Id, &native));
    }

    /// <summary>Updates this effect in place. The effect kind cannot change (SDL contract).</summary>
    /// <param name="effect">The new parameters.</param>
    public void Update(in HapticRampEffect effect)
    {
        var native = effect.ToNative();
        Check(SDL_UpdateHapticEffect(_device.Handle, Id, &native));
    }

    /// <summary>Updates this effect in place. The effect kind cannot change (SDL contract).</summary>
    /// <param name="effect">The new parameters.</param>
    public void Update(in HapticLeftRightEffect effect)
    {
        var native = effect.ToNative();
        Check(SDL_UpdateHapticEffect(_device.Handle, Id, &native));
    }

    /// <summary>Updates this effect in place. The effect kind cannot change (SDL contract).</summary>
    /// <param name="effect">The new parameters.</param>
    public void Update(in HapticCustomEffect effect)
    {
        fixed (ushort* data = effect.Data)
        {
            var native = effect.ToNative(data);
            Check(SDL_UpdateHapticEffect(_device.Handle, Id, &native));
        }
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_id >= 0 && !_device.IsDisposed)
        {
            SDL_DestroyHapticEffect(_device.Handle, new Native.SDL_HapticEffectID(_id));
        }

        _id = -1;
    }
}
```

- [ ] **Step 2: Add to `Haptic.cs`** (inside the class):

```csharp
    internal bool IsDisposed => _handle == null;

    /// <summary>Gets the instance ID of this haptic device.</summary>
    public uint Id => SDL_GetHapticID(Handle).Value;

    /// <summary>
    /// Gets an existing opened haptic device by instance ID. The returned wrapper does
    /// not own the device; each access returns a new wrapper and wrappers are not equal
    /// to each other.
    /// </summary>
    /// <param name="id">The haptic instance ID.</param>
    public static Haptic FromId(uint id) =>
        new(Check(SDL_GetHapticFromID(new Native.SDL_HapticID(id))), ownsHandle: false);

    /// <summary>Gets the effect kinds and controls this device supports.</summary>
    public HapticFeatures Features => (HapticFeatures)SDL_GetHapticFeatures(Handle);

    /// <summary>Gets the number of axes the device can use for effects.</summary>
    public int NumAxes
    {
        get
        {
            var result = SDL_GetNumHapticAxes(Handle);
            if (result < 0) throw new SdlException();
            return result;
        }
    }

    /// <summary>Gets how many effects the device can store.</summary>
    public int MaxEffects
    {
        get
        {
            var result = SDL_GetMaxHapticEffects(Handle);
            if (result < 0) throw new SdlException();
            return result;
        }
    }

    /// <summary>Gets how many effects the device can play at the same time.</summary>
    public int MaxEffectsPlaying
    {
        get
        {
            var result = SDL_GetMaxHapticEffectsPlaying(Handle);
            if (result < 0) throw new SdlException();
            return result;
        }
    }

    /// <summary>Gets whether the mouse has haptic capabilities.</summary>
    public static bool IsMouseHaptic => SDL_IsMouseHaptic();

    /// <summary>Stops playback of all effects on this device.</summary>
    public void StopAllEffects() => Check(SDL_StopHapticEffects(Handle));

    /// <summary>Pauses effect playback on this device (device must support <see cref="HapticFeatures.Pause"/>).</summary>
    public void Pause() => Check(SDL_PauseHaptic(Handle));

    /// <summary>Resumes effect playback after <see cref="Pause"/>.</summary>
    public void Resume() => Check(SDL_ResumeHaptic(Handle));

    /// <summary>Sets the global gain (0-100; device must support <see cref="HapticFeatures.Gain"/>).</summary>
    /// <param name="gain">The gain percentage, 0 to 100.</param>
    public void SetGain(int gain) => Check(SDL_SetHapticGain(Handle, gain));

    /// <summary>Sets autocentering strength (0 = off, 100 = strongest; device must support <see cref="HapticFeatures.Autocenter"/>).</summary>
    /// <param name="autocenter">The autocenter percentage, 0 to 100.</param>
    public void SetAutocenter(int autocenter) => Check(SDL_SetHapticAutocenter(Handle, autocenter));

    private HapticEffect CreateEffectCore(Native.SDL_HapticEffect* native)
    {
        var id = SDL_CreateHapticEffect(Handle, native);
        if (id.Value < 0) throw new SdlException();
        return new HapticEffect(this, id.Value);
    }

    private bool SupportedCore(Native.SDL_HapticEffect* native) => SDL_HapticEffectSupported(Handle, native);

    /// <summary>Uploads a constant-force effect to the device.</summary>
    /// <param name="effect">The effect parameters.</param>
    public HapticEffect CreateEffect(in HapticConstantEffect effect)
    {
        var native = effect.ToNative();
        return CreateEffectCore(&native);
    }

    /// <summary>Uploads a periodic effect to the device.</summary>
    /// <param name="effect">The effect parameters.</param>
    public HapticEffect CreateEffect(in HapticPeriodicEffect effect)
    {
        var native = effect.ToNative();
        return CreateEffectCore(&native);
    }

    /// <summary>Uploads a condition effect to the device.</summary>
    /// <param name="effect">The effect parameters.</param>
    public HapticEffect CreateEffect(in HapticConditionEffect effect)
    {
        var native = effect.ToNative();
        return CreateEffectCore(&native);
    }

    /// <summary>Uploads a ramp effect to the device.</summary>
    /// <param name="effect">The effect parameters.</param>
    public HapticEffect CreateEffect(in HapticRampEffect effect)
    {
        var native = effect.ToNative();
        return CreateEffectCore(&native);
    }

    /// <summary>Uploads a left/right rumble effect to the device.</summary>
    /// <param name="effect">The effect parameters.</param>
    public HapticEffect CreateEffect(in HapticLeftRightEffect effect)
    {
        var native = effect.ToNative();
        return CreateEffectCore(&native);
    }

    /// <summary>Uploads a custom-waveform effect to the device (the sample data is copied by SDL).</summary>
    /// <param name="effect">The effect parameters.</param>
    public HapticEffect CreateEffect(in HapticCustomEffect effect)
    {
        fixed (ushort* data = effect.Data)
        {
            var native = effect.ToNative(data);
            return CreateEffectCore(&native);
        }
    }

    /// <summary>Gets whether the device supports the given effect (no-throw).</summary>
    /// <param name="effect">The effect parameters.</param>
    public bool SupportsEffect(in HapticConstantEffect effect)
    {
        var native = effect.ToNative();
        return SupportedCore(&native);
    }

    /// <summary>Gets whether the device supports the given effect (no-throw).</summary>
    /// <param name="effect">The effect parameters.</param>
    public bool SupportsEffect(in HapticPeriodicEffect effect)
    {
        var native = effect.ToNative();
        return SupportedCore(&native);
    }

    /// <summary>Gets whether the device supports the given effect (no-throw).</summary>
    /// <param name="effect">The effect parameters.</param>
    public bool SupportsEffect(in HapticConditionEffect effect)
    {
        var native = effect.ToNative();
        return SupportedCore(&native);
    }

    /// <summary>Gets whether the device supports the given effect (no-throw).</summary>
    /// <param name="effect">The effect parameters.</param>
    public bool SupportsEffect(in HapticRampEffect effect)
    {
        var native = effect.ToNative();
        return SupportedCore(&native);
    }

    /// <summary>Gets whether the device supports the given effect (no-throw).</summary>
    /// <param name="effect">The effect parameters.</param>
    public bool SupportsEffect(in HapticLeftRightEffect effect)
    {
        var native = effect.ToNative();
        return SupportedCore(&native);
    }

    /// <summary>Gets whether the device supports the given effect (no-throw).</summary>
    /// <param name="effect">The effect parameters.</param>
    public bool SupportsEffect(in HapticCustomEffect effect)
    {
        fixed (ushort* data = effect.Data)
        {
            var native = effect.ToNative(data);
            return SupportedCore(&native);
        }
    }
```

Confirm `SDL_GetHapticID` is bound returning `SDL_HapticID` (it is, per the bound list); if its return needs `.Value`, adjust to match.

- [ ] **Step 3: Add to `Joystick.cs`:**

```csharp
    /// <summary>Gets whether this joystick has haptic (force feedback) capabilities.</summary>
    public bool IsHaptic => Native.Haptic.SDL_IsJoystickHaptic(Handle);
```

- [ ] **Step 4: Build** — 0 Warnings, 0 Errors (this also clears any Task 2 cref forward-reference).

- [ ] **Step 5: Commit**

```bash
git add src/SdlSharp/Input/HapticEffect.cs src/SdlSharp/Input/Haptic.cs src/SdlSharp/Input/Joystick.cs
git commit -m "Add HapticEffect handle and complete managed haptic surface"
```

---

## Task 4: Native/Tray.cs

**Files:**
- Create: `src/SdlSharp/Native/Tray.cs`

All 23 prototypes were verified against `/Users/paulv/Projects/SDL/include/SDL3/SDL_tray.h`. `SDL_TrayCallback` = `void(void* userdata, SDL_TrayEntry* entry)`.

- [ ] **Step 1: Create the file:**

```csharp
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace SdlSharp.Native;

/// <summary>Opaque handle for a system tray icon.</summary>
public struct SDL_Tray;

/// <summary>Opaque handle for a tray menu.</summary>
public struct SDL_TrayMenu;

/// <summary>Opaque handle for a tray menu entry.</summary>
public struct SDL_TrayEntry;

/// <summary>
/// Native bindings for SDL_tray.h — system tray icons and menus.
/// </summary>
[SuppressMessage("Interoperability", "CA1401:P/Invokes should not be visible")]
public static partial class Tray
{
    public const uint SDL_TRAYENTRY_BUTTON = 0x00000001u;
    public const uint SDL_TRAYENTRY_CHECKBOX = 0x00000002u;
    public const uint SDL_TRAYENTRY_SUBMENU = 0x00000004u;
    public const uint SDL_TRAYENTRY_DISABLED = 0x80000000u;
    public const uint SDL_TRAYENTRY_CHECKED = 0x40000000u;

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_CreateTray")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Tray* SDL_CreateTray(SDL_Surface* icon, ReadOnlySpan<byte> tooltip);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetTrayIcon")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_SetTrayIcon(SDL_Tray* tray, SDL_Surface* icon);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetTrayTooltip")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_SetTrayTooltip(SDL_Tray* tray, ReadOnlySpan<byte> tooltip);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_CreateTrayMenu")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_TrayMenu* SDL_CreateTrayMenu(SDL_Tray* tray);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_CreateTraySubmenu")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_TrayMenu* SDL_CreateTraySubmenu(SDL_TrayEntry* entry);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetTrayMenu")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_TrayMenu* SDL_GetTrayMenu(SDL_Tray* tray);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetTraySubmenu")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_TrayMenu* SDL_GetTraySubmenu(SDL_TrayEntry* entry);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetTrayEntries")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_TrayEntry** SDL_GetTrayEntries(SDL_TrayMenu* menu, out int count);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_RemoveTrayEntry")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_RemoveTrayEntry(SDL_TrayEntry* entry);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_InsertTrayEntryAt")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_TrayEntry* SDL_InsertTrayEntryAt(SDL_TrayMenu* menu, int pos, ReadOnlySpan<byte> label, uint flags);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetTrayEntryLabel")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_SetTrayEntryLabel(SDL_TrayEntry* entry, ReadOnlySpan<byte> label);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetTrayEntryLabel")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* SDL_GetTrayEntryLabel(SDL_TrayEntry* entry);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetTrayEntryChecked")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_SetTrayEntryChecked(SDL_TrayEntry* entry, [MarshalAs(UnmanagedType.U1)] bool @checked);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetTrayEntryChecked")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GetTrayEntryChecked(SDL_TrayEntry* entry);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetTrayEntryEnabled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_SetTrayEntryEnabled(SDL_TrayEntry* entry, [MarshalAs(UnmanagedType.U1)] bool enabled);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetTrayEntryEnabled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GetTrayEntryEnabled(SDL_TrayEntry* entry);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetTrayEntryCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_SetTrayEntryCallback(SDL_TrayEntry* entry, delegate* unmanaged[Cdecl]<void*, SDL_TrayEntry*, void> callback, void* userdata);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_ClickTrayEntry")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_ClickTrayEntry(SDL_TrayEntry* entry);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_DestroyTray")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_DestroyTray(SDL_Tray* tray);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetTrayEntryParent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_TrayMenu* SDL_GetTrayEntryParent(SDL_TrayEntry* entry);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetTrayMenuParentEntry")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_TrayEntry* SDL_GetTrayMenuParentEntry(SDL_TrayMenu* menu);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetTrayMenuParentTray")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Tray* SDL_GetTrayMenuParentTray(SDL_TrayMenu* menu);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_UpdateTrays")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_UpdateTrays();
}
```

Note: passing `null` for a `ReadOnlySpan<byte>` string param — `Common.ToUtf8(null)` returns null `byte[]?`, which marshals as a null pointer (established pattern). `SDL_Surface` is defined in `Native/Surface.cs`, same namespace.

- [ ] **Step 2: Build** — 0 Warnings, 0 Errors.

- [ ] **Step 3: Commit**

```bash
git add src/SdlSharp/Native/Tray.cs
git commit -m "Bind SDL_tray.h: 23 functions, opaque types, entry flags"
```

---

## Task 5: Managed Tray / TrayMenu / TrayEntry

**Files:**
- Create: `src/SdlSharp/TrayEntryFlags.cs`
- Create: `src/SdlSharp/Tray.cs`
- Create: `src/SdlSharp/TrayMenu.cs`
- Create: `src/SdlSharp/TrayEntry.cs`

- [ ] **Step 1: Create `TrayEntryFlags.cs`:**

```csharp
namespace SdlSharp;

/// <summary>
/// Flags controlling the kind and initial state of a tray menu entry.
/// Exactly one of <see cref="Button"/>, <see cref="Checkbox"/>, or <see cref="Submenu"/> is required.
/// </summary>
[Flags]
public enum TrayEntryFlags : uint
{
    /// <summary>A simple clickable button.</summary>
    Button = Native.Tray.SDL_TRAYENTRY_BUTTON,
    /// <summary>A checkbox entry.</summary>
    Checkbox = Native.Tray.SDL_TRAYENTRY_CHECKBOX,
    /// <summary>An entry that will host a submenu.</summary>
    Submenu = Native.Tray.SDL_TRAYENTRY_SUBMENU,
    /// <summary>Create the entry disabled.</summary>
    Disabled = Native.Tray.SDL_TRAYENTRY_DISABLED,
    /// <summary>Create a checkbox entry checked.</summary>
    Checked = Native.Tray.SDL_TRAYENTRY_CHECKED,
}
```

- [ ] **Step 2: Create `Tray.cs`:**

```csharp
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using static SdlSharp.Native.Common;
using static SdlSharp.Native.Tray;

namespace SdlSharp;

/// <summary>
/// A system tray (notification area / menu bar) icon with an optional menu.
/// Tray APIs must be called on the main thread. Menus and entries are owned by
/// the tray: they become invalid when the tray is disposed or an entry is removed.
/// </summary>
public sealed unsafe class Tray : IDisposable
{
    internal Native.SDL_Tray* Handle
    {
        get
        {
            ObjectDisposedException.ThrowIf(_handle == null, this);
            return _handle;
        }
    }

    private Native.SDL_Tray* _handle;

    internal bool IsDisposed => _handle == null;

    // One GCHandle per entry that has a managed callback, keyed by the native
    // entry pointer. Freed when the entry's callback is replaced/cleared, when
    // the entry is removed, or (for any still registered) at Dispose.
    internal readonly Dictionary<nint, GCHandle> CallbackHandles = new();

    private Tray(Native.SDL_Tray* handle) => _handle = handle;

    /// <summary>
    /// Creates a tray icon.
    /// </summary>
    /// <param name="icon">The icon image, or null for a platform default/blank icon.</param>
    /// <param name="tooltip">Tooltip text, or null.</param>
    public static Tray Create(Graphics.Surface? icon = null, string? tooltip = null) =>
        new(Check(SDL_CreateTray(icon != null ? icon.Handle : null, ToUtf8(tooltip))));

    /// <summary>Replaces the tray icon.</summary>
    /// <param name="icon">The new icon, or null to remove it.</param>
    public void SetIcon(Graphics.Surface? icon) => SDL_SetTrayIcon(Handle, icon != null ? icon.Handle : null);

    /// <summary>Replaces the tooltip text.</summary>
    /// <param name="tooltip">The new tooltip, or null to remove it.</param>
    public void SetTooltip(string? tooltip) => SDL_SetTrayTooltip(Handle, ToUtf8(tooltip));

    /// <summary>Creates (or replaces) the tray's root menu.</summary>
    public TrayMenu CreateMenu() => new(Check(SDL_CreateTrayMenu(Handle)), this);

    /// <summary>Gets the tray's root menu, or null if none was created.</summary>
    public TrayMenu? Menu
    {
        get
        {
            var menu = SDL_GetTrayMenu(Handle);
            return menu == null ? null : new TrayMenu(menu, this);
        }
    }

    /// <summary>
    /// Processes pending tray events. Call this if the app does not run an SDL event
    /// loop; apps that pump events regularly do not need it.
    /// </summary>
    public static void Update() => SDL_UpdateTrays();

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    internal static void EntryCallback(void* userdata, Native.SDL_TrayEntry* entry)
    {
        try
        {
            var holder = (TrayEntry.CallbackHolder)GCHandle.FromIntPtr((nint)userdata).Target!;
            holder.Callback(new TrayEntry(entry, holder.Tray));
        }
        catch
        {
            // Exceptions must not cross the native boundary.
        }
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_handle != null)
        {
            SDL_DestroyTray(_handle);
        }

        _handle = null;

        lock (CallbackHandles)
        {
            foreach (var (_, handle) in CallbackHandles)
            {
                handle.Free();
            }

            CallbackHandles.Clear();
        }
    }
}
```

- [ ] **Step 3: Create `TrayMenu.cs`:**

```csharp
using static SdlSharp.Native.Common;
using static SdlSharp.Native.Tray;

namespace SdlSharp;

/// <summary>
/// A menu attached to a <see cref="Tray"/> or to a submenu entry. This is a view over
/// tray-owned state: it is valid only while the owning tray lives. Wrappers are not
/// equal to each other.
/// </summary>
public sealed unsafe class TrayMenu
{
    private readonly Native.SDL_TrayMenu* _menu;
    private readonly Tray _tray;

    internal TrayMenu(Native.SDL_TrayMenu* menu, Tray tray)
    {
        _menu = menu;
        _tray = tray;
    }

    private Native.SDL_TrayMenu* Menu
    {
        get
        {
            ObjectDisposedException.ThrowIf(_tray.IsDisposed, _tray);
            return _menu;
        }
    }

    /// <summary>
    /// Inserts an entry into the menu.
    /// </summary>
    /// <param name="index">Position to insert at, or -1 to append.</param>
    /// <param name="label">The entry label, or null for a separator.</param>
    /// <param name="flags">Entry kind and initial state (ignored for separators).</param>
    /// <returns>The new entry, or null when a separator was inserted and SDL returned none.</returns>
    public TrayEntry? InsertEntry(int index, string? label, TrayEntryFlags flags = TrayEntryFlags.Button)
    {
        var entry = SDL_InsertTrayEntryAt(Menu, index, ToUtf8(label), (uint)flags);
        if (entry == null && label != null)
        {
            throw new SdlException();
        }

        return entry == null ? null : new TrayEntry(entry, _tray);
    }

    /// <summary>Gets a snapshot of the menu's entries.</summary>
    public TrayEntry[] Entries
    {
        get
        {
            var entries = SDL_GetTrayEntries(Menu, out var count);
            if (entries == null || count <= 0) return [];

            var result = new TrayEntry[count];
            for (var i = 0; i < count; i++)
            {
                result[i] = new TrayEntry(entries[i], _tray);
            }

            return result;
        }
    }

    /// <summary>Gets the entry this menu is a submenu of, or null for a tray root menu.</summary>
    public TrayEntry? ParentEntry
    {
        get
        {
            var entry = SDL_GetTrayMenuParentEntry(Menu);
            return entry == null ? null : new TrayEntry(entry, _tray);
        }
    }

    /// <summary>Gets the tray this menu ultimately belongs to.</summary>
    public Tray ParentTray => _tray;
}
```

- [ ] **Step 4: Create `TrayEntry.cs`:**

```csharp
using System.Runtime.InteropServices;

using static SdlSharp.Native.Common;
using static SdlSharp.Native.Tray;

namespace SdlSharp;

/// <summary>
/// An entry in a tray menu. This is a view over tray-owned state: it is valid only
/// while the owning tray lives and the entry has not been removed. Wrappers are not
/// equal to each other.
/// </summary>
public sealed unsafe class TrayEntry
{
    internal sealed record CallbackHolder(Tray Tray, Action<TrayEntry> Callback);

    private readonly Native.SDL_TrayEntry* _entry;
    private readonly Tray _tray;

    internal TrayEntry(Native.SDL_TrayEntry* entry, Tray tray)
    {
        _entry = entry;
        _tray = tray;
    }

    private Native.SDL_TrayEntry* Entry
    {
        get
        {
            ObjectDisposedException.ThrowIf(_tray.IsDisposed, _tray);
            return _entry;
        }
    }

    /// <summary>Gets or sets the entry label. Null denotes a separator.</summary>
    public string? Label
    {
        get => Marshal.PtrToStringUTF8((nint)SDL_GetTrayEntryLabel(Entry));
        set => SDL_SetTrayEntryLabel(Entry, ToUtf8(value));
    }

    /// <summary>Gets or sets whether a checkbox entry is checked.</summary>
    public bool IsChecked
    {
        get => SDL_GetTrayEntryChecked(Entry);
        set => SDL_SetTrayEntryChecked(Entry, value);
    }

    /// <summary>Gets or sets whether the entry can be selected.</summary>
    public bool IsEnabled
    {
        get => SDL_GetTrayEntryEnabled(Entry);
        set => SDL_SetTrayEntryEnabled(Entry, value);
    }

    /// <summary>Creates a submenu for this entry (the entry must have been created with <see cref="TrayEntryFlags.Submenu"/>).</summary>
    public TrayMenu CreateSubmenu() => new(Check(SDL_CreateTraySubmenu(Entry)), _tray);

    /// <summary>Gets this entry's submenu, or null if it has none.</summary>
    public TrayMenu? Submenu
    {
        get
        {
            var menu = SDL_GetTraySubmenu(Entry);
            return menu == null ? null : new TrayMenu(menu, _tray);
        }
    }

    /// <summary>Gets the menu containing this entry.</summary>
    public TrayMenu Parent => new(Check(SDL_GetTrayEntryParent(Entry)), _tray);

    /// <summary>Simulates a click on this entry (fires its callback).</summary>
    public void Click() => SDL_ClickTrayEntry(Entry);

    /// <summary>
    /// Sets or clears (<c>null</c>) the callback invoked when this entry is selected.
    /// Setting replaces any previous callback.
    /// </summary>
    /// <param name="callback">The callback, or null to clear.</param>
    public void SetCallback(Action<TrayEntry>? callback)
    {
        var key = (nint)Entry;

        lock (_tray.CallbackHandles)
        {
            _tray.CallbackHandles.Remove(key, out var previous);

            if (callback == null)
            {
                SDL_SetTrayEntryCallback(Entry, null, null);
            }
            else
            {
                var handle = GCHandle.Alloc(new CallbackHolder(_tray, callback));
                SDL_SetTrayEntryCallback(Entry, &Tray.EntryCallback, (void*)GCHandle.ToIntPtr(handle));
                _tray.CallbackHandles[key] = handle;
            }

            if (previous.IsAllocated)
            {
                previous.Free();
            }
        }
    }

    /// <summary>
    /// Removes this entry (and any submenu under it) from its menu. The wrapper is
    /// invalid afterward. Callback handles of a removed submenu's descendants are
    /// released when the tray is disposed.
    /// </summary>
    public void Remove()
    {
        var key = (nint)Entry;

        lock (_tray.CallbackHandles)
        {
            if (_tray.CallbackHandles.Remove(key, out var handle))
            {
                handle.Free();
            }

            SDL_RemoveTrayEntry(_entry);
        }
    }
}
```

- [ ] **Step 5: Build** — 0 Warnings, 0 Errors. Confirm `Graphics.Surface.Handle` is `internal` and accessible (it is, same assembly).

- [ ] **Step 6: Commit**

```bash
git add src/SdlSharp/TrayEntryFlags.cs src/SdlSharp/Tray.cs src/SdlSharp/TrayMenu.cs src/SdlSharp/TrayEntry.cs
git commit -m "Add managed Tray, TrayMenu, and TrayEntry with per-tray callback registry"
```

---

## Task 6: Small items A — Version/Sdl, HintPriority, SdlSystem, SystemInfo

**Files:**
- Create: `src/SdlSharp/Native/Version.cs`
- Create: `src/SdlSharp/Sdl.cs`
- Create: `src/SdlSharp/HintPriority.cs`
- Modify: `src/SdlSharp/SdlHints.cs`
- Create: `src/SdlSharp/Native/SdlSystem.cs`
- Create: `src/SdlSharp/SandboxEnvironment.cs`
- Modify: `src/SdlSharp/SystemInfo.cs`

- [ ] **Step 1: Create `Native/Version.cs`:**

```csharp
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace SdlSharp.Native;

/// <summary>
/// Native bindings for SDL_version.h.
/// </summary>
[SuppressMessage("Interoperability", "CA1401:P/Invokes should not be visible")]
public static partial class Version
{
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetVersion")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_GetVersion();

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetRevision")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* SDL_GetRevision();
}
```

(SDL_version.h's remaining content is compile-time macros — `SDL_MAJOR_VERSION`, `SDL_VERSIONNUM*`, `SDL_VERSION_ATLEAST` — which describe the headers, not the loaded library; the managed decode below covers the runtime need. Add a one-line comment noting this.)

- [ ] **Step 2: Create `Sdl.cs`:**

```csharp
using System.Runtime.InteropServices;

using static SdlSharp.Native.Version;

namespace SdlSharp;

/// <summary>
/// Information about the linked SDL library.
/// </summary>
public static unsafe class Sdl
{
    /// <summary>
    /// Gets the version of the SDL library in use (decoded from SDL's
    /// major*1000000 + minor*1000 + micro encoding).
    /// </summary>
    public static System.Version Version
    {
        get
        {
            var v = SDL_GetVersion();
            return new System.Version(v / 1000000, v / 1000 % 1000, v % 1000);
        }
    }

    /// <summary>Gets the source revision of the SDL library in use, or null if unavailable.</summary>
    public static string? Revision => Marshal.PtrToStringUTF8((nint)SDL_GetRevision());
}
```

Do NOT add `using SdlSharp.Native;` to this file — `SdlSharp.Native.Version` vs `System.Version` would become ambiguous. `using static` imports members only.

- [ ] **Step 3: Create `HintPriority.cs`** — first read the native `SDL_HintPriority` enum in `src/SdlSharp/Native/Hints.cs` to get exact member names, then:

```csharp
namespace SdlSharp;

/// <summary>
/// Priority levels for hint values (higher priorities override lower ones).
/// </summary>
public enum HintPriority
{
    /// <summary>Low priority; used for default values.</summary>
    Default = (int)Native.SDL_HintPriority.SDL_HINT_DEFAULT,
    /// <summary>Normal priority.</summary>
    Normal = (int)Native.SDL_HintPriority.SDL_HINT_NORMAL,
    /// <summary>High priority; overrides normal-priority values.</summary>
    Override = (int)Native.SDL_HintPriority.SDL_HINT_OVERRIDE,
}
```

(If the native enum is nested in the `Hints` class or named differently, adjust the references to the actual declaration — read the file first.)

- [ ] **Step 4: Add the priority overload to `SdlHints.cs`:**

```csharp
    /// <summary>
    /// Sets a hint with a specific priority. Higher-priority values override lower ones.
    /// </summary>
    /// <param name="name">The hint name.</param>
    /// <param name="value">The value to set.</param>
    /// <param name="priority">The priority to set it with.</param>
    /// <returns>True if the hint was set.</returns>
    public static bool Set(string name, string value, HintPriority priority) =>
        SDL_SetHintWithPriority(ToUtf8(name), ToUtf8(value), (Native.SDL_HintPriority)priority);
```

(Match the actual native signature — adjust the enum cast to whatever type `SDL_SetHintWithPriority` takes.)

- [ ] **Step 5: Create `Native/SdlSystem.cs`** (class named `SdlSystem`, NOT `System`, to avoid colliding with the BCL namespace — note this in a comment):

```csharp
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace SdlSharp.Native;

/// <summary>
/// Sandbox environments detectable via SDL_GetSandbox.
/// </summary>
public enum SDL_Sandbox
{
    SDL_SANDBOX_NONE = 0,
    SDL_SANDBOX_UNKNOWN_CONTAINER,
    SDL_SANDBOX_FLATPAK,
    SDL_SANDBOX_SNAP,
    SDL_SANDBOX_MACOS,
}

/// <summary>
/// Native bindings for SDL_system.h. Named SdlSystem (not System) to avoid
/// colliding with the .NET System namespace.
/// </summary>
[SuppressMessage("Interoperability", "CA1401:P/Invokes should not be visible")]
public static partial class SdlSystem
{
    // Skipped: the rest of SDL_system.h is platform-specific interop —
    // Android (JNI activity/environment, external storage, permissions, toasts),
    // iOS (animation callbacks, event pump control, HomeKit/CoreBluetooth handles),
    // Windows (message hooks, D3D adapter queries), X11 (event hooks), GDK, and
    // Linux thread priority helpers. These target platform toolchains SdlSharp
    // does not abstract; call them via your own platform bindings if needed.

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_IsTablet")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_IsTablet();

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_IsTV")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_IsTV();

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetSandbox")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Sandbox SDL_GetSandbox();
}
```

Verify the `SDL_Sandbox` enum values against `/Users/paulv/Projects/SDL/include/SDL3/SDL_system.h` (confirmed at plan time: NONE=0, UNKNOWN_CONTAINER, FLATPAK, SNAP, MACOS).

- [ ] **Step 6: Create `SandboxEnvironment.cs`:**

```csharp
namespace SdlSharp;

/// <summary>
/// The sandbox environment the process runs in, if any.
/// </summary>
public enum SandboxEnvironment
{
    /// <summary>Not sandboxed.</summary>
    None = Native.SDL_Sandbox.SDL_SANDBOX_NONE,
    /// <summary>Sandboxed in an unrecognized container.</summary>
    UnknownContainer = Native.SDL_Sandbox.SDL_SANDBOX_UNKNOWN_CONTAINER,
    /// <summary>Running inside Flatpak.</summary>
    Flatpak = Native.SDL_Sandbox.SDL_SANDBOX_FLATPAK,
    /// <summary>Running inside Snap.</summary>
    Snap = Native.SDL_Sandbox.SDL_SANDBOX_SNAP,
    /// <summary>Running inside the macOS App Sandbox.</summary>
    MacOS = Native.SDL_Sandbox.SDL_SANDBOX_MACOS,
}
```

- [ ] **Step 7: Add to `SystemInfo.cs`** (plus `using static SdlSharp.Native.SdlSystem;`):

```csharp
    /// <summary>
    /// Creates a directory (and any missing parents). Succeeds if it already exists.
    /// </summary>
    /// <param name="path">The directory path.</param>
    public static void CreateDirectory(string path) => Check(SDL_CreateDirectory(ToUtf8(path)));

    /// <summary>Gets whether the device is a tablet.</summary>
    public static bool IsTablet => SDL_IsTablet();

    /// <summary>Gets whether the device is a TV.</summary>
    public static bool IsTV => SDL_IsTV();

    /// <summary>Gets the sandbox environment the process runs in, if any.</summary>
    public static SandboxEnvironment Sandbox => (SandboxEnvironment)SDL_GetSandbox();
```

- [ ] **Step 8: Build** — 0 Warnings, 0 Errors.

- [ ] **Step 9: Commit**

```bash
git add src/SdlSharp/Native/Version.cs src/SdlSharp/Sdl.cs src/SdlSharp/HintPriority.cs src/SdlSharp/SdlHints.cs src/SdlSharp/Native/SdlSystem.cs src/SdlSharp/SandboxEnvironment.cs src/SdlSharp/SystemInfo.cs
git commit -m "Add Sdl version info, hint priority, system queries, CreateDirectory"
```

---

## Task 7: Small items B — full MessageBox, property pointers, camera, sensor, touch constants

**Files:**
- Modify: `src/SdlSharp/Native/MessageBox.cs`
- Modify: `src/SdlSharp/Graphics/MessageBox.cs`
- Modify: `src/SdlSharp/Native/Properties.cs`
- Modify: `src/SdlSharp/PropertyGroup.cs`
- Create: `src/SdlSharp/Graphics/CameraPermissionState.cs`
- Modify: `src/SdlSharp/Graphics/Camera.cs`
- Modify: `src/SdlSharp/Input/Sensor.cs`
- Modify: `src/SdlSharp/Input/TouchDevice.cs`

- [ ] **Step 1: Native MessageBox additions** (`Native/MessageBox.cs`). Structs verified against the header (`colors[SDL_MESSAGEBOX_COLOR_COUNT]` = 5 entries, expanded to named fields with identical layout):

```csharp
/// <summary>Data for a single message box button.</summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct SDL_MessageBoxButtonData
{
    public uint flags;
    public int buttonID;
    public byte* text;
}

/// <summary>A color used in a message box color scheme.</summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_MessageBoxColor
{
    public byte r, g, b;
}

/// <summary>A full message box color scheme (five colors, in SDL_MessageBoxColorType order).</summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_MessageBoxColorScheme
{
    public SDL_MessageBoxColor background;
    public SDL_MessageBoxColor text;
    public SDL_MessageBoxColor button_border;
    public SDL_MessageBoxColor button_background;
    public SDL_MessageBoxColor button_selected;
}

/// <summary>Full message box description.</summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct SDL_MessageBoxData
{
    public uint flags;
    public SDL_Window* window;
    public byte* title;
    public byte* message;
    public int numbuttons;
    public SDL_MessageBoxButtonData* buttons;
    public SDL_MessageBoxColorScheme* colorScheme;
}
```

And inside the partial class:

```csharp
    public const uint SDL_MESSAGEBOX_BUTTON_RETURNKEY_DEFAULT = 0x00000001u;
    public const uint SDL_MESSAGEBOX_BUTTON_ESCAPEKEY_DEFAULT = 0x00000002u;

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_ShowMessageBox")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_ShowMessageBox(SDL_MessageBoxData* messageboxdata, int* buttonid);
```

(Place these near the existing `SDL_ShowSimpleMessageBox`; read the file first for the existing `SDL_MessageBoxFlags` type — cast from it or use `uint` consistently with what's there.)

- [ ] **Step 2: Managed MessageBox additions** (`Graphics/MessageBox.cs`). Add records + the full `Show` overload:

```csharp
/// <summary>
/// A button in a multi-button message box.
/// </summary>
/// <param name="Id">The value returned when this button is pressed.</param>
/// <param name="Text">The button label.</param>
/// <param name="IsReturnDefault">True if Return/Enter activates this button.</param>
/// <param name="IsEscapeDefault">True if Escape activates this button.</param>
public readonly record struct MessageBoxButton(
    int Id,
    string Text,
    bool IsReturnDefault = false,
    bool IsEscapeDefault = false);

/// <summary>
/// Optional colors for a message box (honored only by some platforms/backends).
/// </summary>
/// <param name="Background">Dialog background color.</param>
/// <param name="Text">Text color.</param>
/// <param name="ButtonBorder">Button border color.</param>
/// <param name="ButtonBackground">Button background color.</param>
/// <param name="ButtonSelected">Selected-button color.</param>
public readonly record struct MessageBoxColorScheme(
    Color Background,
    Color Text,
    Color ButtonBorder,
    Color ButtonBackground,
    Color ButtonSelected);
```

(Namespace-level records in the same file, `SdlSharp.Graphics`.) And on the `MessageBox` class:

```csharp
    /// <summary>
    /// Shows a modal message box with custom buttons and returns the pressed button's ID.
    /// This blocks the calling thread and opens a native dialog — it cannot run headless.
    /// </summary>
    /// <param name="type">The type of message box (error, warning, information).</param>
    /// <param name="title">The title text.</param>
    /// <param name="message">The message text.</param>
    /// <param name="buttons">The buttons, in layout order.</param>
    /// <param name="parent">Optional parent window.</param>
    /// <param name="colorScheme">Optional color scheme (honored only by some backends).</param>
    /// <returns>The <see cref="MessageBoxButton.Id"/> of the pressed button, or -1 if the dialog was closed without a button.</returns>
    public static int Show(
        MessageBoxType type,
        string title,
        string message,
        MessageBoxButton[] buttons,
        Window? parent = null,
        MessageBoxColorScheme? colorScheme = null)
    {
        ArgumentNullException.ThrowIfNull(buttons);

        var textPins = new GCHandle[buttons.Length];
        var nativeButtons = new Native.SDL_MessageBoxButtonData[buttons.Length];
        try
        {
            for (var i = 0; i < buttons.Length; i++)
            {
                var utf8 = ToUtf8(buttons[i].Text)!;
                textPins[i] = GCHandle.Alloc(utf8, GCHandleType.Pinned);
                nativeButtons[i] = new Native.SDL_MessageBoxButtonData
                {
                    flags = (buttons[i].IsReturnDefault ? SDL_MESSAGEBOX_BUTTON_RETURNKEY_DEFAULT : 0u)
                          | (buttons[i].IsEscapeDefault ? SDL_MESSAGEBOX_BUTTON_ESCAPEKEY_DEFAULT : 0u),
                    buttonID = buttons[i].Id,
                    text = (byte*)textPins[i].AddrOfPinnedObject(),
                };
            }

            Native.SDL_MessageBoxColorScheme nativeScheme;
            Native.SDL_MessageBoxColorScheme* schemePtr = null;
            if (colorScheme is { } scheme)
            {
                nativeScheme = new Native.SDL_MessageBoxColorScheme
                {
                    background = ToNativeColor(scheme.Background),
                    text = ToNativeColor(scheme.Text),
                    button_border = ToNativeColor(scheme.ButtonBorder),
                    button_background = ToNativeColor(scheme.ButtonBackground),
                    button_selected = ToNativeColor(scheme.ButtonSelected),
                };
                schemePtr = &nativeScheme;
            }

            var titleUtf8 = ToUtf8(title)!;
            var messageUtf8 = ToUtf8(message)!;
            int buttonId;
            fixed (byte* titlePtr = titleUtf8)
            fixed (byte* messagePtr = messageUtf8)
            fixed (Native.SDL_MessageBoxButtonData* buttonsPtr = nativeButtons)
            {
                var data = new Native.SDL_MessageBoxData
                {
                    flags = (uint)(Native.SDL_MessageBoxFlags)type,
                    window = parent != null ? parent.Handle : null,
                    title = titlePtr,
                    message = messagePtr,
                    numbuttons = buttons.Length,
                    buttons = buttonsPtr,
                    colorScheme = schemePtr,
                };

                Check(SDL_ShowMessageBox(&data, &buttonId));
            }

            return buttonId;
        }
        finally
        {
            foreach (var pin in textPins)
            {
                if (pin.IsAllocated) pin.Free();
            }
        }
    }

    private static Native.SDL_MessageBoxColor ToNativeColor(Color color) =>
        new() { r = color.R, g = color.G, b = color.B };
```

Add the needed usings (`System.Runtime.InteropServices` for GCHandle). If the `flags` cast to `(uint)(Native.SDL_MessageBoxFlags)type` mismatches the actual native struct/enum types, align with whatever `SDL_ShowSimpleMessageBox` uses in the file.

- [ ] **Step 3: Property pointers.** In `Native/Properties.cs`: replace the skip comment at lines ~81-82 with one noting only the WithCleanup variant is skipped, and bind:

```csharp
    // Skipped: SDL_SetPointerPropertyWithCleanup — its cleanup callback ties native
    // lifetime management to managed state; use SetPointerProperty and manage
    // lifetime on the managed side instead.

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetPointerProperty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetPointerProperty(SDL_PropertiesID props, ReadOnlySpan<byte> name, void* value);
```

In `PropertyGroup.cs` (next to the other Set/Get pairs):

```csharp
    /// <summary>
    /// Sets a pointer-valued property. The caller owns whatever the pointer references.
    /// </summary>
    /// <param name="name">The property name.</param>
    /// <param name="value">The pointer value, or 0 to clear.</param>
    public void SetPointer(string name, nint value) =>
        Check(SDL_SetPointerProperty(Id, ToUtf8(name), (void*)value));

    /// <summary>
    /// Gets a pointer-valued property.
    /// </summary>
    /// <param name="name">The property name.</param>
    /// <param name="defaultValue">Value returned when the property is unset.</param>
    public nint GetPointer(string name, nint defaultValue = 0) =>
        (nint)SDL_GetPointerProperty(Id, ToUtf8(name), (void*)defaultValue);
```

- [ ] **Step 4: Camera.** Create `Graphics/CameraPermissionState.cs`:

```csharp
namespace SdlSharp.Graphics;

/// <summary>
/// The user's response to a camera access request.
/// </summary>
public enum CameraPermissionState
{
    /// <summary>The user denied camera access.</summary>
    Denied = -1,
    /// <summary>The user has not yet responded.</summary>
    Waiting = 0,
    /// <summary>The user approved camera access.</summary>
    Approved = 1,
}
```

In `Graphics/Camera.cs`: change `public int PermissionState => SDL_GetCameraPermissionState(Handle);` to:

```csharp
    /// <summary>
    /// Gets whether the user has approved camera access. Poll while
    /// <see cref="CameraPermissionState.Waiting"/>; frames arrive only after approval.
    /// </summary>
    public CameraPermissionState PermissionState => (CameraPermissionState)SDL_GetCameraPermissionState(Handle);
```

Also add `Camera.Id` (the `SDL_GetCameraID` binding exists but is orphaned — check its exact return type in `Native/Camera.cs` and unwrap accordingly):

```csharp
    /// <summary>Gets the instance ID of this camera.</summary>
    public uint Id => SDL_GetCameraID(Handle).Value;
```

- [ ] **Step 5: Sensor.** In `Input/Sensor.cs` (check whether `SDL_GetSensorID` is bound in `Native/Sensor.cs`; bind it following the house pattern if missing — it takes `SDL_Sensor*` and returns a uint-shaped ID):

```csharp
    /// <summary>A constant for gravity in m/s², for use with accelerometer data.</summary>
    public const float StandardGravity = 9.80665f;

    /// <summary>Gets the instance ID of this sensor.</summary>
    public uint Id => SDL_GetSensorID(Handle);

    /// <summary>
    /// Gets an opened sensor by instance ID. The returned wrapper does not own the
    /// sensor; each access returns a new wrapper and wrappers are not equal to each other.
    /// </summary>
    /// <param name="id">The sensor instance ID.</param>
    public static Sensor FromId(uint id) =>
        new(Check(SDL_GetSensorFromID(id)), ownsHandle: false);
```

(Adjust the `SDL_GetSensorID`/`SDL_GetSensorFromID` id-type wrapping to the actual native signatures — read `Native/Sensor.cs` first.)

- [ ] **Step 6: Touch constants.** In `Input/TouchDevice.cs`:

```csharp
    /// <summary>
    /// The mouse instance ID carried by mouse events that SDL synthesized from touch input.
    /// </summary>
    public const uint MouseId = uint.MaxValue;

    /// <summary>
    /// The touch device ID carried by touch events that SDL synthesized from mouse input.
    /// </summary>
    public const ulong MouseTouchDeviceId = ulong.MaxValue;
```

(Header: `#define SDL_TOUCH_MOUSEID ((SDL_MouseID)-1)` and `#define SDL_MOUSE_TOUCHID ((SDL_TouchID)-1)` — verify in `/Users/paulv/Projects/SDL/include/SDL3/SDL_touch.h`; both are all-ones values.)

- [ ] **Step 7: Build** — 0 Warnings, 0 Errors.

- [ ] **Step 8: Commit**

```bash
git add src/SdlSharp/Native/MessageBox.cs src/SdlSharp/Graphics/MessageBox.cs src/SdlSharp/Native/Properties.cs src/SdlSharp/PropertyGroup.cs src/SdlSharp/Graphics/CameraPermissionState.cs src/SdlSharp/Graphics/Camera.cs src/SdlSharp/Input/Sensor.cs src/SdlSharp/Input/TouchDevice.cs src/SdlSharp/Native/Sensor.cs
git commit -m "Add full message box, property pointers, camera/sensor/touch polish"
```

(Drop `Native/Sensor.cs` from the add list if it needed no change.)

---

## Task 8: SdlLog emit + MiscOps sample (with the ABI gate)

**Files:**
- Modify: `src/SdlSharp/Native/Log.cs`
- Modify: `src/SdlSharp/SdlLog.cs`
- Create: `Samples/MiscOps/MiscOps.csproj`
- Create: `Samples/MiscOps/Program.cs`
- Modify: `SdlSharp.slnx`

- [ ] **Step 1: Bind `SDL_LogMessage` with a fixed shape** (`Native/Log.cs`):

```csharp
    // SDL_LogMessage is C-variadic. We bind a fixed shape and only ever call it with
    // the format "%s" and a single string argument. NOTE: C varargs calling
    // conventions differ from fixed-arg conventions on some ABIs (notably Apple
    // arm64, where variadic arguments are passed on the stack). The MiscOps sample
    // verifies this call end-to-end; if it ever fails on a new platform, remove the
    // emit surface per the phase 11 spec contingency.
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_LogMessage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_LogMessage(int category, SDL_LogPriority priority, ReadOnlySpan<byte> fmt, byte* arg1);
```

(Check the actual native `SDL_LogPriority` type name in the file and match it.)

- [ ] **Step 2: Add emit methods to `SdlLog.cs`:**

```csharp
    /// <summary>
    /// Emits a log message through SDL's logging system.
    /// </summary>
    /// <param name="category">The log category.</param>
    /// <param name="priority">The message priority.</param>
    /// <param name="message">The message text.</param>
    public static void Message(LogCategory category, LogPriority priority, string message)
    {
        var utf8 = ToUtf8(message)!;
        fixed (byte* ptr = utf8)
        {
            SDL_LogMessage((int)category, (Native.SDL_LogPriority)priority, "%s"u8, ptr);
        }
    }

    /// <summary>Emits a trace-priority log message.</summary>
    /// <param name="category">The log category.</param>
    /// <param name="message">The message text.</param>
    public static void Trace(LogCategory category, string message) => Message(category, LogPriority.Trace, message);

    /// <summary>Emits a verbose-priority log message.</summary>
    /// <param name="category">The log category.</param>
    /// <param name="message">The message text.</param>
    public static void Verbose(LogCategory category, string message) => Message(category, LogPriority.Verbose, message);

    /// <summary>Emits a debug-priority log message.</summary>
    /// <param name="category">The log category.</param>
    /// <param name="message">The message text.</param>
    public static void Debug(LogCategory category, string message) => Message(category, LogPriority.Debug, message);

    /// <summary>Emits an info-priority log message.</summary>
    /// <param name="category">The log category.</param>
    /// <param name="message">The message text.</param>
    public static void Info(LogCategory category, string message) => Message(category, LogPriority.Info, message);

    /// <summary>Emits a warn-priority log message.</summary>
    /// <param name="category">The log category.</param>
    /// <param name="message">The message text.</param>
    public static void Warn(LogCategory category, string message) => Message(category, LogPriority.Warn, message);

    /// <summary>Emits an error-priority log message.</summary>
    /// <param name="category">The log category.</param>
    /// <param name="message">The message text.</param>
    public static void Error(LogCategory category, string message) => Message(category, LogPriority.Error, message);

    /// <summary>Emits a critical-priority log message.</summary>
    /// <param name="category">The log category.</param>
    /// <param name="message">The message text.</param>
    public static void Critical(LogCategory category, string message) => Message(category, LogPriority.Critical, message);
```

(Verify `LogPriority` member names — Trace/Verbose/Debug/Info/Warn/Error/Critical — against `src/SdlSharp/LogPriority.cs` and adjust; `ToUtf8` needs `using static SdlSharp.Native.Common;` if not present.)

- [ ] **Step 3: Create the MiscOps sample.** `Samples/MiscOps/MiscOps.csproj` — same template as AudioOps. `Samples/MiscOps/Program.cs`:

```csharp
using SdlSharp;
using SdlSharp.Audio;
using SdlSharp.Input;

using var app = new Application(InitFlags.Events);

var failures = 0;

void Check(string name, bool ok)
{
    Console.WriteLine($"{(ok ? "PASS" : "FAIL")}  {name}");
    if (!ok) failures++;
}

// Version / revision.
Check("version >= 3.4", Sdl.Version >= new Version(3, 4, 0));
Check("revision non-null", Sdl.Revision is not null);

// Hint with priority.
Check("hint set w/ priority", SdlHints.Set("SDL_TEST_PHASE11_HINT", "on", HintPriority.Override));
Check("hint round-trip", SdlHints.Get("SDL_TEST_PHASE11_HINT") == "on");

// Log emit round-trip — THE ABI GATE. Captures SDL's log output and asserts the
// emitted string arrives intact through the C varargs call.
string? captured = null;
SdlLog.SetOutputFunction((category, priority, message) => captured = message);
SdlLog.Info(LogCategory.Application, "phase11-abi-check-éü");
SdlLog.SetOutputFunction(null);
Check("log emit round-trip", captured == "phase11-abi-check-éü");

// Pointer property round-trip.
using (var props = PropertyGroup.Create())
{
    props.SetPointer("ptr", 0x12345678);
    Check("pointer property round-trip", props.GetPointer("ptr") == 0x12345678);
    Check("pointer property default", props.GetPointer("missing", 42) == 42);
}

// Touch constants.
Check("touch mouse id", TouchDevice.MouseId == uint.MaxValue);
Check("mouse touch id", TouchDevice.MouseTouchDeviceId == ulong.MaxValue);

// Sensor statics.
Check("standard gravity", Math.Abs(Sensor.StandardGravity - 9.80665f) < 0.0001f);

// System queries (desktop expectations).
Check("not a tablet", !SystemInfo.IsTablet);
Check("not a TV", !SystemInfo.IsTV);
Check("sandbox enum valid", Enum.IsDefined(SystemInfo.Sandbox));

// CreateDirectory.
var dir = Path.Combine(Path.GetTempPath(), $"sdlsharp-phase11-{Environment.ProcessId}");
SystemInfo.CreateDirectory(dir);
Check("create directory", Directory.Exists(dir));
Directory.Delete(dir);

// Haptic (headless: typically no devices; must not crash).
Application.InitSubSystem(InitFlags.Haptic);
var haptics = Haptic.GetDevices();
Check("haptic enumeration no-crash", haptics is not null);
Check("mouse not haptic (headless)", Haptic.IsMouseHaptic == false || true); // property must not throw; value is platform-dependent

Console.WriteLine(failures == 0 ? "ALL PASS" : $"{failures} FAILURES");
return failures == 0 ? 0 : 1;
```

Adjust to actual API shapes discovered while writing (e.g. `PropertyGroup.Create()` — check the real factory name in `PropertyGroup.cs`; `InitFlags.Haptic` member name; `SdlHints.Get` semantics). The LAST check intentionally only proves `IsMouseHaptic` doesn't throw. Register the project in `SdlSharp.slnx` after EventQueueOps.

- [ ] **Step 4: Build and run.**

Run: `dotnet build SdlSharp.slnx` → 0/0.
Run: `dotnet run --project Samples/MiscOps/MiscOps.csproj` → every line PASS, `ALL PASS`, exit 0.

**CONTINGENCY — if (and only if) the "log emit round-trip" check FAILS:** the C-varargs ABI is broken on this platform. Per the spec: (a) delete the `SDL_LogMessage` binding and replace it with a skip comment in `Native/Log.cs`: `// Skipped SDL_LogMessage / SDL_Log / SDL_Log<Priority> emit functions: C-variadic, not portably callable from .NET (on Apple arm64 variadic args use the stack, fixed-signature P/Invoke uses registers). Use .NET logging and bridge SDL's own output via SDL_SetLogOutputFunction.`; (b) delete the emit methods from `SdlLog.cs`; (c) replace the sample's emit check with `Check("log emit documented-skip (varargs ABI)", true);` and a comment; (d) rebuild and rerun. Report which branch was taken.

- [ ] **Step 5: Commit**

```bash
git add src/SdlSharp/Native/Log.cs src/SdlSharp/SdlLog.cs Samples/MiscOps/ SdlSharp.slnx
git commit -m "Add SdlLog emit methods and MiscOps sample with varargs ABI gate"
```

(Adjust the message to "Document SdlLog emit skip..." if the contingency fired.)

---

## Task 9: TrayDemo sample (visible ~3s smoke)

**Files:**
- Create: `Samples/TrayDemo/TrayDemo.csproj`
- Create: `Samples/TrayDemo/Program.cs`
- Modify: `SdlSharp.slnx`

- [ ] **Step 1: csproj** — same template as MiscOps/AudioOps.

- [ ] **Step 2: `Program.cs`:**

```csharp
using SdlSharp;
using SdlSharp.Graphics;

// A real tray icon appears in the menu bar for ~3 seconds during this run.
using var app = new Application(InitFlags.Video);

var failures = 0;

void Check(string name, bool ok)
{
    Console.WriteLine($"{(ok ? "PASS" : "FAIL")}  {name}");
    if (!ok) failures++;
}

// Small solid icon.
using var icon = Surface.Create(16, 16, PixelFormat.Rgba8888);
icon.Fill(new Color(200, 60, 60, 255));

using (var tray = Tray.Create(icon, "SdlSharp TrayDemo"))
{
    var menu = tray.CreateMenu();
    Check("root menu created", tray.Menu is not null);

    var clicked = false;
    var button = menu.InsertEntry(-1, "Do Thing", TrayEntryFlags.Button)!;
    button.SetCallback(_ => clicked = true);

    var checkbox = menu.InsertEntry(-1, "Enabled Option", TrayEntryFlags.Checkbox | TrayEntryFlags.Checked)!;
    menu.InsertEntry(-1, null); // separator

    var parent = menu.InsertEntry(-1, "More", TrayEntryFlags.Submenu)!;
    var submenu = parent.CreateSubmenu();
    var subItem = submenu.InsertEntry(-1, "Nested", TrayEntryFlags.Button)!;

    Check("entries snapshot", menu.Entries.Length == 4);
    Check("label get", button.Label == "Do Thing");
    button.Label = "Do Thing!";
    Check("label set round-trip", button.Label == "Do Thing!");
    Check("checkbox starts checked", checkbox.IsChecked);
    checkbox.IsChecked = false;
    Check("checkbox toggle", !checkbox.IsChecked);
    checkbox.IsEnabled = false;
    Check("enabled toggle", !checkbox.IsEnabled);
    Check("submenu reachable", parent.Submenu is not null);
    Check("submenu parent entry", submenu.ParentEntry is not null);
    Check("nested label", subItem.Label == "Nested");

    button.Click();
    Application.PumpEvents();
    Tray.Update();
    Check("click fired callback", clicked);

    subItem.Remove();
    Check("entry removed", submenu.Entries.Length == 0);

    // Keep the icon visible briefly so the smoke is observable.
    for (var i = 0; i < 30; i++)
    {
        Application.DispatchEvents();
        Tray.Update();
        Thread.Sleep(100);
    }
}

Console.WriteLine(failures == 0 ? "ALL PASS" : $"{failures} FAILURES");
return failures == 0 ? 0 : 1;
```

Adjust `Surface.Create`/`Fill`/`PixelFormat`/`Color` calls to the actual API shapes in `src/SdlSharp/Graphics/Surface.cs` (read it first — phase 9 gave Surface a rich API; use whatever create+fill members exist). If `SDL_ClickTrayEntry`'s callback fires synchronously the Pump/Update before the check is harmless; if it needs a pump, it's already there. If the click callback genuinely cannot be observed programmatically on macOS, report DONE_WITH_CONCERNS with the observed behavior rather than deleting the check silently.

- [ ] **Step 3: Register in `SdlSharp.slnx`** after MiscOps.

- [ ] **Step 4: Build and run.** Build 0/0, then `dotnet run --project Samples/TrayDemo/TrayDemo.csproj` → all PASS, exit 0 (a tray icon appears for ~3 s — expected).

- [ ] **Step 5: Commit**

```bash
git add Samples/TrayDemo/ SdlSharp.slnx
git commit -m "Add TrayDemo smoke sample exercising the tray surface"
```

---

## Task 10: Deferred.cs + full initiative close-out

**Files:**
- Create: `src/SdlSharp/Native/Deferred.cs`
- Modify: `INVENTORY.md`
- Modify: `TODO.md`

- [ ] **Step 1: Derive the fully-deferred header list.** List headers with no native binding file:

```bash
ls /Users/paulv/Projects/SDL/include/SDL3/ | sed 's/SDL_//;s/\.h//' > /tmp/all_headers.txt
# Compare against src/SdlSharp/Native/*.cs class-per-header coverage and INVENTORY.md's deferred sections.
```

Cross-check with INVENTORY.md's deferred-by-design sections (the July audit verified 13). Expected set (verify, don't assume): hidapi, metal, vulkan, egl, opengl/opengl_glext/opengles2 family, main + main_impl, stdinc, atomic, thread, mutex, loadso, bits/endian/intrin, platform/platform_defines, test\* headers, plus any others INVENTORY marks fully deferred. Note: SDL_system.h and SDL_version.h now HAVE native files (Task 6) and must NOT appear in Deferred.cs.

- [ ] **Step 2: Create `src/SdlSharp/Native/Deferred.cs`** — comments only, one block per header, following this shape (write one block per header found in Step 1, with accurate rationales):

```csharp
// This file intentionally contains no code. It documents, per the CLAUDE.md
// skip-comment convention, every SDL3 public header that SdlSharp deliberately
// does not bind at all — so the rationale lives in the native layer even though
// no binding file exists for these headers.
//
// SDL_hidapi.h — raw HID device access. Large, low-level surface serving niche
// hardware; the .NET ecosystem covers this (e.g. HidSharp).
// SDL_metal.h / SDL_vulkan.h / SDL_egl.h / SDL_opengl*.h — platform graphics-API
// interop headers (view creation, instance extensions, GL typedefs). Interop
// belongs to the graphics API's own bindings; GL function loading is available
// via Gl.GetProcAddress.
// SDL_main.h (+ SDL_main_impl.h) — C entry-point plumbing; managed apps own Main().
// SDL_stdinc.h — C standard-library shims; the BCL covers all of it (the few
// needed pieces, e.g. SDL_free, are bound in Common).
// SDL_atomic.h / SDL_thread.h / SDL_mutex.h — threading primitives; use
// System.Threading.
// SDL_loadso.h — shared-object loading; use System.Runtime.InteropServices.NativeLibrary.
// SDL_bits.h / SDL_endian.h / SDL_intrin.h — compiler intrinsics and bit tricks;
// use System.Numerics.BitOperations / BinaryPrimitives.
// SDL_platform.h / SDL_platform_defines.h — compile-time platform macros with no
// runtime exports (runtime queries live in SDL_platform's SDL_GetPlatform, which
// is bound elsewhere if present — verify and adjust this line to the facts).
// SDL_test*.h — SDL's own test harness; not part of the runtime API.
```

Verify every claim in the comments (e.g. whether `SDL_GetPlatform` is bound and where) before writing them — the reviewer will.

- [ ] **Step 3: Set-diff accounting** for every header touched this phase:

```bash
for h in haptic tray version system messagebox properties camera sensor touch filesystem log hints; do
  awk '/^extern SDL_DECLSPEC/{s=$0; while (s !~ /;/) {getline l; s=s" "l} print s}' /Users/paulv/Projects/SDL/include/SDL3/SDL_$h.h | grep -o 'SDLCALL SDL_[A-Za-z0-9_]*' | sed 's/SDLCALL //' | sort -u > /tmp/p11_$h.txt
  echo "== SDL_$h.h unbound:"
  cat src/SdlSharp/Native/*.cs | grep -o 'EntryPoint = "[^"]*"' | sed 's/EntryPoint = "//;s/"//' | sort -u | comm -23 /tmp/p11_$h.txt -
done
```

Required: SDL_haptic.h → empty (31/31). SDL_tray.h → empty (23/23). SDL_version.h → empty (2/2). Every other header's remainder must consist solely of functions with skip comments in its native file (spot-check each name against the file). Any bare unbound name = a miss; fix it before proceeding.

- [ ] **Step 4: Orphan sweep** over every native bound this phase (the 19 haptic + 23 tray + SDL_ShowMessageBox + SDL_SetPointerProperty + SDL_GetVersion + SDL_GetRevision + SDL_IsTablet + SDL_IsTV + SDL_GetSandbox + SDL_LogMessage-if-kept + the un-orphaned SDL_SetHintWithPriority / SDL_CreateDirectory / SDL_GetCameraPermissionState / SDL_GetCameraID / SDL_GetSensorFromID / SDL_GetSensorID):

```bash
for fn in SDL_CreateHapticEffect SDL_DestroyHapticEffect SDL_UpdateHapticEffect SDL_RunHapticEffect SDL_StopHapticEffect SDL_StopHapticEffects SDL_GetHapticEffectStatus SDL_HapticEffectSupported SDL_GetHapticFeatures SDL_GetNumHapticAxes SDL_GetMaxHapticEffects SDL_GetMaxHapticEffectsPlaying SDL_SetHapticGain SDL_SetHapticAutocenter SDL_PauseHaptic SDL_ResumeHaptic SDL_GetHapticFromID SDL_IsJoystickHaptic SDL_IsMouseHaptic SDL_CreateTray SDL_SetTrayIcon SDL_SetTrayTooltip SDL_CreateTrayMenu SDL_CreateTraySubmenu SDL_GetTrayMenu SDL_GetTraySubmenu SDL_GetTrayEntries SDL_RemoveTrayEntry SDL_InsertTrayEntryAt SDL_SetTrayEntryLabel SDL_GetTrayEntryLabel SDL_SetTrayEntryChecked SDL_GetTrayEntryChecked SDL_SetTrayEntryEnabled SDL_GetTrayEntryEnabled SDL_SetTrayEntryCallback SDL_ClickTrayEntry SDL_DestroyTray SDL_GetTrayEntryParent SDL_GetTrayMenuParentEntry SDL_GetTrayMenuParentTray SDL_UpdateTrays SDL_ShowMessageBox SDL_SetPointerProperty SDL_GetVersion SDL_GetRevision SDL_IsTablet SDL_IsTV SDL_GetSandbox SDL_SetHintWithPriority SDL_CreateDirectory SDL_GetCameraPermissionState SDL_GetCameraID SDL_GetSensorFromID; do
  n=$(grep -rl --include="*.cs" "$fn" src/SdlSharp | grep -v '/Native/' | wc -l | tr -d ' ')
  echo "$fn: $n"
done
```

Add `SDL_LogMessage` and `SDL_GetSensorID` to the list per Task 7/8 outcomes. Every count must be ≥ 1.

- [ ] **Step 5: INVENTORY.md.** Update every touched section to ✅ with accurate rows: SDL_haptic.h fully bound (31/31, effect system rows moved to bound with their managed members); SDL_tray.h new section fully bound; SDL_version.h; SDL_system.h (3 bound + the platform-group skip rationale); messagebox/properties/camera/sensor/touch/filesystem/log/hints rows updated (camera enum row, Camera.Id row, SDL_CreateDirectory row, pointer-property rows, log-emit rows per outcome). Then sweep the WHOLE file for remaining bare "deferred/Deferred" rationales and settle each one (reword to the settled reason — e.g. primary-selection = platform-niche, ShowFileDialogWithProperties = property-variant niche, GPU pixel-format pair = niche interop — or wrap if trivially in scope; do NOT leave any row whose rationale is just "deferred"). Add a short "Fully deferred headers" note pointing at `Native/Deferred.cs`. Mirror the existing table formats exactly.

- [ ] **Step 6: TODO.md.** Check off 11.1–11.4 and mark the "Phases 7–11: SDL3 Surface Completion" initiative complete (a one-line completion note with the date).

- [ ] **Step 7: Regression + final gates.**

```bash
dotnet build SdlSharp.slnx                                   # 0 warnings, 0 errors
dotnet run --project Samples/InputInfo/InputInfo.csproj      # prior success output
dotnet run --project Samples/SurfaceOps/SurfaceOps.csproj    # all checks pass
dotnet run --project Samples/AudioOps/AudioOps.csproj        # ALL PASS
dotnet run --project Samples/EventQueueOps/EventQueueOps.csproj  # ALL PASS
dotnet run --project Samples/MiscOps/MiscOps.csproj          # ALL PASS
```

(TrayDemo already ran in Task 9; do not re-run it here to avoid another menu-bar flash.)

- [ ] **Step 8: Commit**

```bash
git add src/SdlSharp/Native/Deferred.cs INVENTORY.md TODO.md
git commit -m "Document fully-deferred headers; close out phases 7-11 surface completion"
```

---

## Self-Review Notes (for the executor)

- **Fixed-buffer access in record structs:** `SDL_HapticDirection.dir`, `SDL_HapticCondition.right_sat` etc. are `fixed` buffers — accessing them requires an `unsafe` method and (for a struct copy in a local) taking them via a local variable or `ref`. The Task 2 `ToNative()` bodies do this on locals of the containing native struct type, which is legal. If the compiler objects to fixed-buffer access on a temporary, bind the struct to a local first (`var d = new ...; d.dir[0] = ...; return d;` — already the written shape).
- **`SDL_HapticEffect` union + `SDL_HapticCondition` contains fixed buffers** — explicit-layout unions over structs containing fixed buffers are fine (all blittable), matching the `SDL_Event`/`SDL_GamepadSensorEvent` precedent.
- **Task 2/3 cref ordering:** `HapticEffects.cs` doc comments reference `HapticEffect.Infinity` (defined in Task 3). If building Task 2 alone produces CS1574 warnings, fold Task 3's file creation into Task 2's commit (the tasks were split for review size, not for compile independence) — every commit must hold the 0-warning gate.
- **Tray callback identity:** `SDL_SetTrayEntryCallback` has a `void` return — there is no failure path, so the registry insert happens directly after the native call; replace-then-free-old ordering matches the house pattern.
- **Tray `InsertEntry` null returns:** SDL returns NULL both on failure and (per header) potentially for separators — the plan's code throws only when `label != null`, treating null-label null-return as a valid separator insert. Verify against the header docs when implementing; if SDL does return a valid entry for separators, the `TrayEntry?` nullability still holds.
- **MessageBox `flags` type:** the existing `Native/MessageBox.cs` uses `SDL_MessageBoxFlags` — align the new struct field & cast with what's actually there rather than the plan's `uint` guess.
- **`Camera.Id` / `Sensor.Id` unwrapping:** match the actual native ID shapes (`SDL_CameraID(uint Value)` exists; check `SDL_GetSensorID`'s binding or bind it).
- **MiscOps `PropertyGroup.Create()`:** check the real creation API (`PropertyGroup.Create()` vs constructor vs `Global`) in `PropertyGroup.cs` and use it; the round-trip is the point, not the factory shape.
- **TrayDemo Surface API:** read `Surface.cs` for the real create/fill member names before writing the sample.
