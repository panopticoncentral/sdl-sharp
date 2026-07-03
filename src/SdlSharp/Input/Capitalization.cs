namespace SdlSharp.Input;

/// <summary>
/// How text input should be auto-capitalized.
/// </summary>
public enum Capitalization
{
    /// <summary>No auto-capitalization.</summary>
    None = (int)Native.SDL_Capitalization.SDL_CAPITALIZE_NONE,
    /// <summary>The first letter of sentences will be capitalized.</summary>
    Sentences = (int)Native.SDL_Capitalization.SDL_CAPITALIZE_SENTENCES,
    /// <summary>The first letter of words will be capitalized.</summary>
    Words = (int)Native.SDL_Capitalization.SDL_CAPITALIZE_WORDS,
    /// <summary>All letters will be capitalized.</summary>
    Letters = (int)Native.SDL_Capitalization.SDL_CAPITALIZE_LETTERS,
}
