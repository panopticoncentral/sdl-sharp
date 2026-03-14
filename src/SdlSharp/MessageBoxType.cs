namespace SdlSharp;

/// <summary>
/// Type of message box to display.
/// </summary>
public enum MessageBoxType
{
    /// <summary>Error dialog.</summary>
    Error = (int)Native.SDL_MessageBoxFlags.SDL_MESSAGEBOX_ERROR,
    /// <summary>Warning dialog.</summary>
    Warning = (int)Native.SDL_MessageBoxFlags.SDL_MESSAGEBOX_WARNING,
    /// <summary>Informational dialog.</summary>
    Information = (int)Native.SDL_MessageBoxFlags.SDL_MESSAGEBOX_INFORMATION,
}
