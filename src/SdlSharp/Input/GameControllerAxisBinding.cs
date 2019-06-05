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
        public int Axis { get; }

        internal GameControllerAxisBinding(int axis)
        {
            Axis = axis;
        }
    }
}
