namespace Sdl3Sharp.ImGui;

/// <summary>
/// Represents a color.
/// </summary>
public unsafe readonly record struct ColorPacked(uint Value)
{
    /// <summary>
    /// Gets the red component of the color.
    /// </summary>
    public byte Red => (byte)((Value >> 16) & 0xFF);

    /// <summary>
    /// Gets the green component of the color.
    /// </summary>
    public byte Green => (byte)((Value >> 8) & 0xFF);

    /// <summary>
    /// Gets the blue component of the color.
    /// </summary>
    public byte Blue => (byte)(Value & 0xFF);
}
