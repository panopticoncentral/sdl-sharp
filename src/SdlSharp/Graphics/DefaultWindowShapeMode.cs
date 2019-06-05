namespace SdlSharp.Graphics
{
    /// <summary>
    /// The default window shape.
    /// </summary>
    public sealed class DefaultWindowShapeMode : WindowShapeMode
    {
        internal override Native.SDL_WindowShapeMode ToNative() =>
            new Native.SDL_WindowShapeMode(Native.WindowShapeMode.Default);
    }
}
