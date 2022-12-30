namespace SdlSharp.Graphics
{
    /// <summary>
    /// A color keyed window shape.
    /// </summary>
    public sealed class ColorKeyWindowShapeMode : WindowShapeMode
    {
        /// <summary>
        /// The key.
        /// </summary>
        public Color Key { get; }

        /// <summary>
        /// Constructs a color keyed window shape.
        /// </summary>
        /// <param name="key">The key.</param>
        public ColorKeyWindowShapeMode(Color key)
        {
            Key = key;
        }

        internal override Native.SDL_WindowShapeMode ToNative() =>
            new()
            {
                mode = Native.WindowShapeMode.ShapeModeColorKey,
                parameters = new Native.SDL_WindowShapeParams() { colorKey = Key.ToNative() }
            };
    }
}
