using System;
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
                var buttons = Native.SDL_GetMouseState(out var x, out var y);
                return ((x, y), buttons);
            }
        }

        /// <summary>
        /// The global state of the mouse.
        /// </summary>
        public static (Point Position, MouseButton Buttons) GlobalState
        {
            get
            {
                var buttons = Native.SDL_GetGlobalMouseState(out var x, out var y);
                return ((x, y), buttons);
            }
        }

        /// <summary>
        /// The relative mouse state.
        /// </summary>
        public static (Point Position, MouseButton Buttons) RelativeState
        {
            get
            {
                var buttons = Native.SDL_GetRelativeMouseState(out var x, out var y);
                return ((x, y), buttons);
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
            Native.SDL_MouseIsHaptic();

        /// <summary>
        /// Returns the haptic device for the mouse;
        /// </summary>
        public static Haptic Haptic =>
            Haptic.PointerToInstanceNotNull(Native.SDL_HapticOpenFromMouse());

        /// <summary>
        /// Warps the mouse to a location in a window.
        /// </summary>
        /// <param name="window">The window to warp within.</param>
        /// <param name="position">The position to warp to.</param>
        public static void Warp(Window? window, Point position) =>
            Native.SDL_WarpMouseInWindow(window == null ? null : window.Pointer, position.X, position.Y);

        /// <summary>
        /// Warps the mouse to a location on the screen.
        /// </summary>
        /// <param name="position">The position to warp to.</param>
        public static void Warp(Point position) =>
            Native.SDL_WarpMouseGlobal(position.X, position.Y);

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
            switch (e.Type)
            {
                case Native.SDL_EventType.MouseButtonDown:
                    ButtonDown?.Invoke(null, new MouseButtonEventArgs(e.Button));
                    break;

                case Native.SDL_EventType.MouseButtonUp:
                    ButtonUp?.Invoke(null, new MouseButtonEventArgs(e.Button));
                    break;

                case Native.SDL_EventType.MouseMotion:
                    Motion?.Invoke(null, new MouseMotionEventArgs(e.Motion));
                    break;

                case Native.SDL_EventType.MouseWheel:
                    Wheel?.Invoke(null, new MouseWheelEventArgs(e.Wheel));
                    break;

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
