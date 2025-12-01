using System.Runtime.InteropServices;
using System.Text;
using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Error;
using static Sdl3Sharp.Native.Init;
using static Sdl3Sharp.Native.Misc;
using static Sdl3Sharp.Native.Platform;
using static Sdl3Sharp.Native.Power;

namespace Sdl3Sharp;

/// <summary>
/// A class representing the SDL application.
/// </summary>
public sealed unsafe class Application : IDisposable
{
    /// <summary>
    /// Occurs when the user requests a quit (e.g., by closing the last window).
    /// </summary>
    public static event EventHandler<SdlEventArgs>? Quitting;

    /// <summary>
    /// Occurs when the application is being terminated by the OS.
    /// </summary>
    public static event EventHandler<SdlEventArgs>? Terminating;

    /// <summary>
    /// Occurs when the application is low on memory.
    /// </summary>
    public static event EventHandler<SdlEventArgs>? LowMemory;

    /// <summary>
    /// Occurs when the application is about to enter the background.
    /// </summary>
    public static event EventHandler<SdlEventArgs>? WillEnterBackground;

    /// <summary>
    /// Occurs when the application did enter the background.
    /// </summary>
    public static event EventHandler<SdlEventArgs>? DidEnterBackground;

    /// <summary>
    /// Occurs when the application is about to enter the foreground.
    /// </summary>
    public static event EventHandler<SdlEventArgs>? WillEnterForeground;

    /// <summary>
    /// Occurs when the application is now interactive.
    /// </summary>
    public static event EventHandler<SdlEventArgs>? DidEnterForeground;

    /// <summary>
    /// Occurs when the user's locale preferences have changed.
    /// </summary>
    public static event EventHandler<SdlEventArgs>? LocaleChanged;

    /// <summary>
    /// Occurs when the system theme has changed.
    /// </summary>
    public static event EventHandler<SdlEventArgs>? SystemThemeChanged;

    /// <summary>
    /// Occurs when files are dropped on the application.
    /// </summary>
    public static event EventHandler<DropEventArgs>? FileDropped;

    /// <summary>
    /// Occurs when text is dropped on the application.
    /// </summary>
    public static event EventHandler<DropEventArgs>? TextDropped;

    /// <summary>
    /// Occurs when a drop operation begins.
    /// </summary>
    public static event EventHandler<DropEventArgs>? DropBegin;

    /// <summary>
    /// Occurs when a drop operation completes.
    /// </summary>
    public static event EventHandler<DropEventArgs>? DropComplete;

    /// <summary>
    /// Occurs when the cursor is moving during a drop operation.
    /// </summary>
    public static event EventHandler<DropEventArgs>? DropPosition;

    /// <summary>
    /// Occurs when the clipboard contents have changed.
    /// </summary>
    public static event EventHandler<SdlEventArgs>? ClipboardUpdate;

    /// <summary>
    /// The name of the platform SDL is running on.
    /// </summary>
    public static string Platform
    {
        get
        {
            var ptr = SDL_GetPlatform();
            return Marshal.PtrToStringUTF8((nint)ptr) ?? "Unknown";
        }
    }

    /// <summary>
    /// Gets the current power supply details.
    /// </summary>
    public static PowerInfo PowerInfo
    {
        get
        {
            int seconds;
            int percent;
            SDL_PowerState state = SDL_GetPowerInfo(&seconds, &percent);

            return state == SDL_PowerState.SDL_POWERSTATE_ERROR
                ? throw new SdlException(SDL_GetError())
                : new PowerInfo((PowerState)state, seconds == -1 ? null : seconds, percent == -1 ? null : percent);
        }
    }

    /// <summary>
    /// The SDL subsystems that have been initialized.
    /// </summary>
    public Subsystems InitializedSubystems
    {
        get => (Subsystems)SDL_WasInit((uint)Subsystems.None);
        set
        {
            Subsystems current = InitializedSubystems;

            SDL_QuitSubSystem((SDL_InitFlags)(current & ~value));
            _ = CheckErrorBool(SDL_InitSubSystem((SDL_InitFlags)(value & ~current)));
        }
    }

    /// <summary>
    /// Gets a value indicating whether this is the main thread.
    /// </summary>
    /// <remarks>
    /// On Apple platforms, the main thread is the thread that runs your program's main entry point.
    /// On other platforms, the main thread is the one that calls SDL_Init().
    /// </remarks>
    public static bool IsMainThread => SDL_IsMainThread();

