namespace SdlSharp.Graphics;

/// <summary>
/// The user's response to a camera access request.
/// </summary>
public enum CameraPermissionState
{
    /// <summary>The user denied camera access.</summary>
    Denied = -1,
    /// <summary>The user has not yet responded.</summary>
    Waiting = 0,
    /// <summary>The user approved camera access.</summary>
    Approved = 1,
}
