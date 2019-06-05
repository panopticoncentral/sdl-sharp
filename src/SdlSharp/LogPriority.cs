namespace SdlSharp
{
    /// <summary>
    /// The priority of a log message.
    /// </summary>
    public enum LogPriority
    {
        /// <summary>
        /// Verbose.
        /// </summary>
        Verbose = 1,

        /// <summary>
        /// Debug.
        /// </summary>
        Debug,

        /// <summary>
        /// Info.
        /// </summary>
        Info,

        /// <summary>
        /// Warn.
        /// </summary>
        Warn,

        /// <summary>
        /// Error.
        /// </summary>
        Error,

        /// <summary>
        /// Critical.
        /// </summary>
        Critical,

        /// <summary>
        /// The number of priorities.
        /// </summary>
        PriorityCount
    }
}
