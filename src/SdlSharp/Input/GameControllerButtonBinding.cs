namespace SdlSharp.Input
{
    /// <summary>
    /// A game controller button binding.
    /// </summary>
    public sealed class GameControllerButtonBinding : GameControllerBinding
    {
        /// <summary>
        /// The button.
        /// </summary>
        public int Button { get; }

        internal GameControllerButtonBinding(int button)
        {
            Button = button;
        }
    }
}
