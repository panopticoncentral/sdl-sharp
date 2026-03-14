using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace SdlSharp.Native;

// Skipped: SDL_TextInputType, SDL_Capitalization enums and SDL_StartTextInputWithProperties
// (text input properties — deferred, can add later if needed).
// Skipped: SDL_SetTextInputArea, SDL_GetTextInputArea (needs SDL_Rect pointer from Rect.cs).

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
