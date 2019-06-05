using System;

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
            switch (mode.Mode)
            {
                case Native.WindowShapeMode.Default:
                    return new DefaultWindowShapeMode();

                case Native.WindowShapeMode.BinarizeAlpha:
                    return new BinarizeAlphaWindowShapeMode(false, mode.Parameters._binarizationCutoff);

                case Native.WindowShapeMode.ReverseBinarizeAlpha:
                    return new BinarizeAlphaWindowShapeMode(true, mode.Parameters._binarizationCutoff);

                case Native.WindowShapeMode.ColorKey:
                    return new ColorKeyWindowShapeMode(mode.Parameters._colorKey);

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
