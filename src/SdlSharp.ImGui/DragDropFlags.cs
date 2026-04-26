namespace SdlSharp.Gui;

/// <summary>Flags for <see cref="Gui.BeginDragDropSource"/> and <see cref="Gui.AcceptDragDropPayload"/>.</summary>
[Flags]
public enum DragDropFlags
{
    /// <summary>No flags.</summary>
    None = 0,

    // BeginDragDropSource flags
    /// <summary>Disable the preview tooltip.</summary>
    SourceNoPreviewTooltip = 1 << 0,
    /// <summary>By default, when dragging we clear data so IsItemHovered returns false. This flag disables that.</summary>
    SourceNoDisableHover = 1 << 1,
    /// <summary>Disable the behavior that allows opening tree nodes / collapsing headers by holding over them while dragging.</summary>
    SourceNoHoldToOpenOthers = 1 << 2,
    /// <summary>Allow items such as Text, Image that have no unique identifier to be used as drag source.</summary>
    SourceAllowNullID = 1 << 3,
    /// <summary>External source (from outside of dear imgui); always returns true.</summary>
    SourceExtern = 1 << 4,
    /// <summary>Automatically expire the payload if the source ceases to be submitted.</summary>
    PayloadAutoExpire = 1 << 5,
    /// <summary>Hint to specify that the payload may not be copied outside the current ImGui context.</summary>
    PayloadNoCrossContext = 1 << 6,
    /// <summary>Hint to specify that the payload may not be copied outside the current process.</summary>
    PayloadNoCrossProcess = 1 << 7,

    // AcceptDragDropPayload flags
    /// <summary>AcceptDragDropPayload returns true even before the mouse button is released. Then call <see cref="DragDropPayload.IsDelivery"/> to test delivery.</summary>
    AcceptBeforeDelivery = 1 << 10,
    /// <summary>Do not draw the default highlight rectangle when hovering over target.</summary>
    AcceptNoDrawDefaultRect = 1 << 11,
    /// <summary>Request hiding the source tooltip from the target site.</summary>
    AcceptNoPreviewTooltip = 1 << 12,
    /// <summary>Accepting item will render as if hovered (useful for e.g. a Button used as drop target).</summary>
    AcceptDrawAsHovered = 1 << 13,
    /// <summary>For peeking ahead and inspecting the payload before delivery.</summary>
    AcceptPeekOnly = AcceptBeforeDelivery | AcceptNoDrawDefaultRect,
}
