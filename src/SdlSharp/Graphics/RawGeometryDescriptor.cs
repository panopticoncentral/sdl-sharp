using System.Runtime.InteropServices;

namespace SdlSharp.Graphics
{

    /// <summary>
    /// An interface implemented by types that support raw geometry rendering.
    /// </summary>
    /// <param name="FieldXy">The name of the XY field.</param>
    /// <param name="FieldColor">The name of the Color field.</param>
    /// <param name="FieldUv">The namve of the UV field.</param>
    public readonly record struct RawGeometryDescriptor<TVertex, TIndex>(string FieldXy, string FieldColor, string FieldUv)
        where TVertex : unmanaged
        where TIndex : unmanaged
    {
        internal nint VertexSize { get; } = Marshal.SizeOf<TVertex>();
        internal nint FieldXyOffset { get; } = Marshal.OffsetOf<TVertex>(FieldXy);
        internal nint FieldColorOffset { get; } = Marshal.OffsetOf<TVertex>(FieldColor);
        internal nint FieldUvOffset { get; } = Marshal.OffsetOf<TVertex>(FieldUv);
        internal nint IndexSize { get; } = Marshal.SizeOf<TIndex>();
    }
}
