using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.IOStream;
using static Sdl3Sharp.Native.StdInc;

namespace Sdl3Sharp;

/// <summary>
/// Represents an SDL I/O stream for reading and writing data.
/// </summary>
public sealed unsafe class IOStream : IDisposable
{
    private bool _disposed;
    private readonly bool _ownsHandle;

    /// <summary>
    /// Gets the underlying SDL_IOStream pointer.
    /// </summary>
    public SDL_IOStream* Handle { get; private set; }

    /// <summary>
    /// Gets the properties associated with this stream.
    /// </summary>
    public PropertyGroup Properties
    {
        get
        {
            ThrowIfDisposed();
            return new(SDL_GetIOProperties(Handle), ownsHandle: false);
        }
    }

    /// <summary>
    /// Gets the current status of the stream.
    /// </summary>
    public IOStatus Status
    {
        get
        {
            ThrowIfDisposed();
            return (IOStatus)SDL_GetIOStatus(Handle);
        }
    }

    /// <summary>
    /// Gets the size of the data stream in bytes.
    /// </summary>
    public long Size
    {
        get
        {
            ThrowIfDisposed();
            var size = SDL_GetIOSize(Handle);
            return size < 0 ? throw new SdlException() : size;
        }
    }

    /// <summary>
    /// Gets or sets the current read/write position in the stream.
    /// </summary>
    public long Position
    {
        get
        {
            ThrowIfDisposed();
            var pos = SDL_TellIO(Handle);
            return pos < 0 ? throw new SdlException() : pos;
        }
        set
        {
            ThrowIfDisposed();
            _ = Seek(value, SeekOrigin.Begin);
        }
    }

    /// <summary>
    /// Opens a file for reading and/or writing.
    /// </summary>
    /// <param name="path">The path to the file to open.</param>
    /// <param name="mode">The mode to open the file in (e.g., "r", "w", "rb", "wb+").</param>
    /// <returns>A new IOStream instance.</returns>
    public static IOStream FromFile(string path, string mode)
    {
        return new IOStream(CheckPointer(SDL_IOFromFile(path, mode)), ownsHandle: true);
    }

    /// <summary>
    /// Creates a stream from a read-write memory buffer.
    /// </summary>
    /// <param name="buffer">The memory buffer to use.</param>
    /// <returns>A new IOStream instance.</returns>
    public static IOStream FromMemory(Span<byte> buffer)
    {
        fixed (byte* ptr = buffer)
        {
            return new IOStream(CheckPointer(SDL_IOFromMem(ptr, (nuint)buffer.Length)), ownsHandle: true);
        }
    }

    /// <summary>
    /// Creates a stream from a read-only memory buffer.
    /// </summary>
    /// <param name="buffer">The read-only memory buffer to use.</param>
    /// <returns>A new IOStream instance.</returns>
    public static IOStream FromReadOnlyMemory(ReadOnlySpan<byte> buffer)
    {
        fixed (byte* ptr = buffer)
        {
            return new IOStream(CheckPointer(SDL_IOFromConstMem(ptr, (nuint)buffer.Length)), ownsHandle: true);
        }
    }

    /// <summary>
    /// Creates a stream backed by dynamically allocated memory.
    /// </summary>
    /// <returns>A new IOStream instance.</returns>
    public static IOStream FromDynamicMemory()
    {
        return new IOStream(CheckPointer(SDL_IOFromDynamicMem()), ownsHandle: true);
    }

    /// <summary>
    /// Loads all data from a file.
    /// </summary>
    /// <param name="path">The path to the file to load.</param>
    /// <returns>The contents of the file as a byte array.</returns>
    public static byte[] LoadFile(string path)
    {
        nuint size;
        var data = CheckPointer(SDL_LoadFile(path, &size));

        try
        {
            var result = new byte[size];
            new Span<byte>(data, (int)size).CopyTo(result);
            return result;
        }
        finally
        {
            SDL_free(data);
        }
    }

    /// <summary>
    /// Saves data to a file.
    /// </summary>
    /// <param name="path">The path to the file to save.</param>
    /// <param name="data">The data to save.</param>
    public static void SaveFile(string path, ReadOnlySpan<byte> data)
    {
        fixed (byte* ptr = data)
        {
            _ = CheckErrorBool(SDL_SaveFile(path, ptr, (nuint)data.Length));
        }
    }

    /// <summary>
    /// Wraps an existing SDL_IOStream pointer.
    /// </summary>
    /// <param name="handle">The SDL_IOStream pointer to wrap.</param>
    /// <param name="ownsHandle">Whether this instance should close the stream when disposed.</param>
    internal IOStream(SDL_IOStream* handle, bool ownsHandle = true)
    {
        Handle = handle;
        _ownsHandle = ownsHandle;
    }

