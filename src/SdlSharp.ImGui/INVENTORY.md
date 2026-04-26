# Dear ImGui API Inventory

Cross-reference of Dear ImGui (imgui.h) sections with SdlSharp.ImGui native bindings and managed wrappers.

- **Native Wrapper**: Qualified name in the `SdlSharp.ImGui.Native` class (e.g. `Native.IGSharp_Begin`).
- **Managed Wrapper**: Qualified name of the public C# API (e.g. `ImGui.Begin`).
- **Notes**: Why an unwrapped API is skipped: *deferred* = planned but not yet done, *variadic* = C va_list/printf-style, *niche* = rarely needed, *internal* = not part of the public API, *C++ only* = template or operator overload.
- **"-"**: Not yet wrapped.

---

## Context creation and access

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| CreateContext | function | Native.IGSharp_CreateContext | ImGuiContext.Create | |
| DestroyContext | function | Native.IGSharp_DestroyContext | ImGuiContext.Dispose | |
| GetCurrentContext | function | Native.IGSharp_GetCurrentContext | - | deferred |
| SetCurrentContext | function | Native.IGSharp_SetCurrentContext | - | deferred |

## Main

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| GetIO | function | - | - | deferred — struct accessor, exposed piecemeal via IO helpers |
| GetPlatformIO | function | - | - | deferred |
| GetStyle | function | - | - | deferred — style exposed piecemeal via Style helpers |
| NewFrame | function | Native.IGSharp_NewFrame | ImGuiBackend.NewFrame (internal) | Called by backend |
| EndFrame | function | Native.IGSharp_EndFrame | - | deferred |
| Render | function | Native.IGSharp_Render | ImGui.Render | |
| GetDrawData | function | Native.IGSharp_GetDrawData | ImGui.GetDrawData | |
| GetVersion | function | Native.IGSharp_GetVersion | ImGui.GetVersion | |
| IMGUI_CHECKVERSION | macro | Native.IGSharp_CheckVersion | ImGuiContext.Create (internal) | |

## Demo, Debug, Information

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| ShowDemoWindow | function | Native.IGSharp_ShowDemoWindow | ImGui.ShowDemoWindow | |
| ShowMetricsWindow | function | Native.IGSharp_ShowMetricsWindow | ImGui.ShowMetricsWindow | |
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
| StyleColorsDark | function | Native.IGSharp_StyleColorsDark | ImGui.StyleColorsDark | |
| StyleColorsLight | function | Native.IGSharp_StyleColorsLight | ImGui.StyleColorsLight | |
| StyleColorsClassic | function | Native.IGSharp_StyleColorsClassic | ImGui.StyleColorsClassic | |

## Windows

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| Begin | function | Native.IGSharp_Begin | ImGui.Begin | |
| End | function | Native.IGSharp_End | ImGui.End | |

## Child Windows

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| BeginChild (str) | function | Native.IGSharp_BeginChild | - | deferred |
| BeginChild (ID) | function | - | - | deferred |
| EndChild | function | Native.IGSharp_EndChild | - | deferred |

## Windows Utilities

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| IsWindowAppearing | function | Native.IGSharp_IsWindowAppearing | - | deferred |
| IsWindowCollapsed | function | Native.IGSharp_IsWindowCollapsed | - | deferred |
| IsWindowFocused | function | Native.IGSharp_IsWindowFocused | - | deferred |
| IsWindowHovered | function | Native.IGSharp_IsWindowHovered | - | deferred |
| GetWindowPos | function | Native.IGSharp_GetWindowPos | - | deferred |
| GetWindowSize | function | Native.IGSharp_GetWindowSize | - | deferred |

