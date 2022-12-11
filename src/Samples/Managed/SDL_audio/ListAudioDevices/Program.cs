using SdlSharp;
using SdlSharp.Sound;

namespace Samples
{
    public static class Program
    {
        public static void Main()
        {
            static void PrintAudioDevice((string name, AudioSpecification spec) device)
            {
                Console.WriteLine($"{device.name}:");
                Console.WriteLine($"\tFrequency: {device.spec.Frequency}");
                Console.WriteLine($"\tChannels: {device.spec.Channels}");
            }

            using var application = new Application(Subsystems.Audio);

            Console.WriteLine("Non-capture devices:");
            foreach (var device in Audio.NonCaptureDevices)
            {
                PrintAudioDevice(device);
            }
            Console.WriteLine();

            Console.WriteLine("Default non-capture device:");
            PrintAudioDevice(Audio.DefaultNonCaptureDevice);
            Console.WriteLine();

            Console.WriteLine("Capture devices:");
            foreach (var device in Audio.CaptureDevices)
            {
                PrintAudioDevice(device);
            }
            Console.WriteLine();

            Console.WriteLine("Default capture device:");
            PrintAudioDevice(Audio.DefaultCaptureDevice);

            _ = Console.ReadLine();
        }
    }
}
