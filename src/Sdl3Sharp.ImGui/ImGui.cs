using System.Runtime.InteropServices;
using System.Text;
using static Sdl3Sharp.ImGui.Native.ImGui;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Provides high-level wrapper methods for Dear ImGui functionality.
/// </summary>
public static unsafe class ImGui
{
    #region Widgets: Color Editor/Picker

    /// <summary>
    /// Displays a color button that opens a color picker when clicked.
    /// </summary>
    /// <param name="descId">Description ID for the button.</param>
    /// <param name="col">The color to display.</param>
    /// <param name="flags">Color edit behavior flags.</param>
    /// <returns>True when clicked.</returns>
    public static bool ColorButton(ReadOnlySpan<byte> descId, Vec4 col, ColorEditFlags flags = ColorEditFlags.None)
    {
        fixed (byte* ptr = descId)
        {
            return ImGui_ColorButton(ptr, col.Value, (Native.ImGuiColorEditFlags)flags);
        }
    }

    /// <summary>
    /// Displays a color button with explicit size that opens a color picker when clicked.
    /// </summary>
    /// <param name="descId">Description ID for the button.</param>
    /// <param name="col">The color to display.</param>
    /// <param name="flags">Color edit behavior flags.</param>
    /// <param name="size">The button size.</param>
    /// <returns>True when clicked.</returns>
    public static bool ColorButton(ReadOnlySpan<byte> descId, Vec4 col, ColorEditFlags flags, Vec2 size)
    {
        fixed (byte* ptr = descId)
        {
            return ImGui_ColorButtonEx(ptr, col.Value, (Native.ImGuiColorEditFlags)flags, size.Value);
        }
    }

    /// <summary>
    /// Sets the default color edit options.
    /// </summary>
    /// <param name="flags">Color edit behavior flags to use as defaults.</param>
    /// <remarks>
    /// Initialize current options (generally on application startup) if you want to select
    /// a default format, picker type, etc. User will be able to change many settings,
    /// unless you pass the NoOptions flag to your calls.
    /// </remarks>
    public static void SetColorEditOptions(ColorEditFlags flags)
    {
        ImGui_SetColorEditOptions((Native.ImGuiColorEditFlags)flags);
    }

    #endregion

    #region Widgets: Trees

