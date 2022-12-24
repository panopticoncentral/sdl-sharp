namespace SdlSharp
{
    /// <summary>
    /// Event arguments for a hint changing.
    /// </summary>
    public sealed class HintChangedEventArgs : EventArgs
    {
        /// <summary>
        /// The previous value of the hint.
        /// </summary>
        public string? PreviousValue { get; }

        /// <summary>
        /// The new value of the hint.
        /// </summary>
        public string? NewValue { get; }

        internal HintChangedEventArgs(string? previousValue, string? newValue)
        {
            PreviousValue = previousValue;
            NewValue = newValue;
        }
    }
}
