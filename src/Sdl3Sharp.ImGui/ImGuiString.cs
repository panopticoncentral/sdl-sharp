using System.Text;

namespace Sdl3Sharp.ImGui;

public readonly ref struct ImGuiString
{
    private static readonly byte[] emptyString = new byte[1];

    private readonly ReadOnlySpan<byte> _value;

    public ImGuiString(ReadOnlySpan<byte> value)
    {
        _value = value.IsEmpty ? emptyString : value;
    }

    public ReadOnlySpan<byte> AsSpan()
    {
        return _value;
    }

    public static implicit operator ImGuiString(ReadOnlySpan<byte> value)
    {
        return new ImGuiString(value);
    }

    public static explicit operator ImGuiString(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return new ImGuiString(emptyString);
        }

        var bytes = Encoding.UTF8.GetBytes(value);
        return new ImGuiString(bytes);
    }
}