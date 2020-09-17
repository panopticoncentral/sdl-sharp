using SdlSharp;
using SdlSharp.Graphics;
using SdlSharp.Input;

using Application app = new (Subsystems.Video, fontSupport: true);
Size windowSize = (640, 480);
Rectangle windowRectangle = (Window.UndefinedWindowLocation, windowSize);
using var window = Window.Create("Text", windowRectangle, WindowFlags.Shown);
using var renderer = Renderer.Create(window, -1, RendererFlags.Accelerated | RendererFlags.PresentVSync);
using var font = Font.Create("SDS_8x8.ttf", 12);
using var textTexture = font.RenderSolid("The quick brown fox jumped over the lazy dog", Colors.Black, renderer);

Color textColor = new (0, 0, 0, 0xFF);
var inputText = "Some editable text";
var inputTextTexture = font.RenderSolid(inputText, textColor, renderer);
var renderText = false;

Keyboard.StartTextInput();

Keyboard.KeyDown += (s, e) =>
{
    switch (e.Keycode)
    {
        case Keycode.Backspace:
            if (inputText.Length > 0)
            {
                inputText = inputText[0..^1];
                renderText = true;
            }
            break;
        case Keycode.c:
            if ((Keyboard.KeyModifierState & KeyModifier.Ctrl) != 0)
            {
                Clipboard.Text = inputText;
            }
            break;
        case Keycode.v:
            if ((Keyboard.KeyModifierState & KeyModifier.Ctrl) != 0)
            {
                inputText = Clipboard.Text ?? string.Empty;
                renderText = true;
            }
            break;
    }
};

Keyboard.TextInput += (s, e) =>
{
    if (!((e.Text[0] == 'c' || e.Text[0] == 'C') && (e.Text[0] == 'v' || e.Text[0] == 'V') && ((Keyboard.KeyModifierState & KeyModifier.Ctrl) != 0)))
    {
        inputText += e.Text;
        renderText = true;
    }
};

while (app.DispatchEvent())
{
    renderer.DrawColor = Colors.White;
    renderer.Clear();

    if (renderText)
    {
        inputTextTexture.Dispose();
        inputTextTexture = font.RenderSolid(string.IsNullOrEmpty(inputText) ? " " : inputText, textColor, renderer);
        renderText = false;
    }

    renderer.Copy(textTexture, null, (Point.Origin, textTexture.Size));
    renderer.Copy(inputTextTexture, null, ((0, textTexture.Size.Height * 2), inputTextTexture.Size));

    renderer.Present();
}
