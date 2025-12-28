using System.Runtime.InteropServices;
using System.Text;
using static Sdl3Sharp.ImGui.Native.ImGui;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Provides high-level wrapper methods for Dear ImGui functionality.
/// </summary>
public static unsafe class ImGui
{
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

    public static ReadOnlySpan<byte> GetStyleColorName(StyleColor color)
    {
        return MemoryMarshal.CreateReadOnlySpanFromNullTerminated(ImGui_GetStyleColorName((Native.ImGuiCol)color));
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