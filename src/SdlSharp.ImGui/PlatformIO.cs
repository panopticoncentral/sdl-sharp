using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static SdlSharp.ImGui.Native;

namespace SdlSharp.ImGui;

/// <summary>
/// IME placement data passed to a <see cref="ImeDataHandler"/> — where and whether the
/// platform should show its input-method (IME / on-screen keyboard) UI.
/// </summary>
/// <param name="WantVisible">True when a text-input widget is active and the IME UI should be visible.</param>
/// <param name="WantTextInput">True when a text-input widget is active (platform should enable text input).</param>
/// <param name="InputPos">Position of the input cursor (bottom-left corner), in screen coordinates.</param>
/// <param name="InputLineHeight">Height of the line where the input cursor is.</param>
/// <param name="ViewportId">Id of the viewport the text input is happening in.</param>
public readonly record struct PlatformImeData(
    bool WantVisible,
    bool WantTextInput,
    Vec2 InputPos,
    float InputLineHeight,
    uint ViewportId);

/// <summary>
/// Delegate invoked when ImGui wants the platform to reposition / show / hide its IME UI.
/// </summary>
public delegate void ImeDataHandler(Viewport viewport, in PlatformImeData data);

/// <summary>
/// Accessors for the global ImGui <c>ImGuiPlatformIO</c> struct — optional platform/renderer
/// handler overrides (clipboard, open-in-shell, IME) and renderer capabilities.
/// Handler registration is global (per-process): the handlers apply to the current ImGui
/// context and any context that shares this platform IO.
/// </summary>
public static unsafe class PlatformIO
{
    // Managed handler storage. Registration is static/global: the native callbacks
    // receive the ImGui context pointer, but since these handlers are process-global
    // in this wrapper we ignore it for lookup.
    private static Func<string?>? _getClipboardText;
    private static Action<string?>? _setClipboardText;
    private static Func<string, bool>? _openInShell;
    private static ImeDataHandler? _imeDataHandler;

    // Persistent unmanaged UTF-8 buffer for the clipboard get-handler. ImGui consumes the
    // returned pointer after the callback returns, so it must outlive the call; we keep it
    // alive until the next get-call (which reallocates it) or until handlers are cleared.
    private static IntPtr _clipboardTextBuffer;

    // --- Renderer capabilities ---

    /// <summary>Maximum texture width supported by the renderer backend (0 = no limit communicated).</summary>
    public static int RendererTextureMaxWidth
    {
        get => IGSharp_PlatformIO_GetRendererTextureMaxWidth(IGSharp_GetPlatformIO());
        set => IGSharp_PlatformIO_SetRendererTextureMaxWidth(IGSharp_GetPlatformIO(), value);
    }

    /// <summary>Maximum texture height supported by the renderer backend (0 = no limit communicated).</summary>
    public static int RendererTextureMaxHeight
    {
        get => IGSharp_PlatformIO_GetRendererTextureMaxHeight(IGSharp_GetPlatformIO());
        set => IGSharp_PlatformIO_SetRendererTextureMaxHeight(IGSharp_GetPlatformIO(), value);
    }

    // --- Locale ---

    /// <summary>
    /// Decimal point character used by numeric input widgets ('.' by default). Set from the
    /// user locale (e.g. ',') so InputFloat/InputDouble parse localized numbers.
    /// </summary>
    public static char LocaleDecimalPoint
    {
        get => (char)IGSharp_PlatformIO_GetPlatformLocaleDecimalPoint(IGSharp_GetPlatformIO());
        set => IGSharp_PlatformIO_SetPlatformLocaleDecimalPoint(IGSharp_GetPlatformIO(), value);
    }

    // --- Textures (renderer-owned list) ---

    /// <summary>Number of textures in the renderer texture list (owned by ImGui / the renderer backend).</summary>
    public static int TexturesCount => IGSharp_PlatformIO_GetTexturesCount(IGSharp_GetPlatformIO());

    /// <summary>Gets a texture from the platform IO textures list (backend-managed; view is frame-transient).</summary>
    public static TextureData GetTexture(int index) => new(IGSharp_PlatformIO_GetTexture(IGSharp_GetPlatformIO(), index));

    // --- Handler lifetime ---

    /// <summary>
    /// Clears all platform handlers (clipboard, open-in-shell, IME) and their user data,
    /// including any managed handlers registered through this class. Typically called by
    /// a platform backend on shutdown.
    /// </summary>
    public static void ClearPlatformHandlers()
    {
        IGSharp_PlatformIO_ClearPlatformHandlers(IGSharp_GetPlatformIO());
        _getClipboardText = null;
        _setClipboardText = null;
        _openInShell = null;
        _imeDataHandler = null;
        FreeClipboardTextBuffer();
    }

