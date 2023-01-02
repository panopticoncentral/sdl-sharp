using SdlSharp;

Application.ShowMessageBox(MessageBoxType.Information, "Test", "This is a test.", null);

while (Application.ShowMessageBox(
    MessageBoxType.Error | MessageBoxType.ButtonsLeftToRight,
    null,
    "Another Test",
    "This is another test.",
    new MessageBoxButton[]
    {
        new(MessageBoxButtonOptions.ReturnKeyDefault, 1, "Return"),
        new MessageBoxButton(MessageBoxButtonOptions.EscapeKeyDefault, 2, "Escape"),
        new MessageBoxButton(MessageBoxButtonOptions.None, 3, "Quit")
    },
    new MessageBoxColorScheme
    (
        new(0, 0, 255),
        new(255, 255, 255),
        new(128, 128, 128),
        new(0, 0, 0),
        new(64, 64, 64)
    )) != 3)
{
}
