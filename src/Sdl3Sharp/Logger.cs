using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Log;

namespace Sdl3Sharp;

/// <summary>
/// Represents a callback that is invoked when SDL logs a message.
/// </summary>
/// <param name="category">The category of the message.</param>
/// <param name="priority">The priority of the message.</param>
/// <param name="message">The message being output.</param>
public delegate void LogOutputCallback(LogCategory category, LogPriority priority, string message);

/// <summary>
/// Provides methods for logging messages with SDL's logging system.
/// </summary>
/// <remarks>
/// <para>SDL provides a simple logging system with priorities and categories. Every log message
/// has a priority and a category. Messages are only sent out if they meet the minimum priority
/// threshold for their category.</para>
/// <para>All logging operations are thread-safe.</para>
/// </remarks>
public static unsafe class Logger
{
    private static LogOutputCallback? _currentCallback;
    private static SDL_LogOutputFunction? _nativeCallback;

    /// <summary>
    /// Set the priority of all log categories.
    /// </summary>
    /// <param name="priority">The priority to assign.</param>
    public static void SetAllPriorities(LogPriority priority)
    {
        SDL_SetLogPriorities((SDL_LogPriority)priority);
    }

    /// <summary>
    /// Set the priority of a particular log category.
    /// </summary>
    /// <param name="category">The category to assign a priority to.</param>
    /// <param name="priority">The priority to assign.</param>
    public static void SetPriority(LogCategory category, LogPriority priority)
    {
        SDL_SetLogPriority((int)category, (SDL_LogPriority)priority);
    }

    /// <summary>
    /// Get the priority of a particular log category.
    /// </summary>
    /// <param name="category">The category to query.</param>
    /// <returns>The priority for the requested category.</returns>
    public static LogPriority GetPriority(LogCategory category)
    {
        return (LogPriority)SDL_GetLogPriority((int)category);
    }

    /// <summary>
    /// Reset all priorities to default.
    /// </summary>
    /// <remarks>
    /// This is called by SDL_Quit().
    /// </remarks>
    public static void ResetPriorities()
    {
        SDL_ResetLogPriorities();
    }

    /// <summary>
    /// Set the text prepended to log messages of a given priority.
    /// </summary>
    /// <param name="priority">The priority to modify.</param>
    /// <param name="prefix">The prefix to use for that log priority, or null to use no prefix.</param>
    /// <exception cref="SdlException">Thrown when setting the prefix fails.</exception>
    /// <remarks>
    /// By default INFO and below have no prefix, and WARN and higher have a prefix showing
    /// their priority, e.g. "WARNING: ".
    /// </remarks>
    public static void SetPriorityPrefix(LogPriority priority, string? prefix)
    {
        _ = CheckErrorBool(SDL_SetLogPriorityPrefix((SDL_LogPriority)priority, prefix));
    }

    /// <summary>
    /// Log a message with SDL_LOG_CATEGORY_APPLICATION and SDL_LOG_PRIORITY_INFO.
    /// </summary>
    /// <param name="message">The message to log.</param>
    public static void Log(string message)
    {
        SDL_Log(message);
    }

    /// <summary>
    /// Log a trace message.
    /// </summary>
    /// <param name="category">The category of the message.</param>
    /// <param name="message">The message to log.</param>
    public static void Trace(LogCategory category, string message)
    {
        SDL_LogTrace((int)category, message);
    }

    /// <summary>
    /// Log a trace message in the Application category.
    /// </summary>
    /// <param name="message">The message to log.</param>
    public static void Trace(string message)
    {
        SDL_LogTrace((int)LogCategory.Application, message);
    }

    /// <summary>
    /// Log a verbose message.
    /// </summary>
    /// <param name="category">The category of the message.</param>
    /// <param name="message">The message to log.</param>
    public static void Verbose(LogCategory category, string message)
    {
        SDL_LogVerbose((int)category, message);
    }

    /// <summary>
    /// Log a verbose message in the Application category.
    /// </summary>
    /// <param name="message">The message to log.</param>
    public static void Verbose(string message)
    {
        SDL_LogVerbose((int)LogCategory.Application, message);
    }