    /// <summary>
    /// Clears all renderer handlers (texture limits, render state). Typically called by
    /// a renderer backend on shutdown.
    /// </summary>
    public static void ClearRendererHandlers()
        => IGSharp_PlatformIO_ClearRendererHandlers(IGSharp_GetPlatformIO());

    // --- Clipboard override ---

    /// <summary>
    /// Overrides the clipboard handlers used by ImGui (e.g. for copy/paste in text widgets).
    /// Pass null for either handler to remove that override (leaving no handler installed —
    /// reinstall a backend such as <see cref="ImGuiBackend"/> to restore its default).
    /// The <paramref name="getText"/> result is copied to an internal unmanaged buffer that
    /// stays valid until the next clipboard read or <see cref="ClearPlatformHandlers"/>.
    /// </summary>
    public static void SetClipboardHandlers(Func<string?>? getText, Action<string?>? setText)
    {
        var pio = IGSharp_GetPlatformIO();
        _getClipboardText = getText;
        _setClipboardText = setText;

        delegate* unmanaged[Cdecl]<IGSharp_Context*, byte*> getFn = null;
        if (getText != null)
        {
            getFn = &GetClipboardTextThunk;
        }
        IGSharp_PlatformIO_SetPlatformGetClipboardTextFn(pio, getFn);

        delegate* unmanaged[Cdecl]<IGSharp_Context*, byte*, void> setFn = null;
        if (setText != null)
        {
            setFn = &SetClipboardTextThunk;
        }
        IGSharp_PlatformIO_SetPlatformSetClipboardTextFn(pio, setFn);
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static byte* GetClipboardTextThunk(IGSharp_Context* context)
    {
        var text = _getClipboardText?.Invoke();

        // ImGui uses the returned pointer after the callback returns, so hand back a
        // persistent unmanaged copy; the previous copy is released on each new request.
        FreeClipboardTextBuffer();
        if (text == null)
        {
            return null;
        }
        _clipboardTextBuffer = Marshal.StringToCoTaskMemUTF8(text);
        return (byte*)_clipboardTextBuffer;
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void SetClipboardTextThunk(IGSharp_Context* context, byte* text)
        => _setClipboardText?.Invoke(Marshal.PtrToStringUTF8((nint)text));

    private static void FreeClipboardTextBuffer()
    {
        if (_clipboardTextBuffer != IntPtr.Zero)
        {
            Marshal.FreeCoTaskMem(_clipboardTextBuffer);
            _clipboardTextBuffer = IntPtr.Zero;
        }
    }

    // --- Open-in-shell override ---

    /// <summary>
    /// Overrides the handler ImGui invokes to open a URL or file path in the OS shell
    /// (e.g. links in text). Return true from the handler on success. Pass null to remove
    /// the override (leaving no handler installed).
    /// </summary>
    public static void SetOpenInShellHandler(Func<string, bool>? handler)
    {
        _openInShell = handler;
        delegate* unmanaged[Cdecl]<IGSharp_Context*, byte*, byte> fn = null;
        if (handler != null)
        {
            fn = &OpenInShellThunk;
        }
        IGSharp_PlatformIO_SetPlatformOpenInShellFn(IGSharp_GetPlatformIO(), fn);
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static byte OpenInShellThunk(IGSharp_Context* context, byte* path)
    {
        var handler = _openInShell;
        if (handler == null)
        {
            return 0;
        }
        return handler(Marshal.PtrToStringUTF8((nint)path) ?? string.Empty) ? (byte)1 : (byte)0;
    }

    // --- IME override ---

    /// <summary>
    /// Overrides the handler ImGui invokes to notify the platform of IME (input method)
    /// placement — called when a text widget becomes active or the cursor moves. Pass null
    /// to remove the override (leaving no handler installed).
    /// </summary>
    public static void SetImeDataHandler(ImeDataHandler? handler)
    {
        _imeDataHandler = handler;
        delegate* unmanaged[Cdecl]<IGSharp_Context*, IGSharp_Viewport*, IGSharp_PlatformImeData*, void> fn = null;
        if (handler != null)
        {
            fn = &SetImeDataThunk;
        }
        IGSharp_PlatformIO_SetPlatformSetImeDataFn(IGSharp_GetPlatformIO(), fn);
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void SetImeDataThunk(IGSharp_Context* context, IGSharp_Viewport* viewport, IGSharp_PlatformImeData* data)
    {
        var handler = _imeDataHandler;
        if (handler == null || data == null)
        {
            return;
        }
        var imeData = new PlatformImeData(
            data->WantVisible,
            data->WantTextInput,
            new Vec2(data->InputPos.X, data->InputPos.Y),
            data->InputLineHeight,
            data->ViewportId);
        handler(new Viewport(viewport), in imeData);
    }
}
