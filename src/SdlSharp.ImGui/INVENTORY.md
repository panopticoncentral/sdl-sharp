# Dear ImGui API Inventory

Cross-reference of Dear ImGui (imgui.h) sections with SdlSharp.ImGui native bindings and managed wrappers.

- **Native Wrapper**: Qualified name in the `SdlSharp.ImGui.Native` class (e.g. `Native.IGSharp_Begin`). Mirror-struct field access is shown as `Native.IGSharp_GetIO()->Field`.
- **Managed Wrapper**: Qualified name of the public C# API (e.g. `ImGui.Begin`).
- **Notes**: Why an unwrapped API is skipped: *deferred* = planned but not yet done, *variadic* = C va_list/printf-style, *niche* = rarely needed, *internal* = not part of the public API / backend-owned, *C++ only* = template or operator overload, *redundant* = another wrapper covers it, *.NET* = a BCL type covers it.
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
| GetIO | function | Native.IGSharp_GetIO | Io (static class) | Direct mirror-struct access |
| GetPlatformIO | function | Native.IGSharp_GetPlatformIO | PlatformIO (static class) | Handlers + properties |
| GetStyle | function | Native.IGSharp_GetStyle | Style (static class) | Direct mirror-struct access |
| NewFrame | function | Native.IGSharp_NewFrame | ImGuiBackend.NewFrame (internal) | Called by backend |
| EndFrame | function | Native.IGSharp_EndFrame | ImGui.EndFrame | |
| Render | function | Native.IGSharp_Render | ImGui.Render | |
| GetDrawData | function | Native.IGSharp_GetDrawData | ImGui.GetDrawData | Returns DrawData |
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
| BeginChild (str) | function | Native.IGSharp_BeginChild | ImGui.BeginChild (string) | |
| BeginChild (ID) | function | Native.IGSharp_BeginChildID | ImGui.BeginChild (uint) | |
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
| SetWindowPos (current) | function | Native.IGSharp_SetWindowPos | ImGui.SetWindowPos (x, y) | |
| SetWindowSize (current) | function | Native.IGSharp_SetWindowSize | ImGui.SetWindowSize (w, h) | |
| SetWindowCollapsed (current) | function | Native.IGSharp_SetWindowCollapsed | ImGui.SetWindowCollapsed (bool) | |
| SetWindowFocus (current) | function | Native.IGSharp_SetWindowFocus | ImGui.SetWindowFocus () | |
| SetWindowPos (named) | function | Native.IGSharp_SetWindowPosNamed | ImGui.SetWindowPos (string, x, y) | |
| SetWindowSize (named) | function | Native.IGSharp_SetWindowSizeNamed | ImGui.SetWindowSize (string, w, h) | |
| SetWindowCollapsed (named) | function | Native.IGSharp_SetWindowCollapsedNamed | ImGui.SetWindowCollapsed (string, bool) | |
| SetWindowFocus (named) | function | Native.IGSharp_SetWindowFocusNamed | ImGui.SetWindowFocus (string) | |
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
| PushStyleVarX | function | Native.IGSharp_PushStyleVarX | ImGui.PushStyleVarX | |
| PushStyleVarY | function | Native.IGSharp_PushStyleVarY | ImGui.PushStyleVarY | |
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
| GetFontBaked | function | Native.IGSharp_GetFontBaked | ImGui.GetFontBaked | Returns FontBaked |
| GetFontTexUvWhitePixel | function | Native.IGSharp_GetFontTexUvWhitePixel | ImGui.GetFontTexUvWhitePixel | |
| GetColorU32 (idx) | function | Native.IGSharp_GetColorU32 | ImGui.GetColorU32 (Col) | |
| GetColorU32 (Vec4) | function | Native.IGSharp_GetColorU32Vec4 | ImGui.GetColorU32 (float r,g,b,a) | |
| GetColorU32 (U32) | function | Native.IGSharp_GetColorU32Packed | ImGui.GetColorU32 (uint) | |
| GetStyleColorVec4 | function | Native.IGSharp_GetStyleColorVec4 | ImGui.GetStyleColorVec4, Style.GetColor | |
| ScaleAllSizes | function | Native.IGSharp_Style_ScaleAllSizes | ImGui.ScaleAllSizes | |
| SetFontScaleDpi | function | Native.IGSharp_GetStyle()->FontScaleDpi | ImGui.SetFontScaleDpi | Custom wrapper |

## Layout cursor positioning

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| GetCursorScreenPos | function | Native.IGSharp_GetCursorScreenPos | ImGui.GetCursorScreenPos | |
| SetCursorScreenPos | function | Native.IGSharp_SetCursorScreenPos | ImGui.SetCursorScreenPos | |
| GetContentRegionAvail | function | Native.IGSharp_GetContentRegionAvail | ImGui.GetContentRegionAvail | |
| GetCursorPos | function | Native.IGSharp_GetCursorPos | ImGui.GetCursorPos | |
| GetCursorPosX | function | Native.IGSharp_GetCursorPosX | ImGui.GetCursorPosX | |
| GetCursorPosY | function | Native.IGSharp_GetCursorPosY | ImGui.GetCursorPosY | |
| SetCursorPos | function | Native.IGSharp_SetCursorPos | ImGui.SetCursorPos | |
| SetCursorPosX | function | Native.IGSharp_SetCursorPosX | ImGui.SetCursorPosX | |
| SetCursorPosY | function | Native.IGSharp_SetCursorPosY | ImGui.SetCursorPosY | |
| GetCursorStartPos | function | Native.IGSharp_GetCursorStartPos | ImGui.GetCursorStartPos | |

## Other layout functions

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| Separator | function | Native.IGSharp_Separator | ImGui.Separator | |
| SameLine | function | Native.IGSharp_SameLine | ImGui.SameLine | |
| NewLine | function | Native.IGSharp_NewLine | ImGui.NewLine | |
| Spacing | function | Native.IGSharp_Spacing | ImGui.Spacing | |
| Dummy | function | Native.IGSharp_Dummy | ImGui.Dummy | |
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
| PushID (str begin/end) | function | Native.IGSharp_PushIDStrRange | - | C# substrings cover ranges |
| PushID (ptr) | function | Native.IGSharp_PushIDPtr | ImGui.PushID (nint) | |
| PushID (int) | function | Native.IGSharp_PushIDInt | ImGui.PushID (int) | |
| PopID | function | Native.IGSharp_PopID | ImGui.PopID | |
| GetID (str) | function | Native.IGSharp_GetIDStr | ImGui.GetID (string) | |
| GetID (str begin/end) | function | Native.IGSharp_GetIDStrRange | - | C# substrings cover ranges |
| GetID (ptr) | function | Native.IGSharp_GetIDPtr | ImGui.GetID (nint) | |
| GetID (int) | function | Native.IGSharp_GetIDInt | ImGui.GetID (int) | |

## Widgets: Text

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| TextUnformatted | function | Native.IGSharp_TextUnformatted | ImGui.TextUnformatted | |
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
| CheckboxFlags (int) | function | Native.IGSharp_CheckboxFlags | ImGui.CheckboxFlags (ref int) | |
| CheckboxFlags (uint) | function | Native.IGSharp_CheckboxFlagsUInt | ImGui.CheckboxFlags (ref uint) | |
| RadioButton | function | Native.IGSharp_RadioButton | ImGui.RadioButton (string, bool) | |
| RadioButton (int) | function | Native.IGSharp_RadioButtonInt | ImGui.RadioButton (string, ref int, int) | |
| ProgressBar | function | Native.IGSharp_ProgressBar | ImGui.ProgressBar | |
| Bullet | function | Native.IGSharp_Bullet | ImGui.Bullet | |

## Widgets: Images

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| Image | function | Native.IGSharp_Image | ImGui.Image | Overloads for ulong textureId and GpuTexture |
| ImageWithBg | function | Native.IGSharp_ImageWithBg | ImGui.Image (uv/tint/border overload) | Border color maps to bg color |
| ImageButton | function | Native.IGSharp_ImageButton | ImGui.ImageButton | Overloads for ulong textureId and GpuTexture |
| Image (ImTextureData) | function | Native.IGSharp_ImageTextureData | - | deferred — TextureData image overloads |
| ImageWithBg (ImTextureData) | function | Native.IGSharp_ImageWithBgTextureData | - | deferred — TextureData image overloads |
| ImageButton (ImTextureData) | function | Native.IGSharp_ImageButtonTextureData | - | deferred — TextureData image overloads |

## Widgets: Combo Box (Dropdown)

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| BeginCombo | function | Native.IGSharp_BeginCombo | ImGui.BeginCombo | |
| EndCombo | function | Native.IGSharp_EndCombo | ImGui.EndCombo | |
| Combo (items[]) | function | Native.IGSharp_Combo | ImGui.Combo (string[]) | |
| Combo (str zeros) | function | Native.IGSharp_ComboStr | - | redundant — array overload covers it |
| Combo (getter) | function | Native.IGSharp_ComboCallback | ImGui.Combo (Func&lt;int, string&gt;) | |

