namespace Sdl3Sharp;

/// <summary>
/// Represents locale information including language and optional country code.
/// </summary>
/// <param name="Language">The ISO-639 language code (e.g., "en" for English, "de" for German).</param>
/// <param name="Country">The optional ISO-3166 country code (e.g., "US" for United States, "CA" for Canada), or null if not specified.</param>
/// <remarks>
/// <para>Language codes are in ISO-639 format and are never null. Examples: "en", "de", "jp".</para>
/// <para>Country codes are in ISO-3166 format and may be null if there's no specific regional
/// guidance. For example, "en_US" has both language and country, while "en" with null country
/// means English generically.</para>
/// <para>Note that not all language or country codes are 2 characters; some are three or more.</para>
/// </remarks>
public readonly record struct LocaleInfo(string Language, string? Country)
{
    /// <summary>
    /// Returns a string representation of the locale in standard format.
    /// </summary>
    /// <returns>
    /// A string in the format "language" if country is null, or "language_COUNTRY" if country is specified.
    /// </returns>
    public override string ToString()
    {
        return Country is null ? Language : $"{Language}_{Country}";
    }
}
