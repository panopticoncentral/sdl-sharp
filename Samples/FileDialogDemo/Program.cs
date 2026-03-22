// FileDialogDemo sample — shows a file open dialog and prints selected paths.
// Exercises: Window.Create, FileDialog.OpenFileAsync, DialogFileFilter, DialogResult.
// Uses the async Task-based API. The dialog is modal to the created window.
// Run interactively; selecting files or cancelling both print the outcome.

using SdlSharp;
using SdlSharp.Graphics;

using var app = new Application(InitFlags.Video);

Console.WriteLine("=== File Dialog Demo ===");
Console.WriteLine("Opening an 'Open File' dialog...");
Console.WriteLine("(Select one or more files, or cancel.)");
Console.WriteLine();

using var window = Window.Create("FileDialogDemo", 400, 200);

var filters = new DialogFileFilter[]
{
    new("Image files", "png;jpg;jpeg;bmp;gif;tiff;webp"),
    new("Text files",  "txt;md;csv;log"),
    new("All files",   "*"),
};

var result = await FileDialog.OpenFileAsync(
    parent: window,
    filters: filters,
    defaultLocation: null,
    allowMany: true);

Console.WriteLine("=== Result ===");
if (result.Files is { Length: > 0 } files)
{
    Console.WriteLine($"  Filter selected: [{result.FilterIndex}] {filters[Math.Clamp(result.FilterIndex, 0, filters.Length - 1)].Name}");
    Console.WriteLine($"  Files selected ({files.Length}):");
    foreach (var f in files)
        Console.WriteLine($"    {f}");
}
else
{
    Console.WriteLine("  Dialog cancelled (no files selected).");
}

Console.WriteLine();
Console.WriteLine("Done.");
