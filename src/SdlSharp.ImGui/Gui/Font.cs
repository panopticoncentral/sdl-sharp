namespace SdlSharp.Gui;

/// <summary>
/// A handle to an ImGui font loaded into the font atlas. Obtain via
/// <see cref="FontAtlas"/> methods (e.g. <see cref="FontAtlas.AddFontFromFileTTF"/>)
/// or from <see cref="Gui.GetFont"/>. The handle is owned by the font atlas — do not dispose.
/// </summary>
public readonly unsafe struct Font
{
    internal readonly void* Handle;

    internal Font(void* handle) => Handle = handle;

    /// <summary>True if this handle refers to a valid font.</summary>
    public bool IsValid => Handle != null;
}
