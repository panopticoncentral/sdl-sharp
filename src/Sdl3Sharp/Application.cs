using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Init;
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
    /// The SDL subsystems that have been initialized.
    /// </summary>
    public Subsystems InitializedSubystems
    {
        get => (Subsystems)SDL_WasInit((uint)Subsystems.None);
        set
        {
            Subsystems current = InitializedSubystems;

            SDL_QuitSubSystem((SDL_InitFlags)(current & ~value));
            _ = CheckError(SDL_InitSubSystem((SDL_InitFlags)(value & ~current)));
        }
    }

    /// <summary>
    /// Starts the application with the specified capabilities.
    /// </summary>
    /// <param name="subsystems">The subsystems to initialize.</param>
    public Application(Subsystems subsystems)
    {
        _ = CheckError(SDL_Init((SDL_InitFlags)subsystems));
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        SDL_Quit();
    }
}
