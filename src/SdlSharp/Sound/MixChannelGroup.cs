namespace SdlSharp.Sound
{
    /// <summary>
    /// A group of mixer channels.
    /// </summary>
    public sealed class MixChannelGroup : NativeStaticIndexBase<int, MixChannelGroup>
    {
        /// <summary>
        /// The first available channel in the group.
        /// </summary>
        public MixChannel Available =>
            MixChannel.Get(Native.Mix_GroupAvailable(Index));

        /// <summary>
        /// The number of channels in the group.
        /// </summary>
        public int Count =>
            Native.Mix_GroupCount(Index);

        /// <summary>
        /// The oldest actively playing channel in the group.
        /// </summary>
        public MixChannel Oldest =>
            MixChannel.Get(Native.Mix_GroupOldest(Index));

        /// <summary>
        /// The newest actively playing channel in the group.
        /// </summary>
        public MixChannel Newer =>
            MixChannel.Get(Native.Mix_GroupNewer(Index));

        /// <summary>
        /// Adds a channel to the group.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <returns><c>true</c> if the channel is added, <c>false</c> otherwise.</returns>
        public bool Add(MixChannel channel) =>
            Native.Mix_GroupChannel(channel.Index, Index);

        /// <summary>
        /// Adds a set of channels to the group.
        /// </summary>
        /// <param name="from">The first channel.</param>
        /// <param name="to">The last channel.</param>
        /// <returns><c>true</c> if the channels are added, <c>false</c> otherwise.</returns>
        public bool Add(MixChannel from, MixChannel to) =>
            Native.Mix_GroupChannels(from.Index, to.Index, Index);

        /// <summary>
        /// Halts the group.
        /// </summary>
        public void Halt() =>
            Native.CheckError(Native.Mix_HaltGroup(Index));

        /// <summary>
        /// Fades the group out.
        /// </summary>
        /// <param name="ms">The length of the fade.</param>
        /// <returns>The number of channels that will fade out.</returns>
        public int FadeOut(int ms) =>
            Native.Mix_FadeOutGroup(Index, ms);
    }
}
