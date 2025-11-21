namespace Sdl3Sharp.Input;

/// <summary>
/// Describes the type of a touch device.
/// </summary>
public enum TouchDeviceType
{
    /// <summary>Invalid touch device type.</summary>
    Invalid = -1,
    /// <summary>Touch screen with window-relative coordinates.</summary>
    Direct,
    /// <summary>Trackpad with absolute device coordinates.</summary>
    IndirectAbsolute,
    /// <summary>Trackpad with screen cursor-relative coordinates.</summary>
    IndirectRelative
}
