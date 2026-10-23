using System.Collections.Concurrent;
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
/// Accessors for the current context's <c>ImGuiPlatformIO</c> struct.
/// </summary>
public static unsafe class PlatformIO
{
    private static IGSharp_PlatformIO* CurrentHandle { get { Context.Current.CheckCurrent(); return IGSharp_GetPlatformIO(); } }

    private static readonly ConcurrentDictionary<nint, HandlerState> Handlers = new();

    private static IDisposable RegisterOverride(string kind, Action restore)
    {
        var context = Context.Current; var state = GetCurrentState();
        if (!state.Overrides.TryGetValue(kind, out var stack)) state.Overrides[kind] = stack = [];
        var token = new object(); stack.Push(token);
        var lifetime = new Lifetime(0, context, context.Lifetime, () => { restore(); stack.Pop(); });
        state.Registrations.Add(lifetime);
        return new CallbackOverride(context, lifetime, () => ReferenceEquals(stack.Peek(), token));
    }
    private sealed class CallbackOverride(Context context, Lifetime lifetime, Func<bool> isTop) : IDisposable
    {
        public void Dispose()
        {
            if (!lifetime.Alive) return;
            context.CheckAccess();
            if (!isTop()) throw new InvalidOperationException("Dispose callback overrides in reverse registration order.");
            using var scope = context.Enter(); lifetime.Dispose();
        }
    }
    /// <summary>Platform session date in ImGui's YYYYMMDD integer encoding; zero means unspecified.</summary>
    public static int SessionDate { get => IGSharp_PlatformIO_GetPlatformSessionDate(CurrentHandle); set => IGSharp_PlatformIO_SetPlatformSessionDate(CurrentHandle, value); }

    // --- Renderer capabilities ---

    /// <summary>Maximum texture width supported by the renderer backend (0 = no limit communicated).</summary>
    public static int RendererTextureMaxWidth
    {
        get => IGSharp_PlatformIO_GetRendererTextureMaxWidth(CurrentHandle);
        set => IGSharp_PlatformIO_SetRendererTextureMaxWidth(CurrentHandle, value);
    }

    /// <summary>Maximum texture height supported by the renderer backend (0 = no limit communicated).</summary>
    public static int RendererTextureMaxHeight
    {
        get => IGSharp_PlatformIO_GetRendererTextureMaxHeight(CurrentHandle);
        set => IGSharp_PlatformIO_SetRendererTextureMaxHeight(CurrentHandle, value);
    }

    // --- Locale ---

    /// <summary>
    /// Decimal point character used by numeric input widgets ('.' by default). Set from the
    /// user locale (e.g. ',') so InputFloat/InputDouble parse localized numbers.
    /// </summary>
    public static char LocaleDecimalPoint
    {
        get => (char)IGSharp_PlatformIO_GetPlatformLocaleDecimalPoint(CurrentHandle);
        set => IGSharp_PlatformIO_SetPlatformLocaleDecimalPoint(CurrentHandle, value);
    }

    // --- Textures (renderer-owned list) ---

    /// <summary>Number of textures in the renderer texture list (owned by ImGui / the renderer backend).</summary>
    public static int TexturesCount => IGSharp_PlatformIO_GetTexturesCount(CurrentHandle);

