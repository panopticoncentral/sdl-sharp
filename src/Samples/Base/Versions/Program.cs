using Sdl3Sharp;

Console.WriteLine($"SDL3 Compiled Version: {Sdl3Sharp.Version.CompiledVersion}.");
Console.WriteLine($"SDL3 Running Version: {Sdl3Sharp.Version.RunningVersion}.");
Console.WriteLine($"SDL3 Revision: {Sdl3Sharp.Version.Revision}.");
Console.WriteLine();
Console.WriteLine($"Base path: {FileSystem.GetBasePath()}.");
Console.WriteLine($"Platform: {Application.Platform}.");
Console.WriteLine($"Preferred locales:");
foreach ((var language, var country) in Locale.GetPreferredLocales())
{
    Console.WriteLine($"\t{language}, {country}");
}
