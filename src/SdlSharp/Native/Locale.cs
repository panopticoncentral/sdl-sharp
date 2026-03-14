using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace SdlSharp.Native;

/// <summary>
/// A struct to provide locale data.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct SDL_Locale
{
    /// <summary>A BCP 47 language tag (e.g. "en").</summary>
    public byte* language;
    /// <summary>A BCP 47 country code (e.g. "US"), or null.</summary>
    public byte* country;
}

/// <summary>
/// Native bindings for SDL_locale.h — locale/language detection.
/// </summary>
[SuppressMessage("Interoperability", "CA1401:P/Invokes should not be visible")]
public static partial class Locale
{
    /// <summary>
    /// Report the user's preferred locale.
    /// Returns an array of SDL_Locale structs, allocated with SDL_malloc.
    /// Free the returned pointer with SDL_free.
    /// </summary>
    /// <param name="count">Filled with the number of locales returned.</param>
    /// <returns>A pointer to an array of locales, or null on failure.</returns>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetPreferredLocales")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Locale** SDL_GetPreferredLocales(out int count);
}
