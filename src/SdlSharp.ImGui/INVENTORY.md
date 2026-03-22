# Dear ImGui API Inventory

Cross-reference of Dear ImGui (imgui.h) sections with SdlSharp.ImGui native bindings and managed wrappers.

- **Native Wrapper**: Qualified name in the `SdlSharp.ImGui.Native` namespace (e.g. `ImGui.IGSharp_Begin`).
- **Managed Wrapper**: Qualified name of the public C# API (e.g. `Gui.Begin`).
- **Notes**: Why an unwrapped API is skipped: *deferred* = planned but not yet done, *variadic* = C va_list/printf-style, *niche* = rarely needed, *internal* = not part of the public API, *C++ only* = template or operator overload.
- **"-"**: Not yet wrapped.

---

## Context creation and access

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| CreateContext | function | ImGui.IGSharp_CreateContext | GuiContext.Create | |
| DestroyContext | function | ImGui.IGSharp_DestroyContext | GuiContext.Dispose | |
| GetCurrentContext | function | ImGui.IGSharp_GetCurrentContext | - | deferred |
| SetCurrentContext | function | ImGui.IGSharp_SetCurrentContext | - | deferred |

## Main

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| GetIO | function | - | - | deferred — struct accessor, exposed piecemeal via IO helpers |
| GetPlatformIO | function | - | - | deferred |
| GetStyle | function | - | - | deferred — style exposed piecemeal via Style helpers |
| NewFrame | function | ImGui.IGSharp_NewFrame | GuiBackend.NewFrame (internal) | Called by backend |
| EndFrame | function | ImGui.IGSharp_EndFrame | - | deferred |
| Render | function | ImGui.IGSharp_Render | Gui.Render | |
| GetDrawData | function | ImGui.IGSharp_GetDrawData | Gui.GetDrawData | |
| GetVersion | function | ImGui.IGSharp_GetVersion | Gui.GetVersion | |
| IMGUI_CHECKVERSION | macro | ImGui.IGSharp_CheckVersion | GuiContext.Create (internal) | |

## Demo, Debug, Information

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| ShowDemoWindow | function | ImGui.IGSharp_ShowDemoWindow | Gui.ShowDemoWindow | |
| ShowMetricsWindow | function | ImGui.IGSharp_ShowMetricsWindow | Gui.ShowMetricsWindow | |
| ShowDebugLogWindow | function | - | - | deferred |
| ShowIDStackToolWindow | function | - | - | deferred |
| ShowAboutWindow | function | - | - | deferred |
| ShowStyleEditor | function | - | - | deferred |
| ShowStyleSelector | function | - | - | deferred |
| ShowFontSelector | function | - | - | deferred |
| ShowUserGuide | function | - | - | deferred |

## Styles

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| StyleColorsDark | function | ImGui.IGSharp_StyleColorsDark | Gui.StyleColorsDark | |
| StyleColorsLight | function | ImGui.IGSharp_StyleColorsLight | Gui.StyleColorsLight | |
| StyleColorsClassic | function | ImGui.IGSharp_StyleColorsClassic | Gui.StyleColorsClassic | |

## Windows

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| Begin | function | ImGui.IGSharp_Begin | Gui.Begin | |
| End | function | ImGui.IGSharp_End | Gui.End | |

## Child Windows

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| BeginChild (str) | function | ImGui.IGSharp_BeginChild | - | deferred |
| BeginChild (ID) | function | - | - | deferred |
| EndChild | function | ImGui.IGSharp_EndChild | - | deferred |

## Windows Utilities

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| IsWindowAppearing | function | ImGui.IGSharp_IsWindowAppearing | - | deferred |
| IsWindowCollapsed | function | ImGui.IGSharp_IsWindowCollapsed | - | deferred |
| IsWindowFocused | function | ImGui.IGSharp_IsWindowFocused | - | deferred |
| IsWindowHovered | function | ImGui.IGSharp_IsWindowHovered | - | deferred |
| GetWindowPos | function | ImGui.IGSharp_GetWindowPos | - | deferred |
| GetWindowSize | function | ImGui.IGSharp_GetWindowSize | - | deferred |

