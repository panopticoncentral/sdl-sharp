namespace SdlSharp.Gui;

/// <summary>Flags for <see cref="Gui.IsWindowFocused"/>.</summary>
[Flags]
public enum FocusedFlags
{
    /// <summary>No flags.</summary>
    None = 0,
    /// <summary>Return true if any children of the window is focused.</summary>
    ChildWindows = 1 << 0,
    /// <summary>Test from root window (top most parent of the current hierarchy).</summary>
    RootWindow = 1 << 1,
    /// <summary>Return true if any window is focused.</summary>
    AnyWindow = 1 << 2,
    /// <summary>Do not consider popup hierarchy.</summary>
    NoPopupHierarchy = 1 << 3,
    /// <summary>Combination: RootWindow | ChildWindows.</summary>
    RootAndChildWindows = RootWindow | ChildWindows,
}
