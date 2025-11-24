using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Joystick;

// We are intentionally exposing the P/Invoke calls so people can do low-level calls if needed
#pragma warning disable CA1401 // P/Invokes should not be visible

namespace Sdl3Sharp.Native;

/// <summary>
/// P/Invoke bindings for SDL_haptic.h - Haptic (force feedback) support.
/// The haptic subsystem manages haptic (force feedback) devices.
/// </summary>
public static unsafe partial class Haptic
{
    /// <summary>
    /// A unique ID for a haptic device for the time it is connected to the system,
    /// and is never reused for the lifetime of the application.
    /// If the haptic device is disconnected and reconnected, it will get a new ID.
    /// The value 0 is an invalid ID.
    /// </summary>
    /// <param name="value">The haptic ID value.</param>
    public readonly struct SDL_HapticID(uint value)
    {
        /// <summary>The underlying haptic ID value.</summary>
        public readonly uint Value = value;

        /// <summary>Implicitly converts an SDL_HapticID to uint.</summary>
        /// <param name="id">The haptic ID to convert.</param>
        public static implicit operator uint(SDL_HapticID id)
        {
            return id.Value;
        }

        /// <summary>Implicitly converts a uint to SDL_HapticID.</summary>
        /// <param name="value">The value to convert.</param>
        public static implicit operator SDL_HapticID(uint value)
        {
            return new(value);
        }
    }

    /// <summary>
    /// The opaque structure used to identify an SDL haptic device.
    /// </summary>
    public struct SDL_Haptic
    {
    }

    /// <summary>Constant effect supported.</summary>
    public const uint SDL_HAPTIC_CONSTANT = 1u << 0;

    /// <summary>Sine wave effect supported.</summary>
    public const uint SDL_HAPTIC_SINE = 1u << 1;

    /// <summary>Square wave effect supported.</summary>
    public const uint SDL_HAPTIC_SQUARE = 1u << 2;

    /// <summary>Triangle wave effect supported.</summary>
    public const uint SDL_HAPTIC_TRIANGLE = 1u << 3;

    /// <summary>Sawtoothup wave effect supported.</summary>
    public const uint SDL_HAPTIC_SAWTOOTHUP = 1u << 4;

    /// <summary>Sawtoothdown wave effect supported.</summary>
    public const uint SDL_HAPTIC_SAWTOOTHDOWN = 1u << 5;

    /// <summary>Ramp effect supported.</summary>
    public const uint SDL_HAPTIC_RAMP = 1u << 6;

    /// <summary>Spring effect supported - uses axes position.</summary>
    public const uint SDL_HAPTIC_SPRING = 1u << 7;

    /// <summary>Damper effect supported - uses axes velocity.</summary>
    public const uint SDL_HAPTIC_DAMPER = 1u << 8;

    /// <summary>Inertia effect supported - uses axes acceleration.</summary>
    public const uint SDL_HAPTIC_INERTIA = 1u << 9;

    /// <summary>Friction effect supported - uses axes movement.</summary>
    public const uint SDL_HAPTIC_FRICTION = 1u << 10;

    /// <summary>Left/Right effect supported.</summary>
    public const uint SDL_HAPTIC_LEFTRIGHT = 1u << 11;

    /// <summary>Reserved for future use.</summary>
    public const uint SDL_HAPTIC_RESERVED1 = 1u << 12;

    /// <summary>Reserved for future use.</summary>
    public const uint SDL_HAPTIC_RESERVED2 = 1u << 13;

    /// <summary>Reserved for future use.</summary>
    public const uint SDL_HAPTIC_RESERVED3 = 1u << 14;

    /// <summary>Custom effect is supported.</summary>
    public const uint SDL_HAPTIC_CUSTOM = 1u << 15;

    /// <summary>Device can set global gain.</summary>
    public const uint SDL_HAPTIC_GAIN = 1u << 16;

    /// <summary>Device can set autocenter.</summary>
    public const uint SDL_HAPTIC_AUTOCENTER = 1u << 17;

    /// <summary>Device can be queried for effect status.</summary>
    public const uint SDL_HAPTIC_STATUS = 1u << 18;

    /// <summary>Device can be paused.</summary>
    public const uint SDL_HAPTIC_PAUSE = 1u << 19;

