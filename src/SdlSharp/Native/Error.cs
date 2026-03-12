using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SdlSharp.Native;

/// <summary>
/// Native bindings for SDL_error.h — error handling functions.
/// </summary>
[SuppressMessage("Interoperability", "CA1401:P/Invokes should not be visible")]
public static partial class Error
{
    // SDL_SetErrorV is not wrapped — it takes a C va_list which has no safe C# equivalent.
    // Use SDL_SetError with a pre-formatted message instead.

    /// <summary>
    /// Set the SDL error message for the current thread.
    /// </summary>
    /// <param name="fmt">The error message.</param>
    /// <returns>false.</returns>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetError")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetError(ReadOnlySpan<byte> fmt);

    /// <summary>
    /// Set an error indicating that memory allocation failed.
    /// </summary>
    /// <returns>false.</returns>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_OutOfMemory")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_OutOfMemory();

    /// <summary>
    /// Set a standardized error for unsupported operations.
    /// </summary>
    /// <returns>false.</returns>
    public static bool SDL_Unsupported() =>
        SDL_SetError("That operation is not supported"u8);

    /// <summary>
    /// Set a standardized error for invalid parameters.
    /// </summary>
    /// <param name="param">The name of the invalid parameter.</param>
    /// <returns>false.</returns>
    public static bool SDL_InvalidParamError(string param) =>
        SDL_SetError(Common.ToUtf8($"Parameter '{param}' is invalid"));

    /// <summary>
    /// Retrieve a message about the last error that occurred on the current thread.
    /// </summary>
    /// <returns>A message with information about the specific error that occurred.</returns>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetError")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* SDL_GetError();

    /// <summary>
    /// Clear any previous error message for this thread.
    /// </summary>
    /// <returns>true.</returns>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_ClearError")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_ClearError();
}
