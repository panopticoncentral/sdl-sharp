namespace SdlSharp
{
    /// <summary>
    /// The state of device power.
    /// </summary>
    public enum PowerState
    {
        /// <summary>
        /// Unknown.
        /// </summary>
        Unknown,

        /// <summary>
        /// Running on battery.
        /// </summary>
        OnBattery,

        /// <summary>
        /// No battery.
        /// </summary>
        NoBattery,

        /// <summary>
        /// Battery is charging.
        /// </summary>
        Charging,

        /// <summary>
        /// Battery is charged.
        /// </summary>
        Charged
    }
}
