using SdlSharp;
using SdlSharp.Sound;

foreach (var driver in Audio.Drivers)
{
    Console.WriteLine(driver);
}

_ = Console.ReadLine();