## Window manipulation

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SetNextWindowPos | function | Native.IGSharp_SetNextWindowPos | - | deferred |
| SetNextWindowSize | function | Native.IGSharp_SetNextWindowSize | - | deferred |
| SetNextWindowSizeConstraints | function | - | - | deferred |
| SetNextWindowContentSize | function | - | - | deferred |
| SetNextWindowCollapsed | function | - | - | deferred |
| SetNextWindowFocus | function | Native.IGSharp_SetNextWindowFocus | - | deferred |
| SetNextWindowScroll | function | - | - | deferred |
| SetNextWindowBgAlpha | function | Native.IGSharp_SetNextWindowBgAlpha | - | deferred |

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
| PushStyleColor (Vec4) | function | Native.IGSharp_PushStyleColorVec4 | - | deferred |
| PopStyleColor | function | Native.IGSharp_PopStyleColor | - | deferred |
| PushStyleVar (float) | function | Native.IGSharp_PushStyleVarFloat | ImGui.PushStyleVar | |
| PushStyleVar (Vec2) | function | Native.IGSharp_PushStyleVarVec2 | - | deferred |
| PopStyleVar | function | Native.IGSharp_PopStyleVar | ImGui.PopStyleVar | |
| PushItemFlag | function | - | - | deferred |
| PopItemFlag | function | - | - | deferred |

## Parameters stacks (current window)

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| PushItemWidth | function | Native.IGSharp_PushItemWidth | - | deferred |
| PopItemWidth | function | Native.IGSharp_PopItemWidth | - | deferred |
| SetNextItemWidth | function | Native.IGSharp_SetNextItemWidth | ImGui.SetNextItemWidth | |
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
| ScaleAllSizes | function | Native.IGSharp_Style_ScaleAllSizes | ImGui.ScaleAllSizes | |
| SetFontScaleDpi | function | Native.IGSharp_Style_SetFontScaleDpi | ImGui.SetFontScaleDpi | Custom wrapper |

## Layout cursor positioning

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| GetCursorScreenPos | function | Native.IGSharp_GetCursorScreenPos | - | deferred |
| SetCursorScreenPos | function | Native.IGSharp_SetCursorScreenPos | - | deferred |
| GetContentRegionAvail | function | Native.IGSharp_GetContentRegionAvail | - | deferred |
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
| Separator | function | Native.IGSharp_Separator | ImGui.Separator | |
| SameLine | function | Native.IGSharp_SameLine | ImGui.SameLine | |
| NewLine | function | Native.IGSharp_NewLine | ImGui.NewLine | |
| Spacing | function | Native.IGSharp_Spacing | ImGui.Spacing | |
| Dummy | function | Native.IGSharp_Dummy | - | deferred |
| Indent | function | Native.IGSharp_Indent | - | deferred |
| Unindent | function | Native.IGSharp_Unindent | - | deferred |
| BeginGroup | function | Native.IGSharp_BeginGroup | ImGui.BeginGroup | |
| EndGroup | function | Native.IGSharp_EndGroup | ImGui.EndGroup | |
| AlignTextToFramePadding | function | Native.IGSharp_AlignTextToFramePadding | - | deferred |
| GetTextLineHeight | function | Native.IGSharp_GetTextLineHeight | - | deferred |
| GetTextLineHeightWithSpacing | function | - | - | deferred |
| GetFrameHeight | function | Native.IGSharp_GetFrameHeight | - | deferred |
| GetFrameHeightWithSpacing | function | - | - | deferred |

## ID stack/scopes

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| PushID (str) | function | Native.IGSharp_PushIDStr | - | deferred |
| PushID (str begin/end) | function | - | - | deferred |
| PushID (ptr) | function | - | - | deferred |
| PushID (int) | function | Native.IGSharp_PushIDInt | - | deferred |
| PopID | function | Native.IGSharp_PopID | - | deferred |
| GetID (str) | function | - | - | deferred |
| GetID (ptr) | function | - | - | deferred |
| GetID (int) | function | - | - | deferred |