    /// <summary>
    /// Direction encoding types for haptic effects.
    /// Specifies how the direction of a haptic effect is encoded.
    /// </summary>
    public enum SDL_HapticDirectionType : byte
    {
        /// <summary>Uses polar coordinates for the direction.</summary>
        SDL_HAPTIC_POLAR = 0,

        /// <summary>Uses cartesian coordinates for the direction.</summary>
        SDL_HAPTIC_CARTESIAN = 1,

        /// <summary>Uses spherical coordinates for the direction.</summary>
        SDL_HAPTIC_SPHERICAL = 2,

        /// <summary>Use this value to play an effect on the steering wheel axis.</summary>
        SDL_HAPTIC_STEERING_AXIS = 3
    }

    /// <summary>Used to play a device an infinite number of times.</summary>
    public const uint SDL_HAPTIC_INFINITY = 4294967295U;

    /// <summary>
    /// Structure that represents a haptic direction.
    /// This is the direction where the force comes from, instead of the direction
    /// in which the force is exerted.
    /// </summary>
    public struct SDL_HapticDirection
    {
        /// <summary>The type of encoding.</summary>
        public SDL_HapticDirectionType type;

        /// <summary>The encoded direction (3 values).</summary>
        public fixed int dir[3];
    }

    /// <summary>
    /// A structure containing a template for a Constant effect.
    /// This struct is exclusively for the SDL_HAPTIC_CONSTANT effect.
    /// A constant effect applies a constant force in the specified direction to the joystick.
    /// </summary>
    public struct SDL_HapticConstant
    {
        /// <summary>SDL_HAPTIC_CONSTANT.</summary>
        public ushort type;

        /// <summary>Direction of the effect.</summary>
        public SDL_HapticDirection direction;

        /// <summary>Duration of the effect.</summary>
        public uint length;

        /// <summary>Delay before starting the effect.</summary>
        public ushort delay;

        /// <summary>Button that triggers the effect.</summary>
        public ushort button;

        /// <summary>How soon it can be triggered again after button.</summary>
        public ushort interval;

        /// <summary>Strength of the constant effect.</summary>
        public short level;

        /// <summary>Duration of the attack.</summary>
        public ushort attack_length;

        /// <summary>Level at the start of the attack.</summary>
        public ushort attack_level;

        /// <summary>Duration of the fade.</summary>
        public ushort fade_length;

        /// <summary>Level at the end of the fade.</summary>
        public ushort fade_level;
    }

    /// <summary>
    /// A structure containing a template for a Periodic effect.
    /// The struct handles SDL_HAPTIC_SINE, SDL_HAPTIC_SQUARE, SDL_HAPTIC_TRIANGLE,
    /// SDL_HAPTIC_SAWTOOTHUP, and SDL_HAPTIC_SAWTOOTHDOWN effects.
    /// </summary>
    public struct SDL_HapticPeriodic
    {
        /// <summary>SDL_HAPTIC_SINE, SDL_HAPTIC_SQUARE, SDL_HAPTIC_TRIANGLE, SDL_HAPTIC_SAWTOOTHUP, or SDL_HAPTIC_SAWTOOTHDOWN.</summary>
        public ushort type;

        /// <summary>Direction of the effect.</summary>
        public SDL_HapticDirection direction;

        /// <summary>Duration of the effect.</summary>
        public uint length;

        /// <summary>Delay before starting the effect.</summary>
        public ushort delay;

        /// <summary>Button that triggers the effect.</summary>
        public ushort button;

        /// <summary>How soon it can be triggered again after button.</summary>
        public ushort interval;

        /// <summary>Period of the wave.</summary>
        public ushort period;

        /// <summary>Peak value; if negative, equivalent to 180 degrees extra phase shift.</summary>
        public short magnitude;

        /// <summary>Mean value of the wave.</summary>
        public short offset;

        /// <summary>Positive phase shift given by hundredth of a degree.</summary>
        public ushort phase;

        /// <summary>Duration of the attack.</summary>
        public ushort attack_length;

        /// <summary>Level at the start of the attack.</summary>
        public ushort attack_level;

        /// <summary>Duration of the fade.</summary>
        public ushort fade_length;

        /// <summary>Level at the end of the fade.</summary>
        public ushort fade_level;
    }

