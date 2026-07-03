namespace SdlSharp.Input;

/// <summary>
/// Optional text input configuration for <see cref="SdlSharp.Graphics.Window.StartTextInput(in TextInputProperties)"/>.
/// Unset (null) fields are not sent to SDL, which then applies its own
/// context-sensitive defaults (for example, capitalization defaults depend on
/// the input type).
/// </summary>
/// <param name="Type">The kind of text being input, or null for SDL's default (plain text).</param>
/// <param name="Capitalization">The auto-capitalization mode, or null for SDL's type-dependent default.</param>
/// <param name="Autocorrect">Whether auto completion/correction is enabled, or null for SDL's default (true).</param>
/// <param name="Multiline">Whether multiple lines are allowed, or null for SDL's hint-dependent default.</param>
public readonly record struct TextInputProperties(
    TextInputType? Type = null,
    Capitalization? Capitalization = null,
    bool? Autocorrect = null,
    bool? Multiline = null);
