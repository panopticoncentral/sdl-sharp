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
