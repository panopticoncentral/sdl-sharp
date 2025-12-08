using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Flags for OpenPopup*(), BeginPopupContext*(), IsPopupOpen() functions.
/// </summary>
/// <remarks>
/// <para>To be backward compatible with older API which took an 'int mouse_button = 1' argument instead of flags,
/// we need to treat small flags values as a mouse button index, so we encode the mouse button in the first few bits of the flags.
/// It is therefore guaranteed to be legal to pass a mouse button index in PopupFlags.</para>
/// <para>IMPORTANT: because the default parameter is 1 (==MouseButtonRight), if you rely on the default parameter
/// and want to use another flag, you need to pass in the MouseButtonRight flag explicitly.</para>
/// </remarks>
[Flags]
public enum PopupFlags
{
    /// <summary>
    /// No flags set.
    /// </summary>
    None = ImGuiPopupFlags.None,

    /// <summary>
    /// For BeginPopupContext*(): open on Left Mouse release. Guaranteed to always be == 0 (same as MouseButton.Left).
    /// </summary>
    MouseButtonLeft = ImGuiPopupFlags.MouseButtonLeft,

    /// <summary>
    /// For BeginPopupContext*(): open on Right Mouse release. Guaranteed to always be == 1 (same as MouseButton.Right).
    /// </summary>
    MouseButtonRight = ImGuiPopupFlags.MouseButtonRight,

    /// <summary>
    /// For BeginPopupContext*(): open on Middle Mouse release. Guaranteed to always be == 2 (same as MouseButton.Middle).
    /// </summary>
    MouseButtonMiddle = ImGuiPopupFlags.MouseButtonMiddle,

    /// <summary>
    /// For OpenPopup*(), BeginPopupContext*(): don't reopen same popup if already open (won't reposition, won't reinitialize navigation).
    /// </summary>
    NoReopen = ImGuiPopupFlags.NoReopen,

    /// <summary>
    /// For OpenPopup*(), BeginPopupContext*(): don't open if there's already a popup at the same level of the popup stack.
    /// </summary>
    NoOpenOverExistingPopup = ImGuiPopupFlags.NoOpenOverExistingPopup,

    /// <summary>
    /// For BeginPopupContextWindow(): don't return true when hovering items, only when hovering empty space.
    /// </summary>
    NoOpenOverItems = ImGuiPopupFlags.NoOpenOverItems,

    /// <summary>
    /// For IsPopupOpen(): ignore the ImGuiID parameter and test for any popup.
    /// </summary>
    AnyPopupId = ImGuiPopupFlags.AnyPopupId,

    /// <summary>
    /// For IsPopupOpen(): search/test at any level of the popup stack (default test in the current level).
    /// </summary>
    AnyPopupLevel = ImGuiPopupFlags.AnyPopupLevel,

    /// <summary>
    /// Combination of AnyPopupId | AnyPopupLevel.
    /// </summary>
    AnyPopup = ImGuiPopupFlags.AnyPopup
}
