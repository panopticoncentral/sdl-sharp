using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Keycode;
using static Sdl3Sharp.Native.Properties;
using static Sdl3Sharp.Native.Rect;
using static Sdl3Sharp.Native.Scancode;
using static Sdl3Sharp.Native.Video;

// We are intentionally exposing the P/Invoke calls so people can do low-level calls if needed
#pragma warning disable CA1401 // P/Invokes should not be visible

namespace Sdl3Sharp.Native;

/// <summary>
/// P/Invoke bindings for SDL_keyboard.h - Keyboard input and text input management.
/// </summary>
/// <remarks>
/// Please refer to the Best Keyboard Practices document for details on how best to accept keyboard input:
/// https://wiki.libsdl.org/SDL3/BestKeyboardPractices
/// </remarks>
public static unsafe partial class Keyboard
{
    /// <summary>
    /// This is a unique ID for a keyboard for the time it is connected to the system,
    /// and is never reused for the lifetime of the application.
    /// If the keyboard is disconnected and reconnected, it will get a new ID.
    /// The value 0 is an invalid ID.
    /// </summary>
    /// <param name="value">The keyboard ID value.</param>
    public readonly struct SDL_KeyboardID(uint value)
    {
        /// <summary>The underlying keyboard ID value.</summary>
        public readonly uint Value = value;

        /// <summary>Implicitly converts an SDL_KeyboardID to uint.</summary>
        /// <param name="id">The keyboard ID to convert.</param>
        public static implicit operator uint(SDL_KeyboardID id)
        {
            return id.Value;
        }

        /// <summary>Implicitly converts a uint to SDL_KeyboardID.</summary>
        /// <param name="value">The value to convert.</param>
        public static implicit operator SDL_KeyboardID(uint value)
        {
            return new(value);
        }
    }

    /// <summary>
    /// Text input type.
    /// These are the valid values for SDL_PROP_TEXTINPUT_TYPE_NUMBER.
    /// Not every value is valid on every platform, but where a value isn't supported, a reasonable fallback will be used.
    /// </summary>
    public enum SDL_TextInputType
    {
        /// <summary>The input is text.</summary>
        SDL_TEXTINPUT_TYPE_TEXT,
        /// <summary>The input is a person's name.</summary>
        SDL_TEXTINPUT_TYPE_TEXT_NAME,
        /// <summary>The input is an e-mail address.</summary>
        SDL_TEXTINPUT_TYPE_TEXT_EMAIL,
        /// <summary>The input is a username.</summary>
        SDL_TEXTINPUT_TYPE_TEXT_USERNAME,
        /// <summary>The input is a secure password that is hidden.</summary>
        SDL_TEXTINPUT_TYPE_TEXT_PASSWORD_HIDDEN,
        /// <summary>The input is a secure password that is visible.</summary>
        SDL_TEXTINPUT_TYPE_TEXT_PASSWORD_VISIBLE,
        /// <summary>The input is a number.</summary>
        SDL_TEXTINPUT_TYPE_NUMBER,
        /// <summary>The input is a secure PIN that is hidden.</summary>
        SDL_TEXTINPUT_TYPE_NUMBER_PASSWORD_HIDDEN,
        /// <summary>The input is a secure PIN that is visible.</summary>
        SDL_TEXTINPUT_TYPE_NUMBER_PASSWORD_VISIBLE
    }

    /// <summary>
    /// Auto capitalization type.
    /// These are the valid values for SDL_PROP_TEXTINPUT_CAPITALIZATION_NUMBER.
    /// Not every value is valid on every platform, but where a value isn't supported, a reasonable fallback will be used.
    /// </summary>
    public enum SDL_Capitalization
    {
        /// <summary>No auto-capitalization will be done.</summary>
        SDL_CAPITALIZE_NONE,
        /// <summary>The first letter of sentences will be capitalized.</summary>
        SDL_CAPITALIZE_SENTENCES,
        /// <summary>The first letter of words will be capitalized.</summary>
        SDL_CAPITALIZE_WORDS,
        /// <summary>All letters will be capitalized.</summary>
        SDL_CAPITALIZE_LETTERS
    }

    /// <summary>Property name for text input type (SDL_TextInputType value).</summary>
    public const string SDL_PROP_TEXTINPUT_TYPE_NUMBER = "SDL.textinput.type";
    /// <summary>Property name for text input capitalization (SDL_Capitalization value).</summary>
    public const string SDL_PROP_TEXTINPUT_CAPITALIZATION_NUMBER = "SDL.textinput.capitalization";
    /// <summary>Property name for text input autocorrect (boolean).</summary>
    public const string SDL_PROP_TEXTINPUT_AUTOCORRECT_BOOLEAN = "SDL.textinput.autocorrect";
    /// <summary>Property name for text input multiline (boolean).</summary>
    public const string SDL_PROP_TEXTINPUT_MULTILINE_BOOLEAN = "SDL.textinput.multiline";
    /// <summary>Property name for Android-specific text input type (integer).</summary>
    public const string SDL_PROP_TEXTINPUT_ANDROID_INPUTTYPE_NUMBER = "SDL.textinput.android.inputtype";

