using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Haptic;
using static Sdl3Sharp.Native.StdInc;

namespace Sdl3Sharp.Input;

/// <summary>
/// Represents an opened SDL haptic (force feedback) device.
/// </summary>
public sealed unsafe class Haptic : IDisposable
{
    private bool _disposed;
    private readonly bool _ownsHandle;

    /// <summary>
    /// Used to play a device an infinite number of times.
    /// </summary>
    public const uint Infinity = SDL_HAPTIC_INFINITY;

    /// <summary>
    /// Gets the underlying SDL_Haptic pointer.
    /// </summary>
    public SDL_Haptic* Handle { get; private set; }

    /// <summary>
    /// Gets the implementation dependent name of this haptic device.
    /// </summary>
    public string Name
    {
        get
        {
            ThrowIfDisposed();
            return CheckErrorNull(SDL_GetHapticName(Handle));
        }
    }

    /// <summary>
    /// Gets the descriptor for this haptic device.
    /// </summary>
    public HapticDescriptor Descriptor
    {
        get
        {
            ThrowIfDisposed();
            return new(CheckErrorZero(SDL_GetHapticID(Handle)));
        }
    }

    /// <summary>
    /// Gets the number of effects this haptic device can store.
    /// On some platforms this isn't fully supported, and therefore is an approximation.
    /// </summary>
    public int MaxEffects
    {
        get
        {
            ThrowIfDisposed();
            return CheckErrorNegativeOne(SDL_GetMaxHapticEffects(Handle));
        }
    }

    /// <summary>
    /// Gets the number of effects this haptic device can play at the same time.
    /// </summary>
    public int MaxEffectsPlaying
    {
        get
        {
            ThrowIfDisposed();
            return CheckErrorNegativeOne(SDL_GetMaxHapticEffectsPlaying(Handle));
        }
    }

    /// <summary>
    /// Gets the supported features of this haptic device.
    /// </summary>
    public HapticFeatures Features
    {
        get
        {
            ThrowIfDisposed();
            return (HapticFeatures)SDL_GetHapticFeatures(Handle);
        }
    }

    /// <summary>
    /// Gets the number of haptic axes this device has.
    /// </summary>
    public int AxisCount
    {
        get
        {
            ThrowIfDisposed();
            return CheckErrorNegativeOne(SDL_GetNumHapticAxes(Handle));
        }
    }

    /// <summary>
    /// Gets whether rumble is supported on this haptic device.
    /// </summary>
    public bool RumbleSupported
    {
        get
        {
            ThrowIfDisposed();
            return SDL_HapticRumbleSupported(Handle);
        }
    }

    /// <summary>
    /// Gets whether the current mouse has haptic capabilities.
    /// </summary>
    public static bool IsMouseHaptic => SDL_IsMouseHaptic();

    /// <summary>
    /// Gets a list of currently connected haptic devices.
    /// </summary>
    /// <returns>An array of haptic descriptors.</returns>
    public static HapticDescriptor[] GetHaptics()
    {
        int count;
        SDL_HapticID* haptics = CheckErrorPointer(SDL_GetHaptics(&count));

        try
        {
            var result = new HapticDescriptor[count];
            for (var i = 0; i < count; i++)
            {
                result[i] = new HapticDescriptor(haptics[i]);
            }

            return result;
        }
        finally
        {
            SDL_free(haptics);
        }
    }

    /// <summary>
    /// Opens a haptic device from the current mouse.
    /// </summary>
    /// <returns>A new Haptic instance.</returns>
    public static Haptic OpenFromMouse()
    {
        return new(CheckErrorPointer(SDL_OpenHapticFromMouse()), ownsHandle: true);
    }

    /// <summary>
    /// Checks if a joystick has haptic features.
    /// </summary>
    /// <param name="joystick">The joystick to test.</param>
    /// <returns>True if the joystick has haptic capabilities.</returns>
    public static bool IsJoystickHaptic(Joystick joystick)
    {
        return SDL_IsJoystickHaptic(joystick.Handle);
    }

    /// <summary>
    /// Opens a haptic device from a joystick.
    /// You must still close the haptic device separately. It will not be closed with the joystick.
    /// </summary>
    /// <param name="joystick">The joystick to create a haptic device from.</param>
    /// <returns>A new Haptic instance.</returns>
    public static Haptic OpenFromJoystick(Joystick joystick)
    {
        return new(CheckErrorPointer(SDL_OpenHapticFromJoystick(joystick.Handle)), ownsHandle: true);
    }

    /// <summary>
    /// Wraps an existing SDL_Haptic pointer.
    /// </summary>
    /// <param name="handle">The SDL_Haptic pointer to wrap.</param>
    /// <param name="ownsHandle">Whether this instance should close the haptic device when disposed.</param>
    internal Haptic(SDL_Haptic* handle, bool ownsHandle = true)
    {
        Handle = handle;
        _ownsHandle = ownsHandle;
    }