    /// <summary>
    /// Creates a tree node.
    /// </summary>
    /// <param name="label">The node label.</param>
    /// <returns>True if the node is open. Call <see cref="TreePop"/> when done if this returns true.</returns>
    public static bool TreeNode(ReadOnlySpan<byte> label)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_TreeNode(ptr);
        }
    }

    /// <summary>
    /// Creates a tree node with flags.
    /// </summary>
    /// <param name="label">The node label.</param>
    /// <param name="flags">Tree node behavior flags.</param>
    /// <returns>True if the node is open. Call <see cref="TreePop"/> when done if this returns true.</returns>
    public static bool TreeNode(ReadOnlySpan<byte> label, TreeNodeFlags flags)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_TreeNodeEx(ptr, (Native.ImGuiTreeNodeFlags)flags);
        }
    }

    /// <summary>
    /// Pushes a tree node onto the stack (equivalent to Indent + PushID).
    /// </summary>
    /// <param name="strId">The string ID to push.</param>
    /// <remarks>
    /// Already called by TreeNode() when returning true, but you can call TreePush/TreePop yourself if desired.
    /// </remarks>
    public static void TreePush(ReadOnlySpan<byte> strId)
    {
        fixed (byte* ptr = strId)
        {
            ImGui_TreePush(ptr);
        }
    }

    /// <summary>
    /// Pushes a tree node onto the stack using a pointer ID.
    /// </summary>
    /// <param name="ptrId">The pointer ID to push.</param>
    public static void TreePush(nint ptrId)
    {
        ImGui_TreePushPtr((void*)ptrId);
    }

    /// <summary>
    /// Pops a tree node from the stack (equivalent to Unindent + PopID).
    /// </summary>
    public static void TreePop()
    {
        ImGui_TreePop();
    }

    /// <summary>
    /// Gets the horizontal distance preceding a label when using TreeNode or Bullet.
    /// </summary>
    /// <returns>The spacing in pixels.</returns>
    public static float GetTreeNodeToLabelSpacing()
    {
        return ImGui_GetTreeNodeToLabelSpacing();
    }

    /// <summary>
    /// Creates a collapsing header.
    /// </summary>
    /// <param name="label">The header label.</param>
    /// <param name="flags">Tree node behavior flags.</param>
    /// <returns>True if the header is open.</returns>
    /// <remarks>
    /// Doesn't indent or push onto the ID stack. User doesn't have to call TreePop().
    /// </remarks>
    public static bool CollapsingHeader(ReadOnlySpan<byte> label, TreeNodeFlags flags = TreeNodeFlags.None)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_CollapsingHeader(ptr, (Native.ImGuiTreeNodeFlags)flags);
        }
    }

    /// <summary>
    /// Creates a collapsing header with a close button.
    /// </summary>
    /// <param name="label">The header label.</param>
    /// <param name="pVisible">Reference to visibility state. If false, header is not displayed.</param>
    /// <param name="flags">Tree node behavior flags.</param>
    /// <returns>True if the header is open.</returns>
    public static bool CollapsingHeader(ReadOnlySpan<byte> label, StateRef<bool> pVisible, TreeNodeFlags flags = TreeNodeFlags.None)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_CollapsingHeaderBoolPtr(ptr, pVisible.Ptr, (Native.ImGuiTreeNodeFlags)flags);
        }
    }

    /// <summary>
    /// Sets the next TreeNode/CollapsingHeader open state.
    /// </summary>
    /// <param name="isOpen">Whether the node should be open.</param>
    /// <param name="cond">Condition for applying the state.</param>
    public static void SetNextItemOpen(bool isOpen, Cond cond = Cond.None)
    {
        ImGui_SetNextItemOpen(isOpen, (Native.ImGuiCond)cond);
    }

    /// <summary>
    /// Sets the ID to use for open/close storage (default is same as item ID).
    /// </summary>
    /// <param name="storageId">The storage ID.</param>
    public static void SetNextItemStorageID(Id storageId)
    {
        ImGui_SetNextItemStorageID(storageId.Value);
    }

    #endregion

    #region Widgets: Selectables

    /// <summary>
    /// Creates a selectable item.
    /// </summary>
    /// <param name="label">The item label.</param>
    /// <returns>True when clicked.</returns>
    public static bool Selectable(ReadOnlySpan<byte> label)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_Selectable(ptr);
        }
    }

    /// <summary>
    /// Creates a selectable item with explicit parameters.
    /// </summary>
    /// <param name="label">The item label.</param>
    /// <param name="selected">Whether the item is currently selected (read-only).</param>
    /// <param name="flags">Selectable behavior flags.</param>
    /// <param name="size">The item size.</param>
    /// <returns>True when clicked.</returns>
    public static bool Selectable(ReadOnlySpan<byte> label, bool selected, SelectableFlags flags = SelectableFlags.None, Vec2 size = default)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_SelectableEx(ptr, selected, (Native.ImGuiSelectableFlags)flags, size.Value);
        }
    }

    /// <summary>
    /// Creates a selectable item with mutable selection state.
    /// </summary>
    /// <param name="label">The item label.</param>
    /// <param name="pSelected">Reference to selection state (read-write).</param>
    /// <param name="flags">Selectable behavior flags.</param>
    /// <returns>True when clicked.</returns>
    public static bool Selectable(ReadOnlySpan<byte> label, StateRef<bool> pSelected, SelectableFlags flags = SelectableFlags.None)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_SelectableBoolPtr(ptr, pSelected.Ptr, (Native.ImGuiSelectableFlags)flags);
        }
    }

    /// <summary>
    /// Creates a selectable item with mutable selection state and explicit size.
    /// </summary>
    /// <param name="label">The item label.</param>
    /// <param name="pSelected">Reference to selection state (read-write).</param>
    /// <param name="flags">Selectable behavior flags.</param>
    /// <param name="size">The item size.</param>
    /// <returns>True when clicked.</returns>
    public static bool Selectable(ReadOnlySpan<byte> label, StateRef<bool> pSelected, SelectableFlags flags, Vec2 size)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_SelectableBoolPtrEx(ptr, pSelected.Ptr, (Native.ImGuiSelectableFlags)flags, size.Value);
        }
    }

    #endregion

    #region Widgets: List Boxes

    /// <summary>
    /// Begins a list box. Must be followed by <see cref="EndListBox"/> if this returns true.
    /// </summary>
    /// <param name="label">The label for the list box.</param>
    /// <param name="size">The size of the list box.</param>
    /// <returns>True if the list box is open and items should be rendered.</returns>
    public static bool BeginListBox(ReadOnlySpan<byte> label, Vec2 size = default)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_BeginListBox(ptr, size.Value);
        }
    }

    /// <summary>
    /// Ends a list box. Only call this if <see cref="BeginListBox"/> returned true.
    /// </summary>
    public static void EndListBox()
    {
        ImGui_EndListBox();
    }

    #endregion

    #region Widgets: Data Plotting

    /// <summary>
    /// Plots a line graph from an array of values.
    /// </summary>
    /// <param name="label">The label for the plot.</param>
    /// <param name="values">The array of values to plot.</param>
    public static void PlotLines(ReadOnlySpan<byte> label, ReadOnlySpan<float> values)
    {
        fixed (byte* labelPtr = label)
        fixed (float* valuesPtr = values)
        {
            ImGui_PlotLines(labelPtr, valuesPtr, values.Length);
        }
    }

    /// <summary>
    /// Plots a line graph from an array of values with extended options.
    /// </summary>
    /// <param name="label">The label for the plot.</param>
    /// <param name="values">The array of values to plot.</param>
    /// <param name="valuesOffset">Index offset into the values array.</param>
    /// <param name="overlayText">Text to overlay on the graph.</param>
    /// <param name="scaleMin">The minimum scale value (float.MaxValue for auto).</param>
    /// <param name="scaleMax">The maximum scale value (float.MaxValue for auto).</param>
    /// <param name="graphSize">The size of the graph (0,0 for default).</param>
    public static void PlotLines(ReadOnlySpan<byte> label, ReadOnlySpan<float> values, int valuesOffset, ReadOnlySpan<byte> overlayText, float scaleMin = float.MaxValue, float scaleMax = float.MaxValue, Vec2 graphSize = default)
    {
        fixed (byte* labelPtr = label)
        fixed (float* valuesPtr = values)
        fixed (byte* overlayPtr = overlayText)
        {
            ImGui_PlotLinesEx(labelPtr, valuesPtr, values.Length, valuesOffset, overlayPtr, scaleMin, scaleMax, graphSize.Value, sizeof(float));
        }
    }

    /// <summary>
    /// Plots a histogram from an array of values.
    /// </summary>
    /// <param name="label">The label for the plot.</param>
    /// <param name="values">The array of values to plot.</param>
    public static void PlotHistogram(ReadOnlySpan<byte> label, ReadOnlySpan<float> values)
    {
        fixed (byte* labelPtr = label)
        fixed (float* valuesPtr = values)
        {
            ImGui_PlotHistogram(labelPtr, valuesPtr, values.Length);
        }
    }

    /// <summary>
    /// Plots a histogram from an array of values with extended options.
    /// </summary>
    /// <param name="label">The label for the plot.</param>
    /// <param name="values">The array of values to plot.</param>
    /// <param name="valuesOffset">Index offset into the values array.</param>
    /// <param name="overlayText">Text to overlay on the graph.</param>
    /// <param name="scaleMin">The minimum scale value (float.MaxValue for auto).</param>
    /// <param name="scaleMax">The maximum scale value (float.MaxValue for auto).</param>
    /// <param name="graphSize">The size of the graph (0,0 for default).</param>
    public static void PlotHistogram(ReadOnlySpan<byte> label, ReadOnlySpan<float> values, int valuesOffset, ReadOnlySpan<byte> overlayText, float scaleMin = float.MaxValue, float scaleMax = float.MaxValue, Vec2 graphSize = default)
    {
        fixed (byte* labelPtr = label)
        fixed (float* valuesPtr = values)
        fixed (byte* overlayPtr = overlayText)
        {
            ImGui_PlotHistogramEx(labelPtr, valuesPtr, values.Length, valuesOffset, overlayPtr, scaleMin, scaleMax, graphSize.Value, sizeof(float));
        }
    }

    #endregion

    #region Widgets: Menus

    /// <summary>
    /// Begins appending to a menu bar of the current window.
    /// </summary>
    /// <returns>True if the menu bar is visible. Only call <see cref="EndMenuBar"/> if this returns true.</returns>
    /// <remarks>
    /// Requires the parent window to have the MenuBar window flag set.
    /// </remarks>
    public static bool BeginMenuBar()
    {
        return ImGui_BeginMenuBar();
    }

    /// <summary>
    /// Ends appending to the menu bar. Only call if <see cref="BeginMenuBar"/> returned true.
    /// </summary>
    public static void EndMenuBar()
    {
        ImGui_EndMenuBar();
    }

    /// <summary>
    /// Creates and appends to a full-screen menu bar.
    /// </summary>
    /// <returns>True if the main menu bar is visible. Only call <see cref="EndMainMenuBar"/> if this returns true.</returns>
    public static bool BeginMainMenuBar()
    {
        return ImGui_BeginMainMenuBar();
    }

    /// <summary>
    /// Ends the main menu bar. Only call if <see cref="BeginMainMenuBar"/> returned true.
    /// </summary>
    public static void EndMainMenuBar()
    {
        ImGui_EndMainMenuBar();
    }

    /// <summary>
    /// Creates a sub-menu entry.
    /// </summary>
    /// <param name="label">The menu label.</param>
    /// <returns>True if the menu is open. Only call <see cref="EndMenu"/> if this returns true.</returns>
    public static bool BeginMenu(ReadOnlySpan<byte> label)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_BeginMenu(ptr);
        }
    }

    /// <summary>
    /// Creates a sub-menu entry with explicit enabled state.
    /// </summary>
    /// <param name="label">The menu label.</param>
    /// <param name="enabled">Whether the menu is enabled.</param>
    /// <returns>True if the menu is open. Only call <see cref="EndMenu"/> if this returns true.</returns>
    public static bool BeginMenu(ReadOnlySpan<byte> label, bool enabled)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_BeginMenuEx(ptr, enabled);
        }
    }

    /// <summary>
    /// Ends a menu. Only call if <see cref="BeginMenu"/> returned true.
    /// </summary>
    public static void EndMenu()
    {
        ImGui_EndMenu();
    }

    /// <summary>
    /// Creates a menu item.
    /// </summary>
    /// <param name="label">The item label.</param>
    /// <returns>True when activated.</returns>
    public static bool MenuItem(ReadOnlySpan<byte> label)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_MenuItem(ptr);
        }
    }

    /// <summary>
    /// Creates a menu item with explicit parameters.
    /// </summary>
    /// <param name="label">The item label.</param>
    /// <param name="shortcut">Optional shortcut text displayed on the right.</param>
    /// <param name="selected">Whether to show a check mark.</param>
    /// <param name="enabled">Whether the item is enabled.</param>
    /// <returns>True when activated.</returns>
    public static bool MenuItem(ReadOnlySpan<byte> label, ReadOnlySpan<byte> shortcut, bool selected = false, bool enabled = true)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* shortcutPtr = shortcut)
        {
            return ImGui_MenuItemEx(labelPtr, shortcutPtr, selected, enabled);
        }
    }

    /// <summary>
    /// Creates a menu item with mutable selection state.
    /// </summary>
    /// <param name="label">The item label.</param>
    /// <param name="shortcut">Optional shortcut text displayed on the right.</param>
    /// <param name="pSelected">Reference to selection state (toggles on activation).</param>
    /// <param name="enabled">Whether the item is enabled.</param>
    /// <returns>True when activated.</returns>
    public static bool MenuItem(ReadOnlySpan<byte> label, ReadOnlySpan<byte> shortcut, StateRef<bool> pSelected, bool enabled = true)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* shortcutPtr = shortcut)
        {
            return ImGui_MenuItemBoolPtr(labelPtr, shortcutPtr, pSelected.Ptr, enabled);
        }
    }

    #endregion

    #region Tooltips

    /// <summary>
    /// Begins a tooltip window.
    /// </summary>
    /// <returns>True if the tooltip is visible. Only call <see cref="EndTooltip"/> if this returns true.</returns>
    public static bool BeginTooltip()
    {
        return ImGui_BeginTooltip();
    }

    /// <summary>
    /// Ends a tooltip window. Only call if <see cref="BeginTooltip"/> or <see cref="BeginItemTooltip"/> returned true.
    /// </summary>
    public static void EndTooltip()
    {
        ImGui_EndTooltip();
    }

    /// <summary>
    /// Begins a tooltip window if the preceding item was hovered.
    /// </summary>
    /// <returns>True if the tooltip is visible. Only call <see cref="EndTooltip"/> if this returns true.</returns>
    /// <remarks>
    /// Shortcut for: if (IsItemHovered(HoveredFlags.ForTooltip) &amp;&amp; BeginTooltip())
    /// </remarks>
    public static bool BeginItemTooltip()
    {
        return ImGui_BeginItemTooltip();
    }

    /// <summary>
    /// Sets a text-only tooltip.
    /// </summary>
    /// <param name="text">The tooltip text.</param>
    /// <remarks>
    /// Often used after an IsItemHovered() check. Overrides any previous call to SetTooltip().
    /// </remarks>
    public static void SetTooltip(ReadOnlySpan<byte> text)
    {
        fixed (byte* ptr = text)
        {
            ImGui_SetTooltip(ptr);
        }
    }

    #endregion

    #region Popups, Modals

    /// <summary>
    /// Begins a popup window.
    /// </summary>
    /// <param name="strId">The popup string ID.</param>
    /// <param name="flags">Window behavior flags.</param>
    /// <returns>True if the popup is open. Only call <see cref="EndPopup"/> if this returns true.</returns>
    public static bool BeginPopup(ReadOnlySpan<byte> strId, WindowFlags flags = WindowFlags.None)
    {
        fixed (byte* ptr = strId)
        {
            return ImGui_BeginPopup(ptr, (Native.ImGuiWindowFlags)flags);
        }
    }

    /// <summary>
    /// Begins a modal popup window.
    /// </summary>
    /// <param name="name">The modal name.</param>
    /// <param name="pOpen">Optional reference to open state. If provided, shows a close button.</param>
    /// <param name="flags">Window behavior flags.</param>
    /// <returns>True if the modal is open. Only call <see cref="EndPopup"/> if this returns true.</returns>
    /// <remarks>
    /// Modal windows block all interaction behind them and cannot be closed by clicking outside.
    /// </remarks>
    public static bool BeginPopupModal(ReadOnlySpan<byte> name, StateRef<bool>? pOpen = null, WindowFlags flags = WindowFlags.None)
    {
        fixed (byte* ptr = name)
        {
            return ImGui_BeginPopupModal(ptr, pOpen.HasValue ? pOpen.Value.Ptr : null, (Native.ImGuiWindowFlags)flags);
        }
    }

    /// <summary>
    /// Ends a popup window. Only call if BeginPopup/BeginPopupModal returned true.
    /// </summary>
    public static void EndPopup()
    {
        ImGui_EndPopup();
    }

    /// <summary>
    /// Opens a popup by string ID.
    /// </summary>
    /// <param name="strId">The popup string ID.</param>
    /// <param name="popupFlags">Popup behavior flags.</param>
    /// <remarks>
    /// Call to mark popup as open (don't call every frame!).
    /// </remarks>
    public static void OpenPopup(ReadOnlySpan<byte> strId, PopupFlags popupFlags = PopupFlags.None)
    {
        fixed (byte* ptr = strId)
        {
            ImGui_OpenPopup(ptr, (Native.ImGuiPopupFlags)popupFlags);
        }
    }

    /// <summary>
    /// Opens a popup by ID.
    /// </summary>
    /// <param name="id">The popup ID.</param>
    /// <param name="popupFlags">Popup behavior flags.</param>
    public static void OpenPopup(Id id, PopupFlags popupFlags = PopupFlags.None)
    {
        ImGui_OpenPopupID(id.Value, (Native.ImGuiPopupFlags)popupFlags);
    }

    /// <summary>
    /// Helper to open a popup when the last item was clicked.
    /// </summary>
    /// <param name="strId">The popup string ID. Use null to associate with previous item.</param>
    /// <param name="popupFlags">Popup behavior flags. Defaults to right mouse button.</param>
    public static void OpenPopupOnItemClick(ReadOnlySpan<byte> strId = default, PopupFlags popupFlags = PopupFlags.MouseButtonRight)
    {
        fixed (byte* ptr = strId)
        {
            ImGui_OpenPopupOnItemClick(ptr, (Native.ImGuiPopupFlags)popupFlags);
        }
    }

    /// <summary>
    /// Manually closes the current popup.
    /// </summary>
    public static void CloseCurrentPopup()
    {
        ImGui_CloseCurrentPopup();
    }

    /// <summary>
    /// Opens and begins a popup when the last item was clicked.
    /// </summary>
    /// <returns>True if the popup is open.</returns>
    public static bool BeginPopupContextItem()
    {
        return ImGui_BeginPopupContextItem();
    }

    /// <summary>
    /// Opens and begins a popup when the last item was clicked, with explicit parameters.
    /// </summary>
    /// <param name="strId">The popup string ID. Use null to associate with previous item.</param>
    /// <param name="popupFlags">Popup behavior flags. Defaults to right mouse button.</param>
    /// <returns>True if the popup is open.</returns>
    public static bool BeginPopupContextItem(ReadOnlySpan<byte> strId, PopupFlags popupFlags = PopupFlags.MouseButtonRight)
    {
        fixed (byte* ptr = strId)
        {
            return ImGui_BeginPopupContextItemEx(ptr, (Native.ImGuiPopupFlags)popupFlags);
        }
    }

    /// <summary>
    /// Opens and begins a popup when the current window was clicked.
    /// </summary>
    /// <returns>True if the popup is open.</returns>
    public static bool BeginPopupContextWindow()
    {
        return ImGui_BeginPopupContextWindow();
    }

    /// <summary>
    /// Opens and begins a popup when the current window was clicked, with explicit parameters.
    /// </summary>
    /// <param name="strId">The popup string ID.</param>
    /// <param name="popupFlags">Popup behavior flags. Defaults to right mouse button.</param>
    /// <returns>True if the popup is open.</returns>
    public static bool BeginPopupContextWindow(ReadOnlySpan<byte> strId, PopupFlags popupFlags = PopupFlags.MouseButtonRight)
    {
        fixed (byte* ptr = strId)
        {
            return ImGui_BeginPopupContextWindowEx(ptr, (Native.ImGuiPopupFlags)popupFlags);
        }
    }

    /// <summary>
    /// Opens and begins a popup when clicking in void (where there are no windows).
    /// </summary>
    /// <returns>True if the popup is open.</returns>
    public static bool BeginPopupContextVoid()
    {
        return ImGui_BeginPopupContextVoid();
    }

    /// <summary>
    /// Opens and begins a popup when clicking in void, with explicit parameters.
    /// </summary>
    /// <param name="strId">The popup string ID.</param>
    /// <param name="popupFlags">Popup behavior flags. Defaults to right mouse button.</param>
    /// <returns>True if the popup is open.</returns>
    public static bool BeginPopupContextVoid(ReadOnlySpan<byte> strId, PopupFlags popupFlags = PopupFlags.MouseButtonRight)
    {
        fixed (byte* ptr = strId)
        {
            return ImGui_BeginPopupContextVoidEx(ptr, (Native.ImGuiPopupFlags)popupFlags);
        }
    }

    /// <summary>
    /// Checks if a popup is open.
    /// </summary>
    /// <param name="strId">The popup string ID.</param>
    /// <param name="flags">Popup flags for query behavior.</param>
    /// <returns>True if the popup is open.</returns>
    public static bool IsPopupOpen(ReadOnlySpan<byte> strId, PopupFlags flags = PopupFlags.None)
    {
        fixed (byte* ptr = strId)
        {
            return ImGui_IsPopupOpen(ptr, (Native.ImGuiPopupFlags)flags);
        }
    }

    #endregion

    #region Tables

    /// <summary>
    /// Begins a table.
    /// </summary>
    /// <param name="strId">The table string ID.</param>
    /// <param name="columns">The number of columns.</param>
    /// <param name="flags">Table behavior flags.</param>
    /// <returns>True if the table is visible. Only call <see cref="EndTable"/> if this returns true.</returns>
    public static bool BeginTable(ReadOnlySpan<byte> strId, int columns, TableFlags flags = TableFlags.None)
    {
        fixed (byte* ptr = strId)
        {
            return ImGui_BeginTable(ptr, columns, (Native.ImGuiTableFlags)flags);
        }
    }

    /// <summary>
    /// Begins a table with explicit size parameters.
    /// </summary>
    /// <param name="strId">The table string ID.</param>
    /// <param name="columns">The number of columns.</param>
    /// <param name="flags">Table behavior flags.</param>
    /// <param name="outerSize">The outer size of the table.</param>
    /// <param name="innerWidth">The inner width for scrolling.</param>
    /// <returns>True if the table is visible. Only call <see cref="EndTable"/> if this returns true.</returns>
    public static bool BeginTable(ReadOnlySpan<byte> strId, int columns, TableFlags flags, Vec2 outerSize, float innerWidth = 0.0f)
    {
        fixed (byte* ptr = strId)
        {
            return ImGui_BeginTableEx(ptr, columns, (Native.ImGuiTableFlags)flags, outerSize.Value, innerWidth);
        }
    }

    /// <summary>
    /// Ends a table. Only call if <see cref="BeginTable"/> returned true.
    /// </summary>
    public static void EndTable()
    {
        ImGui_EndTable();
    }

    /// <summary>
    /// Appends into the first cell of a new row.
    /// </summary>
    public static void TableNextRow()
    {
        ImGui_TableNextRow();
    }

    /// <summary>
    /// Appends into the first cell of a new row with explicit parameters.
    /// </summary>
    /// <param name="rowFlags">Row behavior flags.</param>
    /// <param name="minRowHeight">Minimum row height.</param>
    public static void TableNextRow(TableRowFlags rowFlags, float minRowHeight = 0.0f)
    {
        ImGui_TableNextRowEx((Native.ImGuiTableRowFlags)rowFlags, minRowHeight);
    }

    /// <summary>
    /// Appends into the next column (or first column of next row if currently in last column).
    /// </summary>
    /// <returns>True when the column is visible.</returns>
    public static bool TableNextColumn()
    {
        return ImGui_TableNextColumn();
    }

    /// <summary>
    /// Appends into the specified column.
    /// </summary>
    /// <param name="columnN">The column index.</param>
    /// <returns>True when the column is visible.</returns>
    public static bool TableSetColumnIndex(int columnN)
    {
        return ImGui_TableSetColumnIndex(columnN);
    }

    /// <summary>
    /// Sets up a column for the table.
    /// </summary>
    /// <param name="label">The column label.</param>
    /// <param name="flags">Column behavior flags.</param>
    public static void TableSetupColumn(ReadOnlySpan<byte> label, TableColumnFlags flags = TableColumnFlags.None)
    {
        fixed (byte* ptr = label)
        {
            ImGui_TableSetupColumn(ptr, (Native.ImGuiTableColumnFlags)flags);
        }
    }

    /// <summary>
    /// Sets up a column for the table with explicit parameters.
    /// </summary>
    /// <param name="label">The column label.</param>
    /// <param name="flags">Column behavior flags.</param>
    /// <param name="initWidthOrWeight">Initial width or weight depending on flags.</param>
    /// <param name="userId">User ID for the column.</param>
    public static void TableSetupColumn(ReadOnlySpan<byte> label, TableColumnFlags flags, float initWidthOrWeight, Id userId = default)
    {
        fixed (byte* ptr = label)
        {
            ImGui_TableSetupColumnEx(ptr, (Native.ImGuiTableColumnFlags)flags, initWidthOrWeight, userId.Value);
        }
    }

    /// <summary>
    /// Locks columns/rows so they stay visible when scrolled.
    /// </summary>
    /// <param name="cols">Number of columns to freeze.</param>
    /// <param name="rows">Number of rows to freeze.</param>
    public static void TableSetupScrollFreeze(int cols, int rows)
    {
        ImGui_TableSetupScrollFreeze(cols, rows);
    }

    /// <summary>
    /// Submits one header cell manually.
    /// </summary>
    /// <param name="label">The header label.</param>
    public static void TableHeader(ReadOnlySpan<byte> label)
    {
        fixed (byte* ptr = label)
        {
            ImGui_TableHeader(ptr);
        }
    }

    /// <summary>
    /// Submits a row with header cells based on data provided to <see cref="TableSetupColumn"/>.
    /// </summary>
    public static void TableHeadersRow()
    {
        ImGui_TableHeadersRow();
    }

    /// <summary>
    /// Submits a row with angled headers for every column with the AngledHeader flag.
    /// </summary>
    /// <remarks>
    /// Must be the first row.
    /// </remarks>
    public static void TableAngledHeadersRow()
    {
        ImGui_TableAngledHeadersRow();
    }

    /// <summary>
    /// Gets the number of columns in the current table.
    /// </summary>
    /// <returns>The column count.</returns>
    public static int TableGetColumnCount()
    {
        return ImGui_TableGetColumnCount();
    }

    /// <summary>
    /// Gets the current column index.
    /// </summary>
    /// <returns>The current column index.</returns>
    public static int TableGetColumnIndex()
    {
        return ImGui_TableGetColumnIndex();
    }

    /// <summary>
    /// Gets the current row index.
    /// </summary>
    /// <returns>The current row index.</returns>
    public static int TableGetRowIndex()
    {
        return ImGui_TableGetRowIndex();
    }

    /// <summary>
    /// Gets the column flags for a column.
    /// </summary>
    /// <param name="columnN">The column index. Use -1 for current column.</param>
    /// <returns>The column flags.</returns>
    public static TableColumnFlags TableGetColumnFlags(int columnN = -1)
    {
        return (TableColumnFlags)ImGui_TableGetColumnFlags(columnN);
    }

    /// <summary>
    /// Sets the enabled state of a column.
    /// </summary>
    /// <param name="columnN">The column index.</param>
    /// <param name="v">Whether the column is enabled.</param>
    public static void TableSetColumnEnabled(int columnN, bool v)
    {
        ImGui_TableSetColumnEnabled(columnN, v);
    }

    /// <summary>
    /// Gets the hovered column index.
    /// </summary>
    /// <returns>The hovered column index, or -1 if table is not hovered.</returns>
    public static int TableGetHoveredColumn()
    {
        return ImGui_TableGetHoveredColumn();
    }

    #endregion

    #region Tab Bars, Tabs

    /// <summary>
    /// Creates and appends into a tab bar.
    /// </summary>
    /// <param name="strId">The tab bar string ID.</param>
    /// <param name="flags">Tab bar behavior flags.</param>
    /// <returns>True if the tab bar is visible. Only call <see cref="EndTabBar"/> if this returns true.</returns>
    public static bool BeginTabBar(ReadOnlySpan<byte> strId, TabBarFlags flags = TabBarFlags.None)
    {
        fixed (byte* ptr = strId)
        {
            return ImGui_BeginTabBar(ptr, (Native.ImGuiTabBarFlags)flags);
        }
    }

    /// <summary>
    /// Ends a tab bar. Only call if <see cref="BeginTabBar"/> returned true.
    /// </summary>
    public static void EndTabBar()
    {
        ImGui_EndTabBar();
    }

    /// <summary>
    /// Creates a tab.
    /// </summary>
    /// <param name="label">The tab label.</param>
    /// <param name="pOpen">Optional reference to open state. If provided, shows a close button.</param>
    /// <param name="flags">Tab item behavior flags.</param>
    /// <returns>True if the tab is selected. Only call <see cref="EndTabItem"/> if this returns true.</returns>
    public static bool BeginTabItem(ReadOnlySpan<byte> label, StateRef<bool>? pOpen = null, TabItemFlags flags = TabItemFlags.None)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_BeginTabItem(ptr, pOpen.HasValue ? pOpen.Value.Ptr : null, (Native.ImGuiTabItemFlags)flags);
        }
    }

    /// <summary>
    /// Ends a tab item. Only call if <see cref="BeginTabItem"/> returned true.
    /// </summary>
    public static void EndTabItem()
    {
        ImGui_EndTabItem();
    }

    /// <summary>
    /// Creates a tab button (cannot be selected, returns true when clicked).
    /// </summary>
    /// <param name="label">The button label.</param>
    /// <param name="flags">Tab item behavior flags.</param>
    /// <returns>True when clicked.</returns>
    public static bool TabItemButton(ReadOnlySpan<byte> label, TabItemFlags flags = TabItemFlags.None)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_TabItemButton(ptr, (Native.ImGuiTabItemFlags)flags);
        }
    }

    /// <summary>
    /// Notifies the tab bar of a closed tab/window ahead of time.
    /// </summary>
    /// <param name="tabOrDockedWindowLabel">The tab or window label.</param>
    public static void SetTabItemClosed(ReadOnlySpan<byte> tabOrDockedWindowLabel)
    {
        fixed (byte* ptr = tabOrDockedWindowLabel)
        {
            ImGui_SetTabItemClosed(ptr);
        }
    }

    #endregion

    #region Logging/Capture

    /// <summary>
    /// Starts logging to tty (stdout).
    /// </summary>
    /// <param name="autoOpenDepth">Depth to auto-open tree nodes (-1 for default).</param>
    public static void LogToTTY(int autoOpenDepth = -1)
    {
        ImGui_LogToTTY(autoOpenDepth);
    }

    /// <summary>
    /// Starts logging to a file.
    /// </summary>
    /// <param name="autoOpenDepth">Depth to auto-open tree nodes (-1 for default).</param>
    /// <param name="filename">The filename to log to (null for default "imgui_log.txt").</param>
    public static void LogToFile(int autoOpenDepth = -1, ReadOnlySpan<byte> filename = default)
    {
        fixed (byte* ptr = filename)
        {
            ImGui_LogToFile(autoOpenDepth, ptr);
        }
    }

    /// <summary>
    /// Starts logging to the OS clipboard.
    /// </summary>
    /// <param name="autoOpenDepth">Depth to auto-open tree nodes (-1 for default).</param>
    public static void LogToClipboard(int autoOpenDepth = -1)
    {
        ImGui_LogToClipboard(autoOpenDepth);
    }

    /// <summary>
    /// Stops logging (closes file, etc.).
    /// </summary>
    public static void LogFinish()
    {
        ImGui_LogFinish();
    }

    /// <summary>
    /// Displays buttons for logging to tty/file/clipboard.
    /// </summary>
    public static void LogButtons()
    {
        ImGui_LogButtons();
    }

    /// <summary>
    /// Passes text data straight to the log without being displayed.
    /// </summary>
    /// <param name="text">The text to log.</param>
    public static void LogText(ReadOnlySpan<byte> text)
    {
        fixed (byte* ptr = text)
        {
            ImGui_LogText(ptr);
        }
    }

    #endregion

    #region Drag and Drop

    /// <summary>
    /// Begins a drag-and-drop source. Call after submitting an item which may be dragged.
    /// </summary>
    /// <param name="flags">Drag-and-drop flags.</param>
    /// <returns>True if drag source is active; call SetDragDropPayload() + EndDragDropSource().</returns>
    public static bool BeginDragDropSource(DragDropFlags flags = DragDropFlags.None)
    {
        return ImGui_BeginDragDropSource((Native.ImGuiDragDropFlags)flags);
    }

    /// <summary>
    /// Sets the payload data for the current drag-and-drop operation.
    /// </summary>
    /// <param name="type">A user-defined string type (max 32 characters).</param>
    /// <param name="data">The data to be copied and held by ImGui.</param>
    /// <param name="cond">Condition for setting the payload.</param>
    /// <returns>True when payload has been accepted.</returns>
    public static bool SetDragDropPayload(ReadOnlySpan<byte> type, ReadOnlySpan<byte> data, Cond cond = Cond.None)
    {
        fixed (byte* typePtr = type)
        fixed (byte* dataPtr = data)
        {
            return ImGui_SetDragDropPayload(typePtr, dataPtr, (nuint)data.Length, (Native.ImGuiCond)cond);
        }
    }

    /// <summary>
    /// Ends the drag-and-drop source. Only call if BeginDragDropSource() returned true.
    /// </summary>
    public static void EndDragDropSource()
    {
        ImGui_EndDragDropSource();
    }

    /// <summary>
    /// Begins a drag-and-drop target. Call after submitting an item that may receive a payload.
    /// </summary>
    /// <returns>True if can accept payload; call AcceptDragDropPayload() + EndDragDropTarget().</returns>
    public static bool BeginDragDropTarget()
    {
        return ImGui_BeginDragDropTarget();
    }

    /// <summary>
    /// Ends the drag-and-drop target. Only call if BeginDragDropTarget() returned true.
    /// </summary>
    public static void EndDragDropTarget()
    {
        ImGui_EndDragDropTarget();
    }

    #endregion

    #region Clipping

    /// <summary>
    /// Pushes a clipping rectangle for both ImGui logic (hit testing) and rendering.
    /// </summary>
    /// <param name="clipRectMin">The minimum corner of the clip rectangle.</param>
    /// <param name="clipRectMax">The maximum corner of the clip rectangle.</param>
    /// <param name="intersectWithCurrentClipRect">Whether to intersect with the current clip rectangle.</param>
    public static void PushClipRect(Vec2 clipRectMin, Vec2 clipRectMax, bool intersectWithCurrentClipRect)
    {
        ImGui_PushClipRect(clipRectMin.Value, clipRectMax.Value, intersectWithCurrentClipRect);
    }

    /// <summary>
    /// Pops the last clip rectangle.
    /// </summary>
    public static void PopClipRect()
    {
        ImGui_PopClipRect();
    }

    #endregion

    #region Focus, Activation

    /// <summary>
    /// Makes the last item the default focused item of a newly appearing window.
    /// </summary>
    public static void SetItemDefaultFocus()
    {
        ImGui_SetItemDefaultFocus();
    }

    /// <summary>
    /// Focuses keyboard on the next widget.
    /// </summary>
    public static void SetKeyboardFocusHere()
    {
        ImGui_SetKeyboardFocusHere();
    }

    /// <summary>
    /// Focuses keyboard on a widget relative to current position.
    /// </summary>
    /// <param name="offset">Use positive offset to access sub components, -1 for previous widget.</param>
    public static void SetKeyboardFocusHere(int offset)
    {
        ImGui_SetKeyboardFocusHereEx(offset);
    }

    #endregion

    #region Keyboard/Gamepad Navigation

    #endregion

    #region Overlapping mode

    /// <summary>
    /// Allows the next item to be overlapped by a subsequent item.
    /// </summary>
    public static void SetNextItemAllowOverlap()
    {
        ImGui_SetNextItemAllowOverlap();
    }

    #endregion

    #region Item/Widgets Utilities and Query Functions

    /// <summary>
    /// Checks if the last item is hovered.
    /// </summary>
    /// <param name="flags">Hover behavior flags.</param>
    /// <returns>True if the item is hovered.</returns>
    public static bool IsItemHovered(HoveredFlags flags = HoveredFlags.None)
    {
        return ImGui_IsItemHovered((Native.ImGuiHoveredFlags)flags);
    }

    /// <summary>
    /// Checks if the last item is active (e.g., button being held, text field being edited).
    /// </summary>
    /// <returns>True if the item is active.</returns>
    public static bool IsItemActive()
    {
        return ImGui_IsItemActive();
    }

    /// <summary>
    /// Checks if the last item is focused for keyboard/gamepad navigation.
    /// </summary>
    /// <returns>True if the item is focused.</returns>
    public static bool IsItemFocused()
    {
        return ImGui_IsItemFocused();
    }

    /// <summary>
    /// Checks if the last item was clicked with the left mouse button.
    /// </summary>
    /// <returns>True if the item was clicked.</returns>
    public static bool IsItemClicked()
    {
        return ImGui_IsItemClicked();
    }

    /// <summary>
    /// Checks if the last item was clicked with a specific mouse button.
    /// </summary>
    /// <param name="mouseButton">The mouse button to check.</param>
    /// <returns>True if the item was clicked.</returns>
    public static bool IsItemClicked(MouseButton mouseButton)
    {
        return ImGui_IsItemClickedEx((Native.ImGuiMouseButton)mouseButton);
    }

    /// <summary>
    /// Checks if the last item is visible (not clipped/scrolled out of view).
    /// </summary>
    /// <returns>True if the item is visible.</returns>
    public static bool IsItemVisible()
    {
        return ImGui_IsItemVisible();
    }

    /// <summary>
    /// Checks if the last item modified its underlying value this frame.
    /// </summary>
    /// <returns>True if the item was edited.</returns>
    public static bool IsItemEdited()
    {
        return ImGui_IsItemEdited();
    }

    /// <summary>
    /// Checks if the last item was just made active (was previously inactive).
    /// </summary>
    /// <returns>True if the item was activated.</returns>
    public static bool IsItemActivated()
    {
        return ImGui_IsItemActivated();
    }

    /// <summary>
    /// Checks if the last item was just made inactive (was previously active).
    /// </summary>
    /// <returns>True if the item was deactivated.</returns>
    public static bool IsItemDeactivated()
    {
        return ImGui_IsItemDeactivated();
    }

    /// <summary>
    /// Checks if the last item was just made inactive and made a value change when active.
    /// </summary>
    /// <returns>True if the item was deactivated after edit.</returns>
    public static bool IsItemDeactivatedAfterEdit()
    {
        return ImGui_IsItemDeactivatedAfterEdit();
    }

    /// <summary>
    /// Checks if the last item's open state was toggled (set by TreeNode).
    /// </summary>
    /// <returns>True if the item was toggled open.</returns>
    public static bool IsItemToggledOpen()
    {
        return ImGui_IsItemToggledOpen();
    }

    /// <summary>
    /// Checks if any item is hovered.
    /// </summary>
    /// <returns>True if any item is hovered.</returns>
    public static bool IsAnyItemHovered()
    {
        return ImGui_IsAnyItemHovered();
    }

    /// <summary>
    /// Checks if any item is active.
    /// </summary>
    /// <returns>True if any item is active.</returns>
    public static bool IsAnyItemActive()
    {
        return ImGui_IsAnyItemActive();
    }

    /// <summary>
    /// Checks if any item is focused.
    /// </summary>
    /// <returns>True if any item is focused.</returns>
    public static bool IsAnyItemFocused()
    {
        return ImGui_IsAnyItemFocused();
    }

    /// <summary>
    /// Gets the ID of the last item.
    /// </summary>
    /// <returns>The ID of the last item.</returns>
    public static Id GetItemID()
    {
        return new(ImGui_GetItemID());
    }

    /// <summary>
    /// Gets the upper-left bounding rectangle of the last item (screen space).
    /// </summary>
    /// <returns>The minimum bounding rectangle position.</returns>
    public static Vec2 GetItemRectMin()
    {
        return new(ImGui_GetItemRectMin());
    }

    /// <summary>
    /// Gets the lower-right bounding rectangle of the last item (screen space).
    /// </summary>
    /// <returns>The maximum bounding rectangle position.</returns>
    public static Vec2 GetItemRectMax()
    {
        return new(ImGui_GetItemRectMax());
    }

    /// <summary>
    /// Gets the size of the last item.
    /// </summary>
    /// <returns>The size of the last item.</returns>
    public static Vec2 GetItemRectSize()
    {
        return new(ImGui_GetItemRectSize());
    }

    #endregion

    #region Miscellaneous Utilities

    /// <summary>
    /// Tests if a rectangle of given size starting from cursor position is visible/not clipped.
    /// </summary>
    /// <param name="size">The size of the rectangle to test.</param>
    /// <returns>True if the rectangle is visible.</returns>
    public static bool IsRectVisible(Vec2 size)
    {
        return ImGui_IsRectVisibleBySize(size.Value);
    }

    /// <summary>
    /// Tests if a rectangle in screen space is visible/not clipped.
    /// </summary>
    /// <param name="rectMin">The upper-left corner of the rectangle.</param>
    /// <param name="rectMax">The lower-right corner of the rectangle.</param>
    /// <returns>True if the rectangle is visible.</returns>
    public static bool IsRectVisible(Vec2 rectMin, Vec2 rectMax)
    {
        return ImGui_IsRectVisible(rectMin.Value, rectMax.Value);
    }

    /// <summary>
    /// Gets the global ImGui time, incremented by io.DeltaTime every frame.
    /// </summary>
    /// <returns>The global time in seconds.</returns>
    public static double GetTime()
    {
        return ImGui_GetTime();
    }

    /// <summary>
    /// Gets the global ImGui frame count, incremented by 1 every frame.
    /// </summary>
    /// <returns>The frame count.</returns>
    public static int GetFrameCount()
    {
        return ImGui_GetFrameCount();
    }

    #endregion

    #region Text Utilities

    /// <summary>
    /// Calculates the size of text.
    /// </summary>
    /// <param name="text">The text to measure.</param>
    /// <returns>The calculated text size.</returns>
    public static Vec2 CalcTextSize(ReadOnlySpan<byte> text)
    {
        fixed (byte* ptr = text)
        {
            return new(ImGui_CalcTextSize(ptr));
        }
    }

    /// <summary>
    /// Calculates the size of text with extended options.
    /// </summary>
    /// <param name="text">The text to measure.</param>
    /// <param name="hideTextAfterDoubleHash">If true, stop measuring at ##.</param>
    /// <param name="wrapWidth">The wrap width (-1.0f for no wrapping).</param>
    /// <returns>The calculated text size.</returns>
    public static Vec2 CalcTextSize(ReadOnlySpan<byte> text, bool hideTextAfterDoubleHash, float wrapWidth = -1.0f)
    {
        fixed (byte* ptr = text)
        {
            return new(ImGui_CalcTextSizeEx(ptr, null, hideTextAfterDoubleHash, wrapWidth));
        }
    }

    #endregion

    #region Color Utilities

    /// <summary>
    /// Converts a 32-bit color value to a Vec4 float color.
    /// </summary>
    /// <param name="color">The 32-bit color value (0xRRGGBBAA or ImU32 format).</param>
    /// <returns>The color as a Vec4 (RGBA, 0-1 range).</returns>
    public static Vec4 ColorConvertU32ToFloat4(uint color)
    {
        return new(ImGui_ColorConvertU32ToFloat4(color));
    }

    /// <summary>
    /// Converts a Vec4 float color to a 32-bit color value.
    /// </summary>
    /// <param name="color">The color as a Vec4 (RGBA, 0-1 range).</param>
    /// <returns>The 32-bit color value.</returns>
    public static uint ColorConvertFloat4ToU32(Vec4 color)
    {
        return ImGui_ColorConvertFloat4ToU32(color.Value);
    }

    /// <summary>
    /// Converts RGB color values to HSV.
    /// </summary>
    /// <param name="r">Red component (0-1).</param>
    /// <param name="g">Green component (0-1).</param>
    /// <param name="b">Blue component (0-1).</param>
    /// <returns>A tuple containing (H, S, V) values.</returns>
    public static (float H, float S, float V) ColorConvertRGBtoHSV(float r, float g, float b)
    {
        float h, s, v;
        ImGui_ColorConvertRGBtoHSV(r, g, b, &h, &s, &v);
        return (h, s, v);
    }

    /// <summary>
    /// Converts HSV color values to RGB.
    /// </summary>
    /// <param name="h">Hue component (0-1).</param>
    /// <param name="s">Saturation component (0-1).</param>
    /// <param name="v">Value component (0-1).</param>
    /// <returns>A tuple containing (R, G, B) values.</returns>
    public static (float R, float G, float B) ColorConvertHSVtoRGB(float h, float s, float v)
    {
        float r, g, b;
        ImGui_ColorConvertHSVtoRGB(h, s, v, &r, &g, &b);
        return (r, g, b);
    }

    #endregion

    #region Inputs Utilities: Keyboard/Mouse/Gamepad

    /// <summary>
    /// Checks if a key is being held.
    /// </summary>
    /// <param name="key">The key to check.</param>
    /// <returns>True if the key is down.</returns>
    public static bool IsKeyDown(Key key)
    {
        return ImGui_IsKeyDown((Native.ImGuiKey)key);
    }

    /// <summary>
    /// Checks if a key was pressed (went from !Down to Down).
    /// </summary>
    /// <param name="key">The key to check.</param>
    /// <returns>True if the key was pressed.</returns>
    public static bool IsKeyPressed(Key key)
    {
        return ImGui_IsKeyPressed((Native.ImGuiKey)key);
    }

    /// <summary>
    /// Checks if a key was pressed (went from !Down to Down).
    /// </summary>
    /// <param name="key">The key to check.</param>
    /// <param name="repeat">If true, uses io.KeyRepeatDelay / KeyRepeatRate.</param>
    /// <returns>True if the key was pressed.</returns>
    public static bool IsKeyPressed(Key key, bool repeat)
    {
        return ImGui_IsKeyPressedEx((Native.ImGuiKey)key, repeat);
    }

    /// <summary>
    /// Checks if a key was released (went from Down to !Down).
    /// </summary>
    /// <param name="key">The key to check.</param>
    /// <returns>True if the key was released.</returns>
    public static bool IsKeyReleased(Key key)
    {
        return ImGui_IsKeyReleased((Native.ImGuiKey)key);
    }

    /// <summary>
    /// Gets how many times a key was pressed using provided repeat rate/delay.
    /// </summary>
    /// <param name="key">The key to check.</param>
    /// <param name="repeatDelay">The repeat delay.</param>
    /// <param name="rate">The repeat rate.</param>
    /// <returns>The press count (most often 0 or 1, but can be higher).</returns>
    public static int GetKeyPressedAmount(Key key, float repeatDelay, float rate)
    {
        return ImGui_GetKeyPressedAmount((Native.ImGuiKey)key, repeatDelay, rate);
    }

    /// <summary>
    /// Overrides the io.WantCaptureKeyboard flag next frame.
    /// </summary>
    /// <param name="wantCaptureKeyboard">Whether to capture keyboard input.</param>
    public static void SetNextFrameWantCaptureKeyboard(bool wantCaptureKeyboard)
    {
        ImGui_SetNextFrameWantCaptureKeyboard(wantCaptureKeyboard);
    }

    #endregion

    #region Inputs Utilities: Mouse

    /// <summary>
    /// Checks if a mouse button is held.
    /// </summary>
    /// <param name="button">The mouse button to check.</param>
    /// <returns>True if the mouse button is down.</returns>
    public static bool IsMouseDown(MouseButton button)
    {
        return ImGui_IsMouseDown((Native.ImGuiMouseButton)button);
    }

    /// <summary>
    /// Checks if a mouse button was clicked (went from !Down to Down).
    /// </summary>
    /// <param name="button">The mouse button to check.</param>
    /// <returns>True if the mouse button was clicked.</returns>
    public static bool IsMouseClicked(MouseButton button)
    {
        return ImGui_IsMouseClicked((Native.ImGuiMouseButton)button);
    }

    /// <summary>
    /// Checks if a mouse button was clicked (went from !Down to Down).
    /// </summary>
    /// <param name="button">The mouse button to check.</param>
    /// <param name="repeat">If true, uses io.KeyRepeatDelay / KeyRepeatRate.</param>
    /// <returns>True if the mouse button was clicked.</returns>
    public static bool IsMouseClicked(MouseButton button, bool repeat)
    {
        return ImGui_IsMouseClickedEx((Native.ImGuiMouseButton)button, repeat);
    }

    /// <summary>
    /// Checks if a mouse button was released (went from Down to !Down).
    /// </summary>
    /// <param name="button">The mouse button to check.</param>
    /// <returns>True if the mouse button was released.</returns>
    public static bool IsMouseReleased(MouseButton button)
    {
        return ImGui_IsMouseReleased((Native.ImGuiMouseButton)button);
    }

    /// <summary>
    /// Checks if a mouse button was double-clicked.
    /// </summary>
    /// <param name="button">The mouse button to check.</param>
    /// <returns>True if the mouse button was double-clicked.</returns>
    public static bool IsMouseDoubleClicked(MouseButton button)
    {
        return ImGui_IsMouseDoubleClicked((Native.ImGuiMouseButton)button);
    }

    /// <summary>
    /// Gets the number of successive mouse clicks at the time of click (otherwise 0).
    /// </summary>
    /// <param name="button">The mouse button to check.</param>
    /// <returns>The click count.</returns>
    public static int GetMouseClickedCount(MouseButton button)
    {
        return ImGui_GetMouseClickedCount((Native.ImGuiMouseButton)button);
    }

    /// <summary>
    /// Checks if the mouse is hovering a given bounding rectangle.
    /// </summary>
    /// <param name="rMin">The upper-left corner of the rectangle.</param>
    /// <param name="rMax">The lower-right corner of the rectangle.</param>
    /// <param name="clip">If true, clip by current clipping settings.</param>
    /// <returns>True if the mouse is hovering the rectangle.</returns>
    public static bool IsMouseHoveringRect(Vec2 rMin, Vec2 rMax, bool clip = true)
    {
        return clip
            ? ImGui_IsMouseHoveringRect(rMin.Value, rMax.Value)
            : ImGui_IsMouseHoveringRectEx(rMin.Value, rMax.Value, false);
    }

    /// <summary>
    /// Checks if any mouse button is held.
    /// </summary>
    /// <returns>True if any mouse button is down.</returns>
    public static bool IsAnyMouseDown()
    {
        return ImGui_IsAnyMouseDown();
    }

    /// <summary>
    /// Gets the current mouse position.
    /// </summary>
    /// <returns>The mouse position in screen space.</returns>
    public static Vec2 GetMousePos()
    {
        return new(ImGui_GetMousePos());
    }

    /// <summary>
    /// Gets the mouse position at the time of opening the current popup.
    /// </summary>
    /// <returns>The mouse position when the popup was opened.</returns>
    public static Vec2 GetMousePosOnOpeningCurrentPopup()
    {
        return new(ImGui_GetMousePosOnOpeningCurrentPopup());
    }

    /// <summary>
    /// Checks if the mouse is dragging.
    /// </summary>
    /// <param name="button">The mouse button to check.</param>
    /// <param name="lockThreshold">The distance threshold (-1.0f uses io.MouseDraggingThreshold).</param>
    /// <returns>True if dragging.</returns>
    public static bool IsMouseDragging(MouseButton button, float lockThreshold = -1.0f)
    {
        return ImGui_IsMouseDragging((Native.ImGuiMouseButton)button, lockThreshold);
    }

    /// <summary>
    /// Gets the delta from the initial clicking position while the mouse button is pressed.
    /// </summary>
    /// <param name="button">The mouse button to check.</param>
    /// <param name="lockThreshold">The distance threshold (-1.0f uses io.MouseDraggingThreshold).</param>
    /// <returns>The drag delta.</returns>
    public static Vec2 GetMouseDragDelta(MouseButton button = MouseButton.Left, float lockThreshold = -1.0f)
    {
        return new(ImGui_GetMouseDragDelta((Native.ImGuiMouseButton)button, lockThreshold));
    }

    /// <summary>
    /// Resets the mouse drag delta.
    /// </summary>
    /// <param name="button">The mouse button to reset (defaults to left button).</param>
    public static void ResetMouseDragDelta(MouseButton button = MouseButton.Left)
    {
        if (button == MouseButton.Left)
        {
            ImGui_ResetMouseDragDelta();
        }
        else
        {
            ImGui_ResetMouseDragDeltaEx((Native.ImGuiMouseButton)button);
        }
    }

    /// <summary>
    /// Gets the desired mouse cursor shape.
    /// </summary>
    /// <returns>The current mouse cursor.</returns>
    public static MouseCursor GetMouseCursor()
    {
        return (MouseCursor)ImGui_GetMouseCursor();
    }

    /// <summary>
    /// Sets the desired mouse cursor shape.
    /// </summary>
    /// <param name="cursorType">The cursor to set.</param>
    public static void SetMouseCursor(MouseCursor cursorType)
    {
        ImGui_SetMouseCursor((Native.ImGuiMouseCursor)cursorType);
    }

    /// <summary>
    /// Overrides the io.WantCaptureMouse flag next frame.
    /// </summary>
    /// <param name="wantCaptureMouse">Whether to capture mouse input.</param>
    public static void SetNextFrameWantCaptureMouse(bool wantCaptureMouse)
    {
        ImGui_SetNextFrameWantCaptureMouse(wantCaptureMouse);
    }

    #endregion

    #region Clipboard Utilities

    /// <summary>
    /// Gets the text from the clipboard.
    /// </summary>
    /// <returns>The clipboard text, or an empty string if empty.</returns>
    public static string GetClipboardText()
    {
        var ptr = ImGui_GetClipboardText();

        return ptr == null
            ? string.Empty
            : Encoding.UTF8.GetString(MemoryMarshal.CreateReadOnlySpanFromNullTerminated(ptr));
    }

    /// <summary>
    /// Sets the clipboard text.
    /// </summary>
    /// <param name="text">The text to set.</param>
    public static void SetClipboardText(ReadOnlySpan<byte> text)
    {
        fixed (byte* ptr = text)
        {
            ImGui_SetClipboardText(ptr);
        }
    }

    #endregion

    #region Settings/.Ini Utilities

    /// <summary>
    /// Loads settings from a .ini file on disk.
    /// </summary>
    /// <param name="iniFilename">The path to the .ini file.</param>
    /// <remarks>
    /// Call after CreateContext() and before the first call to NewFrame().
    /// NewFrame() automatically calls this with io.IniFilename if set.
    /// </remarks>
    public static void LoadIniSettingsFromDisk(ReadOnlySpan<byte> iniFilename)
    {
        fixed (byte* ptr = iniFilename)
        {
            ImGui_LoadIniSettingsFromDisk(ptr);
        }
    }

    /// <summary>
    /// Loads settings from a memory buffer.
    /// </summary>
    /// <param name="iniData">The .ini data to load.</param>
    /// <remarks>
    /// Call after CreateContext() and before the first call to NewFrame()
    /// to provide .ini data from your own data source.
    /// </remarks>
    public static void LoadIniSettingsFromMemory(ReadOnlySpan<byte> iniData)
    {
        fixed (byte* ptr = iniData)
        {
            ImGui_LoadIniSettingsFromMemory(ptr, (nuint)iniData.Length);
        }
    }

    /// <summary>
    /// Saves settings to a .ini file on disk.
    /// </summary>
    /// <param name="iniFilename">The path to the .ini file.</param>
    /// <remarks>
    /// This is automatically called (if io.IniFilename is not empty) a few seconds
    /// after any modification that should be reflected in the .ini file, and also by DestroyContext().
    /// </remarks>
    public static void SaveIniSettingsToDisk(ReadOnlySpan<byte> iniFilename)
    {
        fixed (byte* ptr = iniFilename)
        {
            ImGui_SaveIniSettingsToDisk(ptr);
        }
    }

    /// <summary>
    /// Saves settings to a string in memory.
    /// </summary>
    /// <returns>The .ini data as a string.</returns>
    /// <remarks>
    /// Call when io.WantSaveIniSettings is set, then save the data by your own means
    /// and clear io.WantSaveIniSettings.
    /// </remarks>
    public static string SaveIniSettingsToMemory()
    {
        nuint size;
        var ptr = ImGui_SaveIniSettingsToMemory(&size);

        return ptr == null
            ? string.Empty
            : Encoding.UTF8.GetString(ptr, (int)size);
    }

    #endregion
}