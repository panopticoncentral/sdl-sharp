using static Sdl3Sharp.Native.Keyboard;

namespace Sdl3Sharp.Input;

/// <summary>
/// Represents the auto-capitalization mode for text input.
/// </summary>
/// <remarks>
/// Not every value is valid on every platform, but where a value isn't supported,
/// a reasonable fallback will be used.
/// </remarks>
public enum Capitalization
{
    /// <summary>No auto-capitalization will be done.</summary>
    None = SDL_Capitalization.SDL_CAPITALIZE_NONE,

    /// <summary>The first letter of sentences will be capitalized.</summary>
    Sentences = SDL_Capitalization.SDL_CAPITALIZE_SENTENCES,

    /// <summary>The first letter of words will be capitalized.</summary>
    Words = SDL_Capitalization.SDL_CAPITALIZE_WORDS,

    /// <summary>All letters will be capitalized.</summary>
    Letters = SDL_Capitalization.SDL_CAPITALIZE_LETTERS
}
