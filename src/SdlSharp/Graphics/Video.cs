using System.Collections.Generic;

namespace SdlSharp.Graphics
{
    /// <summary>
    /// The video subsystem.
    /// </summary>
    public static class Video
    {
        private static ItemCollection<string>? s_drivers;

        /// <summary>
        /// The video drivers in the system.
        /// </summary>
        public static IReadOnlyList<string> Drivers => s_drivers ?? (s_drivers = new ItemCollection<string>(
            Native.SDL_GetVideoDriver,
            Native.SDL_GetNumVideoDrivers));

        /// <summary>
        /// The current driver.
        /// </summary>
        public static string CurrentDriver => Native.SDL_GetCurrentVideoDriver();

        /// <summary>
        /// Initializes the video subsystem.
        /// </summary>
        /// <param name="driver">The video driver to use.</param>
        public static void Init(string driver)
        {
            _ = Native.CheckError(Native.SDL_VideoInit(driver));
        }
        
        /// <summary>
        /// Quits the video subsystem.
        /// </summary>
        public static void Quit()
        {
            Native.SDL_VideoQuit();
        }
    }
}
