using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Flags for BeginDragDropSource(), AcceptDragDropPayload().
/// </summary>
[Flags]
public enum DragDropFlags
{
    /// <summary>
    /// No flags set.
    /// </summary>
    None = ImGuiDragDropFlags.None,

    /// <summary>
    /// Disable preview tooltip. By default, a successful call to BeginDragDropSource opens a tooltip so you can display a preview or description of the source contents. This flag disables this behavior.
    /// </summary>
    SourceNoPreviewTooltip = ImGuiDragDropFlags.SourceNoPreviewTooltip,

    /// <summary>
    /// By default, when dragging we clear data so that IsItemHovered() will return false, to avoid subsequent user code submitting tooltips. This flag disables this behavior so you can still call IsItemHovered() on the source item.
    /// </summary>
    SourceNoDisableHover = ImGuiDragDropFlags.SourceNoDisableHover,

    /// <summary>
    /// Disable the behavior that allows to open tree nodes and collapsing header by holding over them while dragging a source item.
    /// </summary>
    SourceNoHoldToOpenOthers = ImGuiDragDropFlags.SourceNoHoldToOpenOthers,

    /// <summary>
    /// Allow items such as Text(), Image() that have no unique identifier to be used as drag source, by manufacturing a temporary identifier based on their window-relative position.
    /// </summary>
    SourceAllowNullID = ImGuiDragDropFlags.SourceAllowNullID,

    /// <summary>
    /// External source (from outside of dear imgui), won't attempt to read current item/window info. Will always return true. Only one Extern source can be active simultaneously.
    /// </summary>
    SourceExtern = ImGuiDragDropFlags.SourceExtern,

    /// <summary>
    /// Automatically expire the payload if the source cease to be submitted (otherwise payloads are persisting while being dragged).
    /// </summary>
    PayloadAutoExpire = ImGuiDragDropFlags.PayloadAutoExpire,

    /// <summary>
    /// Hint to specify that the payload may not be copied outside current dear imgui context.
    /// </summary>
    PayloadNoCrossContext = ImGuiDragDropFlags.PayloadNoCrossContext,

    /// <summary>
    /// Hint to specify that the payload may not be copied outside current process.
    /// </summary>
    PayloadNoCrossProcess = ImGuiDragDropFlags.PayloadNoCrossProcess,

    /// <summary>
    /// AcceptDragDropPayload() will returns true even before the mouse button is released. You can then call IsDelivery() to test if the payload needs to be delivered.
    /// </summary>
    AcceptBeforeDelivery = ImGuiDragDropFlags.AcceptBeforeDelivery,

    /// <summary>
    /// Do not draw the default highlight rectangle when hovering over target.
    /// </summary>
    AcceptNoDrawDefaultRect = ImGuiDragDropFlags.AcceptNoDrawDefaultRect,

    /// <summary>
    /// Request hiding the BeginDragDropSource tooltip from the BeginDragDropTarget site.
    /// </summary>
    AcceptNoPreviewTooltip = ImGuiDragDropFlags.AcceptNoPreviewTooltip,

    /// <summary>
    /// Accepting item will render as if hovered. Useful for e.g. a Button() used as a drop target.
    /// </summary>
    AcceptDrawAsHovered = ImGuiDragDropFlags.AcceptDrawAsHovered,

    /// <summary>
    /// For peeking ahead and inspecting the payload before delivery.
    /// </summary>
    AcceptPeekOnly = ImGuiDragDropFlags.AcceptPeekOnly
}
