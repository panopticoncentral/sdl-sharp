using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Error;
using static Sdl3Sharp.Native.Init;
using static Sdl3Sharp.Native.Misc;
using static Sdl3Sharp.Native.Platform;
using static Sdl3Sharp.Native.Power;
using static Sdl3Sharp.Native.Version;

namespace Sdl3Sharp;

/// <summary>
/// A class representing the SDL application.
/// </summary>
public sealed unsafe class Application : IDisposable
{
    /// <summary>
    /// The version of SDL that was compiled against.
    /// </summary>
    public static Version CompiledVersion => new(SDL_VERSION);

    /// <summary>
    /// The version of SDL that is being run against.
    /// </summary>
    public static Version Version => new(SDL_GetVersion());

    /// <summary>
    /// The revision string of the version of SDL that's being used.
    /// </summary>
    public static unsafe string Revision
    {
        get
        {
            var ptr = SDL_GetRevision();
            return System.Runtime.InteropServices.Marshal.PtrToStringUTF8((nint)ptr) ?? string.Empty;
        }
    }

    /// <summary>
    /// The name of the platform SDL is running on.
    /// </summary>
    /// <remarks>
    /// <para>Here are the names returned for some (but not all) supported platforms:</para>
    /// <list type="bullet">
    /// <item><description>"Windows"</description></item>
    /// <item><description>"macOS"</description></item>
    /// <item><description>"Linux"</description></item>
    /// <item><description>"iOS"</description></item>
    /// <item><description>"Android"</description></item>
    /// </list>
    /// <para>If the correct platform name is not available, returns a string beginning with the text "Unknown".</para>
    /// </remarks>
    public static string Platform
    {
        get
        {
            var ptr = SDL_GetPlatform();
            return System.Runtime.InteropServices.Marshal.PtrToStringUTF8((nint)ptr) ?? "Unknown";
        }
    }

    /// <summary>
    /// Opens a URL/URI in the browser or other appropriate external application.
    /// </summary>
    /// <param name="url">A valid URL/URI to open. Use <c>file:///full/path/to/file</c> for local files, if supported.</param>
    /// <exception cref="SdlException">Thrown when opening the URL fails.</exception>
    /// <remarks>
    /// <para>Open a URL in a separate, system-provided application. How this works will
    /// vary wildly depending on the platform. This will likely launch what makes
    /// sense to handle a specific URL's protocol (a web browser for <c>http://</c>,
    /// etc), but it might also be able to launch file managers for directories and
    /// other things.</para>
    /// <para>What happens when you open a URL varies wildly as well: your game window
    /// may lose focus (and may or may not lose focus if your game was fullscreen
    /// or grabbing input at the time). On mobile devices, your app will likely
    /// move to the background or your process might be paused. Any given platform
    /// may or may not handle a given URL.</para>
    /// <para>If this is unimplemented (or simply unavailable) for a platform, this will
    /// fail with an error. A successful result does not mean the URL loaded, just
    /// that we launched <i>something</i> to handle it (or at least believe we did).</para>
    /// <para>All this to say: this function can be useful, but you should definitely
    /// test it on every platform you target.</para>
    /// </remarks>
    public static void OpenUrl(string url)
    {
        _ = CheckErrorBool(SDL_OpenURL(url));
    }

    /// <summary>
    /// Gets the current power supply details.
    /// </summary>
    /// <remarks>
    /// You should never take a battery status as absolute truth. Batteries
    /// (especially failing batteries) are delicate hardware, and the values
    /// reported here are best estimates based on what that hardware reports.
    /// Battery status can change at any time; if you are concerned with power
    /// state, you should call this function frequently.
    /// </remarks>
    /// <returns>Information about the current power state.</returns>
    /// <exception cref="SdlException">Thrown when an error occurs determining the power state.</exception>
    public static PowerInfo GetPowerInfo()
    {
        int seconds;
        int percent;
        SDL_PowerState state = SDL_GetPowerInfo(&seconds, &percent);

        return state == SDL_PowerState.SDL_POWERSTATE_ERROR
            ? throw new SdlException(SDL_GetError())
            : new PowerInfo(
            (PowerState)state,
            seconds == -1 ? null : seconds,
            percent == -1 ? null : percent);
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
    /// Starts the application with the specified capabilities.
    /// </summary>
    /// <param name="subsystems">The subsystems to initialize.</param>
    public Application(Subsystems subsystems)
    {
        _ = CheckErrorBool(SDL_Init((SDL_InitFlags)subsystems));
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        SDL_Quit();
    }
}
