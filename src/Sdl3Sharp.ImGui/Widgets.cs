using System;
using System.Runtime.CompilerServices;
using static Sdl3Sharp.ImGui.Native.ImGui;

namespace Sdl3Sharp.ImGui;

public unsafe static class Widgets
{
    /// <summary>
    /// Begins a child window. Must be paired with a call to <see cref="EndChild"/>.
    /// </summary>
    /// <param name="id">The child window string ID.</param>
    /// <param name="size">
    /// The size of the child window. Use 0.0f for an axis to use remaining parent window size.
    /// Use values greater than 0.0f for explicit size. Use negative values to right/bottom-align.
    /// </param>
    /// <param name="childFlags">Child window behavior flags.</param>
    /// <param name="windowFlags">Window behavior flags.</param>
    /// <returns>
    /// False if the window is collapsed or fully clipped (you can early out and skip submitting content).
    /// Always call <see cref="EndChild"/> regardless of this return value.
    /// </returns>
    /// <remarks>
    /// Use child windows to create independent scrolling/clipping regions within a host window.
    /// Child windows can embed their own child windows.
    /// </remarks>
    public static bool BeginChild(ReadOnlySpan<byte> id, Vec2 size = default, ChildFlags childFlags = ChildFlags.None, WindowFlags windowFlags = WindowFlags.None)
    {
        fixed (byte* ptr = id)
        {
            return ImGui_BeginChild(ptr, size.Value, (Native.ImGuiChildFlags)childFlags, (Native.ImGuiWindowFlags)windowFlags);
        }
    }

    /// <summary>
    /// Begins a child window using an integer ID. Must be paired with a call to <see cref="EndChild"/>.
    /// </summary>
    /// <param name="id">The child window integer ID.</param>
    /// <param name="size">
    /// The size of the child window. Use 0.0f for an axis to use remaining parent window size.
    /// Use values greater than 0.0f for explicit size. Use negative values to right/bottom-align.
    /// </param>
    /// <param name="childFlags">Child window behavior flags.</param>
    /// <param name="windowFlags">Window behavior flags.</param>
    /// <returns>
    /// False if the window is collapsed or fully clipped (you can early out and skip submitting content).
    /// Always call <see cref="EndChild"/> regardless of this return value.
    /// </returns>
    /// <remarks>
    /// Use child windows to create independent scrolling/clipping regions within a host window.
    /// Child windows can embed their own child windows.
    /// </remarks>
    public static bool BeginChild(Id id, Vec2 size = default, ChildFlags childFlags = ChildFlags.None, WindowFlags windowFlags = WindowFlags.None)
    {
        return ImGui_BeginChildID(id.Value, size.Value, (Native.ImGuiChildFlags)childFlags, (Native.ImGuiWindowFlags)windowFlags);
    }

    /// <summary>
    /// Ends a child window. Must be called for every <see cref="BeginChild"/> call, regardless of its return value.
    /// </summary>
    public static void EndChild()
    {
        ImGui_EndChild();
    }

    /// <summary>
    /// Draws a separator, generally horizontal. Inside a menu bar or in horizontal layout mode, this becomes a vertical separator.
    /// </summary>
    public static void Separator()
    {
        ImGui_Separator();
    }

    /// <summary>
    /// Calls between widgets or groups to layout them horizontally with custom positioning.
    /// </summary>
    /// <param name="offsetFromStartX">X position from window start in window coordinates. 0.0f to use current position.</param>
    /// <param name="spacing">Spacing between the previous widget and current position. -1.0f to use default spacing.</param>
    public static void SameLine(float offsetFromStartX = 0.0f, float spacing = -1.0f)
    {
        ImGui_SameLineEx(offsetFromStartX, spacing);
    }

    /// <summary>
    /// Undoes a <see cref="SameLine"/> or forces a new line when in a horizontal-layout context.
    /// </summary>
    public static void NewLine()
    {
        ImGui_NewLine();
    }

    /// <summary>
    /// Adds vertical spacing.
    /// </summary>
    public static void Spacing()
    {
        ImGui_Spacing();
    }

    /// <summary>
    /// Adds a dummy item of the given size.
    /// </summary>
    /// <param name="size">The size of the dummy item.</param>
    /// <remarks>
    /// Unlike InvisibleButton(), Dummy() won't take mouse clicks or be navigable.
    /// </remarks>
    public static void Dummy(Size size)
    {
        ImGui_Dummy(size.Value);
    }

    /// <summary>
    /// Moves content position toward the right by the specified amount.
    /// </summary>
    /// <param name="indentW">The indentation amount. If less than or equal to 0, uses style.IndentSpacing.</param>
    public static void Indent(float indentW = 0.0f)
    {
        ImGui_IndentEx(indentW);
    }

    /// <summary>
    /// Moves content position back to the left by the specified amount.
    /// </summary>
    /// <param name="indentW">The unindentation amount. If less than or equal to 0, uses style.IndentSpacing.</param>
    public static void Unindent(float indentW = 0.0f)
    {
        ImGui_UnindentEx(indentW);
    }

    /// <summary>
    /// Locks the horizontal starting position for a group of items.
    /// </summary>
    /// <remarks>
    /// Must be paired with <see cref="EndGroup"/>.
    /// </remarks>
    public static void BeginGroup()
    {
        ImGui_BeginGroup();
    }

    /// <summary>
    /// Unlocks the horizontal starting position and captures the whole group bounding box into one "item".
    /// </summary>
    /// <remarks>
    /// After calling this, you can use IsItemHovered() or layout primitives such as SameLine() on the whole group.
    /// </remarks>
    public static void EndGroup()
    {
        ImGui_EndGroup();
    }

    /// <summary>
    /// Displays raw text without formatting.
    /// </summary>
    /// <param name="text">The text to display.</param>
    /// <remarks>
    /// This is faster than formatted text functions, with no memory copy or buffer size limits.
    /// Recommended for long chunks of text.
    /// </remarks>
    public static void TextUnformatted(ReadOnlySpan<byte> text)
    {
        fixed (byte* ptr = text)
        {
            ImGui_TextUnformatted(ptr);
        }
    }

    /// <summary>
    /// Displays formatted text.
    /// </summary>
    /// <param name="text">The text to display.</param>
    /// <remarks>
    /// The text is passed directly to ImGui. Any '%' characters in the text are escaped
    /// to prevent them from being interpreted as format specifiers.
    /// </remarks>
    public static void Text(ReadOnlySpan<byte> text)
    {
        fixed (byte* ptr = text)
        {
            ImGui_Text(ptr);
        }
    }

    /// <summary>
    /// Displays colored text.
    /// </summary>
    /// <param name="color">The text color as RGBA values (0-1 range).</param>
    /// <param name="text">The text to display.</param>
    /// <remarks>
    /// Shortcut for PushStyleColor(ImGuiCol_Text, col); Text(text); PopStyleColor();
    /// </remarks>
    public static void TextColored(Vec4 color, ReadOnlySpan<byte> text)
    {
        fixed (byte* ptr = text)
        {
            ImGui_TextColored(color.Value, ptr);
        }
    }

    /// <summary>
    /// Displays text in the disabled color.
    /// </summary>
    /// <param name="text">The text to display.</param>
    /// <remarks>
    /// Shortcut for PushStyleColor(ImGuiCol_Text, style.Colors[ImGuiCol_TextDisabled]); Text(text); PopStyleColor();
    /// </remarks>
    public static void TextDisabled(ReadOnlySpan<byte> text)
    {
        fixed (byte* ptr = text)
        {
            ImGui_TextDisabled(ptr);
        }
    }

    /// <summary>
    /// Displays text with automatic word wrapping.
    /// </summary>
    /// <param name="text">The text to display.</param>
    /// <remarks>
    /// Shortcut for PushTextWrapPos(0.0f); Text(text); PopTextWrapPos();
    /// Note that this won't work on an auto-resizing window if there's no other widgets
    /// to extend the window width. You may need to set a size using SetNextWindowSize().
    /// </remarks>
    public static void TextWrapped(ReadOnlySpan<byte> text)
    {
        fixed (byte* ptr = text)
        {
            ImGui_TextWrapped(ptr);
        }
    }

