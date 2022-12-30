namespace SdlSharp
{
    /// <summary>
    /// A line between two points.
    /// </summary>
    /// <param name="Start">The start of the line.</param>
    /// <param name="End">The end of the line.</param>
    public readonly record struct LineF(PointF Start, PointF End)
    {
        /// <summary>
        /// Converts a tuple to a line.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        public static implicit operator LineF((PointF start, PointF end) tuple) => new(tuple.start, tuple.end);

    }
}
