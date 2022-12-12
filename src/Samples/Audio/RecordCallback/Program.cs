using SdlSharp;
using SdlSharp.Sound;

namespace Samples
{
    internal static class Program
    {
        private static byte[]? s_recordingBuffer;
        private static int s_currentOffset;
        private static int s_currentLength;

        private static void RecordCallback(Span<byte> buffer)
        {
            buffer.CopyTo(new(s_recordingBuffer, s_currentOffset, s_recordingBuffer!.Length - s_currentOffset));
            s_currentOffset += buffer.Length;
        }

        private static void PlayCallback(Span<byte> buffer)
        {
            new Span<byte>(s_recordingBuffer, s_currentOffset, Math.Min(s_currentLength - s_currentOffset, buffer.Length)).CopyTo(buffer);
            s_currentOffset += Math.Min(buffer.Length, s_currentLength - s_currentOffset);
        }

        private static void Main()
        {
            using var application = new Application(Subsystems.Audio);

            var quit = false;
            while (!quit)
            {
                var command = Console.ReadLine()?.Trim();

                switch (command)
                {
                    case "g":
                        {
                            using var device = Audio.Open(null, true, new(), RecordCallback, out var actualSpec, AudioAllowChange.Any);

                            const int RecordingSeconds = 5;
                            var bytesPerSecond = actualSpec.Frequency * actualSpec.Channels * (actualSpec.Format.Bitsize / 8);
                            s_recordingBuffer = new byte[(RecordingSeconds + 1) * bytesPerSecond];
                            s_currentOffset = 0;

                            Console.WriteLine("Recording...");
                            device.Unpause();
                            for (var i = 0; i < RecordingSeconds * 2; i++)
                            {
                                Thread.Sleep(TimeSpan.FromSeconds(0.5));
                            }
                            device.Pause();
                            Thread.Sleep(TimeSpan.FromSeconds(0.5));
                            Console.WriteLine("Stopped recording...");

                            s_currentLength = s_currentOffset;
                        }
                        break;

                    case "p":
                        {
                            using var device = Audio.Open(null, false, new(), PlayCallback, out var actualSpec, AudioAllowChange.Any);

                            s_currentOffset = 0;

                            Console.WriteLine("Playing...");
                            device.Unpause();

                            while (s_currentOffset < s_currentLength)
                            {
                                Thread.Sleep(TimeSpan.FromSeconds(0.5));
                            }

                            device.Pause();
                            Console.WriteLine("Stopped playing...");
                        }
                        break;

                    case "q":
                        quit = true;
                        break;
                }
            }
        }
    }
}
