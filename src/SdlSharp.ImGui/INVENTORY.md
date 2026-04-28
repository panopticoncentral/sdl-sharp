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
| CreateContext | function | Native.IGSharp_CreateContext | Context.Create | |
| DestroyContext | function | Native.IGSharp_DestroyContext | Context.Dispose | |
| GetCurrentContext | function | Native.IGSharp_GetCurrentContext | Context.IsCurrent | |
| SetCurrentContext | function | Native.IGSharp_SetCurrentContext | Context.MakeCurrent | |

## Main

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| GetIO | function | - | - | deferred — struct accessor, exposed piecemeal via Io static class |
| GetPlatformIO | function | - | - | deferred |
| GetStyle | function | - | - | exposed piecemeal via Style static class |
| NewFrame | function | Native.IGSharp_NewFrame | ImGuiBackend.NewFrame (internal) | Called by backend |
| EndFrame | function | Native.IGSharp_EndFrame | - | deferred |
| Render | function | Native.IGSharp_Render | ImGui.Render | |
| GetDrawData | function | Native.IGSharp_GetDrawData | ImGui.GetDrawData | |
| GetVersion | function | Native.IGSharp_GetVersion | ImGui.GetVersion | |
| IMGUI_CHECKVERSION | macro | Native.IGSharp_CheckVersion | Context.Create (internal) | |

## Demo, Debug, Information

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| ShowDemoWindow | function | Native.IGSharp_ShowDemoWindow | ImGui.ShowDemoWindow | |
| ShowMetricsWindow | function | Native.IGSharp_ShowMetricsWindow | ImGui.ShowMetricsWindow | |
| ShowDebugLogWindow | function | Native.IGSharp_ShowDebugLogWindow | ImGui.ShowDebugLogWindow | |
| ShowIDStackToolWindow | function | Native.IGSharp_ShowIDStackToolWindow | ImGui.ShowIDStackToolWindow | |
| ShowAboutWindow | function | Native.IGSharp_ShowAboutWindow | ImGui.ShowAboutWindow | |
| ShowStyleEditor | function | Native.IGSharp_ShowStyleEditor | ImGui.ShowStyleEditor | |
| ShowStyleSelector | function | Native.IGSharp_ShowStyleSelector | ImGui.ShowStyleSelector | |
| ShowFontSelector | function | Native.IGSharp_ShowFontSelector | ImGui.ShowFontSelector | |
| ShowUserGuide | function | Native.IGSharp_ShowUserGuide | ImGui.ShowUserGuide | |

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
| BeginChild (str) | function | Native.IGSharp_BeginChild | ImGui.BeginChild | |
| BeginChild (ID) | function | - | - | deferred |
| EndChild | function | Native.IGSharp_EndChild | ImGui.EndChild | |

## Windows Utilities

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| IsWindowAppearing | function | Native.IGSharp_IsWindowAppearing | ImGui.IsWindowAppearing | |
| IsWindowCollapsed | function | Native.IGSharp_IsWindowCollapsed | ImGui.IsWindowCollapsed | |
| IsWindowFocused | function | Native.IGSharp_IsWindowFocused | ImGui.IsWindowFocused | |
| IsWindowHovered | function | Native.IGSharp_IsWindowHovered | ImGui.IsWindowHovered | |
| GetWindowPos | function | Native.IGSharp_GetWindowPos | ImGui.GetWindowPos | |
| GetWindowSize | function | Native.IGSharp_GetWindowSize | ImGui.GetWindowSize | |
| GetWindowWidth | function | Native.IGSharp_GetWindowWidth | ImGui.GetWindowWidth | |
| GetWindowHeight | function | Native.IGSharp_GetWindowHeight | ImGui.GetWindowHeight | |

## Window manipulation

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SetNextWindowPos | function | Native.IGSharp_SetNextWindowPos | ImGui.SetNextWindowPos | |
| SetNextWindowSize | function | Native.IGSharp_SetNextWindowSize | ImGui.SetNextWindowSize | |
| SetNextWindowSizeConstraints | function | Native.IGSharp_SetNextWindowSizeConstraints | ImGui.SetNextWindowSizeConstraints | |
| SetNextWindowContentSize | function | Native.IGSharp_SetNextWindowContentSize | ImGui.SetNextWindowContentSize | |
| SetNextWindowCollapsed | function | Native.IGSharp_SetNextWindowCollapsed | ImGui.SetNextWindowCollapsed | |
| SetNextWindowFocus | function | Native.IGSharp_SetNextWindowFocus | ImGui.SetNextWindowFocus | |
| SetNextWindowScroll | function | Native.IGSharp_SetNextWindowScroll | ImGui.SetNextWindowScroll | |
| SetNextWindowBgAlpha | function | Native.IGSharp_SetNextWindowBgAlpha | ImGui.SetNextWindowBgAlpha | |
| GetMainViewport | function | Native.IGSharp_GetMainViewport | ImGui.GetMainViewport | Returns Viewport |

## Windows Scrolling

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| GetScrollX | function | Native.IGSharp_GetScrollX | ImGui.GetScrollX | |
| GetScrollY | function | Native.IGSharp_GetScrollY | ImGui.GetScrollY | |
| SetScrollX | function | Native.IGSharp_SetScrollX | ImGui.SetScrollX | |
| SetScrollY | function | Native.IGSharp_SetScrollY | ImGui.SetScrollY | |
| GetScrollMaxX | function | Native.IGSharp_GetScrollMaxX | ImGui.GetScrollMaxX | |
| GetScrollMaxY | function | Native.IGSharp_GetScrollMaxY | ImGui.GetScrollMaxY | |
| SetScrollHereX | function | Native.IGSharp_SetScrollHereX | ImGui.SetScrollHereX | |
| SetScrollHereY | function | Native.IGSharp_SetScrollHereY | ImGui.SetScrollHereY | |
| SetScrollFromPosX | function | Native.IGSharp_SetScrollFromPosX | ImGui.SetScrollFromPosX | |
| SetScrollFromPosY | function | Native.IGSharp_SetScrollFromPosY | ImGui.SetScrollFromPosY | |

## Parameters stacks (font)

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| PushFont | function | Native.IGSharp_PushFont | ImGui.PushFont | |
| PopFont | function | Native.IGSharp_PopFont | ImGui.PopFont | |
| GetFont | function | Native.IGSharp_GetFont | ImGui.GetFont | |
| GetFontSize | function | Native.IGSharp_GetFontSize | ImGui.GetFontSize | |

## Parameters stacks (shared)

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| PushStyleColor (U32) | function | Native.IGSharp_PushStyleColorU32 | ImGui.PushStyleColor (Col, uint) | |
| PushStyleColor (Vec4) | function | Native.IGSharp_PushStyleColorVec4 | ImGui.PushStyleColor (Col, float r,g,b,a) | |
| PopStyleColor | function | Native.IGSharp_PopStyleColor | ImGui.PopStyleColor | |
| PushStyleVar (float) | function | Native.IGSharp_PushStyleVarFloat | ImGui.PushStyleVar (StyleVar, float) | |
| PushStyleVar (Vec2) | function | Native.IGSharp_PushStyleVarVec2 | ImGui.PushStyleVar (StyleVar, float x,y) | |
| PopStyleVar | function | Native.IGSharp_PopStyleVar | ImGui.PopStyleVar | |
| PushItemFlag | function | Native.IGSharp_PushItemFlag | ImGui.PushItemFlag | |
| PopItemFlag | function | Native.IGSharp_PopItemFlag | ImGui.PopItemFlag | |

## Parameters stacks (current window)

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| PushItemWidth | function | Native.IGSharp_PushItemWidth | ImGui.PushItemWidth | |
| PopItemWidth | function | Native.IGSharp_PopItemWidth | ImGui.PopItemWidth | |
| SetNextItemWidth | function | Native.IGSharp_SetNextItemWidth | ImGui.SetNextItemWidth | |
| CalcItemWidth | function | Native.IGSharp_CalcItemWidth | ImGui.CalcItemWidth | |
| PushTextWrapPos | function | Native.IGSharp_PushTextWrapPos | ImGui.PushTextWrapPos | |
| PopTextWrapPos | function | Native.IGSharp_PopTextWrapPos | ImGui.PopTextWrapPos | |

