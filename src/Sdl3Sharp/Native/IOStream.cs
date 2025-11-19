using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

using static Sdl3Sharp.Native.Properties;
using static Sdl3Sharp.Native.Common;

// We are intentionally exposing the P/Invoke calls so people can do low-level calls if needed
#pragma warning disable CA1401 // P/Invokes should not be visible

namespace Sdl3Sharp.Native;

/// <summary>
/// P/Invoke bindings for SDL_iostream.h - Abstract interface for reading and writing data streams.
/// </summary>
public static unsafe partial class IOStream
{
    /// <summary>
    /// SDL_IOStream status, set by a read or write operation.
    /// </summary>
    public enum SDL_IOStatus
    {
        /// <summary>Everything is ready (no errors and not EOF).</summary>
        SDL_IO_STATUS_READY,
        /// <summary>Read or write I/O error.</summary>
        SDL_IO_STATUS_ERROR,
        /// <summary>End of file.</summary>
        SDL_IO_STATUS_EOF,
        /// <summary>Non blocking I/O, not ready.</summary>
        SDL_IO_STATUS_NOT_READY,
        /// <summary>Tried to write a read-only buffer.</summary>
        SDL_IO_STATUS_READONLY,
        /// <summary>Tried to read a write-only buffer.</summary>
        SDL_IO_STATUS_WRITEONLY
    }

    /// <summary>
    /// Possible whence values for SDL_IOStream seeking.
    /// </summary>
    public enum SDL_IOWhence
    {
        /// <summary>Seek from the beginning of data.</summary>
        SDL_IO_SEEK_SET,
        /// <summary>Seek relative to current read point.</summary>
        SDL_IO_SEEK_CUR,
        /// <summary>Seek relative to the end of data.</summary>
        SDL_IO_SEEK_END
    }

    /// <summary>
    /// The function pointers that drive an SDL_IOStream.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_IOStreamInterface
    {
        /// <summary>The version of this interface.</summary>
        public uint version;

        /// <summary>
        /// Return the number of bytes in this SDL_IOStream.
        /// </summary>
        public delegate* unmanaged[Cdecl]<nuint, long> size;

        /// <summary>
        /// Seek to offset relative to whence.
        /// </summary>
        public delegate* unmanaged[Cdecl]<nuint, long, SDL_IOWhence, long> seek;

        /// <summary>
        /// Read up to size bytes from the data stream to the area pointed at by ptr.
        /// </summary>
        public delegate* unmanaged[Cdecl]<nuint, void*, nuint, SDL_IOStatus*, nuint> read;

        /// <summary>
        /// Write exactly size bytes from the area pointed at by ptr to data stream.
        /// </summary>
        public delegate* unmanaged[Cdecl]<nuint, void*, nuint, SDL_IOStatus*, nuint> write;

        /// <summary>
        /// If the stream is buffering, make sure the data is written out.
        /// </summary>
        public delegate* unmanaged[Cdecl]<nuint, SDL_IOStatus*, byte> flush;

        /// <summary>
        /// Close and free any allocated resources.
        /// </summary>
        public delegate* unmanaged[Cdecl]<nuint, byte> close;
    }

    /// <summary>
    /// The read/write operation structure.
    /// </summary>
    public struct SDL_IOStream
    {
    }

    /// <summary>
    /// Property name for a pointer that can be cast to a Win32 HANDLE that this SDL_IOStream is using to access the filesystem.
    /// </summary>
    public const string SDL_PROP_IOSTREAM_WINDOWS_HANDLE_POINTER = "SDL.iostream.windows.handle";

    /// <summary>
    /// Property name for a pointer that can be cast to a stdio FILE* that this SDL_IOStream is using to access the filesystem.
    /// </summary>
    public const string SDL_PROP_IOSTREAM_STDIO_FILE_POINTER = "SDL.iostream.stdio.file";

    /// <summary>
    /// Property name for a file descriptor number that this SDL_IOStream is using to access the filesystem.
    /// </summary>
    public const string SDL_PROP_IOSTREAM_FILE_DESCRIPTOR_NUMBER = "SDL.iostream.file_descriptor";

