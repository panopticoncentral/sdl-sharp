namespace Sdl3Sharp;

/// <summary>
/// The predefined log priorities.
/// </summary>
public enum LogPriority
{
    /// <summary>
    /// Invalid priority.
    /// </summary>
    Invalid = 0,

    /// <summary>
    /// Trace priority - very detailed information.
    /// </summary>
    Trace = 1,

    /// <summary>
    /// Verbose priority - detailed information.
    /// </summary>
    Verbose = 2,

    /// <summary>
    /// Debug priority - debug information.
    /// </summary>
    Debug = 3,

    /// <summary>
    /// Info priority - informational messages.
    /// </summary>
    Info = 4,

    /// <summary>
    /// Warn priority - warning messages.
    /// </summary>
    Warn = 5,

    /// <summary>
    /// Error priority - error messages.
    /// </summary>
    Error = 6,

    /// <summary>
    /// Critical priority - critical error messages.
    /// </summary>
    Critical = 7,

    /// <summary>
    /// Number of log priorities.
    /// </summary>
    Count = 8
}
