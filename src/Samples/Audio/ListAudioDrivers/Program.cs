using SdlSharp;
using SdlSharp.Sound;

Console.WriteLine("Available audio drivers:");
foreach (var driver in Audio.Drivers)
{
    Console.WriteLine(driver);
}

using var application = new Application(Subsystems.Audio);
Console.WriteLine();
Console.WriteLine("Current audio driver:");
Console.WriteLine(Audio.CurrentDriver);
