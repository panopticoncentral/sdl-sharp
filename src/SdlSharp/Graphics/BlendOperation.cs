namespace SdlSharp.Graphics
{
    /// <summary>
    /// A blend operation.
    /// </summary>
    public enum BlendOperation
    {
        /// <summary>
        /// Add.
        /// </summary>
        Add = Native.SDL_BlendOperation.SDL_BLENDOPERATION_ADD,

        /// <summary>
        /// Subtract.
        /// </summary>
        Subtract = Native.SDL_BlendOperation.SDL_BLENDOPERATION_SUBTRACT,

        /// <summary>
        /// Reverse subtract.
        /// </summary>
        RevSubtract = Native.SDL_BlendOperation.SDL_BLENDOPERATION_REV_SUBTRACT,

        /// <summary>
        /// Minimum.
        /// </summary>
        Minimum = Native.SDL_BlendOperation.SDL_BLENDOPERATION_MINIMUM,

        /// <summary>
        /// Maximum
        /// </summary>
        Maximum = Native.SDL_BlendOperation.SDL_BLENDOPERATION_MAXIMUM
    }
}
