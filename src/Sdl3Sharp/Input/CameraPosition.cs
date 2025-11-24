namespace Sdl3Sharp.Input;

/// <summary>
/// The position of a camera in relation to the system device.
/// </summary>
public enum CameraPosition
{
    /// <summary>
    /// Unknown camera position.
    /// </summary>
    Unknown,

    /// <summary>
    /// Camera is on the front of the device (facing the user, for taking "selfies").
    /// </summary>
    FrontFacing,

    /// <summary>
    /// Camera is on the back of the device (for filming in the direction the user is facing).
    /// </summary>
    BackFacing
}
