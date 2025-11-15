using System.Runtime.InteropServices;

// We are intentionally exposing the P/Invoke calls so people can do low-level calls if needed
#pragma warning disable CA1401 // P/Invokes should not be visible

namespace Sdl3Sharp.Native;

/// <summary>
/// P/Invoke bindings for SDL_init.h - SDL initialization and shutdown APIs.
/// </summary>
public static unsafe partial class SDL
{
    private const string Sdl3 = "SDL3";

    /// <summary>
    /// Initialization flags for SDL_Init and/or SDL_InitSubSystem.
    /// </summary>
    [Flags]
    public enum SDL_InitFlags : uint
    {
        /// <summary>
        /// Audio subsystem; automatically initializes the events subsystem.
        /// </summary>
        SDL_INIT_AUDIO = 0x00000010u,

        /// <summary>
        /// Video subsystem; automatically initializes the events subsystem, should be initialized on the main thread.
        /// </summary>
        SDL_INIT_VIDEO = 0x00000020u,

        /// <summary>
        /// Joystick subsystem; automatically initializes the events subsystem.
        /// </summary>
        SDL_INIT_JOYSTICK = 0x00000200u,

        /// <summary>
        /// Haptic (force feedback) subsystem.
        /// </summary>
        SDL_INIT_HAPTIC = 0x00001000u,

        /// <summary>
        /// Gamepad subsystem; automatically initializes the joystick subsystem.
        /// </summary>
        SDL_INIT_GAMEPAD = 0x00002000u,

        /// <summary>
        /// Events subsystem.
        /// </summary>
        SDL_INIT_EVENTS = 0x00004000u,

        /// <summary>
        /// Sensor subsystem; automatically initializes the events subsystem.
        /// </summary>
        SDL_INIT_SENSOR = 0x00008000u,

        /// <summary>
        /// Camera subsystem; automatically initializes the events subsystem.
        /// </summary>
        SDL_INIT_CAMERA = 0x00010000u
    }

    // SDL_AppResult doesn't apply to .NET, so we don't need to define it.
    // SDL_AppInit_func doesn't apply to .NET, so we don't need to define it.
    // SDL_AppIterate_func doesn't apply to .NET, so we don't need to define it.
    // SDL_AppQuit_func doesn't apply to .NET, so we don't need to define it.

    /// <summary>
    /// Initialize the SDL library.
    /// </summary>
    /// <param name="flags">subsystem initialization flags.</param>
    /// <returns>true on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_Init(SDL_InitFlags flags);

    /// <summary>
    /// Compatibility function to initialize the SDL library.
    /// </summary>
    /// <param name="flags">any of the flags used by SDL_Init(); see SDL_Init for details.</param>
    /// <returns>true on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_InitSubSystem(SDL_InitFlags flags);

    /// <summary>
    /// Shut down specific SDL subsystems.
    /// </summary>
    /// <param name="flags">any of the flags used by SDL_Init(); see SDL_Init for details.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial void SDL_QuitSubSystem(SDL_InitFlags flags);

    /// <summary>
    /// Get a mask of the specified subsystems which are currently initialized.
    /// </summary>
    /// <param name="flags">any of the flags used by SDL_Init(); see SDL_Init for details.</param>
    /// <returns>a mask of all initialized subsystems if flags is 0, otherwise it returns the initialization status of the specified subsystems.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial SDL_InitFlags SDL_WasInit(SDL_InitFlags flags);

    /// <summary>
    /// Clean up all initialized subsystems.
    /// </summary>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial void SDL_Quit();

    /// <summary>
    /// Return whether this is the main thread.
    /// </summary>
    /// <returns>true if this thread is the main thread, or false otherwise.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_IsMainThread();

    /// <summary>
    /// Call a function on the main thread during event processing.
    /// </summary>
    /// <param name="callback">the callback to call on the main thread.</param>
    /// <param name="userdata">an opaque value that is passed to callback.</param>
    /// <param name="wait_complete">true to wait for the callback to complete, false to return immediately.</param>
    /// <returns>true on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_RunOnMainThread(delegate* unmanaged[Cdecl]<nuint, void> callback, nuint userdata, [MarshalAs(UnmanagedType.U1)] bool wait_complete);

    /// <summary>
    /// Specify basic metadata about your app.
    /// </summary>
    /// <param name="appname">The name of the application.</param>
    /// <param name="appversion">The version of the application.</param>
    /// <param name="appidentifier">A unique string in reverse-domain format that identifies this app.</param>
    /// <returns>true on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetAppMetadata(byte* appname, byte* appversion, byte* appidentifier);

    /// <summary>
    /// Specify metadata about your app through a set of properties.
    /// </summary>
    /// <param name="name">the name of the metadata property to set.</param>
    /// <param name="value">the value of the property, or NULL to remove that property.</param>
    /// <returns>true on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetAppMetadataProperty(byte* name, byte* value);

    /// <summary>
    /// The human-readable name of the application, like "My Game 2: Bad Guy's Revenge!".
    /// </summary>
    public const string SDL_PROP_APP_METADATA_NAME_STRING = "SDL.app.metadata.name";

    /// <summary>
    /// The version of the app that is running; there are no rules on format.
    /// </summary>
    public const string SDL_PROP_APP_METADATA_VERSION_STRING = "SDL.app.metadata.version";

    /// <summary>
    /// A unique string in reverse-domain format that identifies this app, like "com.example.mygame2".
    /// </summary>
    public const string SDL_PROP_APP_METADATA_IDENTIFIER_STRING = "SDL.app.metadata.identifier";

    /// <summary>
    /// The human-readable name of the creator/developer/maker of this app, like "MojoWorkshop, LLC".
    /// </summary>
    public const string SDL_PROP_APP_METADATA_CREATOR_STRING = "SDL.app.metadata.creator";

    /// <summary>
    /// The human-readable copyright notice, like "Copyright (c) 2024 MojoWorkshop, LLC".
    /// </summary>
    public const string SDL_PROP_APP_METADATA_COPYRIGHT_STRING = "SDL.app.metadata.copyright";

    /// <summary>
    /// A URL to the app on the web. Maybe a product page, or a storefront, or even a GitHub repository.
    /// </summary>
    public const string SDL_PROP_APP_METADATA_URL_STRING = "SDL.app.metadata.url";

    /// <summary>
    /// The type of application this is. Currently can be "game", "mediaplayer", or "application".
    /// </summary>
    public const string SDL_PROP_APP_METADATA_TYPE_STRING = "SDL.app.metadata.type";

    /// <summary>
    /// Get metadata about your app.
    /// </summary>
    /// <param name="name">the name of the metadata property to get.</param>
    /// <returns>the current value of the metadata property, or the default if it is not set, NULL for properties with no default.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial byte* SDL_GetAppMetadataProperty(byte* name);
}