## Widgets: Drag Sliders

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| DragFloat | function | Native.IGSharp_DragFloat | ImGui.DragFloat | |
| DragFloat2 | function | Native.IGSharp_DragFloat2 | ImGui.DragFloat2 | |
| DragFloat3 | function | Native.IGSharp_DragFloat3 | ImGui.DragFloat3 | |
| DragFloat4 | function | Native.IGSharp_DragFloat4 | ImGui.DragFloat4 | |
| DragFloatRange2 | function | Native.IGSharp_DragFloatRange2 | ImGui.DragFloatRange2 | |
| DragInt | function | Native.IGSharp_DragInt | ImGui.DragInt | |
| DragInt2 | function | Native.IGSharp_DragInt2 | ImGui.DragInt2 | |
| DragInt3 | function | Native.IGSharp_DragInt3 | ImGui.DragInt3 | |
| DragInt4 | function | Native.IGSharp_DragInt4 | ImGui.DragInt4 | |
| DragIntRange2 | function | Native.IGSharp_DragIntRange2 | ImGui.DragIntRange2 | |
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
| VSliderFloat | function | Native.IGSharp_VSliderFloat | ImGui.VSliderFloat | Plus generic VSlider&lt;T&gt; |
| VSliderInt | function | Native.IGSharp_VSliderInt | ImGui.VSliderInt | Plus generic VSlider&lt;T&gt; |
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
| SetColorEditOptions | function | Native.IGSharp_SetColorEditOptions | ImGui.SetColorEditOptions | |

## Widgets: Trees

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| TreeNode (str) | function | Native.IGSharp_TreeNode | ImGui.TreeNode | |
| TreeNode (str, fmt) | function | - | - | variadic |
| TreeNode (ptr, fmt) | function | - | - | variadic |
| TreeNodeEx (str) | function | Native.IGSharp_TreeNodeEx | ImGui.TreeNodeEx | |
| TreeNodeEx (str, fmt) | function | - | - | variadic |
| TreeNodeEx (ptr, fmt) | function | - | - | variadic |
| TreePush (str) | function | Native.IGSharp_TreePushStr | ImGui.TreePush (string) | |
| TreePush (ptr) | function | Native.IGSharp_TreePushPtr | ImGui.TreePush (nint) | |
| TreePop | function | Native.IGSharp_TreePop | ImGui.TreePop | |
| GetTreeNodeToLabelSpacing | function | Native.IGSharp_GetTreeNodeToLabelSpacing | ImGui.GetTreeNodeToLabelSpacing | |
| CollapsingHeader (str) | function | Native.IGSharp_CollapsingHeader | ImGui.CollapsingHeader (string, TreeNodeFlags) | |
| CollapsingHeader (str, bool*) | function | Native.IGSharp_CollapsingHeaderClosable | ImGui.CollapsingHeader (string, ref bool, TreeNodeFlags) | |
| SetNextItemOpen | function | Native.IGSharp_SetNextItemOpen | ImGui.SetNextItemOpen | |
| SetNextItemStorageID | function | Native.IGSharp_SetNextItemStorageID | ImGui.SetNextItemStorageID | |
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
| ListBox (items[]) | function | Native.IGSharp_ListBox | ImGui.ListBox (string[]) | |
| ListBox (getter) | function | Native.IGSharp_ListBoxCallback | ImGui.ListBox (Func&lt;int, string&gt;) | |

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
| Value (bool) | function | Native.IGSharp_ValueBool | ImGui.Value (string, bool) | |
| Value (int) | function | Native.IGSharp_ValueInt | ImGui.Value (string, int) | |
| Value (uint) | function | Native.IGSharp_ValueUInt | ImGui.Value (string, uint) | |
| Value (float) | function | Native.IGSharp_ValueFloat | ImGui.Value (string, float, string?) | Optional printf float format |

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
| SetItemTooltip | function | Native.IGSharp_SetItemTooltip | ImGui.SetItemTooltip | |
| SetItemTooltipV | function | - | - | variadic |

## Popups, Modals

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| BeginPopup | function | Native.IGSharp_BeginPopup | ImGui.BeginPopup | |
| BeginPopupModal | function | Native.IGSharp_BeginPopupModal | ImGui.BeginPopupModal | Overloads with/without ref bool |
| EndPopup | function | Native.IGSharp_EndPopup | ImGui.EndPopup | |
| OpenPopup (str) | function | Native.IGSharp_OpenPopup | ImGui.OpenPopup (string) | |
| OpenPopup (ID) | function | Native.IGSharp_OpenPopupID | ImGui.OpenPopup (uint) | |
| OpenPopupOnItemClick | function | Native.IGSharp_OpenPopupOnItemClick | ImGui.OpenPopupOnItemClick | |
| CloseCurrentPopup | function | Native.IGSharp_CloseCurrentPopup | ImGui.CloseCurrentPopup | |
| BeginPopupContextItem | function | Native.IGSharp_BeginPopupContextItem | ImGui.BeginPopupContextItem | |
| BeginPopupContextWindow | function | Native.IGSharp_BeginPopupContextWindow | ImGui.BeginPopupContextWindow | |
| BeginPopupContextVoid | function | Native.IGSharp_BeginPopupContextVoid | ImGui.BeginPopupContextVoid | |
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
| TableAngledHeadersRow | function | Native.IGSharp_TableAngledHeadersRow | ImGui.TableAngledHeadersRow | |
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

## Columns (legacy)

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| Columns | function | Native.IGSharp_Columns | ImGui.Columns | |
| NextColumn | function | Native.IGSharp_NextColumn | ImGui.NextColumn | |
| GetColumnIndex | function | Native.IGSharp_GetColumnIndex | ImGui.GetColumnIndex | |
| GetColumnWidth | function | Native.IGSharp_GetColumnWidth | ImGui.GetColumnWidth | |
| SetColumnWidth | function | Native.IGSharp_SetColumnWidth | ImGui.SetColumnWidth | |
| GetColumnOffset | function | Native.IGSharp_GetColumnOffset | ImGui.GetColumnOffset | |
| SetColumnOffset | function | Native.IGSharp_SetColumnOffset | ImGui.SetColumnOffset | |
| GetColumnsCount | function | Native.IGSharp_GetColumnsCount | ImGui.GetColumnsCount | |

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
| LogToTTY | function | Native.IGSharp_LogToTTY | ImGui.LogToTTY | |
| LogToFile | function | Native.IGSharp_LogToFile | ImGui.LogToFile | |
| LogToClipboard | function | Native.IGSharp_LogToClipboard | ImGui.LogToClipboard | |
| LogFinish | function | Native.IGSharp_LogFinish | ImGui.LogFinish | |
| LogButtons | function | Native.IGSharp_LogButtons | ImGui.LogButtons | |
| LogText | function | Native.IGSharp_LogText | ImGui.LogText | Single-string wrapper |
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
| PushClipRect | function | Native.IGSharp_PushClipRect | ImGui.PushClipRect | Also DrawList.PushClipRect |
| PopClipRect | function | Native.IGSharp_PopClipRect | ImGui.PopClipRect | Also DrawList.PopClipRect |

## Focus, Activation

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SetItemDefaultFocus | function | Native.IGSharp_SetItemDefaultFocus | ImGui.SetItemDefaultFocus | |
| SetKeyboardFocusHere | function | Native.IGSharp_SetKeyboardFocusHere | ImGui.SetKeyboardFocusHere | |
| SetNavCursorVisible | function | Native.IGSharp_SetNavCursorVisible | ImGui.SetNavCursorVisible | |
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
| GetItemFlags | function | Native.IGSharp_GetItemFlags | ImGui.GetItemFlags | |
| GetItemID | function | Native.IGSharp_GetItemID | ImGui.GetItemId | |
| GetItemRectMin | function | Native.IGSharp_GetItemRectMin | ImGui.GetItemRectMin | |
| GetItemRectMax | function | Native.IGSharp_GetItemRectMax | ImGui.GetItemRectMax | |
| GetItemRectSize | function | Native.IGSharp_GetItemRectSize | ImGui.GetItemRectSize | |