## Widgets: Text

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| TextUnformatted | function | Native.IGSharp_TextUnformatted | - | deferred |
| Text | function | Native.IGSharp_Text | ImGui.Text | |
| TextV | function | - | - | variadic |
| TextColored | function | Native.IGSharp_TextColored | ImGui.TextColored | |
| TextColoredV | function | - | - | variadic |
| TextDisabled | function | Native.IGSharp_TextDisabled | ImGui.TextDisabled | |
| TextDisabledV | function | - | - | variadic |
| TextWrapped | function | Native.IGSharp_TextWrapped | ImGui.TextWrapped | |
| TextWrappedV | function | - | - | variadic |
| LabelText | function | - | - | deferred |
| LabelTextV | function | - | - | variadic |
| BulletText | function | Native.IGSharp_BulletText | ImGui.BulletText | |
| BulletTextV | function | - | - | variadic |
| SeparatorText | function | Native.IGSharp_SeparatorText | ImGui.SeparatorText | |

## Widgets: Main

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| Button | function | Native.IGSharp_Button | ImGui.Button | |
| SmallButton | function | Native.IGSharp_SmallButton | ImGui.SmallButton | |
| InvisibleButton | function | - | - | deferred |
| ArrowButton | function | - | - | deferred |
| Checkbox | function | Native.IGSharp_Checkbox | ImGui.Checkbox | |
| CheckboxFlags (int) | function | - | - | deferred |
| CheckboxFlags (uint) | function | - | - | deferred |
| RadioButton | function | Native.IGSharp_RadioButton | ImGui.RadioButton | |
| RadioButton (int) | function | - | - | deferred |
| ProgressBar | function | Native.IGSharp_ProgressBar | - | deferred |
| Bullet | function | - | - | deferred |

## Widgets: Images

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| Image | function | - | - | deferred |
| ImageButton | function | - | - | deferred |

## Widgets: Combo Box (Dropdown)

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| BeginCombo | function | Native.IGSharp_BeginCombo | ImGui.BeginCombo | |
| EndCombo | function | Native.IGSharp_EndCombo | ImGui.EndCombo | |
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
| SliderFloat | function | Native.IGSharp_SliderFloat | ImGui.SliderFloat | |
| SliderFloat2 | function | - | - | deferred |
| SliderFloat3 | function | - | - | deferred |
| SliderFloat4 | function | - | - | deferred |
| SliderAngle | function | - | - | deferred |
| SliderInt | function | Native.IGSharp_SliderInt | ImGui.SliderInt | |
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
| InputText | function | Native.IGSharp_InputText | - | deferred |
| InputTextMultiline | function | - | - | deferred |
| InputTextWithHint | function | - | - | deferred |
| InputFloat | function | Native.IGSharp_InputFloat | - | deferred |
| InputFloat2 | function | - | - | deferred |
| InputFloat3 | function | - | - | deferred |
| InputFloat4 | function | - | - | deferred |
| InputInt | function | Native.IGSharp_InputInt | - | deferred |
| InputInt2 | function | - | - | deferred |
| InputInt3 | function | - | - | deferred |
| InputInt4 | function | - | - | deferred |
| InputDouble | function | - | - | deferred |
| InputScalar | function | - | - | deferred |
| InputScalarN | function | - | - | deferred |

## Widgets: Color Editor/Picker

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| ColorEdit3 | function | Native.IGSharp_ColorEdit3 | ImGui.ColorEdit3 | |
| ColorEdit4 | function | Native.IGSharp_ColorEdit4 | - | deferred |
| ColorPicker3 | function | - | - | deferred |
| ColorPicker4 | function | - | - | deferred |
| ColorButton | function | - | - | deferred |
| SetColorEditOptions | function | - | - | deferred |

