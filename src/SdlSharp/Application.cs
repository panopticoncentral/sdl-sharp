using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using static SdlSharp.Native.Common;
using static SdlSharp.Native.Init;

namespace SdlSharp;

/// <summary>
/// Manages the SDL library lifecycle: initialization, metadata, and shutdown.
/// </summary>
public sealed unsafe class Application : IDisposable
{
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void RunOnMainThreadCallback(void* userdata)
    {
        var handle = GCHandle.FromIntPtr((nint)userdata);
        try
        {
            ((Action)handle.Target!)();
        }
        finally
        {
            handle.Free();
        }
    }

    /// <summary>
    /// Initializes SDL with the specified subsystems.
    /// </summary>
    /// <param name="flags">The subsystems to initialize.</param>
    public Application(InitFlags flags)
    {
        Check(SDL_Init((Native.SDL_InitFlags)flags));
    }

    /// <summary>
    /// Initializes additional SDL subsystems.
    /// </summary>
    /// <param name="flags">The additional subsystems to initialize.</param>
    public static void InitSubSystem(InitFlags flags) =>
        Check(SDL_InitSubSystem((Native.SDL_InitFlags)flags));

    /// <summary>
    /// Shuts down specific SDL subsystems.
    /// </summary>
    /// <param name="flags">The subsystems to shut down.</param>
    public static void QuitSubSystem(InitFlags flags) =>
        SDL_QuitSubSystem((Native.SDL_InitFlags)flags);

    /// <summary>
    /// Gets a mask of the currently initialized subsystems.
    /// </summary>
    /// <param name="flags">The subsystems to check, or 0 for all.</param>
    public static InitFlags WasInit(InitFlags flags = 0) =>
        (InitFlags)SDL_WasInit((Native.SDL_InitFlags)flags);

    /// <summary>
    /// Returns whether this is the main thread.
    /// </summary>
    public static bool IsMainThread => SDL_IsMainThread();

    /// <summary>
    /// Runs an action on the main thread during event processing.
    /// If called from the main thread, the action executes immediately.
    /// </summary>
    /// <param name="action">The action to run on the main thread.</param>
    /// <param name="waitComplete">true to block until the action completes, false to return immediately.</param>
    public static void RunOnMainThread(Action action, bool waitComplete = true)
    {
        var handle = GCHandle.Alloc(action);
        if (!SDL_RunOnMainThread(&RunOnMainThreadCallback, (void*)(nint)handle, waitComplete))
        {
            handle.Free();
            throw new SdlException();
        }
    }

    /// <summary>
    /// Sets basic metadata about the application.
    /// </summary>
    /// <param name="name">The name of the application.</param>
    /// <param name="version">The version of the application.</param>
    /// <param name="identifier">A unique reverse-domain identifier.</param>
    public static void SetMetadata(string? name, string? version, string? identifier) =>
        Check(SDL_SetAppMetadata(ToUtf8(name), ToUtf8(version), ToUtf8(identifier)));

    /// <summary>
    /// Sets a specific metadata property.
    /// </summary>
    /// <param name="name">The property name.</param>
    /// <param name="value">The property value, or null to remove.</param>
    public static void SetMetadataProperty(string name, string? value) =>
        Check(SDL_SetAppMetadataProperty(ToUtf8(name), ToUtf8(value)));

    /// <summary>
    /// Gets a metadata property value.
    /// </summary>
    /// <param name="name">The property name.</param>
    /// <returns>The property value, or null if not set.</returns>
    public static string? GetMetadataProperty(string name) =>
        Marshal.PtrToStringUTF8((nint)SDL_GetAppMetadataProperty(ToUtf8(name)));

    /// <summary>
    /// Pumps the event loop, gathering pending input events.
    /// Must be called on the main thread.
    /// </summary>
    public static void PumpEvents() => Native.Events.SDL_PumpEvents();

    /// <inheritdoc/>
    public void Dispose() => SDL_Quit();
}
