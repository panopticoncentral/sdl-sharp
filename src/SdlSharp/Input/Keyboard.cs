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
    /// <summary>Gets a snapshot of connected keyboard instance IDs.</summary>
    public static uint[] GetDevices()
    {
        int count;
        var ids = Check(SDL_GetKeyboards(&count));
        try { return new ReadOnlySpan<uint>(ids, count).ToArray(); }
        finally { SDL_free(ids); }
    }
    /// <summary>Gets a connected keyboard's name.</summary>
    public static string? GetName(uint id) => Marshal.PtrToStringUTF8((nint)SDL_GetKeyboardNameForID(id));
    /// <summary>Gets a borrowed view of the keyboard focus window.</summary>
    public static Graphics.Window? Focus
    {
        get { var handle = SDL_GetKeyboardFocus(); return handle == null ? null : new Graphics.Window(handle, false); }
    }
    /// <summary>Translates a key to its scancode and required modifiers.</summary>
    public static Scancode GetScancodeFromKey(Keycode key, out KeyModifiers modifiers)
    {
        Native.SDL_Keymod native;
        var code = SDL_GetScancodeFromKey((Native.SDL_Keycode)key, &native);
        modifiers = (KeyModifiers)native;
        return (Scancode)code;
    }
    /// <summary>Looks up a scancode by name.</summary>
    public static Scancode GetScancodeFromName(string name) => (Scancode)SDL_GetScancodeFromName(ToUtf8(name));
    /// <summary>Looks up a key by name.</summary>
    public static Keycode GetKeyFromName(string name) => (Keycode)SDL_GetKeyFromName(ToUtf8(name));
    /// <summary>Returns whether the on-screen keyboard is visible for a window.</summary>
    public static bool IsScreenKeyboardShown(Graphics.Window window) => SDL_ScreenKeyboardShown(window.Handle);

    private static readonly Dictionary<Scancode, GCHandle> Names = [];
    /// <summary>Overrides a scancode name until application shutdown. Call on the main thread.</summary>
    public static void SetScancodeName(Scancode scancode, string name)
    {
        ArgumentNullException.ThrowIfNull(name);
        if (Application.CurrentSession == null) throw new InvalidOperationException("Initialize SDL first.");
        if (!Application.IsMainThread) throw new InvalidOperationException("Set scancode names on the main thread.");
        var bytes = ToUtf8(name)!;
        var pin = GCHandle.Alloc(bytes, GCHandleType.Pinned);
        try { Check(SDL_SetScancodeName((Native.SDL_Scancode)scancode, bytes)); }
        catch { pin.Free(); throw; }
        if (Names.Remove(scancode, out var old)) old.Free();
        Names[scancode] = pin;
    }
    internal static void ReleaseNames()
    {
        foreach (var pin in Names.Values) pin.Free();
        Names.Clear();
    }
}
