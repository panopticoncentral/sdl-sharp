namespace SdlSharp.ImGui;

/// <summary>Flags stored in <see cref="Viewport.Flags"/>.</summary>
[Flags]
public enum ViewportFlags
{
    /// <summary>No flags.</summary>
    None = 0,
    /// <summary>Represents a platform window.</summary>
    IsPlatformWindow = 1 << 0,
    /// <summary>Represents a platform monitor (unused yet).</summary>
    IsPlatformMonitor = 1 << 1,
    /// <summary>Platform window is created/managed by the application (rather than a dear imgui backend).</summary>
    OwnedByApp = 1 << 2,
}
