using SdlSharp.Graphics;

namespace SdlSharp.Input
{
    /// <summary>
    /// A mouse cursor.
    /// </summary>
    public sealed unsafe class Cursor : IDisposable
    {
        private readonly Native.SDL_Cursor* _cursor;

        /// <summary>
        /// The current cursor.
        /// </summary>
        public static Cursor Current
        {
            get => new(Native.SDL_GetCursor());
            set => Native.SDL_SetCursor(value.ToNative());
        }

        /// <summary>
        /// The default cursor.
        /// </summary>
        public static Cursor Default => new(Native.SDL_GetDefaultCursor());

        /// <summary>
        /// The cursor's ID.
        /// </summary>
        public nuint Id => (nuint)_cursor;

        internal Cursor(Native.SDL_Cursor* cursor)
        {
            _cursor = cursor;
        }

        /// <summary>
        /// Creates a new cursor.
        /// </summary>
        /// <param name="data">The cursor data.</param>
        /// <param name="mask">The cursor mask.</param>
        /// <param name="size">The size of the cursor.</param>
        /// <param name="hotspot">The location of the hotspot.</param>
        /// <returns>The cursor.</returns>
        public static Cursor Create(Span<byte> data, Span<byte> mask, Size size, Point hotspot)
        {
            fixed (byte* dataPointer = data)
            fixed (byte* maskPointer = mask)
            {
                return new(Native.SDL_CreateCursor(dataPointer, maskPointer, size.Width, size.Height, hotspot.X, hotspot.Y));
            }
        }

        /// <summary>
        /// Creates a new cursor.
        /// </summary>
        /// <param name="surface">The surface the cursor comes from.</param>
        /// <param name="hotspot">The location of the hotspot.</param>
        /// <returns>The cursor.</returns>
        public static Cursor Create(Surface surface, Point hotspot) =>
            new(Native.SDL_CreateColorCursor(surface.ToNative(), hotspot.X, hotspot.Y));

        /// <summary>
        /// Creates a new cursor.
        /// </summary>
        /// <param name="systemCursor">The system cursor.</param>
        /// <returns>The cursor.</returns>
        public static Cursor Create(SystemCursor systemCursor) =>
            new(Native.SDL_CreateSystemCursor((Native.SDL_SystemCursor)systemCursor));

        /// <inheritdoc/>
        public void Dispose() => Native.SDL_FreeCursor(_cursor);

        /// <summary>
        /// Shows or hides the cursor.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns>The old state.</returns>
        public static State Show(State state) =>
            (State)Native.CheckError(Native.SDL_ShowCursor((int)state));

        internal Native.SDL_Cursor* ToNative() => _cursor;
    }
}
