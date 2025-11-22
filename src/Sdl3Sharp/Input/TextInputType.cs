using static Sdl3Sharp.Native.Keyboard;

namespace Sdl3Sharp.Input;

/// <summary>
/// Represents the type of text input being entered.
/// </summary>
/// <remarks>
/// Not every value is valid on every platform, but where a value isn't supported,
/// a reasonable fallback will be used.
/// </remarks>
public enum TextInputType
{
    /// <summary>The input is text.</summary>
    Text = SDL_TextInputType.SDL_TEXTINPUT_TYPE_TEXT,

    /// <summary>The input is a person's name.</summary>
    TextName = SDL_TextInputType.SDL_TEXTINPUT_TYPE_TEXT_NAME,

    /// <summary>The input is an e-mail address.</summary>
    TextEmail = SDL_TextInputType.SDL_TEXTINPUT_TYPE_TEXT_EMAIL,

    /// <summary>The input is a username.</summary>
    TextUsername = SDL_TextInputType.SDL_TEXTINPUT_TYPE_TEXT_USERNAME,

    /// <summary>The input is a secure password that is hidden.</summary>
    TextPasswordHidden = SDL_TextInputType.SDL_TEXTINPUT_TYPE_TEXT_PASSWORD_HIDDEN,

    /// <summary>The input is a secure password that is visible.</summary>
    TextPasswordVisible = SDL_TextInputType.SDL_TEXTINPUT_TYPE_TEXT_PASSWORD_VISIBLE,

    /// <summary>The input is a number.</summary>
    Number = SDL_TextInputType.SDL_TEXTINPUT_TYPE_NUMBER,

    /// <summary>The input is a secure PIN that is hidden.</summary>
    NumberPasswordHidden = SDL_TextInputType.SDL_TEXTINPUT_TYPE_NUMBER_PASSWORD_HIDDEN,

    /// <summary>The input is a secure PIN that is visible.</summary>
    NumberPasswordVisible = SDL_TextInputType.SDL_TEXTINPUT_TYPE_NUMBER_PASSWORD_VISIBLE
}
