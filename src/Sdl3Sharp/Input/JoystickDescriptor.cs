using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Joystick;

namespace Sdl3Sharp.Input;

/// <summary>
/// Describes a joystick that is connected to the system but not yet opened.
/// Use <see cref="Open"/> to open the joystick for use.
/// </summary>
/// <param name="id">The underlying SDL joystick ID.</param>
public readonly unsafe struct JoystickDescriptor(SDL_JoystickID id)
{
    /// <summary>
    /// Gets the underlying SDL joystick ID.
    /// </summary>
    public SDL_JoystickID Id { get; } = id;

    /// <summary>
    /// Gets the implementation dependent name of this joystick.
    /// </summary>
    public string? Name => SDL_GetJoystickNameForID(Id);

    /// <summary>
    /// Gets the implementation dependent path of this joystick.
    /// </summary>
    public string? Path => SDL_GetJoystickPathForID(Id);

    /// <summary>
    /// Gets the player index of this joystick, or -1 if not available.
    /// </summary>
    public int PlayerIndex => SDL_GetJoystickPlayerIndexForID(Id);

    /// <summary>
    /// Gets the implementation-dependent GUID of this joystick.
    /// Returns a zero GUID if called with an invalid instance ID.
    /// </summary>
    public Guid Guid => SDL_GetJoystickGUIDForID(Id);

    /// <summary>
    /// Gets the USB vendor ID of this joystick, or 0 if not available.
    /// </summary>
    public ushort VendorId => SDL_GetJoystickVendorForID(Id);

    /// <summary>
    /// Gets the USB product ID of this joystick, or 0 if not available.
    /// </summary>
    public ushort ProductId => SDL_GetJoystickProductForID(Id);

    /// <summary>
    /// Gets the product version of this joystick, or 0 if not available.
    /// </summary>
    public ushort ProductVersion => SDL_GetJoystickProductVersionForID(Id);

    /// <summary>
    /// Gets the type of this joystick.
    /// </summary>
    public JoystickType Type => (JoystickType)SDL_GetJoystickTypeForID(Id);

    /// <summary>
    /// Gets whether this joystick is virtual.
    /// </summary>
    public bool IsVirtual => SDL_IsJoystickVirtual(Id);

    /// <summary>
    /// Opens this joystick for use.
    /// </summary>
    /// <returns>A new Joystick instance.</returns>
    public Joystick Open()
    {
        return new(CheckErrorPointer(SDL_OpenJoystick(Id)), ownsHandle: true);
    }

    /// <summary>
    /// Gets the Joystick associated with this descriptor, if it has been opened.
    /// </summary>
    /// <returns>A Joystick instance if opened, null otherwise.</returns>
    public Joystick? GetOpened()
    {
        SDL_Joystick* joystick = SDL_GetJoystickFromID(Id);
        return joystick != null ? new Joystick(joystick, ownsHandle: false) : null;
    }

    /// <summary>
    /// Detaches this virtual joystick.
    /// </summary>
    /// <exception cref="SdlException">Thrown if the joystick is not virtual or detach fails.</exception>
    public void DetachVirtual()
    {
        _ = CheckErrorBool(SDL_DetachVirtualJoystick(Id));
    }

    /// <summary>
    /// Implicitly converts an SDL_JoystickID to a JoystickDescriptor.
    /// </summary>
    /// <param name="id">The joystick ID to convert.</param>
    public static implicit operator JoystickDescriptor(SDL_JoystickID id)
    {
        return new(id);
    }

    /// <summary>
    /// Implicitly converts a JoystickDescriptor to an SDL_JoystickID.
    /// </summary>
    /// <param name="descriptor">The descriptor to convert.</param>
    public static implicit operator SDL_JoystickID(JoystickDescriptor descriptor)
    {
        return descriptor.Id;
    }
}