## Style read access

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| GetFont | function | Native.IGSharp_GetFont | ImGui.GetFont | |
| GetFontSize | function | Native.IGSharp_GetFontSize | ImGui.GetFontSize | |
| GetFontTexUvWhitePixel | function | - | - | niche |
| GetColorU32 (idx) | function | Native.IGSharp_GetColorU32 | ImGui.GetColorU32 (Col) | |
| GetColorU32 (Vec4) | function | Native.IGSharp_GetColorU32Vec4 | ImGui.GetColorU32 (float r,g,b,a) | |
| GetColorU32 (U32) | function | Native.IGSharp_GetColorU32Packed | ImGui.GetColorU32 (uint) | |
| GetStyleColorVec4 | function | Native.IGSharp_Style_GetColor | Style.GetColor | |
| ScaleAllSizes | function | Native.IGSharp_Style_ScaleAllSizes | ImGui.ScaleAllSizes | |
| SetFontScaleDpi | function | Native.IGSharp_Style_SetFontScaleDpi | ImGui.SetFontScaleDpi | Custom wrapper |

## Layout cursor positioning

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| GetCursorScreenPos | function | Native.IGSharp_GetCursorScreenPos | ImGui.GetCursorScreenPos | |
| SetCursorScreenPos | function | Native.IGSharp_SetCursorScreenPos | ImGui.SetCursorScreenPos | |
| GetContentRegionAvail | function | Native.IGSharp_GetContentRegionAvail | ImGui.GetContentRegionAvail | |
| GetCursorPos | function | Native.IGSharp_GetCursorPos | ImGui.GetCursorPos | |
| GetCursorPosX | function | - | - | deferred — use GetCursorPos |
| GetCursorPosY | function | - | - | deferred — use GetCursorPos |
| SetCursorPos | function | Native.IGSharp_SetCursorPos | ImGui.SetCursorPos | |
| SetCursorPosX | function | - | - | deferred — use SetCursorPos |
| SetCursorPosY | function | - | - | deferred — use SetCursorPos |
| GetCursorStartPos | function | - | - | deferred |

## Other layout functions

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| Separator | function | Native.IGSharp_Separator | ImGui.Separator | |
| SameLine | function | Native.IGSharp_SameLine | ImGui.SameLine | |
| NewLine | function | Native.IGSharp_NewLine | ImGui.NewLine | |
| Spacing | function | Native.IGSharp_Spacing | ImGui.Spacing | |
| Dummy | function | Native.IGSharp_Dummy | - | deferred |
| Indent | function | Native.IGSharp_Indent | ImGui.Indent | |
| Unindent | function | Native.IGSharp_Unindent | ImGui.Unindent | |
| BeginGroup | function | Native.IGSharp_BeginGroup | ImGui.BeginGroup | |
| EndGroup | function | Native.IGSharp_EndGroup | ImGui.EndGroup | |
| AlignTextToFramePadding | function | Native.IGSharp_AlignTextToFramePadding | ImGui.AlignTextToFramePadding | |
| GetTextLineHeight | function | Native.IGSharp_GetTextLineHeight | ImGui.GetTextLineHeight | |
| GetTextLineHeightWithSpacing | function | Native.IGSharp_GetTextLineHeightWithSpacing | ImGui.GetTextLineHeightWithSpacing | |
| GetFrameHeight | function | Native.IGSharp_GetFrameHeight | ImGui.GetFrameHeight | |
| GetFrameHeightWithSpacing | function | Native.IGSharp_GetFrameHeightWithSpacing | ImGui.GetFrameHeightWithSpacing | |

## ID stack/scopes

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| PushID (str) | function | Native.IGSharp_PushIDStr | ImGui.PushID (string) | |
| PushID (str begin/end) | function | - | - | deferred |
| PushID (ptr) | function | - | - | deferred |
| PushID (int) | function | Native.IGSharp_PushIDInt | ImGui.PushID (int) | |
| PopID | function | Native.IGSharp_PopID | ImGui.PopID | |
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
| LabelText | function | Native.IGSharp_LabelText | ImGui.LabelText | |
| LabelTextV | function | - | - | variadic |
| BulletText | function | Native.IGSharp_BulletText | ImGui.BulletText | |
| BulletTextV | function | - | - | variadic |
| SeparatorText | function | Native.IGSharp_SeparatorText | ImGui.SeparatorText | |
| TextLink | function | Native.IGSharp_TextLink | ImGui.TextLink | |
| TextLinkOpenURL | function | Native.IGSharp_TextLinkOpenURL | ImGui.TextLinkOpenURL | |

## Widgets: Main

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| Button | function | Native.IGSharp_Button | ImGui.Button | |
| SmallButton | function | Native.IGSharp_SmallButton | ImGui.SmallButton | |
| InvisibleButton | function | Native.IGSharp_InvisibleButton | ImGui.InvisibleButton | |
| ArrowButton | function | Native.IGSharp_ArrowButton | ImGui.ArrowButton | |
| Checkbox | function | Native.IGSharp_Checkbox | ImGui.Checkbox | |
| CheckboxFlags (int) | function | - | - | deferred |
| CheckboxFlags (uint) | function | - | - | deferred |
| RadioButton | function | Native.IGSharp_RadioButton | ImGui.RadioButton (string, bool) | |
| RadioButton (int) | function | Native.IGSharp_RadioButtonInt | ImGui.RadioButton (string, ref int, int) | |
| ProgressBar | function | Native.IGSharp_ProgressBar | ImGui.ProgressBar | |
| Bullet | function | Native.IGSharp_Bullet | ImGui.Bullet | |

## Widgets: Images

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| Image | function | Native.IGSharp_Image | ImGui.Image | Overloads for ulong textureId and GpuTexture |
| ImageButton | function | Native.IGSharp_ImageButton | ImGui.ImageButton | Overloads for ulong textureId and GpuTexture |

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
| DragFloat | function | Native.IGSharp_DragFloat | ImGui.DragFloat | |
| DragFloat2 | function | Native.IGSharp_DragFloat2 | ImGui.DragFloat2 | |
| DragFloat3 | function | Native.IGSharp_DragFloat3 | ImGui.DragFloat3 | |
| DragFloat4 | function | Native.IGSharp_DragFloat4 | ImGui.DragFloat4 | |
| DragFloatRange2 | function | - | - | deferred |
| DragInt | function | Native.IGSharp_DragInt | ImGui.DragInt | |
| DragInt2 | function | Native.IGSharp_DragInt2 | ImGui.DragInt2 | |
| DragInt3 | function | Native.IGSharp_DragInt3 | ImGui.DragInt3 | |
| DragInt4 | function | Native.IGSharp_DragInt4 | ImGui.DragInt4 | |
| DragIntRange2 | function | - | - | deferred |
| DragScalar | function | Native.IGSharp_DragScalar | ImGui.Drag&lt;T&gt; | Generic single-value |
| DragScalarN | function | Native.IGSharp_DragScalarN | ImGui.Drag&lt;T&gt; | Generic Span&lt;T&gt; overload |

## Widgets: Regular Sliders

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SliderFloat | function | Native.IGSharp_SliderFloat | ImGui.SliderFloat | |
| SliderFloat2 | function | Native.IGSharp_SliderFloat2 | ImGui.SliderFloat2 | |
| SliderFloat3 | function | Native.IGSharp_SliderFloat3 | ImGui.SliderFloat3 | |
| SliderFloat4 | function | Native.IGSharp_SliderFloat4 | ImGui.SliderFloat4 | |
| SliderAngle | function | Native.IGSharp_SliderAngle | ImGui.SliderAngle | |
| SliderInt | function | Native.IGSharp_SliderInt | ImGui.SliderInt | |
| SliderInt2 | function | Native.IGSharp_SliderInt2 | ImGui.SliderInt2 | |
| SliderInt3 | function | Native.IGSharp_SliderInt3 | ImGui.SliderInt3 | |
| SliderInt4 | function | Native.IGSharp_SliderInt4 | ImGui.SliderInt4 | |
| SliderScalar | function | Native.IGSharp_SliderScalar | ImGui.Slider&lt;T&gt; | Generic single-value |
| SliderScalarN | function | Native.IGSharp_SliderScalarN | ImGui.Slider&lt;T&gt; | Generic Span&lt;T&gt; overload |
| VSliderFloat | function | - | ImGui.VSlider&lt;T&gt; | Via generic VSlider |
| VSliderInt | function | - | ImGui.VSlider&lt;T&gt; | Via generic VSlider |
| VSliderScalar | function | Native.IGSharp_VSliderScalar | ImGui.VSlider&lt;T&gt; | Generic |

