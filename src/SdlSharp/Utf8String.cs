using System;
using System.Text;

namespace SdlSharp
{
    /// <summary>
    /// A UTF-8 string from native string.
    /// </summary>
    public readonly unsafe struct Utf8String : IDisposable
    {
        private readonly byte* _pointer;

        /// <summary>
        /// Constructs a UTF-8 string from a native string.
        /// </summary>
        /// <param name="s">The native string.</param>
        public Utf8String(byte* s)
        {
            _pointer = s;
        }

        /// <inheritdoc/>
        public override string? ToString()
        {
            static int StringLength(byte* v)
            {
                var current = v;
                while (*current != 0)
                {
                    current++;
                }

                return (int)(current - v);
            }

            return _pointer == null ? null : Encoding.UTF8.GetString(_pointer, StringLength(_pointer));
        }

        /// <summary>
        /// Frees the string's storage.
        /// </summary>
        public void Dispose() => Native.SDL_free(_pointer);

        /// <summary>
        /// Convert a regular string to a UTF-8 string.
        /// </summary>
        /// <param name="s">The regular string.</param>
        /// <returns>The new UTF-8 string.</returns>
        public static Utf8String ToUtf8String(string? s)
        {
            byte* pointer = null;

            if (s != null)
            {
                var terminatedString = s + '\0';
                var byteCount = Encoding.UTF8.GetByteCount(terminatedString);

                pointer = (byte*)Native.SDL_malloc((nuint)byteCount);

                fixed (char* terminatedStringBuffer = terminatedString)
                {
                    _ = Encoding.UTF8.GetBytes(terminatedStringBuffer, terminatedString.Length, pointer, byteCount);
                }
            }

            return new Utf8String(pointer);
        }
    }
}