    /// <summary>
    /// Property name for a pointer that can be cast to an Android NDK AAsset* that this SDL_IOStream is using to access the filesystem.
    /// </summary>
    public const string SDL_PROP_IOSTREAM_ANDROID_AASSET_POINTER = "SDL.iostream.android.aasset";

    /// <summary>
    /// Property name for a pointer to the internal memory of a memory-based SDL_IOStream.
    /// </summary>
    public const string SDL_PROP_IOSTREAM_MEMORY_POINTER = "SDL.iostream.memory.base";

    /// <summary>
    /// Property name for the size in bytes of a memory-based SDL_IOStream.
    /// </summary>
    public const string SDL_PROP_IOSTREAM_MEMORY_SIZE_NUMBER = "SDL.iostream.memory.size";

    /// <summary>
    /// Property name for a pointer to the internal memory of a dynamic memory SDL_IOStream. Can be set to NULL to transfer ownership to the application.
    /// </summary>
    public const string SDL_PROP_IOSTREAM_DYNAMIC_MEMORY_POINTER = "SDL.iostream.dynamic.memory";

    /// <summary>
    /// Property name for the chunk size (in bytes) for memory allocation in a dynamic memory SDL_IOStream. Defaults to 1024.
    /// </summary>
    public const string SDL_PROP_IOSTREAM_DYNAMIC_CHUNKSIZE_NUMBER = "SDL.iostream.dynamic.chunksize";

    /// <summary>
    /// Use this function to create a new SDL_IOStream structure for reading from and/or writing to a named file.
    /// </summary>
    /// <param name="file">a UTF-8 string representing the filename to open.</param>
    /// <param name="mode">an ASCII string representing the mode to be used for opening the file.</param>
    /// <returns>a pointer to the SDL_IOStream structure that is created or NULL on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_IOStream* SDL_IOFromFile([MarshalUsing(typeof(Utf8StringMarshaller))] string file, [MarshalUsing(typeof(Utf8StringMarshaller))] string mode);

    /// <summary>
    /// Use this function to prepare a read-write memory buffer for use with SDL_IOStream.
    /// </summary>
    /// <param name="mem">a pointer to a buffer to feed an SDL_IOStream stream.</param>
    /// <param name="size">the buffer size, in bytes.</param>
    /// <returns>a pointer to a new SDL_IOStream structure or NULL on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_IOStream* SDL_IOFromMem(void* mem, nuint size);

    /// <summary>
    /// Use this function to prepare a read-only memory buffer for use with SDL_IOStream.
    /// </summary>
    /// <param name="mem">a pointer to a read-only buffer to feed an SDL_IOStream stream.</param>
    /// <param name="size">the buffer size, in bytes.</param>
    /// <returns>a pointer to a new SDL_IOStream structure or NULL on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_IOStream* SDL_IOFromConstMem(void* mem, nuint size);

    /// <summary>
    /// Use this function to create an SDL_IOStream that is backed by dynamically allocated memory.
    /// </summary>
    /// <returns>a pointer to a new SDL_IOStream structure or NULL on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_IOStream* SDL_IOFromDynamicMem();

    /// <summary>
    /// Create a custom SDL_IOStream.
    /// </summary>
    /// <param name="iface">the interface that implements this SDL_IOStream, initialized using SDL_INIT_INTERFACE().</param>
    /// <param name="userdata">the pointer that will be passed to the interface functions.</param>
    /// <returns>a pointer to the allocated memory on success or NULL on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_IOStream* SDL_OpenIO(SDL_IOStreamInterface* iface, nuint userdata);

    /// <summary>
    /// Close and free an allocated SDL_IOStream structure.
    /// </summary>
    /// <param name="context">SDL_IOStream structure to close.</param>
    /// <returns>true on success or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_CloseIO(SDL_IOStream* context);