## Widgets: Input with Keyboard

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| InputText | function | Native.IGSharp_InputText, IGSharp_InputTextEx | ImGui.InputText | Overloads: Span&lt;byte&gt;, ref string, with-callback |
| InputTextMultiline | function | Native.IGSharp_InputTextMultiline, IGSharp_InputTextMultilineEx | ImGui.InputTextMultiline | Overloads: Span&lt;byte&gt;, ref string, with-callback |
| InputTextWithHint | function | Native.IGSharp_InputTextWithHint, IGSharp_InputTextWithHintEx | ImGui.InputTextWithHint | Overloads: Span&lt;byte&gt;, ref string, with-callback |
| InputFloat | function | Native.IGSharp_InputFloat | ImGui.InputFloat | Plus generic Input&lt;T&gt; |
| InputFloat2 | function | Native.IGSharp_InputFloat2 | ImGui.InputFloat2 | |
| InputFloat3 | function | Native.IGSharp_InputFloat3 | ImGui.InputFloat3 | |
| InputFloat4 | function | Native.IGSharp_InputFloat4 | ImGui.InputFloat4 | |
| InputInt | function | Native.IGSharp_InputInt | ImGui.InputInt | Plus generic Input&lt;T&gt; |
| InputInt2 | function | Native.IGSharp_InputInt2 | ImGui.InputInt2 | |
| InputInt3 | function | Native.IGSharp_InputInt3 | ImGui.InputInt3 | |
| InputInt4 | function | Native.IGSharp_InputInt4 | ImGui.InputInt4 | |
| InputDouble | function | Native.IGSharp_InputDouble | ImGui.InputDouble | Plus generic Input&lt;T&gt; |
| InputScalar | function | Native.IGSharp_InputScalar | ImGui.Input&lt;T&gt; | Generic |
| InputScalarN | function | Native.IGSharp_InputScalarN | ImGui.Input&lt;T&gt; | Generic Span&lt;T&gt; overload |

## Widgets: Color Editor/Picker

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| ColorEdit3 | function | Native.IGSharp_ColorEdit3 | ImGui.ColorEdit3 | |
| ColorEdit4 | function | Native.IGSharp_ColorEdit4 | ImGui.ColorEdit4 | |
| ColorPicker3 | function | Native.IGSharp_ColorPicker3 | ImGui.ColorPicker3 | |
| ColorPicker4 | function | Native.IGSharp_ColorPicker4 | ImGui.ColorPicker4 | |
| ColorButton | function | Native.IGSharp_ColorButton | ImGui.ColorButton | |
| SetColorEditOptions | function | - | - | deferred |

## Widgets: Trees

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| TreeNode (str) | function | Native.IGSharp_TreeNode | ImGui.TreeNode | |
| TreeNode (str, fmt) | function | - | - | variadic |
| TreeNode (ptr, fmt) | function | - | - | variadic |
| TreeNodeEx (str) | function | Native.IGSharp_TreeNodeEx | ImGui.TreeNodeEx | |
| TreeNodeEx (str, fmt) | function | - | - | variadic |
| TreeNodeEx (ptr, fmt) | function | - | - | variadic |
| TreePush (str) | function | - | - | deferred |
| TreePush (ptr) | function | - | - | deferred |
| TreePop | function | Native.IGSharp_TreePop | ImGui.TreePop | |
| GetTreeNodeToLabelSpacing | function | Native.IGSharp_GetTreeNodeToLabelSpacing | ImGui.GetTreeNodeToLabelSpacing | |
| CollapsingHeader (str) | function | Native.IGSharp_CollapsingHeader | ImGui.CollapsingHeader (string, TreeNodeFlags) | |
| CollapsingHeader (str, bool*) | function | Native.IGSharp_CollapsingHeaderClosable | ImGui.CollapsingHeader (string, ref bool, TreeNodeFlags) | |
| SetNextItemOpen | function | Native.IGSharp_SetNextItemOpen | ImGui.SetNextItemOpen | |
| SetNextItemStorageID | function | - | - | deferred |
| TreeNodeGetOpen | function | Native.IGSharp_TreeNodeGetOpen | ImGui.TreeNodeGetOpen | Custom wrapper for storage query |

## Widgets: Selectables

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| Selectable (bool) | function | Native.IGSharp_Selectable | ImGui.Selectable (string, bool) | |
| Selectable (bool*) | function | Native.IGSharp_SelectablePtr | ImGui.Selectable (string, ref bool) | |

## Widgets: Multi-selection

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| BeginMultiSelect | function | Native.IGSharp_BeginMultiSelect | ImGui.BeginMultiSelect | Returns MultiSelectIO |
| EndMultiSelect | function | Native.IGSharp_EndMultiSelect | ImGui.EndMultiSelect | Returns MultiSelectIO |
| SetNextItemSelectionUserData | function | Native.IGSharp_SetNextItemSelectionUserData | ImGui.SetNextItemSelectionUserData | |
| IsItemToggledSelection | function | Native.IGSharp_IsItemToggledSelection | ImGui.IsItemToggledSelection | |

## Widgets: List Boxes

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| BeginListBox | function | Native.IGSharp_BeginListBox | ImGui.BeginListBox | |
| EndListBox | function | Native.IGSharp_EndListBox | ImGui.EndListBox | |
| ListBox (items[]) | function | - | - | deferred |
| ListBox (getter) | function | - | - | deferred |

## Widgets: Data Plotting

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| PlotLines (values) | function | Native.IGSharp_PlotLines | ImGui.PlotLines (ReadOnlySpan&lt;float&gt;) | |
| PlotLines (getter) | function | Native.IGSharp_PlotLinesCallback | ImGui.PlotLines (PlotValuesGetter) | |
| PlotHistogram (values) | function | Native.IGSharp_PlotHistogram | ImGui.PlotHistogram (ReadOnlySpan&lt;float&gt;) | |
| PlotHistogram (getter) | function | Native.IGSharp_PlotHistogramCallback | ImGui.PlotHistogram (PlotValuesGetter) | |

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
| MenuItem (str) | function | Native.IGSharp_MenuItem | ImGui.MenuItem (string, string?, bool, bool) | |
| MenuItem (str, bool*) | function | Native.IGSharp_MenuItemPtr | ImGui.MenuItem (string, string?, ref bool, bool) | |

## Tooltips

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| BeginTooltip | function | Native.IGSharp_BeginTooltip | ImGui.BeginTooltip | |
| EndTooltip | function | Native.IGSharp_EndTooltip | ImGui.EndTooltip | |
| SetTooltip | function | Native.IGSharp_SetTooltip | ImGui.SetTooltip | |
| SetTooltipV | function | - | - | variadic |
| BeginItemTooltip | function | Native.IGSharp_BeginItemTooltip | ImGui.BeginItemTooltip | |
| SetItemTooltip | function | - | - | deferred |
| SetItemTooltipV | function | - | - | variadic |

## Popups, Modals

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| BeginPopup | function | Native.IGSharp_BeginPopup | ImGui.BeginPopup | |
| BeginPopupModal | function | Native.IGSharp_BeginPopupModal | ImGui.BeginPopupModal | Overloads with/without ref bool |
| EndPopup | function | Native.IGSharp_EndPopup | ImGui.EndPopup | |
| OpenPopup (str) | function | Native.IGSharp_OpenPopup | ImGui.OpenPopup | |
| OpenPopup (ID) | function | - | - | deferred |
| OpenPopupOnItemClick | function | - | - | deferred |
| CloseCurrentPopup | function | Native.IGSharp_CloseCurrentPopup | ImGui.CloseCurrentPopup | |
| BeginPopupContextItem | function | Native.IGSharp_BeginPopupContextItem | ImGui.BeginPopupContextItem | |
| BeginPopupContextWindow | function | Native.IGSharp_BeginPopupContextWindow | ImGui.BeginPopupContextWindow | |
| BeginPopupContextVoid | function | - | - | deferred |
| IsPopupOpen | function | Native.IGSharp_IsPopupOpen | ImGui.IsPopupOpen | |

