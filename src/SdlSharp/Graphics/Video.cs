namespace SdlSharp.Graphics
{
    /// <summary>
    /// The video subsystem.
    /// </summary>
    public static unsafe class Video
    {
        /// <summary>
        /// The video drivers in the system.
        /// </summary>
        public static IReadOnlyList<string> Drivers => Native.GetIndexedCollection(i => Native.Utf8ToString(Native.SDL_GetVideoDriver(i))!, Native.SDL_GetNumVideoDrivers);

        /// <summary>
        /// The current driver.
        /// </summary>
        public static string? CurrentDriver => Native.Utf8ToString(Native.SDL_GetCurrentVideoDriver());

        /// <summary>
        /// Initializes the video subsystem.
        /// </summary>
        /// <param name="driver">The video driver to use.</param>
        public static void Init(string driver)
        {
            fixed (byte* ptr = Native.StringToUtf8(driver))
            {
                _ = Native.CheckError(Native.SDL_VideoInit(ptr));
            }
        }

        /// <summary>
        /// Quits the video subsystem.
        /// </summary>
        public static void Quit() => Native.SDL_VideoQuit();
    }
}
