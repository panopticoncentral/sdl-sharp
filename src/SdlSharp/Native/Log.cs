using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace SdlSharp.Native;

// Deferred: SDL_Log, SDL_LogTrace, SDL_LogVerbose, SDL_LogDebug, SDL_LogInfo,
// SDL_LogWarn, SDL_LogError, SDL_LogCritical, SDL_LogMessage, SDL_LogMessageV
// (variadic printf-style functions — not compatible with LibraryImport).
// Use SDL_SetLogOutputFunction callback to capture SDL log output instead.
// Also deferred: SDL_GetDefaultLogOutputFunction, SDL_GetLogOutputFunction
// (function pointer retrieval — rarely needed).

/// <summary>
/// SDL log categories.
/// </summary>
public enum SDL_LogCategory
{
    /// <summary>Application log category.</summary>
    SDL_LOG_CATEGORY_APPLICATION = 0,
    /// <summary>Error log category.</summary>
    SDL_LOG_CATEGORY_ERROR = 1,
    /// <summary>Assert log category.</summary>
    SDL_LOG_CATEGORY_ASSERT = 2,
    /// <summary>System log category.</summary>
    SDL_LOG_CATEGORY_SYSTEM = 3,
    /// <summary>Audio log category.</summary>
    SDL_LOG_CATEGORY_AUDIO = 4,
    /// <summary>Video log category.</summary>
    SDL_LOG_CATEGORY_VIDEO = 5,
    /// <summary>Render log category.</summary>
    SDL_LOG_CATEGORY_RENDER = 6,
    /// <summary>Input log category.</summary>
    SDL_LOG_CATEGORY_INPUT = 7,
    /// <summary>Test log category.</summary>
    SDL_LOG_CATEGORY_TEST = 8,
    /// <summary>GPU log category.</summary>
    SDL_LOG_CATEGORY_GPU = 9,
    /// <summary>Start of custom log categories.</summary>
    SDL_LOG_CATEGORY_CUSTOM = 19,
}

/// <summary>
/// SDL log priority levels.
/// </summary>
public enum SDL_LogPriority
{
    /// <summary>Invalid priority.</summary>
    SDL_LOG_PRIORITY_INVALID = 0,
    /// <summary>Trace priority.</summary>
    SDL_LOG_PRIORITY_TRACE = 1,
    /// <summary>Verbose priority.</summary>
    SDL_LOG_PRIORITY_VERBOSE = 2,
    /// <summary>Debug priority.</summary>
    SDL_LOG_PRIORITY_DEBUG = 3,
    /// <summary>Info priority.</summary>
    SDL_LOG_PRIORITY_INFO = 4,
    /// <summary>Warning priority.</summary>
    SDL_LOG_PRIORITY_WARN = 5,
    /// <summary>Error priority.</summary>
    SDL_LOG_PRIORITY_ERROR = 6,
    /// <summary>Critical priority.</summary>
    SDL_LOG_PRIORITY_CRITICAL = 7,
}

/// <summary>
/// Native bindings for SDL_log.h — logging.
/// </summary>
[SuppressMessage("Interoperability", "CA1401:P/Invokes should not be visible")]
public static partial class Log
{
    /// <summary>Set the priority of all log categories.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetLogPriorities")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_SetLogPriorities(SDL_LogPriority priority);

    /// <summary>Set the priority of a particular log category.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetLogPriority")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_SetLogPriority(int category, SDL_LogPriority priority);

    /// <summary>Get the priority of a particular log category.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetLogPriority")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_LogPriority SDL_GetLogPriority(int category);

    /// <summary>Reset all priorities to default.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_ResetLogPriorities")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_ResetLogPriorities();

    /// <summary>Set the text prepended to log messages of a given priority.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetLogPriorityPrefix")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetLogPriorityPrefix(SDL_LogPriority priority, ReadOnlySpan<byte> prefix);

    /// <summary>Set the log output function.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetLogOutputFunction")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_SetLogOutputFunction(
        delegate* unmanaged[Cdecl]<void*, int, SDL_LogPriority, byte*, void> callback,
        void* userdata);
}