    /// <summary>
    /// Seeks to a position in the stream.
    /// </summary>
    /// <param name="offset">The offset to seek to.</param>
    /// <param name="origin">The origin from which to seek.</param>
    /// <returns>The final position in the stream.</returns>
    public long Seek(long offset, SeekOrigin origin)
    {
        ThrowIfDisposed();

        SDL_IOWhence whence = origin switch
        {
            SeekOrigin.Begin => SDL_IOWhence.SDL_IO_SEEK_SET,
            SeekOrigin.Current => SDL_IOWhence.SDL_IO_SEEK_CUR,
            SeekOrigin.End => SDL_IOWhence.SDL_IO_SEEK_END,
            _ => throw new ArgumentException("Invalid seek origin", nameof(origin))
        };

        return CheckErrorZero(SDL_SeekIO(Handle, offset, whence));
    }

    /// <summary>
    /// Reads data from the stream.
    /// </summary>
    /// <param name="buffer">The buffer to read data into.</param>
    /// <returns>The number of bytes actually read.</returns>
    public int Read(Span<byte> buffer)
    {
        ThrowIfDisposed();

        fixed (byte* ptr = buffer)
        {
            return (int)SDL_ReadIO(Handle, ptr, (nuint)buffer.Length);
        }
    }

    /// <summary>
    /// Writes data to the stream.
    /// </summary>
    /// <param name="buffer">The buffer containing data to write.</param>
    /// <returns>The number of bytes actually written.</returns>
    public int Write(ReadOnlySpan<byte> buffer)
    {
        ThrowIfDisposed();

        fixed (byte* ptr = buffer)
        {
            return (int)SDL_WriteIO(Handle, ptr, (nuint)buffer.Length);
        }
    }

