namespace SdlSharp.Graphics;

/// <summary>
/// Camera position relative to the device.
/// </summary>
public enum CameraPosition
{
    /// <summary>Camera position is unknown.</summary>
    Unknown = (int)Native.SDL_CameraPosition.SDL_CAMERA_POSITION_UNKNOWN,
    /// <summary>Camera is front-facing (toward the user).</summary>
    FrontFacing = (int)Native.SDL_CameraPosition.SDL_CAMERA_POSITION_FRONT_FACING,
    /// <summary>Camera is back-facing (away from the user).</summary>
    BackFacing = (int)Native.SDL_CameraPosition.SDL_CAMERA_POSITION_BACK_FACING,
}