    /// <summary>
    /// Get the properties associated with an SDL_IOStream.
    /// </summary>
    /// <param name="context">a pointer to an SDL_IOStream structure.</param>
    /// <returns>a valid property ID on success or 0 on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_PropertiesID SDL_GetIOProperties(SDL_IOStream* context);

    /// <summary>
    /// Query the stream status of an SDL_IOStream.
    /// </summary>
    /// <param name="context">the SDL_IOStream to query.</param>
    /// <returns>an SDL_IOStatus enum with the current state.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_IOStatus SDL_GetIOStatus(SDL_IOStream* context);

    /// <summary>
    /// Use this function to get the size of the data stream in an SDL_IOStream.
    /// </summary>
    /// <param name="context">the SDL_IOStream to get the size of the data stream from.</param>
    /// <returns>the size of the data stream in the SDL_IOStream on success or a negative error code on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial long SDL_GetIOSize(SDL_IOStream* context);

    /// <summary>
    /// Seek within an SDL_IOStream data stream.
    /// </summary>
    /// <param name="context">a pointer to an SDL_IOStream structure.</param>
    /// <param name="offset">an offset in bytes, relative to whence location; can be negative.</param>
    /// <param name="whence">any of SDL_IO_SEEK_SET, SDL_IO_SEEK_CUR, SDL_IO_SEEK_END.</param>
    /// <returns>the final offset in the data stream after the seek or -1 on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial long SDL_SeekIO(SDL_IOStream* context, long offset, SDL_IOWhence whence);

    /// <summary>
    /// Determine the current read/write offset in an SDL_IOStream data stream.
    /// </summary>
    /// <param name="context">an SDL_IOStream data stream object from which to get the current offset.</param>
    /// <returns>the current offset in the stream, or -1 if the information can not be determined.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial long SDL_TellIO(SDL_IOStream* context);

    /// <summary>
    /// Read from a data source.
    /// </summary>
    /// <param name="context">a pointer to an SDL_IOStream structure.</param>
    /// <param name="ptr">a pointer to a buffer to read data into.</param>
    /// <param name="size">the number of bytes to read from the data source.</param>
    /// <returns>the number of bytes read, or 0 on end of file or other failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint SDL_ReadIO(SDL_IOStream* context, void* ptr, nuint size);

    /// <summary>
    /// Write to an SDL_IOStream data stream.
    /// </summary>
    /// <param name="context">a pointer to an SDL_IOStream structure.</param>
    /// <param name="ptr">a pointer to a buffer containing data to write.</param>
    /// <param name="size">the number of bytes to write.</param>
    /// <returns>the number of bytes written, which will be less than size on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint SDL_WriteIO(SDL_IOStream* context, void* ptr, nuint size);

    // SDL_IOprintf is not wrapped - variadic functions are not supported in C# P/Invoke.
    // SDL_IOvprintf is not wrapped - va_list is not supported in C# P/Invoke.

    /// <summary>
    /// Flush any buffered data in the stream.
    /// </summary>
    /// <param name="context">SDL_IOStream structure to flush.</param>
    /// <returns>true on success or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_FlushIO(SDL_IOStream* context);

    /// <summary>
    /// Load all the data from an SDL data stream.
    /// </summary>
    /// <param name="src">the SDL_IOStream to read all available data from.</param>
    /// <param name="datasize">a pointer filled in with the number of bytes read, may be NULL.</param>
    /// <param name="closeio">if true, calls SDL_CloseIO() on src before returning, even in the case of an error.</param>
    /// <returns>the data or NULL on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* SDL_LoadFile_IO(SDL_IOStream* src, nuint* datasize, [MarshalAs(UnmanagedType.U1)] bool closeio);

    /// <summary>
    /// Load all the data from a file path.
    /// </summary>
    /// <param name="file">the path to read all available data from.</param>
    /// <param name="datasize">if not NULL, will store the number of bytes read.</param>
    /// <returns>the data or NULL on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* SDL_LoadFile([MarshalUsing(typeof(Utf8StringMarshaller))] string file, nuint* datasize);

