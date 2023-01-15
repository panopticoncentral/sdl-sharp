using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SdlSharp
{
    /// <summary>
    /// Logging.
    /// </summary>
    public static unsafe class Log
    {
        private static Action<LogCategory, LogPriority, string?>? s_handler;

        /// <summary>
        /// Sets the log handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        public static void SetLogHandler(Action<LogCategory, LogPriority, string?>? handler)
        {
            s_handler = handler;
            Native.SDL_LogSetOutputFunction(handler == null ? null : &LogCallback, 0);
        }

        [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        private static unsafe void LogCallback(nint userData, int category, Native.SDL_LogPriority priority, byte* message) =>
            s_handler?.Invoke((LogCategory)category, (LogPriority)priority, Native.Utf8ToString(message));

        /// <summary>
        /// Sets the priority of all log categories.
        /// </summary>
        /// <param name="priority">The priority.</param>
        public static void SetAllPriority(LogPriority priority) =>
            Native.SDL_LogSetAllPriority((Native.SDL_LogPriority)priority);

        /// <summary>
        /// Sets the priority of a log category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="priority">The priority.</param>
        public static void SetPriority(LogCategory category, LogPriority priority) =>
            Native.SDL_LogSetPriority((int)category, (Native.SDL_LogPriority)priority);

        /// <summary>
        /// Gets the priority of a log category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns>The priority.</returns>
        public static LogPriority GetPriority(LogCategory category) =>
            (LogPriority)Native.SDL_LogGetPriority((int)category);

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
        public static void Verbose(string message, LogCategory category = LogCategory.Application) => Native.StringToUtf8Action(message, ptr => Native.SDL_LogVerbose((int)category, ptr));

        /// <summary>
        /// Logs a debug priority message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="category">The category.</param>
        public static void Debug(string message, LogCategory category = LogCategory.Application) => Native.StringToUtf8Action(message, ptr => Native.SDL_LogDebug((int)category, ptr));

        /// <summary>
        /// Logs a info priority message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="category">The category.</param>
        public static void Info(string message, LogCategory category = LogCategory.Application) => Native.StringToUtf8Action(message, ptr => Native.SDL_LogInfo((int)category, ptr));

        /// <summary>
        /// Logs a warn priority message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="category">The category.</param>
        public static void Warn(string message, LogCategory category = LogCategory.Application) => Native.StringToUtf8Action(message, ptr => Native.SDL_LogWarn((int)category, ptr));

        /// <summary>
        /// Logs a error priority message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="category">The category.</param>
        public static void Error(string message, LogCategory category = LogCategory.Application) => Native.StringToUtf8Action(message, ptr => Native.SDL_LogError((int)category, ptr));

        /// <summary>
        /// Logs a critical priority message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="category">The category.</param>
        public static void Critical(string message, LogCategory category = LogCategory.Application) => Native.StringToUtf8Action(message, ptr => Native.SDL_LogCritical((int)category, ptr));

        /// <summary>
        /// Logs a message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="category">The category.</param>
        /// <param name="priority">The priority.</param>
        public static void Message(string message, LogCategory category = LogCategory.Application, LogPriority priority = LogPriority.Info) => Native.StringToUtf8Action(message, ptr => Native.SDL_LogMessage((int)category, (Native.SDL_LogPriority)priority, ptr));
    }
}