## Tables

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| BeginTable | function | Native.IGSharp_BeginTable | ImGui.BeginTable | |
| EndTable | function | Native.IGSharp_EndTable | ImGui.EndTable | |
| TableNextRow | function | Native.IGSharp_TableNextRow | ImGui.TableNextRow | |
| TableNextColumn | function | Native.IGSharp_TableNextColumn | ImGui.TableNextColumn | |
| TableSetColumnIndex | function | Native.IGSharp_TableSetColumnIndex | ImGui.TableSetColumnIndex | |
| TableSetupColumn | function | Native.IGSharp_TableSetupColumn | ImGui.TableSetupColumn | |
| TableSetupScrollFreeze | function | Native.IGSharp_TableSetupScrollFreeze | ImGui.TableSetupScrollFreeze | |
| TableHeadersRow | function | Native.IGSharp_TableHeadersRow | ImGui.TableHeadersRow | |
| TableHeader | function | Native.IGSharp_TableHeader | ImGui.TableHeader | |
| TableGetSortSpecs | function | Native.IGSharp_TableGetSortSpecs | ImGui.TableGetSortSpecs | Returns TableSortSpecs |
| TableGetColumnCount | function | Native.IGSharp_TableGetColumnCount | ImGui.TableGetColumnCount | |
| TableGetColumnIndex | function | Native.IGSharp_TableGetColumnIndex | ImGui.TableGetColumnIndex | |
| TableGetRowIndex | function | Native.IGSharp_TableGetRowIndex | ImGui.TableGetRowIndex | |
| TableGetColumnName | function | Native.IGSharp_TableGetColumnName | ImGui.TableGetColumnName | |
| TableGetColumnFlags | function | Native.IGSharp_TableGetColumnFlags | ImGui.TableGetColumnFlags | |
| TableSetColumnEnabled | function | Native.IGSharp_TableSetColumnEnabled | ImGui.TableSetColumnEnabled | |
| TableGetHoveredColumn | function | Native.IGSharp_TableGetHoveredColumn | ImGui.TableGetHoveredColumn | |
| TableSetBgColor | function | Native.IGSharp_TableSetBgColor | ImGui.TableSetBgColor | |

## Tab Bars, Tabs

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| BeginTabBar | function | Native.IGSharp_BeginTabBar | ImGui.BeginTabBar | |
| EndTabBar | function | Native.IGSharp_EndTabBar | ImGui.EndTabBar | |
| BeginTabItem | function | Native.IGSharp_BeginTabItem | ImGui.BeginTabItem | Overloads with/without ref bool |
| EndTabItem | function | Native.IGSharp_EndTabItem | ImGui.EndTabItem | |
| TabItemButton | function | Native.IGSharp_TabItemButton | ImGui.TabItemButton | |
| SetTabItemClosed | function | Native.IGSharp_SetTabItemClosed | ImGui.SetTabItemClosed | |

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
| BeginDragDropSource | function | Native.IGSharp_BeginDragDropSource | ImGui.BeginDragDropSource | |
| SetDragDropPayload | function | Native.IGSharp_SetDragDropPayload | ImGui.SetDragDropPayload | Overloads: ReadOnlySpan&lt;byte&gt;, generic T |
| EndDragDropSource | function | Native.IGSharp_EndDragDropSource | ImGui.EndDragDropSource | |
| BeginDragDropTarget | function | Native.IGSharp_BeginDragDropTarget | ImGui.BeginDragDropTarget | |
| AcceptDragDropPayload | function | Native.IGSharp_AcceptDragDropPayload | ImGui.AcceptDragDropPayload | Overloads: returns DragDropPayload, generic out T |
| EndDragDropTarget | function | Native.IGSharp_EndDragDropTarget | ImGui.EndDragDropTarget | |
| GetDragDropPayload | function | Native.IGSharp_GetDragDropPayload | ImGui.GetDragDropPayload | |

## Disabling

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| BeginDisabled | function | Native.IGSharp_BeginDisabled | ImGui.BeginDisabled | |
| EndDisabled | function | Native.IGSharp_EndDisabled | ImGui.EndDisabled | |

## Clipping

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| PushClipRect | function | Native.IGSharp_DrawList_PushClipRect | DrawList.PushClipRect | On DrawList |
| PopClipRect | function | Native.IGSharp_DrawList_PopClipRect | DrawList.PopClipRect | On DrawList |

## Focus, Activation

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SetItemDefaultFocus | function | Native.IGSharp_SetItemDefaultFocus | ImGui.SetItemDefaultFocus | |
| SetKeyboardFocusHere | function | Native.IGSharp_SetKeyboardFocusHere | ImGui.SetKeyboardFocusHere | |
| SetNextItemAllowOverlap | function | Native.IGSharp_SetNextItemAllowOverlap | ImGui.SetNextItemAllowOverlap | |

## Item/Widgets Utilities and Query Functions

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| IsItemHovered | function | Native.IGSharp_IsItemHovered | ImGui.IsItemHovered | |
| IsItemActive | function | Native.IGSharp_IsItemActive | ImGui.IsItemActive | |
| IsItemFocused | function | Native.IGSharp_IsItemFocused | ImGui.IsItemFocused | |
| IsItemClicked | function | Native.IGSharp_IsItemClicked | ImGui.IsItemClicked | |
| IsItemVisible | function | Native.IGSharp_IsItemVisible | ImGui.IsItemVisible | |
| IsItemEdited | function | Native.IGSharp_IsItemEdited | ImGui.IsItemEdited | |
| IsItemActivated | function | Native.IGSharp_IsItemActivated | ImGui.IsItemActivated | |
| IsItemDeactivated | function | Native.IGSharp_IsItemDeactivated | ImGui.IsItemDeactivated | |
| IsItemDeactivatedAfterEdit | function | Native.IGSharp_IsItemDeactivatedAfterEdit | ImGui.IsItemDeactivatedAfterEdit | |
| IsItemToggledOpen | function | Native.IGSharp_IsItemToggledOpen | ImGui.IsItemToggledOpen | |
| IsAnyItemHovered | function | Native.IGSharp_IsAnyItemHovered | ImGui.IsAnyItemHovered | |
| IsAnyItemActive | function | Native.IGSharp_IsAnyItemActive | ImGui.IsAnyItemActive | |
| IsAnyItemFocused | function | Native.IGSharp_IsAnyItemFocused | ImGui.IsAnyItemFocused | |
| GetItemID | function | Native.IGSharp_GetItemID | ImGui.GetItemId | |
| GetItemRectMin | function | Native.IGSharp_GetItemRectMin | ImGui.GetItemRectMin | |
| GetItemRectMax | function | Native.IGSharp_GetItemRectMax | ImGui.GetItemRectMax | |
| GetItemRectSize | function | Native.IGSharp_GetItemRectSize | ImGui.GetItemRectSize | |

## Miscellaneous Utilities

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| IsRectVisible (size) | function | - | - | deferred |
| IsRectVisible (min, max) | function | - | - | deferred |
| GetTime | function | - | - | deferred |
| GetFrameCount | function | - | - | deferred |
| GetBackgroundDrawList | function | Native.IGSharp_GetBackgroundDrawList | ImGui.GetBackgroundDrawList | Returns DrawList |
| GetForegroundDrawList | function | Native.IGSharp_GetForegroundDrawList | ImGui.GetForegroundDrawList | Returns DrawList |
| GetWindowDrawList | function | Native.IGSharp_GetWindowDrawList | ImGui.GetWindowDrawList | Returns DrawList |
| GetDrawListSharedData | function | - | - | internal |

