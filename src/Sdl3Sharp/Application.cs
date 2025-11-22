using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Error;
using static Sdl3Sharp.Native.Init;
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
    public static string Revision => SDL_GetRevision();

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

        if (state == SDL_PowerState.SDL_POWERSTATE_ERROR)
        {
            throw new SdlException(SDL_GetError());
        }

        return new PowerInfo(
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
