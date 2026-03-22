// src/SdlSharp/Input/Gamepad.cs
using System.Runtime.InteropServices;

using static SdlSharp.Native.Common;
using static SdlSharp.Native.Gamepad;

namespace SdlSharp.Input;

/// <summary>
/// Managed wrapper for an SDL gamepad device.
/// </summary>
public sealed unsafe class Gamepad : IDisposable
{
    private readonly bool _ownsHandle;

    internal Native.SDL_Gamepad* Handle { get; private set; }

    internal Gamepad(Native.SDL_Gamepad* handle, bool ownsHandle = true)
    {
        Handle = handle;
        _ownsHandle = ownsHandle;
    }

    /// <summary>Opens a gamepad by joystick instance ID.</summary>
    public static Gamepad Open(uint id) =>
        new(Check(SDL_OpenGamepad(new Native.SDL_JoystickID(id))));

    /// <summary>Gets the joystick instance IDs of all connected gamepads.</summary>
    public static uint[] GetDevices()
    {
        int count;
        var ids = SDL_GetGamepads(&count);
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

    /// <summary>Returns whether the given joystick instance ID is a gamepad.</summary>
    public static bool IsGamepad(uint joystickId) =>
        SDL_IsGamepad(new Native.SDL_JoystickID(joystickId));

    /// <summary>Gets the name of a gamepad by joystick instance ID.</summary>
    public static string? GetName(uint id) =>
        Marshal.PtrToStringUTF8((nint)SDL_GetGamepadNameForID(new Native.SDL_JoystickID(id)));

    /// <summary>Gets the type of a gamepad by joystick instance ID.</summary>
    public static GamepadType GetType(uint id) =>
        (GamepadType)SDL_GetGamepadTypeForID(new Native.SDL_JoystickID(id));

    /// <summary>Gets the name of this gamepad.</summary>
    public string? Name => Marshal.PtrToStringUTF8((nint)SDL_GetGamepadName(Handle));

    /// <summary>Gets the type of this gamepad.</summary>
    public GamepadType Type => (GamepadType)SDL_GetGamepadType(Handle);

    /// <summary>Gets the current value of an axis.</summary>
    public short GetAxis(GamepadAxis axis) =>
        SDL_GetGamepadAxis(Handle, (Native.SDL_GamepadAxis)axis);

    /// <summary>Gets the current state of a button.</summary>
    public bool GetButton(GamepadButton button) =>
        SDL_GetGamepadButton(Handle, (Native.SDL_GamepadButton)button);

    /// <summary>Gets the label for a button on this gamepad.</summary>
    public GamepadButtonLabel GetButtonLabel(GamepadButton button) =>
        (GamepadButtonLabel)SDL_GetGamepadButtonLabel(Handle, (Native.SDL_GamepadButton)button);

    /// <summary>Gets the connection state of this gamepad.</summary>
    public JoystickConnectionState ConnectionState =>
        (JoystickConnectionState)SDL_GetGamepadConnectionState(Handle);

    /// <summary>Updates the state of all open gamepads.</summary>
    public static void Update() => SDL_UpdateGamepads();

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_ownsHandle && Handle != null)
        {
            SDL_CloseGamepad(Handle);
            Handle = null;
        }
    }
}
