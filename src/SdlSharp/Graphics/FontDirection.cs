namespace SdlSharp.Graphics
{
    /// <summary>
    /// The direction of the font.
    /// </summary>
    public enum FontDirection
    {
        /// <summary>
        /// Left to right
        /// </summary>
        LeftToRight = Native.TTF_Direction.TTF_DIRECTION_LTR,

        /// <summary>
        /// Right to left
        /// </summary>
        RightToLeft = Native.TTF_Direction.TTF_DIRECTION_RTL,

        /// <summary>
        /// Top to bottom
        /// </summary>
        TopToBottom = Native.TTF_Direction.TTF_DIRECTION_TTB,

        /// <summary>
        /// Bottom to top
        /// </summary>
        BottomToTop = Native.TTF_Direction.TTF_DIRECTION_BTT
    }
}