## Miscellaneous Utilities

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| IsRectVisible (size) | function | Native.IGSharp_IsRectVisible | ImGui.IsRectVisible (Vec2) | |
| IsRectVisible (min, max) | function | Native.IGSharp_IsRectVisibleRange | ImGui.IsRectVisible (Vec2, Vec2) | |
| GetTime | function | Native.IGSharp_GetTime | ImGui.GetTime | |
| GetFrameCount | function | Native.IGSharp_GetFrameCount | ImGui.GetFrameCount | |
| GetDrawListSharedData | function | Native.IGSharp_GetDrawListSharedData | - | niche — opaque shared-data |
| GetStyleColorName | function | Native.IGSharp_GetStyleColorName | ImGui.GetStyleColorName | |
| SetStateStorage | function | Native.IGSharp_SetStateStorage | ImGui.SetStateStorage | Takes Storage |
| GetStateStorage | function | Native.IGSharp_GetStateStorage | ImGui.GetStateStorage | Returns Storage |
| GetBackgroundDrawList | function | Native.IGSharp_GetBackgroundDrawList | ImGui.GetBackgroundDrawList | Returns DrawList |
| GetForegroundDrawList | function | Native.IGSharp_GetForegroundDrawList | ImGui.GetForegroundDrawList | Returns DrawList |
| GetWindowDrawList | function | Native.IGSharp_GetWindowDrawList | ImGui.GetWindowDrawList | Returns DrawList |

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
| GetKeyPressedAmount | function | Native.IGSharp_GetKeyPressedAmount | ImGui.GetKeyPressedAmount | |
| GetKeyName | function | Native.IGSharp_GetKeyName | ImGui.GetKeyName | |
| SetNextFrameWantCaptureKeyboard | function | Native.IGSharp_SetNextFrameWantCaptureKeyboard | ImGui.SetNextFrameWantCaptureKeyboard | |
| Shortcut | function | Native.IGSharp_Shortcut | ImGui.Shortcut | |
| SetNextItemShortcut | function | Native.IGSharp_SetNextItemShortcut | ImGui.SetNextItemShortcut | |
| SetItemKeyOwner | function | Native.IGSharp_SetItemKeyOwner | ImGui.SetItemKeyOwner | |
| IsMouseDown | function | Native.IGSharp_IsMouseDown | ImGui.IsMouseDown | |
| IsMouseClicked | function | Native.IGSharp_IsMouseClicked | ImGui.IsMouseClicked | |
| IsMouseReleased | function | Native.IGSharp_IsMouseReleased | ImGui.IsMouseReleased | |
| IsMouseDoubleClicked | function | Native.IGSharp_IsMouseDoubleClicked | ImGui.IsMouseDoubleClicked | |
| IsMouseReleasedWithDelay | function | Native.IGSharp_IsMouseReleasedWithDelay | ImGui.IsMouseReleasedWithDelay | |
| GetMouseClickedCount | function | Native.IGSharp_GetMouseClickedCount | ImGui.GetMouseClickedCount | |
| IsMouseHoveringRect | function | Native.IGSharp_IsMouseHoveringRect | ImGui.IsMouseHoveringRect | |
| IsMousePosValid | function | Native.IGSharp_IsMousePosValid | ImGui.IsMousePosValid | |
| IsAnyMouseDown | function | Native.IGSharp_IsAnyMouseDown | ImGui.IsAnyMouseDown | |
| GetMousePos | function | Native.IGSharp_GetMousePos | ImGui.GetMousePos | |
| GetMousePosOnOpeningCurrentPopup | function | Native.IGSharp_GetMousePosOnOpeningCurrentPopup | ImGui.GetMousePosOnOpeningCurrentPopup | |
| IsMouseDragging | function | Native.IGSharp_IsMouseDragging | ImGui.IsMouseDragging | |
| GetMouseDragDelta | function | Native.IGSharp_GetMouseDragDelta | ImGui.GetMouseDragDelta | |
| ResetMouseDragDelta | function | Native.IGSharp_ResetMouseDragDelta | ImGui.ResetMouseDragDelta | |
| GetMouseCursor | function | Native.IGSharp_GetMouseCursor | ImGui.GetMouseCursor | |
| SetMouseCursor | function | Native.IGSharp_SetMouseCursor | ImGui.SetMouseCursor | |
| SetNextFrameWantCaptureMouse | function | Native.IGSharp_SetNextFrameWantCaptureMouse | ImGui.SetNextFrameWantCaptureMouse | |

## Clipboard Utilities

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| GetClipboardText | function | Native.IGSharp_GetClipboardText | ImGui.GetClipboardText | |
| SetClipboardText | function | Native.IGSharp_SetClipboardText | ImGui.SetClipboardText | |

## Settings/.Ini Utilities

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| LoadIniSettingsFromDisk | function | Native.IGSharp_LoadIniSettingsFromDisk | ImGui.LoadIniSettingsFromDisk | |
| LoadIniSettingsFromMemory | function | Native.IGSharp_LoadIniSettingsFromMemory | ImGui.LoadIniSettingsFromMemory | |
| SaveIniSettingsToDisk | function | Native.IGSharp_SaveIniSettingsToDisk | ImGui.SaveIniSettingsToDisk | |
| SaveIniSettingsToMemory | function | Native.IGSharp_SaveIniSettingsToMemory | ImGui.SaveIniSettingsToMemory | |

## Debug Utilities

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| DebugTextEncoding | function | Native.IGSharp_DebugTextEncoding | ImGui.DebugTextEncoding | |
| DebugFlashStyleColor | function | Native.IGSharp_DebugFlashStyleColor | ImGui.DebugFlashStyleColor | |
| DebugStartItemPicker | function | Native.IGSharp_DebugStartItemPicker | ImGui.DebugStartItemPicker | |
| DebugCheckVersionAndDataLayout | function | Native.IGSharp_DebugCheckVersionAndDataLayout | - | internal — meaningless from C# |
| DebugLog | function | Native.IGSharp_DebugLog | ImGui.DebugLog | Single-string wrapper |
| DebugLogV | function | - | - | variadic |

## Memory Allocators

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| SetAllocatorFunctions | function | Native.IGSharp_SetAllocatorFunctions | - | niche — .NET has its own memory management |
| GetAllocatorFunctions | function | Native.IGSharp_GetAllocatorFunctions | - | niche |
| MemAlloc | function | Native.IGSharp_MemAlloc | - | niche |
| MemFree | function | Native.IGSharp_MemFree | - | niche |

## ImGuiIO Accessors

ImGuiIO is exposed as a static class `Io` with direct field access on the `IGSharp_IO` mirror struct (`Native.IGSharp_GetIO()->Field`); event-queue functions go through C wrappers.

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| io.WantCaptureMouse | field | Native.IGSharp_GetIO()->WantCaptureMouse | Io.WantCaptureMouse, ImGui.WantCaptureMouse | |
| io.WantCaptureKeyboard | field | Native.IGSharp_GetIO()->WantCaptureKeyboard | Io.WantCaptureKeyboard, ImGui.WantCaptureKeyboard | |
| io.WantTextInput | field | Native.IGSharp_GetIO()->WantTextInput | Io.WantTextInput | |
| io.WantSetMousePos | field | Native.IGSharp_GetIO()->WantSetMousePos | Io.WantSetMousePos | |
| io.WantSaveIniSettings | field | Native.IGSharp_GetIO()->WantSaveIniSettings | Io.WantSaveIniSettings | |
| io.NavActive | field | Native.IGSharp_GetIO()->NavActive | Io.NavActive | |
| io.NavVisible | field | Native.IGSharp_GetIO()->NavVisible | Io.NavVisible | |
| io.Framerate | field | Native.IGSharp_GetIO()->Framerate | Io.Framerate, ImGui.Framerate | |
| io.ConfigFlags | field | Native.IGSharp_GetIO()->ConfigFlags | Io.ConfigFlags | |
| io.BackendFlags | field | Native.IGSharp_GetIO()->BackendFlags | Io.BackendFlags | |
| io.IniFilename | field | Native.IGSharp_GetIO()->IniFilename | Io.SetIniFilename, ImGui.SetIniFilename | |
| io.DisplaySize | field | Native.IGSharp_GetIO()->DisplaySize | Io.DisplaySize | |
| io.DisplayFramebufferScale | field | Native.IGSharp_GetIO()->DisplayFramebufferScale | Io.DisplayFramebufferScale | |
| io.DeltaTime | field | Native.IGSharp_GetIO()->DeltaTime | Io.DeltaTime | |
| io.MousePos | field | Native.IGSharp_GetIO()->MousePos | Io.MousePos | |
| io.MouseDelta | field | Native.IGSharp_GetIO()->MouseDelta | Io.MouseDelta | |
| io.MouseWheel | field | Native.IGSharp_GetIO()->MouseWheel | Io.MouseWheel | |
| io.MouseWheelH | field | Native.IGSharp_GetIO()->MouseWheelH | Io.MouseWheelHorizontal | |
| io.KeyCtrl | field | Native.IGSharp_GetIO()->KeyCtrl | Io.KeyCtrl | |
| io.KeyShift | field | Native.IGSharp_GetIO()->KeyShift | Io.KeyShift | |
| io.KeyAlt | field | Native.IGSharp_GetIO()->KeyAlt | Io.KeyAlt | |
| io.KeySuper | field | Native.IGSharp_GetIO()->KeySuper | Io.KeySuper | |
| io.MouseDoubleClickTime | field | Native.IGSharp_GetIO()->MouseDoubleClickTime | Io.MouseDoubleClickTime | |
| io.MouseDoubleClickMaxDist | field | Native.IGSharp_GetIO()->MouseDoubleClickMaxDist | Io.MouseDoubleClickMaxDist | |
| io.MouseDragThreshold | field | Native.IGSharp_GetIO()->MouseDragThreshold | Io.MouseDragThreshold | |
| io.KeyRepeatDelay | field | Native.IGSharp_GetIO()->KeyRepeatDelay | Io.KeyRepeatDelay | |
| io.KeyRepeatRate | field | Native.IGSharp_GetIO()->KeyRepeatRate | Io.KeyRepeatRate | |
| io.MetricsRenderVertices | field | Native.IGSharp_GetIO()->MetricsRenderVertices | Io.MetricsRenderVertices | |
| io.MetricsRenderIndices | field | Native.IGSharp_GetIO()->MetricsRenderIndices | Io.MetricsRenderIndices | |
| io.MetricsRenderWindows | field | Native.IGSharp_GetIO()->MetricsRenderWindows | Io.MetricsRenderWindows | |
| io.MetricsActiveWindows | field | Native.IGSharp_GetIO()->MetricsActiveWindows | Io.MetricsActiveWindows | |
| io.Fonts | field | Native.IGSharp_GetIO()->Fonts | ImGui.GetFontAtlas | Returns FontAtlas |
| io.FontDefault | field | Native.IGSharp_GetIO()->FontDefault | ImGui.GetDefaultFont, ImGui.SetDefaultFont | |
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
| io.SetKeyEventNativeData | function | Native.IGSharp_IO_SetKeyEventNativeData | - | niche — legacy backend helper |
| io.SetAppAcceptingEvents | function | Native.IGSharp_IO_SetAppAcceptingEvents | Io.SetAppAcceptingEvents | |
| io.ClearEventsQueue | function | Native.IGSharp_IO_ClearEventsQueue | Io.ClearEventsQueue | |
| io.ClearInputKeys | function | Native.IGSharp_IO_ClearInputKeys | Io.ClearInputKeys | |
| io.ClearInputMouse | function | Native.IGSharp_IO_ClearInputMouse | Io.ClearInputMouse | |

## ImGuiPlatformIO Accessors

