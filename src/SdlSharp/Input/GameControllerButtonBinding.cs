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
        public GameControllerButton Button { get; }

        internal GameControllerButtonBinding(int button)
        {
            Button = new((Native.SDL_GameControllerButton)button);
        }
    }
}