## Text Utilities

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| CalcTextSize | function | Native.IGSharp_CalcTextSize | ImGui.CalcTextSize | |

## Color Utilities

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| ColorConvertU32ToFloat4 | function | Native.IGSharp_ColorConvertU32ToFloat4 | ImGui.ColorConvertU32ToFloat4 | |
| ColorConvertFloat4ToU32 | function | Native.IGSharp_ColorConvertFloat4ToU32 | ImGui.ColorConvertFloat4ToU32 | |
| ColorConvertRGBtoHSV | function | Native.IGSharp_ColorConvertRGBtoHSV | ImGui.ColorConvertRGBtoHSV | |
| ColorConvertHSVtoRGB | function | Native.IGSharp_ColorConvertHSVtoRGB | ImGui.ColorConvertHSVtoRGB | |

## Inputs Utilities

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| IsKeyDown | function | Native.IGSharp_IsKeyDown | ImGui.IsKeyDown | |
| IsKeyPressed | function | Native.IGSharp_IsKeyPressed | ImGui.IsKeyPressed | |
| IsKeyReleased | function | Native.IGSharp_IsKeyReleased | ImGui.IsKeyReleased | |
| IsKeyChordPressed | function | Native.IGSharp_IsKeyChordPressed | ImGui.IsKeyChordPressed | |
| GetKeyPressedAmount | function | - | - | deferred |
| GetKeyName | function | - | - | deferred |
| SetNextFrameWantCaptureKeyboard | function | - | - | deferred |
| Shortcut | function | - | - | deferred |
| SetNextItemShortcut | function | - | - | deferred |
| IsMouseDown | function | Native.IGSharp_IsMouseDown | ImGui.IsMouseDown | |
| IsMouseClicked | function | Native.IGSharp_IsMouseClicked | ImGui.IsMouseClicked | |
| IsMouseReleased | function | Native.IGSharp_IsMouseReleased | ImGui.IsMouseReleased | |
| IsMouseDoubleClicked | function | Native.IGSharp_IsMouseDoubleClicked | ImGui.IsMouseDoubleClicked | |
| IsMouseHoveringRect | function | Native.IGSharp_IsMouseHoveringRect | ImGui.IsMouseHoveringRect | |
| IsMousePosValid | function | Native.IGSharp_IsMousePosValid | ImGui.IsMousePosValid | |
| GetMousePos | function | Native.IGSharp_GetMousePos | ImGui.GetMousePos | |
| GetMousePosOnOpeningCurrentPopup | function | - | - | deferred |
| IsMouseDragging | function | Native.IGSharp_IsMouseDragging | ImGui.IsMouseDragging | |
| GetMouseDragDelta | function | Native.IGSharp_GetMouseDragDelta | ImGui.GetMouseDragDelta | |
| ResetMouseDragDelta | function | - | - | deferred |
| GetMouseCursor | function | Native.IGSharp_GetMouseCursor | ImGui.GetMouseCursor | |
| SetMouseCursor | function | Native.IGSharp_SetMouseCursor | ImGui.SetMouseCursor | |
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

## ImGuiIO Accessors

ImGuiIO is exposed as a static class `Io` with field accessors via custom C wrappers.

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| io.WantCaptureMouse | field | Native.IGSharp_IO_GetWantCaptureMouse | Io.WantCaptureMouse, ImGui.WantCaptureMouse | |
| io.WantCaptureKeyboard | field | Native.IGSharp_IO_GetWantCaptureKeyboard | Io.WantCaptureKeyboard, ImGui.WantCaptureKeyboard | |
| io.WantTextInput | field | Native.IGSharp_IO_GetWantTextInput | Io.WantTextInput | |
| io.WantSetMousePos | field | Native.IGSharp_IO_GetWantSetMousePos | Io.WantSetMousePos | |
| io.WantSaveIniSettings | field | Native.IGSharp_IO_GetWantSaveIniSettings, _Set | Io.WantSaveIniSettings | |
| io.NavActive | field | Native.IGSharp_IO_GetNavActive | Io.NavActive | |
| io.NavVisible | field | Native.IGSharp_IO_GetNavVisible | Io.NavVisible | |
| io.Framerate | field | Native.IGSharp_IO_GetFramerate | Io.Framerate, ImGui.Framerate | |
| io.ConfigFlags | field | Native.IGSharp_IO_GetConfigFlags, _SetConfigFlags | Io.ConfigFlags | |
| io.BackendFlags | field | Native.IGSharp_IO_GetBackendFlags, _SetBackendFlags | Io.BackendFlags | |
| io.IniFilename | field | Native.IGSharp_IO_SetIniFilename | Io.SetIniFilename, ImGui.SetIniFilename | |
| io.DisplaySize | field | Native.IGSharp_IO_GetDisplaySize, _SetDisplaySize | Io.DisplaySize | |
| io.DisplayFramebufferScale | field | Native.IGSharp_IO_GetDisplayFramebufferScale, _Set | Io.DisplayFramebufferScale | |
| io.DeltaTime | field | Native.IGSharp_IO_GetDeltaTime, _SetDeltaTime | Io.DeltaTime | |
| io.MousePos | field | Native.IGSharp_IO_GetMousePos | Io.MousePos | |
| io.MouseDelta | field | Native.IGSharp_IO_GetMouseDelta | Io.MouseDelta | |
| io.MouseWheel | field | Native.IGSharp_IO_GetMouseWheel | Io.MouseWheel | |
| io.MouseWheelH | field | Native.IGSharp_IO_GetMouseWheelH | Io.MouseWheelHorizontal | |
| io.KeyCtrl | field | Native.IGSharp_IO_GetKeyCtrl | Io.KeyCtrl | |
| io.KeyShift | field | Native.IGSharp_IO_GetKeyShift | Io.KeyShift | |
| io.KeyAlt | field | Native.IGSharp_IO_GetKeyAlt | Io.KeyAlt | |
| io.KeySuper | field | Native.IGSharp_IO_GetKeySuper | Io.KeySuper | |
| io.MouseDoubleClickTime | field | Native.IGSharp_IO_GetMouseDoubleClickTime, _Set | Io.MouseDoubleClickTime | |
| io.MouseDoubleClickMaxDist | field | Native.IGSharp_IO_GetMouseDoubleClickMaxDist, _Set | Io.MouseDoubleClickMaxDist | |
| io.MouseDragThreshold | field | Native.IGSharp_IO_GetMouseDragThreshold, _Set | Io.MouseDragThreshold | |
| io.KeyRepeatDelay | field | Native.IGSharp_IO_GetKeyRepeatDelay, _Set | Io.KeyRepeatDelay | |
| io.KeyRepeatRate | field | Native.IGSharp_IO_GetKeyRepeatRate, _Set | Io.KeyRepeatRate | |
| io.MetricsRenderVertices | field | Native.IGSharp_IO_GetMetricsRenderVertices | Io.MetricsRenderVertices | |
| io.MetricsRenderIndices | field | Native.IGSharp_IO_GetMetricsRenderIndices | Io.MetricsRenderIndices | |
| io.MetricsRenderWindows | field | Native.IGSharp_IO_GetMetricsRenderWindows | Io.MetricsRenderWindows | |
| io.MetricsActiveWindows | field | Native.IGSharp_IO_GetMetricsActiveWindows | Io.MetricsActiveWindows | |
| io.Fonts | field | Native.IGSharp_IO_GetFonts | ImGui.GetFontAtlas | Returns FontAtlas |
| io.FontDefault | field | Native.IGSharp_IO_GetFontDefault, _SetFontDefault | ImGui.GetDefaultFont, ImGui.SetDefaultFont | |
| io.AddKeyEvent | function | Native.IGSharp_IO_AddKeyEvent | Io.AddKeyEvent | |
| io.AddKeyAnalogEvent | function | Native.IGSharp_IO_AddKeyAnalogEvent | Io.AddKeyAnalogEvent | |
| io.AddMousePosEvent | function | Native.IGSharp_IO_AddMousePosEvent | Io.AddMousePosEvent | |
| io.AddMouseButtonEvent | function | Native.IGSharp_IO_AddMouseButtonEvent | Io.AddMouseButtonEvent | |
| io.AddMouseWheelEvent | function | Native.IGSharp_IO_AddMouseWheelEvent | Io.AddMouseWheelEvent | |
| io.AddMouseSourceEvent | function | Native.IGSharp_IO_AddMouseSourceEvent | Io.AddMouseSourceEvent | |
| io.AddFocusEvent | function | Native.IGSharp_IO_AddFocusEvent | Io.AddFocusEvent | |
| io.AddInputCharacter | function | Native.IGSharp_IO_AddInputCharacter | Io.AddInputCharacter | |
| io.AddInputCharacterUTF16 | function | Native.IGSharp_IO_AddInputCharacterUTF16 | Io.AddInputCharacterUTF16 | |
| io.AddInputCharactersUTF8 | function | Native.IGSharp_IO_AddInputCharactersUTF8 | Io.AddInputCharactersUTF8 | |
| io.SetAppAcceptingEvents | function | Native.IGSharp_IO_SetAppAcceptingEvents | Io.SetAppAcceptingEvents | |
| io.ClearEventsQueue | function | Native.IGSharp_IO_ClearEventsQueue | Io.ClearEventsQueue | |
| io.ClearInputKeys | function | Native.IGSharp_IO_ClearInputKeys | Io.ClearInputKeys | |
| io.ClearInputMouse | function | Native.IGSharp_IO_ClearInputMouse | Io.ClearInputMouse | |