ImGuiPlatformIO is exposed as a static class `PlatformIO`. Handler overrides marshal managed delegates through C function pointers; registration is global (per-process).

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| Platform_GetClipboardTextFn | field | Native.IGSharp_PlatformIO_SetPlatformGetClipboardTextFn | PlatformIO.SetClipboardHandlers | |
| Platform_SetClipboardTextFn | field | Native.IGSharp_PlatformIO_SetPlatformSetClipboardTextFn | PlatformIO.SetClipboardHandlers | |
| Platform_ClipboardUserData | field | Native.IGSharp_PlatformIO_GetPlatformClipboardUserData, _Set | - | internal — bridge design/backend channel |
| Platform_OpenInShellFn | field | Native.IGSharp_PlatformIO_SetPlatformOpenInShellFn | PlatformIO.SetOpenInShellHandler | |
| Platform_OpenInShellUserData | field | Native.IGSharp_PlatformIO_GetPlatformOpenInShellUserData, _Set | - | internal — bridge design/backend channel |
| Platform_SetImeDataFn | field | Native.IGSharp_PlatformIO_SetPlatformSetImeDataFn | PlatformIO.SetImeDataHandler | ImeDataHandler + PlatformImeData |
| Platform_ImeUserData | field | Native.IGSharp_PlatformIO_GetPlatformImeUserData, _Set | - | internal — bridge design/backend channel |
| Platform_LocaleDecimalPoint | field | Native.IGSharp_PlatformIO_GetPlatformLocaleDecimalPoint, _Set | PlatformIO.LocaleDecimalPoint | |
| Renderer_TextureMaxWidth | field | Native.IGSharp_PlatformIO_GetRendererTextureMaxWidth, _Set | PlatformIO.RendererTextureMaxWidth | |
| Renderer_TextureMaxHeight | field | Native.IGSharp_PlatformIO_GetRendererTextureMaxHeight, _Set | PlatformIO.RendererTextureMaxHeight | |
| Renderer_RenderState | field | Native.IGSharp_PlatformIO_GetRendererRenderState, _Set | - | internal — bridge design/backend channel |
| Textures.Size | field | Native.IGSharp_PlatformIO_GetTexturesCount | PlatformIO.TexturesCount | |
| Textures[idx] | field | Native.IGSharp_PlatformIO_GetTexture | PlatformIO.GetTexture | Returns TextureData |
| (clear platform handlers) | - | Native.IGSharp_PlatformIO_ClearPlatformHandlers | PlatformIO.ClearPlatformHandlers | Custom wrapper |
| (clear renderer handlers) | - | Native.IGSharp_PlatformIO_ClearRendererHandlers | PlatformIO.ClearRendererHandlers | Custom wrapper |

## ImGuiStyle Accessors

ImGuiStyle is exposed as a static class `Style` with direct field access on the `IGSharp_Style` mirror struct (`Native.IGSharp_GetStyle()->Field`). Most properties have both get and set.

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| style.FontSizeBase | field | Native.IGSharp_GetStyle()->FontSizeBase | Style.FontSizeBase | |
| style.FontScaleMain | field | Native.IGSharp_GetStyle()->FontScaleMain | Style.FontScaleMain | |
| style.FontScaleDpi | field | Native.IGSharp_GetStyle()->FontScaleDpi | Style.FontScaleDpi | Read-only; set via ImGui.SetFontScaleDpi |
| style.Alpha | field | Native.IGSharp_GetStyle()->Alpha | Style.Alpha | |
| style.DisabledAlpha | field | Native.IGSharp_GetStyle()->DisabledAlpha | Style.DisabledAlpha | |
| style.WindowRounding | field | Native.IGSharp_GetStyle()->WindowRounding | Style.WindowRounding | |
| style.WindowBorderSize | field | Native.IGSharp_GetStyle()->WindowBorderSize | Style.WindowBorderSize | |
| style.WindowPadding | field | Native.IGSharp_GetStyle()->WindowPadding | Style.WindowPadding | |
| style.WindowMinSize | field | Native.IGSharp_GetStyle()->WindowMinSize | Style.WindowMinSize | |
| style.WindowTitleAlign | field | Native.IGSharp_GetStyle()->WindowTitleAlign | Style.WindowTitleAlign | |
| style.ChildRounding | field | Native.IGSharp_GetStyle()->ChildRounding | Style.ChildRounding | |
| style.ChildBorderSize | field | Native.IGSharp_GetStyle()->ChildBorderSize | Style.ChildBorderSize | |
| style.PopupRounding | field | Native.IGSharp_GetStyle()->PopupRounding | Style.PopupRounding | |
| style.PopupBorderSize | field | Native.IGSharp_GetStyle()->PopupBorderSize | Style.PopupBorderSize | |
| style.FrameRounding | field | Native.IGSharp_GetStyle()->FrameRounding | Style.FrameRounding | |
| style.FrameBorderSize | field | Native.IGSharp_GetStyle()->FrameBorderSize | Style.FrameBorderSize | |
| style.FramePadding | field | Native.IGSharp_GetStyle()->FramePadding | Style.FramePadding | |
| style.ItemSpacing | field | Native.IGSharp_GetStyle()->ItemSpacing | Style.ItemSpacing | |
| style.ItemInnerSpacing | field | Native.IGSharp_GetStyle()->ItemInnerSpacing | Style.ItemInnerSpacing | |
| style.CellPadding | field | Native.IGSharp_GetStyle()->CellPadding | Style.CellPadding | |
| style.TouchExtraPadding | field | Native.IGSharp_GetStyle()->TouchExtraPadding | Style.TouchExtraPadding | |
| style.IndentSpacing | field | Native.IGSharp_GetStyle()->IndentSpacing | Style.IndentSpacing | |
| style.ColumnsMinSpacing | field | Native.IGSharp_GetStyle()->ColumnsMinSpacing | Style.ColumnsMinSpacing | |
| style.ScrollbarSize | field | Native.IGSharp_GetStyle()->ScrollbarSize | Style.ScrollbarSize | |
| style.ScrollbarRounding | field | Native.IGSharp_GetStyle()->ScrollbarRounding | Style.ScrollbarRounding | |
| style.GrabMinSize | field | Native.IGSharp_GetStyle()->GrabMinSize | Style.GrabMinSize | |
| style.GrabRounding | field | Native.IGSharp_GetStyle()->GrabRounding | Style.GrabRounding | |
| style.ImageRounding | field | Native.IGSharp_GetStyle()->ImageRounding | Style.ImageRounding | |
| style.ImageBorderSize | field | Native.IGSharp_GetStyle()->ImageBorderSize | Style.ImageBorderSize | |
| style.TabRounding | field | Native.IGSharp_GetStyle()->TabRounding | Style.TabRounding | |
| style.TabBorderSize | field | Native.IGSharp_GetStyle()->TabBorderSize | Style.TabBorderSize | |
| style.SeparatorSize | field | Native.IGSharp_GetStyle()->SeparatorSize | Style.SeparatorSize | |
| style.ButtonTextAlign | field | Native.IGSharp_GetStyle()->ButtonTextAlign | Style.ButtonTextAlign | |
| style.SelectableTextAlign | field | Native.IGSharp_GetStyle()->SelectableTextAlign | Style.SelectableTextAlign | |
| style.SeparatorTextAlign | field | Native.IGSharp_GetStyle()->SeparatorTextAlign | Style.SeparatorTextAlign | |
| style.SeparatorTextPadding | field | Native.IGSharp_GetStyle()->SeparatorTextPadding | Style.SeparatorTextPadding | |
| style.DisplayWindowPadding | field | Native.IGSharp_GetStyle()->DisplayWindowPadding | Style.DisplayWindowPadding | |
| style.DisplaySafeAreaPadding | field | Native.IGSharp_GetStyle()->DisplaySafeAreaPadding | Style.DisplaySafeAreaPadding | |
| style.MouseCursorScale | field | Native.IGSharp_GetStyle()->MouseCursorScale | Style.MouseCursorScale | |
| style.AntiAliasedLines | field | Native.IGSharp_GetStyle()->AntiAliasedLines | Style.AntiAliasedLines | |
| style.AntiAliasedFill | field | Native.IGSharp_GetStyle()->AntiAliasedFill | Style.AntiAliasedFill | |
| style.CurveTessellationTol | field | Native.IGSharp_GetStyle()->CurveTessellationTol | Style.CurveTessellationTol | |
| style.CircleTessellationMaxError | field | Native.IGSharp_GetStyle()->CircleTessellationMaxError | Style.CircleTessellationMaxError | |
| style.Colors[idx] | field | Native.IGSharp_GetStyle()->Colors[idx] | Style.GetColor, Style.SetColor | |

## ImDrawList API