## Widgets: Trees

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| TreeNode (str) | function | Native.IGSharp_TreeNode | ImGui.TreeNode | |
| TreeNode (str, fmt) | function | - | - | variadic |
| TreeNode (ptr, fmt) | function | - | - | variadic |
| TreeNodeEx (str) | function | - | - | deferred |
| TreeNodeEx (str, fmt) | function | - | - | variadic |
| TreeNodeEx (ptr, fmt) | function | - | - | variadic |
| TreePush (str) | function | - | - | deferred |
| TreePush (ptr) | function | - | - | deferred |
| TreePop | function | Native.IGSharp_TreePop | ImGui.TreePop | |
| GetTreeNodeToLabelSpacing | function | - | - | deferred |
| CollapsingHeader (str) | function | Native.IGSharp_CollapsingHeader | ImGui.CollapsingHeader | |
| CollapsingHeader (str, bool*) | function | - | - | deferred |
| SetNextItemOpen | function | - | - | deferred |
| SetNextItemStorageID | function | - | - | deferred |

## Widgets: Selectables

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| Selectable (bool) | function | Native.IGSharp_Selectable | ImGui.Selectable | |
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
| BeginMenuBar | function | Native.IGSharp_BeginMenuBar | ImGui.BeginMenuBar | |
| EndMenuBar | function | Native.IGSharp_EndMenuBar | ImGui.EndMenuBar | |
| BeginMainMenuBar | function | Native.IGSharp_BeginMainMenuBar | ImGui.BeginMainMenuBar | |
| EndMainMenuBar | function | Native.IGSharp_EndMainMenuBar | ImGui.EndMainMenuBar | |
| BeginMenu | function | Native.IGSharp_BeginMenu | ImGui.BeginMenu | |
| EndMenu | function | Native.IGSharp_EndMenu | ImGui.EndMenu | |
| MenuItem (str) | function | Native.IGSharp_MenuItem | ImGui.MenuItem | |
| MenuItem (str, bool*) | function | - | - | deferred |

## Tooltips

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| BeginTooltip | function | Native.IGSharp_BeginTooltip | ImGui.BeginTooltip | |
| EndTooltip | function | Native.IGSharp_EndTooltip | ImGui.EndTooltip | |
| SetTooltip | function | Native.IGSharp_SetTooltip | ImGui.SetTooltip | |
| SetTooltipV | function | - | - | variadic |
| BeginItemTooltip | function | - | - | deferred |
| SetItemTooltip | function | - | - | deferred |
| SetItemTooltipV | function | - | - | variadic |

## Popups, Modals

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| BeginPopup | function | Native.IGSharp_BeginPopup | ImGui.BeginPopup | |
| BeginPopupModal | function | Native.IGSharp_BeginPopupModal | - | deferred |
| EndPopup | function | Native.IGSharp_EndPopup | ImGui.EndPopup | |
| OpenPopup (str) | function | Native.IGSharp_OpenPopup | ImGui.OpenPopup | |
| OpenPopup (ID) | function | - | - | deferred |
| OpenPopupOnItemClick | function | - | - | deferred |
| CloseCurrentPopup | function | Native.IGSharp_CloseCurrentPopup | ImGui.CloseCurrentPopup | |
| BeginPopupContextItem | function | - | - | deferred |
| BeginPopupContextWindow | function | - | - | deferred |
| BeginPopupContextVoid | function | - | - | deferred |
| IsPopupOpen | function | - | - | deferred |

