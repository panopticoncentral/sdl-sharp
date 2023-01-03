using SdlSharp;
using SdlSharp.Sound;

byte[]? recordingBuffer = null;
var recordingLength = 0;
var currentOffset = 0;

using var application = new Application(Subsystems.Audio);

var quit = false;
while (!quit)
{
    var command = Console.ReadLine()?.Trim();

    switch (command)
    {
        case "g":
            {
                var recordingSource = new AudioSource(data =>
                {
                    data.CopyTo(new(recordingBuffer, currentOffset, recordingBuffer!.Length - currentOffset));
                    currentOffset += data.Length;
                });
                using var device = Audio.Open(null, true, new(), recordingSource, out var actualSpec, AudioAllowChange.Any);

                const int RecordingSeconds = 5;
                var bytesPerSecond = actualSpec.Frequency * actualSpec.Channels * (actualSpec.Format.Bitsize / 8);
                recordingBuffer = new byte[(RecordingSeconds + 1) * bytesPerSecond];

                Console.WriteLine("Recording...");
                device.Unpause();
                for (var i = 0; i < RecordingSeconds * 2; i++)
                {
                    Thread.Sleep(TimeSpan.FromSeconds(0.5));
                }
                device.Pause();
                Thread.Sleep(TimeSpan.FromSeconds(0.5));
                Console.WriteLine("Stopped recording...");

                recordingLength = currentOffset;
                currentOffset = 0;
            }
            break;

        case "p":
            {
                var playbackSource = new AudioSource(data =>
                {
                    new Span<byte>(recordingBuffer, currentOffset, Math.Min(recordingLength - currentOffset, data.Length)).CopyTo(data);
                    currentOffset += Math.Min(data.Length, recordingLength - currentOffset);
                });
                using var device = Audio.Open(null, false, new(), playbackSource, out var actualSpec, AudioAllowChange.Any);

                Console.WriteLine("Playing...");
                device.Unpause();

                while (currentOffset < recordingLength)
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
