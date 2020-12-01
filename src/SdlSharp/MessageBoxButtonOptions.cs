namespace SdlSharp
{
    /// <summary>
    /// Flags for a message box button.
    /// </summary>
    public enum MessageBoxButtonOptions : uint
    {
        /// <summary>
        /// None.
        /// </summary>
        None = 0x00000000,

        /// <summary>
        /// This button is the return key button.
        /// </summary>
        ReturnKeyDefault = 0x00000001,

        /// <summary>
        /// This button is the escape key button.
        /// </summary>
        EscapeKeyDefault = 0x00000002
    }
}