    /// <summary>
    /// Return whether a keyboard is currently connected.
    /// </summary>
    /// <returns>True if a keyboard is connected, false otherwise.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HasKeyboard();

    /// <summary>
    /// Get a list of currently connected keyboards.
    /// Note that this will include any device or virtual driver that includes keyboard functionality,
    /// including some mice, KVM switches, motherboard power buttons, etc.
    /// </summary>
    /// <param name="count">A pointer filled in with the number of keyboards returned, may be NULL.</param>
    /// <returns>A 0 terminated array of keyboard instance IDs or NULL on failure. This should be freed with SDL_free() when it is no longer needed.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_KeyboardID* SDL_GetKeyboards(int* count);

    /// <summary>
    /// Get the name of a keyboard.
    /// This function returns "" if the keyboard doesn't have a name.
    /// </summary>
    /// <param name="instance_id">The keyboard instance ID.</param>
    /// <returns>The name of the selected keyboard or NULL on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(Utf8StringMarshaller))]
    public static partial string? SDL_GetKeyboardNameForID(SDL_KeyboardID instance_id);

    /// <summary>
    /// Query the window which currently has keyboard focus.
    /// </summary>
    /// <returns>The window with keyboard focus.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Window* SDL_GetKeyboardFocus();

    /// <summary>
    /// Get a snapshot of the current state of the keyboard.
    /// The pointer returned is a pointer to an internal SDL array. It will be valid for the whole
    /// lifetime of the application and should not be freed by the caller.
    /// A array element with a value of true means that the key is pressed and a value of false means that it is not.
    /// Indexes into this array are obtained by using SDL_Scancode values.
    /// Use SDL_PumpEvents() to update the state array.
    /// </summary>
    /// <param name="numkeys">If non-NULL, receives the length of the returned array.</param>
    /// <returns>A pointer to an array of key states.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial bool* SDL_GetKeyboardState(int* numkeys);

    /// <summary>
    /// Clear the state of the keyboard.
    /// This function will generate key up events for all pressed keys.
    /// </summary>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_ResetKeyboard();

    /// <summary>
    /// Get the current key modifier state for the keyboard.
    /// </summary>
    /// <returns>An OR'd combination of the modifier keys for the keyboard.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Keymod SDL_GetModState();

    /// <summary>
    /// Set the current key modifier state for the keyboard.
    /// The inverse of SDL_GetModState(), SDL_SetModState() allows you to impose
    /// modifier key states on your application. This does not change the keyboard state,
    /// only the key modifier flags that SDL reports.
    /// </summary>
    /// <param name="modstate">The desired SDL_Keymod for the keyboard.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_SetModState(SDL_Keymod modstate);

    /// <summary>
    /// Get the key code corresponding to the given scancode according to the current keyboard layout.
    /// If you want to get the keycode as it would be delivered in key events, including options specified
    /// in SDL_HINT_KEYCODE_OPTIONS, then you should pass key_event as true.
    /// Otherwise this function simply translates the scancode based on the given modifier state.
    /// </summary>
    /// <param name="scancode">The desired SDL_Scancode to query.</param>
    /// <param name="modstate">The modifier state to use when translating the scancode to a keycode.</param>
    /// <param name="key_event">True if the keycode will be used in key events.</param>
    /// <returns>The SDL_Keycode that corresponds to the given SDL_Scancode.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Keycode SDL_GetKeyFromScancode(SDL_Scancode scancode, SDL_Keymod modstate, [MarshalAs(UnmanagedType.U1)] bool key_event);

    /// <summary>
    /// Get the scancode corresponding to the given key code according to the current keyboard layout.
    /// Note that there may be multiple scancode+modifier states that can generate this keycode,
    /// this will just return the first one found.
    /// </summary>
    /// <param name="key">The desired SDL_Keycode to query.</param>
    /// <param name="modstate">A pointer to the modifier state that would be used when the scancode generates this key, may be NULL.</param>
    /// <returns>The SDL_Scancode that corresponds to the given SDL_Keycode.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Scancode SDL_GetScancodeFromKey(SDL_Keycode key, SDL_Keymod* modstate);

    /// <summary>
    /// Set a human-readable name for a scancode.
    /// </summary>
    /// <param name="scancode">The desired SDL_Scancode.</param>
    /// <param name="name">The name to use for the scancode, encoded as UTF-8. The string is not copied, so the pointer given to this function must stay valid while SDL is being used.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetScancodeName(SDL_Scancode scancode, [MarshalUsing(typeof(Utf8StringMarshaller))] string name);