ImDrawList is exposed as a `readonly struct DrawList` wrapping the native pointer. `CloneOutput` returns an owned `ClonedDrawList` (disposable).

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| Flags | field | Native.IGSharp_DrawList_GetFlags, _SetFlags | DrawList.Flags | DrawListFlags |
| PushClipRect | function | Native.IGSharp_DrawList_PushClipRect | DrawList.PushClipRect | |
| PushClipRectFullScreen | function | Native.IGSharp_DrawList_PushClipRectFullScreen | DrawList.PushClipRectFullScreen | |
| PopClipRect | function | Native.IGSharp_DrawList_PopClipRect | DrawList.PopClipRect | |
| PushTexture | function | Native.IGSharp_DrawList_PushTexture | DrawList.PushTexture | Overloads: ulong textureId, GpuTexture |
| PushTexture (ImTextureData) | function | Native.IGSharp_DrawList_PushTextureData | - | deferred — TextureData image overloads |
| PopTexture | function | Native.IGSharp_DrawList_PopTexture | DrawList.PopTexture | |
| GetClipRectMin | function | Native.IGSharp_DrawList_GetClipRectMin | DrawList.ClipRectMin | |
| GetClipRectMax | function | Native.IGSharp_DrawList_GetClipRectMax | DrawList.ClipRectMax | |
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
| AddText (font, size) | function | Native.IGSharp_DrawList_AddTextFont | DrawList.AddText (Font overloads) | With/without fine clip rect |
| AddBezierCubic | function | Native.IGSharp_DrawList_AddBezierCubic | DrawList.AddBezierCubic | |
| AddBezierQuadratic | function | Native.IGSharp_DrawList_AddBezierQuadratic | DrawList.AddBezierQuadratic | |
| AddPolyline | function | Native.IGSharp_DrawList_AddPolyline | DrawList.AddPolyline | |
| AddConvexPolyFilled | function | Native.IGSharp_DrawList_AddConvexPolyFilled | DrawList.AddConvexPolyFilled | |
| AddConcavePolyFilled | function | Native.IGSharp_DrawList_AddConcavePolyFilled | DrawList.AddConcavePolyFilled | |
| AddImage | function | Native.IGSharp_DrawList_AddImage | DrawList.AddImage | Overloads: ulong textureId, GpuTexture |
| AddImageQuad | function | Native.IGSharp_DrawList_AddImageQuad | DrawList.AddImageQuad | Overloads: ulong textureId, GpuTexture |
| AddImageRounded | function | Native.IGSharp_DrawList_AddImageRounded | DrawList.AddImageRounded | Overloads: ulong textureId, GpuTexture |
| AddImage (ImTextureData) | function | Native.IGSharp_DrawList_AddImageTextureData | - | deferred — TextureData image overloads |
| AddImageQuad (ImTextureData) | function | Native.IGSharp_DrawList_AddImageQuadTextureData | - | deferred — TextureData image overloads |
| AddImageRounded (ImTextureData) | function | Native.IGSharp_DrawList_AddImageRoundedTextureData | - | deferred — TextureData image overloads |
| PathClear | function | Native.IGSharp_DrawList_PathClear | DrawList.PathClear | |
| PathLineTo | function | Native.IGSharp_DrawList_PathLineTo | DrawList.PathLineTo | |
| PathLineToMergeDuplicate | function | Native.IGSharp_DrawList_PathLineToMergeDuplicate | DrawList.PathLineToMergeDuplicate | |
| PathFillConvex | function | Native.IGSharp_DrawList_PathFillConvex | DrawList.PathFillConvex | |
| PathFillConcave | function | Native.IGSharp_DrawList_PathFillConcave | DrawList.PathFillConcave | |
| PathStroke | function | Native.IGSharp_DrawList_PathStroke | DrawList.PathStroke | |
| PathArcTo | function | Native.IGSharp_DrawList_PathArcTo | DrawList.PathArcTo | |
| PathArcToFast | function | Native.IGSharp_DrawList_PathArcToFast | DrawList.PathArcToFast | |
| PathEllipticalArcTo | function | Native.IGSharp_DrawList_PathEllipticalArcTo | DrawList.PathEllipticalArcTo | |
| PathBezierCubicCurveTo | function | Native.IGSharp_DrawList_PathBezierCubicCurveTo | DrawList.PathBezierCubicCurveTo | |
| PathBezierQuadraticCurveTo | function | Native.IGSharp_DrawList_PathBezierQuadraticCurveTo | DrawList.PathBezierQuadraticCurveTo | |
| PathRect | function | Native.IGSharp_DrawList_PathRect | DrawList.PathRect | |
| AddCallback | function | Native.IGSharp_DrawList_AddCallback | DrawList.AddCallback | Action&lt;DrawList, DrawCmd&gt; |
| AddDrawCmd | function | Native.IGSharp_DrawList_AddDrawCmd | DrawList.AddDrawCmd | |
| CloneOutput | function | Native.IGSharp_DrawList_CloneOutput | DrawList.CloneOutput | Returns owned ClonedDrawList |
| ChannelsSplit | function | Native.IGSharp_DrawList_ChannelsSplit | DrawList.ChannelsSplit | |
| ChannelsMerge | function | Native.IGSharp_DrawList_ChannelsMerge | DrawList.ChannelsMerge | |
| ChannelsSetCurrent | function | Native.IGSharp_DrawList_ChannelsSetCurrent | DrawList.ChannelsSetCurrent | |
| PrimReserve | function | Native.IGSharp_DrawList_PrimReserve | - | niche — raw mesh emission |
| PrimUnreserve | function | Native.IGSharp_DrawList_PrimUnreserve | - | niche — raw mesh emission |
| PrimRect | function | Native.IGSharp_DrawList_PrimRect | - | niche — raw mesh emission |
| PrimRectUV | function | Native.IGSharp_DrawList_PrimRectUV | - | niche — raw mesh emission |
| PrimQuadUV | function | Native.IGSharp_DrawList_PrimQuadUV | - | niche — raw mesh emission |
| PrimVtx | function | Native.IGSharp_DrawList_PrimVtx | - | niche — raw mesh emission |
| PrimWriteVtx | function | Native.IGSharp_DrawList_PrimWriteVtx | - | niche — raw mesh emission |
| PrimWriteIdx | function | Native.IGSharp_DrawList_PrimWriteIdx | - | niche — raw mesh emission |
| CmdBuffer.Size | field | Native.IGSharp_DrawList_GetCmdBufferSize | DrawList.CmdBufferSize | |
| CmdBuffer[idx] | field | Native.IGSharp_DrawList_GetCmd | DrawList.GetCmd | Returns DrawCmd |
| CmdBuffer.Data | field | Native.IGSharp_DrawList_GetCmdBufferData | - | internal — renderer-backend concern |
| IdxBuffer.Size | field | Native.IGSharp_DrawList_GetIdxBufferSize | DrawList.IdxBufferSize | |
| IdxBuffer.Data | field | Native.IGSharp_DrawList_GetIdxBufferData | - | internal — renderer-backend concern |
| VtxBuffer.Size | field | Native.IGSharp_DrawList_GetVtxBufferSize | DrawList.VtxBufferSize | |
| VtxBuffer.Data | field | Native.IGSharp_DrawList_GetVtxBufferData | - | internal — renderer-backend concern |
| (constructor) | function | Native.IGSharp_DrawList_Create | StandaloneDrawList.Create | Owned initialized list |
| ResetForNewFrame | function | Native.IGSharp_DrawList_ResetForNewFrame | StandaloneDrawList.ResetForNewFrame | Reuse after starting a frame |
| (destructor) | function | Native.IGSharp_DrawList_Destroy | ClonedDrawList.Dispose | Used to free CloneOutput result |

## ImDrawData API

ImDrawData is exposed as a `readonly struct DrawData` wrapping the native pointer (returned by `ImGui.GetDrawData`).

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| Valid | field | Native.IGSharp_DrawData_GetValid | DrawData.Valid | |
| CmdListsCount | field | Native.IGSharp_DrawData_GetCmdListsCount | DrawData.CmdListsCount | |
| CmdLists[idx] | field | Native.IGSharp_DrawData_GetCmdList | DrawData.GetCmdList | Returns DrawList |
| TotalIdxCount | field | Native.IGSharp_DrawData_GetTotalIdxCount | DrawData.TotalIdxCount | |
| TotalVtxCount | field | Native.IGSharp_DrawData_GetTotalVtxCount | DrawData.TotalVtxCount | |
| DisplayPos | field | Native.IGSharp_DrawData_GetDisplayPos | DrawData.DisplayPos | |
| DisplaySize | field | Native.IGSharp_DrawData_GetDisplaySize | DrawData.DisplaySize | |
| FramebufferScale | field | Native.IGSharp_DrawData_GetFramebufferScale | DrawData.FramebufferScale | |
| OwnerViewport | field | Native.IGSharp_DrawData_GetOwnerViewport | DrawData.OwnerViewport | Returns Viewport |
| Textures.Size | field | Native.IGSharp_DrawData_GetTexturesCount | DrawData.TexturesCount | |
| Textures[idx] | field | Native.IGSharp_DrawData_GetTextures, _GetTexturesData, _SetTextures | - | internal — renderer-backend concern |
| Clear | function | Native.IGSharp_DrawData_Clear | - | internal — engine-owned draw data |
| AddDrawList | function | Native.IGSharp_DrawData_AddDrawList | - | internal — engine-owned draw data |
| DeIndexAllBuffers | function | Native.IGSharp_DrawData_DeIndexAllBuffers | DrawData.DeIndexAllBuffers | |
| ScaleClipRects | function | Native.IGSharp_DrawData_ScaleClipRects | DrawData.ScaleClipRects | |

## ImDrawCmd API

ImDrawCmd is exposed as a `readonly struct DrawCmd` wrapping the native pointer.

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| ClipRect | field | Native.IGSharp_DrawCmd_GetClipRect | DrawCmd.ClipRect | |
| GetTexID | function | Native.IGSharp_DrawCmd_GetTexID | DrawCmd.TextureId | |
| VtxOffset | field | Native.IGSharp_DrawCmd_GetVtxOffset | DrawCmd.VtxOffset | |
| IdxOffset | field | Native.IGSharp_DrawCmd_GetIdxOffset | DrawCmd.IdxOffset | |
| ElemCount | field | Native.IGSharp_DrawCmd_GetElemCount | DrawCmd.ElemCount | |
| UserCallback | field | Native.IGSharp_DrawCmd_GetUserCallback | DrawCmd.HasUserCallback | Exposed as boolean |
| UserCallbackData | field | Native.IGSharp_DrawCmd_GetUserCallbackData | DrawCmd.HasUserCallbackData | Exposed as boolean |
| UserCallbackDataSize | field | Native.IGSharp_DrawCmd_GetUserCallbackDataSize | DrawCmd.UserCallbackDataSize | |

