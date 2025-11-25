namespace Sdl3Sharp.Graphics.Gpu;

/// <summary>
/// Specifies the type of a texture.
/// </summary>
public enum GpuTextureType
{
    /// <summary>The texture is a 2-dimensional image.</summary>
    Texture2D,

    /// <summary>The texture is a 2-dimensional array image.</summary>
    Texture2DArray,

    /// <summary>The texture is a 3-dimensional image.</summary>
    Texture3D,

    /// <summary>The texture is a cube image.</summary>
    Cube,

    /// <summary>The texture is a cube array image.</summary>
    CubeArray
}
