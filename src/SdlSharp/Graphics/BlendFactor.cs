namespace SdlSharp.Graphics
{
    /// <summary>
    /// The normalized factor used to multiply pixel components.
    /// </summary>
    public enum BlendFactor
    {
        /// <summary>
        /// Zero.
        /// </summary>
        Zero = 0x1,

        /// <summary>
        /// One.
        /// </summary>
        One = 0x2,

        /// <summary>
        /// Source color.
        /// </summary>
        SourceColor = 0x3,

        /// <summary>
        /// One minus source color.
        /// </summary>
        OneMinusSourceColor = 0x4,

        /// <summary>
        /// Source alpha.
        /// </summary>
        SourceAlpha = 0x5,

        /// <summary>
        /// One minus source alpha.
        /// </summary>
        OneMinusSourceAlpha = 0x6,

        /// <summary>
        /// Destination color.
        /// </summary>
        DestinationColor = 0x7,

        /// <summary>
        /// One minus destination color.
        /// </summary>
        OneMinusDestinationColor = 0x8,

        /// <summary>
        /// Destination alpha.
        /// </summary>
        DestinationAlpha = 0x9,

        /// <summary>
        /// One minus destination alpha.
        /// </summary>
        OneMinusDestinationAlpha = 0xA
    }
}
