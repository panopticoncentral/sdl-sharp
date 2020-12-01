// There are going to be unused fields in some of the interop structures
#pragma warning disable CS0169, RCS1213, IDE0051, IDE0052

namespace SdlSharp.Graphics
{
    /// <summary>
    /// A mode for blending textures.
    /// </summary>
    public readonly struct BlendMode
    {
        /// <summary>
        /// No blending.
        /// </summary>
        public static readonly BlendMode None = new(0x00000000);

        /// <summary>
        /// Alpha blending.
        /// </summary>
        public static readonly BlendMode Blend = new(0x00000001);

        /// <summary>
        /// Additive blending.
        /// </summary>
        public static readonly BlendMode Add = new(0x00000002);

        /// <summary>
        /// Color modulate blending.
        /// </summary>
        public static readonly BlendMode Mod = new(0x00000004);

        /// <summary>
        /// Color multiple blending.
        /// </summary>
        public static readonly BlendMode Mul = new(0x00000008);

        /// <summary>
        /// Invalid blend mode.
        /// </summary>
        public static readonly BlendMode Invalid = new(0x7FFFFFFF);

        private readonly uint _mode;

        private BlendMode(uint mode)
        {
            _mode = mode;
        }

        /// <summary>
        /// Creates a custom blending mode.
        /// </summary>
        /// <param name="sourceColorFactor">The source color factor.</param>
        /// <param name="destinationColorFactor">The destination color factor.</param>
        /// <param name="colorOperation">The color operation.</param>
        /// <param name="sourceAlphaFactor">The source alpha factor.</param>
        /// <param name="destinationAlphaFactor">The destination alpha factor.</param>
        /// <param name="alphaOperation">The alpha operation.</param>
        /// <returns>The custom blend mode.</returns>
        public static BlendMode Custom(BlendFactor sourceColorFactor, BlendFactor destinationColorFactor, BlendOperation colorOperation, BlendFactor sourceAlphaFactor, BlendFactor destinationAlphaFactor, BlendOperation alphaOperation) =>
            Native.SDL_ComposeCustomBlendMode(sourceColorFactor, destinationColorFactor, colorOperation, sourceAlphaFactor, destinationAlphaFactor, alphaOperation);
    }
}
