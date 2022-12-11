using SdlSharp;
using SdlSharp.Sound;

namespace Samples
{
    public static class Program
    {
        public static void Main()
        {
            using var application = new Application(Subsystems.Audio);

            byte[]? recordingBuffer = null;
            var currentOffset = 0;

            var quit = false;
            while (!quit)
            {
                var command = Console.ReadLine()?.Trim();

                switch (command)
                {
                    case "g":
                        {
                            var (_, requestedSpec) = Audio.DefaultCaptureDevice;

                            using var device = Audio.Open(null, true, requestedSpec, out var actualSpec, AudioAllowChange.Any);

                            const int RecordingSeconds = 5;
                            var bytesPerSecond = actualSpec.Frequency * actualSpec.Channels * (actualSpec.Format.Bitsize / 8);
                            recordingBuffer = new byte[RecordingSeconds * bytesPerSecond];
                            currentOffset = 0;

                            void Dequeue()
                            {
                                var bytesDequeued = device.DequeueAudio(new Span<byte>(recordingBuffer, currentOffset, recordingBuffer.Length - currentOffset));
                                currentOffset += (int)bytesDequeued;
                            }

                            Console.WriteLine("Recording...");
                            device.Unpause();
                            for (var i = 0; i < RecordingSeconds * 2; i++)
                            {
                                Thread.Sleep(TimeSpan.FromSeconds(0.5));
                                Dequeue();
                            }
                            device.Pause();
                            Thread.Sleep(TimeSpan.FromSeconds(0.5));
                            Dequeue();
                            Console.WriteLine("Stopped recording...");
                        }
                        break;

                    case "p":
                        {
                            var (_, requestedSpec) = Audio.DefaultNonCaptureDevice;

                            using var device = Audio.Open(null, false, requestedSpec, out var actualSpec, AudioAllowChange.Any);
                            device.QueueAudio(new Span<byte>(recordingBuffer, 0, currentOffset));

                            Console.WriteLine("Playing...");
                            device.Unpause();

                            while (device.QueuedAudioSize > 0)
                            {
                                Thread.Sleep(TimeSpan.FromSeconds(0.5));
                            }

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
