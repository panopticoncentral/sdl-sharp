namespace SdlSharp.Graphics;

/// <summary>
/// Window flash operation.
/// </summary>
public enum FlashOperation
{
    /// <summary>Cancel any window flash state.</summary>
    Cancel = (int)Native.SDL_FlashOperation.SDL_FLASH_CANCEL,

    /// <summary>Flash the window briefly to get attention.</summary>
    Briefly = (int)Native.SDL_FlashOperation.SDL_FLASH_BRIEFLY,

    /// <summary>Flash the window until it gets focus.</summary>
    UntilFocused = (int)Native.SDL_FlashOperation.SDL_FLASH_UNTIL_FOCUSED,
}
