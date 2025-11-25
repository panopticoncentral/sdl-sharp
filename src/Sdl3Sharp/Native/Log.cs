using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using static Sdl3Sharp.Native.Common;

// We are intentionally exposing the P/Invoke calls so people can do low-level calls if needed
#pragma warning disable CA1401 // P/Invokes should not be visible

namespace Sdl3Sharp.Native;

/// <summary>
/// P/Invoke bindings for SDL_log.h - Simple log messages with priorities and categories.
/// </summary>
public static unsafe partial class Log
{
    /// <summary>
    /// The predefined log categories.
    /// </summary>
    /// <remarks>
    /// By default the application and gpu categories are enabled at the INFO
    /// level, the assert category is enabled at the WARN level, test is enabled at
    /// the VERBOSE level and all other categories are enabled at the ERROR level.
    /// </remarks>
    public enum SDL_LogCategory
    {
        /// <summary>
        /// Application log category.
        /// </summary>
        SDL_LOG_CATEGORY_APPLICATION,

        /// <summary>
        /// Error log category.
        /// </summary>
        SDL_LOG_CATEGORY_ERROR,

        /// <summary>
        /// Assert log category.
        /// </summary>
        SDL_LOG_CATEGORY_ASSERT,

        /// <summary>
        /// System log category.
        /// </summary>
        SDL_LOG_CATEGORY_SYSTEM,

        /// <summary>
        /// Audio log category.
        /// </summary>
        SDL_LOG_CATEGORY_AUDIO,

        /// <summary>
        /// Video log category.
        /// </summary>
        SDL_LOG_CATEGORY_VIDEO,

        /// <summary>
        /// Render log category.
        /// </summary>
        SDL_LOG_CATEGORY_RENDER,

        /// <summary>
        /// Input log category.
        /// </summary>
        SDL_LOG_CATEGORY_INPUT,

        /// <summary>
        /// Test log category.
        /// </summary>
        SDL_LOG_CATEGORY_TEST,

        /// <summary>
        /// GPU log category.
        /// </summary>
        SDL_LOG_CATEGORY_GPU,

        /// <summary>
        /// Reserved for future SDL library use.
        /// </summary>
        SDL_LOG_CATEGORY_RESERVED2,

        /// <summary>
        /// Reserved for future SDL library use.
        /// </summary>
        SDL_LOG_CATEGORY_RESERVED3,

        /// <summary>
        /// Reserved for future SDL library use.
        /// </summary>
        SDL_LOG_CATEGORY_RESERVED4,

        /// <summary>
        /// Reserved for future SDL library use.
        /// </summary>
        SDL_LOG_CATEGORY_RESERVED5,

        /// <summary>
        /// Reserved for future SDL library use.
        /// </summary>
        SDL_LOG_CATEGORY_RESERVED6,

        /// <summary>
        /// Reserved for future SDL library use.
        /// </summary>
        SDL_LOG_CATEGORY_RESERVED7,

        /// <summary>
        /// Reserved for future SDL library use.
        /// </summary>
        SDL_LOG_CATEGORY_RESERVED8,

        /// <summary>
        /// Reserved for future SDL library use.
        /// </summary>
        SDL_LOG_CATEGORY_RESERVED9,

        /// <summary>
        /// Reserved for future SDL library use.
        /// </summary>
        SDL_LOG_CATEGORY_RESERVED10,

        /// <summary>
        /// Starting point for application-defined log categories.
        /// </summary>
        SDL_LOG_CATEGORY_CUSTOM
    }

    /// <summary>
    /// The predefined log priorities.
    /// </summary>
    public enum SDL_LogPriority
    {
        /// <summary>
        /// Invalid priority.
        /// </summary>
        SDL_LOG_PRIORITY_INVALID,

        /// <summary>
        /// Trace priority - very detailed information.
        /// </summary>
        SDL_LOG_PRIORITY_TRACE,

        /// <summary>
        /// Verbose priority - detailed information.
        /// </summary>
        SDL_LOG_PRIORITY_VERBOSE,

        /// <summary>
        /// Debug priority - debug information.
        /// </summary>
        SDL_LOG_PRIORITY_DEBUG,

        /// <summary>
        /// Info priority - informational messages.
        /// </summary>
        SDL_LOG_PRIORITY_INFO,

        /// <summary>
        /// Warn priority - warning messages.
        /// </summary>
        SDL_LOG_PRIORITY_WARN,

        /// <summary>
        /// Error priority - error messages.
        /// </summary>
        SDL_LOG_PRIORITY_ERROR,

        /// <summary>
        /// Critical priority - critical error messages.
        /// </summary>
        SDL_LOG_PRIORITY_CRITICAL,