    /// <summary>
    /// Log a debug message.
    /// </summary>
    /// <param name="category">The category of the message.</param>
    /// <param name="message">The message to log.</param>
    public static void Debug(LogCategory category, string message)
    {
        SDL_LogDebug((int)category, message);
    }

    /// <summary>
    /// Log a debug message in the Application category.
    /// </summary>
    /// <param name="message">The message to log.</param>
    public static void Debug(string message)
    {
        SDL_LogDebug((int)LogCategory.Application, message);
    }

    /// <summary>
    /// Log an info message.
    /// </summary>
    /// <param name="category">The category of the message.</param>
    /// <param name="message">The message to log.</param>
    public static void Info(LogCategory category, string message)
    {
        SDL_LogInfo((int)category, message);
    }

    /// <summary>
    /// Log an info message in the Application category.
    /// </summary>
    /// <param name="message">The message to log.</param>
    public static void Info(string message)
    {
        SDL_LogInfo((int)LogCategory.Application, message);
    }

    /// <summary>
    /// Log a warning message.
    /// </summary>
    /// <param name="category">The category of the message.</param>
    /// <param name="message">The message to log.</param>
    public static void Warn(LogCategory category, string message)
    {
        SDL_LogWarn((int)category, message);
    }

    /// <summary>
    /// Log a warning message in the Application category.
    /// </summary>
    /// <param name="message">The message to log.</param>
    public static void Warn(string message)
    {
        SDL_LogWarn((int)LogCategory.Application, message);
    }

    /// <summary>
    /// Log an error message.
    /// </summary>
    /// <param name="category">The category of the message.</param>
    /// <param name="message">The message to log.</param>
    public static void Error(LogCategory category, string message)
    {
        SDL_LogError((int)category, message);
    }

    /// <summary>
    /// Log an error message in the Application category.
    /// </summary>
    /// <param name="message">The message to log.</param>
    public static void Error(string message)
    {
        SDL_LogError((int)LogCategory.Application, message);
    }

    /// <summary>
    /// Log a critical message.
    /// </summary>
    /// <param name="category">The category of the message.</param>
    /// <param name="message">The message to log.</param>
    public static void Critical(LogCategory category, string message)
    {
        SDL_LogCritical((int)category, message);
    }

    /// <summary>
    /// Log a critical message in the Application category.
    /// </summary>
    /// <param name="message">The message to log.</param>
    public static void Critical(string message)
    {
        SDL_LogCritical((int)LogCategory.Application, message);
    }

    /// <summary>
    /// Log a message with the specified category and priority.
    /// </summary>
    /// <param name="category">The category of the message.</param>
    /// <param name="priority">The priority of the message.</param>
    /// <param name="message">The message to log.</param>
    public static void LogMessage(LogCategory category, LogPriority priority, string message)
    {
        SDL_LogMessage((int)category, (SDL_LogPriority)priority, message);
    }

    /// <summary>
    /// Get the default log output function.
    /// </summary>
    /// <returns>The default log output callback.</returns>
    public static SDL_LogOutputFunction GetDefaultOutputFunction()
    {
        return SDL_GetDefaultLogOutputFunction();
    }

    /// <summary>
    /// Replace the default log output function with a custom one.
    /// </summary>
    /// <param name="callback">A callback to call instead of the default, or null to restore the default.</param>
    /// <remarks>
    /// The callback will be invoked for all log messages. Keep a reference to the callback
    /// delegate to prevent it from being garbage collected.
    /// </remarks>
    public static void SetOutputFunction(LogOutputCallback? callback)
    {
        if (callback is null)
        {
            // Restore default
            _currentCallback = null;
            _nativeCallback = null;
            SDL_LogOutputFunction defaultFunc = SDL_GetDefaultLogOutputFunction();
            SDL_SetLogOutputFunction(defaultFunc, 0);
        }
        else
        {
            _currentCallback = callback;
            _nativeCallback = (userdata, category, priority, message) =>
            {
                try
                {
                    var managedCategory = (LogCategory)category;
                    var managedPriority = (LogPriority)priority;
                    _currentCallback?.Invoke(managedCategory, managedPriority, message);
                }
                catch
                {
                    // Suppress exceptions to prevent them from propagating to native code
                }
            };
            SDL_SetLogOutputFunction(_nativeCallback, 0);
        }
    }
}
