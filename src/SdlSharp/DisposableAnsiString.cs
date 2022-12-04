using System.Runtime.InteropServices;

namespace SdlSharp
{
    /// <summary>
    /// An ANSI native string that must be freed.
    /// </summary>
    public readonly unsafe struct DisposableAnsiString : IDisposable
    {
        private readonly void* _string;

        /// <summary>
        /// Frees the string.
        /// </summary>
        public void Dispose()
        {
            if (_string != null)
            {
                Native.SDL_free(_string);
            }
        }

        /// <inheritdoc/>
        public override string? ToString() => _string == null ? null : Marshal.PtrToStringAnsi((nint)_string);
    }
}
