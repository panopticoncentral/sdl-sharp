namespace Sdl3Sharp.Input;

/// <summary>
/// Represents the permission state for camera access.
/// </summary>
public enum CameraPermissionState
{
    /// <summary>
    /// The user denied access to the camera.
    /// </summary>
    Denied = -1,

    /// <summary>
    /// No decision has been made yet. The user has not yet approved or denied access.
    /// </summary>
    Pending = 0,

    /// <summary>
    /// The user approved access to the camera.
    /// </summary>
    Approved = 1
}
