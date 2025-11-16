using Sdl3Sharp;

using var application = new Application(Subsystems.Events);

if (application.InitializedSubystems != Subsystems.Events)
{
    throw new InvalidOperationException();
}

application.InitializedSubystems |= Subsystems.Haptic;

if (application.InitializedSubystems != (Subsystems.Events | Subsystems.Haptic))
{
    throw new InvalidOperationException();
}

application.InitializedSubystems &= ~Subsystems.Haptic;

if (application.InitializedSubystems != Subsystems.Events)
{
    throw new InvalidOperationException();
}