    /// <summary>
    /// Save all the data into an SDL data stream.
    /// </summary>
    /// <param name="src">the SDL_IOStream to write all data to.</param>
    /// <param name="data">the data to be written. If datasize is 0, may be NULL or a invalid pointer.</param>
    /// <param name="datasize">the number of bytes to be written.</param>
    /// <param name="closeio">if true, calls SDL_CloseIO() on src before returning, even in the case of an error.</param>
    /// <returns>true on success or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SaveFile_IO(SDL_IOStream* src, void* data, nuint datasize, [MarshalAs(UnmanagedType.U1)] bool closeio);

    /// <summary>
    /// Save all the data into a file path.
    /// </summary>
    /// <param name="file">the path to write all available data into.</param>
    /// <param name="data">the data to be written. If datasize is 0, may be NULL or a invalid pointer.</param>
    /// <param name="datasize">the number of bytes to be written.</param>
    /// <returns>true on success or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SaveFile([MarshalUsing(typeof(Utf8StringMarshaller))] string file, void* data, nuint datasize);

    /// <summary>
    /// Use this function to read a byte from an SDL_IOStream.
    /// </summary>
    /// <param name="src">the SDL_IOStream to read from.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <returns>true on success or false on failure or EOF.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_ReadU8(SDL_IOStream* src, byte* value);

    /// <summary>
    /// Use this function to read a signed byte from an SDL_IOStream.
    /// </summary>
    /// <param name="src">the SDL_IOStream to read from.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <returns>true on success or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_ReadS8(SDL_IOStream* src, sbyte* value);

    /// <summary>
    /// Use this function to read 16 bits of little-endian data from an SDL_IOStream and return in native format.
    /// </summary>
    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <returns>true on successful write or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_ReadU16LE(SDL_IOStream* src, ushort* value);

    /// <summary>
    /// Use this function to read 16 bits of little-endian data from an SDL_IOStream and return in native format.
    /// </summary>
    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <returns>true on successful write or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_ReadS16LE(SDL_IOStream* src, short* value);

    /// <summary>
    /// Use this function to read 16 bits of big-endian data from an SDL_IOStream and return in native format.
    /// </summary>
    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <returns>true on successful write or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_ReadU16BE(SDL_IOStream* src, ushort* value);

    /// <summary>
    /// Use this function to read 16 bits of big-endian data from an SDL_IOStream and return in native format.
    /// </summary>
    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <returns>true on successful write or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_ReadS16BE(SDL_IOStream* src, short* value);

    /// <summary>
    /// Use this function to read 32 bits of little-endian data from an SDL_IOStream and return in native format.
    /// </summary>
    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <returns>true on successful write or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_ReadU32LE(SDL_IOStream* src, uint* value);

    /// <summary>
    /// Use this function to read 32 bits of little-endian data from an SDL_IOStream and return in native format.
    /// </summary>
    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <returns>true on successful write or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_ReadS32LE(SDL_IOStream* src, int* value);

    /// <summary>
    /// Use this function to read 32 bits of big-endian data from an SDL_IOStream and return in native format.
    /// </summary>
    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <returns>true on successful write or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_ReadU32BE(SDL_IOStream* src, uint* value);

    /// <summary>
    /// Use this function to read 32 bits of big-endian data from an SDL_IOStream and return in native format.
    /// </summary>
    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <returns>true on successful write or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_ReadS32BE(SDL_IOStream* src, int* value);

    /// <summary>
    /// Use this function to read 64 bits of little-endian data from an SDL_IOStream and return in native format.
    /// </summary>
    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <returns>true on successful write or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_ReadU64LE(SDL_IOStream* src, ulong* value);

    /// <summary>
    /// Use this function to read 64 bits of little-endian data from an SDL_IOStream and return in native format.
    /// </summary>
    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <returns>true on successful write or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_ReadS64LE(SDL_IOStream* src, long* value);

