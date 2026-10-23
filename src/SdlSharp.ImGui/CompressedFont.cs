using System.Buffers.Binary;

namespace SdlSharp.ImGui;

// Bounded reader for the stb stream emitted by ImGui's binary_to_compressed_c.
// Unlike the upstream decoder, every token is checked against both buffers.
internal static class CompressedFont
{
    internal const int DefaultMaximumSize = 64 * 1024 * 1024;

    internal static byte[] DecodeBase85(string text)
    {
        ArgumentNullException.ThrowIfNull(text);
        if (text.Length == 0 || text.Length % 5 != 0) throw Invalid();
        var result = new byte[checked(text.Length / 5 * 4)];
        for (int input = 0, output = 0; input < text.Length; input += 5, output += 4)
        {
            ulong value = 0, multiplier = 1;
            for (var i = 0; i < 5; i++, multiplier *= 85)
            {
                var c = text[input + i];
                if (c < '#' || c > 'x' || c == '\\') throw Invalid();
                value += (ulong)(c >= '\\' ? c - 36 : c - 35) * multiplier;
            }
            if (value > uint.MaxValue) throw Invalid();
            BinaryPrimitives.WriteUInt32LittleEndian(result.AsSpan(output), (uint)value);
        }
        return result;
    }

    internal static byte[] Decode(ReadOnlySpan<byte> source, int maximumSize = DefaultMaximumSize)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maximumSize);
        if (source.Length < 16 || Read(source, 0, 4) != 0x57bc0000 || Read(source, 4, 4) != 0) throw Invalid();
        var size = Read(source, 8, 4);
        if (size == 0 || size > maximumSize || size > Array.MaxLength) throw Invalid();
        var output = new byte[(int)size];
        var cursor = 16;
        var written = 0;
        while (true)
        {
            var token = Read(source, cursor, 1);
            if (token == 5)
            {
                if (Read(source, cursor + 1, 1) != 0xfa || written != output.Length ||
                    Read(source, cursor + 2, 4) != Checksum(output)) throw Invalid();
                cursor += 6;
                // Base85 pads its last group to four bytes.
                if (source.Length - cursor > 3) throw Invalid();
                foreach (var padding in source[cursor..]) if (padding != 0) throw Invalid();
                return output;
            }
            int header, length, distance = 0;
            if (token >= 0x80) { header = 2; length = (int)token - 0x80 + 1; distance = (int)Read(source, cursor + 1, 1) + 1; }
            else if (token >= 0x40) { header = 3; length = (int)Read(source, cursor + 2, 1) + 1; distance = (int)Read(source, cursor, 2) - 0x4000 + 1; }
            else if (token >= 0x20) { header = 1; length = (int)token - 0x20 + 1; }
            else if (token >= 0x18) { header = 4; length = (int)Read(source, cursor + 3, 1) + 1; distance = (int)Read(source, cursor, 3) - 0x180000 + 1; }
            else if (token >= 0x10) { header = 5; length = (int)Read(source, cursor + 3, 2) + 1; distance = (int)Read(source, cursor, 3) - 0x100000 + 1; }
            else if (token >= 8) { header = 2; length = (int)Read(source, cursor, 2) - 0x0800 + 1; }
            else if (token == 7) { header = 3; length = (int)Read(source, cursor + 1, 2) + 1; }
            else if (token == 6) { header = 5; length = (int)Read(source, cursor + 4, 1) + 1; distance = (int)Read(source, cursor + 1, 3) + 1; }
            else if (token == 4) { header = 6; length = (int)Read(source, cursor + 4, 2) + 1; distance = (int)Read(source, cursor + 1, 3) + 1; }
            else throw Invalid();

            cursor += header;
            if (length > output.Length - written) throw Invalid();
            if (distance == 0)
            {
                if (length > source.Length - cursor) throw Invalid();
                source.Slice(cursor, length).CopyTo(output.AsSpan(written));
                cursor += length;
                written += length;
            }
            else
            {
                if (distance > written) throw Invalid();
                // Overlapping matches intentionally copy the newly written bytes.
                for (var i = 0; i < length; i++, written++) output[written] = output[written - distance];
            }
        }
    }

    private static uint Read(ReadOnlySpan<byte> source, int offset, int count)
    {
        if (offset < 0 || offset > source.Length - count) throw Invalid();
        uint value = 0;
        foreach (var b in source.Slice(offset, count)) value = (value << 8) | b;
        return value;
    }

    private static uint Checksum(ReadOnlySpan<byte> bytes)
    {
        uint first = 1, second = 0;
        foreach (var b in bytes) { first = (first + b) % 65521; second = (second + first) % 65521; }
        return (second << 16) | first;
    }
    private static InvalidDataException Invalid() => new("Invalid, truncated, or oversized ImGui compressed font data.");
}
