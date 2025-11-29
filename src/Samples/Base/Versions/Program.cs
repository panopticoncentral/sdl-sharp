using Sdl3Sharp;

Console.WriteLine($"SDL3 Version: {Application.Version}.");
Console.WriteLine($"SDL3 Revision: {Application.Revision}.");
Console.WriteLine();
Console.WriteLine($"Base path: {FileSystem.GetBasePath()}.");
Console.WriteLine($"Platform: {Application.Platform}.");
Console.WriteLine($"Preferred locales:");
foreach ((var language, var country) in Locale.GetPreferredLocales())
{
    Console.WriteLine($"\t{language}, {country}");
}
