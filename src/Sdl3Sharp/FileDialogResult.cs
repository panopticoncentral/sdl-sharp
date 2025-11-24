namespace Sdl3Sharp;

/// <summary>
/// Represents the result of a file dialog operation.
/// </summary>
public sealed class FileDialogResult
{
    /// <summary>
    /// Gets the files selected by the user, or an empty array if the dialog was cancelled.
    /// </summary>
    public string[] Files { get; }

    /// <summary>
    /// Gets the index of the filter that was selected, or -1 if no filter was selected
    /// or if the platform doesn't support fetching the selected filter.
    /// </summary>
    public int SelectedFilterIndex { get; }

    /// <summary>
    /// Gets a value indicating whether the user cancelled the dialog.
    /// </summary>
    public bool IsCancelled => Files.Length == 0;

    /// <summary>
    /// Gets the selected file, or null if the dialog was cancelled or multiple files were selected.
    /// </summary>
    public string? SelectedFile => Files.Length == 1 ? Files[0] : null;

    internal FileDialogResult(string[] files, int selectedFilterIndex)
    {
        Files = files;
        SelectedFilterIndex = selectedFilterIndex;
    }
}
