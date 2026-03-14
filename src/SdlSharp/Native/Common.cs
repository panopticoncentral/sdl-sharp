using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
// ReSharper disable InconsistentNaming

namespace SdlSharp.Native;

/// <summary>
/// Common constants, types, and helpers shared across all native SDL3 bindings.
/// </summary>
public static unsafe partial class Common
{
    /// <summary>
    /// The native SDL3 library name.
    /// </summary>
    public const string Sdl3 = "SDL3";

    /// <summary>
    /// Converts a string to a null-terminated UTF-8 byte array suitable for passing
    /// as a <c>ReadOnlySpan&lt;byte&gt;</c> to native SDL functions.
    /// Returns null for null input (which becomes a null pointer when pinned by LibraryImport).
    /// </summary>
    internal static byte[]? ToUtf8(string? s)
    {
        if (s is null)
        {
            return null;
        }

        var byteCount = Encoding.UTF8.GetByteCount(s);
        var bytes = new byte[byteCount + 1];
        Encoding.UTF8.GetBytes(s, bytes);
        // bytes[byteCount] is already 0 (null terminator)
        return bytes;
    }

    /// <summary>
    /// Checks a bool return value from SDL. Throws <see cref="SdlException"/> if false.
    /// </summary>
    /// <param name="result">The bool result from an SDL function.</param>
    public static void Check(bool result)
    {
        if (!result)
        {
            throw new SdlException();
        }
    }

    /// <summary>
    /// Checks a pointer return value from SDL. Throws <see cref="SdlException"/> if null.
    /// </summary>
    /// <typeparam name="T">The unmanaged pointer type.</typeparam>
    /// <param name="ptr">The pointer result from an SDL function.</param>
    /// <returns>The non-null pointer.</returns>
    public static T* Check<T>(T* ptr) where T : unmanaged
    {
        if (ptr == null)
        {
            throw new SdlException();
        }

        return ptr;
    }

    /// <summary>
    /// Checks a uint ID return value from SDL. Throws <see cref="SdlException"/> if 0.
    /// </summary>
    /// <param name="id">The ID result from an SDL function.</param>
    /// <returns>The non-zero ID.</returns>
    public static uint CheckId(uint id)
    {
        if (id == 0)
        {
            throw new SdlException();
        }

        return id;
    }

    /// <summary>
    /// Checks an SDL_PropertiesID return value. Throws <see cref="SdlException"/> if 0.
    /// </summary>
    public static SDL_PropertiesID CheckId(SDL_PropertiesID id)
    {
        if (id.Value == 0)
        {
            throw new SdlException();
        }

        return id;
    }

    /// <summary>
    /// Free memory allocated by SDL.
    /// </summary>
    [LibraryImport(Sdl3, EntryPoint = "SDL_free")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_free(void* mem);
}