    /// <summary>
    /// A structure containing a template for a Condition effect.
    /// The struct handles SDL_HAPTIC_SPRING, SDL_HAPTIC_DAMPER, SDL_HAPTIC_INERTIA,
    /// and SDL_HAPTIC_FRICTION effects.
    /// </summary>
    public struct SDL_HapticCondition
    {
        /// <summary>SDL_HAPTIC_SPRING, SDL_HAPTIC_DAMPER, SDL_HAPTIC_INERTIA, or SDL_HAPTIC_FRICTION.</summary>
        public ushort type;

        /// <summary>Direction of the effect.</summary>
        public SDL_HapticDirection direction;

        /// <summary>Duration of the effect.</summary>
        public uint length;

        /// <summary>Delay before starting the effect.</summary>
        public ushort delay;

        /// <summary>Button that triggers the effect.</summary>
        public ushort button;

        /// <summary>How soon it can be triggered again after button.</summary>
        public ushort interval;

        /// <summary>Level when joystick is to the positive side; max 0xFFFF (3 values for X, Y, Z axes).</summary>
        public fixed ushort right_sat[3];

        /// <summary>Level when joystick is to the negative side; max 0xFFFF (3 values for X, Y, Z axes).</summary>
        public fixed ushort left_sat[3];

        /// <summary>How fast to increase the force towards the positive side (3 values for X, Y, Z axes).</summary>
        public fixed short right_coeff[3];

        /// <summary>How fast to increase the force towards the negative side (3 values for X, Y, Z axes).</summary>
        public fixed short left_coeff[3];

        /// <summary>Size of the dead zone; max 0xFFFF: whole axis-range when 0-centered (3 values for X, Y, Z axes).</summary>
        public fixed ushort deadband[3];

        /// <summary>Position of the dead zone (3 values for X, Y, Z axes).</summary>
        public fixed short center[3];
    }

    /// <summary>
    /// A structure containing a template for a Ramp effect.
    /// This struct is exclusively for the SDL_HAPTIC_RAMP effect.
    /// The ramp effect starts at start strength and ends at end strength.
    /// </summary>
    public struct SDL_HapticRamp
    {
        /// <summary>SDL_HAPTIC_RAMP.</summary>
        public ushort type;

        /// <summary>Direction of the effect.</summary>
        public SDL_HapticDirection direction;

        /// <summary>Duration of the effect.</summary>
        public uint length;

        /// <summary>Delay before starting the effect.</summary>
        public ushort delay;

        /// <summary>Button that triggers the effect.</summary>
        public ushort button;

        /// <summary>How soon it can be triggered again after button.</summary>
        public ushort interval;

        /// <summary>Beginning strength level.</summary>
        public short start;

        /// <summary>Ending strength level.</summary>
        public short end;

        /// <summary>Duration of the attack.</summary>
        public ushort attack_length;

        /// <summary>Level at the start of the attack.</summary>
        public ushort attack_level;

        /// <summary>Duration of the fade.</summary>
        public ushort fade_length;

        /// <summary>Level at the end of the fade.</summary>
        public ushort fade_level;
    }

    /// <summary>
    /// A structure containing a template for a Left/Right effect.
    /// This struct is exclusively for the SDL_HAPTIC_LEFTRIGHT effect.
    /// The Left/Right effect is used to explicitly control the large and small motors.
    /// </summary>
    public struct SDL_HapticLeftRight
    {
        /// <summary>SDL_HAPTIC_LEFTRIGHT.</summary>
        public ushort type;

        /// <summary>Duration of the effect in milliseconds.</summary>
        public uint length;

        /// <summary>Control of the large controller motor.</summary>
        public ushort large_magnitude;

        /// <summary>Control of the small controller motor.</summary>
        public ushort small_magnitude;
    }

    /// <summary>
    /// A structure containing a template for the SDL_HAPTIC_CUSTOM effect.
    /// This struct is exclusively for the SDL_HAPTIC_CUSTOM effect.
    /// A custom force feedback effect is much like a periodic effect, where the
    /// application can define its exact shape.
    /// </summary>
    public struct SDL_HapticCustom
    {
        /// <summary>SDL_HAPTIC_CUSTOM.</summary>
        public ushort type;

        /// <summary>Direction of the effect.</summary>
        public SDL_HapticDirection direction;

        /// <summary>Duration of the effect.</summary>
        public uint length;

