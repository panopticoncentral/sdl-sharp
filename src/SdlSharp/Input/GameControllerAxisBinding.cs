namespace SdlSharp.Input
{
    /// <summary>
    /// An axis binding.
    /// </summary>
    public sealed class GameControllerAxisBinding : GameControllerBinding
    {
        /// <summary>
        /// The axis.
        /// </summary>
        public GameControllerAxis Axis { get; }

        internal GameControllerAxisBinding(int axis)
        {
            Axis = new((Native.SDL_GameControllerAxis)axis);
        }
    }
}