    /// <summary>
    /// Use this function to read 64 bits of big-endian data from an SDL_IOStream and return in native format.
    /// </summary>
    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <returns>true on successful write or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_ReadU64BE(SDL_IOStream* src, ulong* value);

    /// <summary>
    /// Use this function to read 64 bits of big-endian data from an SDL_IOStream and return in native format.
    /// </summary>
    /// <param name="src">the stream from which to read data.</param>
    /// <param name="value">a pointer filled in with the data read.</param>
    /// <returns>true on successful write or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_ReadS64BE(SDL_IOStream* src, long* value);

    /// <summary>
    /// Use this function to write a byte to an SDL_IOStream.
    /// </summary>
    /// <param name="dst">the SDL_IOStream to write to.</param>
    /// <param name="value">the byte value to write.</param>
    /// <returns>true on successful write or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_WriteU8(SDL_IOStream* dst, byte value);

    /// <summary>
    /// Use this function to write a signed byte to an SDL_IOStream.
    /// </summary>
    /// <param name="dst">the SDL_IOStream to write to.</param>
    /// <param name="value">the byte value to write.</param>
    /// <returns>true on successful write or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_WriteS8(SDL_IOStream* dst, sbyte value);

    /// <summary>
    /// Use this function to write 16 bits in native format to an SDL_IOStream as little-endian data.
    /// </summary>
    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <returns>true on successful write or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_WriteU16LE(SDL_IOStream* dst, ushort value);

    /// <summary>
    /// Use this function to write 16 bits in native format to an SDL_IOStream as little-endian data.
    /// </summary>
    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <returns>true on successful write or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_WriteS16LE(SDL_IOStream* dst, short value);

    /// <summary>
    /// Use this function to write 16 bits in native format to an SDL_IOStream as big-endian data.
    /// </summary>
    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <returns>true on successful write or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_WriteU16BE(SDL_IOStream* dst, ushort value);

    /// <summary>
    /// Use this function to write 16 bits in native format to an SDL_IOStream as big-endian data.
    /// </summary>
    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <returns>true on successful write or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_WriteS16BE(SDL_IOStream* dst, short value);

    /// <summary>
    /// Use this function to write 32 bits in native format to an SDL_IOStream as little-endian data.
    /// </summary>
    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <returns>true on successful write or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_WriteU32LE(SDL_IOStream* dst, uint value);

    /// <summary>
    /// Use this function to write 32 bits in native format to an SDL_IOStream as little-endian data.
    /// </summary>
    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <returns>true on successful write or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_WriteS32LE(SDL_IOStream* dst, int value);

    /// <summary>
    /// Use this function to write 32 bits in native format to an SDL_IOStream as big-endian data.
    /// </summary>
    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <returns>true on successful write or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_WriteU32BE(SDL_IOStream* dst, uint value);

    /// <summary>
    /// Use this function to write 32 bits in native format to an SDL_IOStream as big-endian data.
    /// </summary>
    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <returns>true on successful write or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_WriteS32BE(SDL_IOStream* dst, int value);

    /// <summary>
    /// Use this function to write 64 bits in native format to an SDL_IOStream as little-endian data.
    /// </summary>
    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <returns>true on successful write or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_WriteU64LE(SDL_IOStream* dst, ulong value);

    /// <summary>
    /// Use this function to write 64 bits in native format to an SDL_IOStream as little-endian data.
    /// </summary>
    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <returns>true on successful write or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_WriteS64LE(SDL_IOStream* dst, long value);

    /// <summary>
    /// Use this function to write 64 bits in native format to an SDL_IOStream as big-endian data.
    /// </summary>
    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <returns>true on successful write or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_WriteU64BE(SDL_IOStream* dst, ulong value);

    /// <summary>
    /// Use this function to write 64 bits in native format to an SDL_IOStream as big-endian data.
    /// </summary>
    /// <param name="dst">the stream to which data will be written.</param>
    /// <param name="value">the data to be written, in native format.</param>
    /// <returns>true on successful write or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_WriteS64BE(SDL_IOStream* dst, long value);
}
