using Sdl3Sharp;
using Sdl3Sharp.Graphics;

MessageBox.Show(MessageBoxFlags.Information, "Test", "This is a test.", null);

while (MessageBox.ShowCustom(
    MessageBoxFlags.Error | MessageBoxFlags.ButtonsLeftToRight,
    "Another Test",
    "This is another test.",
    [
        new("Return", 1, MessageBoxButtonFlags.ReturnKeyDefault),
        new("Escape", 2, MessageBoxButtonFlags.EscapeKeyDefault),
        new("Quit", 3, MessageBoxButtonFlags.None)
    ],
    null,
    new MessageBoxColorScheme
    (
        Colors.Blue,
        Colors.White,
        Colors.Grey,
        Colors.Black,
        Colors.DarkerGrey
    )) != 3)
{
}
