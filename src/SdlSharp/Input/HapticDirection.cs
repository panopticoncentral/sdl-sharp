namespace SdlSharp.Input
{
    /// <summary>
    /// A direction indication for a haptic effect.
    /// </summary>
    public abstract class HapticDirection
    {
        internal abstract Native.SDL_HapticDirection ToNative();
    }
}