## ImDrawListSplitter API

ImDrawListSplitter is exposed as a `sealed class DrawListSplitter : IDisposable`.

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| (constructor) | function | Native.IGSharp_DrawListSplitter_Create | DrawListSplitter.ctor | |
| (destructor) | function | Native.IGSharp_DrawListSplitter_Destroy | DrawListSplitter.Dispose | |
| Clear | function | Native.IGSharp_DrawListSplitter_Clear | DrawListSplitter.Clear | |
| ClearFreeMemory | function | Native.IGSharp_DrawListSplitter_ClearFreeMemory | DrawListSplitter.ClearFreeMemory | |
| Split | function | Native.IGSharp_DrawListSplitter_Split | DrawListSplitter.Split | |
| SetCurrentChannel | function | Native.IGSharp_DrawListSplitter_SetCurrentChannel | DrawListSplitter.SetCurrentChannel | |
| Merge | function | Native.IGSharp_DrawListSplitter_Merge | DrawListSplitter.Merge | |

## ImFontAtlas API

ImFontAtlas is exposed as a `readonly struct FontAtlas` wrapping the native pointer.

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| (constructor) | function | Native.IGSharp_FontAtlas_Create | - | deferred — standalone/shared atlas needs Context integration |
| (destructor) | function | Native.IGSharp_FontAtlas_Destroy | - | deferred — standalone/shared atlas needs Context integration |
| AddFont | function | Native.IGSharp_FontAtlas_AddFont | - | internal |
| AddFontDefault | function | Native.IGSharp_FontAtlas_AddFontDefault | FontAtlas.AddDefaultFont | |
| AddFontDefaultBitmap | function | Native.IGSharp_FontAtlas_AddFontDefaultBitmap | FontAtlas.AddDefaultBitmapFont | |
| AddFontDefaultVector | function | Native.IGSharp_FontAtlas_AddFontDefaultVector | FontAtlas.AddDefaultVectorFont | |
| AddFontFromFileTTF | function | Native.IGSharp_FontAtlas_AddFontFromFileTTF | FontAtlas.AddFontFromFileTTF | |
| AddFontFromMemoryTTF | function | Native.IGSharp_FontAtlas_AddFontFromMemoryTTF | FontAtlas.AddFontFromMemoryTTF | |
| AddFontFromMemoryCompressedTTF | function | Native.IGSharp_FontAtlas_AddFontFromMemoryCompressedTTF | FontAtlas.AddFontFromMemoryCompressedTTF | |
| AddFontFromMemoryCompressedBase85TTF | function | Native.IGSharp_FontAtlas_AddFontFromMemoryCompressedBase85TTF | FontAtlas.AddFontFromMemoryCompressedBase85TTF | |
| RemoveFont | function | Native.IGSharp_FontAtlas_RemoveFont | FontAtlas.RemoveFont | |
| Clear | function | Native.IGSharp_FontAtlas_Clear | FontAtlas.Clear | |
| CompactCache | function | Native.IGSharp_FontAtlas_CompactCache | FontAtlas.CompactCache | |
| ClearInputData | function | Native.IGSharp_FontAtlas_ClearInputData | FontAtlas.ClearInputData | |
| ClearFonts | function | Native.IGSharp_FontAtlas_ClearFonts | FontAtlas.ClearFonts | |
| ClearTexData | function | Native.IGSharp_FontAtlas_ClearTexData | FontAtlas.ClearTexData | |
| Build | function | Native.IGSharp_FontAtlas_Build | FontAtlas.Build | |
| GetGlyphRangesDefault | function | Native.IGSharp_FontAtlas_GetGlyphRangesDefault | FontAtlas.GetGlyphRangesDefault | |
| AddCustomRect | function | Native.IGSharp_FontAtlas_AddCustomRect | FontAtlas.AddCustomRect | Overload with out FontAtlasRect |
| RemoveCustomRect | function | Native.IGSharp_FontAtlas_RemoveCustomRect | FontAtlas.RemoveCustomRect | |
| GetCustomRect | function | Native.IGSharp_FontAtlas_GetCustomRect | FontAtlas.TryGetCustomRect | Returns FontAtlasRect |
| Flags | field | Native.IGSharp_FontAtlas_GetFlags, _SetFlags | FontAtlas.Flags | FontAtlasFlags |
| TexDesiredFormat | field | Native.IGSharp_FontAtlas_GetTexDesiredFormat, _Set | FontAtlas.TexDesiredFormat | TextureFormat |
| TexGlyphPadding | field | Native.IGSharp_FontAtlas_GetTexGlyphPadding, _Set | FontAtlas.TexGlyphPadding | |
| TexMinWidth/TexMinHeight | field | Native.IGSharp_FontAtlas_GetTexMinWidth, _GetTexMinHeight, _Set | FontAtlas.TexMinWidth, FontAtlas.TexMinHeight | |
| TexMaxWidth/TexMaxHeight | field | Native.IGSharp_FontAtlas_GetTexMaxWidth, _GetTexMaxHeight, _Set | FontAtlas.TexMaxWidth, FontAtlas.TexMaxHeight | |
| TexData | field | Native.IGSharp_FontAtlas_GetTexData | FontAtlas.GetTexData | Returns TextureData |
| TexUvScale | field | Native.IGSharp_FontAtlas_GetTexUvScale | FontAtlas.TexUvScale | |
| TexUvWhitePixel | field | Native.IGSharp_FontAtlas_GetTexUvWhitePixel | FontAtlas.TexUvWhitePixel | |
| TexPixelsUseColors | field | Native.IGSharp_FontAtlas_GetTexPixelsUseColors, _Set | FontAtlas.TexPixelsUseColors | |
| GetTexID (TexRef) | function | Native.IGSharp_FontAtlas_GetTexID | FontAtlas.TextureId | |
| TexIsBuilt | field | Native.IGSharp_FontAtlas_GetTexIsBuilt | FontAtlas.TexIsBuilt | |
| Locked | field | Native.IGSharp_FontAtlas_GetLocked | FontAtlas.IsLocked | |
| RendererHasTextures | field | Native.IGSharp_FontAtlas_GetRendererHasTextures | FontAtlas.RendererHasTextures | |
| FontLoaderName | field | Native.IGSharp_FontAtlas_GetFontLoaderName | FontAtlas.FontLoaderName | |
| FontLoaderFlags | field | Native.IGSharp_FontAtlas_GetFontLoaderFlags, _Set | FontAtlas.FontLoaderFlags | |
| SetFontLoader | function | Native.IGSharp_FontAtlas_SetFontLoader | - | internal |
| UserData | field | Native.IGSharp_FontAtlas_GetUserData, _Set | - | internal |
| Fonts.Size | field | Native.IGSharp_FontAtlas_GetFontCount | FontAtlas.FontCount | |
| Fonts[idx] | field | Native.IGSharp_FontAtlas_GetFont | FontAtlas.GetFont | |

## ImFont API

ImFont is exposed as a `readonly struct Font` wrapping the native pointer.

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| OwnerAtlas | field | Native.IGSharp_Font_GetOwnerAtlas | Font.OwnerAtlas | Returns FontAtlas |
| Flags | field | Native.IGSharp_Font_GetFlags, _SetFlags | Font.Flags | FontFlags |
| LegacySize | field | Native.IGSharp_Font_GetLegacySize, _Set | Font.LegacySize | |
| FallbackChar | field | Native.IGSharp_Font_GetFallbackChar, _Set | Font.FallbackChar | |
| EllipsisChar | field | Native.IGSharp_Font_GetEllipsisChar, _Set | Font.EllipsisChar | |
| EllipsisAutoBake | field | Native.IGSharp_Font_GetEllipsisAutoBake, _Set | Font.EllipsisAutoBake | |
| IsLoaded | function | Native.IGSharp_Font_IsLoaded | Font.IsLoaded | |
| GetDebugName | function | Native.IGSharp_Font_GetDebugName | Font.DebugName | |
| GetFontBaked | function | Native.IGSharp_Font_GetFontBaked | Font.GetFontBaked | Returns FontBaked |
| CalcTextSizeA | function | Native.IGSharp_Font_CalcTextSizeA | Font.CalcTextSize | |
| CalcWordWrapPosition | function | Native.IGSharp_Font_CalcWordWrapPosition | Font.CalcWordWrapPosition | |
| RenderChar | function | Native.IGSharp_Font_RenderChar | - | internal |
| RenderText | function | Native.IGSharp_Font_RenderText | - | internal |
| ClearOutputData | function | Native.IGSharp_Font_ClearOutputData | - | internal |
| AddRemapChar | function | Native.IGSharp_Font_AddRemapChar | Font.AddRemapChar | |
| IsGlyphInFont | function | Native.IGSharp_Font_IsGlyphInFont | Font.IsGlyphInFont | |
| IsGlyphRangeUnused | function | Native.IGSharp_Font_IsGlyphRangeUnused | Font.IsGlyphRangeUnused | |

## ImFontBaked API

