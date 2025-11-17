namespace Sdl3Sharp.Graphics;

/// <summary>
/// Represents a set of bit masks that define the layout of color channels in a pixel format.
/// </summary>
/// <param name="Bits">The total number of bits used to represent a pixel in the format.</param>
/// <param name="Red">The bit mask that specifies the position and width of the red channel within the pixel.</param>
/// <param name="Green">The bit mask that specifies the position and width of the green channel within the pixel.</param>
/// <param name="Blue">The bit mask that specifies the position and width of the blue channel within the pixel.</param>
/// <param name="Alpha">The bit mask that specifies the position and width of the alpha (transparency) channel within the pixel.</param>
public readonly record struct PixelFormatMask(int Bits, uint Red, uint Green, uint Blue, uint Alpha)
{
}
