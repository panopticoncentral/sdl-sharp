using System.Runtime.InteropServices;

namespace SdlSharp
{
    /// <summary>
    /// A class that represents storage.
    /// </summary>
    public sealed unsafe class RWOps : NativePointerBase<Native.SDL_RWops, RWOps>
    {
        /// <summary>
        /// Returns the size of the storage.
        /// </summary>
        public long Size => SdlSharp.Native.SDL_RWsize(Native);

        /// <summary>
        /// Creates a storage from a filename.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="mode">The file mode.</param>
        /// <returns>The storage.</returns>
        public static RWOps Create(string filename, string mode) =>
            PointerToInstanceNotNull(SdlSharp.Native.SDL_RWFromFile(filename, mode));

        /// <summary>
        /// Creates a storage over a block of memory.
        /// </summary>
        /// <param name="memory">The memory.</param>
        /// <returns>The storage.</returns>
        public static RWOps Create(NativeMemoryBlock memory) =>
            PointerToInstanceNotNull(SdlSharp.Native.SDL_RWFromMem(memory.Block, (int)memory.Size));

        /// <summary>
        /// Creates a storage over a read-only block of memory.
        /// </summary>
        /// <param name="memory">The memory.</param>
        /// <returns>The storage.</returns>
        public static RWOps CreateReadOnly(NativeMemoryBlock memory) =>
            PointerToInstanceNotNull(SdlSharp.Native.SDL_RWFromConstMem(memory.Block, (int)memory.Size));

        /// <summary>
        /// Creates a storage over a read-only byte array.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <returns>The storage.</returns>
        public static RWOps CreateReadOnly(byte[] array)
        {
            var instance = PointerToInstanceNotNull(SdlSharp.Native.SDL_AllocRW());
            var wrapper = new ReadOnlyByteArrayWrapper(array);
            instance.Native->Type = SdlSharp.Native.SDL_RWOpsType.Unknown;
            instance.Native->Size = Marshal.GetFunctionPointerForDelegate<Native.SizeRWOps>(wrapper.Size);
            instance.Native->Seek = Marshal.GetFunctionPointerForDelegate<Native.SeekRWOps>(wrapper.Seek);
            instance.Native->Read = Marshal.GetFunctionPointerForDelegate<Native.ReadRWOps>(wrapper.Read);
            instance.Native->Write = Marshal.GetFunctionPointerForDelegate<Native.WriteRWOps>(ReadOnlyByteArrayWrapper.Write);
            instance.Native->Close = Marshal.GetFunctionPointerForDelegate<Native.CloseRWOps>(wrapper.Close);
            return instance;
        }

        /// <summary>
        /// Seeks to a point in the storage.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="type">The type of seek to perform.</param>
        /// <returns>The new location.</returns>
        public long Seek(long offset, SeekType type) =>
            SdlSharp.Native.SDL_RWseek(Native, offset, type);

        /// <summary>
        /// Reads a value from the storage.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <returns>The value, or <c>null</c> if the value could not be read.</returns>
        public T? Read<T>() where T : unmanaged
        {
            T value = default;
            return SdlSharp.Native.SDL_RWread(Native, &value, (uint)sizeof(T), 1) == 0 ? null : value;
        }

        /// <summary>
        /// Writes a value to the storage.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the value was written, <c>false</c> otherwise.</returns>
        public bool Write<T>(T value) where T : unmanaged => SdlSharp.Native.SDL_RWwrite(Native, &value, (uint)sizeof(T), 1) != 0;

        /// <inheritdoc/>
        public override void Dispose()
        {
            _ = SdlSharp.Native.CheckError(SdlSharp.Native.SDL_RWclose(Native));
            base.Dispose();
        }

        /// <summary>
        /// Reads an unsigned byte.
        /// </summary>
        /// <returns>The value.</returns>
        public byte ReadU8() => SdlSharp.Native.SDL_ReadU8(Native);

        /// <summary>
        /// Reads a little-endian unsigned short.
        /// </summary>
        /// <returns>The value.</returns>
        public ushort ReadLE16() => SdlSharp.Native.SDL_ReadLE16(Native);

        /// <summary>
        /// Reads a big-endian unsigned short.
        /// </summary>
        /// <returns>The value.</returns>
        public ushort ReadBE16() => SdlSharp.Native.SDL_ReadBE16(Native);

        /// <summary>
        /// Reads a little-endian unsigned int.
        /// </summary>
        /// <returns>The value.</returns>
        public uint ReadLE32() => SdlSharp.Native.SDL_ReadLE32(Native);

