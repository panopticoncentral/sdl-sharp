namespace SdlSharp
{
    /// <summary>
    /// Event arguments for a drop event where a value was dropped.
    /// </summary>
    public sealed class DroppedEventArgs : DropEventArgs
    {
        /// <summary>
        /// The value being dropped, if any. 
        /// </summary>
        public string? Value { get; }

        internal unsafe DroppedEventArgs(Native.SDL_DropEvent drop) : base(drop)
        {
            Value = drop.File.ToString();
        }
    }
}
