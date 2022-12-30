namespace SdlSharp.Graphics
{
    /// <summary>
    /// Shapes a window using a binarized alpha cutoff with a given integer value.
    /// </summary>
    public sealed class BinarizeAlphaWindowShapeMode : WindowShapeMode
    {
        /// <summary>
        /// The comparison is reversed.
        /// </summary>
        public bool IsReverse { get; }

        /// <summary>
        /// The cutoff.
        /// </summary>
        public byte BinarizationCutoff { get; }

        /// <summary>
        /// Constructs a binarized alpha cutoff window shape.
        /// </summary>
        /// <param name="isReverse">The comparison is reversed.</param>
        /// <param name="binarizationCutoff">The cutoff.</param>
        public BinarizeAlphaWindowShapeMode(bool isReverse, byte binarizationCutoff)
        {
            IsReverse = isReverse;
            BinarizationCutoff = binarizationCutoff;
        }

        internal override Native.SDL_WindowShapeMode ToNative() =>
            new()
            {
                mode = IsReverse ? Native.WindowShapeMode.ShapeModeReverseBinarizeAlpha : Native.WindowShapeMode.ShapeModeBinarizeAlpha,
                parameters = new Native.SDL_WindowShapeParams() { binarizationCutoff = BinarizationCutoff }
            };
    }
}