## Window manipulation

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SetNextWindowPos | function | ImGui.IGSharp_SetNextWindowPos | - | deferred |
| SetNextWindowSize | function | ImGui.IGSharp_SetNextWindowSize | - | deferred |
| SetNextWindowSizeConstraints | function | - | - | deferred |
| SetNextWindowContentSize | function | - | - | deferred |
| SetNextWindowCollapsed | function | - | - | deferred |
| SetNextWindowFocus | function | ImGui.IGSharp_SetNextWindowFocus | - | deferred |
| SetNextWindowScroll | function | - | - | deferred |
| SetNextWindowBgAlpha | function | ImGui.IGSharp_SetNextWindowBgAlpha | - | deferred |

## Windows Scrolling

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| GetScrollX | function | - | - | deferred |
| GetScrollY | function | - | - | deferred |
| SetScrollX | function | - | - | deferred |
| SetScrollY | function | - | - | deferred |
| GetScrollMaxX | function | - | - | deferred |
| GetScrollMaxY | function | - | - | deferred |
| SetScrollHereX | function | - | - | deferred |
| SetScrollHereY | function | - | - | deferred |
| SetScrollFromPosX | function | - | - | deferred |
| SetScrollFromPosY | function | - | - | deferred |

## Parameters stacks (font)

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| PushFont | function | - | - | deferred |
| PopFont | function | - | - | deferred |

## Parameters stacks (shared)

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| PushStyleColor (U32) | function | - | - | deferred |
| PushStyleColor (Vec4) | function | ImGui.IGSharp_PushStyleColorVec4 | - | deferred |
| PopStyleColor | function | ImGui.IGSharp_PopStyleColor | - | deferred |
| PushStyleVar (float) | function | ImGui.IGSharp_PushStyleVarFloat | Gui.PushStyleVar | |
| PushStyleVar (Vec2) | function | ImGui.IGSharp_PushStyleVarVec2 | - | deferred |
| PopStyleVar | function | ImGui.IGSharp_PopStyleVar | Gui.PopStyleVar | |
| PushItemFlag | function | - | - | deferred |
| PopItemFlag | function | - | - | deferred |

## Parameters stacks (current window)

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| PushItemWidth | function | ImGui.IGSharp_PushItemWidth | - | deferred |
| PopItemWidth | function | ImGui.IGSharp_PopItemWidth | - | deferred |
| SetNextItemWidth | function | ImGui.IGSharp_SetNextItemWidth | Gui.SetNextItemWidth | |
| CalcItemWidth | function | - | - | deferred |
| PushTextWrapPos | function | - | - | deferred |
| PopTextWrapPos | function | - | - | deferred |

## Style read access

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| GetFont | function | - | - | deferred |
| GetFontSize | function | - | - | deferred |
| GetFontTexUvWhitePixel | function | - | - | niche |
| GetColorU32 (idx) | function | - | - | deferred |
| GetColorU32 (Vec4) | function | - | - | deferred |
| GetColorU32 (U32) | function | - | - | deferred |
| GetStyleColorVec4 | function | - | - | deferred |
| ScaleAllSizes | function | ImGui.IGSharp_Style_ScaleAllSizes | Gui.ScaleAllSizes | |
| SetFontScaleDpi | function | ImGui.IGSharp_Style_SetFontScaleDpi | Gui.SetFontScaleDpi | Custom wrapper |

## Layout cursor positioning

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| GetCursorScreenPos | function | ImGui.IGSharp_GetCursorScreenPos | - | deferred |
| SetCursorScreenPos | function | ImGui.IGSharp_SetCursorScreenPos | - | deferred |
| GetContentRegionAvail | function | ImGui.IGSharp_GetContentRegionAvail | - | deferred |
| GetCursorPos | function | - | - | deferred |
| GetCursorPosX | function | - | - | deferred |
| GetCursorPosY | function | - | - | deferred |
| SetCursorPos | function | - | - | deferred |
| SetCursorPosX | function | - | - | deferred |
| SetCursorPosY | function | - | - | deferred |
| GetCursorStartPos | function | - | - | deferred |

