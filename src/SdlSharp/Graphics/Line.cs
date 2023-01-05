namespace SdlSharp.Graphics
{
    /// <summary>
    /// A line between two points.
    /// </summary>
    /// <param name="Start">The start of the line.</param>
    /// <param name="End">The end of the line.</param>
    public readonly record struct Line(Point Start, Point End)
    {
    }
}