## ImGuiStyle Accessors

ImGuiStyle is exposed as a static class `Style` with field accessors via custom C wrappers. Most properties have both get and set.

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| style.FontSizeBase | field | Native.IGSharp_Style_GetFontSizeBase, _Set | Style.FontSizeBase | |
| style.FontScaleMain | field | Native.IGSharp_Style_GetFontScaleMain, _Set | Style.FontScaleMain | |
| style.FontScaleDpi | field | Native.IGSharp_Style_GetFontScaleDpi, _Set | Style.FontScaleDpi | |
| style.Alpha | field | Native.IGSharp_Style_GetAlpha, _Set | Style.Alpha | |
| style.DisabledAlpha | field | Native.IGSharp_Style_GetDisabledAlpha, _Set | Style.DisabledAlpha | |
| style.WindowRounding | field | Native.IGSharp_Style_GetWindowRounding, _Set | Style.WindowRounding | |
| style.WindowBorderSize | field | Native.IGSharp_Style_GetWindowBorderSize, _Set | Style.WindowBorderSize | |
| style.WindowPadding | field | Native.IGSharp_Style_GetWindowPadding, _Set | Style.WindowPadding | |
| style.WindowMinSize | field | Native.IGSharp_Style_GetWindowMinSize, _Set | Style.WindowMinSize | |
| style.WindowTitleAlign | field | Native.IGSharp_Style_GetWindowTitleAlign, _Set | Style.WindowTitleAlign | |
| style.ChildRounding | field | Native.IGSharp_Style_GetChildRounding, _Set | Style.ChildRounding | |
| style.ChildBorderSize | field | Native.IGSharp_Style_GetChildBorderSize, _Set | Style.ChildBorderSize | |
| style.PopupRounding | field | Native.IGSharp_Style_GetPopupRounding, _Set | Style.PopupRounding | |
| style.PopupBorderSize | field | Native.IGSharp_Style_GetPopupBorderSize, _Set | Style.PopupBorderSize | |
| style.FrameRounding | field | Native.IGSharp_Style_GetFrameRounding, _Set | Style.FrameRounding | |
| style.FrameBorderSize | field | Native.IGSharp_Style_GetFrameBorderSize, _Set | Style.FrameBorderSize | |
| style.FramePadding | field | Native.IGSharp_Style_GetFramePadding, _Set | Style.FramePadding | |
| style.ItemSpacing | field | Native.IGSharp_Style_GetItemSpacing, _Set | Style.ItemSpacing | |
| style.ItemInnerSpacing | field | Native.IGSharp_Style_GetItemInnerSpacing, _Set | Style.ItemInnerSpacing | |
| style.CellPadding | field | Native.IGSharp_Style_GetCellPadding, _Set | Style.CellPadding | |
| style.TouchExtraPadding | field | Native.IGSharp_Style_GetTouchExtraPadding, _Set | Style.TouchExtraPadding | |
| style.IndentSpacing | field | Native.IGSharp_Style_GetIndentSpacing, _Set | Style.IndentSpacing | |
| style.ColumnsMinSpacing | field | Native.IGSharp_Style_GetColumnsMinSpacing, _Set | Style.ColumnsMinSpacing | |
| style.ScrollbarSize | field | Native.IGSharp_Style_GetScrollbarSize, _Set | Style.ScrollbarSize | |
| style.ScrollbarRounding | field | Native.IGSharp_Style_GetScrollbarRounding, _Set | Style.ScrollbarRounding | |
| style.GrabMinSize | field | Native.IGSharp_Style_GetGrabMinSize, _Set | Style.GrabMinSize | |
| style.GrabRounding | field | Native.IGSharp_Style_GetGrabRounding, _Set | Style.GrabRounding | |
| style.ImageRounding | field | Native.IGSharp_Style_GetImageRounding, _Set | Style.ImageRounding | |
| style.ImageBorderSize | field | Native.IGSharp_Style_GetImageBorderSize, _Set | Style.ImageBorderSize | |
| style.TabRounding | field | Native.IGSharp_Style_GetTabRounding, _Set | Style.TabRounding | |
| style.TabBorderSize | field | Native.IGSharp_Style_GetTabBorderSize, _Set | Style.TabBorderSize | |
| style.SeparatorSize | field | Native.IGSharp_Style_GetSeparatorSize, _Set | Style.SeparatorSize | |
| style.ButtonTextAlign | field | Native.IGSharp_Style_GetButtonTextAlign, _Set | Style.ButtonTextAlign | |
| style.SelectableTextAlign | field | Native.IGSharp_Style_GetSelectableTextAlign, _Set | Style.SelectableTextAlign | |
| style.SeparatorTextAlign | field | Native.IGSharp_Style_GetSeparatorTextAlign, _Set | Style.SeparatorTextAlign | |
| style.SeparatorTextPadding | field | Native.IGSharp_Style_GetSeparatorTextPadding, _Set | Style.SeparatorTextPadding | |
| style.DisplayWindowPadding | field | Native.IGSharp_Style_GetDisplayWindowPadding, _Set | Style.DisplayWindowPadding | |
| style.DisplaySafeAreaPadding | field | Native.IGSharp_Style_GetDisplaySafeAreaPadding, _Set | Style.DisplaySafeAreaPadding | |
| style.MouseCursorScale | field | Native.IGSharp_Style_GetMouseCursorScale, _Set | Style.MouseCursorScale | |
| style.AntiAliasedLines | field | Native.IGSharp_Style_GetAntiAliasedLines, _Set | Style.AntiAliasedLines | |
| style.AntiAliasedFill | field | Native.IGSharp_Style_GetAntiAliasedFill, _Set | Style.AntiAliasedFill | |
| style.CurveTessellationTol | field | Native.IGSharp_Style_GetCurveTessellationTol, _Set | Style.CurveTessellationTol | |
| style.CircleTessellationMaxError | field | Native.IGSharp_Style_GetCircleTessellationMaxError, _Set | Style.CircleTessellationMaxError | |
| style.Colors[idx] | field | Native.IGSharp_Style_GetColor, _SetColor | Style.GetColor, Style.SetColor | |

## ImDrawList API