## Other layout functions

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| Separator | function | ImGui.IGSharp_Separator | Gui.Separator | |
| SameLine | function | ImGui.IGSharp_SameLine | Gui.SameLine | |
| NewLine | function | ImGui.IGSharp_NewLine | Gui.NewLine | |
| Spacing | function | ImGui.IGSharp_Spacing | Gui.Spacing | |
| Dummy | function | ImGui.IGSharp_Dummy | - | deferred |
| Indent | function | ImGui.IGSharp_Indent | - | deferred |
| Unindent | function | ImGui.IGSharp_Unindent | - | deferred |
| BeginGroup | function | ImGui.IGSharp_BeginGroup | Gui.BeginGroup | |
| EndGroup | function | ImGui.IGSharp_EndGroup | Gui.EndGroup | |
| AlignTextToFramePadding | function | ImGui.IGSharp_AlignTextToFramePadding | - | deferred |
| GetTextLineHeight | function | ImGui.IGSharp_GetTextLineHeight | - | deferred |
| GetTextLineHeightWithSpacing | function | - | - | deferred |
| GetFrameHeight | function | ImGui.IGSharp_GetFrameHeight | - | deferred |
| GetFrameHeightWithSpacing | function | - | - | deferred |

## ID stack/scopes

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| PushID (str) | function | ImGui.IGSharp_PushIDStr | - | deferred |
| PushID (str begin/end) | function | - | - | deferred |
| PushID (ptr) | function | - | - | deferred |
| PushID (int) | function | ImGui.IGSharp_PushIDInt | - | deferred |
| PopID | function | ImGui.IGSharp_PopID | - | deferred |
| GetID (str) | function | - | - | deferred |
| GetID (ptr) | function | - | - | deferred |
| GetID (int) | function | - | - | deferred |

## Widgets: Text

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| TextUnformatted | function | ImGui.IGSharp_TextUnformatted | - | deferred |
| Text | function | ImGui.IGSharp_Text | Gui.Text | |
| TextV | function | - | - | variadic |
| TextColored | function | ImGui.IGSharp_TextColored | Gui.TextColored | |
| TextColoredV | function | - | - | variadic |
| TextDisabled | function | ImGui.IGSharp_TextDisabled | Gui.TextDisabled | |
| TextDisabledV | function | - | - | variadic |
| TextWrapped | function | ImGui.IGSharp_TextWrapped | Gui.TextWrapped | |
| TextWrappedV | function | - | - | variadic |
| LabelText | function | - | - | deferred |
| LabelTextV | function | - | - | variadic |
| BulletText | function | ImGui.IGSharp_BulletText | Gui.BulletText | |
| BulletTextV | function | - | - | variadic |
| SeparatorText | function | ImGui.IGSharp_SeparatorText | Gui.SeparatorText | |

## Widgets: Main

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| Button | function | ImGui.IGSharp_Button | Gui.Button | |
| SmallButton | function | ImGui.IGSharp_SmallButton | Gui.SmallButton | |
| InvisibleButton | function | - | - | deferred |
| ArrowButton | function | - | - | deferred |
| Checkbox | function | ImGui.IGSharp_Checkbox | Gui.Checkbox | |
| CheckboxFlags (int) | function | - | - | deferred |
| CheckboxFlags (uint) | function | - | - | deferred |
| RadioButton | function | ImGui.IGSharp_RadioButton | Gui.RadioButton | |
| RadioButton (int) | function | - | - | deferred |
| ProgressBar | function | ImGui.IGSharp_ProgressBar | - | deferred |
| Bullet | function | - | - | deferred |

## Widgets: Images

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| Image | function | - | - | deferred |
| ImageButton | function | - | - | deferred |

