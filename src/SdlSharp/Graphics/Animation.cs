namespace SdlSharp.Graphics
{
    /// <summary>
    /// An animated image.
    /// </summary>
    public sealed unsafe class Animation : IDisposable
    {
        private readonly Native.IMG_Animation* _animation;

        /// <summary>
        /// The animation's ID.
        /// </summary>
        public nuint Id => (nuint)_animation;

        /// <summary>
        /// The size of the animation.
        /// </summary>
        public Size Size => new(_animation->w, _animation->h);

        /// <summary>
        /// The animation frames.
        /// </summary>
        public IReadOnlyCollection<(Surface Frame, int Delay)> Frames => Native.GetIndexedCollection(
            i => (new Surface(_animation->frames[i]), _animation->delays[i]), () => _animation->count);

        internal Animation(Native.IMG_Animation* animation)
        {
            _animation = animation;
        }

        /// <summary>
        /// Releases all resources.
        /// </summary>
        public void Dispose() => Native.IMG_FreeAnimation(_animation);
    }
}
