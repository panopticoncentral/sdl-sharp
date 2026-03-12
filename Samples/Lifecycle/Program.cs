using SdlSharp;

Console.WriteLine("Creating Application with Events subsystem...");
using (var app = new Application(InitFlags.Events))
{
    Console.WriteLine($"  Initialized: {Application.WasInit()}");

    Console.WriteLine("Initializing Video subsystem...");
    Application.InitSubSystem(InitFlags.Video);
    Console.WriteLine($"  Initialized: {Application.WasInit()}");

    Console.WriteLine("Quitting Video subsystem...");
    Application.QuitSubSystem(InitFlags.Video);
    Console.WriteLine($"  Initialized: {Application.WasInit()}");

    Console.WriteLine("Disposing Application...");
}

Console.WriteLine("Done.");
