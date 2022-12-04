using System.Text;

namespace SdlSharp
{
    /// <summary>
    /// A UTF-8 string from a static native string.
    /// </summary>
    public readonly unsafe struct StaticUtf8String
    {
        private readonly byte* _pointer;

        /// <summary>
        /// Constructs a static UTF-8 string from a native string.
        /// </summary>
        /// <param name="s">The native string.</param>
        public StaticUtf8String(byte* s)
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
    }
}
