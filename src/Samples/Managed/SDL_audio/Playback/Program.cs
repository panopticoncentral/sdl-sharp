using SdlSharp;
using SdlSharp.Sound;

using var application = new Application(Subsystems.Audio);

var buffer = Audio.LoadWav("Fanfare60.wav", out var wavSpec);

using var device = Audio.Open(null, false, wavSpec, out var deviceSpec, AudioAllowChange.None);
device.QueueAudio(buffer);

var quit = false;
while (!quit)
{
    var command = Console.ReadLine()?.Trim();

    switch (command)
    {
        case "g":
            device.Unpause();
            break;

        case "p":
            device.Pause();
            break;

        case "q":
            quit = true;
            break;
    }
}
