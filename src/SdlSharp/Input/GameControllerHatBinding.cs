namespace SdlSharp.Input
{
    /// <summary>
    /// A game controller hat binding.
    /// </summary>
    public sealed class GameControllerHatBinding : GameControllerBinding
    {
        /// <summary>
        /// The hat.
        /// </summary>
        public int Hat { get; }

        /// <summary>
        /// The hat mask.
        /// </summary>
        public int HatMask { get; }

        internal GameControllerHatBinding(int hat, int hatMask)
        {
            Hat = hat;
            HatMask = hatMask;
        }
    }
}
