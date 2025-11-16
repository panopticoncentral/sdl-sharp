using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using static Sdl3Sharp.Native.Common;

// We are intentionally exposing the P/Invoke calls so people can do low-level calls if needed
#pragma warning disable CA1401 // P/Invokes should not be visible

namespace Sdl3Sharp.Native;

/// <summary>
/// P/Invoke bindings for SDL_error.h - Simple error message routines for SDL.
/// </summary>
public static unsafe partial class Error
{
    /// <summary>
    /// Set the SDL error message for the current thread.
    /// </summary>
    /// <param name="fmt">a printf()-style message format string.</param>
    /// <returns>false.</returns>
    /// <remarks>
    /// Calling this function will replace any previous error message that was set.
    /// This function always returns false, since SDL frequently uses false to
    /// signify a failing result.
    /// </remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetError([MarshalUsing(typeof(Utf8StringMarshaller))] string fmt);

    // SDL_SetErrorV is not wrapped - va_list parameters are not supported in C# P/Invoke.

    /// <summary>
    /// Set an error indicating that memory allocation failed.
    /// </summary>
    /// <returns>false.</returns>
    /// <remarks>
    /// This function does not do any memory allocation.
    /// </remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_OutOfMemory();

    /// <summary>
    /// Retrieve a message about the last error that occurred on the current thread.
    /// </summary>
    /// <returns>
    /// A message with information about the specific error that occurred,
    /// or an empty string if there hasn't been an error message set since
    /// the last call to SDL_ClearError().
    /// </returns>
    /// <remarks>
    /// <para>It is possible for multiple errors to occur before calling SDL_GetError().
    /// Only the last error is returned.</para>
    /// <para>The message is only applicable when an SDL function has signaled an error.
    /// You must check the return values of SDL function calls to determine when to
    /// appropriately call SDL_GetError(). You should *not* use the results of
    /// SDL_GetError() to decide if an error has occurred! Sometimes SDL will set
    /// an error string even when reporting success.</para>
    /// <para>SDL will *not* clear the error string for successful API calls. You *must*
    /// check return values for failure cases before you can assume the error
    /// string applies.</para>
    /// <para>Error strings are set per-thread, so an error set in a different thread
    /// will not interfere with the current thread's operation.</para>
    /// <para>The returned value is a thread-local string which will remain valid until
    /// the current thread's error string is changed. The caller should make a copy
    /// if the value is needed after the next SDL API call.</para>
    /// </remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalUsing(typeof(Utf8StringMarshaller))]
    public static partial string SDL_GetError();

    /// <summary>
    /// Clear any previous error message for this thread.
    /// </summary>
    /// <returns>true.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_ClearError();

    // SDL_Unsupported() macro is not wrapped - this is a simple macro that calls SDL_SetError.

    // SDL_InvalidParamError(param) macro is not wrapped - this is a simple macro that calls SDL_SetError.
}