    /// <summary>
    /// Checks if an effect is supported by this haptic device.
    /// </summary>
    /// <param name="effect">The effect to check.</param>
    /// <returns>True if the effect is supported.</returns>
    public bool IsEffectSupported(SDL_HapticEffect* effect)
    {
        ThrowIfDisposed();
        return SDL_HapticEffectSupported(Handle, effect);
    }

    /// <summary>
    /// Creates a new haptic effect on this device.
    /// </summary>
    /// <param name="effect">The effect to create.</param>
    /// <returns>The ID of the created effect.</returns>
    public int CreateEffect(SDL_HapticEffect* effect)
    {
        ThrowIfDisposed();
        return CheckErrorNegativeOne(SDL_CreateHapticEffect(Handle, effect));
    }

    /// <summary>
    /// Updates the properties of an effect.
    /// </summary>
    /// <param name="effectId">The ID of the effect to update.</param>
    /// <param name="effect">The new effect properties.</param>
    public void UpdateEffect(int effectId, SDL_HapticEffect* effect)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_UpdateHapticEffect(Handle, effectId, effect));
    }

    /// <summary>
    /// Runs a haptic effect on this device.
    /// </summary>
    /// <param name="effectId">The ID of the effect to run.</param>
    /// <param name="iterations">The number of iterations to run the effect. Use <see cref="Infinity"/> to repeat forever.</param>
    public void RunEffect(int effectId, uint iterations = 1)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_RunHapticEffect(Handle, effectId, iterations));
    }

    /// <summary>
    /// Stops a haptic effect on this device.
    /// </summary>
    /// <param name="effectId">The ID of the effect to stop.</param>
    public void StopEffect(int effectId)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_StopHapticEffect(Handle, effectId));
    }

    /// <summary>
    /// Destroys a haptic effect on this device.
    /// This will stop the effect if it's running.
    /// </summary>
    /// <param name="effectId">The ID of the effect to destroy.</param>
    public void DestroyEffect(int effectId)
    {
        ThrowIfDisposed();
        SDL_DestroyHapticEffect(Handle, effectId);
    }

    /// <summary>
    /// Gets the status of an effect on this device.
    /// Device must support the <see cref="HapticFeatures.Status"/> feature.
    /// </summary>
    /// <param name="effectId">The ID of the effect to query.</param>
    /// <returns>True if the effect is playing, false otherwise.</returns>
    public bool GetEffectStatus(int effectId)
    {
        ThrowIfDisposed();
        return SDL_GetHapticEffectStatus(Handle, effectId);
    }

    /// <summary>
    /// Sets the global gain of this haptic device.
    /// Device must support the <see cref="HapticFeatures.Gain"/> feature.
    /// </summary>
    /// <param name="gain">Value to set the gain to, should be between 0 and 100.</param>
    public void SetGain(int gain)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_SetHapticGain(Handle, gain));
    }

    /// <summary>
    /// Sets the global autocenter of this device.
    /// Device must support the <see cref="HapticFeatures.Autocenter"/> feature.
    /// </summary>
    /// <param name="autocenter">Value to set autocenter to (0-100). Setting to 0 disables autocentering.</param>
    public void SetAutocenter(int autocenter)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_SetHapticAutocenter(Handle, autocenter));
    }

    /// <summary>
    /// Pauses this haptic device.
    /// Device must support the <see cref="HapticFeatures.Pause"/> feature.
    /// Call <see cref="Resume"/> to resume playback.
    /// </summary>
    public void Pause()
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_PauseHaptic(Handle));
    }

    /// <summary>
    /// Resumes this haptic device after being paused.
    /// </summary>
    public void Resume()
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_ResumeHaptic(Handle));
    }

    /// <summary>
    /// Stops all currently playing effects on this haptic device.
    /// </summary>
    public void StopAllEffects()
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_StopHapticEffects(Handle));
    }

    /// <summary>
    /// Initializes this haptic device for simple rumble playback.
    /// </summary>
    public void InitRumble()
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_InitHapticRumble(Handle));
    }

    /// <summary>
    /// Runs a simple rumble effect on this haptic device.
    /// </summary>
    /// <param name="strength">Strength of the rumble to play as a 0-1 float value.</param>
    /// <param name="lengthMs">Length of the rumble to play in milliseconds.</param>
    public void PlayRumble(float strength, uint lengthMs)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_PlayHapticRumble(Handle, strength, lengthMs));
    }

    /// <summary>
    /// Stops the simple rumble on this haptic device.
    /// </summary>
    public void StopRumble()
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_StopHapticRumble(Handle));
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        if (_ownsHandle && Handle is not null)
        {
            SDL_CloseHaptic(Handle);
            Handle = null;
        }

        _disposed = true;
    }

    private void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
    }
}
