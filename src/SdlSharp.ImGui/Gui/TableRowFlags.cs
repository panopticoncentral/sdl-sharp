namespace SdlSharp.Gui;

/// <summary>Flags for <see cref="Gui.TableNextRow"/>.</summary>
[Flags]
public enum TableRowFlags
{
    /// <summary>No flags.</summary>
    None = 0,
    /// <summary>Identify header row.</summary>
    Headers = 1 << 0,
}
