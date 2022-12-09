using SdlSharp;

_ = Native.CheckError(Native.SDL_Init(Native.SDL_INIT_AUDIO));

unsafe
{
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
                    byte* name;
                    Native.SDL_AudioSpec requestedSpec;
                    Native.SDL_AudioSpec actualSpec;

                    _ = Native.CheckError(Native.SDL_GetDefaultAudioInfo(&name, &requestedSpec, Native.BoolToInt(true)));
                    Native.SDL_free(name);

                    var deviceId = Native.CheckValid(Native.SDL_OpenAudioDevice(null, Native.BoolToInt(true), &requestedSpec, &actualSpec, (int)Native.SDL_AUDIO_ALLOW_ANY_CHANGE));

                    const int RecordingSeconds = 5;
                    var bytesPerSecond = actualSpec.freq * actualSpec.channels * (Native.SDL_AUDIO_BITSIZE(actualSpec.format) / 8);
                    recordingBuffer = new byte[RecordingSeconds * bytesPerSecond];
                    currentOffset = 0;

                    void Dequeue()
                    {
                        fixed (byte* ptr = recordingBuffer)
                        {
                            var bytesDequeued = Native.SDL_DequeueAudio(deviceId, ptr + currentOffset, (uint)(recordingBuffer.Length - currentOffset));
                            currentOffset += (int)bytesDequeued;
                        }
                    }

                    Console.WriteLine("Recording...");
                    Native.SDL_PauseAudioDevice(deviceId, Native.BoolToInt(false));
                    for (var i = 0; i < RecordingSeconds * 2; i++)
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(0.5));
                        Dequeue();
                    }
                    Native.SDL_PauseAudioDevice(deviceId, Native.BoolToInt(true));
                    Thread.Sleep(TimeSpan.FromSeconds(0.5));
                    Dequeue();
                    Console.WriteLine("Stopped recording...");

                    Native.SDL_CloseAudioDevice(deviceId);
                }
                break;

            case "p":
                {
                    byte* name;
                    Native.SDL_AudioSpec requestedSpec;
                    Native.SDL_AudioSpec actualSpec;

                    _ = Native.CheckError(Native.SDL_GetDefaultAudioInfo(&name, &requestedSpec, Native.BoolToInt(false)));
                    Native.SDL_free(name);

                    var deviceId = Native.CheckValid(Native.SDL_OpenAudioDevice(null, Native.BoolToInt(false), &requestedSpec, &actualSpec, (int)Native.SDL_AUDIO_ALLOW_ANY_CHANGE));

                    fixed (byte* ptr = recordingBuffer)
                    {
                        _ = Native.CheckError(Native.SDL_QueueAudio(deviceId, ptr, (uint)currentOffset));
                    }

                    Console.WriteLine("Playing...");
                    Native.SDL_PauseAudioDevice(deviceId, Native.BoolToInt(false));

                    while (Native.SDL_GetQueuedAudioSize(deviceId) > 0)
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(0.5));
                    }

                    Console.WriteLine("Stopped playing...");

                    Native.SDL_CloseAudioDevice(deviceId);
                }
                break;

            case "q":
                quit = true;
                break;
        }
    }
}

Native.SDL_Quit();
