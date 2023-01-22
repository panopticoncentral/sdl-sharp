using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SdlSharp
{
    /// <summary>
    /// A timer that can be used to track time.
    /// </summary>
    public readonly unsafe struct Timer : IDisposable
    {
        private static Dictionary<nuint, Timer>? s_timers;
        private static Dictionary<nuint, Timer> Timers => s_timers ??= new Dictionary<nuint, Timer>();

        private readonly Native.SDL_TimerID _id;
        private readonly Func<int, int> _callback;

        /// <summary>
        /// The number of milliseconds since the SDL library initialization.
        /// </summary>
        public static ulong Ticks => Native.SDL_GetTicks64();

        /// <summary>
        /// Current value of the high resolution counter.
        /// </summary>
        public static ulong PerformanceCounter => Native.SDL_GetPerformanceCounter();

        /// <summary>
        /// The frequency of the high resolution counter.
        /// </summary>
        public static ulong PerformanceFrequency => Native.SDL_GetPerformanceFrequency();

        private Timer(Native.SDL_TimerID id, Func<int, int> callback)
        {
            _id = id;
            _callback = callback;
            Timers[(nuint)_callback.GetHashCode()] = this;
        }

        /// <summary>
        /// Delays the specified number of milliseconds.
        /// </summary>
        /// <param name="ms">The number of milliseconds to delay.</param>
        public static void Delay(uint ms) => Native.SDL_Delay(ms);

        [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        private static unsafe int TimerCallback(int interval, nuint userData) => Timers.TryGetValue(userData, out var instance) ? instance._callback(interval) : 0;

        /// <summary>
        /// Creates a new timer.
        /// </summary>
        /// <param name="interval">The number of milliseconds to delay until the callback is called.</param>
        /// <param name="callback">The callback to call.</param>
        /// <returns>The timer.</returns>
        public static Timer Create(uint interval, Func<int, int> callback) => new(Native.SDL_AddTimer(interval, &TimerCallback, callback.GetHashCode()), callback);

        /// <summary>
        /// Removes a timer.
        /// </summary>
        /// <returns><c>true</c> if the timer was removed, <c>false</c> otherwise.</returns>
        public bool Remove() => Native.SDL_RemoveTimer(_id);

        /// <summary>
        /// Disposes a timer.
        /// </summary>
        public void Dispose() => Remove();
    }
}

