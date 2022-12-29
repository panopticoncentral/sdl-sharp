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
        Verbose = Native.SDL_LogPriority.SDL_LOG_PRIORITY_VERBOSE,

        /// <summary>
        /// Debug.
        /// </summary>
        Debug = Native.SDL_LogPriority.SDL_LOG_PRIORITY_DEBUG,

        /// <summary>
        /// Info.
        /// </summary>
        Info = Native.SDL_LogPriority.SDL_LOG_PRIORITY_INFO,

        /// <summary>
        /// Warn.
        /// </summary>
        Warn = Native.SDL_LogPriority.SDL_LOG_PRIORITY_WARN,

        /// <summary>
        /// Error.
        /// </summary>
        Error = Native.SDL_LogPriority.SDL_LOG_PRIORITY_ERROR,

        /// <summary>
        /// Critical.
        /// </summary>
        Critical = Native.SDL_LogPriority.SDL_LOG_PRIORITY_CRITICAL,

        /// <summary>
        /// The number of priorities.
        /// </summary>
        PriorityCount = Native.SDL_LogPriority.SDL_NUM_LOG_PRIORITIES
    }
}
