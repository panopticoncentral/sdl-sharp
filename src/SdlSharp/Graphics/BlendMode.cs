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
        public static readonly BlendMode None = new(Native.SDL_BlendMode.SDL_BLENDMODE_NONE);

        /// <summary>
        /// Alpha blending.
        /// </summary>
        public static readonly BlendMode Blend = new(Native.SDL_BlendMode.SDL_BLENDMODE_BLEND);

        /// <summary>
        /// Additive blending.
        /// </summary>
        public static readonly BlendMode Add = new(Native.SDL_BlendMode.SDL_BLENDMODE_ADD);

        /// <summary>
        /// Color modulate blending.
        /// </summary>
        public static readonly BlendMode Mod = new(Native.SDL_BlendMode.SDL_BLENDMODE_MOD);

        /// <summary>
        /// Color multiple blending.
        /// </summary>
        public static readonly BlendMode Mul = new(Native.SDL_BlendMode.SDL_BLENDMODE_MUL);

        /// <summary>
        /// Invalid blend mode.
        /// </summary>
        public static readonly BlendMode Invalid = new(Native.SDL_BlendMode.SDL_BLENDMODE_INVALID);

        private readonly Native.SDL_BlendMode _mode;

        private BlendMode(Native.SDL_BlendMode mode)
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
            new(Native.SDL_ComposeCustomBlendMode((Native.SDL_BlendFactor)sourceColorFactor, (Native.SDL_BlendFactor)destinationColorFactor, (Native.SDL_BlendOperation)colorOperation, (Native.SDL_BlendFactor)sourceAlphaFactor, (Native.SDL_BlendFactor)destinationAlphaFactor, (Native.SDL_BlendOperation)alphaOperation));
    }
}
