namespace SdlSharp.ImGui;

/// <summary>Flags for <see cref="ImGui.TableNextRow"/>.</summary>
[Flags]
public enum TableRowFlags
{
    /// <summary>No flags.</summary>
    None = 0,
    /// <summary>Identify header row.</summary>
    Headers = 1 << 0,
}
