namespace SdlSharp
{
    /// <summary>
    /// A size.
    /// </summary>
    public readonly struct Size
    {
        /// <summary>
        /// The width.
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// The height.
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// Constructs a new size.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public Size(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public static implicit operator Size((int Width, int Height) tuple) => new Size(tuple.Width, tuple.Height);
    }
}
