namespace Sdl3Sharp;

/// <summary>
/// A handle to an event watch registration.
/// </summary>
public readonly struct EventWatchHandle
{
    internal EventQueue.EventWatchRegistration? Registration { get; }

    internal EventWatchHandle(EventQueue.EventWatchRegistration registration)
    {
        Registration = registration;
    }
}