ImFontBaked is exposed as a `readonly struct FontBaked` wrapping the native pointer.

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| Size | field | Native.IGSharp_FontBaked_GetSize | FontBaked.Size | |
| RasterizerDensity | field | Native.IGSharp_FontBaked_GetRasterizerDensity | FontBaked.RasterizerDensity | |
| Ascent | field | Native.IGSharp_FontBaked_GetAscent | FontBaked.Ascent | |
| Descent | field | Native.IGSharp_FontBaked_GetDescent | FontBaked.Descent | |
| FallbackAdvanceX | field | Native.IGSharp_FontBaked_GetFallbackAdvanceX | FontBaked.FallbackAdvanceX | |
| Glyphs.Size | field | Native.IGSharp_FontBaked_GetGlyphsCount | FontBaked.GlyphCount | |
| Glyphs[idx] | field | Native.IGSharp_FontBaked_GetGlyph | FontBaked.GetGlyph | Returns FontGlyph |
| ClearOutputData | function | Native.IGSharp_FontBaked_ClearOutputData | - | internal |
| FindGlyph | function | Native.IGSharp_FontBaked_FindGlyph | FontBaked.FindGlyph | |
| FindGlyphNoFallback | function | Native.IGSharp_FontBaked_FindGlyphNoFallback | FontBaked.FindGlyphNoFallback | |
| GetCharAdvance | function | Native.IGSharp_FontBaked_GetCharAdvance | FontBaked.GetCharAdvance | |
| IsGlyphLoaded | function | Native.IGSharp_FontBaked_IsGlyphLoaded | FontBaked.IsGlyphLoaded | |

## ImFontGlyph API

ImFontGlyph is exposed as a `readonly struct FontGlyph` wrapping the native pointer.

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| Colored | field | Native.IGSharp_FontGlyph_GetColored | FontGlyph.Colored | |
| Visible | field | Native.IGSharp_FontGlyph_GetVisible | FontGlyph.Visible | |
| SourceIdx | field | Native.IGSharp_FontGlyph_GetSourceIdx | FontGlyph.SourceIndex | |
| Codepoint | field | Native.IGSharp_FontGlyph_GetCodepoint | FontGlyph.Codepoint | |
| AdvanceX | field | Native.IGSharp_FontGlyph_GetAdvanceX | FontGlyph.AdvanceX | |
| X0, Y0 | field | Native.IGSharp_FontGlyph_GetX0, _GetY0 | FontGlyph.Min | Combined into Vec2 |
| X1, Y1 | field | Native.IGSharp_FontGlyph_GetX1, _GetY1 | FontGlyph.Max | Combined into Vec2 |
| U0, V0 | field | Native.IGSharp_FontGlyph_GetU0, _GetV0 | FontGlyph.Uv0 | Combined into Vec2 |
| U1, V1 | field | Native.IGSharp_FontGlyph_GetU1, _GetV1 | FontGlyph.Uv1 | Combined into Vec2 |
| PackId | field | Native.IGSharp_FontGlyph_GetPackId | FontGlyph.PackId | |

## ImFontGlyphRangesBuilder API

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| (constructor/destructor) | function | Native.IGSharp_FontGlyphRangesBuilder_New, _Delete | - | deferred — future GlyphRangesBuilder |
| Clear | function | Native.IGSharp_FontGlyphRangesBuilder_Clear | - | deferred — future GlyphRangesBuilder |
| GetBit / SetBit | function | Native.IGSharp_FontGlyphRangesBuilder_GetBit, _SetBit | - | deferred — future GlyphRangesBuilder |
| AddChar | function | Native.IGSharp_FontGlyphRangesBuilder_AddChar | - | deferred — future GlyphRangesBuilder |
| AddText | function | Native.IGSharp_FontGlyphRangesBuilder_AddText | - | deferred — future GlyphRangesBuilder |
| AddRanges | function | Native.IGSharp_FontGlyphRangesBuilder_AddRanges | - | deferred — future GlyphRangesBuilder |
| BuildRanges | function | Native.IGSharp_FontGlyphRangesBuilder_BuildRanges | - | deferred — future GlyphRangesBuilder |

## ImFontConfig API

ImFontConfig is exposed as a `sealed class FontConfig : IDisposable` owning a native ImFontConfig.

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| (constructor) | function | Native.IGSharp_FontConfig_Create | FontConfig.ctor | |
| (destructor) | function | Native.IGSharp_FontConfig_Destroy | FontConfig.Dispose | |
| FontData, FontDataSize | field | Native.IGSharp_FontConfig_GetFontData, _GetFontDataSize, _Set | - | internal |
| FontDataOwnedByAtlas | field | Native.IGSharp_FontConfig_GetFontDataOwnedByAtlas, _Set | FontConfig.FontDataOwnedByAtlas | |
| MergeMode | field | Native.IGSharp_FontConfig_GetMergeMode, _Set | FontConfig.MergeMode | |
| PixelSnapH | field | Native.IGSharp_FontConfig_GetPixelSnapH, _Set | FontConfig.PixelSnapH | |
| FontNo | field | Native.IGSharp_FontConfig_GetFontNo, _Set | FontConfig.FontNo | |
| OversampleH | field | Native.IGSharp_FontConfig_GetOversampleH, _Set | FontConfig.OversampleH | |
| OversampleV | field | Native.IGSharp_FontConfig_GetOversampleV, _Set | FontConfig.OversampleV | |
| SizePixels | field | Native.IGSharp_FontConfig_GetSizePixels, _Set | FontConfig.SizePixels | |
| GlyphRanges | field | Native.IGSharp_FontConfig_GetGlyphRanges, _Set | FontConfig.GlyphRanges | |
| GlyphExcludeRanges | field | Native.IGSharp_FontConfig_GetGlyphExcludeRanges, _Set | FontConfig.GlyphExcludeRanges | |
| GlyphOffset | field | Native.IGSharp_FontConfig_GetGlyphOffset, _Set | FontConfig.GlyphOffset | |
| GlyphMinAdvanceX | field | Native.IGSharp_FontConfig_GetGlyphMinAdvanceX, _Set | FontConfig.GlyphMinAdvanceX | |
| GlyphMaxAdvanceX | field | Native.IGSharp_FontConfig_GetGlyphMaxAdvanceX, _Set | FontConfig.GlyphMaxAdvanceX | |
| GlyphExtraAdvanceX | field | Native.IGSharp_FontConfig_GetGlyphExtraAdvanceX, _Set | FontConfig.GlyphExtraAdvanceX | |
| FontLoaderFlags | field | Native.IGSharp_FontConfig_GetFontLoaderFlags, _Set | FontConfig.FontLoaderFlags | |
| RasterizerMultiply | field | Native.IGSharp_FontConfig_GetRasterizerMultiply, _Set | FontConfig.RasterizerMultiply | |
| RasterizerDensity | field | Native.IGSharp_FontConfig_GetRasterizerDensity, _Set | FontConfig.RasterizerDensity | |
| EllipsisChar | field | Native.IGSharp_FontConfig_GetEllipsisChar, _Set | FontConfig.EllipsisChar | |
| ExtraSizeScale | field | Native.IGSharp_FontConfig_GetExtraSizeScale, _Set | FontConfig.ExtraSizeScale | |
| Name | field | Native.IGSharp_FontConfig_GetName, _Set | FontConfig.Name | |

## ImTextureData API

ImTextureData is exposed as a `readonly struct TextureData` wrapping the native pointer.

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| (constructor) | function | Native.IGSharp_TextureData_Create | - | internal — backend-owned |
| DestroyPixels | function | Native.IGSharp_TextureData_DestroyPixels | - | internal — backend-owned |
| UniqueID | field | Native.IGSharp_TextureData_GetUniqueID | TextureData.UniqueId | |
| Status | field | Native.IGSharp_TextureData_GetStatus | TextureData.Status | TextureStatus |
| SetStatus | function | Native.IGSharp_TextureData_SetStatus | - | internal — backend-owned |
| Format | field | Native.IGSharp_TextureData_GetFormat | TextureData.Format | TextureFormat |
| Width | field | Native.IGSharp_TextureData_GetWidth | TextureData.Width | |
| Height | field | Native.IGSharp_TextureData_GetHeight | TextureData.Height | |
| BytesPerPixel | field | Native.IGSharp_TextureData_GetBytesPerPixel | TextureData.BytesPerPixel | |
| GetPitch | function | Native.IGSharp_TextureData_GetPitch | TextureData.Pitch | |
| Pixels | field | Native.IGSharp_TextureData_GetPixels, _GetPixelsPtr | TextureData.Pixels, TextureData.GetPixelSpan | |
| GetPixelsAt | function | Native.IGSharp_TextureData_GetPixelsAt | TextureData.GetPixelsAt | |
| GetSizeInBytes | function | Native.IGSharp_TextureData_GetSizeInBytes | TextureData.SizeInBytes | |
| GetTexID | function | Native.IGSharp_TextureData_GetTexID | TextureData.TextureId | |
| SetTexID | function | Native.IGSharp_TextureData_SetTexID | - | internal — backend-owned |
| UsedRect | field | Native.IGSharp_TextureData_GetUsedRect | TextureData.UsedRect | TextureRect |
| UpdateRect | field | Native.IGSharp_TextureData_GetUpdateRect | TextureData.UpdateRect | TextureRect |
| Updates.Size | field | Native.IGSharp_TextureData_GetUpdatesCount | TextureData.UpdateCount | |
| Updates[idx] | field | Native.IGSharp_TextureData_GetUpdate | TextureData.GetUpdate | Returns TextureRect |
| UnusedFrames | field | Native.IGSharp_TextureData_GetUnusedFrames | TextureData.UnusedFrames | |
| RefCount | field | Native.IGSharp_TextureData_GetRefCount | TextureData.RefCount | |
| UseColors | field | Native.IGSharp_TextureData_GetUseColors | TextureData.UseColors | |
| BackendUserData | field | Native.IGSharp_TextureData_GetBackendUserData, _Set | - | internal — backend-owned |

## ImGuiViewport API

