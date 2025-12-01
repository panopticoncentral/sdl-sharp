namespace Sdl3Sharp.Graphics;

/// <summary>
/// Event arguments for render events.
/// </summary>
public sealed class RenderEventArgs : SdlEventArgs
{
    internal RenderEventArgs(ulong timestamp) : base(timestamp)
    {
    }
}
