using static Sdl3Sharp.ImGui.Native.ImGui;

namespace Sdl3Sharp.ImGui;

public unsafe static class Widgets
{
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
    public static bool Checkbox(ReadOnlySpan<byte> label, StateRef<bool> v)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_Checkbox(ptr, v.Ptr);
        }
    }

    /// <summary>
    /// Creates a checkbox widget for signed integer flags.
    /// </summary>
    /// <param name="label">The checkbox label.</param>
    /// <param name="flags">Reference to the flags value.</param>
    /// <param name="flagsValue">The flag bit(s) to toggle.</param>
    /// <returns>True when the value has been changed.</returns>
    public static bool CheckboxFlags(ReadOnlySpan<byte> label, StateRef<int> flags, int flagsValue)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_CheckboxFlagsIntPtr(ptr, flags.Ptr, flagsValue);
        }
    }

    /// <summary>
    /// Creates a checkbox widget for unsigned integer flags.
    /// </summary>
    /// <param name="label">The checkbox label.</param>
    /// <param name="flags">Reference to the flags value.</param>
    /// <param name="flagsValue">The flag bit(s) to toggle.</param>
    /// <returns>True when the value has been changed.</returns>
    public static bool CheckboxFlags(ReadOnlySpan<byte> label, StateRef<uint> flags, uint flagsValue)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_CheckboxFlagsUintPtr(ptr, flags.Ptr, flagsValue);
        }
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
    public static bool RadioButton(ReadOnlySpan<byte> label, StateRef<int> v, int vButton)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_RadioButtonIntPtr(ptr, v.Ptr, vButton);
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
    public static bool Drag(ReadOnlySpan<byte> label, StateRef<int> v, float vSpeed, int vMin = 0, int vMax = 0, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_DragIntEx(labelPtr, v.Ptr, vSpeed, vMin, vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
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
    public static bool Drag(ReadOnlySpan<byte> label, StateRef<float> v, float vSpeed, float vMin = 0.0f, float vMax = 0.0f, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_DragFloatEx(labelPtr, v.Ptr, vSpeed, vMin, vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
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
    public static bool Drag(ReadOnlySpan<byte> label, StateRef<sbyte> v, float vSpeed, sbyte vMin = 0, sbyte vMax = 0, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_DragScalarEx(labelPtr, Native.ImGuiDataType.S8, v.Ptr, vSpeed, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
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
    public static bool Drag(ReadOnlySpan<byte> label, StateRef<byte> v, float vSpeed, byte vMin = 0, byte vMax = 0, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_DragScalarEx(labelPtr, Native.ImGuiDataType.U8, v.Ptr, vSpeed, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
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
    public static bool Drag(ReadOnlySpan<byte> label, StateRef<short> v, float vSpeed, short vMin = 0, short vMax = 0, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_DragScalarEx(labelPtr, Native.ImGuiDataType.S16, v.Ptr, vSpeed, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
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
    public static bool Drag(ReadOnlySpan<byte> label, StateRef<ushort> v, float vSpeed, ushort vMin = 0, ushort vMax = 0, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_DragScalarEx(labelPtr, Native.ImGuiDataType.U16, v.Ptr, vSpeed, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
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
    public static bool Drag(ReadOnlySpan<byte> label, StateRef<uint> v, float vSpeed, uint vMin = 0, uint vMax = 0, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_DragScalarEx(labelPtr, Native.ImGuiDataType.U32, v.Ptr, vSpeed, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
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
    public static bool Drag(ReadOnlySpan<byte> label, StateRef<long> v, float vSpeed, long vMin = 0, long vMax = 0, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_DragScalarEx(labelPtr, Native.ImGuiDataType.S64, v.Ptr, vSpeed, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
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
    public static bool Drag(ReadOnlySpan<byte> label, StateRef<ulong> v, float vSpeed, ulong vMin = 0, ulong vMax = 0, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_DragScalarEx(labelPtr, Native.ImGuiDataType.U64, v.Ptr, vSpeed, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
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
    public static bool Drag(ReadOnlySpan<byte> label, StateRef<double> v, float vSpeed, double vMin = 0.0, double vMax = 0.0, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_DragScalarEx(labelPtr, Native.ImGuiDataType.Double, v.Ptr, vSpeed, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
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
    public static bool Drag(ReadOnlySpan<byte> label, StateArrayRef<int> v, float vSpeed, int vMin = 0, int vMax = 0, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return v.Length switch
            {
                2 => ImGui_DragInt2Ex(labelPtr, v.Ptr, vSpeed, vMin, vMax, formatPtr, (Native.ImGuiSliderFlags)flags),
                3 => ImGui_DragInt3Ex(labelPtr, v.Ptr, vSpeed, vMin, vMax, formatPtr, (Native.ImGuiSliderFlags)flags),
                4 => ImGui_DragInt4Ex(labelPtr, v.Ptr, vSpeed, vMin, vMax, formatPtr, (Native.ImGuiSliderFlags)flags),
                _ => ImGui_DragScalarNEx(labelPtr, Native.ImGuiDataType.S32, v.Ptr, v.Length, vSpeed, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags),
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
    public static bool Drag(ReadOnlySpan<byte> label, StateArrayRef<float> v, float vSpeed, float vMin = 0.0f, float vMax = 0.0f, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return v.Length switch
            {
                2 => ImGui_DragFloat2Ex(labelPtr, v.Ptr, vSpeed, vMin, vMax, formatPtr, (Native.ImGuiSliderFlags)flags),
                3 => ImGui_DragFloat3Ex(labelPtr, v.Ptr, vSpeed, vMin, vMax, formatPtr, (Native.ImGuiSliderFlags)flags),
                4 => ImGui_DragFloat4Ex(labelPtr, v.Ptr, vSpeed, vMin, vMax, formatPtr, (Native.ImGuiSliderFlags)flags),
                _ => ImGui_DragScalarNEx(labelPtr, Native.ImGuiDataType.Float, v.Ptr, v.Length, vSpeed, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags),
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
    public static bool Drag(ReadOnlySpan<byte> label, StateArrayRef<sbyte> v, float vSpeed, sbyte vMin = 0, sbyte vMax = 0, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_DragScalarNEx(labelPtr, Native.ImGuiDataType.S8, v.Ptr, v.Length, vSpeed, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
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
    public static bool Drag(ReadOnlySpan<byte> label, StateArrayRef<byte> v, float vSpeed, byte vMin = 0, byte vMax = 0, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_DragScalarNEx(labelPtr, Native.ImGuiDataType.U8, v.Ptr, v.Length, vSpeed, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
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
    public static bool Drag(ReadOnlySpan<byte> label, StateArrayRef<short> v, float vSpeed, short vMin = 0, short vMax = 0, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_DragScalarNEx(labelPtr, Native.ImGuiDataType.S16, v.Ptr, v.Length, vSpeed, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
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
    public static bool Drag(ReadOnlySpan<byte> label, StateArrayRef<ushort> v, float vSpeed, ushort vMin = 0, ushort vMax = 0, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_DragScalarNEx(labelPtr, Native.ImGuiDataType.U16, v.Ptr, v.Length, vSpeed, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
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
    public static bool Drag(ReadOnlySpan<byte> label, StateArrayRef<uint> v, float vSpeed, uint vMin = 0, uint vMax = 0, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_DragScalarNEx(labelPtr, Native.ImGuiDataType.U32, v.Ptr, v.Length, vSpeed, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
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
    public static bool Drag(ReadOnlySpan<byte> label, StateArrayRef<long> v, float vSpeed, long vMin = 0, long vMax = 0, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_DragScalarNEx(labelPtr, Native.ImGuiDataType.S64, v.Ptr, v.Length, vSpeed, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
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
    public static bool Drag(ReadOnlySpan<byte> label, StateArrayRef<ulong> v, float vSpeed, ulong vMin = 0, ulong vMax = 0, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_DragScalarNEx(labelPtr, Native.ImGuiDataType.U64, v.Ptr, v.Length, vSpeed, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
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
    public static bool Drag(ReadOnlySpan<byte> label, StateArrayRef<double> v, float vSpeed, double vMin = 0.0, double vMax = 0.0, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_DragScalarNEx(labelPtr, Native.ImGuiDataType.Double, v.Ptr, v.Length, vSpeed, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
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
    public static bool DragRange(ReadOnlySpan<byte> label, StateRef<float> vCurrentMin, StateRef<float> vCurrentMax, float vSpeed, float vMin = 0.0f, float vMax = 0.0f, ReadOnlySpan<byte> format = default, ReadOnlySpan<byte> formatMax = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (byte* formatMaxPtr = formatMax)
        {
            return ImGui_DragFloatRange2Ex(labelPtr, vCurrentMin.Ptr, vCurrentMax.Ptr, vSpeed, vMin, vMax, formatPtr, formatMaxPtr, (Native.ImGuiSliderFlags)flags);
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
    public static bool DragRange(ReadOnlySpan<byte> label, StateRef<int> vCurrentMin, StateRef<int> vCurrentMax, float vSpeed, int vMin = 0, int vMax = 0, ReadOnlySpan<byte> format = default, ReadOnlySpan<byte> formatMax = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (byte* formatMaxPtr = formatMax)
        {
            return ImGui_DragIntRange2Ex(labelPtr, vCurrentMin.Ptr, vCurrentMax.Ptr, vSpeed, vMin, vMax, formatPtr, formatMaxPtr, (Native.ImGuiSliderFlags)flags);
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
    public static bool Slider(ReadOnlySpan<byte> label, StateRef<int> v, int vMin, int vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_SliderIntEx(labelPtr, v.Ptr, vMin, vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
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
    public static bool Slider(ReadOnlySpan<byte> label, StateRef<float> v, float vMin, float vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_SliderFloatEx(labelPtr, v.Ptr, vMin, vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
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
    public static bool Slider(ReadOnlySpan<byte> label, StateRef<sbyte> v, sbyte vMin, sbyte vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_SliderScalarEx(labelPtr, Native.ImGuiDataType.S8, v.Ptr, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
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
    public static bool Slider(ReadOnlySpan<byte> label, StateRef<byte> v, byte vMin, byte vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_SliderScalarEx(labelPtr, Native.ImGuiDataType.U8, v.Ptr, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
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
    public static bool Slider(ReadOnlySpan<byte> label, StateRef<short> v, short vMin, short vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_SliderScalarEx(labelPtr, Native.ImGuiDataType.S16, v.Ptr, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
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
    public static bool Slider(ReadOnlySpan<byte> label, StateRef<ushort> v, ushort vMin, ushort vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_SliderScalarEx(labelPtr, Native.ImGuiDataType.U16, v.Ptr, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
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
    public static bool Slider(ReadOnlySpan<byte> label, StateRef<uint> v, uint vMin, uint vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_SliderScalarEx(labelPtr, Native.ImGuiDataType.U32, v.Ptr, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
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
    public static bool Slider(ReadOnlySpan<byte> label, StateRef<long> v, long vMin, long vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_SliderScalarEx(labelPtr, Native.ImGuiDataType.S64, v.Ptr, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
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
    public static bool Slider(ReadOnlySpan<byte> label, StateRef<ulong> v, ulong vMin, ulong vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_SliderScalarEx(labelPtr, Native.ImGuiDataType.U64, v.Ptr, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
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
    public static bool Slider(ReadOnlySpan<byte> label, StateRef<double> v, double vMin, double vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_SliderScalarEx(labelPtr, Native.ImGuiDataType.Double, v.Ptr, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
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
    public static bool Slider(ReadOnlySpan<byte> label, StateArrayRef<int> v, int vMin, int vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return v.Length switch
            {
                2 => ImGui_SliderInt2Ex(labelPtr, v.Ptr, vMin, vMax, formatPtr, (Native.ImGuiSliderFlags)flags),
                3 => ImGui_SliderInt3Ex(labelPtr, v.Ptr, vMin, vMax, formatPtr, (Native.ImGuiSliderFlags)flags),
                4 => ImGui_SliderInt4Ex(labelPtr, v.Ptr, vMin, vMax, formatPtr, (Native.ImGuiSliderFlags)flags),
                _ => ImGui_SliderScalarNEx(labelPtr, Native.ImGuiDataType.S32, v.Ptr, v.Length, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags),
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
    public static bool Slider(ReadOnlySpan<byte> label, StateArrayRef<float> v, float vMin, float vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return v.Length switch
            {
                2 => ImGui_SliderFloat2Ex(labelPtr, v.Ptr, vMin, vMax, formatPtr, (Native.ImGuiSliderFlags)flags),
                3 => ImGui_SliderFloat3Ex(labelPtr, v.Ptr, vMin, vMax, formatPtr, (Native.ImGuiSliderFlags)flags),
                4 => ImGui_SliderFloat4Ex(labelPtr, v.Ptr, vMin, vMax, formatPtr, (Native.ImGuiSliderFlags)flags),
                _ => ImGui_SliderScalarNEx(labelPtr, Native.ImGuiDataType.Float, v.Ptr, v.Length, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags),
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
    public static bool Slider(ReadOnlySpan<byte> label, StateArrayRef<sbyte> v, sbyte vMin, sbyte vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_SliderScalarNEx(labelPtr, Native.ImGuiDataType.S8, v.Ptr, v.Length, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
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
    public static bool Slider(ReadOnlySpan<byte> label, StateArrayRef<byte> v, byte vMin, byte vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_SliderScalarNEx(labelPtr, Native.ImGuiDataType.U8, v.Ptr, v.Length, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
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
    public static bool Slider(ReadOnlySpan<byte> label, StateArrayRef<short> v, short vMin, short vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_SliderScalarNEx(labelPtr, Native.ImGuiDataType.S16, v.Ptr, v.Length, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
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
    public static bool Slider(ReadOnlySpan<byte> label, StateArrayRef<ushort> v, ushort vMin, ushort vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_SliderScalarNEx(labelPtr, Native.ImGuiDataType.U16, v.Ptr, v.Length, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
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
    public static bool Slider(ReadOnlySpan<byte> label, StateArrayRef<uint> v, uint vMin, uint vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_SliderScalarNEx(labelPtr, Native.ImGuiDataType.U32, v.Ptr, v.Length, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
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
    public static bool Slider(ReadOnlySpan<byte> label, StateArrayRef<long> v, long vMin, long vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_SliderScalarNEx(labelPtr, Native.ImGuiDataType.S64, v.Ptr, v.Length, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
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
    public static bool Slider(ReadOnlySpan<byte> label, StateArrayRef<ulong> v, ulong vMin, ulong vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_SliderScalarNEx(labelPtr, Native.ImGuiDataType.U64, v.Ptr, v.Length, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
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
    public static bool Slider(ReadOnlySpan<byte> label, StateArrayRef<double> v, double vMin, double vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_SliderScalarNEx(labelPtr, Native.ImGuiDataType.Double, v.Ptr, v.Length, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
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
    public static bool SliderAngle(ReadOnlySpan<byte> label, StateRef<float> vRad, float vDegreesMin = -360.0f, float vDegreesMax = 360.0f, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_SliderAngleEx(labelPtr, vRad.Ptr, vDegreesMin, vDegreesMax, formatPtr, (Native.ImGuiSliderFlags)flags);
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
    public static bool VSlider(ReadOnlySpan<byte> label, Size size, StateRef<int> v, int vMin, int vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_VSliderIntEx(labelPtr, size.Value, v.Ptr, vMin, vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
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
    public static bool VSlider(ReadOnlySpan<byte> label, Size size, StateRef<float> v, float vMin, float vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_VSliderFloatEx(labelPtr, size.Value, v.Ptr, vMin, vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
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
    public static bool VSlider(ReadOnlySpan<byte> label, Size size, StateRef<sbyte> v, sbyte vMin, sbyte vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_VSliderScalarEx(labelPtr, size.Value, Native.ImGuiDataType.S8, v.Ptr, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
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
    public static bool VSlider(ReadOnlySpan<byte> label, Size size, StateRef<byte> v, byte vMin, byte vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_VSliderScalarEx(labelPtr, size.Value, Native.ImGuiDataType.U8, v.Ptr, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
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
    public static bool VSlider(ReadOnlySpan<byte> label, Size size, StateRef<short> v, short vMin, short vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_VSliderScalarEx(labelPtr, size.Value, Native.ImGuiDataType.S16, v.Ptr, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
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
    public static bool VSlider(ReadOnlySpan<byte> label, Size size, StateRef<ushort> v, ushort vMin, ushort vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_VSliderScalarEx(labelPtr, size.Value, Native.ImGuiDataType.U16, v.Ptr, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
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
    public static bool VSlider(ReadOnlySpan<byte> label, Size size, StateRef<uint> v, uint vMin, uint vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_VSliderScalarEx(labelPtr, size.Value, Native.ImGuiDataType.U32, v.Ptr, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
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
    public static bool VSlider(ReadOnlySpan<byte> label, Size size, StateRef<long> v, long vMin, long vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_VSliderScalarEx(labelPtr, size.Value, Native.ImGuiDataType.S64, v.Ptr, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
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
    public static bool VSlider(ReadOnlySpan<byte> label, Size size, StateRef<ulong> v, ulong vMin, ulong vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_VSliderScalarEx(labelPtr, size.Value, Native.ImGuiDataType.U64, v.Ptr, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
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
    public static bool VSlider(ReadOnlySpan<byte> label, Size size, StateRef<double> v, double vMin, double vMax, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_VSliderScalarEx(labelPtr, size.Value, Native.ImGuiDataType.Double, v.Ptr, &vMin, &vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
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
    public static bool Input(ReadOnlySpan<byte> label, StateRef<int> v, int step = 0, int stepFast = 0, InputTextFlags flags = InputTextFlags.None)
    {
        fixed (byte* labelPtr = label)
        {
            return ImGui_InputIntEx(labelPtr, v.Ptr, step, stepFast, (Native.ImGuiInputTextFlags)flags);
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
    public static bool Input(ReadOnlySpan<byte> label, StateRef<float> v, float step = 0.0f, float stepFast = 0.0f, ReadOnlySpan<byte> format = default, InputTextFlags flags = InputTextFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_InputFloatEx(labelPtr, v.Ptr, step, stepFast, formatPtr, (Native.ImGuiInputTextFlags)flags);
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
    public static bool Input(ReadOnlySpan<byte> label, StateRef<double> v, double step = 0.0, double stepFast = 0.0, ReadOnlySpan<byte> format = default, InputTextFlags flags = InputTextFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_InputDoubleEx(labelPtr, v.Ptr, step, stepFast, formatPtr, (Native.ImGuiInputTextFlags)flags);
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
    public static bool Input(ReadOnlySpan<byte> label, StateRef<sbyte> v, sbyte step = 0, sbyte stepFast = 0, ReadOnlySpan<byte> format = default, InputTextFlags flags = InputTextFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_InputScalarEx(labelPtr, Native.ImGuiDataType.S8, v.Ptr, &step, &stepFast, formatPtr, (Native.ImGuiInputTextFlags)flags);
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
    public static bool Input(ReadOnlySpan<byte> label, StateRef<byte> v, byte step = 0, byte stepFast = 0, ReadOnlySpan<byte> format = default, InputTextFlags flags = InputTextFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_InputScalarEx(labelPtr, Native.ImGuiDataType.U8, v.Ptr, &step, &stepFast, formatPtr, (Native.ImGuiInputTextFlags)flags);
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
    public static bool Input(ReadOnlySpan<byte> label, StateRef<short> v, short step = 0, short stepFast = 0, ReadOnlySpan<byte> format = default, InputTextFlags flags = InputTextFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_InputScalarEx(labelPtr, Native.ImGuiDataType.S16, v.Ptr, &step, &stepFast, formatPtr, (Native.ImGuiInputTextFlags)flags);
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
    public static bool Input(ReadOnlySpan<byte> label, StateRef<ushort> v, ushort step = 0, ushort stepFast = 0, ReadOnlySpan<byte> format = default, InputTextFlags flags = InputTextFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_InputScalarEx(labelPtr, Native.ImGuiDataType.U16, v.Ptr, &step, &stepFast, formatPtr, (Native.ImGuiInputTextFlags)flags);
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
    public static bool Input(ReadOnlySpan<byte> label, StateRef<uint> v, uint step = 0, uint stepFast = 0, ReadOnlySpan<byte> format = default, InputTextFlags flags = InputTextFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_InputScalarEx(labelPtr, Native.ImGuiDataType.U32, v.Ptr, &step, &stepFast, formatPtr, (Native.ImGuiInputTextFlags)flags);
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
    public static bool Input(ReadOnlySpan<byte> label, StateRef<long> v, long step = 0, long stepFast = 0, ReadOnlySpan<byte> format = default, InputTextFlags flags = InputTextFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_InputScalarEx(labelPtr, Native.ImGuiDataType.S64, v.Ptr, &step, &stepFast, formatPtr, (Native.ImGuiInputTextFlags)flags);
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
    public static bool Input(ReadOnlySpan<byte> label, StateRef<ulong> v, ulong step = 0, ulong stepFast = 0, ReadOnlySpan<byte> format = default, InputTextFlags flags = InputTextFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_InputScalarEx(labelPtr, Native.ImGuiDataType.U64, v.Ptr, &step, &stepFast, formatPtr, (Native.ImGuiInputTextFlags)flags);
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
    public static bool Input(ReadOnlySpan<byte> label, StateArrayRef<int> v, int step = 0, int stepFast = 0, InputTextFlags flags = InputTextFlags.None)
    {
        fixed (byte* labelPtr = label)
        {
            return v.Length switch
            {
                2 => ImGui_InputInt2(labelPtr, v.Ptr, (Native.ImGuiInputTextFlags)flags),
                3 => ImGui_InputInt3(labelPtr, v.Ptr, (Native.ImGuiInputTextFlags)flags),
                4 => ImGui_InputInt4(labelPtr, v.Ptr, (Native.ImGuiInputTextFlags)flags),
                _ => ImGui_InputScalarNEx(labelPtr, Native.ImGuiDataType.S32, v.Ptr, v.Length, &step, &stepFast, null, (Native.ImGuiInputTextFlags)flags),
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
    public static bool Input(ReadOnlySpan<byte> label, StateArrayRef<float> v, float step = 0.0f, float stepFast = 0.0f, ReadOnlySpan<byte> format = default, InputTextFlags flags = InputTextFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return v.Length switch
            {
                2 => ImGui_InputFloat2Ex(labelPtr, v.Ptr, formatPtr, (Native.ImGuiInputTextFlags)flags),
                3 => ImGui_InputFloat3Ex(labelPtr, v.Ptr, formatPtr, (Native.ImGuiInputTextFlags)flags),
                4 => ImGui_InputFloat4Ex(labelPtr, v.Ptr, formatPtr, (Native.ImGuiInputTextFlags)flags),
                _ => ImGui_InputScalarNEx(labelPtr, Native.ImGuiDataType.Float, v.Ptr, v.Length, &step, &stepFast, formatPtr, (Native.ImGuiInputTextFlags)flags),
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
    public static bool Input(ReadOnlySpan<byte> label, StateArrayRef<sbyte> v, sbyte step = 0, sbyte stepFast = 0, ReadOnlySpan<byte> format = default, InputTextFlags flags = InputTextFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_InputScalarNEx(labelPtr, Native.ImGuiDataType.S8, v.Ptr, v.Length, &step, &stepFast, formatPtr, (Native.ImGuiInputTextFlags)flags);
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
    public static bool Input(ReadOnlySpan<byte> label, StateArrayRef<byte> v, byte step = 0, byte stepFast = 0, ReadOnlySpan<byte> format = default, InputTextFlags flags = InputTextFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_InputScalarNEx(labelPtr, Native.ImGuiDataType.U8, v.Ptr, v.Length, &step, &stepFast, formatPtr, (Native.ImGuiInputTextFlags)flags);
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
    public static bool Input(ReadOnlySpan<byte> label, StateArrayRef<short> v, short step = 0, short stepFast = 0, ReadOnlySpan<byte> format = default, InputTextFlags flags = InputTextFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_InputScalarNEx(labelPtr, Native.ImGuiDataType.S16, v.Ptr, v.Length, &step, &stepFast, formatPtr, (Native.ImGuiInputTextFlags)flags);
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
    public static bool Input(ReadOnlySpan<byte> label, StateArrayRef<ushort> v, ushort step = 0, ushort stepFast = 0, ReadOnlySpan<byte> format = default, InputTextFlags flags = InputTextFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_InputScalarNEx(labelPtr, Native.ImGuiDataType.U16, v.Ptr, v.Length, &step, &stepFast, formatPtr, (Native.ImGuiInputTextFlags)flags);
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
    public static bool Input(ReadOnlySpan<byte> label, StateArrayRef<uint> v, uint step = 0, uint stepFast = 0, ReadOnlySpan<byte> format = default, InputTextFlags flags = InputTextFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_InputScalarNEx(labelPtr, Native.ImGuiDataType.U32, v.Ptr, v.Length, &step, &stepFast, formatPtr, (Native.ImGuiInputTextFlags)flags);
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
    public static bool Input(ReadOnlySpan<byte> label, StateArrayRef<long> v, long step = 0, long stepFast = 0, ReadOnlySpan<byte> format = default, InputTextFlags flags = InputTextFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_InputScalarNEx(labelPtr, Native.ImGuiDataType.S64, v.Ptr, v.Length, &step, &stepFast, formatPtr, (Native.ImGuiInputTextFlags)flags);
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
    public static bool Input(ReadOnlySpan<byte> label, StateArrayRef<ulong> v, ulong step = 0, ulong stepFast = 0, ReadOnlySpan<byte> format = default, InputTextFlags flags = InputTextFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_InputScalarNEx(labelPtr, Native.ImGuiDataType.U64, v.Ptr, v.Length, &step, &stepFast, formatPtr, (Native.ImGuiInputTextFlags)flags);
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
    public static bool Input(ReadOnlySpan<byte> label, StateArrayRef<double> v, double step = 0.0, double stepFast = 0.0, ReadOnlySpan<byte> format = default, InputTextFlags flags = InputTextFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_InputScalarNEx(labelPtr, Native.ImGuiDataType.Double, v.Ptr, v.Length, &step, &stepFast, formatPtr, (Native.ImGuiInputTextFlags)flags);
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
    public static bool ColorEdit(ReadOnlySpan<byte> label, StateArrayRef<float> col, ColorEditFlags flags = ColorEditFlags.None)
    {
        fixed (byte* labelPtr = label)
        {
            return col.Length switch
            {
                3 => ImGui_ColorEdit3(labelPtr, col.Ptr, (Native.ImGuiColorEditFlags)flags),
                4 => ImGui_ColorEdit4(labelPtr, col.Ptr, (Native.ImGuiColorEditFlags)flags),
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
    public static bool ColorPicker(ReadOnlySpan<byte> label, StateArrayRef<float> col, ColorEditFlags flags = ColorEditFlags.None, StateArrayRef<float> refCol = default)
    {
        fixed (byte* labelPtr = label)
        {
            return col.Length switch
            {
                3 => ImGui_ColorPicker3(labelPtr, col.Ptr, (Native.ImGuiColorEditFlags)flags),
                4 => ImGui_ColorPicker4(labelPtr, col.Ptr, (Native.ImGuiColorEditFlags)flags, refCol.Ptr),
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
    public static bool Selectable(ReadOnlySpan<byte> label, StateRef<bool> pSelected, SelectableFlags flags = SelectableFlags.None, Size size = default)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_SelectableBoolPtrEx(ptr, pSelected.Ptr, (Native.ImGuiSelectableFlags)flags, size.Value);
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
}