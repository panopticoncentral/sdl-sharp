using System.Runtime.InteropServices;

using SdlSharp.Input;
using SdlSharp.Native;
using static SdlSharp.Native.Common;
using static SdlSharp.Native.Keyboard;
using static SdlSharp.Native.Video;

namespace SdlSharp.Graphics;

/// <summary>
/// A managed wrapper around an SDL window (SDL_Window).
/// </summary>
public sealed unsafe class Window : IDisposable
{
    private readonly bool _ownsHandle;

    /// <summary>
    /// The underlying native SDL_Window pointer.
    /// </summary>
    internal SDL_Window* Handle
    {
        get
        {
            ObjectDisposedException.ThrowIf(_handle == null, this);
            return _handle;
        }
    }

    private SDL_Window* _handle;

    internal Window(SDL_Window* handle, bool ownsHandle = true)
    {
        _handle = handle;
        _ownsHandle = ownsHandle;
    }

    /// <summary>
    /// A special position value indicating the window position is undefined.
    /// </summary>
    public const int UndefinedPosition = SDL_WINDOWPOS_UNDEFINED;

    /// <summary>
    /// A special position value indicating the window should be centered.
    /// </summary>
    public const int CenteredPosition = SDL_WINDOWPOS_CENTERED;

    /// <summary>
    /// Creates a new window with the specified title, dimensions, and flags.
    /// </summary>
    /// <param name="title">The title of the window.</param>
    /// <param name="w">The width of the window in screen coordinates.</param>
    /// <param name="h">The height of the window in screen coordinates.</param>
    /// <param name="flags">Window creation flags.</param>
    /// <returns>A new window.</returns>
    public static Window Create(string title, int w, int h, WindowFlags flags = 0) =>
        new(Check(SDL_CreateWindow(ToUtf8(title), w, h, (SDL_WindowFlags)flags)));

    /// <summary>
    /// Creates a new window with the specified properties.
    /// </summary>
    /// <param name="properties">The properties to create the window with.</param>
    /// <returns>A new window.</returns>
    public static Window Create(PropertyGroup properties) =>
        new(Check(SDL_CreateWindowWithProperties(properties.Id)));

    /// <summary>
    /// Gets a window from a stored ID.
    /// </summary>
    /// <param name="id">The ID of the window.</param>
    /// <returns>A non-owning window wrapper, or <c>null</c> if no window with that ID exists.</returns>
    public static Window? FromId(uint id)
    {
        var ptr = SDL_GetWindowFromID(new SDL_WindowID(id));
        return ptr == null ? null : new Window(ptr, ownsHandle: false);
    }

    /// <summary>
    /// Gets the number of video drivers compiled into SDL.
    /// </summary>
    public static int NumVideoDrivers => SDL_GetNumVideoDrivers();

    /// <summary>
    /// Gets the name of a built-in video driver.
    /// </summary>
    /// <param name="index">The index of the video driver.</param>
    /// <returns>The name of the video driver.</returns>
    public static string? GetVideoDriver(int index) =>
        Marshal.PtrToStringUTF8((nint)SDL_GetVideoDriver(index));

    /// <summary>
    /// Gets the name of the currently initialized video driver.
    /// </summary>
    public static string? CurrentVideoDriver =>
        Marshal.PtrToStringUTF8((nint)SDL_GetCurrentVideoDriver());

    /// <summary>
    /// Gets the numeric ID of this window.
    /// </summary>
    public uint Id => CheckId(SDL_GetWindowID(Handle).Value);

    /// <summary>
    /// Gets the properties associated with this window.
    /// </summary>
    public PropertyGroup Properties =>
        new(CheckId(SDL_GetWindowProperties(Handle)), ownsHandle: false);

    /// <summary>
    /// Gets the current window flags.
    /// </summary>
    public WindowFlags Flags => (WindowFlags)SDL_GetWindowFlags(Handle);

    /// <summary>
    /// Gets or sets the title of the window.
    /// </summary>
    public string? Title
    {
        get => Marshal.PtrToStringUTF8((nint)SDL_GetWindowTitle(Handle));
        set => Check(SDL_SetWindowTitle(Handle, ToUtf8(value)));
    }

