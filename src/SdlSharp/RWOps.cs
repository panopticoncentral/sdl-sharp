using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SdlSharp
{
    /// <summary>
    /// A class that represents storage.
    /// </summary>
    public sealed unsafe class RWOps : IDisposable
    {
        private readonly Native.SDL_RWops* _rwops;

        /// <summary>
        /// Returns the size of the storage.
        /// </summary>
        public long Size => Native.SDL_RWsize(_rwops);

        internal RWOps(Native.SDL_RWops* rwops)
        {
            _rwops = rwops;
        }

        /// <summary>
        /// Creates a storage from a filename.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="mode">The file mode.</param>
        /// <returns>The storage.</returns>
        public static RWOps Create(string filename, string mode) => Native.StringToUtf8Func(filename, mode, (filenamePtr, modePtr) => new RWOps(Native.CheckPointer(Native.SDL_RWFromFile(filenamePtr, modePtr))));

        /// <summary>
        /// Creates a storage over a block of memory.
        /// </summary>
        /// <param name="memory">The memory.</param>
        /// <returns>The storage.</returns>
        public static RWOps Create(NativeMemoryBlock memory) =>
            new(Native.SDL_RWFromMem(memory.ToNative(), (int)memory.Size));

        /// <summary>
        /// Creates a storage over a read-only block of memory.
        /// </summary>
        /// <param name="memory">The memory.</param>
        /// <returns>The storage.</returns>
        public static RWOps CreateReadOnly(NativeMemoryBlock memory) =>
            new(Native.SDL_RWFromConstMem(memory.ToNative(), (int)memory.Size));

        /// <summary>
        /// Creates a storage over a read-only byte array.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <returns>The storage.</returns>
        public static RWOps CreateReadOnly(byte[] array)
        {
            RWOps instance = new(Native.SDL_AllocRW());
            var wrapper = new ReadOnlyByteArrayWrapper(array, instance._rwops);
            instance._rwops->type = Native.SDL_RWOPS_UNKNOWN;
            instance._rwops->size = &ReadOnlyByteArrayWrapper.Size;
            instance._rwops->seek = &ReadOnlyByteArrayWrapper.Seek;
            instance._rwops->read = &ReadOnlyByteArrayWrapper.Read;
            instance._rwops->write = &ReadOnlyByteArrayWrapper.Write;
            instance._rwops->close = &ReadOnlyByteArrayWrapper.Close;
            return instance;
        }

        /// <summary>
        /// Seeks to a point in the storage.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="type">The type of seek to perform.</param>
        /// <returns>The new location.</returns>
        public long Seek(long offset, SeekType type) =>
            Native.SDL_RWseek(_rwops, offset, (int)type);

        /// <summary>
        /// Gives the location in the storage.
        /// </summary>
        /// <returns>The location in the storage.</returns>
        public long Tell() => Native.SDL_RWtell(_rwops);

        /// <summary>
        /// Reads a value from the storage.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <returns>The value, or <c>null</c> if the value could not be read.</returns>
        public T? Read<T>() where T : unmanaged
        {
            T value = default;
            return Native.SDL_RWread(_rwops, (byte*)&value, (uint)sizeof(T), 1) == 0 ? null : value;
        }

        /// <summary>
        /// Writes a value to the storage.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the value was written, <c>false</c> otherwise.</returns>
        public bool Write<T>(T value) where T : unmanaged => Native.SDL_RWwrite(_rwops, (byte*)&value, (uint)sizeof(T), 1) != 0;

        /// <inheritdoc/>
        public void Dispose() => _ = Native.CheckError(Native.SDL_RWclose(_rwops));

        /// <summary>
        /// Reads an unsigned byte.
        /// </summary>
        /// <returns>The value.</returns>
        public byte ReadU8() => Native.SDL_ReadU8(_rwops);

        /// <summary>
        /// Reads a little-endian unsigned short.
        /// </summary>
        /// <returns>The value.</returns>
        public ushort ReadLE16() => Native.SDL_ReadLE16(_rwops);

        /// <summary>
        /// Reads a big-endian unsigned short.
        /// </summary>
        /// <returns>The value.</returns>
        public ushort ReadBE16() => Native.SDL_ReadBE16(_rwops);

        /// <summary>
        /// Reads a little-endian unsigned int.
        /// </summary>
        /// <returns>The value.</returns>
        public uint ReadLE32() => Native.SDL_ReadLE32(_rwops);

        /// <summary>
        /// Reads a big-endian unsigned int.
        /// </summary>
        /// <returns>The value.</returns>
        public uint ReadBE32() => Native.SDL_ReadBE32(_rwops);

        /// <summary>
        /// Reads a little-endian unsigned long.
        /// </summary>
        /// <returns>The value.</returns>
        public ulong ReadLE64() => Native.SDL_ReadLE64(_rwops);

        /// <summary>
        /// Reads a big-endian unsigned long.
        /// </summary>
        /// <returns>The value.</returns>
        public ulong ReadBE64() => Native.SDL_ReadBE64(_rwops);

        /// <summary>
        /// Writes an unsigned byte.
        /// </summary>
        /// <param name="value">The value.</param>
        public void WriteU8(byte value) => Native.CheckErrorZero(Native.SDL_WriteU8(_rwops, value));

        /// <summary>
        /// Writes a little-endian unsigned short.
        /// </summary>
        /// <param name="value">The value.</param>
        public void WriteLE16(ushort value) => Native.CheckErrorZero(Native.SDL_WriteLE16(_rwops, value));

        /// <summary>
        /// Writes a big-endian unsigned short.
        /// </summary>
        /// <param name="value">The value.</param>
        public void WriteBE16(ushort value) => Native.CheckErrorZero(Native.SDL_WriteBE16(_rwops, value));

        /// <summary>
        /// Writes a little-endian unsigned int.
        /// </summary>
        /// <param name="value">The value.</param>
        public void WriteLE32(uint value) => Native.CheckErrorZero(Native.SDL_WriteLE32(_rwops, value));

        /// <summary>
        /// Writes a big-endian unsigned int.
        /// </summary>
        /// <param name="value">The value.</param>
        public void WriteBE32(uint value) => Native.CheckErrorZero(Native.SDL_WriteBE32(_rwops, value));

        /// <summary>
        /// Writes a little-endian unsigned long.
        /// </summary>
        /// <param name="value">The value.</param>
        public void WriteLE64(ulong value) => Native.CheckErrorZero(Native.SDL_WriteLE64(_rwops, value));

        /// <summary>
        /// Writes a big-endian unsigned long.
        /// </summary>
        /// <param name="value">The value.</param>
        public void WriteBE64(ulong value) => Native.CheckErrorZero(Native.SDL_WriteBE64(_rwops, value));

        internal Native.SDL_RWops* ToNative() => _rwops;

        private sealed unsafe class ReadOnlyByteArrayWrapper
        {
            private static Dictionary<nint, ReadOnlyByteArrayWrapper>? s_wrappers;

            private static Dictionary<nint, ReadOnlyByteArrayWrapper> Wrappers => s_wrappers ??= new();

            private readonly byte[] _array;
            private int _index;
            private bool _isClosed;

            public ReadOnlyByteArrayWrapper(byte[] array, Native.SDL_RWops* context)
            {
                _array = array;
                Wrappers[(nint)context] = this;
            }

            [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvCdecl) })]
            public static long Size(Native.SDL_RWops* context) =>
                Wrappers.TryGetValue((nint)context, out var instance) ? instance._isClosed ? -1 : instance._array.Length : -1;

            [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvCdecl) })]
            public static int Close(Native.SDL_RWops* context)
            {
                if (Wrappers.TryGetValue((nint)context, out var instance))
                {
                    if (instance._isClosed)
                    {
                        return -1;
                    }
                    instance._isClosed = true;
                    _ = Wrappers.Remove((nint)context);
                    return 0;
                }

                return -1;
            }

            [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvCdecl) })]
            public static long Seek(Native.SDL_RWops* context, long offset, int whence)
            {
                if (Wrappers.TryGetValue((nint)context, out var instance))
                {
                    var newIndex = instance._index;

                    switch ((SeekType)whence)
                    {
                        case SeekType.Set:
                            newIndex = (int)offset;
                            break;

                        case SeekType.Current:
                            newIndex += (int)offset;
                            break;

                        case SeekType.End:
                            newIndex = instance._array.Length + (int)offset;
                            break;
                    }

                    if (newIndex < 0 || newIndex >= instance._array.Length)
                    {
                        return -1;
                    }

                    instance._index = newIndex;
                    return instance._index;
                }

                return -1;
            }

            [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvCdecl) })]
            public static nuint Read(Native.SDL_RWops* context, byte* ptr, nuint size, nuint maxnum)
            {
                if (Wrappers.TryGetValue((nint)context, out var instance))
                {
                    uint numberRead = 0;
                    var sizeNumber = (uint)size;
                    var maxNumNumber = (uint)maxnum;
                    var byteIndex = 0;

                    bool InBounds() => instance._index + sizeNumber < instance._array.Length;

                    if (instance._isClosed || !InBounds())
                    {
                        return 0;
                    }

                    while (maxNumNumber > 0 && InBounds())
                    {
                        maxNumNumber--;
                        numberRead++;

                        for (var index = 0; index < sizeNumber; index++)
                        {
                            ptr[byteIndex++] = instance._array[instance._index++];
                        }
                    }

                    return numberRead;
                }

                return 0;
            }

            [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvCdecl) })]
            internal static nuint Write(Native.SDL_RWops* _1, byte* _2, nuint _3, nuint _4) => 0;
        }
    }
}
