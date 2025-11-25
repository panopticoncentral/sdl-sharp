namespace Sdl3Sharp;

/// <summary>
/// Represents a color scheme for a message box.
/// </summary>
public sealed class MessageBoxColorScheme
{
    private readonly MessageBoxColor[] _colors = new MessageBoxColor[5];

    /// <summary>
    /// Gets or sets a color in the color scheme.
    /// </summary>
    /// <param name="type">The type of color to get or set.</param>
    /// <returns>The color for the specified type.</returns>
    public MessageBoxColor this[MessageBoxColorType type]
    {
        get => _colors[(int)type];
        set => _colors[(int)type] = value;
    }

    /// <summary>
    /// Gets or sets the background color.
    /// </summary>
    public MessageBoxColor Background
    {
        get => this[MessageBoxColorType.Background];
        set => this[MessageBoxColorType.Background] = value;
    }

    /// <summary>
    /// Gets or sets the text color.
    /// </summary>
    public MessageBoxColor Text
    {
        get => this[MessageBoxColorType.Text];
        set => this[MessageBoxColorType.Text] = value;
    }

    /// <summary>
    /// Gets or sets the button border color.
    /// </summary>
    public MessageBoxColor ButtonBorder
    {
        get => this[MessageBoxColorType.ButtonBorder];
        set => this[MessageBoxColorType.ButtonBorder] = value;
    }

    /// <summary>
    /// Gets or sets the button background color.
    /// </summary>
    public MessageBoxColor ButtonBackground
    {
        get => this[MessageBoxColorType.ButtonBackground];
        set => this[MessageBoxColorType.ButtonBackground] = value;
    }

    /// <summary>
    /// Gets or sets the selected button color.
    /// </summary>
    public MessageBoxColor ButtonSelected
    {
        get => this[MessageBoxColorType.ButtonSelected];
        set => this[MessageBoxColorType.ButtonSelected] = value;
    }

    internal MessageBoxColor[] GetColors()
    {
        return _colors;
    }
}