## Widgets: Combo Box (Dropdown)

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| BeginCombo | function | ImGui.IGSharp_BeginCombo | Gui.BeginCombo | |
| EndCombo | function | ImGui.IGSharp_EndCombo | Gui.EndCombo | |
| Combo (items[]) | function | - | - | deferred |
| Combo (getter) | function | - | - | deferred |

## Widgets: Drag Sliders

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| DragFloat | function | - | - | deferred |
| DragFloat2 | function | - | - | deferred |
| DragFloat3 | function | - | - | deferred |
| DragFloat4 | function | - | - | deferred |
| DragFloatRange2 | function | - | - | deferred |
| DragInt | function | - | - | deferred |
| DragInt2 | function | - | - | deferred |
| DragInt3 | function | - | - | deferred |
| DragInt4 | function | - | - | deferred |
| DragIntRange2 | function | - | - | deferred |
| DragScalar | function | - | - | deferred |
| DragScalarN | function | - | - | deferred |

## Widgets: Regular Sliders

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SliderFloat | function | ImGui.IGSharp_SliderFloat | Gui.SliderFloat | |
| SliderFloat2 | function | - | - | deferred |
| SliderFloat3 | function | - | - | deferred |
| SliderFloat4 | function | - | - | deferred |
| SliderAngle | function | - | - | deferred |
| SliderInt | function | ImGui.IGSharp_SliderInt | Gui.SliderInt | |
| SliderInt2 | function | - | - | deferred |
| SliderInt3 | function | - | - | deferred |
| SliderInt4 | function | - | - | deferred |
| SliderScalar | function | - | - | deferred |
| SliderScalarN | function | - | - | deferred |
| VSliderFloat | function | - | - | deferred |
| VSliderInt | function | - | - | deferred |
| VSliderScalar | function | - | - | deferred |

## Widgets: Input with Keyboard

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| InputText | function | ImGui.IGSharp_InputText | - | deferred |
| InputTextMultiline | function | - | - | deferred |
| InputTextWithHint | function | - | - | deferred |
| InputFloat | function | ImGui.IGSharp_InputFloat | - | deferred |
| InputFloat2 | function | - | - | deferred |
| InputFloat3 | function | - | - | deferred |
| InputFloat4 | function | - | - | deferred |
| InputInt | function | ImGui.IGSharp_InputInt | - | deferred |
| InputInt2 | function | - | - | deferred |
| InputInt3 | function | - | - | deferred |
| InputInt4 | function | - | - | deferred |
| InputDouble | function | - | - | deferred |
| InputScalar | function | - | - | deferred |
| InputScalarN | function | - | - | deferred |

## Widgets: Color Editor/Picker

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| ColorEdit3 | function | ImGui.IGSharp_ColorEdit3 | Gui.ColorEdit3 | |
| ColorEdit4 | function | ImGui.IGSharp_ColorEdit4 | - | deferred |
| ColorPicker3 | function | - | - | deferred |
| ColorPicker4 | function | - | - | deferred |
| ColorButton | function | - | - | deferred |
| SetColorEditOptions | function | - | - | deferred |

## Widgets: Trees

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| TreeNode (str) | function | ImGui.IGSharp_TreeNode | Gui.TreeNode | |
| TreeNode (str, fmt) | function | - | - | variadic |
| TreeNode (ptr, fmt) | function | - | - | variadic |
| TreeNodeEx (str) | function | - | - | deferred |
| TreeNodeEx (str, fmt) | function | - | - | variadic |
| TreeNodeEx (ptr, fmt) | function | - | - | variadic |
| TreePush (str) | function | - | - | deferred |
| TreePush (ptr) | function | - | - | deferred |
| TreePop | function | ImGui.IGSharp_TreePop | Gui.TreePop | |
| GetTreeNodeToLabelSpacing | function | - | - | deferred |
| CollapsingHeader (str) | function | ImGui.IGSharp_CollapsingHeader | Gui.CollapsingHeader | |
| CollapsingHeader (str, bool*) | function | - | - | deferred |
| SetNextItemOpen | function | - | - | deferred |
| SetNextItemStorageID | function | - | - | deferred |