        /// <summary>
        /// Number of log priorities.
        /// </summary>
        SDL_LOG_PRIORITY_COUNT
    }

    /// <summary>
    /// The prototype for the log output callback function.
    /// </summary>
    /// <param name="userdata">What was passed as userdata to SDL_SetLogOutputFunction.</param>
    /// <param name="category">The category of the message.</param>
    /// <param name="priority">The priority of the message.</param>
    /// <param name="message">The message being output.</param>
    /// <remarks>
    /// This function is called by SDL when there is new text to be logged. A mutex
    /// is held so that this function is never called by more than one thread at once.
    /// </remarks>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void SDL_LogOutputFunction(
        nuint userdata,
        int category,
        SDL_LogPriority priority,
        [MarshalAs(UnmanagedType.LPUTF8Str)] string message);

    /// <summary>
    /// Set the priority of all log categories.
    /// </summary>
    /// <param name="priority">The SDL_LogPriority to assign.</param>
    /// <remarks>
    /// This function is thread-safe.
    /// </remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial void SDL_SetLogPriorities(SDL_LogPriority priority);

    /// <summary>
    /// Set the priority of a particular log category.
    /// </summary>
    /// <param name="category">The category to assign a priority to.</param>
    /// <param name="priority">The SDL_LogPriority to assign.</param>
    /// <remarks>
    /// This function is thread-safe.
    /// </remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial void SDL_SetLogPriority(int category, SDL_LogPriority priority);

    /// <summary>
    /// Get the priority of a particular log category.
    /// </summary>
    /// <param name="category">The category to query.</param>
    /// <returns>The SDL_LogPriority for the requested category.</returns>
    /// <remarks>
    /// This function is thread-safe.
    /// </remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial SDL_LogPriority SDL_GetLogPriority(int category);

    /// <summary>
    /// Reset all priorities to default.
    /// </summary>
    /// <remarks>
    /// <para>This is called by SDL_Quit().</para>
    /// <para>This function is thread-safe.</para>
    /// </remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial void SDL_ResetLogPriorities();

    /// <summary>
    /// Set the text prepended to log messages of a given priority.
    /// </summary>
    /// <param name="priority">The SDL_LogPriority to modify.</param>
    /// <param name="prefix">The prefix to use for that log priority, or null to use no prefix.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    /// <remarks>
    /// <para>By default SDL_LOG_PRIORITY_INFO and below have no prefix, and
    /// SDL_LOG_PRIORITY_WARN and higher have a prefix showing their priority, e.g. "WARNING: ".</para>
    /// <para>This function is thread-safe.</para>
    /// </remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetLogPriorityPrefix(
        SDL_LogPriority priority,
        [MarshalUsing(typeof(Utf8StringMarshaller))] string? prefix);

    /// <summary>
    /// Log a message with SDL_LOG_CATEGORY_APPLICATION and SDL_LOG_PRIORITY_INFO.
    /// </summary>
    /// <param name="fmt">A printf()-style message format string.</param>
    /// <remarks>
    /// <para>This is a variadic function. Only the format string parameter can be passed from C#.
    /// For formatted logging, use string interpolation and pass the formatted string as the fmt parameter.</para>
    /// <para>This function is thread-safe.</para>
    /// </remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial void SDL_Log([MarshalUsing(typeof(Utf8StringMarshaller))] string fmt);

    /// <summary>
    /// Log a message with SDL_LOG_PRIORITY_TRACE.
    /// </summary>
    /// <param name="category">The category of the message.</param>
    /// <param name="fmt">A printf()-style message format string.</param>
    /// <remarks>
    /// <para>This is a variadic function. Only the format string parameter can be passed from C#.
    /// For formatted logging, use string interpolation and pass the formatted string as the fmt parameter.</para>
    /// <para>This function is thread-safe.</para>
    /// </remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial void SDL_LogTrace(int category, [MarshalUsing(typeof(Utf8StringMarshaller))] string fmt);

    /// <summary>
    /// Log a message with SDL_LOG_PRIORITY_VERBOSE.
    /// </summary>
    /// <param name="category">The category of the message.</param>
    /// <param name="fmt">A printf()-style message format string.</param>
    /// <remarks>
    /// <para>This is a variadic function. Only the format string parameter can be passed from C#.
    /// For formatted logging, use string interpolation and pass the formatted string as the fmt parameter.</para>
    /// <para>This function is thread-safe.</para>
    /// </remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial void SDL_LogVerbose(int category, [MarshalUsing(typeof(Utf8StringMarshaller))] string fmt);

    /// <summary>
    /// Log a message with SDL_LOG_PRIORITY_DEBUG.
    /// </summary>
    /// <param name="category">The category of the message.</param>
    /// <param name="fmt">A printf()-style message format string.</param>
    /// <remarks>
    /// <para>This is a variadic function. Only the format string parameter can be passed from C#.
    /// For formatted logging, use string interpolation and pass the formatted string as the fmt parameter.</para>
    /// <para>This function is thread-safe.</para>
    /// </remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial void SDL_LogDebug(int category, [MarshalUsing(typeof(Utf8StringMarshaller))] string fmt);

    /// <summary>
    /// Log a message with SDL_LOG_PRIORITY_INFO.
    /// </summary>
    /// <param name="category">The category of the message.</param>
    /// <param name="fmt">A printf()-style message format string.</param>
    /// <remarks>
    /// <para>This is a variadic function. Only the format string parameter can be passed from C#.
    /// For formatted logging, use string interpolation and pass the formatted string as the fmt parameter.</para>
    /// <para>This function is thread-safe.</para>
    /// </remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial void SDL_LogInfo(int category, [MarshalUsing(typeof(Utf8StringMarshaller))] string fmt);

    /// <summary>
    /// Log a message with SDL_LOG_PRIORITY_WARN.
    /// </summary>
    /// <param name="category">The category of the message.</param>
    /// <param name="fmt">A printf()-style message format string.</param>
    /// <remarks>
    /// <para>This is a variadic function. Only the format string parameter can be passed from C#.
    /// For formatted logging, use string interpolation and pass the formatted string as the fmt parameter.</para>
    /// <para>This function is thread-safe.</para>
    /// </remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial void SDL_LogWarn(int category, [MarshalUsing(typeof(Utf8StringMarshaller))] string fmt);

    /// <summary>
    /// Log a message with SDL_LOG_PRIORITY_ERROR.
    /// </summary>
    /// <param name="category">The category of the message.</param>
    /// <param name="fmt">A printf()-style message format string.</param>
    /// <remarks>
    /// <para>This is a variadic function. Only the format string parameter can be passed from C#.
    /// For formatted logging, use string interpolation and pass the formatted string as the fmt parameter.</para>
    /// <para>This function is thread-safe.</para>
    /// </remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial void SDL_LogError(int category, [MarshalUsing(typeof(Utf8StringMarshaller))] string fmt);

    /// <summary>
    /// Log a message with SDL_LOG_PRIORITY_CRITICAL.
    /// </summary>
    /// <param name="category">The category of the message.</param>
    /// <param name="fmt">A printf()-style message format string.</param>
    /// <remarks>
    /// <para>This is a variadic function. Only the format string parameter can be passed from C#.
    /// For formatted logging, use string interpolation and pass the formatted string as the fmt parameter.</para>
    /// <para>This function is thread-safe.</para>
    /// </remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial void SDL_LogCritical(int category, [MarshalUsing(typeof(Utf8StringMarshaller))] string fmt);

    /// <summary>
    /// Log a message with the specified category and priority.
    /// </summary>
    /// <param name="category">The category of the message.</param>
    /// <param name="priority">The priority of the message.</param>
    /// <param name="fmt">A printf()-style message format string.</param>
    /// <remarks>
    /// <para>This is a variadic function. Only the format string parameter can be passed from C#.
    /// For formatted logging, use string interpolation and pass the formatted string as the fmt parameter.</para>
    /// <para>This function is thread-safe.</para>
    /// </remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial void SDL_LogMessage(
        int category,
        SDL_LogPriority priority,
        [MarshalUsing(typeof(Utf8StringMarshaller))] string fmt);

    // SDL_LogMessageV is not wrapped - va_list parameters are not supported in C# P/Invoke.

    /// <summary>
    /// Get the default log output function.
    /// </summary>
    /// <returns>The default log output callback.</returns>
    /// <remarks>
    /// This function is thread-safe.
    /// </remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial SDL_LogOutputFunction SDL_GetDefaultLogOutputFunction();

    /// <summary>
    /// Get the current log output function.
    /// </summary>
    /// <param name="callback">An SDL_LogOutputFunction filled in with the current log callback.</param>
    /// <param name="userdata">A pointer filled in with the pointer that is passed to callback.</param>
    /// <remarks>
    /// This function is thread-safe.
    /// </remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial void SDL_GetLogOutputFunction(
        out SDL_LogOutputFunction callback,
        out nuint userdata);

    /// <summary>
    /// Replace the default log output function with one of your own.
    /// </summary>
    /// <param name="callback">An SDL_LogOutputFunction to call instead of the default.</param>
    /// <param name="userdata">A pointer that is passed to callback.</param>
    /// <remarks>
    /// This function is thread-safe.
    /// </remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial void SDL_SetLogOutputFunction(
        SDL_LogOutputFunction callback,
        nuint userdata);
}
