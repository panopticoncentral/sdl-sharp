namespace SdlSharp
{
    /// <summary>
    ///  An SDL exception.
    /// </summary>
    public sealed class SdlException : Exception
    {
        /// <summary>
        /// Constructs an SDL exception for the last error.
        /// </summary>
        public SdlException() : base(Native.SDL_GetError().ToString())
        {
            Native.SDL_ClearError();
        }

        /// <summary>
        /// Constructs a custom SDL exception.
        /// </summary>
        /// <param name="message">The message.</param>
        public SdlException(string message) : base(message)
        {
        }

        /// <summary>
        /// Constructs a custom SDL exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public SdlException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
