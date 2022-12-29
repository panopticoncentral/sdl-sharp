using SdlSharp.Graphics;

namespace SdlSharp.Input
{
    /// <summary>
    /// Mouse related APIs.
    /// </summary>
    public static unsafe class Mouse
    {
        /// <summary>
        /// The window that has mouse focus, if any.
        /// </summary>
        public static Window? Focus => Window.PointerToInstance(Native.SDL_GetMouseFocus());

        /// <summary>
        /// The state of the mouse.
        /// </summary>
        public static (Point Position, MouseButton Buttons) State
        {
            get
            {
                int x, y;
                var buttons = Native.SDL_GetMouseState(&x, &y);
                return ((x, y), (MouseButton)buttons);
            }
        }

        /// <summary>
        /// The global state of the mouse.
        /// </summary>
        public static (Point Position, MouseButton Buttons) GlobalState
        {
            get
            {
                int x, y;
                var buttons = Native.SDL_GetGlobalMouseState(&x, &y);
                return ((x, y), (MouseButton)buttons);
            }
        }

        /// <summary>
        /// The relative mouse state.
        /// </summary>
        public static (Point Position, MouseButton Buttons) RelativeState
        {
            get
            {
                int x, y;
                var buttons = Native.SDL_GetRelativeMouseState(&x, &y);
                return ((x, y), (MouseButton)buttons);
            }
        }

        /// <summary>
        /// Whether the mouse is in relative mode.
        /// </summary>
        public static bool RelativeMode
        {
            get => Native.SDL_GetRelativeMouseMode();
            set => Native.CheckError(Native.SDL_SetRelativeMouseMode(value));
        }

        /// <summary>
        /// Whether the mouse supports haptic effects.
        /// </summary>
        public static bool IsHaptic =>
            Native.SDL_MouseIsHaptic() != 0;

        /// <summary>
        /// Returns the haptic device for the mouse;
        /// </summary>
        public static Haptic Haptic =>
            new(Native.SDL_HapticOpenFromMouse());

        /// <summary>
        /// Warps the mouse to a location in a window.
        /// </summary>
        /// <param name="window">The window to warp within.</param>
        /// <param name="position">The position to warp to.</param>
        public static void Warp(Window? window, Point position) =>
            Native.SDL_WarpMouseInWindow(window == null ? null : window.Native, position.X, position.Y);

        /// <summary>
        /// Warps the mouse to a location on the screen.
        /// </summary>
        /// <param name="position">The position to warp to.</param>
        public static void Warp(Point position) =>
            Native.CheckError(Native.SDL_WarpMouseGlobal(position.X, position.Y));

        /// <summary>
        /// Captures or un-captures the mouse.
        /// </summary>
        /// <param name="captured">Whether the mouse is captured.</param>
        public static void Capture(bool captured) =>
            Native.CheckError(Native.SDL_CaptureMouse(captured));

        /// <summary>
        /// Event fired when a mouse button is pressed.
        /// </summary>
        public static event EventHandler<MouseButtonEventArgs>? ButtonDown;

        /// <summary>
        /// Event fired when a mouse button is released.
        /// </summary>
        public static event EventHandler<MouseButtonEventArgs>? ButtonUp;

        /// <summary>
        /// Event fired when a mouse moves.
        /// </summary>
        public static event EventHandler<MouseMotionEventArgs>? Motion;

        /// <summary>
        /// Event fired when a mouse wheel moves.
        /// </summary>
        public static event EventHandler<MouseWheelEventArgs>? Wheel;

        internal static void DispatchEvent(Native.SDL_Event e)
        {
            switch ((Native.SDL_EventType)e.type)
            {
                case Native.SDL_EventType.SDL_MOUSEBUTTONDOWN:
                    ButtonDown?.Invoke(null, new MouseButtonEventArgs(e.button));
                    break;

                case Native.SDL_EventType.SDL_MOUSEBUTTONUP:
                    ButtonUp?.Invoke(null, new MouseButtonEventArgs(e.button));
                    break;

                case Native.SDL_EventType.SDL_MOUSEMOTION:
                    Motion?.Invoke(null, new MouseMotionEventArgs(e.motion));
                    break;

                case Native.SDL_EventType.SDL_MOUSEWHEEL:
                    Wheel?.Invoke(null, new MouseWheelEventArgs(e.wheel));
                    break;

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