        /// <summary>Delay before starting the effect.</summary>
        public ushort delay;

        /// <summary>Button that triggers the effect.</summary>
        public ushort button;

        /// <summary>How soon it can be triggered again after button.</summary>
        public ushort interval;

        /// <summary>Axes to use, minimum of one.</summary>
        public byte channels;

        /// <summary>Sample periods.</summary>
        public ushort period;

        /// <summary>Amount of samples.</summary>
        public ushort samples;

        /// <summary>Should contain channels*samples items.</summary>
        public ushort* data;

        /// <summary>Duration of the attack.</summary>
        public ushort attack_length;

        /// <summary>Level at the start of the attack.</summary>
        public ushort attack_level;

        /// <summary>Duration of the fade.</summary>
        public ushort fade_length;

        /// <summary>Level at the end of the fade.</summary>
        public ushort fade_level;
    }

    /// <summary>
    /// The generic template for any haptic effect.
    /// All values max at 32767 (0x7FFF). Signed values also can be negative.
    /// Time values unless specified otherwise are in milliseconds.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct SDL_HapticEffect
    {
        /// <summary>Effect type.</summary>
        [FieldOffset(0)]
        public ushort type;

        /// <summary>Constant effect.</summary>
        [FieldOffset(0)]
        public SDL_HapticConstant constant;

        /// <summary>Periodic effect.</summary>
        [FieldOffset(0)]
        public SDL_HapticPeriodic periodic;

        /// <summary>Condition effect.</summary>
        [FieldOffset(0)]
        public SDL_HapticCondition condition;

        /// <summary>Ramp effect.</summary>
        [FieldOffset(0)]
        public SDL_HapticRamp ramp;

        /// <summary>Left/Right effect.</summary>
        [FieldOffset(0)]
        public SDL_HapticLeftRight leftright;

        /// <summary>Custom effect.</summary>
        [FieldOffset(0)]
        public SDL_HapticCustom custom;
    }

    /// <summary>
    /// Get a list of currently connected haptic devices.
    /// </summary>
    /// <param name="count">A pointer filled in with the number of haptic devices returned, may be NULL.</param>
    /// <returns>A 0 terminated array of haptic device instance IDs or NULL on failure; call SDL_GetError() for more information. This should be freed with SDL_free() when it is no longer needed.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_HapticID* SDL_GetHaptics(int* count);

    /// <summary>
    /// Get the implementation dependent name of a haptic device.
    /// This can be called before any haptic devices are opened.
    /// </summary>
    /// <param name="instance_id">The haptic device instance ID.</param>
    /// <returns>The name of the selected haptic device. If no name can be found, this function returns NULL; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(Utf8StringMarshaller))]
    public static partial string? SDL_GetHapticNameForID(SDL_HapticID instance_id);

    /// <summary>
    /// Open a haptic device for use.
    /// The index passed as an argument refers to the N'th haptic device on this system.
    /// When opening a haptic device, its gain will be set to maximum and autocenter will be disabled.
    /// </summary>
    /// <param name="instance_id">The haptic device instance ID.</param>
    /// <returns>The device identifier or NULL on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Haptic* SDL_OpenHaptic(SDL_HapticID instance_id);

    /// <summary>
    /// Get the SDL_Haptic associated with an instance ID, if it has been opened.
    /// </summary>
    /// <param name="instance_id">The instance ID to get the SDL_Haptic for.</param>
    /// <returns>An SDL_Haptic on success or NULL on failure or if it hasn't been opened yet; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Haptic* SDL_GetHapticFromID(SDL_HapticID instance_id);

    /// <summary>
    /// Get the instance ID of an opened haptic device.
    /// </summary>
    /// <param name="haptic">The SDL_Haptic device to query.</param>
    /// <returns>The instance ID of the specified haptic device on success or 0 on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_HapticID SDL_GetHapticID(SDL_Haptic* haptic);

    /// <summary>
    /// Get the implementation dependent name of a haptic device.
    /// </summary>
    /// <param name="haptic">The SDL_Haptic obtained from SDL_OpenHaptic().</param>
    /// <returns>The name of the selected haptic device. If no name can be found, this function returns NULL; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(Utf8StringMarshaller))]
    public static partial string? SDL_GetHapticName(SDL_Haptic* haptic);

