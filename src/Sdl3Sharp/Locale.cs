using System.Runtime.InteropServices;

using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Locale;
using static Sdl3Sharp.Native.StdInc;

namespace Sdl3Sharp;

/// <summary>
/// Provides access to the user's preferred locale settings.
/// </summary>
/// <remarks>
/// <para>This class allows applications to query the user's language and regional preferences
/// for internationalization and localization purposes.</para>
/// <para>The locale list can change when the user modifies system preferences. SDL will send
/// an SDL_EVENT_LOCALE_CHANGED event when this occurs, and applications should call
/// GetPreferredLocales() again to get the updated list.</para>
/// </remarks>
public static unsafe class Locale
{
    /// <summary>
    /// Gets the user's preferred locales in order of preference.
    /// </summary>
    /// <returns>
    /// A list of LocaleInfo objects representing the user's preferred locales,
    /// ordered by preference (most preferred first).
    /// </returns>
    /// <exception cref="SdlException">Thrown when the platform does not provide locale information or an error occurs.</exception>
    /// <remarks>
    /// <para>The returned list is in order of the user's preference. For example, a German citizen
    /// fluent in US English who knows some Japanese might have: { "de", "en_US", "jp" }.
    /// Someone from England might prefer British English but accept any English: { "en_GB", "en" }.</para>
    /// <para>This may be a "slow" call that queries the operating system. Cache the results
    /// and only refresh when an SDL_EVENT_LOCALE_CHANGED event is received.</para>
    /// <para>Language codes are in ISO-639 format (e.g., "en", "de", "jp").</para>
    /// <para>Country codes are in ISO-3166 format (e.g., "US", "CA", "GB") and may be null
    /// if no specific regional guidance is available.</para>
    /// </remarks>
    public static List<LocaleInfo> GetPreferredLocales()
    {
        int count;
        SDL_Locale** localesPtr = CheckErrorPointer(SDL_GetPreferredLocales(&count));

        try
        {
            var result = new List<LocaleInfo>(count);

            for (var i = 0; i < count; i++)
            {
                SDL_Locale* locale = localesPtr[i];
                var language = Marshal.PtrToStringUTF8((nint)locale->language);
                var country = locale->country is not null
                    ? Marshal.PtrToStringUTF8((nint)locale->country)
                    : null;

                if (language is not null)
                {
                    result.Add(new LocaleInfo(language, country));
                }
            }

            return result;
        }
        finally
        {
            SDL_free(localesPtr);
        }
    }
}
