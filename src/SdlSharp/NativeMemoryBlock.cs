namespace SdlSharp
{
    /// <summary>
    /// A block of memory allocated by SDL.
    /// </summary>
    public sealed unsafe class NativeMemoryBlock : IDisposable
    {
        /// <summary>
        /// The block pointer.
        /// </summary>
        public void* Block { get; private set; }

        /// <summary>
        /// The size of the block of memory.
        /// </summary>
        public uint Size { get; private set; }

        /// <summary>
        /// Constructs a native memory block.
        /// </summary>
        /// <param name="size">The size of the block of memory to allocate.</param>
        public NativeMemoryBlock(uint size)
        {
            Block = Native.SDL_malloc(size);
            Size = size;

            if (Block == null)
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
            Block = Native.SDL_LoadFile(filename, out var size);
            Size = (uint)size;
        }

        /// <summary>
        /// Loads a memory block from a storage.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <param name="shouldDispose">Whether the storage should be disposed when finished with.</param>
        public NativeMemoryBlock(RWOps rwops, bool shouldDispose)
        {
            Block = Native.SDL_LoadFile_RW(rwops.Native, out var size, shouldDispose);
            Size = (uint)size;
        }

        /// <summary>
        /// The block of memory as a span.
        /// </summary>
        public Span<byte> AsSpan() => new(Block, (int)Size);

        /// <summary>
        /// Frees the block of memory.
        /// </summary>
        public void Dispose()
        {
            if (Block != null)
            {
                Native.SDL_free(Block);
                Block = null;
                Size = 0;
            }
        }

        /// <summary>
        /// Converts a native memory block to a byte span.
        /// </summary>
        /// <param name="block">The memory block.</param>
        public static implicit operator Span<byte>(NativeMemoryBlock block) => block.AsSpan();
    }
}
