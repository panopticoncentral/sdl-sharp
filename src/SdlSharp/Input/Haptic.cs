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

    internal Native.SDL_Haptic* Handle
    {
        get
        {
            ObjectDisposedException.ThrowIf(_handle == null, this);
            return _handle;
        }
    }

    private Native.SDL_Haptic* _handle;

    internal Haptic(Native.SDL_Haptic* handle, bool ownsHandle = true)
    {
        _handle = handle;
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

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_ownsHandle && _handle != null)
        {
            SDL_CloseHaptic(_handle);
        }
        _handle = null;
    }
}