    /// <summary>
    /// Gets or sets the position of the window.
    /// </summary>
    public Point Position
    {
        get
        {
            Check(SDL_GetWindowPosition(Handle, out var x, out var y));
            return new Point(x, y);
        }
        set => Check(SDL_SetWindowPosition(Handle, value.X, value.Y));
    }

    /// <summary>
    /// Gets or sets the size of the window's client area.
    /// </summary>
    public Size Size
    {
        get
        {
            Check(SDL_GetWindowSize(Handle, out var w, out var h));
            return new Size(w, h);
        }
        set => Check(SDL_SetWindowSize(Handle, value.W, value.H));
    }

    /// <summary>
    /// Gets the size of the window's client area in pixels.
    /// </summary>
    public Size SizeInPixels
    {
        get
        {
            Check(SDL_GetWindowSizeInPixels(Handle, out var w, out var h));
            return new Size(w, h);
        }
    }

    /// <summary>
    /// Gets or sets the minimum size of the window's client area.
    /// </summary>
    public Size MinimumSize
    {
        get
        {
            Check(SDL_GetWindowMinimumSize(Handle, out var w, out var h));
            return new Size(w, h);
        }
        set => Check(SDL_SetWindowMinimumSize(Handle, value.W, value.H));
    }

    /// <summary>
    /// Gets or sets the maximum size of the window's client area.
    /// </summary>
    public Size MaximumSize
    {
        get
        {
            Check(SDL_GetWindowMaximumSize(Handle, out var w, out var h));
            return new Size(w, h);
        }
        set => Check(SDL_SetWindowMaximumSize(Handle, value.W, value.H));
    }

    /// <summary>
    /// Gets or sets the opacity of the window (0.0f transparent, 1.0f opaque).
    /// </summary>
    public float Opacity
    {
        get => SDL_GetWindowOpacity(Handle);
        set => Check(SDL_SetWindowOpacity(Handle, value));
    }

    /// <summary>
    /// Gets the pixel density of the window.
    /// </summary>
    public float PixelDensity => SDL_GetWindowPixelDensity(Handle);

    /// <summary>
    /// Gets the content display scale relative to the window's pixel size.
    /// </summary>
    public float DisplayScale => SDL_GetWindowDisplayScale(Handle);

    /// <summary>
    /// Shows the window.
    /// </summary>
    public void Show() => Check(SDL_ShowWindow(Handle));

    /// <summary>
    /// Hides the window.
    /// </summary>
    public void Hide() => Check(SDL_HideWindow(Handle));

    /// <summary>
    /// Raises the window above other windows and requests input focus.
    /// </summary>
    public void Raise() => Check(SDL_RaiseWindow(Handle));

    /// <summary>
    /// Maximizes the window.
    /// </summary>
    public void Maximize() => Check(SDL_MaximizeWindow(Handle));

    /// <summary>
    /// Minimizes the window to an iconic representation.
    /// </summary>
    public void Minimize() => Check(SDL_MinimizeWindow(Handle));

    /// <summary>
    /// Restores the window from minimized or maximized state.
    /// </summary>
    public void Restore() => Check(SDL_RestoreWindow(Handle));

    /// <summary>
    /// Sets the fullscreen state of the window.
    /// </summary>
    /// <param name="fullscreen">true for fullscreen, false for windowed.</param>
    public void SetFullscreen(bool fullscreen) => Check(SDL_SetWindowFullscreen(Handle, fullscreen));

    /// <summary>
    /// Sets whether the window has a border.
    /// </summary>
    /// <param name="bordered">true for bordered, false for borderless.</param>
    public void SetBordered(bool bordered) => Check(SDL_SetWindowBordered(Handle, bordered));

    /// <summary>
    /// Sets whether the window is resizable.
    /// </summary>
    /// <param name="resizable">true for resizable, false for fixed size.</param>
    public void SetResizable(bool resizable) => Check(SDL_SetWindowResizable(Handle, resizable));

    /// <summary>
    /// Sets the icon for the window.
    /// </summary>
    /// <param name="icon">The surface to use as the icon.</param>
    public void SetIcon(Surface icon) => Check(SDL_SetWindowIcon(Handle, icon.Handle));

    /// <summary>
    /// Requests the window to flash to get attention.
    /// </summary>
    /// <param name="operation">The flash operation to perform.</param>
    public void Flash(FlashOperation operation) => Check(SDL_FlashWindow(Handle, (SDL_FlashOperation)operation));