    /// <summary>
    /// Get a human-readable name for a scancode.
    /// Warning: The returned name is by design not stable across platforms.
    /// </summary>
    /// <param name="scancode">The desired SDL_Scancode to query.</param>
    /// <returns>A pointer to the name for the scancode. If the scancode doesn't have a name this function returns an empty string ("").</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(Utf8StringMarshaller))]
    public static partial string SDL_GetScancodeName(SDL_Scancode scancode);

    /// <summary>
    /// Get a scancode from a human-readable name.
    /// </summary>
    /// <param name="name">The human-readable scancode name.</param>
    /// <returns>The SDL_Scancode, or SDL_SCANCODE_UNKNOWN if the name wasn't recognized.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Scancode SDL_GetScancodeFromName([MarshalUsing(typeof(Utf8StringMarshaller))] string name);

    /// <summary>
    /// Get a human-readable name for a key.
    /// If the key doesn't have a name, this function returns an empty string ("").
    /// Letters will be presented in their uppercase form, if applicable.
    /// </summary>
    /// <param name="key">The desired SDL_Keycode to query.</param>
    /// <returns>A UTF-8 encoded string of the key name.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(Utf8StringMarshaller))]
    public static partial string SDL_GetKeyName(SDL_Keycode key);

    /// <summary>
    /// Get a key code from a human-readable name.
    /// </summary>
    /// <param name="name">The human-readable key name.</param>
    /// <returns>The key code, or SDLK_UNKNOWN if the name wasn't recognized.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Keycode SDL_GetKeyFromName([MarshalUsing(typeof(Utf8StringMarshaller))] string name);

    /// <summary>
    /// Start accepting Unicode text input events in a window.
    /// This function will enable text input (SDL_EVENT_TEXT_INPUT and SDL_EVENT_TEXT_EDITING events) in the specified window.
    /// Text input events are not received by default.
    /// On some platforms using this function shows the screen keyboard and/or activates an IME.
    /// </summary>
    /// <param name="window">The window to enable text input.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_StartTextInput(SDL_Window* window);

    /// <summary>
    /// Start accepting Unicode text input events in a window, with properties describing the input.
    /// This function will enable text input (SDL_EVENT_TEXT_INPUT and SDL_EVENT_TEXT_EDITING events) in the specified window.
    /// Text input events are not received by default.
    /// </summary>
    /// <param name="window">The window to enable text input.</param>
    /// <param name="props">The properties to use.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_StartTextInputWithProperties(SDL_Window* window, SDL_PropertiesID props);

    /// <summary>
    /// Check whether or not Unicode text input events are enabled for a window.
    /// </summary>
    /// <param name="window">The window to check.</param>
    /// <returns>True if text input events are enabled else false.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_TextInputActive(SDL_Window* window);

    /// <summary>
    /// Stop receiving any text input events in a window.
    /// If SDL_StartTextInput() showed the screen keyboard, this function will hide it.
    /// </summary>
    /// <param name="window">The window to disable text input.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_StopTextInput(SDL_Window* window);

    /// <summary>
    /// Dismiss the composition window/IME without disabling the subsystem.
    /// </summary>
    /// <param name="window">The window to affect.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_ClearComposition(SDL_Window* window);

    /// <summary>
    /// Set the area used to type Unicode text input.
    /// Native input methods may place a window with word suggestions near the cursor, without covering the text being entered.
    /// </summary>
    /// <param name="window">The window for which to set the text input area.</param>
    /// <param name="rect">The SDL_Rect representing the text input area, in window coordinates, or NULL to clear it.</param>
    /// <param name="cursor">The offset of the current cursor location relative to rect->x, in window coordinates.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetTextInputArea(SDL_Window* window, SDL_Rect* rect, int cursor);

    /// <summary>
    /// Get the area used to type Unicode text input.
    /// This returns the values previously set by SDL_SetTextInputArea().
    /// </summary>
    /// <param name="window">The window for which to query the text input area.</param>
    /// <param name="rect">A pointer to an SDL_Rect filled in with the text input area, may be NULL.</param>
    /// <param name="cursor">A pointer to the offset of the current cursor location relative to rect->x, may be NULL.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetTextInputArea(SDL_Window* window, SDL_Rect* rect, int* cursor);

    /// <summary>
    /// Check whether the platform has screen keyboard support.
    /// </summary>
    /// <returns>True if the platform has some screen keyboard support or false if not.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HasScreenKeyboardSupport();

    /// <summary>
    /// Check whether the screen keyboard is shown for given window.
    /// </summary>
    /// <param name="window">The window for which screen keyboard should be queried.</param>
    /// <returns>True if screen keyboard is shown or false if not.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_ScreenKeyboardShown(SDL_Window* window);
}
