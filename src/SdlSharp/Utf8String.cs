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
        public override string? ToString() => Native.Utf8ToString(_pointer);

        /// <summary>
        /// Frees the string's storage.
        /// </summary>
        public void Dispose() => Native.SDL_free(_pointer);

        /// <summary>
        /// Convert a regular string to a UTF-8 string.
        /// </summary>
        /// <param name="s">The regular string.</param>
        /// <returns>The new UTF-8 string.</returns>
        public static Utf8String ToUtf8String(string? s) => new(Native.StringToUtf8(s));
    }
}
