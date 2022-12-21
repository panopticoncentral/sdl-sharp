namespace SdlSharp.Input
{
    /// <summary>
    /// A description of a haptic effect.
    /// </summary>
    public abstract class HapticEffect
    {
        internal unsafe delegate T NativeHapticAction<T>(Native.SDL_HapticEffect* effect);

        internal abstract T NativeCall<T>(NativeHapticAction<T> call);
    }
}