    /// <summary>
    /// Opens a URL/URI in the browser or other appropriate external application.
    /// </summary>
    /// <param name="url">A valid URL/URI to open. Use <c>file:///full/path/to/file</c> for local files, if supported.</param>
    public static void OpenUrl(string url)
    {
        fixed (byte* urlPtr = Encoding.UTF8.GetBytes(url + '\0'))
        {
            _ = CheckErrorBool(SDL_OpenURL(urlPtr));
        }
    }

    /// <summary>
    /// Calls an action on the main thread during event processing.
    /// </summary>
    /// <param name="action">The action to call on the main thread.</param>
    /// <param name="waitComplete">If true, waits for the action to complete before returning; if false, returns immediately.</param>
    /// <remarks>
    /// <para>If this is called on the main thread, the action is executed immediately.
    /// If this is called on another thread, this call blocks until the action is run on the main thread.</para>
    /// <para>If you want the action to run without blocking, set <paramref name="waitComplete"/> to false.</para>
    /// <para>When <paramref name="waitComplete"/> is false, the action is prevented from being garbage collected
    /// until it has been executed on the main thread.</para>
    /// </remarks>
    public static void RunOnMainThread(Action action, bool waitComplete = true)
    {
        var handle = GCHandle.Alloc(action);
        try
        {
            _ = CheckErrorBool(SDL_RunOnMainThread(&RunOnMainThreadCallbackPointer, (nuint)(nint)handle, waitComplete));
        }
        catch
        {
            // Free the handle here on failure; on success, the callback frees it.
            handle.Free();
            throw;
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static void RunOnMainThreadCallbackPointer(nuint userdata)
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
    /// Starts the application with the specified capabilities.
    /// </summary>
    /// <param name="subsystems">The subsystems to initialize.</param>
    /// <param name="name">The name of the application.</param>
    /// <param name="version">The version of the application.</param>
    /// <param name="identifier">The unique identifier of the application.</param>
    public Application(Subsystems subsystems, string? name = null, string? version = null, string? identifier = null)
    {
        if (name != null || version != null || identifier != null)
        {
            fixed (byte* namePtr = name != null ? Encoding.UTF8.GetBytes(name + '\0') : null)
            fixed (byte* versionPtr = version != null ? Encoding.UTF8.GetBytes(version + '\0') : null)
            fixed (byte* identifierPtr = identifier != null ? Encoding.UTF8.GetBytes(identifier + '\0') : null)
            {
                _ = CheckErrorBool(SDL_SetAppMetadata(namePtr, versionPtr, identifierPtr));
            }
        }

        _ = CheckErrorBool(SDL_Init((SDL_InitFlags)subsystems));
    }

    internal static void DispatchEvent(Event e)
    {
        switch (e.Type)
        {
            case EventType.Quit:
                Quitting?.Invoke(null, e.TranslateEvent());
                break;

            case EventType.Terminating:
                Terminating?.Invoke(null, e.TranslateEvent());
                break;

            case EventType.LowMemory:
                LowMemory?.Invoke(null, e.TranslateEvent());
                break;

            case EventType.WillEnterBackground:
                WillEnterBackground?.Invoke(null, e.TranslateEvent());
                break;

            case EventType.DidEnterBackground:
                DidEnterBackground?.Invoke(null, e.TranslateEvent());
                break;

            case EventType.WillEnterForeground:
                WillEnterForeground?.Invoke(null, e.TranslateEvent());
                break;

            case EventType.DidEnterForeground:
                DidEnterForeground?.Invoke(null, e.TranslateEvent());
                break;

            case EventType.LocaleChanged:
                LocaleChanged?.Invoke(null, e.TranslateEvent());
                break;

            case EventType.SystemThemeChanged:
                SystemThemeChanged?.Invoke(null, e.TranslateEvent());
                break;

            case EventType.ClipboardUpdate:
                ClipboardUpdate?.Invoke(null, e.TranslateEvent());
                break;

            case EventType.DropFile:
                FileDropped?.Invoke(null, (DropEventArgs)e.TranslateEvent());
                break;

            case EventType.DropText:
                TextDropped?.Invoke(null, (DropEventArgs)e.TranslateEvent());
                break;

            case EventType.DropBegin:
                DropBegin?.Invoke(null, (DropEventArgs)e.TranslateEvent());
                break;

            case EventType.DropComplete:
                DropComplete?.Invoke(null, (DropEventArgs)e.TranslateEvent());
                break;

            case EventType.DropPosition:
                DropPosition?.Invoke(null, (DropEventArgs)e.TranslateEvent());
                break;
        }
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        SDL_Quit();
    }
}
