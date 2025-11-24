using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Gamepad;
using static Sdl3Sharp.Native.Joystick;
using static Sdl3Sharp.Native.StdInc;

namespace Sdl3Sharp.Input;

/// <summary>
/// Describes a gamepad that is connected to the system but not yet opened.
/// Use <see cref="Open"/> to open the gamepad for use.
/// </summary>
/// <param name="id">The underlying SDL joystick ID.</param>
public readonly unsafe struct GamepadDescriptor(SDL_JoystickID id)
{
    /// <summary>
    /// Gets the underlying SDL joystick ID.
    /// </summary>
    public SDL_JoystickID Id { get; } = id;

    /// <summary>
    /// Gets the implementation dependent name of this gamepad.
    /// </summary>
    public string Name => CheckErrorNull(SDL_GetGamepadNameForID(Id));

    /// <summary>
    /// Gets the implementation dependent path of this gamepad.
    /// </summary>
    public string Path => CheckErrorNull(SDL_GetGamepadPathForID(Id));

    /// <summary>
    /// Gets the player index of this gamepad, or -1 if not available.
    /// </summary>
    public int PlayerIndex => SDL_GetGamepadPlayerIndexForID(Id);

    /// <summary>
    /// Gets the implementation-dependent GUID of this gamepad.
    /// Returns a zero GUID if called with an invalid instance ID.
    /// </summary>
    public Guid Guid => SDL_GetGamepadGUIDForID(Id);

    /// <summary>
    /// Gets the USB vendor ID of this gamepad, or 0 if not available.
    /// </summary>
    public ushort VendorId => SDL_GetGamepadVendorForID(Id);

    /// <summary>
    /// Gets the USB product ID of this gamepad, or 0 if not available.
    /// </summary>
    public ushort ProductId => SDL_GetGamepadProductForID(Id);

    /// <summary>
    /// Gets the product version of this gamepad, or 0 if not available.
    /// </summary>
    public ushort ProductVersion => SDL_GetGamepadProductVersionForID(Id);

    /// <summary>
    /// Gets the type of this gamepad.
    /// </summary>
    public GamepadType Type => (GamepadType)SDL_GetGamepadTypeForID(Id);

    /// <summary>
    /// Gets the real type of this gamepad, ignoring any mapping override.
    /// </summary>
    public GamepadType RealType => (GamepadType)SDL_GetRealGamepadTypeForID(Id);

    /// <summary>
    /// Gets the mapping string for this gamepad, or null if not available.
    /// </summary>
    public string? Mapping
    {
        get
        {
            var mapping = SDL_GetGamepadMappingForID(Id);
            if (mapping is null)
            {
                return null;
            }

            try
            {
                return System.Runtime.InteropServices.Marshal.PtrToStringUTF8((nint)mapping);
            }
            finally
            {
                SDL_free(mapping);
            }
        }
    }

    /// <summary>
    /// Opens this gamepad for use.
    /// </summary>
    /// <returns>A new Gamepad instance.</returns>
    public Gamepad Open()
    {
        return new(CheckErrorPointer(SDL_OpenGamepad(Id)), ownsHandle: true);
    }

    /// <summary>
    /// Gets the Gamepad associated with this descriptor, if it has been opened.
    /// </summary>
    /// <returns>A Gamepad instance if opened.</returns>
    public Gamepad GetOpened()
    {
        return new(CheckErrorPointer(SDL_GetGamepadFromID(Id)), ownsHandle: false);
    }

    /// <summary>
    /// Sets the mapping for this gamepad.
    /// </summary>
    /// <param name="mapping">The mapping to use for this device, or null to clear the mapping.</param>
    public void SetMapping(string? mapping)
    {
        _ = CheckErrorBool(SDL_SetGamepadMapping(Id, mapping));
    }

    /// <summary>
    /// Implicitly converts an SDL_JoystickID to a GamepadDescriptor.
    /// </summary>
    /// <param name="id">The joystick ID to convert.</param>
    public static implicit operator GamepadDescriptor(SDL_JoystickID id)
    {
        return new(id);
    }

    /// <summary>
    /// Implicitly converts a GamepadDescriptor to an SDL_JoystickID.
    /// </summary>
    /// <param name="descriptor">The descriptor to convert.</param>
    public static implicit operator SDL_JoystickID(GamepadDescriptor descriptor)
    {
        return descriptor.Id;
    }
}
