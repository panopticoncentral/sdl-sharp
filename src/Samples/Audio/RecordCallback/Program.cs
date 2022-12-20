using SdlSharp;
using SdlSharp.Sound;

byte[]? recordingBuffer = null;
var recordingLength = 0;

using var application = new Application(Subsystems.Audio);

var quit = false;
while (!quit)
{
    var command = Console.ReadLine()?.Trim();

    switch (command)
    {
        case "g":
            {
                var recordingSource = new RecordingAudioSource();
                using var device = Audio.Open(null, true, new(), recordingSource, out var actualSpec, AudioAllowChange.Any);

                const int RecordingSeconds = 5;
                var bytesPerSecond = actualSpec.Frequency * actualSpec.Channels * (actualSpec.Format.Bitsize / 8);
                recordingBuffer = new byte[(RecordingSeconds + 1) * bytesPerSecond];
                recordingSource.Initialize(recordingBuffer);

                Console.WriteLine("Recording...");
                device.Unpause();
                for (var i = 0; i < RecordingSeconds * 2; i++)
                {
                    Thread.Sleep(TimeSpan.FromSeconds(0.5));
                }
                device.Pause();
                Thread.Sleep(TimeSpan.FromSeconds(0.5));
                Console.WriteLine("Stopped recording...");

                recordingLength = recordingSource.CurrentOffset;
            }
            break;

        case "p":
            {
                var playbackSource = new PlaybackAudioSource(recordingBuffer!, recordingLength);
                using var device = Audio.Open(null, false, new(), playbackSource, out var actualSpec, AudioAllowChange.Any);

                Console.WriteLine("Playing...");
                device.Unpause();

                while (playbackSource.CurrentOffset < recordingLength)
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

internal sealed class RecordingAudioSource : AudioSource
{
    private byte[]? _recordingBuffer;

    public int CurrentOffset { get; private set; }

    public void Initialize(byte[] buffer)
    {
        _recordingBuffer = buffer;
        CurrentOffset = 0;
    }

    protected override void GetData(Span<byte> data)
    {
        data.CopyTo(new(_recordingBuffer, CurrentOffset, _recordingBuffer!.Length - CurrentOffset));
        CurrentOffset += data.Length;
    }
}

internal sealed class PlaybackAudioSource : AudioSource
{
    private readonly byte[] _playbackBuffer;
    private readonly int _recordingLength;

    public int CurrentOffset { get; private set; }

    public PlaybackAudioSource(byte[] buffer, int length)
    {
        _playbackBuffer = buffer;
        _recordingLength = length;
        CurrentOffset = 0;
    }

    protected override void GetData(Span<byte> data)
    {
        new Span<byte>(_playbackBuffer, CurrentOffset, Math.Min(_recordingLength - CurrentOffset, data.Length)).CopyTo(data);
        CurrentOffset += Math.Min(data.Length, _recordingLength - CurrentOffset);
    }
}