    /// <summary>
    /// Displays a label and text aligned the same way as value+label widgets.
    /// </summary>
    /// <param name="label">The label text.</param>
    /// <param name="text">The value text to display.</param>
    public static void LabelText(ReadOnlySpan<byte> label, ReadOnlySpan<byte> text)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* textPtr = text)
        {
            ImGui_LabelText(labelPtr, textPtr);
        }
    }

    /// <summary>
    /// Displays a bullet point followed by text.
    /// </summary>
    /// <param name="text">The text to display after the bullet.</param>
    /// <remarks>
    /// Shortcut for Bullet()+Text().
    /// </remarks>
    public static void BulletText(ReadOnlySpan<byte> text)
    {
        fixed (byte* ptr = text)
        {
            ImGui_BulletText(ptr);
        }
    }

    /// <summary>
    /// Displays text with a horizontal separator line.
    /// </summary>
    /// <param name="label">The text label to display.</param>
    public static void SeparatorText(ReadOnlySpan<byte> label)
    {
        fixed (byte* ptr = label)
        {
            ImGui_SeparatorText(ptr);
        }
    }

    /// <summary>
    /// Creates a button widget with explicit size.
    /// </summary>
    /// <param name="label">The button label. ID is derived from the label (use ## to append non-visible ID).</param>
    /// <param name="size">The button size. Use (0,0) for automatic sizing.</param>
    /// <returns>True when clicked.</returns>
    public static bool Button(ReadOnlySpan<byte> label, Size size = default)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_ButtonEx(ptr, size.Value);
        }
    }

    /// <summary>
    /// Creates a small button with FramePadding.y == 0, easily embeddable within text.
    /// </summary>
    /// <param name="label">The button label.</param>
    /// <returns>True when clicked.</returns>
    public static bool SmallButton(ReadOnlySpan<byte> label)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_SmallButton(ptr);
        }
    }

    /// <summary>
    /// Creates a flexible button behavior without visuals.
    /// </summary>
    /// <param name="strId">The string ID.</param>
    /// <param name="size">The clickable area size.</param>
    /// <param name="flags">Button behavior flags.</param>
    /// <returns>True when clicked.</returns>
    /// <remarks>
    /// Useful for building custom behaviors using the public API (along with IsItemActive, IsItemHovered, etc.).
    /// </remarks>
    public static bool InvisibleButton(ReadOnlySpan<byte> strId, Size size, ButtonFlags flags = ButtonFlags.None)
    {
        fixed (byte* ptr = strId)
        {
            return ImGui_InvisibleButton(ptr, size.Value, (Native.ImGuiButtonFlags)flags);
        }
    }

    /// <summary>
    /// Creates a square button with an arrow shape.
    /// </summary>
    /// <param name="strId">The string ID.</param>
    /// <param name="dir">The arrow direction.</param>
    /// <returns>True when clicked.</returns>
    public static bool ArrowButton(ReadOnlySpan<byte> strId, Dir dir)
    {
        fixed (byte* ptr = strId)
        {
            return ImGui_ArrowButton(ptr, (Native.ImGuiDir)dir);
        }
    }

    /// <summary>
    /// Creates a checkbox widget.
    /// </summary>
    /// <param name="label">The checkbox label.</param>
    /// <param name="v">Reference to the boolean value.</param>
    /// <returns>True when the value has been changed.</returns>
    public static bool Checkbox(ReadOnlySpan<byte> label, ref bool v)
    {
        fixed (byte* ptr = label)
        fixed (bool* vPtr = &v)
        {
            return ImGui_Checkbox(ptr, vPtr);
        }
    }

    /// <summary>
    /// Creates a checkbox widget for signed integer flags.
    /// </summary>
    /// <param name="label">The checkbox label.</param>
    /// <param name="flags">Reference to the flags value.</param>
    /// <param name="flagsValue">The flag bit(s) to toggle.</param>
    /// <returns>True when the value has been changed.</returns>
    public static bool CheckboxFlags<T>(ReadOnlySpan<byte> label, ref T flags, T flagsValue)
        where T : unmanaged, Enum
    {
        uint flagsLocal;
        uint flagsValueLocal;
        switch (Unsafe.SizeOf<T>())
        {
            case 1:
                flagsLocal = Unsafe.As<T, byte>(ref flags);
                flagsValueLocal = Unsafe.As<T, byte>(ref Unsafe.AsRef(in flagsValue));
                break;
            case 2:
                flagsLocal = Unsafe.As<T, ushort>(ref flags);
                flagsValueLocal = Unsafe.As<T, ushort>(ref Unsafe.AsRef(in flagsValue));
                break;
            case 4:
                flagsLocal = Unsafe.As<T, uint>(ref flags);
                flagsValueLocal = Unsafe.As<T, uint>(ref Unsafe.AsRef(in flagsValue));
                break;
            default:
                throw new NotSupportedException("CheckboxFlags does not support 64-bit enums. ImGui only provides 32-bit flag checkbox APIs.");
        }

        bool result;
        fixed (byte* ptr = label)
        {
            result = ImGui_CheckboxFlagsUintPtr(ptr, &flagsLocal, flagsValueLocal);
        }

        switch (Unsafe.SizeOf<T>())
        {
            case 1:
                Unsafe.As<T, byte>(ref flags) = (byte)flagsLocal;
                break;
            case 2:
                Unsafe.As<T, ushort>(ref flags) = (ushort)flagsLocal;
                break;
            case 4:
                Unsafe.As<T, uint>(ref flags) = flagsLocal;
                break;
        }

        return result;
    }

    /// <summary>
    /// Creates a radio button widget.
    /// </summary>
    /// <param name="label">The radio button label.</param>
    /// <param name="active">Whether this radio button is currently active.</param>
    /// <returns>True when clicked.</returns>
    /// <remarks>
    /// Use with e.g. if (RadioButton("one", myValue == 1)) { myValue = 1; }
    /// </remarks>
    public static bool RadioButton(ReadOnlySpan<byte> label, bool active)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_RadioButton(ptr, active);
        }
    }

    /// <summary>
    /// Creates a radio button widget that modifies an integer value.
    /// </summary>
    /// <param name="label">The radio button label.</param>
    /// <param name="v">Reference to the current value.</param>
    /// <param name="vButton">The value this button represents.</param>
    /// <returns>True when clicked (and value changed).</returns>
    /// <remarks>
    /// Shortcut to handle the RadioButton pattern when the value is an integer.
    /// </remarks>
    public static bool RadioButton(ReadOnlySpan<byte> label, ref int v, int vButton)
    {
        fixed (byte* ptr = label)
        fixed (int* vPtr = &v)
        {
            return ImGui_RadioButtonIntPtr(ptr, vPtr, vButton);
        }
    }

    /// <summary>
    /// Creates a progress bar widget.
    /// </summary>
    /// <param name="fraction">The progress value between 0.0f and 1.0f.</param>
    /// <param name="sizeArg">The bar size. Use (-FLT_MIN, 0) for default size.</param>
    /// <param name="overlay">Optional overlay text.</param>
    public static void ProgressBar(float fraction, Size sizeArg = default, ReadOnlySpan<byte> overlay = default)
    {
        fixed (byte* overlayPtr = overlay)
        {
            ImGui_ProgressBar(fraction, sizeArg.Value, overlayPtr);
        }
    }

    /// <summary>
    /// Draws a small circle (bullet) and keeps the cursor on the same line.
    /// </summary>
    /// <remarks>
    /// Advances cursor x position by GetTreeNodeToLabelSpacing(), same distance that TreeNode() uses.
    /// </remarks>
    public static void Bullet()
    {
        ImGui_Bullet();
    }

    /// <summary>
    /// Creates a hyperlink text button.
    /// </summary>
    /// <param name="label">The link text.</param>
    /// <returns>True when clicked.</returns>
    public static bool TextLink(ReadOnlySpan<byte> label)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_TextLink(ptr);
        }
    }

    /// <summary>
    /// Creates a hyperlink text button that opens a URL when clicked.
    /// </summary>
    /// <param name="label">The link text (also used as the URL if url is not specified).</param>
    /// <returns>True when clicked.</returns>
    public static bool TextLinkOpenURL(ReadOnlySpan<byte> label)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_TextLinkOpenURL(ptr);
        }
    }

    /// <summary>
    /// Creates a hyperlink text button that opens a specified URL when clicked.
    /// </summary>
    /// <param name="label">The link text.</param>
    /// <param name="url">The URL to open.</param>
    /// <returns>True when clicked.</returns>
    public static bool TextLinkOpenURL(ReadOnlySpan<byte> label, ReadOnlySpan<byte> url)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* urlPtr = url)
        {
            return ImGui_TextLinkOpenURLEx(labelPtr, urlPtr);
        }
    }

    /// <summary>
    /// Displays an image.
    /// </summary>
    /// <param name="texRef">The texture reference.</param>
    /// <param name="imageSize">The size of the image to display.</param>
    /// <remarks>
    /// Image() adds style.ImageBorderSize on each side.
    /// Read about ImTextureID/ImTextureRef here: https://github.com/ocornut/imgui/wiki/Image-Loading-and-Displaying-Examples
    /// </remarks>
    public static void Image(TextureRef texRef, Size imageSize)
    {
        ImGui_Image(texRef.Native, imageSize.Value);
    }

    /// <summary>
    /// Displays an image with explicit UV coordinates.
    /// </summary>
    /// <param name="texRef">The texture reference.</param>
    /// <param name="imageSize">The size of the image to display.</param>
    /// <param name="uv0">The UV coordinate of the top-left corner.</param>
    /// <param name="uv1">The UV coordinate of the bottom-right corner.</param>
    public static void Image(TextureRef texRef, Size imageSize, Vec2 uv0, Vec2 uv1)
    {
        ImGui_ImageEx(texRef.Native, imageSize.Value, uv0.Value, uv1.Value);
    }

    /// <summary>
    /// Displays an image with background and tint color support.
    /// </summary>
    /// <param name="texRef">The texture reference.</param>
    /// <param name="imageSize">The size of the image to display.</param>
    public static void ImageWithBg(TextureRef texRef, Size imageSize)
    {
        ImGui_ImageWithBg(texRef.Native, imageSize.Value);
    }

    /// <summary>
    /// Displays an image with background and tint color support, with explicit UV coordinates and colors.
    /// </summary>
    /// <param name="texRef">The texture reference.</param>
    /// <param name="imageSize">The size of the image to display.</param>
    /// <param name="uv0">The UV coordinate of the top-left corner.</param>
    /// <param name="uv1">The UV coordinate of the bottom-right corner.</param>
    /// <param name="bgCol">The background color.</param>
    /// <param name="tintCol">The tint color to apply to the image.</param>
    public static void ImageWithBg(TextureRef texRef, Size imageSize, Vec2 uv0, Vec2 uv1, Color bgCol, Color tintCol)
    {
        ImGui_ImageWithBgEx(texRef.Native, imageSize.Value, uv0.Value, uv1.Value, bgCol.Value, tintCol.Value);
    }

    /// <summary>
    /// Creates an image button.
    /// </summary>
    /// <param name="strId">The string ID for the button.</param>
    /// <param name="texRef">The texture reference.</param>
    /// <param name="imageSize">The size of the image.</param>
    /// <returns>True when clicked.</returns>
    /// <remarks>
    /// ImageButton() adds style.FramePadding on each side and draws a background based on regular Button() color.
    /// </remarks>
    public static bool ImageButton(ReadOnlySpan<byte> strId, TextureRef texRef, Size imageSize)
    {
        fixed (byte* ptr = strId)
        {
            return ImGui_ImageButton(ptr, texRef.Native, imageSize.Value);
        }
    }

    /// <summary>
    /// Creates an image button with explicit UV coordinates and colors.
    /// </summary>
    /// <param name="strId">The string ID for the button.</param>
    /// <param name="texRef">The texture reference.</param>
    /// <param name="imageSize">The size of the image.</param>
    /// <param name="uv0">The UV coordinate of the top-left corner.</param>
    /// <param name="uv1">The UV coordinate of the bottom-right corner.</param>
    /// <param name="bgCol">The background color.</param>
    /// <param name="tintCol">The tint color to apply to the image.</param>
    /// <returns>True when clicked.</returns>
    public static bool ImageButton(ReadOnlySpan<byte> strId, TextureRef texRef, Size imageSize, Vec2 uv0, Vec2 uv1, Color bgCol, Color tintCol)
    {
        fixed (byte* ptr = strId)
        {
            return ImGui_ImageButtonEx(ptr, texRef.Native, imageSize.Value, uv0.Value, uv1.Value, bgCol.Value, tintCol.Value);
        }
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
    public static bool BeginCombo(ReadOnlySpan<byte> label, ReadOnlySpan<byte> previewValue, ComboFlags flags = ComboFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* previewPtr = previewValue)
        {
            return ImGui_BeginCombo(labelPtr, previewPtr, (Native.ImGuiComboFlags)flags);
        }
    }

    /// <summary>
    /// Ends a combo box. Only call this if <see cref="BeginCombo"/> returned true.
    /// </summary>
    public static void EndCombo()
    {
        ImGui_EndCombo();
    }

    /// <summary>
    /// Creates a combo box with items separated by null characters and explicit popup height.
    /// </summary>
    /// <param name="label">The label for the combo box.</param>
    /// <param name="currentItem">Reference to the current selected item index.</param>
    /// <param name="itemsSeparatedByZeros">Items separated by \0, ending with \0\0. e.g. "One\0Two\0Three\0"</param>
    /// <param name="popupMaxHeightInItems">Maximum height in items. Use -1 for default.</param>
    /// <returns>True if the selection changed.</returns>
    public static bool Combo(ReadOnlySpan<byte> label, ref int currentItem, ReadOnlySpan<byte> itemsSeparatedByZeros, int popupMaxHeightInItems = -1)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* itemsPtr = itemsSeparatedByZeros)
        fixed (int* currentItemPtr = &currentItem)
        {
            return ImGui_ComboEx(labelPtr, currentItemPtr, itemsPtr, popupMaxHeightInItems);
        }
    }

    /// <summary>
    /// Creates a drag slider for an int value with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="v">Reference to the value.</param>
    /// <param name="vSpeed">The speed of value change per pixel of mouse movement.</param>
    /// <param name="vMin">Minimum value. If vMin >= vMax, there is no bound.</param>
    /// <param name="vMax">Maximum value. If vMin >= vMax, there is no bound.</param>
    /// <param name="format">Printf format string for display (e.g., "%.3f").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if the value changed.</returns>
    public static bool Drag(ReadOnlySpan<byte> label, ref int v, float vSpeed, int vMin = 0, int vMax = 0, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (int* vPtr = &v)
        {
            return ImGui_DragIntEx(labelPtr, vPtr, vSpeed, vMin, vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a drag slider for a float value with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="v">Reference to the value.</param>
    /// <param name="vSpeed">The speed of value change per pixel of mouse movement.</param>
    /// <param name="vMin">Minimum value. If vMin >= vMax, there is no bound.</param>
    /// <param name="vMax">Maximum value. If vMin >= vMax, there is no bound.</param>
    /// <param name="format">Printf format string for display (e.g., "%.3f").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if the value changed.</returns>
    public static bool Drag(ReadOnlySpan<byte> label, ref float v, float vSpeed, float vMin = 0.0f, float vMax = 0.0f, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (float* vPtr = &v)
        {
            return ImGui_DragFloatEx(labelPtr, vPtr, vSpeed, vMin, vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a drag slider for a signed byte value with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="v">Reference to the value.</param>
    /// <param name="vSpeed">The speed of value change per pixel of mouse movement.</param>
    /// <param name="vMin">Minimum value. If vMin >= vMax, there is no bound.</param>
    /// <param name="vMax">Maximum value. If vMin >= vMax, there is no bound.</param>
    /// <param name="format">Printf format string for display (e.g., "%d").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if the value changed.</returns>
    public static bool Drag(ReadOnlySpan<byte> label, ref sbyte v, float vSpeed, sbyte vMin = 0, sbyte vMax = 0, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (sbyte* vPtr = &v)
        {
            return ImGui_DragScalarEx(labelPtr, Native.ImGuiDataType.S8, vPtr, vSpeed, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a drag slider for a byte value with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="v">Reference to the value.</param>
    /// <param name="vSpeed">The speed of value change per pixel of mouse movement.</param>
    /// <param name="vMin">Minimum value. If vMin >= vMax, there is no bound.</param>
    /// <param name="vMax">Maximum value. If vMin >= vMax, there is no bound.</param>
    /// <param name="format">Printf format string for display (e.g., "%u").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if the value changed.</returns>
    public static bool Drag(ReadOnlySpan<byte> label, ref byte v, float vSpeed, byte vMin = 0, byte vMax = 0, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (byte* vPtr = &v)
        {
            return ImGui_DragScalarEx(labelPtr, Native.ImGuiDataType.U8, vPtr, vSpeed, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a drag slider for a short value with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="v">Reference to the value.</param>
    /// <param name="vSpeed">The speed of value change per pixel of mouse movement.</param>
    /// <param name="vMin">Minimum value. If vMin >= vMax, there is no bound.</param>
    /// <param name="vMax">Maximum value. If vMin >= vMax, there is no bound.</param>
    /// <param name="format">Printf format string for display (e.g., "%d").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if the value changed.</returns>
    public static bool Drag(ReadOnlySpan<byte> label, ref short v, float vSpeed, short vMin = 0, short vMax = 0, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (short* vPtr = &v)
        {
            return ImGui_DragScalarEx(labelPtr, Native.ImGuiDataType.S16, vPtr, vSpeed, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a drag slider for an unsigned short value with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="v">Reference to the value.</param>
    /// <param name="vSpeed">The speed of value change per pixel of mouse movement.</param>
    /// <param name="vMin">Minimum value. If vMin >= vMax, there is no bound.</param>
    /// <param name="vMax">Maximum value. If vMin >= vMax, there is no bound.</param>
    /// <param name="format">Printf format string for display (e.g., "%u").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if the value changed.</returns>
    public static bool Drag(ReadOnlySpan<byte> label, ref ushort v, float vSpeed, ushort vMin = 0, ushort vMax = 0, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (ushort* vPtr = &v)
        {
            return ImGui_DragScalarEx(labelPtr, Native.ImGuiDataType.U16, vPtr, vSpeed, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a drag slider for an unsigned int value with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="v">Reference to the value.</param>
    /// <param name="vSpeed">The speed of value change per pixel of mouse movement.</param>
    /// <param name="vMin">Minimum value. If vMin >= vMax, there is no bound.</param>
    /// <param name="vMax">Maximum value. If vMin >= vMax, there is no bound.</param>
    /// <param name="format">Printf format string for display (e.g., "%u").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if the value changed.</returns>
    public static bool Drag(ReadOnlySpan<byte> label, ref uint v, float vSpeed, uint vMin = 0, uint vMax = 0, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (uint* vPtr = &v)
        {
            return ImGui_DragScalarEx(labelPtr, Native.ImGuiDataType.U32, vPtr, vSpeed, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a drag slider for a long value with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="v">Reference to the value.</param>
    /// <param name="vSpeed">The speed of value change per pixel of mouse movement.</param>
    /// <param name="vMin">Minimum value. If vMin >= vMax, there is no bound.</param>
    /// <param name="vMax">Maximum value. If vMin >= vMax, there is no bound.</param>
    /// <param name="format">Printf format string for display (e.g., "%lld").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if the value changed.</returns>
    public static bool Drag(ReadOnlySpan<byte> label, ref long v, float vSpeed, long vMin = 0, long vMax = 0, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (long* vPtr = &v)
        {
            return ImGui_DragScalarEx(labelPtr, Native.ImGuiDataType.S64, vPtr, vSpeed, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a drag slider for an unsigned long value with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="v">Reference to the value.</param>
    /// <param name="vSpeed">The speed of value change per pixel of mouse movement.</param>
    /// <param name="vMin">Minimum value. If vMin >= vMax, there is no bound.</param>
    /// <param name="vMax">Maximum value. If vMin >= vMax, there is no bound.</param>
    /// <param name="format">Printf format string for display (e.g., "%llu").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if the value changed.</returns>
    public static bool Drag(ReadOnlySpan<byte> label, ref ulong v, float vSpeed, ulong vMin = 0, ulong vMax = 0, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (ulong* vPtr = &v)
        {
            return ImGui_DragScalarEx(labelPtr, Native.ImGuiDataType.U64, vPtr, vSpeed, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a drag slider for a double value with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="v">Reference to the value.</param>
    /// <param name="vSpeed">The speed of value change per pixel of mouse movement.</param>
    /// <param name="vMin">Minimum value. If vMin >= vMax, there is no bound.</param>
    /// <param name="vMax">Maximum value. If vMin >= vMax, there is no bound.</param>
    /// <param name="format">Printf format string for display (e.g., "%.6f").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if the value changed.</returns>
    public static bool Drag(ReadOnlySpan<byte> label, ref double v, float vSpeed, double vMin = 0.0, double vMax = 0.0, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (double* vPtr = &v)
        {
            return ImGui_DragScalarEx(labelPtr, Native.ImGuiDataType.Double, vPtr, vSpeed, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a drag slider for an array of int values with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="v">Reference to the array of values.</param>
    /// <param name="vSpeed">The speed of value change per pixel of mouse movement.</param>
    /// <param name="vMin">Minimum value. If vMin >= vMax, there is no bound.</param>
    /// <param name="vMax">Maximum value. If vMin >= vMax, there is no bound.</param>
    /// <param name="format">Printf format string for display (e.g., "%d").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if any value changed.</returns>
    public static bool Drag(ReadOnlySpan<byte> label, Span<int> v, float vSpeed, int vMin = 0, int vMax = 0, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (int* vPtr = v)
        {
            return v.Length switch
            {
                2 => ImGui_DragInt2Ex(labelPtr, vPtr, vSpeed, vMin, vMax, formatPtr, (Native.ImGuiSliderFlags)flags),
                3 => ImGui_DragInt3Ex(labelPtr, vPtr, vSpeed, vMin, vMax, formatPtr, (Native.ImGuiSliderFlags)flags),
                4 => ImGui_DragInt4Ex(labelPtr, vPtr, vSpeed, vMin, vMax, formatPtr, (Native.ImGuiSliderFlags)flags),
                _ => ImGui_DragScalarNEx(labelPtr, Native.ImGuiDataType.S32, vPtr, v.Length, vSpeed, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags),
            };
        }
    }

    /// <summary>
    /// Creates a drag slider for an array of float values with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="v">Reference to the array of values.</param>
    /// <param name="vSpeed">The speed of value change per pixel of mouse movement.</param>
    /// <param name="vMin">Minimum value. If vMin >= vMax, there is no bound.</param>
    /// <param name="vMax">Maximum value. If vMin >= vMax, there is no bound.</param>
    /// <param name="format">Printf format string for display (e.g., "%.3f").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if any value changed.</returns>
    public static bool Drag(ReadOnlySpan<byte> label, Span<float> v, float vSpeed, float vMin = 0.0f, float vMax = 0.0f, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (float* vPtr = v)
        {
            return v.Length switch
            {
                2 => ImGui_DragFloat2Ex(labelPtr, vPtr, vSpeed, vMin, vMax, formatPtr, (Native.ImGuiSliderFlags)flags),
                3 => ImGui_DragFloat3Ex(labelPtr, vPtr, vSpeed, vMin, vMax, formatPtr, (Native.ImGuiSliderFlags)flags),
                4 => ImGui_DragFloat4Ex(labelPtr, vPtr, vSpeed, vMin, vMax, formatPtr, (Native.ImGuiSliderFlags)flags),
                _ => ImGui_DragScalarNEx(labelPtr, Native.ImGuiDataType.Float, vPtr, v.Length, vSpeed, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags),
            };
        }
    }

    /// <summary>
    /// Creates a drag slider for an array of signed byte values with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="v">Reference to the array of values.</param>
    /// <param name="vSpeed">The speed of value change per pixel of mouse movement.</param>
    /// <param name="vMin">Minimum value. If vMin >= vMax, there is no bound.</param>
    /// <param name="vMax">Maximum value. If vMin >= vMax, there is no bound.</param>
    /// <param name="format">Printf format string for display (e.g., "%d").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if any value changed.</returns>
    public static bool Drag(ReadOnlySpan<byte> label, Span<sbyte> v, float vSpeed, sbyte vMin = 0, sbyte vMax = 0, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (sbyte* vPtr = v)
        {
            return ImGui_DragScalarNEx(labelPtr, Native.ImGuiDataType.S8, vPtr, v.Length, vSpeed, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a drag slider for an array of byte values with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="v">Reference to the array of values.</param>
    /// <param name="vSpeed">The speed of value change per pixel of mouse movement.</param>
    /// <param name="vMin">Minimum value. If vMin >= vMax, there is no bound.</param>
    /// <param name="vMax">Maximum value. If vMin >= vMax, there is no bound.</param>
    /// <param name="format">Printf format string for display (e.g., "%u").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if any value changed.</returns>
    public static bool Drag(ReadOnlySpan<byte> label, Span<byte> v, float vSpeed, byte vMin = 0, byte vMax = 0, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (byte* vPtr = v)
        {
            return ImGui_DragScalarNEx(labelPtr, Native.ImGuiDataType.U8, vPtr, v.Length, vSpeed, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a drag slider for an array of short values with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="v">Reference to the array of values.</param>
    /// <param name="vSpeed">The speed of value change per pixel of mouse movement.</param>
    /// <param name="vMin">Minimum value. If vMin >= vMax, there is no bound.</param>
    /// <param name="vMax">Maximum value. If vMin >= vMax, there is no bound.</param>
    /// <param name="format">Printf format string for display (e.g., "%d").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if any value changed.</returns>
    public static bool Drag(ReadOnlySpan<byte> label, Span<short> v, float vSpeed, short vMin = 0, short vMax = 0, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (short* vPtr = v)
        {
            return ImGui_DragScalarNEx(labelPtr, Native.ImGuiDataType.S16, vPtr, v.Length, vSpeed, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a drag slider for an array of unsigned short values with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="v">Reference to the array of values.</param>
    /// <param name="vSpeed">The speed of value change per pixel of mouse movement.</param>
    /// <param name="vMin">Minimum value. If vMin >= vMax, there is no bound.</param>
    /// <param name="vMax">Maximum value. If vMin >= vMax, there is no bound.</param>
    /// <param name="format">Printf format string for display (e.g., "%u").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if any value changed.</returns>
    public static bool Drag(ReadOnlySpan<byte> label, Span<ushort> v, float vSpeed, ushort vMin = 0, ushort vMax = 0, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (ushort* vPtr = v)
        {
            return ImGui_DragScalarNEx(labelPtr, Native.ImGuiDataType.U16, vPtr, v.Length, vSpeed, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a drag slider for an array of unsigned int values with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="v">Reference to the array of values.</param>
    /// <param name="vSpeed">The speed of value change per pixel of mouse movement.</param>
    /// <param name="vMin">Minimum value. If vMin >= vMax, there is no bound.</param>
    /// <param name="vMax">Maximum value. If vMin >= vMax, there is no bound.</param>
    /// <param name="format">Printf format string for display (e.g., "%u").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if any value changed.</returns>
    public static bool Drag(ReadOnlySpan<byte> label, Span<uint> v, float vSpeed, uint vMin = 0, uint vMax = 0, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (uint* vPtr = v)
        {
            return ImGui_DragScalarNEx(labelPtr, Native.ImGuiDataType.U32, vPtr, v.Length, vSpeed, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a drag slider for an array of long values with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="v">Reference to the array of values.</param>
    /// <param name="vSpeed">The speed of value change per pixel of mouse movement.</param>
    /// <param name="vMin">Minimum value. If vMin >= vMax, there is no bound.</param>
    /// <param name="vMax">Maximum value. If vMin >= vMax, there is no bound.</param>
    /// <param name="format">Printf format string for display (e.g., "%lld").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if any value changed.</returns>
    public static bool Drag(ReadOnlySpan<byte> label, Span<long> v, float vSpeed, long vMin = 0, long vMax = 0, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (long* vPtr = v)
        {
            return ImGui_DragScalarNEx(labelPtr, Native.ImGuiDataType.S64, vPtr, v.Length, vSpeed, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a drag slider for an array of unsigned long values with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="v">Reference to the array of values.</param>
    /// <param name="vSpeed">The speed of value change per pixel of mouse movement.</param>
    /// <param name="vMin">Minimum value. If vMin >= vMax, there is no bound.</param>
    /// <param name="vMax">Maximum value. If vMin >= vMax, there is no bound.</param>
    /// <param name="format">Printf format string for display (e.g., "%llu").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if any value changed.</returns>
    public static bool Drag(ReadOnlySpan<byte> label, Span<ulong> v, float vSpeed, ulong vMin = 0, ulong vMax = 0, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (ulong* vPtr = v)
        {
            return ImGui_DragScalarNEx(labelPtr, Native.ImGuiDataType.U64, vPtr, v.Length, vSpeed, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a drag slider for an array of double values with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="v">Reference to the array of values.</param>
    /// <param name="vSpeed">The speed of value change per pixel of mouse movement.</param>
    /// <param name="vMin">Minimum value. If vMin >= vMax, there is no bound.</param>
    /// <param name="vMax">Maximum value. If vMin >= vMax, there is no bound.</param>
    /// <param name="format">Printf format string for display (e.g., "%.6f").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if any value changed.</returns>
    public static bool Drag(ReadOnlySpan<byte> label, Span<double> v, float vSpeed, double vMin = 0.0, double vMax = 0.0, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (double* vPtr = v)
        {
            return ImGui_DragScalarNEx(labelPtr, Native.ImGuiDataType.Double, vPtr, v.Length, vSpeed, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a drag slider for a float range with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="vCurrentMin">Reference to the minimum value.</param>
    /// <param name="vCurrentMax">Reference to the maximum value.</param>
    /// <param name="vSpeed">The speed of value change per pixel of mouse movement.</param>
    /// <param name="vMin">Minimum allowed value.</param>
    /// <param name="vMax">Maximum allowed value.</param>
    /// <param name="format">Printf format string for the min value.</param>
    /// <param name="formatMax">Printf format string for the max value. If null, uses format.</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if either value changed.</returns>
    public static bool DragRange(ReadOnlySpan<byte> label, ref float vCurrentMin, ref float vCurrentMax, float vSpeed, float vMin = 0.0f, float vMax = 0.0f, ReadOnlySpan<byte> format = default, ReadOnlySpan<byte> formatMax = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (byte* formatMaxPtr = formatMax)
        fixed (float* vCurrentMinPtr = &vCurrentMin)
        fixed (float* vCurrentMaxPtr = &vCurrentMax)
        {
            return ImGui_DragFloatRange2Ex(labelPtr, vCurrentMinPtr, vCurrentMaxPtr, vSpeed, vMin, vMax, formatPtr, formatMaxPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a drag slider for an integer range with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="vCurrentMin">Reference to the minimum value.</param>
    /// <param name="vCurrentMax">Reference to the maximum value.</param>
    /// <param name="vSpeed">The speed of value change per pixel of mouse movement.</param>
    /// <param name="vMin">Minimum allowed value.</param>
    /// <param name="vMax">Maximum allowed value.</param>
    /// <param name="format">Printf format string for the min value.</param>
    /// <param name="formatMax">Printf format string for the max value. If null, uses format.</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if either value changed.</returns>
    public static bool DragRange(ReadOnlySpan<byte> label, ref int vCurrentMin, ref int vCurrentMax, float vSpeed, int vMin = 0, int vMax = 0, ReadOnlySpan<byte> format = default, ReadOnlySpan<byte> formatMax = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (byte* formatMaxPtr = formatMax)
        fixed (int* vCurrentMinPtr = &vCurrentMin)
        fixed (int* vCurrentMaxPtr = &vCurrentMax)
        {
            return ImGui_DragIntRange2Ex(labelPtr, vCurrentMinPtr, vCurrentMaxPtr, vSpeed, vMin, vMax, formatPtr, formatMaxPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a slider for an int value with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="v">Reference to the value.</param>
    /// <param name="vMin">Minimum value.</param>
    /// <param name="vMax">Maximum value.</param>
    /// <param name="format">Printf format string for display (e.g., "%d").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if the value changed.</returns>
    public static bool Slider(ReadOnlySpan<byte> label, ref int v, int vMin, int vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (int* vPtr = &v)
        {
            return ImGui_SliderIntEx(labelPtr, vPtr, vMin, vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a slider for a float value with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="v">Reference to the value.</param>
    /// <param name="vMin">Minimum value.</param>
    /// <param name="vMax">Maximum value.</param>
    /// <param name="format">Printf format string for display (e.g., "%.3f").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if the value changed.</returns>
    public static bool Slider(ReadOnlySpan<byte> label, ref float v, float vMin, float vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (float* vPtr = &v)
        {
            return ImGui_SliderFloatEx(labelPtr, vPtr, vMin, vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a slider for a signed byte value with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="v">Reference to the value.</param>
    /// <param name="vMin">Minimum value.</param>
    /// <param name="vMax">Maximum value.</param>
    /// <param name="format">Printf format string for display (e.g., "%d").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if the value changed.</returns>
    public static bool Slider(ReadOnlySpan<byte> label, ref sbyte v, sbyte vMin, sbyte vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (sbyte* vPtr = &v)
        {
            return ImGui_SliderScalarEx(labelPtr, Native.ImGuiDataType.S8, vPtr, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a slider for a byte value with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="v">Reference to the value.</param>
    /// <param name="vMin">Minimum value.</param>
    /// <param name="vMax">Maximum value.</param>
    /// <param name="format">Printf format string for display (e.g., "%u").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if the value changed.</returns>
    public static bool Slider(ReadOnlySpan<byte> label, ref byte v, byte vMin, byte vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (byte* vPtr = &v)
        {
            return ImGui_SliderScalarEx(labelPtr, Native.ImGuiDataType.U8, vPtr, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a slider for a short value with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="v">Reference to the value.</param>
    /// <param name="vMin">Minimum value.</param>
    /// <param name="vMax">Maximum value.</param>
    /// <param name="format">Printf format string for display (e.g., "%d").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if the value changed.</returns>
    public static bool Slider(ReadOnlySpan<byte> label, ref short v, short vMin, short vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (short* vPtr = &v)
        {
            return ImGui_SliderScalarEx(labelPtr, Native.ImGuiDataType.S16, vPtr, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a slider for an unsigned short value with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="v">Reference to the value.</param>
    /// <param name="vMin">Minimum value.</param>
    /// <param name="vMax">Maximum value.</param>
    /// <param name="format">Printf format string for display (e.g., "%u").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if the value changed.</returns>
    public static bool Slider(ReadOnlySpan<byte> label, ref ushort v, ushort vMin, ushort vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (ushort* vPtr = &v)
        {
            return ImGui_SliderScalarEx(labelPtr, Native.ImGuiDataType.U16, vPtr, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a slider for an unsigned int value with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="v">Reference to the value.</param>
    /// <param name="vMin">Minimum value.</param>
    /// <param name="vMax">Maximum value.</param>
    /// <param name="format">Printf format string for display (e.g., "%u").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if the value changed.</returns>
    public static bool Slider(ReadOnlySpan<byte> label, ref uint v, uint vMin, uint vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (uint* vPtr = &v)
        {
            return ImGui_SliderScalarEx(labelPtr, Native.ImGuiDataType.U32, vPtr, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a slider for a long value with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="v">Reference to the value.</param>
    /// <param name="vMin">Minimum value.</param>
    /// <param name="vMax">Maximum value.</param>
    /// <param name="format">Printf format string for display (e.g., "%lld").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if the value changed.</returns>
    public static bool Slider(ReadOnlySpan<byte> label, ref long v, long vMin, long vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (long* vPtr = &v)
        {
            return ImGui_SliderScalarEx(labelPtr, Native.ImGuiDataType.S64, vPtr, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a slider for an unsigned long value with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="v">Reference to the value.</param>
    /// <param name="vMin">Minimum value.</param>
    /// <param name="vMax">Maximum value.</param>
    /// <param name="format">Printf format string for display (e.g., "%llu").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if the value changed.</returns>
    public static bool Slider(ReadOnlySpan<byte> label, ref ulong v, ulong vMin, ulong vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (ulong* vPtr = &v)
        {
            return ImGui_SliderScalarEx(labelPtr, Native.ImGuiDataType.U64, vPtr, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a slider for a double value with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="v">Reference to the value.</param>
    /// <param name="vMin">Minimum value.</param>
    /// <param name="vMax">Maximum value.</param>
    /// <param name="format">Printf format string for display (e.g., "%.6f").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if the value changed.</returns>
    public static bool Slider(ReadOnlySpan<byte> label, ref double v, double vMin, double vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (double* vPtr = &v)
        {
            return ImGui_SliderScalarEx(labelPtr, Native.ImGuiDataType.Double, vPtr, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a slider for an array of int values with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="v">Reference to the array of values.</param>
    /// <param name="vMin">Minimum value.</param>
    /// <param name="vMax">Maximum value.</param>
    /// <param name="format">Printf format string for display (e.g., "%d").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if any value changed.</returns>
    public static bool Slider(ReadOnlySpan<byte> label, Span<int> v, int vMin, int vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (int* vPtr = v)
        {
            return v.Length switch
            {
                2 => ImGui_SliderInt2Ex(labelPtr, vPtr, vMin, vMax, formatPtr, (Native.ImGuiSliderFlags)flags),
                3 => ImGui_SliderInt3Ex(labelPtr, vPtr, vMin, vMax, formatPtr, (Native.ImGuiSliderFlags)flags),
                4 => ImGui_SliderInt4Ex(labelPtr, vPtr, vMin, vMax, formatPtr, (Native.ImGuiSliderFlags)flags),
                _ => ImGui_SliderScalarNEx(labelPtr, Native.ImGuiDataType.S32, vPtr, v.Length, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags),
            };
        }
    }

    /// <summary>
    /// Creates a slider for an array of float values with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="v">Reference to the array of values.</param>
    /// <param name="vMin">Minimum value.</param>
    /// <param name="vMax">Maximum value.</param>
    /// <param name="format">Printf format string for display (e.g., "%.3f").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if any value changed.</returns>
    public static bool Slider(ReadOnlySpan<byte> label, Span<float> v, float vMin, float vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (float* vPtr = v)
        {
            return v.Length switch
            {
                2 => ImGui_SliderFloat2Ex(labelPtr, vPtr, vMin, vMax, formatPtr, (Native.ImGuiSliderFlags)flags),
                3 => ImGui_SliderFloat3Ex(labelPtr, vPtr, vMin, vMax, formatPtr, (Native.ImGuiSliderFlags)flags),
                4 => ImGui_SliderFloat4Ex(labelPtr, vPtr, vMin, vMax, formatPtr, (Native.ImGuiSliderFlags)flags),
                _ => ImGui_SliderScalarNEx(labelPtr, Native.ImGuiDataType.Float, vPtr, v.Length, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags),
            };
        }
    }

    /// <summary>
    /// Creates a slider for an array of signed byte values with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="v">Reference to the array of values.</param>
    /// <param name="vMin">Minimum value.</param>
    /// <param name="vMax">Maximum value.</param>
    /// <param name="format">Printf format string for display (e.g., "%d").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if any value changed.</returns>
    public static bool Slider(ReadOnlySpan<byte> label, Span<sbyte> v, sbyte vMin, sbyte vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (sbyte* vPtr = v)
        {
            return ImGui_SliderScalarNEx(labelPtr, Native.ImGuiDataType.S8, vPtr, v.Length, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a slider for an array of byte values with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="v">Reference to the array of values.</param>
    /// <param name="vMin">Minimum value.</param>
    /// <param name="vMax">Maximum value.</param>
    /// <param name="format">Printf format string for display (e.g., "%u").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if any value changed.</returns>
    public static bool Slider(ReadOnlySpan<byte> label, Span<byte> v, byte vMin, byte vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (byte* vPtr = v)
        {
            return ImGui_SliderScalarNEx(labelPtr, Native.ImGuiDataType.U8, vPtr, v.Length, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a slider for an array of short values with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="v">Reference to the array of values.</param>
    /// <param name="vMin">Minimum value.</param>
    /// <param name="vMax">Maximum value.</param>
    /// <param name="format">Printf format string for display (e.g., "%d").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if any value changed.</returns>
    public static bool Slider(ReadOnlySpan<byte> label, Span<short> v, short vMin, short vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (short* vPtr = v)
        {
            return ImGui_SliderScalarNEx(labelPtr, Native.ImGuiDataType.S16, vPtr, v.Length, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a slider for an array of unsigned short values with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="v">Reference to the array of values.</param>
    /// <param name="vMin">Minimum value.</param>
    /// <param name="vMax">Maximum value.</param>
    /// <param name="format">Printf format string for display (e.g., "%u").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if any value changed.</returns>
    public static bool Slider(ReadOnlySpan<byte> label, Span<ushort> v, ushort vMin, ushort vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (ushort* vPtr = v)
        {
            return ImGui_SliderScalarNEx(labelPtr, Native.ImGuiDataType.U16, vPtr, v.Length, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a slider for an array of unsigned int values with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="v">Reference to the array of values.</param>
    /// <param name="vMin">Minimum value.</param>
    /// <param name="vMax">Maximum value.</param>
    /// <param name="format">Printf format string for display (e.g., "%u").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if any value changed.</returns>
    public static bool Slider(ReadOnlySpan<byte> label, Span<uint> v, uint vMin, uint vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (uint* vPtr = v)
        {
            return ImGui_SliderScalarNEx(labelPtr, Native.ImGuiDataType.U32, vPtr, v.Length, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a slider for an array of long values with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="v">Reference to the array of values.</param>
    /// <param name="vMin">Minimum value.</param>
    /// <param name="vMax">Maximum value.</param>
    /// <param name="format">Printf format string for display (e.g., "%lld").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if any value changed.</returns>
    public static bool Slider(ReadOnlySpan<byte> label, Span<long> v, long vMin, long vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (long* vPtr = v)
        {
            return ImGui_SliderScalarNEx(labelPtr, Native.ImGuiDataType.S64, vPtr, v.Length, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a slider for an array of unsigned long values with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="v">Reference to the array of values.</param>
    /// <param name="vMin">Minimum value.</param>
    /// <param name="vMax">Maximum value.</param>
    /// <param name="format">Printf format string for display (e.g., "%llu").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if any value changed.</returns>
    public static bool Slider(ReadOnlySpan<byte> label, Span<ulong> v, ulong vMin, ulong vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (ulong* vPtr = v)
        {
            return ImGui_SliderScalarNEx(labelPtr, Native.ImGuiDataType.U64, vPtr, v.Length, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a slider for an array of double values with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="v">Reference to the array of values.</param>
    /// <param name="vMin">Minimum value.</param>
    /// <param name="vMax">Maximum value.</param>
    /// <param name="format">Printf format string for display (e.g., "%.6f").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if any value changed.</returns>
    public static bool Slider(ReadOnlySpan<byte> label, Span<double> v, double vMin, double vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (double* vPtr = v)
        {
            return ImGui_SliderScalarNEx(labelPtr, Native.ImGuiDataType.Double, vPtr, v.Length, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates an angle slider for a float value in radians.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="vRad">Reference to the angle value in radians.</param>
    /// <param name="vDegreesMin">Minimum angle in degrees.</param>
    /// <param name="vDegreesMax">Maximum angle in degrees.</param>
    /// <param name="format">Printf format string for display (e.g., "%.0f deg").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if the value changed.</returns>
    public static bool SliderAngle(ReadOnlySpan<byte> label, ref float vRad, float vDegreesMin = -360.0f, float vDegreesMax = 360.0f, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (float* vRadPtr = &vRad)
        {
            return ImGui_SliderAngleEx(labelPtr, vRadPtr, vDegreesMin, vDegreesMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a vertical slider for an int value with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="size">The size of the slider.</param>
    /// <param name="v">Reference to the value.</param>
    /// <param name="vMin">Minimum value.</param>
    /// <param name="vMax">Maximum value.</param>
    /// <param name="format">Printf format string for display (e.g., "%d").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if the value changed.</returns>
    public static bool VSlider(ReadOnlySpan<byte> label, Size size, ref int v, int vMin, int vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (int* vPtr = &v)
        {
            return ImGui_VSliderIntEx(labelPtr, size.Value, vPtr, vMin, vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a vertical slider for a float value with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="size">The size of the slider.</param>
    /// <param name="v">Reference to the value.</param>
    /// <param name="vMin">Minimum value.</param>
    /// <param name="vMax">Maximum value.</param>
    /// <param name="format">Printf format string for display (e.g., "%.3f").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if the value changed.</returns>
    public static bool VSlider(ReadOnlySpan<byte> label, Size size, ref float v, float vMin, float vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (float* vPtr = &v)
        {
            return ImGui_VSliderFloatEx(labelPtr, size.Value, vPtr, vMin, vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a vertical slider for a signed byte value with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="size">The size of the slider.</param>
    /// <param name="v">Reference to the value.</param>
    /// <param name="vMin">Minimum value.</param>
    /// <param name="vMax">Maximum value.</param>
    /// <param name="format">Printf format string for display (e.g., "%d").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if the value changed.</returns>
    public static bool VSlider(ReadOnlySpan<byte> label, Size size, ref sbyte v, sbyte vMin, sbyte vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (sbyte* vPtr = &v)
        {
            return ImGui_VSliderScalarEx(labelPtr, size.Value, Native.ImGuiDataType.S8, vPtr, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a vertical slider for a byte value with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="size">The size of the slider.</param>
    /// <param name="v">Reference to the value.</param>
    /// <param name="vMin">Minimum value.</param>
    /// <param name="vMax">Maximum value.</param>
    /// <param name="format">Printf format string for display (e.g., "%u").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if the value changed.</returns>
    public static bool VSlider(ReadOnlySpan<byte> label, Size size, ref byte v, byte vMin, byte vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (byte* vPtr = &v)
        {
            return ImGui_VSliderScalarEx(labelPtr, size.Value, Native.ImGuiDataType.U8, vPtr, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a vertical slider for a short value with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="size">The size of the slider.</param>
    /// <param name="v">Reference to the value.</param>
    /// <param name="vMin">Minimum value.</param>
    /// <param name="vMax">Maximum value.</param>
    /// <param name="format">Printf format string for display (e.g., "%d").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if the value changed.</returns>
    public static bool VSlider(ReadOnlySpan<byte> label, Size size, ref short v, short vMin, short vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (short* vPtr = &v)
        {
            return ImGui_VSliderScalarEx(labelPtr, size.Value, Native.ImGuiDataType.S16, vPtr, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a vertical slider for an unsigned short value with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="size">The size of the slider.</param>
    /// <param name="v">Reference to the value.</param>
    /// <param name="vMin">Minimum value.</param>
    /// <param name="vMax">Maximum value.</param>
    /// <param name="format">Printf format string for display (e.g., "%u").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if the value changed.</returns>
    public static bool VSlider(ReadOnlySpan<byte> label, Size size, ref ushort v, ushort vMin, ushort vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (ushort* vPtr = &v)
        {
            return ImGui_VSliderScalarEx(labelPtr, size.Value, Native.ImGuiDataType.U16, vPtr, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a vertical slider for an unsigned int value with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="size">The size of the slider.</param>
    /// <param name="v">Reference to the value.</param>
    /// <param name="vMin">Minimum value.</param>
    /// <param name="vMax">Maximum value.</param>
    /// <param name="format">Printf format string for display (e.g., "%u").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if the value changed.</returns>
    public static bool VSlider(ReadOnlySpan<byte> label, Size size, ref uint v, uint vMin, uint vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (uint* vPtr = &v)
        {
            return ImGui_VSliderScalarEx(labelPtr, size.Value, Native.ImGuiDataType.U32, vPtr, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a vertical slider for a long value with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="size">The size of the slider.</param>
    /// <param name="v">Reference to the value.</param>
    /// <param name="vMin">Minimum value.</param>
    /// <param name="vMax">Maximum value.</param>
    /// <param name="format">Printf format string for display (e.g., "%lld").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if the value changed.</returns>
    public static bool VSlider(ReadOnlySpan<byte> label, Size size, ref long v, long vMin, long vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (long* vPtr = &v)
        {
            return ImGui_VSliderScalarEx(labelPtr, size.Value, Native.ImGuiDataType.S64, vPtr, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a vertical slider for an unsigned long value with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="size">The size of the slider.</param>
    /// <param name="v">Reference to the value.</param>
    /// <param name="vMin">Minimum value.</param>
    /// <param name="vMax">Maximum value.</param>
    /// <param name="format">Printf format string for display (e.g., "%llu").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if the value changed.</returns>
    public static bool VSlider(ReadOnlySpan<byte> label, Size size, ref ulong v, ulong vMin, ulong vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (ulong* vPtr = &v)
        {
            return ImGui_VSliderScalarEx(labelPtr, size.Value, Native.ImGuiDataType.U64, vPtr, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a vertical slider for a double value with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="size">The size of the slider.</param>
    /// <param name="v">Reference to the value.</param>
    /// <param name="vMin">Minimum value.</param>
    /// <param name="vMax">Maximum value.</param>
    /// <param name="format">Printf format string for display (e.g., "%.6f").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if the value changed.</returns>
    public static bool VSlider(ReadOnlySpan<byte> label, Size size, ref double v, double vMin, double vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (double* vPtr = &v)
        {
            return ImGui_VSliderScalarEx(labelPtr, size.Value, Native.ImGuiDataType.Double, vPtr, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a keyboard input for an int value with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the input.</param>
    /// <param name="v">Reference to the value.</param>
    /// <param name="step">Step value for +/- buttons. Use 0 to hide buttons.</param>
    /// <param name="stepFast">Fast step value when holding Ctrl.</param>
    /// <param name="flags">Input text behavior flags.</param>
    /// <returns>True if the value changed.</returns>
    public static bool Input(ReadOnlySpan<byte> label, ref int v, int step = 0, int stepFast = 0, InputTextFlags flags = InputTextFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (int* vPtr = &v)
        {
            return ImGui_InputIntEx(labelPtr, vPtr, step, stepFast, (Native.ImGuiInputTextFlags)flags);
        }
    }

    /// <summary>
    /// Creates a keyboard input for a float value with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the input.</param>
    /// <param name="v">Reference to the value.</param>
    /// <param name="step">Step value for +/- buttons. Use 0 to hide buttons.</param>
    /// <param name="stepFast">Fast step value when holding Ctrl.</param>
    /// <param name="format">Printf format string for display (e.g., "%.3f").</param>
    /// <param name="flags">Input text behavior flags.</param>
    /// <returns>True if the value changed.</returns>
    public static bool Input(ReadOnlySpan<byte> label, ref float v, float step = 0.0f, float stepFast = 0.0f, ReadOnlySpan<byte> format = default, InputTextFlags flags = InputTextFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (float* vPtr = &v)
        {
            return ImGui_InputFloatEx(labelPtr, vPtr, step, stepFast, formatPtr, (Native.ImGuiInputTextFlags)flags);
        }
    }

    /// <summary>
    /// Creates a keyboard input for a double value with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the input.</param>
    /// <param name="v">Reference to the value.</param>
    /// <param name="step">Step value for +/- buttons. Use 0 to hide buttons.</param>
    /// <param name="stepFast">Fast step value when holding Ctrl.</param>
    /// <param name="format">Printf format string for display (e.g., "%.6f").</param>
    /// <param name="flags">Input text behavior flags.</param>
    /// <returns>True if the value changed.</returns>
    public static bool Input(ReadOnlySpan<byte> label, ref double v, double step = 0.0, double stepFast = 0.0, ReadOnlySpan<byte> format = default, InputTextFlags flags = InputTextFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (double* vPtr = &v)
        {
            return ImGui_InputDoubleEx(labelPtr, vPtr, step, stepFast, formatPtr, (Native.ImGuiInputTextFlags)flags);
        }
    }

    /// <summary>
    /// Creates a keyboard input for a signed byte value with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the input.</param>
    /// <param name="v">Reference to the value.</param>
    /// <param name="step">Step value for +/- buttons. Use 0 to hide buttons.</param>
    /// <param name="stepFast">Fast step value when holding Ctrl.</param>
    /// <param name="format">Printf format string for display (e.g., "%d").</param>
    /// <param name="flags">Input text behavior flags.</param>
    /// <returns>True if the value changed.</returns>
    public static bool Input(ReadOnlySpan<byte> label, ref sbyte v, sbyte step = 0, sbyte stepFast = 0, ReadOnlySpan<byte> format = default, InputTextFlags flags = InputTextFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (sbyte* vPtr = &v)
        {
            return ImGui_InputScalarEx(labelPtr, Native.ImGuiDataType.S8, vPtr, &step, &stepFast, formatPtr, (Native.ImGuiInputTextFlags)flags);
        }
    }

    /// <summary>
    /// Creates a keyboard input for a byte value with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the input.</param>
    /// <param name="v">Reference to the value.</param>
    /// <param name="step">Step value for +/- buttons. Use 0 to hide buttons.</param>
    /// <param name="stepFast">Fast step value when holding Ctrl.</param>
    /// <param name="format">Printf format string for display (e.g., "%u").</param>
    /// <param name="flags">Input text behavior flags.</param>
    /// <returns>True if the value changed.</returns>
    public static bool Input(ReadOnlySpan<byte> label, ref byte v, byte step = 0, byte stepFast = 0, ReadOnlySpan<byte> format = default, InputTextFlags flags = InputTextFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (byte* vPtr = &v)
        {
            return ImGui_InputScalarEx(labelPtr, Native.ImGuiDataType.U8, vPtr, &step, &stepFast, formatPtr, (Native.ImGuiInputTextFlags)flags);
        }
    }

    /// <summary>
    /// Creates a keyboard input for a short value with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the input.</param>
    /// <param name="v">Reference to the value.</param>
    /// <param name="step">Step value for +/- buttons. Use 0 to hide buttons.</param>
    /// <param name="stepFast">Fast step value when holding Ctrl.</param>
    /// <param name="format">Printf format string for display (e.g., "%d").</param>
    /// <param name="flags">Input text behavior flags.</param>
    /// <returns>True if the value changed.</returns>
    public static bool Input(ReadOnlySpan<byte> label, ref short v, short step = 0, short stepFast = 0, ReadOnlySpan<byte> format = default, InputTextFlags flags = InputTextFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (short* vPtr = &v)
        {
            return ImGui_InputScalarEx(labelPtr, Native.ImGuiDataType.S16, vPtr, &step, &stepFast, formatPtr, (Native.ImGuiInputTextFlags)flags);
        }
    }

    /// <summary>
    /// Creates a keyboard input for an unsigned short value with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the input.</param>
    /// <param name="v">Reference to the value.</param>
    /// <param name="step">Step value for +/- buttons. Use 0 to hide buttons.</param>
    /// <param name="stepFast">Fast step value when holding Ctrl.</param>
    /// <param name="format">Printf format string for display (e.g., "%u").</param>
    /// <param name="flags">Input text behavior flags.</param>
    /// <returns>True if the value changed.</returns>
    public static bool Input(ReadOnlySpan<byte> label, ref ushort v, ushort step = 0, ushort stepFast = 0, ReadOnlySpan<byte> format = default, InputTextFlags flags = InputTextFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (ushort* vPtr = &v)
        {
            return ImGui_InputScalarEx(labelPtr, Native.ImGuiDataType.U16, vPtr, &step, &stepFast, formatPtr, (Native.ImGuiInputTextFlags)flags);
        }
    }

    /// <summary>
    /// Creates a keyboard input for an unsigned int value with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the input.</param>
    /// <param name="v">Reference to the value.</param>
    /// <param name="step">Step value for +/- buttons. Use 0 to hide buttons.</param>
    /// <param name="stepFast">Fast step value when holding Ctrl.</param>
    /// <param name="format">Printf format string for display (e.g., "%u").</param>
    /// <param name="flags">Input text behavior flags.</param>
    /// <returns>True if the value changed.</returns>
    public static bool Input(ReadOnlySpan<byte> label, ref uint v, uint step = 0, uint stepFast = 0, ReadOnlySpan<byte> format = default, InputTextFlags flags = InputTextFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (uint* vPtr = &v)
        {
            return ImGui_InputScalarEx(labelPtr, Native.ImGuiDataType.U32, vPtr, &step, &stepFast, formatPtr, (Native.ImGuiInputTextFlags)flags);
        }
    }

    /// <summary>
    /// Creates a keyboard input for a long value with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the input.</param>
    /// <param name="v">Reference to the value.</param>
    /// <param name="step">Step value for +/- buttons. Use 0 to hide buttons.</param>
    /// <param name="stepFast">Fast step value when holding Ctrl.</param>
    /// <param name="format">Printf format string for display (e.g., "%lld").</param>
    /// <param name="flags">Input text behavior flags.</param>
    /// <returns>True if the value changed.</returns>
    public static bool Input(ReadOnlySpan<byte> label, ref long v, long step = 0, long stepFast = 0, ReadOnlySpan<byte> format = default, InputTextFlags flags = InputTextFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (long* vPtr = &v)
        {
            return ImGui_InputScalarEx(labelPtr, Native.ImGuiDataType.S64, vPtr, &step, &stepFast, formatPtr, (Native.ImGuiInputTextFlags)flags);
        }
    }

    /// <summary>
    /// Creates a keyboard input for an unsigned long value with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the input.</param>
    /// <param name="v">Reference to the value.</param>
    /// <param name="step">Step value for +/- buttons. Use 0 to hide buttons.</param>
    /// <param name="stepFast">Fast step value when holding Ctrl.</param>
    /// <param name="format">Printf format string for display (e.g., "%llu").</param>
    /// <param name="flags">Input text behavior flags.</param>
    /// <returns>True if the value changed.</returns>
    public static bool Input(ReadOnlySpan<byte> label, ref ulong v, ulong step = 0, ulong stepFast = 0, ReadOnlySpan<byte> format = default, InputTextFlags flags = InputTextFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (ulong* vPtr = &v)
        {
            return ImGui_InputScalarEx(labelPtr, Native.ImGuiDataType.U64, vPtr, &step, &stepFast, formatPtr, (Native.ImGuiInputTextFlags)flags);
        }
    }

    /// <summary>
    /// Creates a keyboard input for an array of int values with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the input.</param>
    /// <param name="v">Reference to the array of values.</param>
    /// <param name="step">Step value for +/- buttons. Use 0 to hide buttons.</param>
    /// <param name="stepFast">Fast step value when holding Ctrl.</param>
    /// <param name="flags">Input text behavior flags.</param>
    /// <returns>True if any value changed.</returns>
    public static bool Input(ReadOnlySpan<byte> label, Span<int> v, int step = 0, int stepFast = 0, InputTextFlags flags = InputTextFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (int* vPtr = v)
        {
            return v.Length switch
            {
                2 => ImGui_InputInt2(labelPtr, vPtr, (Native.ImGuiInputTextFlags)flags),
                3 => ImGui_InputInt3(labelPtr, vPtr, (Native.ImGuiInputTextFlags)flags),
                4 => ImGui_InputInt4(labelPtr, vPtr, (Native.ImGuiInputTextFlags)flags),
                _ => ImGui_InputScalarNEx(labelPtr, Native.ImGuiDataType.S32, vPtr, v.Length, &step, &stepFast, null, (Native.ImGuiInputTextFlags)flags),
            };
        }
    }

    /// <summary>
    /// Creates a keyboard input for an array of float values with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the input.</param>
    /// <param name="v">Reference to the array of values.</param>
    /// <param name="step">Step value for +/- buttons. Use 0 to hide buttons.</param>
    /// <param name="stepFast">Fast step value when holding Ctrl.</param>
    /// <param name="format">Printf format string for display (e.g., "%.3f").</param>
    /// <param name="flags">Input text behavior flags.</param>
    /// <returns>True if any value changed.</returns>
    public static bool Input(ReadOnlySpan<byte> label, Span<float> v, float step = 0.0f, float stepFast = 0.0f, ReadOnlySpan<byte> format = default, InputTextFlags flags = InputTextFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (float* vPtr = v)
        {
            return v.Length switch
            {
                2 => ImGui_InputFloat2Ex(labelPtr, vPtr, formatPtr, (Native.ImGuiInputTextFlags)flags),
                3 => ImGui_InputFloat3Ex(labelPtr, vPtr, formatPtr, (Native.ImGuiInputTextFlags)flags),
                4 => ImGui_InputFloat4Ex(labelPtr, vPtr, formatPtr, (Native.ImGuiInputTextFlags)flags),
                _ => ImGui_InputScalarNEx(labelPtr, Native.ImGuiDataType.Float, vPtr, v.Length, &step, &stepFast, formatPtr, (Native.ImGuiInputTextFlags)flags),
            };
        }
    }

    /// <summary>
    /// Creates a keyboard input for an array of signed byte values with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the input.</param>
    /// <param name="v">Reference to the array of values.</param>
    /// <param name="step">Step value for +/- buttons. Use 0 to hide buttons.</param>
    /// <param name="stepFast">Fast step value when holding Ctrl.</param>
    /// <param name="format">Printf format string for display (e.g., "%d").</param>
    /// <param name="flags">Input text behavior flags.</param>
    /// <returns>True if any value changed.</returns>
    public static bool Input(ReadOnlySpan<byte> label, Span<sbyte> v, sbyte step = 0, sbyte stepFast = 0, ReadOnlySpan<byte> format = default, InputTextFlags flags = InputTextFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (sbyte* vPtr = v)
        {
            return ImGui_InputScalarNEx(labelPtr, Native.ImGuiDataType.S8, vPtr, v.Length, &step, &stepFast, formatPtr, (Native.ImGuiInputTextFlags)flags);
        }
    }

    /// <summary>
    /// Creates a keyboard input for an array of byte values with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the input.</param>
    /// <param name="v">Reference to the array of values.</param>
    /// <param name="step">Step value for +/- buttons. Use 0 to hide buttons.</param>
    /// <param name="stepFast">Fast step value when holding Ctrl.</param>
    /// <param name="format">Printf format string for display (e.g., "%u").</param>
    /// <param name="flags">Input text behavior flags.</param>
    /// <returns>True if any value changed.</returns>
    public static bool Input(ReadOnlySpan<byte> label, Span<byte> v, byte step = 0, byte stepFast = 0, ReadOnlySpan<byte> format = default, InputTextFlags flags = InputTextFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (byte* vPtr = v)
        {
            return ImGui_InputScalarNEx(labelPtr, Native.ImGuiDataType.U8, vPtr, v.Length, &step, &stepFast, formatPtr, (Native.ImGuiInputTextFlags)flags);
        }
    }

    /// <summary>
    /// Creates a keyboard input for an array of short values with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the input.</param>
    /// <param name="v">Reference to the array of values.</param>
    /// <param name="step">Step value for +/- buttons. Use 0 to hide buttons.</param>
    /// <param name="stepFast">Fast step value when holding Ctrl.</param>
    /// <param name="format">Printf format string for display (e.g., "%d").</param>
    /// <param name="flags">Input text behavior flags.</param>
    /// <returns>True if any value changed.</returns>
    public static bool Input(ReadOnlySpan<byte> label, Span<short> v, short step = 0, short stepFast = 0, ReadOnlySpan<byte> format = default, InputTextFlags flags = InputTextFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (short* vPtr = v)
        {
            return ImGui_InputScalarNEx(labelPtr, Native.ImGuiDataType.S16, vPtr, v.Length, &step, &stepFast, formatPtr, (Native.ImGuiInputTextFlags)flags);
        }
    }

    /// <summary>
    /// Creates a keyboard input for an array of unsigned short values with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the input.</param>
    /// <param name="v">Reference to the array of values.</param>
    /// <param name="step">Step value for +/- buttons. Use 0 to hide buttons.</param>
    /// <param name="stepFast">Fast step value when holding Ctrl.</param>
    /// <param name="format">Printf format string for display (e.g., "%u").</param>
    /// <param name="flags">Input text behavior flags.</param>
    /// <returns>True if any value changed.</returns>
    public static bool Input(ReadOnlySpan<byte> label, Span<ushort> v, ushort step = 0, ushort stepFast = 0, ReadOnlySpan<byte> format = default, InputTextFlags flags = InputTextFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (ushort* vPtr = v)
        {
            return ImGui_InputScalarNEx(labelPtr, Native.ImGuiDataType.U16, vPtr, v.Length, &step, &stepFast, formatPtr, (Native.ImGuiInputTextFlags)flags);
        }
    }

    /// <summary>
    /// Creates a keyboard input for an array of unsigned int values with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the input.</param>
    /// <param name="v">Reference to the array of values.</param>
    /// <param name="step">Step value for +/- buttons. Use 0 to hide buttons.</param>
    /// <param name="stepFast">Fast step value when holding Ctrl.</param>
    /// <param name="format">Printf format string for display (e.g., "%u").</param>
    /// <param name="flags">Input text behavior flags.</param>
    /// <returns>True if any value changed.</returns>
    public static bool Input(ReadOnlySpan<byte> label, Span<uint> v, uint step = 0, uint stepFast = 0, ReadOnlySpan<byte> format = default, InputTextFlags flags = InputTextFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (uint* vPtr = v)
        {
            return ImGui_InputScalarNEx(labelPtr, Native.ImGuiDataType.U32, vPtr, v.Length, &step, &stepFast, formatPtr, (Native.ImGuiInputTextFlags)flags);
        }
    }

    /// <summary>
    /// Creates a keyboard input for an array of long values with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the input.</param>
    /// <param name="v">Reference to the array of values.</param>
    /// <param name="step">Step value for +/- buttons. Use 0 to hide buttons.</param>
    /// <param name="stepFast">Fast step value when holding Ctrl.</param>
    /// <param name="format">Printf format string for display (e.g., "%lld").</param>
    /// <param name="flags">Input text behavior flags.</param>
    /// <returns>True if any value changed.</returns>
    public static bool Input(ReadOnlySpan<byte> label, Span<long> v, long step = 0, long stepFast = 0, ReadOnlySpan<byte> format = default, InputTextFlags flags = InputTextFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (long* vPtr = v)
        {
            return ImGui_InputScalarNEx(labelPtr, Native.ImGuiDataType.S64, vPtr, v.Length, &step, &stepFast, formatPtr, (Native.ImGuiInputTextFlags)flags);
        }
    }

    /// <summary>
    /// Creates a keyboard input for an array of unsigned long values with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the input.</param>
    /// <param name="v">Reference to the array of values.</param>
    /// <param name="step">Step value for +/- buttons. Use 0 to hide buttons.</param>
    /// <param name="stepFast">Fast step value when holding Ctrl.</param>
    /// <param name="format">Printf format string for display (e.g., "%llu").</param>
    /// <param name="flags">Input text behavior flags.</param>
    /// <returns>True if any value changed.</returns>
    public static bool Input(ReadOnlySpan<byte> label, Span<ulong> v, ulong step = 0, ulong stepFast = 0, ReadOnlySpan<byte> format = default, InputTextFlags flags = InputTextFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (ulong* vPtr = v)
        {
            return ImGui_InputScalarNEx(labelPtr, Native.ImGuiDataType.U64, vPtr, v.Length, &step, &stepFast, formatPtr, (Native.ImGuiInputTextFlags)flags);
        }
    }

    /// <summary>
    /// Creates a keyboard input for an array of double values with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the input.</param>
    /// <param name="v">Reference to the array of values.</param>
    /// <param name="step">Step value for +/- buttons. Use 0 to hide buttons.</param>
    /// <param name="stepFast">Fast step value when holding Ctrl.</param>
    /// <param name="format">Printf format string for display (e.g., "%.6f").</param>
    /// <param name="flags">Input text behavior flags.</param>
    /// <returns>True if any value changed.</returns>
    public static bool Input(ReadOnlySpan<byte> label, Span<double> v, double step = 0.0, double stepFast = 0.0, ReadOnlySpan<byte> format = default, InputTextFlags flags = InputTextFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (double* vPtr = v)
        {
            return ImGui_InputScalarNEx(labelPtr, Native.ImGuiDataType.Double, vPtr, v.Length, &step, &stepFast, formatPtr, (Native.ImGuiInputTextFlags)flags);
        }
    }

    /// <summary>
    /// Creates a single-line text input field.
    /// </summary>
    /// <param name="label">The label for the input.</param>
    /// <param name="buf">The buffer to hold the text. Must be null-terminated.</param>
    /// <param name="flags">Input text behavior flags.</param>
    /// <returns>True if the text was modified.</returns>
    public static bool InputText(ReadOnlySpan<byte> label, Span<byte> buf, InputTextFlags flags = InputTextFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* bufPtr = buf)
        {
            return ImGui_InputText(labelPtr, bufPtr, (nuint)buf.Length, (Native.ImGuiInputTextFlags)flags);
        }
    }

    /// <summary>
    /// Creates a multi-line text input field.
    /// </summary>
    /// <param name="label">The label for the input.</param>
    /// <param name="buf">The buffer to hold the text. Must be null-terminated.</param>
    /// <param name="size">The size of the input area. Use (0,0) for default size.</param>
    /// <param name="flags">Input text behavior flags.</param>
    /// <returns>True if the text was modified.</returns>
    public static bool InputTextMultiline(ReadOnlySpan<byte> label, Span<byte> buf, Size size = default, InputTextFlags flags = InputTextFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* bufPtr = buf)
        {
            return ImGui_InputTextMultilineEx(labelPtr, bufPtr, (nuint)buf.Length, size.Value, (Native.ImGuiInputTextFlags)flags, null, null);
        }
    }

    /// <summary>
    /// Creates a single-line text input field with a hint displayed when empty.
    /// </summary>
    /// <param name="label">The label for the input.</param>
    /// <param name="hint">The hint text displayed when the input is empty.</param>
    /// <param name="buf">The buffer to hold the text. Must be null-terminated.</param>
    /// <param name="flags">Input text behavior flags.</param>
    /// <returns>True if the text was modified.</returns>
    public static bool InputTextWithHint(ReadOnlySpan<byte> label, ReadOnlySpan<byte> hint, Span<byte> buf, InputTextFlags flags = InputTextFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* hintPtr = hint)
        fixed (byte* bufPtr = buf)
        {
            return ImGui_InputTextWithHint(labelPtr, hintPtr, bufPtr, (nuint)buf.Length, (Native.ImGuiInputTextFlags)flags);
        }
    }

    /// <summary>
    /// Creates a color editor for an RGB or RGBA color value.
    /// </summary>
    /// <param name="label">The label for the color editor.</param>
    /// <param name="col">Reference to the color values (3 floats for RGB, 4 floats for RGBA).</param>
    /// <param name="flags">Color edit behavior flags.</param>
    /// <returns>True if the color was modified.</returns>
    /// <remarks>
    /// The color editor displays a small color preview square that can be clicked to open a picker,
    /// and right-clicked to open an options menu.
    /// </remarks>
    /// <exception cref="ArgumentException">Thrown when the color array length is not 3 or 4.</exception>
    public static bool ColorEdit(ReadOnlySpan<byte> label, Span<float> col, ColorEditFlags flags = ColorEditFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (float* colPtr = col)
        {
            return col.Length switch
            {
                3 => ImGui_ColorEdit3(labelPtr, colPtr, (Native.ImGuiColorEditFlags)flags),
                4 => ImGui_ColorEdit4(labelPtr, colPtr, (Native.ImGuiColorEditFlags)flags),
                _ => throw new ArgumentException("Color array must have 3 (RGB) or 4 (RGBA) elements.", nameof(col))
            };
        }
    }

    /// <summary>
    /// Creates a color picker for an RGB or RGBA color value.
    /// </summary>
    /// <param name="label">The label for the color picker.</param>
    /// <param name="col">Reference to the color values (3 floats for RGB, 4 floats for RGBA).</param>
    /// <param name="flags">Color edit behavior flags.</param>
    /// <param name="refCol">Optional reference color to display for comparison (RGBA only). Pass default for no reference.</param>
    /// <returns>True if the color was modified.</returns>
    /// <remarks>
    /// The color picker displays a full color selection interface with a hue bar/wheel and saturation/value selector.
    /// When a reference color is provided (RGBA only), it is displayed alongside the current color for comparison.
    /// </remarks>
    /// <exception cref="ArgumentException">Thrown when the color array length is not 3 or 4.</exception>
    public static bool ColorPicker(ReadOnlySpan<byte> label, Span<float> col, ColorEditFlags flags = ColorEditFlags.None, Span<float> refCol = default)
    {
        fixed (byte* labelPtr = label)
        fixed (float* colPtr = col)
        fixed (float* refColPtr = refCol)
        {
            return col.Length switch
            {
                3 => ImGui_ColorPicker3(labelPtr, colPtr, (Native.ImGuiColorEditFlags)flags),
                4 => ImGui_ColorPicker4(labelPtr, colPtr, (Native.ImGuiColorEditFlags)flags, refColPtr),
                _ => throw new ArgumentException("Color array must have 3 (RGB) or 4 (RGBA) elements.", nameof(col))
            };
        }
    }

    /// <summary>
    /// Displays a color button that opens a color picker when clicked.
    /// </summary>
    /// <param name="descId">Description ID for the button.</param>
    /// <param name="color">The color to display.</param>
    /// <param name="flags">Color edit behavior flags.</param>
    /// <returns>True when clicked.</returns>
    public static bool ColorButton(ReadOnlySpan<byte> descId, Color color, ColorEditFlags flags = ColorEditFlags.None)
    {
        fixed (byte* ptr = descId)
        {
            return ImGui_ColorButton(ptr, color.Value, (Native.ImGuiColorEditFlags)flags);
        }
    }

    /// <summary>
    /// Displays a color button with explicit size that opens a color picker when clicked.
    /// </summary>
    /// <param name="descId">Description ID for the button.</param>
    /// <param name="color">The color to display.</param>
    /// <param name="flags">Color edit behavior flags.</param>
    /// <param name="size">The button size.</param>
    /// <returns>True when clicked.</returns>
    public static bool ColorButton(ReadOnlySpan<byte> descId, Color color, ColorEditFlags flags, Vec2 size)
    {
        fixed (byte* ptr = descId)
        {
            return ImGui_ColorButtonEx(ptr, color.Value, (Native.ImGuiColorEditFlags)flags, size.Value);
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
    /// Pushes a new tree node onto the stack using the specified identifier.
    /// </summary>
    /// <remarks>This method is typically used to create a new scope in a tree structure, such as when
    /// building hierarchical UI elements. Each call to this method should be paired with a corresponding call to the
    /// method that pops the tree node to maintain stack balance.</remarks>
    /// <param name="strId">A read-only span of bytes representing the identifier for the tree node. The identifier must remain valid for
    /// the duration of the call.</param>
    public static void TreePush(ReadOnlySpan<byte> strId)
    {
        fixed (byte* ptr = strId)
        {
            ImGui_TreePush(ptr);
        }
    }

    /// <summary>
    /// Pushes a new tree node onto the stack using the specified identifier. Subsequent ImGui items will be considered
    /// children of this node until a matching TreePop is called.
    /// </summary>
    /// <remarks>Call TreePop to close the tree node opened by this method. TreePush and TreePop must be used
    /// in pairs to maintain a balanced tree structure. This method is typically used when building custom tree widgets
    /// or hierarchical UI elements in ImGui.</remarks>
    /// <param name="id">The identifier for the tree node. This value must uniquely identify the node within the current tree hierarchy.</param>
    public static void TreePush(nint id)
    {
        ImGui_TreePushPtr((void*)id);
    }

    /// <summary>
    /// Pops the last tree node from the stack.
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
    public static bool CollapsingHeader(ReadOnlySpan<byte> label, ref bool pVisible, TreeNodeFlags flags = TreeNodeFlags.None)
    {
        fixed (byte* ptr = label)
        fixed (bool* pVisiblePtr = &pVisible)
        {
            return ImGui_CollapsingHeaderBoolPtr(ptr, pVisiblePtr, (Native.ImGuiTreeNodeFlags)flags);
        }
    }

    /// <summary>
    /// Sets the next TreeNode/CollapsingHeader open state.
    /// </summary>
    /// <param name="isOpen">Whether the node should be open.</param>
    /// <param name="cond">Condition for applying the state.</param>
    public static void SetNextItemOpen(bool isOpen, Condition cond = Condition.None)
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

    /// <summary>
    /// Creates a selectable item with explicit parameters.
    /// </summary>
    /// <param name="label">The item label.</param>
    /// <param name="selected">Whether the item is currently selected (read-only).</param>
    /// <param name="flags">Selectable behavior flags.</param>
    /// <param name="size">The item size.</param>
    /// <returns>True when clicked.</returns>
    public static bool Selectable(ReadOnlySpan<byte> label, bool selected, SelectableFlags flags = SelectableFlags.None, Size size = default)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_SelectableEx(ptr, selected, (Native.ImGuiSelectableFlags)flags, size.Value);
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
    public static bool Selectable(ReadOnlySpan<byte> label, ref bool pSelected, SelectableFlags flags = SelectableFlags.None, Size size = default)
    {
        fixed (byte* ptr = label)
        fixed (bool* pSelectedPtr = &pSelected)
        {
            return ImGui_SelectableBoolPtrEx(ptr, pSelectedPtr, (Native.ImGuiSelectableFlags)flags, size.Value);
        }
    }

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
    /// Creates a menu item with explicit parameters.
    /// </summary>
    /// <param name="label">The item label.</param>
    /// <param name="shortcut">Optional shortcut text displayed on the right.</param>
    /// <param name="selected">Whether to show a check mark.</param>
    /// <param name="enabled">Whether the item is enabled.</param>
    /// <returns>True when activated.</returns>
    public static bool MenuItem(ReadOnlySpan<byte> label)
    {
        fixed (byte* labelPtr = label)
        {
            return ImGui_MenuItem(labelPtr);
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
    public static bool MenuItem(ReadOnlySpan<byte> label, ReadOnlySpan<byte> shortcut, ref bool pSelected, bool enabled = true)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* shortcutPtr = shortcut)
        fixed (bool* pSelectedPtr = &pSelected)
        {
            return ImGui_MenuItemBoolPtr(labelPtr, shortcutPtr, pSelectedPtr, enabled);
        }
    }

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
    /// Sets a text-only tooltip for the preceding item if it was hovered.
    /// </summary>
    /// <param name="text">The tooltip text.</param>
    /// <remarks>
    /// Shortcut for: if (IsItemHovered(ImGuiHoveredFlags_ForTooltip)) { SetTooltip(...); }.
    /// </remarks>
    public static void SetItemTooltip(ReadOnlySpan<byte> text)
    {
        fixed (byte* ptr = text)
        {
            ImGui_SetItemTooltip(ptr);
        }
    }
}