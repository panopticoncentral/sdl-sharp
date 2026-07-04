namespace SdlSharp;

/// <summary>
/// The sandbox environment the process runs in, if any.
/// </summary>
public enum SandboxEnvironment
{
    /// <summary>Not sandboxed.</summary>
    None = Native.SDL_Sandbox.SDL_SANDBOX_NONE,
    /// <summary>Sandboxed in an unrecognized container.</summary>
    UnknownContainer = Native.SDL_Sandbox.SDL_SANDBOX_UNKNOWN_CONTAINER,
    /// <summary>Running inside Flatpak.</summary>
    Flatpak = Native.SDL_Sandbox.SDL_SANDBOX_FLATPAK,
    /// <summary>Running inside Snap.</summary>
    Snap = Native.SDL_Sandbox.SDL_SANDBOX_SNAP,
    /// <summary>Running inside the macOS App Sandbox.</summary>
    MacOS = Native.SDL_Sandbox.SDL_SANDBOX_MACOS,
}
