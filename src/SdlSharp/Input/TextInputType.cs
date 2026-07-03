namespace SdlSharp.Input;

/// <summary>
/// The kind of text being input, used to hint on-screen keyboards and IMEs.
/// </summary>
public enum TextInputType
{
    /// <summary>The input is text.</summary>
    Text = (int)Native.SDL_TextInputType.SDL_TEXTINPUT_TYPE_TEXT,
    /// <summary>The input is a person's name.</summary>
    TextName = (int)Native.SDL_TextInputType.SDL_TEXTINPUT_TYPE_TEXT_NAME,
    /// <summary>The input is an e-mail address.</summary>
    TextEmail = (int)Native.SDL_TextInputType.SDL_TEXTINPUT_TYPE_TEXT_EMAIL,
    /// <summary>The input is a username.</summary>
    TextUsername = (int)Native.SDL_TextInputType.SDL_TEXTINPUT_TYPE_TEXT_USERNAME,
    /// <summary>The input is a secure password that is hidden.</summary>
    TextPasswordHidden = (int)Native.SDL_TextInputType.SDL_TEXTINPUT_TYPE_TEXT_PASSWORD_HIDDEN,
    /// <summary>The input is a secure password that is visible.</summary>
    TextPasswordVisible = (int)Native.SDL_TextInputType.SDL_TEXTINPUT_TYPE_TEXT_PASSWORD_VISIBLE,
    /// <summary>The input is a number.</summary>
    Number = (int)Native.SDL_TextInputType.SDL_TEXTINPUT_TYPE_NUMBER,
    /// <summary>The input is a secure PIN that is hidden.</summary>
    NumberPasswordHidden = (int)Native.SDL_TextInputType.SDL_TEXTINPUT_TYPE_NUMBER_PASSWORD_HIDDEN,
    /// <summary>The input is a secure PIN that is visible.</summary>
    NumberPasswordVisible = (int)Native.SDL_TextInputType.SDL_TEXTINPUT_TYPE_NUMBER_PASSWORD_VISIBLE,
}
