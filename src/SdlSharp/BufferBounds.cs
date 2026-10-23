namespace SdlSharp;

internal static class BufferBounds
{
    internal static int Required(int rows, int rowBytes, int pitch)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(rows);
        ArgumentOutOfRangeException.ThrowIfNegative(rowBytes);
        if (pitch < rowBytes) throw new ArgumentOutOfRangeException(nameof(pitch));
        return rows == 0 ? 0 : checked((rows - 1) * pitch + rowBytes);
    }
    internal static void Check(int length, int rows, int rowBytes, int pitch, string name)
    {
        if (length < Required(rows, rowBytes, pitch)) throw new ArgumentException("The buffer is too short for the requested dimensions and stride.", name);
    }
}