    /// <summary>
    /// Starts accepting Unicode text input events in this window. Shows the
    /// on-screen keyboard where applicable.
    /// </summary>
    public void StartTextInput() => Check(SDL_StartTextInput(Handle));

    /// <summary>
    /// Starts accepting Unicode text input events in this window, with hints
    /// describing the kind of text being entered.
    /// </summary>
    /// <param name="properties">The text input configuration. Unset fields use SDL's defaults.</param>
    public void StartTextInput(in TextInputProperties properties)
    {
        using var props = new PropertyGroup();
        if (properties.Type is { } type)
            props.SetNumber(Native.Keyboard.SDL_PROP_TEXTINPUT_TYPE_NUMBER, (long)type);
        if (properties.Capitalization is { } capitalization)
            props.SetNumber(Native.Keyboard.SDL_PROP_TEXTINPUT_CAPITALIZATION_NUMBER, (long)capitalization);
        if (properties.Autocorrect is { } autocorrect)
            props.SetBoolean(Native.Keyboard.SDL_PROP_TEXTINPUT_AUTOCORRECT_BOOLEAN, autocorrect);
        if (properties.Multiline is { } multiline)
            props.SetBoolean(Native.Keyboard.SDL_PROP_TEXTINPUT_MULTILINE_BOOLEAN, multiline);
        Check(SDL_StartTextInputWithProperties(Handle, props.Id));
    }

    /// <summary>
    /// Stops accepting text input events in this window.
    /// </summary>
    public void StopTextInput() => Check(SDL_StopTextInput(Handle));

    /// <summary>
    /// Gets whether text input is active in this window.
    /// </summary>
    public bool IsTextInputActive => SDL_TextInputActive(Handle);

    /// <summary>
    /// Sets the area used for typing in this window, informing the IME where to
    /// position candidate/composition windows.
    /// </summary>
    /// <param name="area">The text input area, in window coordinates.</param>
    /// <param name="cursorOffset">The cursor X offset relative to the area's left edge.</param>
    public void SetTextInputArea(Rectangle area, int cursorOffset = 0)
    {
        var native = area.ToNative();
        Check(SDL_SetTextInputArea(Handle, &native, cursorOffset));
    }

    /// <summary>
    /// Clears the area used for typing in this window, letting the IME
    /// position candidate/composition windows wherever it chooses.
    /// </summary>
    public void ClearTextInputArea() => Check(SDL_SetTextInputArea(Handle, null, 0));

    /// <summary>
    /// Gets the area used for typing in this window.
    /// </summary>
    /// <param name="cursorOffset">Receives the cursor X offset relative to the area's left edge.</param>
    /// <returns>The text input area, in window coordinates.</returns>
    public Rectangle GetTextInputArea(out int cursorOffset)
    {
        SDL_Rect rect;
        int cursor;
        Check(SDL_GetTextInputArea(Handle, &rect, &cursor));
        cursorOffset = cursor;
        return Rectangle.FromNative(rect);
    }

    /// <summary>
    /// Dismisses the composition window and clears any pending IME composition state.
    /// </summary>
    public void ClearComposition() => Check(SDL_ClearComposition(Handle));

    /// <summary>
    /// Gets the SDL surface associated with the window for software rendering.
    /// The returned surface is owned by the window and must not be disposed.
    /// </summary>
    /// <returns>A non-owning surface for the window.</returns>
    public Surface GetSurface() =>
        new(Check(SDL_GetWindowSurface(Handle)), ownsHandle: false);

    /// <summary>
    /// Copies the window surface to the screen.
    /// </summary>
    public void UpdateSurface() => Check(SDL_UpdateWindowSurface(Handle));

    /// <summary>
    /// Destroys the surface associated with the window.
    /// </summary>
    public void DestroyWindowSurface() => Check(SDL_DestroyWindowSurface(Handle));

    /// <summary>
    /// Gets the display associated with this window.
    /// </summary>
    /// <returns>The display containing this window.</returns>
    public Display GetDisplay()
    {
        var id = SDL_GetDisplayForWindow(Handle);
        return new Display(CheckId(id.Value));
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_ownsHandle && _handle != null)
        {
            SDL_DestroyWindow(_handle);
        }
        _handle = null;
    }
}
