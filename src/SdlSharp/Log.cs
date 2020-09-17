// There are going to be unused fields in some of the interop structures
#pragma warning disable CS0169, RCS1213, IDE0051, IDE0052

namespace SdlSharp
{
    /// <summary>
    /// Logging.
    /// </summary>
    public static class Log
    {
        private static LogOutputFunction? s_holder;

        /// <summary>
        /// The output function for log messages.
        /// </summary>
        public static (LogOutputFunction Callback, nint UserData) OutputFunction
        {
            get
            {
                Native.SDL_LogGetOutputFunction(out var callback, out var userData);
                return (callback, userData);
            }

            set
            {
                Native.SDL_LogSetOutputFunction(value.Callback, value.UserData);
                s_holder = value.Callback;
            }
        }

        /// <summary>
        /// Sets the priority of all log categories.
        /// </summary>
        /// <param name="priority">The priority.</param>
        public static void SetAllPriority(LogPriority priority) =>
            Native.SDL_LogSetAllPriority(priority);

        /// <summary>
        /// Sets the priority of a log category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="priority">The priority.</param>
        public static void SetPriority(LogCategory category, LogPriority priority) =>
            Native.SDL_LogSetPriority(category, priority);

        /// <summary>
        /// Gets the priority of a log category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns>The priority.</returns>
        public static LogPriority GetPriority(LogCategory category) =>
            Native.SDL_LogGetPriority(category);

        /// <summary>
        /// Resets all the priorities.
        /// </summary>
        public static void ResetPriorities() =>
            Native.SDL_LogResetPriorities();

        /// <summary>
        /// Logs a verbose priority message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="category">The category.</param>
        public static void Verbose(string message, LogCategory category = LogCategory.Application) =>
            Native.SDL_LogVerbose(category, message);

        /// <summary>
        /// Logs a debug priority message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="category">The category.</param>
        public static void Debug(string message, LogCategory category = LogCategory.Application) =>
            Native.SDL_LogDebug(category, message);

        /// <summary>
        /// Logs a info priority message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="category">The category.</param>
        public static void Info(string message, LogCategory category = LogCategory.Application) =>
            Native.SDL_LogInfo(category, message);

        /// <summary>
        /// Logs a warn priority message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="category">The category.</param>
        public static void Warn(string message, LogCategory category = LogCategory.Application) =>
            Native.SDL_LogWarn(category, message);

        /// <summary>
        /// Logs a error priority message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="category">The category.</param>
        public static void Error(string message, LogCategory category = LogCategory.Application) =>
            Native.SDL_LogError(category, message);

        /// <summary>
        /// Logs a critical priority message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="category">The category.</param>
        public static void Critical(string message, LogCategory category = LogCategory.Application) =>
            Native.SDL_LogCritical(category, message);

        /// <summary>
        /// Logs a message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="category">The category.</param>
        /// <param name="priority">The priority.</param>
        public static void Message(string message, LogCategory category = LogCategory.Application, LogPriority priority = LogPriority.Info) =>
            Native.SDL_LogMessage(category, priority, message);
    }
}