## Widgets: Selectables

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| Selectable (bool) | function | ImGui.IGSharp_Selectable | Gui.Selectable | |
| Selectable (bool*) | function | - | - | deferred |

## Widgets: Multi-selection

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| BeginMultiSelect | function | - | - | deferred |
| EndMultiSelect | function | - | - | deferred |

## Widgets: List Boxes

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| BeginListBox | function | - | - | deferred |
| EndListBox | function | - | - | deferred |
| ListBox (items[]) | function | - | - | deferred |
| ListBox (getter) | function | - | - | deferred |

## Widgets: Data Plotting

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| PlotLines (values) | function | - | - | deferred |
| PlotLines (getter) | function | - | - | deferred |
| PlotHistogram (values) | function | - | - | deferred |
| PlotHistogram (getter) | function | - | - | deferred |

## Widgets: Value() Helpers

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| Value (bool) | function | - | - | deferred |
| Value (int) | function | - | - | deferred |
| Value (uint) | function | - | - | deferred |
| Value (float) | function | - | - | deferred |

## Widgets: Menus

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| BeginMenuBar | function | ImGui.IGSharp_BeginMenuBar | Gui.BeginMenuBar | |
| EndMenuBar | function | ImGui.IGSharp_EndMenuBar | Gui.EndMenuBar | |
| BeginMainMenuBar | function | ImGui.IGSharp_BeginMainMenuBar | Gui.BeginMainMenuBar | |
| EndMainMenuBar | function | ImGui.IGSharp_EndMainMenuBar | Gui.EndMainMenuBar | |
| BeginMenu | function | ImGui.IGSharp_BeginMenu | Gui.BeginMenu | |
| EndMenu | function | ImGui.IGSharp_EndMenu | Gui.EndMenu | |
| MenuItem (str) | function | ImGui.IGSharp_MenuItem | Gui.MenuItem | |
| MenuItem (str, bool*) | function | - | - | deferred |

## Tooltips

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| BeginTooltip | function | ImGui.IGSharp_BeginTooltip | Gui.BeginTooltip | |
| EndTooltip | function | ImGui.IGSharp_EndTooltip | Gui.EndTooltip | |
| SetTooltip | function | ImGui.IGSharp_SetTooltip | Gui.SetTooltip | |
| SetTooltipV | function | - | - | variadic |
| BeginItemTooltip | function | - | - | deferred |
| SetItemTooltip | function | - | - | deferred |
| SetItemTooltipV | function | - | - | variadic |

## Popups, Modals

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| BeginPopup | function | ImGui.IGSharp_BeginPopup | Gui.BeginPopup | |
| BeginPopupModal | function | ImGui.IGSharp_BeginPopupModal | - | deferred |
| EndPopup | function | ImGui.IGSharp_EndPopup | Gui.EndPopup | |
| OpenPopup (str) | function | ImGui.IGSharp_OpenPopup | Gui.OpenPopup | |
| OpenPopup (ID) | function | - | - | deferred |
| OpenPopupOnItemClick | function | - | - | deferred |
| CloseCurrentPopup | function | ImGui.IGSharp_CloseCurrentPopup | Gui.CloseCurrentPopup | |
| BeginPopupContextItem | function | - | - | deferred |
| BeginPopupContextWindow | function | - | - | deferred |
| BeginPopupContextVoid | function | - | - | deferred |
| IsPopupOpen | function | - | - | deferred |

