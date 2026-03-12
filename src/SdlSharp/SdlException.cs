using System.Runtime.InteropServices;

using static SdlSharp.Native.Error;

namespace SdlSharp;

/// <summary>
/// An exception thrown when an SDL operation fails.
/// </summary>
public class SdlException : Exception
{
    /// <summary>
    /// Creates an SdlException with the current SDL error message.
    /// </summary>
    public unsafe SdlException()
        : base(Marshal.PtrToStringUTF8((nint)SDL_GetError()) ?? "Unknown SDL error")
    {
        SDL_ClearError();
    }

    /// <summary>
    /// Creates an SdlException with a custom message.
    /// </summary>
    public SdlException(string message) : base(message)
    {
    }

    /// <summary>
    /// Creates an SdlException with a custom message and inner exception.
    /// </summary>
    public SdlException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
