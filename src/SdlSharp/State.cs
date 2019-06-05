namespace SdlSharp
{
    /// <summary>
    /// The state of some aspect of SDL.
    /// </summary>
    public enum State
    {
        /// <summary>
        /// Just query the state, don't set it.
        /// </summary>
        Query = -1,

        /// <summary>
        /// Set the state to ignored.
        /// </summary>
        Ignore = 0,

        /// <summary>
        /// Set the state to disabled.
        /// </summary>
        Disable = Ignore,

        /// <summary>
        /// Set the state to enabled.
        /// </summary>
        Enable = 1
    }
}
