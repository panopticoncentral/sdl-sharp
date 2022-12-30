namespace SdlSharp.Graphics
{
    /// <summary>
    /// The default window shape.
    /// </summary>
    public sealed class DefaultWindowShapeMode : WindowShapeMode
    {
        internal override Native.SDL_WindowShapeMode ToNative() =>
            new()
            {
                mode = Native.WindowShapeMode.ShapeModeDefault
            };
    }
}
