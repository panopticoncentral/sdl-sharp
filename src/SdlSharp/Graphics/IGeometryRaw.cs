using System.Runtime.InteropServices;

namespace SdlSharp.Graphics
{

    /// <summary>
    /// An interface implemented by types that support raw geometry rendering.
    /// </summary>
    public interface IGeometryRaw
    {
        /// <summary>
        /// The name of the XY field.
        /// </summary>
        static abstract string FieldXy { get; }

        /// <summary>
        /// The name of the color field.
        /// </summary>
        static abstract string FieldColor { get; }

        /// <summary>
        /// The name of the UV field.
        /// </summary>
        static abstract string FieldUv { get; }

        internal static class GeometryRawInfo<TVertex, TIndex>
            where TVertex : unmanaged, IGeometryRaw
            where TIndex : unmanaged
        {
            public static nint VertexSize { get; } = Marshal.SizeOf<TVertex>();
            public static nint FieldXyOffset { get; } = Marshal.OffsetOf<TVertex>(T.FieldXy);
            public static nint FieldColorOffset { get; } = Marshal.OffsetOf<TVertex>(T.FieldColor);
            public static nint FieldUvOffset { get; } = Marshal.OffsetOf<TVertex>(T.FieldUv);
            public static nint IndexSize { get; } = Marshal.SizeOf<TIndex>();
        }
    }
}
