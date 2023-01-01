namespace SdlSharp.Graphics
{
    /// <summary>
    /// The alignment of wrapped text.
    /// </summary>
    public enum FontWrappingAlignment
    {
        /// <summary>
        /// Left
        /// </summary>
        Left = Native.TTF_WRAPPED_ALIGN_LEFT,

        /// <summary>
        /// Center
        /// </summary>
        Center = Native.TTF_WRAPPED_ALIGN_CENTER,

        /// <summary>
        /// Right
        /// </summary>
        Right = Native.TTF_WRAPPED_ALIGN_RIGHT
    }
}
