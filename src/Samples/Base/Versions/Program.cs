using SdlSharp;

Console.WriteLine($"SDL2 Version: {Application.Version}.");
Console.WriteLine($"SDL2 Revision: {Application.Revision}.");
Console.WriteLine($"SDL2 Revision Number: {Application.RevisionNumber}.");
Console.WriteLine($"SDL2 Image Version: {Application.ImageVersion}.");
Console.WriteLine($"SDL2 Font Version: {Application.FontVersion}.");
Console.WriteLine($"SDL2 FreeType Version: {Application.FreeTypeVersion}.");
Console.WriteLine($"SDL2 HarfBuzz Version: {Application.HarfBuzzVersion}.");
Console.WriteLine($"SDL2 Mixer Version: {Application.MixerVersion}.");
Console.WriteLine();
Console.WriteLine($"Base path: {Application.BasePath}.");
Console.WriteLine($"Platform: {Application.Platform}.");
Console.WriteLine($"Preferred locales:");
foreach (var (language, country) in Application.PreferredLocales)
{
    Console.WriteLine($"\t{language}, {country}");
}
