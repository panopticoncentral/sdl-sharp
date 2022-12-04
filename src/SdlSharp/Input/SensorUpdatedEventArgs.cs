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

        internal SensorUpdatedEventArgs(Native.SDL_SensorEvent sensor) : base(sensor.Timestamp)
        {
            Data = sensor.Data;
        }
    }
}