    /// <summary>
    /// Query whether or not the current mouse has haptic capabilities.
    /// </summary>
    /// <returns>True if the mouse is haptic or false if it isn't.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_IsMouseHaptic();

    /// <summary>
    /// Try to open a haptic device from the current mouse.
    /// </summary>
    /// <returns>The haptic device identifier or NULL on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Haptic* SDL_OpenHapticFromMouse();

    /// <summary>
    /// Query if a joystick has haptic features.
    /// </summary>
    /// <param name="joystick">The SDL_Joystick to test for haptic capabilities.</param>
    /// <returns>True if the joystick is haptic or false if it isn't.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_IsJoystickHaptic(SDL_Joystick* joystick);

    /// <summary>
    /// Open a haptic device for use from a joystick device.
    /// You must still close the haptic device separately. It will not be closed with the joystick.
    /// </summary>
    /// <param name="joystick">The SDL_Joystick to create a haptic device from.</param>
    /// <returns>A valid haptic device identifier on success or NULL on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Haptic* SDL_OpenHapticFromJoystick(SDL_Joystick* joystick);

    /// <summary>
    /// Close a haptic device previously opened with SDL_OpenHaptic().
    /// </summary>
    /// <param name="haptic">The SDL_Haptic device to close.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_CloseHaptic(SDL_Haptic* haptic);

    /// <summary>
    /// Get the number of effects a haptic device can store.
    /// On some platforms this isn't fully supported, and therefore is an approximation.
    /// </summary>
    /// <param name="haptic">The SDL_Haptic device to query.</param>
    /// <returns>The number of effects the haptic device can store or a negative error code on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_GetMaxHapticEffects(SDL_Haptic* haptic);

    /// <summary>
    /// Get the number of effects a haptic device can play at the same time.
    /// This is not supported on all platforms, but will always return a value.
    /// </summary>
    /// <param name="haptic">The SDL_Haptic device to query maximum playing effects.</param>
    /// <returns>The number of effects the haptic device can play at the same time or -1 on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_GetMaxHapticEffectsPlaying(SDL_Haptic* haptic);

    /// <summary>
    /// Get the haptic device's supported features in bitwise manner.
    /// </summary>
    /// <param name="haptic">The SDL_Haptic device to query.</param>
    /// <returns>A list of supported haptic features in bitwise manner (OR'd), or 0 on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint SDL_GetHapticFeatures(SDL_Haptic* haptic);

    /// <summary>
    /// Get the number of haptic axes the device has.
    /// The number of haptic axes might be useful if working with the SDL_HapticDirection effect.
    /// </summary>
    /// <param name="haptic">The SDL_Haptic device to query.</param>
    /// <returns>The number of axes on success or -1 on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_GetNumHapticAxes(SDL_Haptic* haptic);

    /// <summary>
    /// Check to see if an effect is supported by a haptic device.
    /// </summary>
    /// <param name="haptic">The SDL_Haptic device to query.</param>
    /// <param name="effect">The desired effect to query.</param>
    /// <returns>True if the effect is supported or false if it isn't.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HapticEffectSupported(SDL_Haptic* haptic, SDL_HapticEffect* effect);

    /// <summary>
    /// Create a new haptic effect on a specified device.
    /// </summary>
    /// <param name="haptic">An SDL_Haptic device to create the effect on.</param>
    /// <param name="effect">An SDL_HapticEffect structure containing the properties of the effect to create.</param>
    /// <returns>The ID of the effect on success or -1 on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_CreateHapticEffect(SDL_Haptic* haptic, SDL_HapticEffect* effect);

    /// <summary>
    /// Update the properties of an effect.
    /// Can be used dynamically, although behavior when dynamically changing direction may be strange.
    /// </summary>
    /// <param name="haptic">The SDL_Haptic device that has the effect.</param>
    /// <param name="effect">The identifier of the effect to update.</param>
    /// <param name="data">An SDL_HapticEffect structure containing the new effect properties to use.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_UpdateHapticEffect(SDL_Haptic* haptic, int effect, SDL_HapticEffect* data);