ImGuiViewport is exposed as a `readonly struct Viewport` wrapping the native pointer.

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| ID | field | Native.IGSharp_Viewport_GetID | Viewport.Id | |
| Flags | field | Native.IGSharp_Viewport_GetFlags | Viewport.Flags | ViewportFlags |
| Pos | field | Native.IGSharp_Viewport_GetPos | Viewport.Pos | |
| Size | field | Native.IGSharp_Viewport_GetSize | Viewport.Size | |
| FramebufferScale | field | Native.IGSharp_Viewport_GetFramebufferScale | Viewport.FramebufferScale | |
| WorkPos | field | Native.IGSharp_Viewport_GetWorkPos | Viewport.WorkPos | |
| WorkSize | field | Native.IGSharp_Viewport_GetWorkSize | Viewport.WorkSize | |
| PlatformHandle | field | Native.IGSharp_Viewport_GetPlatformHandle, _Set | Viewport.PlatformHandle | |
| PlatformHandleRaw | field | Native.IGSharp_Viewport_GetPlatformHandleRaw, _Set | Viewport.PlatformHandleRaw | |
| GetCenter | function | Native.IGSharp_Viewport_GetCenter | Viewport.Center | |
| GetWorkCenter | function | Native.IGSharp_Viewport_GetWorkCenter | Viewport.WorkCenter | |

## ImGuiStorage API

ImGuiStorage is exposed as a `sealed class Storage : IDisposable` (also returned borrowed by `ImGui.GetStateStorage`).

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| (constructor) | function | Native.IGSharp_Storage_New | Storage.ctor | |
| (destructor) | function | Native.IGSharp_Storage_Delete | Storage.Dispose | |
| Clear | function | Native.IGSharp_Storage_Clear | Storage.Clear | |
| GetInt / SetInt | function | Native.IGSharp_Storage_GetInt, _SetInt | Storage.GetInt, Storage.SetInt | |
| GetBool / SetBool | function | Native.IGSharp_Storage_GetBool, _SetBool | Storage.GetBool, Storage.SetBool | |
| GetFloat / SetFloat | function | Native.IGSharp_Storage_GetFloat, _SetFloat | Storage.GetFloat, Storage.SetFloat | |
| GetVoidPtr / SetVoidPtr | function | Native.IGSharp_Storage_GetVoidPtr, _SetVoidPtr | Storage.GetPointer, Storage.SetPointer | |
| GetIntRef | function | Native.IGSharp_Storage_GetIntRef | - | niche — dangling-pointer risk |
| GetBoolRef | function | Native.IGSharp_Storage_GetBoolRef | - | niche — dangling-pointer risk |
| GetFloatRef | function | Native.IGSharp_Storage_GetFloatRef | - | niche — dangling-pointer risk |
| GetVoidPtrRef | function | Native.IGSharp_Storage_GetVoidPtrRef | - | niche — dangling-pointer risk |
| BuildSortByKey | function | Native.IGSharp_Storage_BuildSortByKey | Storage.BuildSortByKey | |
| SetAllInt | function | Native.IGSharp_Storage_SetAllInt | Storage.SetAllInt | |

## ImGuiTextFilter API

ImGuiTextFilter is exposed as a `sealed class TextFilter : IDisposable`.

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| (constructor) | function | Native.IGSharp_TextFilter_New | TextFilter.ctor | |
| (destructor) | function | Native.IGSharp_TextFilter_Delete | TextFilter.Dispose | |
| Draw | function | Native.IGSharp_TextFilter_Draw | TextFilter.Draw | |
| PassFilter | function | Native.IGSharp_TextFilter_PassFilter | TextFilter.PassFilter | |
| Build | function | Native.IGSharp_TextFilter_Build | TextFilter.Build | |
| Clear | function | Native.IGSharp_TextFilter_Clear | TextFilter.Clear | |
| IsActive | function | Native.IGSharp_TextFilter_IsActive | TextFilter.IsActive | |

## ImGuiTextBuffer API

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| ImGuiTextBuffer (all members) | class | Native.IGSharp_TextBuffer_New, _Delete, _CStr, _Size, _Empty, _Clear, _Resize, _Reserve, _Append | - | .NET — StringBuilder covers it |

## ImGuiOnceUponAFrame API

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| ImGuiOnceUponAFrame (all members) | class | Native.IGSharp_OnceUponAFrame_New, _Delete, _Check, _GetRefFrame | - | C++ only — C# code gates naturally |

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

## ImGuiSelectionBasicStorage / ImGuiSelectionExternalStorage API

Exposed as `sealed class SelectionBasicStorage : IDisposable` and `sealed class SelectionExternalStorage : IDisposable`.

| ImGui Symbol | Kind | Native Wrapper | Managed Wrapper | Notes |
|---|---|---|---|---|
| (constructor) | function | Native.IGSharp_SelectionBasicStorage_Create | SelectionBasicStorage.ctor | |
| (destructor) | function | Native.IGSharp_SelectionBasicStorage_Destroy | SelectionBasicStorage.Dispose | |
| Size | field | Native.IGSharp_SelectionBasicStorage_GetSize | SelectionBasicStorage.Size | |
| PreserveOrder | field | Native.IGSharp_SelectionBasicStorage_GetPreserveOrder, _Set | SelectionBasicStorage.PreserveOrder | |
| UserData | field | Native.IGSharp_SelectionBasicStorage_GetUserData, _Set | SelectionBasicStorage.UserData | |
| AdapterIndexToStorageId | field | Native.IGSharp_SelectionBasicStorage_SetAdapterIndexToStorageId | SelectionBasicStorage.SetIndexToStorageIdAdapter | SelectionIndexToStorageId delegate |
| ApplyRequests | function | Native.IGSharp_SelectionBasicStorage_ApplyRequests | SelectionBasicStorage.ApplyRequests | |
| Contains | function | Native.IGSharp_SelectionBasicStorage_Contains | SelectionBasicStorage.Contains | |
| Clear | function | Native.IGSharp_SelectionBasicStorage_Clear | SelectionBasicStorage.Clear | |
| Swap | function | - | - | niche |
| SetItemSelected | function | Native.IGSharp_SelectionBasicStorage_SetItemSelected | SelectionBasicStorage.SetItemSelected | |
| GetNextSelectedItem | function | Native.IGSharp_SelectionBasicStorage_GetNextSelectedItem | SelectionBasicStorage.SelectedItems | IEnumerable&lt;uint&gt; |
| GetStorageIdFromIndex | function | Native.IGSharp_SelectionBasicStorage_GetStorageIdFromIndex | SelectionBasicStorage.GetStorageIdFromIndex | |
| (ext constructor) | function | Native.IGSharp_SelectionExternalStorage_Create | SelectionExternalStorage.ctor | |
| (ext destructor) | function | Native.IGSharp_SelectionExternalStorage_Destroy | SelectionExternalStorage.Dispose | |
| ext.UserData | field | Native.IGSharp_SelectionExternalStorage_GetUserData, _Set | SelectionExternalStorage.UserData | |
| ext.AdapterSetItemSelected | field | Native.IGSharp_SelectionExternalStorage_SetAdapterSetItemSelected | SelectionExternalStorage.SetItemSelectedAdapter | SelectionSetItemSelected delegate |
| ext.ApplyRequests | function | Native.IGSharp_SelectionExternalStorage_ApplyRequests | SelectionExternalStorage.ApplyRequests | |

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
| ImFontAtlasRect | struct | FontAtlasRect | `readonly record struct FontAtlasRect(int X, int Y, int Width, int Height, Vec2 Uv0, Vec2 Uv1)` |
| ImTextureRect | struct | TextureRect | `readonly record struct TextureRect(int X, int Y, int Width, int Height)` |
| ImGuiPlatformImeData | struct | PlatformImeData | `readonly record struct` |

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
| ImDrawListFlags | DrawListFlags | |
| ImFontAtlasFlags | FontAtlasFlags | |
| ImFontFlags | FontFlags | |
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
| ImTextureFormat | TextureFormat | |
| ImTextureStatus | TextureStatus | |
| ImGuiTreeNodeFlags | TreeNodeFlags | |
| ImGuiViewportFlags | ViewportFlags | |
| ImGuiWindowFlags | WindowFlags | |

## Delegates

| ImGui Symbol | Managed Wrapper | Notes |
|---|---|---|
| ImGuiInputTextCallback | InputTextCallback | `delegate int InputTextCallback(InputTextCallbackData data)` |
| (PlotLines/Histogram getter) | PlotValuesGetter | `delegate float PlotValuesGetter(int idx)` |
| ImDrawCallback | Action&lt;DrawList, DrawCmd&gt; | Via DrawList.AddCallback |
| (SelectionBasicStorage adapter) | SelectionIndexToStorageId | `delegate uint SelectionIndexToStorageId(int index)` |
| (SelectionExternalStorage adapter) | SelectionSetItemSelected | `delegate void SelectionSetItemSelected(int index, bool selected)` |
| (Platform IME handler) | ImeDataHandler | `delegate void ImeDataHandler(Viewport viewport, in PlatformImeData data)` |

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
| ImGui_ImplSDLGPU3_PrepareDrawData | function | Native.IGSharp_ImplSDLGPU3_PrepareDrawData | ImGuiBackend.PrepareDrawData | Takes DrawData |
| ImGui_ImplSDLGPU3_RenderDrawData | function | Native.IGSharp_ImplSDLGPU3_RenderDrawData | ImGuiBackend.RenderDrawData | Takes DrawData |

Custom SDL_GPU pipelines: `Native.IGSharp_ImplSDLGPU3_RenderDrawDataWithPipeline` is exposed through the four-argument `ImGuiBackend.RenderDrawData` overload.
