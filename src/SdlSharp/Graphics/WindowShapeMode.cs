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
            return mode.Mode switch
            {
                Native.WindowShapeMode.Default => new DefaultWindowShapeMode(),

                Native.WindowShapeMode.BinarizeAlpha => new BinarizeAlphaWindowShapeMode(false, mode.Parameters._binarizationCutoff),

                Native.WindowShapeMode.ReverseBinarizeAlpha => new BinarizeAlphaWindowShapeMode(true, mode.Parameters._binarizationCutoff),

                Native.WindowShapeMode.ColorKey => new ColorKeyWindowShapeMode(mode.Parameters._colorKey),

                _ => throw new InvalidOperationException(),
            };
        }
    }
}
