using System;

// There are going to be unused fields in some of the interop structures
#pragma warning disable CS0169, RCS1213, IDE0051, IDE0052

namespace SdlSharp
{
    /// <summary>
    /// A timer that can be used to track time.
    /// </summary>
    public readonly struct Timer
    {
        private readonly int _id;
        private readonly TimerCallbackDelegate _callback;

        /// <summary>
        /// The number of milliseconds since the SDL library initialization.
        /// </summary>
        public static uint Ticks => Native.SDL_GetTicks();

        /// <summary>
        /// Current value of the high resolution counter.
        /// </summary>
        public static ulong PerformanceCounter => Native.SDL_GetPerformanceCounter();

        /// <summary>
        /// The frequency of the high resolution counter.
        /// </summary>
        public static ulong PerformanceFrequency => Native.SDL_GetPerformanceFrequency();

        private Timer(int id, TimerCallbackDelegate callback)
        {
            _callback = callback;
            _id = id;
        }

        /// <summary>
        /// Delays the specified number of milliseconds.
        /// </summary>
        /// <param name="ms">The number of milliseconds to delay.</param>
        public static void Delay(uint ms) => Native.SDL_Delay(ms);

        /// <summary>
        /// Creates a new timer.
        /// </summary>
        /// <param name="interval">The number of milliseconds to delay until the callback is called.</param>
        /// <param name="callback">The callback to call.</param>
        /// <param name="param">User data to pass the callback.</param>
        /// <returns>The timer.</returns>
        public static Timer Add(uint interval, TimerCallbackDelegate callback, IntPtr param)
        {
            return new Timer(Native.SDL_AddTimer(interval, callback, param).Id, callback);
        }

        /// <summary>
        /// Removes a timer.
        /// </summary>
        /// <returns><c>true</c> if the timer was removed, <c>false</c> otherwise.</returns>
        public bool Remove() => Native.SDL_RemoveTimer(new Native.SDL_TimerID(_id));
    }
}
