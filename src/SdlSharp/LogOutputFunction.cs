using System;
using System.Runtime.InteropServices;

namespace SdlSharp
{
    /// <summary>
    /// A callback for log messages.
    /// </summary>
    /// <param name="userdata">User data specified for the callback.</param>
    /// <param name="category">The category.</param>
    /// <param name="priority">The priority.</param>
    /// <param name="message">The message.</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    public delegate void LogOutputFunction(IntPtr userdata, LogCategory category, LogPriority priority, string message);
}
