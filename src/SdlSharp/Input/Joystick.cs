// src/SdlSharp/Input/Joystick.cs
using System.Runtime.InteropServices;

using static SdlSharp.Native.Common;
using static SdlSharp.Native.Joystick;

namespace SdlSharp.Input;

/// <summary>
/// Managed wrapper for an SDL joystick device.
/// </summary>
public sealed unsafe class Joystick : IDisposable
{
    private readonly bool _ownsHandle;

    internal Native.SDL_Joystick* Handle { get; private set; }

    internal Joystick(Native.SDL_Joystick* handle, bool ownsHandle = true)
    {
        Handle = handle;
        _ownsHandle = ownsHandle;
    }

    /// <summary>Opens a joystick by instance ID.</summary>
    public static Joystick Open(uint id) =>
        new(Check(SDL_OpenJoystick(new Native.SDL_JoystickID(id))));

    /// <summary>Gets the instance IDs of all connected joysticks.</summary>
    public static uint[] GetDevices()
    {
        int count;
        var ids = SDL_GetJoysticks(&count);
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

    /// <summary>Gets the name of a joystick by instance ID.</summary>
    public static string? GetName(uint id) =>
        Marshal.PtrToStringUTF8((nint)SDL_GetJoystickNameForID(new Native.SDL_JoystickID(id)));

    /// <summary>Gets the type of a joystick by instance ID.</summary>
    public static JoystickType GetType(uint id) =>
        (JoystickType)SDL_GetJoystickTypeForID(new Native.SDL_JoystickID(id));

    /// <summary>Gets the name of this joystick.</summary>
    public string? Name => Marshal.PtrToStringUTF8((nint)SDL_GetJoystickName(Handle));

    /// <summary>Gets the type of this joystick.</summary>
    public JoystickType Type => (JoystickType)SDL_GetJoystickType(Handle);

    /// <summary>Gets the number of axes.</summary>
    public int NumAxes => SDL_GetNumJoystickAxes(Handle);

    /// <summary>Gets the number of trackballs.</summary>
    public int NumBalls => SDL_GetNumJoystickBalls(Handle);

    /// <summary>Gets the number of hats.</summary>
    public int NumHats => SDL_GetNumJoystickHats(Handle);

    /// <summary>Gets the number of buttons.</summary>
    public int NumButtons => SDL_GetNumJoystickButtons(Handle);

    /// <summary>Gets the current value of an axis.</summary>
    public short GetAxis(int index) => SDL_GetJoystickAxis(Handle, index);

    /// <summary>Gets the current position of a hat.</summary>
    public HatPosition GetHat(int index) => (HatPosition)SDL_GetJoystickHat(Handle, index);

    /// <summary>Gets the current state of a button.</summary>
    public bool GetButton(int index) => SDL_GetJoystickButton(Handle, index);

    /// <summary>Gets the connection state of this joystick.</summary>
    public JoystickConnectionState ConnectionState =>
        (JoystickConnectionState)SDL_GetJoystickConnectionState(Handle);

    /// <summary>Updates the state of all open joysticks.</summary>
    public static void Update() => SDL_UpdateJoysticks();

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_ownsHandle && Handle != null)
        {
            SDL_CloseJoystick(Handle);
            Handle = null;
        }
    }
}
