using SdlSharp;

using var application = new Application(Subsystems.Timer);

if (application.InitializedSubystems != Subsystems.Timer)
{
    throw new InvalidOperationException();
}

application.InitializedSubystems |= Subsystems.Haptic;

if (application.InitializedSubystems != (Subsystems.Timer | Subsystems.Haptic))
{
    throw new InvalidOperationException();
}

application.InitializedSubystems &= ~Subsystems.Haptic;

if (application.InitializedSubystems != Subsystems.Timer)
{
    throw new InvalidOperationException();
}

Native.SDL_Quit();
