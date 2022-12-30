namespace SdlSharp.Graphics
{
    /// <summary>
    /// A shaping for a window.
    /// </summary>
    public abstract class WindowShapeMode
    {
        internal abstract Native.SDL_WindowShapeMode ToNative();

        internal static WindowShapeMode FromNative(Native.SDL_WindowShapeMode mode)
        {
            return mode.mode switch
            {
                Native.WindowShapeMode.ShapeModeDefault => new DefaultWindowShapeMode(),

                Native.WindowShapeMode.ShapeModeBinarizeAlpha => new BinarizeAlphaWindowShapeMode(false, mode.parameters.binarizationCutoff),

                Native.WindowShapeMode.ShapeModeReverseBinarizeAlpha => new BinarizeAlphaWindowShapeMode(true, mode.parameters.binarizationCutoff),

                Native.WindowShapeMode.ShapeModeColorKey => new ColorKeyWindowShapeMode(new(mode.parameters.colorKey)),

                _ => throw new InvalidOperationException(),
            };
        }
    }
}
