using System.Text;

namespace Sdl3Sharp.ImGui;

public static unsafe class StringExtensions
{
    public static byte[] ToUtf8(this string value)
    {
        return Encoding.UTF8.GetBytes(value); 
    }

    internal static int Length(byte* value)
    {
        var length = 0;
        while (*value != '\0')
        {
            length++;
        }

        return length;
    }
}