        /// <summary>
        /// Reads a big-endian unsigned int.
        /// </summary>
        /// <returns>The value.</returns>
        public uint ReadBE32() => SdlSharp.Native.SDL_ReadBE32(Native);

        /// <summary>
        /// Reads a little-endian unsigned long.
        /// </summary>
        /// <returns>The value.</returns>
        public ulong ReadLE64() => SdlSharp.Native.SDL_ReadLE64(Native);

        /// <summary>
        /// Reads a big-endian unsigned long.
        /// </summary>
        /// <returns>The value.</returns>
        public ulong ReadBE64() => SdlSharp.Native.SDL_ReadBE64(Native);

        /// <summary>
        /// Writes an unsigned byte.
        /// </summary>
        /// <param name="value">The value.</param>
        public void WriteU8(byte value) => SdlSharp.Native.CheckErrorZero(SdlSharp.Native.SDL_WriteU8(Native, value));

        /// <summary>
        /// Writes a little-endian unsigned short.
        /// </summary>
        /// <param name="value">The value.</param>
        public void WriteLE16(ushort value) => SdlSharp.Native.CheckErrorZero(SdlSharp.Native.SDL_WriteLE16(Native, value));

        /// <summary>
        /// Writes a big-endian unsigned short.
        /// </summary>
        /// <param name="value">The value.</param>
        public void WriteBE16(ushort value) => SdlSharp.Native.CheckErrorZero(SdlSharp.Native.SDL_WriteBE16(Native, value));

        /// <summary>
        /// Writes a little-endian unsigned int.
        /// </summary>
        /// <param name="value">The value.</param>
        public void WriteLE32(uint value) => SdlSharp.Native.CheckErrorZero(SdlSharp.Native.SDL_WriteLE32(Native, value));

        /// <summary>
        /// Writes a big-endian unsigned int.
        /// </summary>
        /// <param name="value">The value.</param>
        public void WriteBE32(uint value) => SdlSharp.Native.CheckErrorZero(SdlSharp.Native.SDL_WriteBE32(Native, value));

        /// <summary>
        /// Writes a little-endian unsigned long.
        /// </summary>
        /// <param name="value">The value.</param>
        public void WriteLE64(ulong value) => SdlSharp.Native.CheckErrorZero(SdlSharp.Native.SDL_WriteLE64(Native, value));

        /// <summary>
        /// Writes a big-endian unsigned long.
        /// </summary>
        /// <param name="value">The value.</param>
        public void WriteBE64(ulong value) => SdlSharp.Native.CheckErrorZero(SdlSharp.Native.SDL_WriteBE64(Native, value));

        private sealed class ReadOnlyByteArrayWrapper
        {
            private readonly byte[] _array;
            private int _index;
            private bool _isClosed;

            public ReadOnlyByteArrayWrapper(byte[] array)
            {
                _array = array;
            }

            public long Size(Native.SDL_RWops* _) => _isClosed ? -1 : _array.Length;

            internal int Close(Native.SDL_RWops* _)
            {
                if (_isClosed)
                {
                    return -1;
                }
                _isClosed = true;
                return 0;
            }

            internal long Seek(Native.SDL_RWops* _, long offset, SeekType whence)
            {
                var newIndex = _index;

                switch (whence)
                {
                    case SeekType.Set:
                        newIndex = (int)offset;
                        break;

                    case SeekType.Current:
                        newIndex += (int)offset;
                        break;

                    case SeekType.End:
                        newIndex = _array.Length + (int)offset;
                        break;
                }

                if (newIndex < 0 || newIndex >= _array.Length)
                {
                    return -1;
                }

                _index = newIndex;
                return _index;
            }

            internal nuint Read(Native.SDL_RWops* _, void* ptr, nuint size, nuint maxnum)
            {
                uint numberRead = 0;
                var sizeNumber = (uint)size;
                var maxNumNumber = (uint)maxnum;
                var bytePointer = (byte*)ptr;
                var byteIndex = 0;

                bool InBounds() => _index + sizeNumber < _array.Length;

                if (_isClosed || !InBounds())
                {
                    return 0;
                }

                while (maxNumNumber > 0 && InBounds())
                {
                    maxNumNumber--;
                    numberRead++;

                    for (var index = 0; index < sizeNumber; index++)
                    {
                        bytePointer[byteIndex++] = _array[_index++];
                    }
                }

                return numberRead;
            }

            internal static nuint Write(Native.SDL_RWops* _1, void* _2, nuint _3, nuint _4) => 0;
        }
    }
}
