namespace SdlSharp.Sound
{
    /// <summary>
    /// A group of mixer channels.
    /// </summary>
    public sealed class MixChannelGroup
    {
        /// <summary>
        /// The group tag;
        /// </summary>
        public int Tag { get; }

        /// <summary>
        /// The first available channel in the group.
        /// </summary>
        public MixChannel Available =>
            MixChannel.Get(Native.Mix_GroupAvailable(Tag));

        /// <summary>
        /// The number of channels in the group.
        /// </summary>
        public int Count =>
            Native.Mix_GroupCount(Tag);

        /// <summary>
        /// The oldest actively playing channel in the group.
        /// </summary>
        public MixChannel Oldest =>
            MixChannel.Get(Native.Mix_GroupOldest(Tag));

        /// <summary>
        /// The newest actively playing channel in the group.
        /// </summary>
        public MixChannel Newer =>
            MixChannel.Get(Native.Mix_GroupNewer(Tag));

        /// <summary>
        /// Creates a group of channels with a tag.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public MixChannelGroup(int tag)
        {
            Tag = tag;
        }

        /// <summary>
        /// Adds a channel to the group.
        /// </summary>
        /// <param name="channel">The channel.</param>
        public void Add(MixChannel channel) =>
            _ = Native.CheckError(Native.Mix_GroupChannel(channel.Index, Tag));

        /// <summary>
        /// Adds a set of channels to the group.
        /// </summary>
        /// <param name="from">The first channel.</param>
        /// <param name="to">The last channel.</param>
        public void Add(MixChannel from, MixChannel to) =>
            _ = Native.CheckError(Native.Mix_GroupChannels(from.Index, to.Index, Tag));

        /// <summary>
        /// Halts the group.
        /// </summary>
        public void Halt() =>
            Native.CheckError(Native.Mix_HaltGroup(Tag));

        /// <summary>
        /// Fades the group out.
        /// </summary>
        /// <param name="ms">The length of the fade.</param>
        /// <returns>The number of channels that will fade out.</returns>
        public int FadeOut(int ms) =>
            Native.Mix_FadeOutGroup(Tag, ms);
    }
}
