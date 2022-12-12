using SdlSharp;

namespace Samples
{
    public static class Program
    {
        public static unsafe void Main()
        {
            _ = Native.CheckError(Native.SDL_Init(Native.SDL_INIT_VIDEO));

            var exit = false;
            while (!exit)
            {
                var command = Console.ReadLine();

                switch (command)
                {
                    case "q":
                        exit = true;
                        break;

                    case "c":
                        {
                            var s = Console.ReadLine();
                            var text = Native.StringToUtf8(s);
                            _ = Native.CheckError(Native.SDL_SetClipboardText(text));
                            Native.SDL_free(text);
                            Console.WriteLine("Set clipboard text.");
                        }
                        break;

                    case "p":
                        {
                            var text = Native.CheckPointer(Native.SDL_GetClipboardText());
                            Console.WriteLine(Native.Utf8ToString(text));
                            Native.SDL_free(text);
                        }
                        break;
                }
            }

            Native.SDL_Quit();
        }
    }
}
