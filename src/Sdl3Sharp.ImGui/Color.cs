using Sdl3Sharp.ImGui.Native;

using static Sdl3Sharp.ImGui.Native.ImGui;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Represents a color.
/// </summary>
public unsafe readonly record struct Color
{
    internal readonly ImVec4 Value { get; }

    /// <summary>
    /// The red component of the vector.
    /// </summary>
    public float Red => Value.X;

    /// <summary>
    /// The green component of the vector.
    /// </summary>
    public float Green => Value.Y;

    /// <summary>
    /// The blue component of the vector.
    /// </summary>
    public float Blue => Value.Z;

    /// <summary>
    /// The alpha component of the vector.
    /// </summary>
    public float Alpha => Value.W;

    /// <summary>
    /// Creates a new Color instance.
    /// </summary>
    /// <param name="red">The red component.</param>
    /// <param name="blue">The blue component.</param>
    /// <param name="green">The green component.</param>
    /// <param name="alpha">The alpha component.</param>
    public Color(float red, float green, float blue, float alpha)
    {
        Value = new ImVec4() { X = red, Y = green, Z = blue, W = alpha };
    }

    internal Color(ImVec4 value)
    {
        Value = value;
    }

    /// <summary>
    /// Creates a color from HSV (Hue, Saturation, Value) color space.
    /// </summary>
    /// <param name="h">The hue component.</param>
    /// <param name="s">The saturation component.</param>
    /// <param name="v">The value (brightness) component.</param>
    /// <param name="alpha">The alpha component.</param>
    public static Color FromHSV(float h, float s, float v, float alpha = 1.0f)
    {
        (var r, var g, var b) = ColorConvertHSVtoRGB(h, s, v);
        return new Color(r, g, b, alpha);
    }

    /// <summary>
    /// Gets a style color as a 32-bit packed value with an additional alpha multiplier.
    /// </summary>
    /// <param name="idx">The color index.</param>
    /// <param name="alphaMul">Additional alpha multiplier (0.0 to 1.0).</param>
    /// <returns>The color as a 32-bit packed RGBA value with style alpha and multiplier applied.</returns>
    public static uint GetColorU32(StyleColor idx, float alphaMul = 1.0f)
    {
        return ImGui_GetColorU32Ex((ImGuiCol)idx, alphaMul);
    }

    /// <summary>
    /// Gets a Vec4 color as a 32-bit packed value with style alpha applied.
    /// </summary>
    /// <param name="col">The color as a Vec4.</param>
    /// <returns>The color as a 32-bit packed RGBA value.</returns>
    public static uint GetColorU32(Color col)
    {
        return ImGui_GetColorU32ImVec4(col.Value);
    }

    /// <summary>
    /// Gets a 32-bit color with style alpha and an additional alpha multiplier applied.
    /// </summary>
    /// <param name="col">The color as a 32-bit packed RGBA value.</param>
    /// <param name="alphaMul">Additional alpha multiplier (0.0 to 1.0).</param>
    /// <returns>The color with style alpha and multiplier applied.</returns>
    public static uint GetColorU32(uint col, float alphaMul = 1.0f)
    {
        return ImGui_GetColorU32ImU32Ex(col, alphaMul);
    }

    /// <summary>
    /// Gets a style color as stored in the Style structure.
    /// </summary>
    /// <param name="idx">The color index.</param>
    /// <returns>The color as a Vec4.</returns>
    /// <remarks>
    /// Use this to feed back into <see cref="PushStyleColor(StyleColor, Vec4)"/>.
    /// Otherwise use <see cref="GetColorU32(StyleColor)"/> to get style color with style alpha baked in.
    /// </remarks>
    public static Color GetStyleColor(StyleColor idx)
    {
        return new(*ImGui_GetStyleColorVec4((ImGuiCol)idx));
    }

    /// <summary>
    /// Converts a 32-bit color value to a Vec4 float color.
    /// </summary>
    /// <param name="color">The 32-bit color value (0xRRGGBBAA or ImU32 format).</param>
    /// <returns>The color as a Vec4 (RGBA, 0-1 range).</returns>
    public static Color ColorConvertU32ToColor(uint color)
    {
        return new(ImGui_ColorConvertU32ToFloat4(color));
    }

    /// <summary>
    /// Converts a Vec4 float color to a 32-bit color value.
    /// </summary>
    /// <param name="color">The color as a Vec4 (RGBA, 0-1 range).</param>
    /// <returns>The 32-bit color value.</returns>
    public static uint ColorConvertColorToU32(Color color)
    {
        return ImGui_ColorConvertFloat4ToU32(color.Value);
    }

    /// <summary>
    /// Converts RGB color values to HSV.
    /// </summary>
    /// <param name="r">Red component (0-1).</param>
    /// <param name="g">Green component (0-1).</param>
    /// <param name="b">Blue component (0-1).</param>
    /// <returns>A tuple containing (H, S, V) values.</returns>
    public static (float H, float S, float V) ColorConvertRGBtoHSV(float r, float g, float b)
    {
        float h, s, v;
        ImGui_ColorConvertRGBtoHSV(r, g, b, &h, &s, &v);
        return (h, s, v);
    }

    /// <summary>
    /// Converts HSV color values to RGB.
    /// </summary>
    /// <param name="h">Hue component (0-1).</param>
    /// <param name="s">Saturation component (0-1).</param>
    /// <param name="v">Value component (0-1).</param>
    /// <returns>A tuple containing (R, G, B) values.</returns>
    public static (float R, float G, float B) ColorConvertHSVtoRGB(float h, float s, float v)
    {
        float r, g, b;
        ImGui_ColorConvertHSVtoRGB(h, s, v, &r, &g, &b);
        return (r, g, b);
    }

    /// <inheritdoc />
    public override string ToString()
    {
        // Return the color in RGBA format like 0xRRGGBBAA
        return $"0x{(byte)(Red * 255):X2}{(byte)(Green * 255):X2}{(byte)(Blue * 255):X2}{(byte)(Alpha * 255):X2}";
    }
}