## Tables

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| BeginTable | function | Native.IGSharp_BeginTable | ImGui.BeginTable | |
| EndTable | function | Native.IGSharp_EndTable | ImGui.EndTable | |
| TableNextRow | function | Native.IGSharp_TableNextRow | ImGui.TableNextRow | |
| TableNextColumn | function | Native.IGSharp_TableNextColumn | ImGui.TableNextColumn | |
| TableSetColumnIndex | function | Native.IGSharp_TableSetColumnIndex | - | deferred |
| TableSetupColumn | function | Native.IGSharp_TableSetupColumn | ImGui.TableSetupColumn | |
| TableSetupScrollFreeze | function | - | - | deferred |
| TableHeadersRow | function | Native.IGSharp_TableHeadersRow | ImGui.TableHeadersRow | |
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
| BeginTabBar | function | Native.IGSharp_BeginTabBar | ImGui.BeginTabBar | |
| EndTabBar | function | Native.IGSharp_EndTabBar | ImGui.EndTabBar | |
| BeginTabItem | function | Native.IGSharp_BeginTabItem | ImGui.BeginTabItem | |
| EndTabItem | function | Native.IGSharp_EndTabItem | ImGui.EndTabItem | |
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
| BeginDisabled | function | Native.IGSharp_BeginDisabled | ImGui.BeginDisabled | |
| EndDisabled | function | Native.IGSharp_EndDisabled | ImGui.EndDisabled | |

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
| IsItemHovered | function | Native.IGSharp_IsItemHovered | ImGui.IsItemHovered | |
| IsItemActive | function | - | - | deferred |
| IsItemFocused | function | - | - | deferred |
| IsItemClicked | function | Native.IGSharp_IsItemClicked | ImGui.IsItemClicked | |
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
| io.WantCaptureMouse | field | Native.IGSharp_IO_GetWantCaptureMouse | ImGui.WantCaptureMouse | |
| io.WantCaptureKeyboard | field | Native.IGSharp_IO_GetWantCaptureKeyboard | ImGui.WantCaptureKeyboard | |
| io.ConfigFlags (get) | field | Native.IGSharp_IO_GetConfigFlags | - | deferred |
| io.ConfigFlags (set) | field | Native.IGSharp_IO_SetConfigFlags | - | deferred |
| io.IniFilename | field | Native.IGSharp_IO_SetIniFilename | ImGui.SetIniFilename | |
| io.Framerate | field | Native.IGSharp_IO_GetFramerate | ImGui.Framerate | |

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
| ImGui_ImplSDL3_InitForSDLGPU | function | Native.IGSharp_ImplSDL3_InitForSDLGPU | ImGuiBackend.Init | |
| ImGui_ImplSDL3_Shutdown | function | Native.IGSharp_ImplSDL3_Shutdown | ImGuiBackend.Shutdown | |
| ImGui_ImplSDL3_NewFrame | function | Native.IGSharp_ImplSDL3_NewFrame | ImGuiBackend.NewFrame | |
| ImGui_ImplSDL3_ProcessEvent | function | Native.IGSharp_ImplSDL3_ProcessEvent | ImGuiBackend (internal) | Via event filter |
| ImGui_ImplSDL3_InitForOther | function | - | - | niche — using SDL_GPU path |
| ImGui_ImplSDL3_InitForVulkan | function | - | - | niche — using SDL_GPU path |
| ImGui_ImplSDL3_InitForD3D | function | - | - | niche — using SDL_GPU path |
| ImGui_ImplSDL3_InitForMetal | function | - | - | niche — using SDL_GPU path |
| ImGui_ImplSDL3_InitForOpenGL | function | - | - | niche — using SDL_GPU path |
| ImGui_ImplSDLGPU3_Init | function | Native.IGSharp_ImplSDLGPU3_Init | ImGuiBackend.Init | |
| ImGui_ImplSDLGPU3_Shutdown | function | Native.IGSharp_ImplSDLGPU3_Shutdown | ImGuiBackend.Shutdown | |
| ImGui_ImplSDLGPU3_NewFrame | function | Native.IGSharp_ImplSDLGPU3_NewFrame | ImGuiBackend.NewFrame | |
| ImGui_ImplSDLGPU3_PrepareDrawData | function | Native.IGSharp_ImplSDLGPU3_PrepareDrawData | ImGuiBackend.PrepareDrawData | |
| ImGui_ImplSDLGPU3_RenderDrawData | function | Native.IGSharp_ImplSDLGPU3_RenderDrawData | ImGuiBackend.RenderDrawData | |