    /// <summary>
    /// Flushes any buffered data to the stream.
    /// </summary>
    public void Flush()
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_FlushIO(Handle));
    }

    /// <summary>
    /// Reads a single unsigned byte from the stream.
    /// </summary>
    /// <returns>The byte value read.</returns>
    public byte ReadU8()
    {
        ThrowIfDisposed();
        byte value;
        _ = CheckErrorBool(SDL_ReadU8(Handle, &value));
        return value;
    }

    /// <summary>
    /// Reads a single signed byte from the stream.
    /// </summary>
    /// <returns>The byte value read.</returns>
    public sbyte ReadS8()
    {
        ThrowIfDisposed();
        sbyte value;
        _ = CheckErrorBool(SDL_ReadS8(Handle, &value));
        return value;
    }

    /// <summary>
    /// Reads a 16-bit unsigned integer in little-endian format.
    /// </summary>
    /// <returns>The value read.</returns>
    public ushort ReadU16LE()
    {
        ThrowIfDisposed();
        ushort value;
        _ = CheckErrorBool(SDL_ReadU16LE(Handle, &value));
        return value;
    }

    /// <summary>
    /// Reads a 16-bit signed integer in little-endian format.
    /// </summary>
    /// <returns>The value read.</returns>
    public short ReadS16LE()
    {
        ThrowIfDisposed();
        short value;
        _ = CheckErrorBool(SDL_ReadS16LE(Handle, &value));
        return value;
    }

    /// <summary>
    /// Reads a 16-bit unsigned integer in big-endian format.
    /// </summary>
    /// <returns>The value read.</returns>
    public ushort ReadU16BE()
    {
        ThrowIfDisposed();
        ushort value;
        _ = CheckErrorBool(SDL_ReadU16BE(Handle, &value));
        return value;
    }

    /// <summary>
    /// Reads a 16-bit signed integer in big-endian format.
    /// </summary>
    /// <returns>The value read.</returns>
    public short ReadS16BE()
    {
        ThrowIfDisposed();
        short value;
        _ = CheckErrorBool(SDL_ReadS16BE(Handle, &value));
        return value;
    }

    /// <summary>
    /// Reads a 32-bit unsigned integer in little-endian format.
    /// </summary>
    /// <returns>The value read.</returns>
    public uint ReadU32LE()
    {
        ThrowIfDisposed();
        uint value;
        _ = CheckErrorBool(SDL_ReadU32LE(Handle, &value));
        return value;
    }

    /// <summary>
    /// Reads a 32-bit signed integer in little-endian format.
    /// </summary>
    /// <returns>The value read.</returns>
    public int ReadS32LE()
    {
        ThrowIfDisposed();
        int value;
        _ = CheckErrorBool(SDL_ReadS32LE(Handle, &value));
        return value;
    }

    /// <summary>
    /// Reads a 32-bit unsigned integer in big-endian format.
    /// </summary>
    /// <returns>The value read.</returns>
    public uint ReadU32BE()
    {
        ThrowIfDisposed();
        uint value;
        _ = CheckErrorBool(SDL_ReadU32BE(Handle, &value));
        return value;
    }

    /// <summary>
    /// Reads a 32-bit signed integer in big-endian format.
    /// </summary>
    /// <returns>The value read.</returns>
    public int ReadS32BE()
    {
        ThrowIfDisposed();
        int value;
        _ = CheckErrorBool(SDL_ReadS32BE(Handle, &value));
        return value;
    }

    /// <summary>
    /// Reads a 64-bit unsigned integer in little-endian format.
    /// </summary>
    /// <returns>The value read.</returns>
    public ulong ReadU64LE()
    {
        ThrowIfDisposed();
        ulong value;
        _ = CheckErrorBool(SDL_ReadU64LE(Handle, &value));
        return value;
    }

    /// <summary>
    /// Reads a 64-bit signed integer in little-endian format.
    /// </summary>
    /// <returns>The value read.</returns>
    public long ReadS64LE()
    {
        ThrowIfDisposed();
        long value;
        _ = CheckErrorBool(SDL_ReadS64LE(Handle, &value));
        return value;
    }

    /// <summary>
    /// Reads a 64-bit unsigned integer in big-endian format.
    /// </summary>
    /// <returns>The value read.</returns>
    public ulong ReadU64BE()
    {
        ThrowIfDisposed();
        ulong value;
        _ = CheckErrorBool(SDL_ReadU64BE(Handle, &value));
        return value;
    }

    /// <summary>
    /// Reads a 64-bit signed integer in big-endian format.
    /// </summary>
    /// <returns>The value read.</returns>
    public long ReadS64BE()
    {
        ThrowIfDisposed();
        long value;
        _ = CheckErrorBool(SDL_ReadS64BE(Handle, &value));
        return value;
    }

    /// <summary>
    /// Writes a single unsigned byte to the stream.
    /// </summary>
    /// <param name="value">The value to write.</param>
    public void WriteU8(byte value)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_WriteU8(Handle, value));
    }

    /// <summary>
    /// Writes a single signed byte to the stream.
    /// </summary>
    /// <param name="value">The value to write.</param>
    public void WriteS8(sbyte value)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_WriteS8(Handle, value));
    }

    /// <summary>
    /// Writes a 16-bit unsigned integer in little-endian format.
    /// </summary>
    /// <param name="value">The value to write.</param>
    public void WriteU16LE(ushort value)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_WriteU16LE(Handle, value));
    }

    /// <summary>
    /// Writes a 16-bit signed integer in little-endian format.
    /// </summary>
    /// <param name="value">The value to write.</param>
    public void WriteS16LE(short value)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_WriteS16LE(Handle, value));
    }

    /// <summary>
    /// Writes a 16-bit unsigned integer in big-endian format.
    /// </summary>
    /// <param name="value">The value to write.</param>
    public void WriteU16BE(ushort value)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_WriteU16BE(Handle, value));
    }

    /// <summary>
    /// Writes a 16-bit signed integer in big-endian format.
    /// </summary>
    /// <param name="value">The value to write.</param>
    public void WriteS16BE(short value)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_WriteS16BE(Handle, value));
    }

    /// <summary>
    /// Writes a 32-bit unsigned integer in little-endian format.
    /// </summary>
    /// <param name="value">The value to write.</param>
    public void WriteU32LE(uint value)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_WriteU32LE(Handle, value));
    }

    /// <summary>
    /// Writes a 32-bit signed integer in little-endian format.
    /// </summary>
    /// <param name="value">The value to write.</param>
    public void WriteS32LE(int value)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_WriteS32LE(Handle, value));
    }

    /// <summary>
    /// Writes a 32-bit unsigned integer in big-endian format.
    /// </summary>
    /// <param name="value">The value to write.</param>
    public void WriteU32BE(uint value)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_WriteU32BE(Handle, value));
    }

    /// <summary>
    /// Writes a 32-bit signed integer in big-endian format.
    /// </summary>
    /// <param name="value">The value to write.</param>
    public void WriteS32BE(int value)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_WriteS32BE(Handle, value));
    }

    /// <summary>
    /// Writes a 64-bit unsigned integer in little-endian format.
    /// </summary>
    /// <param name="value">The value to write.</param>
    public void WriteU64LE(ulong value)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_WriteU64LE(Handle, value));
    }

    /// <summary>
    /// Writes a 64-bit signed integer in little-endian format.
    /// </summary>
    /// <param name="value">The value to write.</param>
    public void WriteS64LE(long value)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_WriteS64LE(Handle, value));
    }

    /// <summary>
    /// Writes a 64-bit unsigned integer in big-endian format.
    /// </summary>
    /// <param name="value">The value to write.</param>
    public void WriteU64BE(ulong value)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_WriteU64BE(Handle, value));
    }

    /// <summary>
    /// Writes a 64-bit signed integer in big-endian format.
    /// </summary>
    /// <param name="value">The value to write.</param>
    public void WriteS64BE(long value)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_WriteS64BE(Handle, value));
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        if (_ownsHandle && Handle is not null)
        {
            _ = SDL_CloseIO(Handle);
            Handle = null;
        }

        _disposed = true;
    }

    private void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
    }
}
