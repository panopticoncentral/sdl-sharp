using System.Runtime.CompilerServices;
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

    // --- Position constants ---

    /// <summary>
    /// A special position value indicating the window position is undefined.
    /// </summary>
    public const int UndefinedPosition = SDL_WINDOWPOS_UNDEFINED;

    /// <summary>
    /// A special position value indicating the window should be centered.
    /// </summary>
    public const int CenteredPosition = SDL_WINDOWPOS_CENTERED;

    // --- Creation / lookup ---

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
    /// Gets all currently valid windows, as non-owning wrappers.
    /// </summary>
    /// <returns>An array of windows.</returns>
    public static Window[] GetWindows()
    {
        var ptr = SDL_GetWindows(out var count);
        if (ptr == null)
        {
            throw new SdlException();
        }

        try
        {
            var windows = new Window[count];
            for (var i = 0; i < count; i++)
            {
                windows[i] = new Window(ptr[i], ownsHandle: false);
            }

            return windows;
        }
        finally
        {
            SDL_free(ptr);
        }
    }

    /// <summary>
    /// Gets the window that currently has an input grab (mouse or keyboard), as a
    /// non-owning wrapper.
    /// </summary>
    public static Window? GrabbedWindow
    {
        get
        {
            var ptr = SDL_GetGrabbedWindow();
            return ptr == null ? null : new Window(ptr, ownsHandle: false);
        }
    }

    // --- Video drivers ---

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

    // --- Identity / flags / properties ---

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

    // --- Title / position / size / opacity ---

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

    // --- Window state ---

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

    // --- Appearance ---

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

    // --- Text input ---

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

    // --- Window surface ---

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
    /// Copies areas of the window surface to the screen.
    /// </summary>
    /// <param name="rects">The areas to copy, in window coordinates.</param>
    public unsafe void UpdateSurfaceRects(ReadOnlySpan<Rectangle> rects)
    {
        Span<SDL_Rect> native = stackalloc SDL_Rect[rects.Length];
        for (var i = 0; i < rects.Length; i++)
        {
            native[i] = rects[i].ToNative();
        }

        fixed (SDL_Rect* ptr = native)
        {
            Check(SDL_UpdateWindowSurfaceRects(Handle, ptr, rects.Length));
        }
    }

    /// <summary>
    /// Destroys the surface associated with the window.
    /// </summary>
    public void DestroyWindowSurface() => Check(SDL_DestroyWindowSurface(Handle));

    /// <summary>
    /// Gets whether the window has an associated SDL surface for software rendering.
    /// </summary>
    public bool HasSurface => SDL_WindowHasSurface(Handle);

    /// <summary>
    /// Gets or sets the vertical refresh sync interval for the window surface. 0
    /// disables vsync (the default), 1 synchronizes with every vertical refresh,
    /// 2 with every second vertical refresh, etc.; -1 requests adaptive vsync
    /// (late swap tearing), which is not supported by every driver and will throw
    /// if unsupported. Not every value is supported by every driver.
    /// </summary>
    public int SurfaceVSyncInterval
    {
        get
        {
            Check(SDL_GetWindowSurfaceVSync(Handle, out var vsync));
            return vsync;
        }
        set => Check(SDL_SetWindowSurfaceVSync(Handle, value));
    }

    // --- Display / fullscreen mode ---

    /// <summary>
    /// Gets the display associated with this window.
    /// </summary>
    /// <returns>The display containing this window.</returns>
    public Display GetDisplay()
    {
        var id = SDL_GetDisplayForWindow(Handle);
        return new Display(CheckId(id.Value));
    }

    /// <summary>
    /// Sets the display mode to use when this window is visible at fullscreen. Pass
    /// <c>null</c> for borderless fullscreen desktop mode, or a mode returned by
    /// <see cref="Display.GetFullscreenModes"/> to set an exclusive fullscreen mode.
    /// On some windowing systems this request is asynchronous; call <see cref="Sync"/>
    /// to block until the change has taken effect.
    /// </summary>
    /// <param name="mode">The display mode to use, or <c>null</c> for borderless fullscreen desktop mode.</param>
    public unsafe void SetFullscreenMode(DisplayMode? mode)
    {
        if (mode == null)
        {
            Check(SDL_SetWindowFullscreenMode(Handle, null));
            return;
        }

        var native = mode.Value.ToNative();
        Check(SDL_SetWindowFullscreenMode(Handle, &native));
    }

    /// <summary>
    /// Gets the display mode to use when this window is visible at fullscreen, or
    /// <c>null</c> for borderless fullscreen desktop mode.
    /// </summary>
    /// <returns>The exclusive fullscreen display mode, or <c>null</c>.</returns>
    public unsafe DisplayMode? GetFullscreenMode()
    {
        var ptr = SDL_GetWindowFullscreenMode(Handle);
        return ptr == null ? null : DisplayMode.FromNative(ptr);
    }

    // --- Grabs / mouse confinement ---

    /// <summary>
    /// Gets or sets whether the window's mouse input is grabbed (confined to the window).
    /// </summary>
    public bool MouseGrabbed
    {
        get => SDL_GetWindowMouseGrab(Handle);
        set => Check(SDL_SetWindowMouseGrab(Handle, value));
    }

    /// <summary>
    /// Gets or sets whether the window's keyboard input is grabbed.
    /// </summary>
    public bool KeyboardGrabbed
    {
        get => SDL_GetWindowKeyboardGrab(Handle);
        set => Check(SDL_SetWindowKeyboardGrab(Handle, value));
    }

    /// <summary>
    /// Gets or sets the rectangle, in window coordinates, that confines the cursor
    /// while it is over this window. The getter returns <c>null</c> when no
    /// confinement rectangle is set; setting <c>null</c> clears it.
    /// </summary>
    public unsafe Rectangle? MouseConfinementRect
    {
        get
        {
            var ptr = SDL_GetWindowMouseRect(Handle);
            return ptr == null ? null : Rectangle.FromNative(*ptr);
        }
        set
        {
            if (value == null)
            {
                Check(SDL_SetWindowMouseRect(Handle, null));
                return;
            }

            var native = value.Value.ToNative();
            Check(SDL_SetWindowMouseRect(Handle, &native));
        }
    }

    // --- Layout ---

    /// <summary>
    /// Sets whether the window should always be above other windows.
    /// </summary>
    /// <param name="onTop">true to keep the window above others, false otherwise.</param>
    public void SetAlwaysOnTop(bool onTop) => Check(SDL_SetWindowAlwaysOnTop(Handle, onTop));

    /// <summary>
    /// Requests that the window's aspect ratio be constrained.
    /// </summary>
    /// <param name="minAspect">The minimum aspect ratio (width / height), or 0.0f for no limit.</param>
    /// <param name="maxAspect">The maximum aspect ratio (width / height), or 0.0f for no limit.</param>
    public void SetAspectRatio(float minAspect, float maxAspect) =>
        Check(SDL_SetWindowAspectRatio(Handle, minAspect, maxAspect));

    /// <summary>
    /// Gets the window's requested aspect ratio constraints.
    /// </summary>
    /// <param name="minAspect">Receives the minimum aspect ratio, or 0.0f if unconstrained.</param>
    /// <param name="maxAspect">Receives the maximum aspect ratio, or 0.0f if unconstrained.</param>
    public void GetAspectRatio(out float minAspect, out float maxAspect) =>
        Check(SDL_GetWindowAspectRatio(Handle, out minAspect, out maxAspect));

    /// <summary>
    /// Blocks until any pending window state requested via other methods (position,
    /// size, fullscreen mode, etc.) has been finalized by the windowing system.
    /// </summary>
    public void Sync() => Check(SDL_SyncWindow(Handle));

    /// <summary>
    /// Gets the size of a window's borders (decorations) around the client area.
    /// This may fail on some platforms (e.g. when the window has not yet been made
    /// visible), throwing <see cref="SdlException"/>.
    /// </summary>
    /// <param name="top">Receives the height of the top border.</param>
    /// <param name="left">Receives the width of the left border.</param>
    /// <param name="bottom">Receives the height of the bottom border.</param>
    /// <param name="right">Receives the width of the right border.</param>
    public void GetBordersSize(out int top, out int left, out int bottom, out int right) =>
        Check(SDL_GetWindowBordersSize(Handle, out top, out left, out bottom, out right));

    // --- Popups / parenting / modality / focus ---

    /// <summary>
    /// Creates a child popup window of this window.
    /// </summary>
    /// <param name="offsetX">The x position of the popup window relative to the origin of this window.</param>
    /// <param name="offsetY">The y position of the popup window relative to the origin of this window.</param>
    /// <param name="w">The width of the popup window.</param>
    /// <param name="h">The height of the popup window.</param>
    /// <param name="flags">Window creation flags; must include <see cref="WindowFlags.Tooltip"/> or
    /// <see cref="WindowFlags.PopupMenu"/>.</param>
    /// <returns>A new owning window wrapper for the popup.</returns>
    public Window CreatePopup(int offsetX, int offsetY, int w, int h, WindowFlags flags) =>
        new(Check(SDL_CreatePopupWindow(Handle, offsetX, offsetY, w, h, (SDL_WindowFlags)flags)));

    /// <summary>
    /// Gets the parent of this window, as a non-owning wrapper, or <c>null</c> if this
    /// window has no parent.
    /// </summary>
    public Window? Parent
    {
        get
        {
            var ptr = SDL_GetWindowParent(Handle);
            return ptr == null ? null : new Window(ptr, ownsHandle: false);
        }
    }

    /// <summary>
    /// Sets the parent of this window. Pass <c>null</c> to clear the parent, making
    /// this a toplevel window.
    /// </summary>
    /// <param name="parent">The new parent window, or <c>null</c>.</param>
    public void SetParent(Window? parent) => Check(SDL_SetWindowParent(Handle, parent?.Handle));

    /// <summary>
    /// Toggles the modal state of the window. The window must currently have a
    /// parent (see <see cref="SetParent"/>) or this call will fail.
    /// </summary>
    /// <param name="modal">true to make the window modal, false otherwise.</param>
    public void SetModal(bool modal) => Check(SDL_SetWindowModal(Handle, modal));

    /// <summary>
    /// Sets whether the window may receive keyboard focus.
    /// </summary>
    /// <param name="focusable">true if the window should accept keyboard focus.</param>
    public void SetFocusable(bool focusable) => Check(SDL_SetWindowFocusable(Handle, focusable));

    // --- System menu / shape / pixel format / safe area ---

    /// <summary>
    /// Displays the system-level window menu at the given position, in window coordinates.
    /// </summary>
    /// <param name="x">The x position, relative to the window.</param>
    /// <param name="y">The y position, relative to the window.</param>
    public void ShowSystemMenu(int x, int y) => Check(SDL_ShowWindowSystemMenu(Handle, x, y));

    /// <summary>
    /// Sets the shape of a transparent window from a surface, whose alpha channel
    /// determines the window's shape. The window must have been created with
    /// <see cref="WindowFlags.Transparent"/>. The shape is copied, so the surface may
    /// be disposed afterwards. This is an expensive operation and should be used sparingly.
    /// </summary>
    /// <param name="shape">The surface representing the shape of the window.</param>
    public void SetShape(Surface shape) => Check(SDL_SetWindowShape(Handle, shape.Handle));

    /// <summary>
    /// Gets the raw pixel format associated with the window.
    /// </summary>
    public PixelFormat PixelFormat => (PixelFormat)SDL_GetWindowPixelFormat(Handle);

    /// <summary>
    /// Gets the safe area for this window, in window coordinates, accounting for
    /// screen notches, camera cutouts, etc. that may obscure part of the window.
    /// </summary>
    public Rectangle SafeArea
    {
        get
        {
            Check(SDL_GetWindowSafeArea(Handle, out var rect));
            return Rectangle.FromNative(rect);
        }
    }

    // --- Progress ---

    /// <summary>
    /// Gets or sets the state of the window's taskbar progress bar.
    /// </summary>
    public ProgressState ProgressState
    {
        get => (ProgressState)SDL_GetWindowProgressState(Handle);
        set => Check(SDL_SetWindowProgressState(Handle, (SDL_ProgressState)value));
    }

    /// <summary>
    /// Gets or sets the value of the window's taskbar progress bar, from 0.0f to 1.0f.
    /// </summary>
    public float ProgressValue
    {
        get => SDL_GetWindowProgressValue(Handle);
        set => Check(SDL_SetWindowProgressValue(Handle, value));
    }

    // --- Hit testing ---

    /// <summary>
    /// Handles hit testing for custom window dragging and resizing.
    /// </summary>
    /// <param name="window">The window being tested.</param>
    /// <param name="area">The point being tested, in window coordinates.</param>
    /// <returns>The hit test result for the point.</returns>
    public delegate HitTestResult HitTestHandler(Window window, Point area);

    private GCHandle _hitTestHandle;

    /// <summary>
    /// Sets or clears (<c>null</c>) the hit-test callback used for custom window dragging
    /// and resizing regions. Exceptions thrown by the handler are swallowed and
    /// treated as <see cref="HitTestResult.Normal"/> (they must not cross the
    /// native boundary). The callback may fire frequently and at any time while
    /// the user interacts with the window, so the handler should be fast and
    /// avoid allocating.
    /// </summary>
    /// <param name="handler">The hit-test handler, or <c>null</c> to clear it.</param>
    public unsafe void SetHitTest(HitTestHandler? handler)
    {
        if (_hitTestHandle.IsAllocated)
        {
            _hitTestHandle.Free();
            _hitTestHandle = default;
        }

        if (handler == null)
        {
            Check(SDL_SetWindowHitTest(Handle, null, null));
            return;
        }

        var holder = new HitTestHolder(this, handler);
        _hitTestHandle = GCHandle.Alloc(holder);
        Check(SDL_SetWindowHitTest(Handle, &HitTestCallback, (void*)GCHandle.ToIntPtr(_hitTestHandle)));
    }

    private sealed record HitTestHolder(Window Window, HitTestHandler Handler);

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static SDL_HitTestResult HitTestCallback(SDL_Window* win, SDL_Point* area, void* userdata)
    {
        try
        {
            var holder = (HitTestHolder)GCHandle.FromIntPtr((nint)userdata).Target!;
            return (SDL_HitTestResult)holder.Handler(holder.Window, Point.FromNative(*area));
        }
        catch
        {
            return SDL_HitTestResult.SDL_HITTEST_NORMAL;
        }
    }

    // --- Fill document (Emscripten) ---

    /// <summary>
    /// Requests that the window fill the browser document (Emscripten only; no-op /
    /// throws on other platforms).
    /// </summary>
    /// <param name="fill">true to fill the document, false to restore the window's set size.</param>
    public void SetFillDocument(bool fill) => Check(SDL_SetWindowFillDocument(Handle, fill));

    // --- OpenGL ---

    /// <summary>
    /// Creates an OpenGL context for this window. The window must have been created with
    /// the <see cref="WindowFlags.OpenGL"/> flag.
    /// </summary>
    /// <returns>A new OpenGL context. Dispose it when no longer needed.</returns>
    public GlContext CreateGlContext() => new(Check(SDL_GL_CreateContext(Handle)));

    /// <summary>
    /// Updates this window's OpenGL surface with the rendered content. The window must have
    /// been created with the <see cref="WindowFlags.OpenGL"/> flag, and this window's OpenGL
    /// context must be current.
    /// </summary>
    public void GlSwap() => Check(SDL_GL_SwapWindow(Handle));

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_hitTestHandle.IsAllocated)
        {
            _hitTestHandle.Free();
            _hitTestHandle = default;
        }

        if (_ownsHandle && _handle != null)
        {
            SDL_DestroyWindow(_handle);
        }
        _handle = null;
    }
}