ImDrawList is exposed as a `readonly struct DrawList` wrapping the native pointer.

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| PushClipRect | function | Native.IGSharp_DrawList_PushClipRect | DrawList.PushClipRect | |
| PushClipRectFullScreen | function | Native.IGSharp_DrawList_PushClipRectFullScreen | DrawList.PushClipRectFullScreen | |
| PopClipRect | function | Native.IGSharp_DrawList_PopClipRect | DrawList.PopClipRect | |
| AddLine | function | Native.IGSharp_DrawList_AddLine | DrawList.AddLine | |
| AddRect | function | Native.IGSharp_DrawList_AddRect | DrawList.AddRect | |
| AddRectFilled | function | Native.IGSharp_DrawList_AddRectFilled | DrawList.AddRectFilled | |
| AddRectFilledMultiColor | function | Native.IGSharp_DrawList_AddRectFilledMultiColor | DrawList.AddRectFilledMultiColor | |
| AddQuad | function | Native.IGSharp_DrawList_AddQuad | DrawList.AddQuad | |
| AddQuadFilled | function | Native.IGSharp_DrawList_AddQuadFilled | DrawList.AddQuadFilled | |
| AddTriangle | function | Native.IGSharp_DrawList_AddTriangle | DrawList.AddTriangle | |
| AddTriangleFilled | function | Native.IGSharp_DrawList_AddTriangleFilled | DrawList.AddTriangleFilled | |
| AddCircle | function | Native.IGSharp_DrawList_AddCircle | DrawList.AddCircle | |
| AddCircleFilled | function | Native.IGSharp_DrawList_AddCircleFilled | DrawList.AddCircleFilled | |
| AddNgon | function | Native.IGSharp_DrawList_AddNgon | DrawList.AddNgon | |
| AddNgonFilled | function | Native.IGSharp_DrawList_AddNgonFilled | DrawList.AddNgonFilled | |
| AddEllipse | function | Native.IGSharp_DrawList_AddEllipse | DrawList.AddEllipse | |
| AddEllipseFilled | function | Native.IGSharp_DrawList_AddEllipseFilled | DrawList.AddEllipseFilled | |
| AddText | function | Native.IGSharp_DrawList_AddText | DrawList.AddText | |
| AddBezierCubic | function | Native.IGSharp_DrawList_AddBezierCubic | DrawList.AddBezierCubic | |
| AddBezierQuadratic | function | Native.IGSharp_DrawList_AddBezierQuadratic | DrawList.AddBezierQuadratic | |
| AddPolyline | function | Native.IGSharp_DrawList_AddPolyline | DrawList.AddPolyline | |
| AddConvexPolyFilled | function | Native.IGSharp_DrawList_AddConvexPolyFilled | DrawList.AddConvexPolyFilled | |
| AddConcavePolyFilled | function | Native.IGSharp_DrawList_AddConcavePolyFilled | DrawList.AddConcavePolyFilled | |
| AddImage | function | Native.IGSharp_DrawList_AddImage | DrawList.AddImage | Overloads: ulong textureId, GpuTexture |
| AddImageQuad | function | Native.IGSharp_DrawList_AddImageQuad | DrawList.AddImageQuad | Overloads: ulong textureId, GpuTexture |
| AddImageRounded | function | Native.IGSharp_DrawList_AddImageRounded | DrawList.AddImageRounded | Overloads: ulong textureId, GpuTexture |
| PathClear | function | Native.IGSharp_DrawList_PathClear | DrawList.PathClear | |
| PathLineTo | function | Native.IGSharp_DrawList_PathLineTo | DrawList.PathLineTo | |
| PathLineToMergeDuplicate | function | Native.IGSharp_DrawList_PathLineToMergeDuplicate | DrawList.PathLineToMergeDuplicate | |
| PathFillConvex | function | Native.IGSharp_DrawList_PathFillConvex | DrawList.PathFillConvex | |
| PathStroke | function | Native.IGSharp_DrawList_PathStroke | DrawList.PathStroke | |
| PathArcTo | function | Native.IGSharp_DrawList_PathArcTo | DrawList.PathArcTo | |
| PathArcToFast | function | Native.IGSharp_DrawList_PathArcToFast | DrawList.PathArcToFast | |
| PathBezierCubicCurveTo | function | Native.IGSharp_DrawList_PathBezierCubicCurveTo | DrawList.PathBezierCubicCurveTo | |
| PathBezierQuadraticCurveTo | function | Native.IGSharp_DrawList_PathBezierQuadraticCurveTo | DrawList.PathBezierQuadraticCurveTo | |
| PathRect | function | Native.IGSharp_DrawList_PathRect | DrawList.PathRect | |

## ImFontAtlas API

ImFontAtlas is exposed as a `readonly struct FontAtlas` wrapping the native pointer.

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| AddFontDefault | function | Native.IGSharp_FontAtlas_AddFontDefault | FontAtlas.AddDefaultFont | |
| AddFontFromFileTTF | function | Native.IGSharp_FontAtlas_AddFontFromFileTTF | FontAtlas.AddFontFromFileTTF | |
| AddFontFromMemoryTTF | function | Native.IGSharp_FontAtlas_AddFontFromMemoryTTF | FontAtlas.AddFontFromMemoryTTF | |
| AddFontFromMemoryCompressedTTF | function | Native.IGSharp_FontAtlas_AddFontFromMemoryCompressedTTF | FontAtlas.AddFontFromMemoryCompressedTTF | |
| Build | function | Native.IGSharp_FontAtlas_Build | FontAtlas.Build | |
| Clear | function | Native.IGSharp_FontAtlas_Clear | FontAtlas.Clear | |
| ClearFonts | function | Native.IGSharp_FontAtlas_ClearFonts | FontAtlas.ClearFonts | |
| GetFontCount | function | Native.IGSharp_FontAtlas_GetFontCount | FontAtlas.FontCount | |
| GetFont | function | Native.IGSharp_FontAtlas_GetFont | FontAtlas.GetFont | |

## ImFont API

ImFont is exposed as a `readonly struct Font` opaque handle. No methods on the wrapper itself currently.

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| (handle only) | struct | - | Font | Opaque handle |

## ImGuiViewport API

ImGuiViewport is exposed as a `readonly struct Viewport` wrapping the native pointer.

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| Pos | field | Native.IGSharp_Viewport_GetPos | Viewport.Pos | |
| Size | field | Native.IGSharp_Viewport_GetSize | Viewport.Size | |
| WorkPos | field | Native.IGSharp_Viewport_GetWorkPos | Viewport.WorkPos | |
| WorkSize | field | Native.IGSharp_Viewport_GetWorkSize | Viewport.WorkSize | |

## ImGuiListClipper API

ImGuiListClipper is exposed as a `sealed class ListClipper : IDisposable`.

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| (constructor) | function | Native.IGSharp_ListClipper_New | ListClipper.ctor | |
| (destructor) | function | Native.IGSharp_ListClipper_Delete | ListClipper.Dispose | |
| Begin | function | Native.IGSharp_ListClipper_Begin | ListClipper.Begin | |
| End | function | Native.IGSharp_ListClipper_End | ListClipper.End | |
| Step | function | Native.IGSharp_ListClipper_Step | ListClipper.Step | |
| IncludeItemsByIndex | function | Native.IGSharp_ListClipper_IncludeItemsByIndex | ListClipper.IncludeItemsByIndex | |
| SeekCursorForItem | function | Native.IGSharp_ListClipper_SeekCursorForItem | ListClipper.SeekCursorForItem | |
| DisplayStart | field | Native.IGSharp_ListClipper_GetDisplayStart | ListClipper.DisplayStart | |
| DisplayEnd | field | Native.IGSharp_ListClipper_GetDisplayEnd | ListClipper.DisplayEnd | |

## ImGuiPayload API

ImGuiPayload is exposed as a `readonly struct DragDropPayload` wrapping the native pointer.

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| Data, DataSize | field | Native.IGSharp_Payload_GetData, _GetDataSize | DragDropPayload.Data | Returned as ReadOnlySpan&lt;byte&gt; |
| DataType | field | Native.IGSharp_Payload_GetDataType | DragDropPayload.DataType | |
| IsDataType | function | Native.IGSharp_Payload_IsDataType | DragDropPayload.IsDataType | |
| IsPreview | function | Native.IGSharp_Payload_IsPreview | DragDropPayload.IsPreview | |
| IsDelivery | function | Native.IGSharp_Payload_IsDelivery | DragDropPayload.IsDelivery | |
| (generic) | - | - | DragDropPayload.TryGetValue&lt;T&gt; | Custom helper |

