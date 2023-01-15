namespace SdlSharp
{
    /// <summary>
    /// A block of memory allocated by SDL.
    /// </summary>
    public sealed unsafe class NativeMemoryBlock : IDisposable
    {
        private byte* _block;

        /// <summary>
        /// The size of the block.
        /// </summary>
        public uint Size { get; private set; }

        /// <summary>
        /// Constructs a native memory block.
        /// </summary>
        /// <param name="size">The size of the block of memory to allocate.</param>
        public NativeMemoryBlock(uint size)
        {
            _block = (byte*)Native.SDL_malloc(size);
            Size = size;

            if (_block == null)
            {
                throw new SdlException();
            }
        }

        /// <summary>
        /// Loads a memory block from a file.
        /// </summary>
        /// <param name="filename">The file name.</param>
        public NativeMemoryBlock(string filename)
        {
            Native.StringToUtf8Action(filename, namePtr =>
            {
                nuint size;
                _block = Native.SDL_LoadFile(namePtr, &size);
                Size = (uint)size;
            });
        }

        /// <summary>
        /// Loads a memory block from a storage.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <param name="shouldDispose">Whether the storage should be disposed when finished with.</param>
        public NativeMemoryBlock(RWOps rwops, bool shouldDispose)
        {
            nuint size;
            _block = Native.SDL_LoadFile_RW(rwops.ToNative(), &size, Native.BoolToInt(shouldDispose));
            Size = (uint)size;
        }

        internal NativeMemoryBlock(byte* buffer, uint size)
        {
            _block = buffer;
            Size = size;
        }

        /// <summary>
        /// The block of memory as a span.
        /// </summary>
        public Span<byte> AsSpan() => new(_block, (int)Size);

        /// <summary>
        /// Frees the block of memory.
        /// </summary>
        public void Dispose()
        {
            if (_block != null)
            {
                Native.SDL_free(_block);
                _block = null;
                Size = 0;
            }
        }

        /// <summary>
        /// Converts a native memory block to a byte span.
        /// </summary>
        /// <param name="block">The memory block.</param>
        public static implicit operator Span<byte>(NativeMemoryBlock block) => block.AsSpan();

        internal byte* ToNative() => _block;
    }
}
