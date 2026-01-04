namespace Sdl3Sharp.ImGui;

/// <summary>
/// Represents a color.
/// </summary>
public unsafe readonly record struct ColorRGB(float Red, float Green, float Blue)
{
    public static implicit operator ColorRGB((float R, float G, float B) tuple)
    {
        return new(tuple.R, tuple.G, tuple.B);
    }

    /// <inheritdoc />
    public override string ToString()
    {
        // Return the color in RGBA format like 0xRRGGBBAA
        return $"0x{(byte)(Red * 255):X2}{(byte)(Green * 255):X2}{(byte)(Blue * 255):X2}";
    }
}