## ImGuiMultiSelectIO / ImGuiSelectionRequest API

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| io.RequestsCount | field | Native.IGSharp_MultiSelectIO_GetRequestsCount | MultiSelectIO.RequestsCount | |
| io.Requests[idx] | field | Native.IGSharp_MultiSelectIO_GetRequest | MultiSelectIO.GetRequest | |
| io.RangeSrcItem | field | Native.IGSharp_MultiSelectIO_GetRangeSrcItem | MultiSelectIO.RangeSrcItem | |
| io.NavIdItem | field | Native.IGSharp_MultiSelectIO_GetNavIdItem | MultiSelectIO.NavIdItem | |
| io.NavIdSelected | field | Native.IGSharp_MultiSelectIO_GetNavIdSelected | MultiSelectIO.NavIdSelected | |
| io.RangeSrcReset | field | Native.IGSharp_MultiSelectIO_GetRangeSrcReset, _Set | MultiSelectIO.RangeSrcReset | |
| io.ItemsCount | field | Native.IGSharp_MultiSelectIO_GetItemsCount | MultiSelectIO.ItemsCount | |
| req.Type | field | Native.IGSharp_SelectionRequest_GetType | SelectionRequest.Type | |
| req.Selected | field | Native.IGSharp_SelectionRequest_GetSelected | SelectionRequest.Selected | |
| req.RangeDirection | field | Native.IGSharp_SelectionRequest_GetRangeDirection | SelectionRequest.RangeDirection | |
| req.RangeFirstItem | field | Native.IGSharp_SelectionRequest_GetRangeFirstItem | SelectionRequest.RangeFirstItem | |
| req.RangeLastItem | field | Native.IGSharp_SelectionRequest_GetRangeLastItem | SelectionRequest.RangeLastItem | |

## ImGuiTableSortSpecs / ImGuiTableColumnSortSpecs API

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| specs.SpecsCount | field | Native.IGSharp_TableSortSpecs_GetSpecsCount | TableSortSpecs.SpecsCount | |
| specs.Specs[idx] | field | Native.IGSharp_TableSortSpecs_GetSpec | TableSortSpecs.GetSpec | |
| specs.SpecsDirty | field | Native.IGSharp_TableSortSpecs_GetSpecsDirty, _Set | TableSortSpecs.SpecsDirty | |
| col.ColumnUserID | field | Native.IGSharp_TableColumnSortSpecs_GetColumnUserID | TableColumnSortSpecs.ColumnUserId | |
| col.ColumnIndex | field | Native.IGSharp_TableColumnSortSpecs_GetColumnIndex | TableColumnSortSpecs.ColumnIndex | |
| col.SortOrder | field | Native.IGSharp_TableColumnSortSpecs_GetSortOrder | TableColumnSortSpecs.SortOrder | |
| col.SortDirection | field | Native.IGSharp_TableColumnSortSpecs_GetSortDirection | TableColumnSortSpecs.SortDirection | |

## ImGuiInputTextCallbackData API

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| data.EventFlag | field | Native.IGSharp_InputTextCallbackData_GetEventFlag | InputTextCallbackData.EventFlag | |
| data.Flags | field | Native.IGSharp_InputTextCallbackData_GetFlags | InputTextCallbackData.Flags | |
| data.EventKey | field | Native.IGSharp_InputTextCallbackData_GetEventKey | InputTextCallbackData.EventKey | |
| data.EventChar | field | Native.IGSharp_InputTextCallbackData_GetEventChar, _Set | InputTextCallbackData.EventChar | |
| data.EventActivated | field | Native.IGSharp_InputTextCallbackData_GetEventActivated | InputTextCallbackData.EventActivated | |
| data.Buf, BufTextLen | field | Native.IGSharp_InputTextCallbackData_GetBuf, _Set, _GetBufTextLen, _Set | InputTextCallbackData.Buf, BufTextLen | |
| data.BufSize | field | Native.IGSharp_InputTextCallbackData_GetBufSize | InputTextCallbackData.BufSize | |
| data.BufDirty | field | Native.IGSharp_InputTextCallbackData_GetBufDirty, _Set | InputTextCallbackData.BufDirty | |
| data.CursorPos | field | Native.IGSharp_InputTextCallbackData_GetCursorPos, _Set | InputTextCallbackData.CursorPos | |
| data.SelectionStart | field | Native.IGSharp_InputTextCallbackData_GetSelectionStart, _Set | InputTextCallbackData.SelectionStart | |
| data.SelectionEnd | field | Native.IGSharp_InputTextCallbackData_GetSelectionEnd, _Set | InputTextCallbackData.SelectionEnd | |
| data.UserData | field | Native.IGSharp_InputTextCallbackData_GetUserData | (internal) | Used by callback marshalling |
| data.DeleteChars | function | Native.IGSharp_InputTextCallbackData_DeleteChars | InputTextCallbackData.DeleteChars | |
| data.InsertChars | function | Native.IGSharp_InputTextCallbackData_InsertChars | InputTextCallbackData.InsertChars | |
| data.SelectAll | function | Native.IGSharp_InputTextCallbackData_SelectAll | InputTextCallbackData.SelectAll | |
| data.ClearSelection | function | Native.IGSharp_InputTextCallbackData_ClearSelection | InputTextCallbackData.ClearSelection | |
| data.HasSelection | function | Native.IGSharp_InputTextCallbackData_HasSelection | InputTextCallbackData.HasSelection | |
| data.ResizeBuf | function | Native.IGSharp_InputTextCallbackData_ResizeBuf | InputTextCallbackData.ResizeBuf | |

## Value types

| ImGui Symbol | Kind | Managed Wrapper | Notes |
|---|---|---|---|
| ImVec2 | struct | Vec2 | `readonly record struct Vec2(float X, float Y)` |
| ImVec4 | struct | Vec4 | `readonly record struct Vec4(float X, float Y, float Z, float W)` |

## Flags & Enumerations

All flag/enum types are wrapped as public C# enums in `SdlSharp.ImGui` namespace. Most are passed to native APIs as `int` after a cast.

| ImGui Symbol | Managed Wrapper | Notes |
|---|---|---|
| ImGuiBackendFlags | BackendFlags | |
| ImGuiButtonFlags | ButtonFlags | |
| ImGuiChildFlags | ChildFlags | |
| ImGuiCol | Col | |
| ImGuiColorEditFlags | ColorEditFlags | |
| ImGuiComboFlags | ComboFlags | |
| ImGuiCond | Cond | |
| ImGuiConfigFlags | ConfigFlags | |
| ImGuiDataType | DataType | |
| ImGuiDir | Dir | |
| ImGuiDragDropFlags | DragDropFlags | |
| ImDrawFlags | DrawFlags | |
| ImGuiFocusedFlags | FocusedFlags | |
| ImGuiHoveredFlags | HoveredFlags | |
| ImGuiInputTextFlags | InputTextFlags | |
| ImGuiItemFlags | ItemFlags | |
| ImGuiKey | Key | |
| ImGuiMouseButton | MouseButton | |
| ImGuiMouseCursor | MouseCursor | |
| ImGuiMouseSource | MouseSource | |
| ImGuiMultiSelectFlags | MultiSelectFlags | |
| ImGuiPopupFlags | PopupFlags | |
| ImGuiSelectableFlags | SelectableFlags | |
| ImGuiSelectionRequestType | SelectionRequestType | |
| ImGuiSliderFlags | SliderFlags | |
| ImGuiSortDirection | SortDirection | |
| ImGuiStyleVar | StyleVar | |
| ImGuiTabBarFlags | TabBarFlags | |
| ImGuiTabItemFlags | TabItemFlags | |
| ImGuiTableBgTarget | TableBgTarget | |
| ImGuiTableColumnFlags | TableColumnFlags | |
| ImGuiTableFlags | TableFlags | |
| ImGuiTableRowFlags | TableRowFlags | |
| ImGuiTreeNodeFlags | TreeNodeFlags | |
| ImGuiWindowFlags | WindowFlags | |

## Delegates

| ImGui Symbol | Managed Wrapper | Notes |
|---|---|---|
| ImGuiInputTextCallback | InputTextCallback | `delegate int InputTextCallback(InputTextCallbackData data)` |
| (PlotLines/Histogram getter) | PlotValuesGetter | `delegate float PlotValuesGetter(int idx)` |

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
