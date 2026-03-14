using System.Runtime.InteropServices;

using static SdlSharp.Native.Common;
using static SdlSharp.Native.Locale;

namespace SdlSharp;

/// <summary>
/// Represents a locale (language and optional country).
/// </summary>
/// <param name="Language">A BCP 47 language tag (e.g. "en").</param>
/// <param name="Country">A BCP 47 country code (e.g. "US"), or null.</param>
public readonly record struct LocaleInfo(string Language, string? Country)
{
    /// <summary>
    /// Gets the user's preferred locales, in order of preference.
    /// </summary>
    public static unsafe LocaleInfo[] GetPreferred()
    {
        var locales = SDL_GetPreferredLocales(out var count);
        if (locales == null) return [];
        try
        {
            var result = new LocaleInfo[count];
            for (var i = 0; i < count; i++)
            {
                var locale = locales[i];
                result[i] = new LocaleInfo(
                    Marshal.PtrToStringUTF8((nint)locale->language) ?? "",
                    Marshal.PtrToStringUTF8((nint)locale->country));
            }
            return result;
        }
        finally
        {
            SDL_free(locales);
        }
    }
}
