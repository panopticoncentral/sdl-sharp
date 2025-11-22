using Sdl3Sharp.Graphics;
using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Keyboard;
using static Sdl3Sharp.Native.Keycode;
using static Sdl3Sharp.Native.Scancode;
using static Sdl3Sharp.Native.StdInc;

namespace Sdl3Sharp.Input;

/// <summary>
/// Provides keyboard input functionality.
/// </summary>
/// <remarks>
/// Please refer to the Best Keyboard Practices document for details on how best to accept keyboard input:
/// https://wiki.libsdl.org/SDL3/BestKeyboardPractices
/// </remarks>
public unsafe readonly record struct Keyboard(uint Id)
{
    /// <summary>
    /// Gets whether a keyboard is currently connected.
    /// </summary>
    public static bool HasKeyboard => SDL_HasKeyboard();

    /// <summary>
    /// Gets the name of the keyboard.
    /// </summary>
    public string Name => CheckErrorNull(SDL_GetKeyboardNameForID(Id));

    /// <summary>
    /// Gets a list of currently connected keyboards.
    /// </summary>
    /// <returns>An array of keyboard device information.</returns>
    public static Keyboard[] GetKeyboards()
    {
        int count;
        SDL_KeyboardID* keyboards = CheckErrorPointer(SDL_GetKeyboards(&count));

        try
        {
            var result = new Keyboard[count];
            for (var i = 0; i < count; i++)
            {
                result[i] = new Keyboard(keyboards[i]);
            }
            return result;
        }
        finally
        {
            SDL_free(keyboards);
        }
    }

    /// <summary>
    /// Gets the window which currently has keyboard focus.
    /// </summary>
    /// <returns>The window with keyboard focus, or null if no window has focus.</returns>
    public static Window? GetFocusedWindow()
    {
        Native.Video.SDL_Window* window = SDL_GetKeyboardFocus();
        return window != null ? new Window(window) : null;
    }

    /// <summary>
    /// Gets a snapshot of the current state of the keyboard.
    /// The returned span is valid for the whole lifetime of the application.
    /// Use SDL events or call PumpEvents to update the state.
    /// </summary>
    /// <returns>A read-only span of key states indexed by scancode values.</returns>
    public static ReadOnlySpan<bool> GetState()
    {
        int numKeys;
        var state = SDL_GetKeyboardState(&numKeys);
        return new ReadOnlySpan<bool>(state, numKeys);
    }

    /// <summary>
    /// Checks if a specific key is currently pressed.
    /// </summary>
    /// <param name="scancode">The scancode of the key to check.</param>
    /// <returns>True if the key is pressed, false otherwise.</returns>
    public static bool IsKeyPressed(Scancode scancode)
    {
        int numKeys;
        var state = SDL_GetKeyboardState(&numKeys);
        var index = (int)scancode;
        return index >= 0 && index < numKeys && state[index];
    }

    /// <summary>
    /// Clears the state of the keyboard.
    /// This function will generate key up events for all pressed keys.
    /// </summary>
    public static void Reset()
    {
        SDL_ResetKeyboard();
    }

    /// <summary>
    /// The current key modifier state for the keyboard.
    /// </summary>
    public static KeyModifiers ModifierState
    {
        get => (KeyModifiers)SDL_GetModState(); 
        set => SDL_SetModState((SDL_Keymod)value);
    }

    /// <summary>
    /// Gets the keycode corresponding to the given scancode according to the current keyboard layout.
    /// </summary>
    /// <param name="scancode">The scancode to convert.</param>
    /// <param name="modifiers">The modifier state to use when translating.</param>
    /// <param name="keyEvent">True if the keycode will be used in key events.</param>
    /// <returns>The keycode that corresponds to the given scancode.</returns>
    public static Keycode GetKeyFromScancode(Scancode scancode, KeyModifiers modifiers = KeyModifiers.None, bool keyEvent = false)
    {
        return (Keycode)SDL_GetKeyFromScancode((SDL_Scancode)scancode, (SDL_Keymod)modifiers, keyEvent);
    }

    /// <summary>
    /// Gets the scancode corresponding to the given keycode according to the current keyboard layout.
    /// Note that there may be multiple scancode+modifier states that can generate this keycode;
    /// this will just return the first one found.
    /// </summary>
    /// <param name="key">The keycode to convert.</param>
    /// <returns>The scancode that corresponds to the given keycode.</returns>
    public static Scancode GetScancodeFromKey(Keycode key)
    {
        return (Scancode)SDL_GetScancodeFromKey((SDL_Keycode)key, null);
    }

    /// <summary>
    /// Gets the scancode corresponding to the given keycode according to the current keyboard layout,
    /// along with the modifier state needed.
    /// </summary>
    /// <param name="key">The keycode to convert.</param>
    /// <param name="modifiers">The modifier state that would be used when the scancode generates this key.</param>
    /// <returns>The scancode that corresponds to the given keycode.</returns>
    public static Scancode GetScancodeFromKey(Keycode key, out KeyModifiers modifiers)
    {
        SDL_Keymod mod;
        SDL_Scancode scancode = SDL_GetScancodeFromKey((SDL_Keycode)key, &mod);
        modifiers = (KeyModifiers)mod;
        return (Scancode)scancode;
    }

    /// <summary>
    /// Sets a human-readable name for a scancode.
    /// </summary>
    /// <param name="scancode">The scancode to name.</param>
    /// <param name="name">The name to use for the scancode. The string must remain valid while SDL is being used.</param>
    public static void SetScancodeName(Scancode scancode, string name)
    {
        _ = CheckErrorBool(SDL_SetScancodeName((SDL_Scancode)scancode, name));
    }

    /// <summary>
    /// Gets a human-readable name for a scancode.
    /// Warning: The returned name is by design not stable across platforms.
    /// </summary>
    /// <param name="scancode">The scancode to query.</param>
    /// <returns>The name for the scancode, or an empty string if it doesn't have a name.</returns>
    public static string GetScancodeName(Scancode scancode)
    {
        return SDL_GetScancodeName((SDL_Scancode)scancode);
    }

    /// <summary>
    /// Gets a scancode from a human-readable name.
    /// </summary>
    /// <param name="name">The human-readable scancode name.</param>
    /// <returns>The scancode, or Unknown if the name wasn't recognized.</returns>
    public static Scancode GetScancodeFromName(string name)
    {
        return (Scancode)SDL_GetScancodeFromName(name);
    }

    /// <summary>
    /// Gets a human-readable name for a keycode.
    /// Letters will be presented in their uppercase form, if applicable.
    /// </summary>
    /// <param name="key">The keycode to query.</param>
    /// <returns>The name for the keycode, or an empty string if it doesn't have a name.</returns>
    public static string GetKeyName(Keycode key)
    {
        return SDL_GetKeyName((SDL_Keycode)key);
    }

    /// <summary>
    /// Gets a keycode from a human-readable name.
    /// </summary>
    /// <param name="name">The human-readable key name.</param>
    /// <returns>The keycode, or Unknown if the name wasn't recognized.</returns>
    public static Keycode GetKeyFromName(string name)
    {
        return (Keycode)SDL_GetKeyFromName(name);
    }

    /// <summary>
    /// Gets whether the platform has screen keyboard support.
    /// </summary>
    public static bool HasScreenKeyboardSupport => SDL_HasScreenKeyboardSupport();
}