    /// <summary>Gets a texture from the platform IO textures list (backend-managed; view is frame-transient).</summary>
    public static TextureData GetTexture(int index) { ArgumentOutOfRangeException.ThrowIfNegative(index); ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, TexturesCount); return new(IGSharp_PlatformIO_GetTexture(CurrentHandle, index)); }

    // --- Handler lifetime ---

    /// <summary>
    /// Clears all platform handlers (clipboard, open-in-shell, IME) and their user data,
    /// including any managed handlers registered through this class. Typically called by
    /// a platform backend on shutdown.
    /// </summary>
    public static void ClearPlatformHandlers()
    {
        if (Handlers.TryGetValue((nint)Context.Current.Handle, out var state))
            foreach (var lifetime in state.Registrations.AsEnumerable().Reverse()) lifetime.Dispose();
        IGSharp_PlatformIO_ClearPlatformHandlers(CurrentHandle);
        ReleaseContext((nint)IGSharp_GetCurrentContext());
    }

    /// <summary>
    /// Clears all renderer handlers (texture limits, render state). Typically called by
    /// a renderer backend on shutdown.
    /// </summary>
    public static void ClearRendererHandlers()
        => IGSharp_PlatformIO_ClearRendererHandlers(CurrentHandle);

    // --- Clipboard override ---

    /// <summary>
    /// Overrides the clipboard handlers used by ImGui (e.g. for copy/paste in text widgets).
    /// Pass null for either handler to temporarily disable it. Dispose the returned registration
    /// to restore both previous handlers and their native user data. Dispose nested registrations in reverse order.
    /// The <paramref name="getText"/> result is copied to an internal unmanaged buffer that
    /// stays valid until the next clipboard read or <see cref="ClearPlatformHandlers"/>.
    /// </summary>
    public static IDisposable SetClipboardHandlers(Func<string?>? getText, Action<string?>? setText)
    {
        var savedState = GetCurrentState();
        var oldGetClipboardText = savedState.GetClipboardText;
        var oldSetClipboardText = savedState.SetClipboardText;
        var oldPlatformGetClipboardTextFn = IGSharp_PlatformIO_GetPlatformGetClipboardTextFn(CurrentHandle);
        var oldPlatformSetClipboardTextFn = IGSharp_PlatformIO_GetPlatformSetClipboardTextFn(CurrentHandle);
        var oldPlatformClipboardUserData = IGSharp_PlatformIO_GetPlatformClipboardUserData(CurrentHandle);

        var pio = CurrentHandle;
        var state = GetCurrentState();
        state.GetClipboardText = getText;
        state.SetClipboardText = setText;

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
        return RegisterOverride("SetClipboardHandlers", () =>
        {
            savedState.GetClipboardText = oldGetClipboardText;
            savedState.SetClipboardText = oldSetClipboardText;
            IGSharp_PlatformIO_SetPlatformGetClipboardTextFn(CurrentHandle, oldPlatformGetClipboardTextFn);
            IGSharp_PlatformIO_SetPlatformSetClipboardTextFn(CurrentHandle, oldPlatformSetClipboardTextFn);
            IGSharp_PlatformIO_SetPlatformClipboardUserData(CurrentHandle, oldPlatformClipboardUserData);
        });
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static byte* GetClipboardTextThunk(IGSharp_Context* context)
    {
        if (!Handlers.TryGetValue((nint)context, out var state)) return null;
        try
        {
            using var guard = Context.Current.EnterCallback();
            var text = state.GetClipboardText?.Invoke();
            state.FreeClipboardTextBuffer();
            if (text == null) return null;
            state.ClipboardTextBuffer = Marshal.StringToCoTaskMemUTF8(text);
            return (byte*)state.ClipboardTextBuffer;
        }
        catch (Exception ex)
        {
            ImGui.ReportUnhandledCallbackException(ex);
            return null;
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void SetClipboardTextThunk(IGSharp_Context* context, byte* text)
    {
        if (!Handlers.TryGetValue((nint)context, out var state)) return;
        try { using var guard = Context.Current.EnterCallback(); state.SetClipboardText?.Invoke(Marshal.PtrToStringUTF8((nint)text)); }
        catch (Exception ex) { ImGui.ReportUnhandledCallbackException(ex); }
    }

    internal static void ReleaseContext(nint context)
    {
        if (Handlers.TryRemove(context, out var state))
            state.FreeClipboardTextBuffer();
    }

    private static HandlerState GetCurrentState()
    {
        var context = (nint)IGSharp_GetCurrentContext();
        if (context == 0)
            throw new InvalidOperationException("No current ImGui context.");
        return Handlers.GetOrAdd(context, static _ => new HandlerState());
    }

    private sealed class HandlerState
    {
        public readonly List<Lifetime> Registrations = [];
        public readonly Dictionary<string, Stack<object>> Overrides = [];
        public Func<string?>? GetClipboardText;
        public Action<string?>? SetClipboardText;
        public Func<string, bool>? OpenInShell;
        public ImeDataHandler? ImeDataHandler;
        public IntPtr ClipboardTextBuffer;

        public void FreeClipboardTextBuffer()
        {
            if (ClipboardTextBuffer == IntPtr.Zero) return;
            Marshal.FreeCoTaskMem(ClipboardTextBuffer);
            ClipboardTextBuffer = IntPtr.Zero;
        }
    }

    // --- Open-in-shell override ---

    /// <summary>
    /// Overrides the handler ImGui invokes to open a URL or file path in the OS shell
    /// (e.g. links in text). Return true from the handler on success. Dispose the returned registration to restore the previous handler and native user data.
    /// </summary>
    public static IDisposable SetOpenInShellHandler(Func<string, bool>? handler)
    {
        var savedState = GetCurrentState();
        var oldOpenInShell = savedState.OpenInShell;
        var oldPlatformOpenInShellFn = IGSharp_PlatformIO_GetPlatformOpenInShellFn(CurrentHandle);
        var oldPlatformOpenInShellUserData = IGSharp_PlatformIO_GetPlatformOpenInShellUserData(CurrentHandle);

        GetCurrentState().OpenInShell = handler;
        delegate* unmanaged[Cdecl]<IGSharp_Context*, byte*, byte> fn = null;
        if (handler != null)
        {
            fn = &OpenInShellThunk;
        }
        IGSharp_PlatformIO_SetPlatformOpenInShellFn(CurrentHandle, fn);
        return RegisterOverride("SetOpenInShellHandler", () =>
        {
            savedState.OpenInShell = oldOpenInShell;
            IGSharp_PlatformIO_SetPlatformOpenInShellFn(CurrentHandle, oldPlatformOpenInShellFn);
            IGSharp_PlatformIO_SetPlatformOpenInShellUserData(CurrentHandle, oldPlatformOpenInShellUserData);
        });
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static byte OpenInShellThunk(IGSharp_Context* context, byte* path)
    {
        if (!Handlers.TryGetValue((nint)context, out var state) || state.OpenInShell == null) return 0;
        try { using var guard = Context.Current.EnterCallback(); return state.OpenInShell(Marshal.PtrToStringUTF8((nint)path) ?? string.Empty) ? (byte)1 : (byte)0; }
        catch (Exception ex)
        {
            ImGui.ReportUnhandledCallbackException(ex);
            return 0;
        }
    }

    // --- IME override ---

    /// <summary>
    /// Overrides the handler ImGui invokes to notify the platform of IME (input method)
    /// placement. Dispose the returned registration to restore the previous handler and native user data.
    /// </summary>
    public static IDisposable SetImeDataHandler(ImeDataHandler? handler)
    {
        var savedState = GetCurrentState();
        var oldImeDataHandler = savedState.ImeDataHandler;
        var oldPlatformSetImeDataFn = IGSharp_PlatformIO_GetPlatformSetImeDataFn(CurrentHandle);
        var oldPlatformImeUserData = IGSharp_PlatformIO_GetPlatformImeUserData(CurrentHandle);

        GetCurrentState().ImeDataHandler = handler;
        delegate* unmanaged[Cdecl]<IGSharp_Context*, IGSharp_Viewport*, IGSharp_PlatformImeData*, void> fn = null;
        if (handler != null)
        {
            fn = &SetImeDataThunk;
        }
        IGSharp_PlatformIO_SetPlatformSetImeDataFn(CurrentHandle, fn);
        return RegisterOverride("SetImeDataHandler", () =>
        {
            savedState.ImeDataHandler = oldImeDataHandler;
            IGSharp_PlatformIO_SetPlatformSetImeDataFn(CurrentHandle, oldPlatformSetImeDataFn);
            IGSharp_PlatformIO_SetPlatformImeUserData(CurrentHandle, oldPlatformImeUserData);
        });
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void SetImeDataThunk(IGSharp_Context* context, IGSharp_Viewport* viewport, IGSharp_PlatformImeData* data)
    {
        if (!Handlers.TryGetValue((nint)context, out var state) || state.ImeDataHandler == null || data == null) return;
        try
        {
            var imeData = new PlatformImeData(
                data->WantVisible,
                data->WantTextInput,
                new Vec2(data->InputPos.X, data->InputPos.Y),
                data->InputLineHeight,
                data->ViewportId);
            using var guard = Context.Current.EnterCallback();
            state.ImeDataHandler(new Viewport(viewport), in imeData);
        }
        catch (Exception ex) { ImGui.ReportUnhandledCallbackException(ex); }
    }
}
