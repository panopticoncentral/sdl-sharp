namespace SdlSharp.Input
{
    /// <summary>
    /// Event arguments for a sensor update event.
    /// </summary>
    public sealed class SensorUpdatedEventArgs : SdlEventArgs
    {
        /// <summary>
        /// The data.
        /// </summary>
        public IReadOnlyList<float> Data { get; }

        internal unsafe SensorUpdatedEventArgs(Native.SDL_SensorEvent sensor) : base(sensor.timestamp)
        {
            Data = new Span<float>(sensor.data, 6).ToArray();
        }
    }
}
