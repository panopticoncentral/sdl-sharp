using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace SdlSharp.Native;

// Deferred: Full haptic effect system (SDL_HapticEffect union with ~10 effect structs,
// SDL_CreateHapticEffect, SDL_UpdateHapticEffect, SDL_RunHapticEffect,
// SDL_DestroyHapticEffect, SDL_GetHapticEffectStatus, SDL_SetHapticGain,
// SDL_SetHapticAutocenter, SDL_PauseHaptic, SDL_ResumeHaptic, SDL_StopHapticEffects,
// SDL_GetMaxHapticEffects, SDL_GetMaxHapticEffectsPlaying, SDL_GetHapticFeatures,
// SDL_HapticEffectSupported). The full effect system requires complex struct unions
// and is only needed for advanced force feedback. The simple rumble API covers the
// common case.

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
/// Native bindings for SDL_haptic.h — haptic (force feedback) devices.
/// </summary>
[SuppressMessage("Interoperability", "CA1401:P/Invokes should not be visible")]
public static partial class Haptic
{
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
}
