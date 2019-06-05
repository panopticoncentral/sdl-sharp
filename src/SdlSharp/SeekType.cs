namespace SdlSharp
{
    /// <summary>
    /// The type of seek to perform.
    /// </summary>
    public enum SeekType
    {
        /// <summary>
        /// Seek from beginning of data.
        /// </summary>
        Set,

        /// <summary>
        /// Seek relative to current read point.
        /// </summary>
        Current,

        /// <summary>
        /// Seek relative to the end of the data.
        /// </summary>
        End
    }
}
