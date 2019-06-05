namespace SdlSharp
{
    /// <summary>
    /// Flags for a message box.
    /// </summary>
    public enum MessageBoxFlags
    {
        /// <summary>
        /// Error message box.
        /// </summary>
        Error = 0x00000010,

        /// <summary>
        /// Warning message box.
        /// </summary>
        Warning = 0x00000020,

        /// <summary>
        /// Information message box.
        /// </summary>
        Information = 0x00000040
    }
}
