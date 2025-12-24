using static Sdl3Sharp.ImGui.Native.ImGui;

namespace Sdl3Sharp.ImGui;

public unsafe readonly ref struct ComboScope: IDisposable
{
    private readonly bool _isOpen;

    private ComboScope(bool isOpen)
    {
        _isOpen = isOpen;
    }

    /// <summary>
    /// Begins a combo box (dropdown). Must be followed by <see cref="EndCombo"/> if this returns true.
    /// </summary>
    /// <param name="label">The label for the combo box.</param>
    /// <param name="previewValue">The preview value displayed when closed.</param>
    /// <param name="flags">Combo box behavior flags.</param>
    /// <returns>True if the combo box is open and items should be rendered.</returns>
    /// <remarks>
    /// The BeginCombo()/EndCombo() API allows you to manage your contents and selection state however you want,
    /// by creating e.g. Selectable() items.
    /// </remarks>
    public static ComboScope Begin(ref ComboScope scope, ReadOnlySpan<byte> label, ReadOnlySpan<byte> previewValue, ComboFlags flags = ComboFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* previewPtr = previewValue)
        {
            return new(ImGui_BeginCombo(labelPtr, previewPtr, (Native.ImGuiComboFlags)flags));
        }
    }

    public void Dispose()
    {
        if (_isOpen)
        {
            ImGui_EndCombo();
        }
    }
}
