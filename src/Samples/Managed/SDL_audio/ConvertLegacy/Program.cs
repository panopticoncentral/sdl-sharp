using SdlSharp;
using SdlSharp.Sound;

namespace Samples
{
    public static class Program
    {
        public static void Main()
        {
            using var application = new Application(Subsystems.Audio);

            var wavBuffer = Audio.LoadWav("Fanfare60.wav", out var wavSpec);

            using var device = Audio.Open(null, false, wavSpec, out var deviceSpec, AudioAllowChange.Any);

            var resultBuffer = Audio.Convert(wavBuffer, wavSpec.Format, wavSpec.Channels, wavSpec.Frequency, deviceSpec.Format, deviceSpec.Channels, deviceSpec.Frequency);

            wavBuffer.Dispose();

            device.QueueAudio(resultBuffer);

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
        }
    }
}
