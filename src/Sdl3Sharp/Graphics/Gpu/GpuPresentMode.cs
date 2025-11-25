namespace Sdl3Sharp.Graphics.Gpu;

/// <summary>
/// Specifies the timing that will be used to present swapchain textures to the OS.
/// </summary>
public enum GpuPresentMode
{
    /// <summary>Waits for vblank before presenting. No tearing is possible.</summary>
    VSync,

    /// <summary>Immediately presents. Lowest latency option, but tearing may occur.</summary>
    Immediate,

    /// <summary>Waits for vblank before presenting. Similar to VSYNC, but with reduced visual latency.</summary>
    Mailbox
}