## Tables

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| BeginTable | function | ImGui.IGSharp_BeginTable | Gui.BeginTable | |
| EndTable | function | ImGui.IGSharp_EndTable | Gui.EndTable | |
| TableNextRow | function | ImGui.IGSharp_TableNextRow | Gui.TableNextRow | |
| TableNextColumn | function | ImGui.IGSharp_TableNextColumn | Gui.TableNextColumn | |
| TableSetColumnIndex | function | ImGui.IGSharp_TableSetColumnIndex | - | deferred |
| TableSetupColumn | function | ImGui.IGSharp_TableSetupColumn | Gui.TableSetupColumn | |
| TableSetupScrollFreeze | function | - | - | deferred |
| TableHeadersRow | function | ImGui.IGSharp_TableHeadersRow | Gui.TableHeadersRow | |
| TableHeader | function | - | - | deferred |
| TableGetSortSpecs | function | - | - | deferred |
| TableGetColumnCount | function | - | - | deferred |
| TableGetColumnIndex | function | - | - | deferred |
| TableGetRowIndex | function | - | - | deferred |
| TableGetColumnName | function | - | - | deferred |
| TableGetColumnFlags | function | - | - | deferred |
| TableSetColumnEnabled | function | - | - | deferred |
| TableGetHoveredColumn | function | - | - | deferred |
| TableSetBgColor | function | - | - | deferred |

## Tab Bars, Tabs

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| BeginTabBar | function | ImGui.IGSharp_BeginTabBar | Gui.BeginTabBar | |
| EndTabBar | function | ImGui.IGSharp_EndTabBar | Gui.EndTabBar | |
| BeginTabItem | function | ImGui.IGSharp_BeginTabItem | Gui.BeginTabItem | |
| EndTabItem | function | ImGui.IGSharp_EndTabItem | Gui.EndTabItem | |
| TabItemButton | function | - | - | deferred |
| SetTabItemClosed | function | - | - | deferred |

## Logging/Capture

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| LogToTTY | function | - | - | deferred |
| LogToFile | function | - | - | deferred |
| LogToClipboard | function | - | - | deferred |
| LogFinish | function | - | - | deferred |
| LogButtons | function | - | - | deferred |
| LogText | function | - | - | variadic |
| LogTextV | function | - | - | variadic |

## Drag and Drop

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| BeginDragDropSource | function | - | - | deferred |
| SetDragDropPayload | function | - | - | deferred |
| EndDragDropSource | function | - | - | deferred |
| BeginDragDropTarget | function | - | - | deferred |
| AcceptDragDropPayload | function | - | - | deferred |
| EndDragDropTarget | function | - | - | deferred |
| GetDragDropPayload | function | - | - | deferred |

## Disabling

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| BeginDisabled | function | ImGui.IGSharp_BeginDisabled | Gui.BeginDisabled | |
| EndDisabled | function | ImGui.IGSharp_EndDisabled | Gui.EndDisabled | |

## Clipping

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| PushClipRect | function | - | - | deferred |
| PopClipRect | function | - | - | deferred |

## Focus, Activation

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SetItemDefaultFocus | function | - | - | deferred |
| SetKeyboardFocusHere | function | - | - | deferred |
| SetNextItemAllowOverlap | function | - | - | deferred |

## Item/Widgets Utilities and Query Functions

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| IsItemHovered | function | ImGui.IGSharp_IsItemHovered | Gui.IsItemHovered | |
| IsItemActive | function | - | - | deferred |
| IsItemFocused | function | - | - | deferred |
| IsItemClicked | function | ImGui.IGSharp_IsItemClicked | Gui.IsItemClicked | |
| IsItemVisible | function | - | - | deferred |
| IsItemEdited | function | - | - | deferred |
| IsItemActivated | function | - | - | deferred |
| IsItemDeactivated | function | - | - | deferred |
| IsItemDeactivatedAfterEdit | function | - | - | deferred |
| IsItemToggledOpen | function | - | - | deferred |
| IsAnyItemHovered | function | - | - | deferred |
| IsAnyItemActive | function | - | - | deferred |
| IsAnyItemFocused | function | - | - | deferred |
| GetItemID | function | - | - | deferred |
| GetItemRectMin | function | - | - | deferred |
| GetItemRectMax | function | - | - | deferred |
| GetItemRectSize | function | - | - | deferred |

