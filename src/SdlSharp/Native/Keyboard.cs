using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace SdlSharp.Native;

/// <summary>
/// Text input type for SDL_PROP_TEXTINPUT_TYPE_NUMBER.
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
    SDL_TEXTINPUT_TYPE_NUMBER_PASSWORD_VISIBLE,
}

/// <summary>
/// Auto capitalization type for SDL_PROP_TEXTINPUT_CAPITALIZATION_NUMBER.
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
    SDL_CAPITALIZE_LETTERS,
}

/// <summary>
/// Native bindings for SDL_keyboard.h.
/// </summary>
[SuppressMessage("Interoperability", "CA1401:P/Invokes should not be visible")]
public static partial class Keyboard
{
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_HasKeyboard")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HasKeyboard();

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetKeyboards")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial uint* SDL_GetKeyboards(int* count);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetKeyboardNameForID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* SDL_GetKeyboardNameForID(uint instance_id);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetKeyboardFocus")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Window* SDL_GetKeyboardFocus();

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetKeyboardState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* SDL_GetKeyboardState(int* numkeys);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_ResetKeyboard")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_ResetKeyboard();

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetModState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Keymod SDL_GetModState();

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetModState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_SetModState(SDL_Keymod modstate);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetKeyFromScancode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Keycode SDL_GetKeyFromScancode(SDL_Scancode scancode, SDL_Keymod modstate, [MarshalAs(UnmanagedType.U1)] bool key_event);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetScancodeFromKey")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Scancode SDL_GetScancodeFromKey(SDL_Keycode key, SDL_Keymod* modstate);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetScancodeName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetScancodeName(SDL_Scancode scancode, ReadOnlySpan<byte> name);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetScancodeName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* SDL_GetScancodeName(SDL_Scancode scancode);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetScancodeFromName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Scancode SDL_GetScancodeFromName(ReadOnlySpan<byte> name);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetKeyName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* SDL_GetKeyName(SDL_Keycode key);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetKeyFromName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Keycode SDL_GetKeyFromName(ReadOnlySpan<byte> name);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_StartTextInput")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_StartTextInput(SDL_Window* window);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_TextInputActive")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_TextInputActive(SDL_Window* window);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_StopTextInput")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_StopTextInput(SDL_Window* window);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_StartTextInputWithProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_StartTextInputWithProperties(SDL_Window* window, SDL_PropertiesID props);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetTextInputArea")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetTextInputArea(SDL_Window* window, SDL_Rect* rect, int cursor);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetTextInputArea")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GetTextInputArea(SDL_Window* window, SDL_Rect* rect, int* cursor);

    /// <summary>Property: an SDL_TextInputType describing the text being input.</summary>
    public const string SDL_PROP_TEXTINPUT_TYPE_NUMBER = "SDL.textinput.type";

    /// <summary>Property: an SDL_Capitalization describing how text should be capitalized.</summary>
    public const string SDL_PROP_TEXTINPUT_CAPITALIZATION_NUMBER = "SDL.textinput.capitalization";

    /// <summary>Property: true to enable auto completion and auto correction.</summary>
    public const string SDL_PROP_TEXTINPUT_AUTOCORRECT_BOOLEAN = "SDL.textinput.autocorrect";

    /// <summary>Property: true if multiple lines of text are allowed.</summary>
    public const string SDL_PROP_TEXTINPUT_MULTILINE_BOOLEAN = "SDL.textinput.multiline";

    // Skipped: SDL_PROP_TEXTINPUT_ANDROID_INPUTTYPE_NUMBER (platform-specific).

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_ClearComposition")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_ClearComposition(SDL_Window* window);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_HasScreenKeyboardSupport")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HasScreenKeyboardSupport();

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_ScreenKeyboardShown")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_ScreenKeyboardShown(SDL_Window* window);
}