    /// <summary>
    /// Run the haptic effect on its associated haptic device.
    /// To repeat the effect over and over indefinitely, set iterations to SDL_HAPTIC_INFINITY.
    /// </summary>
    /// <param name="haptic">The SDL_Haptic device to run the effect on.</param>
    /// <param name="effect">The ID of the haptic effect to run.</param>
    /// <param name="iterations">The number of iterations to run the effect; use SDL_HAPTIC_INFINITY to repeat forever.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_RunHapticEffect(SDL_Haptic* haptic, int effect, uint iterations);

    /// <summary>
    /// Stop the haptic effect on its associated haptic device.
    /// </summary>
    /// <param name="haptic">The SDL_Haptic device to stop the effect on.</param>
    /// <param name="effect">The ID of the haptic effect to stop.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_StopHapticEffect(SDL_Haptic* haptic, int effect);

    /// <summary>
    /// Destroy a haptic effect on the device.
    /// This will stop the effect if it's running. Effects are automatically destroyed when the device is closed.
    /// </summary>
    /// <param name="haptic">The SDL_Haptic device to destroy the effect on.</param>
    /// <param name="effect">The ID of the haptic effect to destroy.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_DestroyHapticEffect(SDL_Haptic* haptic, int effect);

    /// <summary>
    /// Get the status of the current effect on the specified haptic device.
    /// Device must support the SDL_HAPTIC_STATUS feature.
    /// </summary>
    /// <param name="haptic">The SDL_Haptic device to query for the effect status on.</param>
    /// <param name="effect">The ID of the haptic effect to query its status.</param>
    /// <returns>True if it is playing, false if it isn't playing or haptic status isn't supported.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetHapticEffectStatus(SDL_Haptic* haptic, int effect);

    /// <summary>
    /// Set the global gain of the specified haptic device.
    /// Device must support the SDL_HAPTIC_GAIN feature.
    /// </summary>
    /// <param name="haptic">The SDL_Haptic device to set the gain on.</param>
    /// <param name="gain">Value to set the gain to, should be between 0 and 100 (0 - 100).</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetHapticGain(SDL_Haptic* haptic, int gain);

    /// <summary>
    /// Set the global autocenter of the device.
    /// Autocenter should be between 0 and 100. Setting it to 0 will disable autocentering.
    /// Device must support the SDL_HAPTIC_AUTOCENTER feature.
    /// </summary>
    /// <param name="haptic">The SDL_Haptic device to set autocentering on.</param>
    /// <param name="autocenter">Value to set autocenter to (0-100).</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetHapticAutocenter(SDL_Haptic* haptic, int autocenter);

    /// <summary>
    /// Pause a haptic device.
    /// Device must support the SDL_HAPTIC_PAUSE feature. Call SDL_ResumeHaptic() to resume playback.
    /// </summary>
    /// <param name="haptic">The SDL_Haptic device to pause.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_PauseHaptic(SDL_Haptic* haptic);

    /// <summary>
    /// Resume a haptic device.
    /// Call to unpause after SDL_PauseHaptic().
    /// </summary>
    /// <param name="haptic">The SDL_Haptic device to unpause.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_ResumeHaptic(SDL_Haptic* haptic);

    /// <summary>
    /// Stop all the currently playing effects on a haptic device.
    /// </summary>
    /// <param name="haptic">The SDL_Haptic device to stop.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_StopHapticEffects(SDL_Haptic* haptic);

    /// <summary>
    /// Check whether rumble is supported on a haptic device.
    /// </summary>
    /// <param name="haptic">Haptic device to check for rumble support.</param>
    /// <returns>True if the effect is supported or false if it isn't.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HapticRumbleSupported(SDL_Haptic* haptic);

    /// <summary>
    /// Initialize a haptic device for simple rumble playback.
    /// </summary>
    /// <param name="haptic">The haptic device to initialize for simple rumble playback.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_InitHapticRumble(SDL_Haptic* haptic);

    /// <summary>
    /// Run a simple rumble effect on a haptic device.
    /// </summary>
    /// <param name="haptic">The haptic device to play the rumble effect on.</param>
    /// <param name="strength">Strength of the rumble to play as a 0-1 float value.</param>
    /// <param name="length">Length of the rumble to play in milliseconds.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_PlayHapticRumble(SDL_Haptic* haptic, float strength, uint length);

    /// <summary>
    /// Stop the simple rumble on a haptic device.
    /// </summary>
    /// <param name="haptic">The haptic device to stop the rumble effect on.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_StopHapticRumble(SDL_Haptic* haptic);
}
