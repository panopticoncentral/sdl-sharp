namespace SdlSharp.ImGui;

/// <summary>Flags for <see cref="ImGui.InvisibleButton(string, float, float, ButtonFlags)"/>.</summary>
[Flags]
public enum ButtonFlags
{
    /// <summary>No flags.</summary>
    None = 0,
    /// <summary>React on left mouse button (default).</summary>
    MouseButtonLeft = 1 << 0,
    /// <summary>React on right mouse button.</summary>
    MouseButtonRight = 1 << 1,
    /// <summary>React on middle mouse button.</summary>
    MouseButtonMiddle = 1 << 2,
    /// <summary>InvisibleButton: do not disable navigation/tabbing (disabled by default).</summary>
    EnableNav = 1 << 3,
    /// <summary>Hit testing will allow subsequent widgets to overlap this one.</summary>
    AllowOverlap = 1 << 12,
}
