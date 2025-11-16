using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Init;

namespace Sdl3Sharp;

/// <summary>
/// A class representing the SDL application.
/// </summary>
public sealed unsafe class Application : IDisposable
{
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