## Miscellaneous Utilities

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| IsRectVisible (size) | function | - | - | deferred |
| IsRectVisible (min, max) | function | - | - | deferred |
| GetTime | function | - | - | deferred |
| GetFrameCount | function | - | - | deferred |
| GetBackgroundDrawList | function | - | - | deferred |
| GetForegroundDrawList | function | - | - | deferred |
| GetDrawListSharedData | function | - | - | internal |

## Text Utilities

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| CalcTextSize | function | - | - | deferred |

## Color Utilities

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| ColorConvertU32ToFloat4 | function | - | - | deferred |
| ColorConvertFloat4ToU32 | function | - | - | deferred |
| ColorConvertRGBtoHSV | function | - | - | deferred |
| ColorConvertHSVtoRGB | function | - | - | deferred |

## Inputs Utilities

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| IsKeyDown | function | - | - | deferred |
| IsKeyPressed | function | - | - | deferred |
| IsKeyReleased | function | - | - | deferred |
| IsKeyChordPressed | function | - | - | deferred |
| GetKeyPressedAmount | function | - | - | deferred |
| GetKeyName | function | - | - | deferred |
| SetNextFrameWantCaptureKeyboard | function | - | - | deferred |
| Shortcut | function | - | - | deferred |
| SetNextItemShortcut | function | - | - | deferred |
| IsMouseDown | function | - | - | deferred |
| IsMouseClicked | function | - | - | deferred |
| IsMouseReleased | function | - | - | deferred |
| IsMouseDoubleClicked | function | - | - | deferred |
| IsMouseHoveringRect | function | - | - | deferred |
| GetMousePos | function | - | - | deferred |
| GetMousePosOnOpeningCurrentPopup | function | - | - | deferred |
| IsMouseDragging | function | - | - | deferred |
| GetMouseDragDelta | function | - | - | deferred |
| ResetMouseDragDelta | function | - | - | deferred |
| GetMouseCursor | function | - | - | deferred |
| SetMouseCursor | function | - | - | deferred |
| SetNextFrameWantCaptureMouse | function | - | - | deferred |

## Settings/.Ini Utilities

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| LoadIniSettingsFromDisk | function | - | - | deferred |
| LoadIniSettingsFromMemory | function | - | - | deferred |
| SaveIniSettingsToDisk | function | - | - | deferred |
| SaveIniSettingsToMemory | function | - | - | deferred |

## Debug Utilities

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| DebugTextEncoding | function | - | - | niche |
| DebugFlashStyleColor | function | - | - | niche |
| DebugStartItemPicker | function | - | - | niche |
| DebugCheckVersionAndDataLayout | function | - | - | internal |

## Memory Allocators

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SetAllocatorFunctions | function | - | - | niche — .NET has its own memory management |
| GetAllocatorFunctions | function | - | - | niche |
| MemAlloc | function | - | - | niche |
| MemFree | function | - | - | niche |

## IO Accessors (custom)

These are custom C wrapper functions that provide access to ImGuiIO fields.

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| io.WantCaptureMouse | field | ImGui.IGSharp_IO_GetWantCaptureMouse | Gui.WantCaptureMouse | |
| io.WantCaptureKeyboard | field | ImGui.IGSharp_IO_GetWantCaptureKeyboard | Gui.WantCaptureKeyboard | |
| io.ConfigFlags (get) | field | ImGui.IGSharp_IO_GetConfigFlags | - | deferred |
| io.ConfigFlags (set) | field | ImGui.IGSharp_IO_SetConfigFlags | - | deferred |
| io.IniFilename | field | ImGui.IGSharp_IO_SetIniFilename | Gui.SetIniFilename | |
| io.Framerate | field | ImGui.IGSharp_IO_GetFramerate | Gui.Framerate | |

