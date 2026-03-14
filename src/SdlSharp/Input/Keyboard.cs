using System.Runtime.InteropServices;

using static SdlSharp.Native.Common;
using static SdlSharp.Native.Keyboard;

namespace SdlSharp.Input;

/// <summary>
/// Provides keyboard state queries and utility functions.
/// </summary>
public static unsafe class Keyboard
{
    /// <summary>
    /// Returns whether any keyboard is currently connected.
    /// </summary>
    public static bool HasKeyboard() => SDL_HasKeyboard();

    /// <summary>
    /// Resets the keyboard state, generating key-up events for all pressed keys.
    /// </summary>
    public static void Reset() => SDL_ResetKeyboard();

    /// <summary>
    /// Gets the current key modifier state.
    /// </summary>
    public static KeyModifiers ModState => (KeyModifiers)SDL_GetModState();

    /// <summary>
    /// Sets the current key modifier state.
    /// </summary>
    /// <param name="modifiers">The modifier state to set.</param>
    public static void SetModState(KeyModifiers modifiers) =>
        SDL_SetModState((Native.SDL_Keymod)modifiers);

    /// <summary>
    /// Returns true if the specified scancode is currently pressed.
    /// </summary>
    /// <param name="scancode">The scancode to check.</param>
    /// <returns>True if the key is pressed.</returns>
    public static bool IsKeyPressed(Scancode scancode)
    {
        int numkeys;
        var state = SDL_GetKeyboardState(&numkeys);
        var index = (int)scancode;
        return index >= 0 && index < numkeys && state[index] != 0;
    }

    /// <summary>
    /// Gets a human-readable name for a scancode.
    /// </summary>
    /// <param name="scancode">The scancode to query.</param>
    /// <returns>The name of the scancode.</returns>
    public static string? GetScancodeName(Scancode scancode) =>
        Marshal.PtrToStringUTF8((nint)SDL_GetScancodeName((Native.SDL_Scancode)scancode));

    /// <summary>
    /// Gets a human-readable name for a key code.
    /// </summary>
    /// <param name="key">The key code to query.</param>
    /// <returns>The name of the key.</returns>
    public static string? GetKeyName(Keycode key) =>
        Marshal.PtrToStringUTF8((nint)SDL_GetKeyName((Native.SDL_Keycode)key));

    /// <summary>
    /// Gets the key code for a scancode based on the current keyboard layout.
    /// </summary>
    /// <param name="scancode">The scancode to translate.</param>
    /// <param name="modifiers">The modifier state to apply.</param>
    /// <returns>The corresponding key code.</returns>
    public static Keycode GetKeyFromScancode(Scancode scancode, KeyModifiers modifiers = KeyModifiers.None) =>
        (Keycode)SDL_GetKeyFromScancode((Native.SDL_Scancode)scancode, (Native.SDL_Keymod)modifiers, true);

    /// <summary>
    /// Returns whether the platform has screen keyboard support.
    /// </summary>
    public static bool HasScreenKeyboardSupport() => SDL_HasScreenKeyboardSupport();
}
