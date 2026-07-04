using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace SdlSharp.Native;

/// <summary>
/// SDL Haptic instance IDs.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly record struct SDL_HapticID(uint Value);

/// <summary>
/// Opaque handle for a haptic (force feedback) device.
/// </summary>
public struct SDL_Haptic;

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

/// <summary>
/// Native bindings for SDL_haptic.h — haptic (force feedback) devices.
/// </summary>
[SuppressMessage("Interoperability", "CA1401:P/Invokes should not be visible")]
public static partial class Haptic
{
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

    /// <summary>Get a list of currently connected haptic devices.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetHaptics")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_HapticID* SDL_GetHaptics(out int count);

    /// <summary>Get the name of a haptic device.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetHapticNameForID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* SDL_GetHapticNameForID(SDL_HapticID instance_id);

    /// <summary>Open a haptic device for use.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_OpenHaptic")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Haptic* SDL_OpenHaptic(SDL_HapticID instance_id);

    /// <summary>Get the SDL_HapticID for an opened haptic device.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetHapticID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_HapticID SDL_GetHapticID(SDL_Haptic* haptic);

    /// <summary>Get the name of an opened haptic device.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetHapticName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* SDL_GetHapticName(SDL_Haptic* haptic);

    /// <summary>Try to open a haptic device from the current mouse.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_OpenHapticFromMouse")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Haptic* SDL_OpenHapticFromMouse();

    /// <summary>Open a haptic device from a joystick.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_OpenHapticFromJoystick")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Haptic* SDL_OpenHapticFromJoystick(SDL_Joystick* joystick);

    /// <summary>Close a haptic device.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_CloseHaptic")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_CloseHaptic(SDL_Haptic* haptic);

    // --- Simple rumble API ---

    /// <summary>Check whether rumble is supported on a haptic device.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_HapticRumbleSupported")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_HapticRumbleSupported(SDL_Haptic* haptic);

    /// <summary>Initialize a haptic device for simple rumble playback.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_InitHapticRumble")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_InitHapticRumble(SDL_Haptic* haptic);

    /// <summary>Run a simple rumble effect on a haptic device.</summary>
    /// <param name="haptic">The haptic device.</param>
    /// <param name="strength">Strength from 0.0 to 1.0.</param>
    /// <param name="length">Duration in milliseconds.</param>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_PlayHapticRumble")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_PlayHapticRumble(SDL_Haptic* haptic, float strength, uint length);

    /// <summary>Stop the simple rumble on a haptic device.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_StopHapticRumble")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_StopHapticRumble(SDL_Haptic* haptic);

    // --- Full effect system ---

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
}
