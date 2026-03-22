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
