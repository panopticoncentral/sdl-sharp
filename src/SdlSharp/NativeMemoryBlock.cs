using System;

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
        public void* Pointer { get; private set; }

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
            Pointer = Native.SDL_malloc((nuint)size);
            Size = size;

            if (Pointer == null)
            {
                throw new OutOfMemoryException();
            }
        }

        /// <summary>
        /// Loads a memory block from a file.
        /// </summary>
        /// <param name="filename">The file name.</param>
        public NativeMemoryBlock(string filename)
        {
            Pointer = Native.SDL_LoadFile(filename, out var size);
            Size = (uint)size;
        }

        /// <summary>
        /// Loads a memory block from a storage.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <param name="shouldDispose">Whether the storage should be disposed when finished with.</param>
        public NativeMemoryBlock(RWOps rwops, bool shouldDispose)
        {
            Pointer = Native.SDL_LoadFile_RW(rwops.Pointer, out var size, shouldDispose);
            Size = (uint)size;
        }

        /// <summary>
        /// The block of memory as a span.
        /// </summary>
        public Span<byte> AsSpan() => new Span<byte>(Pointer, (int)Size);

        /// <summary>
        /// Frees the block of memory.
        /// </summary>
        public void Dispose()
        {
            if (Pointer != null)
            {
                Native.SDL_free(Pointer);
                Pointer = null;
                Size = 0;
            }
        }
    }
}