## Flags & Enumerations

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| ImGuiWindowFlags | enum | - | - | deferred — currently passed as int |
| ImGuiChildFlags | enum | - | - | deferred |
| ImGuiInputTextFlags | enum | - | - | deferred |
| ImGuiTreeNodeFlags | enum | - | - | deferred |
| ImGuiPopupFlags | enum | - | - | deferred |
| ImGuiSelectableFlags | enum | - | - | deferred |
| ImGuiComboFlags | enum | - | - | deferred |
| ImGuiTabBarFlags | enum | - | - | deferred |
| ImGuiTabItemFlags | enum | - | - | deferred |
| ImGuiFocusedFlags | enum | - | - | deferred |
| ImGuiHoveredFlags | enum | - | - | deferred |
| ImGuiDragDropFlags | enum | - | - | deferred |
| ImGuiDataType | enum | - | - | deferred |
| ImGuiDir | enum | - | - | deferred |
| ImGuiSortDirection | enum | - | - | deferred |
| ImGuiKey | enum | - | - | deferred |
| ImGuiConfigFlags | enum | - | - | deferred |
| ImGuiCol | enum | - | - | deferred |
| ImGuiStyleVar | enum | - | - | deferred |
| ImGuiButtonFlags | enum | - | - | deferred |
| ImGuiColorEditFlags | enum | - | - | deferred |
| ImGuiSliderFlags | enum | - | - | deferred |
| ImGuiMouseButton | enum | - | - | deferred |
| ImGuiMouseCursor | enum | - | - | deferred |
| ImGuiTableFlags | enum | - | - | deferred |
| ImGuiTableColumnFlags | enum | - | - | deferred |
| ImGuiTableRowFlags | enum | - | - | deferred |
| ImGuiTableBgTarget | enum | - | - | deferred |
| ImGuiMultiSelectFlags | enum | - | - | deferred |

## SDL3 / SDL_GPU Backends (imgui_impl_sdl3 + imgui_impl_sdlgpu3)

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| ImGui_ImplSDL3_InitForSDLGPU | function | ImGuiBackend.IGSharp_ImplSDL3_InitForSDLGPU | GuiBackend.Init | |
| ImGui_ImplSDL3_Shutdown | function | ImGuiBackend.IGSharp_ImplSDL3_Shutdown | GuiBackend.Shutdown | |
| ImGui_ImplSDL3_NewFrame | function | ImGuiBackend.IGSharp_ImplSDL3_NewFrame | GuiBackend.NewFrame | |
| ImGui_ImplSDL3_ProcessEvent | function | ImGuiBackend.IGSharp_ImplSDL3_ProcessEvent | GuiBackend (internal) | Via event filter |
| ImGui_ImplSDL3_InitForOther | function | - | - | niche — using SDL_GPU path |
| ImGui_ImplSDL3_InitForVulkan | function | - | - | niche — using SDL_GPU path |
| ImGui_ImplSDL3_InitForD3D | function | - | - | niche — using SDL_GPU path |
| ImGui_ImplSDL3_InitForMetal | function | - | - | niche — using SDL_GPU path |
| ImGui_ImplSDL3_InitForOpenGL | function | - | - | niche — using SDL_GPU path |
| ImGui_ImplSDLGPU3_Init | function | ImGuiBackend.IGSharp_ImplSDLGPU3_Init | GuiBackend.Init | |
| ImGui_ImplSDLGPU3_Shutdown | function | ImGuiBackend.IGSharp_ImplSDLGPU3_Shutdown | GuiBackend.Shutdown | |
| ImGui_ImplSDLGPU3_NewFrame | function | ImGuiBackend.IGSharp_ImplSDLGPU3_NewFrame | GuiBackend.NewFrame | |
| ImGui_ImplSDLGPU3_PrepareDrawData | function | ImGuiBackend.IGSharp_ImplSDLGPU3_PrepareDrawData | GuiBackend.PrepareDrawData | |
| ImGui_ImplSDLGPU3_RenderDrawData | function | ImGuiBackend.IGSharp_ImplSDLGPU3_RenderDrawData | GuiBackend.RenderDrawData | |
