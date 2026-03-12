using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace SdlSharp.Native;

// SDL3's "main callbacks" model (SDL_AppInit_func, SDL_AppIterate_func, SDL_AppEvent_func,
// SDL_AppQuit_func, SDL_EnterAppMainCallbacks, SDL_AppResult) is not wrapped. That model has
// SDL own the main loop and call back into the app, which is designed for platforms where the
// app doesn't control main() (mobile, Emscripten). In C#, the runtime already owns main() and
// the traditional SDL_Init → poll events → SDL_Quit model (wrapped via Application) is the
// natural fit. Wrapping app callbacks would mean fighting both the C# runtime and SDL over
// main loop ownership for little practical benefit.

/// <summary>
/// Initialization flags for SDL_Init and/or SDL_InitSubSystem.
/// </summary>
[Flags]
public enum SDL_InitFlags : uint
{
    /// <summary>Audio subsystem (implies Events).</summary>
    SDL_INIT_AUDIO = 0x00000010u,

    /// <summary>Video subsystem (implies Events, should be initialized on the main thread).</summary>
    SDL_INIT_VIDEO = 0x00000020u,

    /// <summary>Joystick subsystem (implies Events).</summary>
    SDL_INIT_JOYSTICK = 0x00000200u,

    /// <summary>Haptic (force feedback) subsystem.</summary>
    SDL_INIT_HAPTIC = 0x00001000u,

    /// <summary>Gamepad subsystem (implies Joystick).</summary>
    SDL_INIT_GAMEPAD = 0x00002000u,

    /// <summary>Events subsystem.</summary>
    SDL_INIT_EVENTS = 0x00004000u,

    /// <summary>Sensor subsystem (implies Events).</summary>
    SDL_INIT_SENSOR = 0x00008000u,

    /// <summary>Camera subsystem (implies Events).</summary>
    SDL_INIT_CAMERA = 0x00010000u,
}

/// <summary>
/// Native bindings for SDL_init.h — initialization and shutdown.
/// </summary>
[SuppressMessage("Interoperability", "CA1401:P/Invokes should not be visible")]
public static partial class Init
{
    /// <summary>
    /// Initialize the SDL library.
    /// </summary>
    /// <param name="flags">Subsystem initialization flags.</param>
    /// <returns>true on success or false on failure.</returns>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_Init")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_Init(SDL_InitFlags flags);

    /// <summary>
    /// Compatibility function to initialize the SDL library.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_InitSubSystem")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_InitSubSystem(SDL_InitFlags flags);

    /// <summary>
    /// Shut down specific SDL subsystems.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_QuitSubSystem")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_QuitSubSystem(SDL_InitFlags flags);

    /// <summary>
    /// Get a mask of the specified subsystems which are currently initialized.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_WasInit")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_InitFlags SDL_WasInit(SDL_InitFlags flags);

    /// <summary>
    /// Clean up all initialized subsystems.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_Quit")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_Quit();

    /// <summary>
    /// Return whether this is the main thread.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_IsMainThread")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_IsMainThread();

    /// <summary>
    /// Call a function on the main thread during event processing.
    /// </summary>
    /// <param name="callback">The callback to call on the main thread.</param>
    /// <param name="userdata">A pointer that is passed to the callback.</param>
    /// <param name="wait_complete">true to wait for the callback to complete, false to return immediately.</param>
    /// <returns>true on success or false on failure.</returns>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_RunOnMainThread")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_RunOnMainThread(
        delegate* unmanaged[Cdecl]<void*, void> callback,
        void* userdata,
        [MarshalAs(UnmanagedType.U1)] bool wait_complete);

    /// <summary>
    /// Specify basic metadata about your app.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetAppMetadata")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetAppMetadata(ReadOnlySpan<byte> appname, ReadOnlySpan<byte> appversion, ReadOnlySpan<byte> appidentifier);

    /// <summary>
    /// Specify metadata about your app through a set of properties.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetAppMetadataProperty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetAppMetadataProperty(ReadOnlySpan<byte> name, ReadOnlySpan<byte> value);

    /// <summary>
    /// Get metadata about your app.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetAppMetadataProperty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* SDL_GetAppMetadataProperty(ReadOnlySpan<byte> name);

    /// <summary>App metadata property: name.</summary>
    public static ReadOnlySpan<byte> SDL_PROP_APP_METADATA_NAME_STRING => "SDL.app.metadata.name"u8;

    /// <summary>App metadata property: version.</summary>
    public static ReadOnlySpan<byte> SDL_PROP_APP_METADATA_VERSION_STRING => "SDL.app.metadata.version"u8;

    /// <summary>App metadata property: identifier (reverse-domain format).</summary>
    public static ReadOnlySpan<byte> SDL_PROP_APP_METADATA_IDENTIFIER_STRING => "SDL.app.metadata.identifier"u8;

    /// <summary>App metadata property: creator.</summary>
    public static ReadOnlySpan<byte> SDL_PROP_APP_METADATA_CREATOR_STRING => "SDL.app.metadata.creator"u8;

    /// <summary>App metadata property: copyright.</summary>
    public static ReadOnlySpan<byte> SDL_PROP_APP_METADATA_COPYRIGHT_STRING => "SDL.app.metadata.copyright"u8;

    /// <summary>App metadata property: URL.</summary>
    public static ReadOnlySpan<byte> SDL_PROP_APP_METADATA_URL_STRING => "SDL.app.metadata.url"u8;

    /// <summary>App metadata property: type ("game", "mediaplayer", "application").</summary>
    public static ReadOnlySpan<byte> SDL_PROP_APP_METADATA_TYPE_STRING => "SDL.app.metadata.type"u8;
}
