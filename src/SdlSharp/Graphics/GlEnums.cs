namespace SdlSharp.Graphics;

/// <summary>
/// An enumeration of OpenGL configuration attributes, used with
/// <see cref="Gl.SetAttribute"/> and <see cref="Gl.GetAttribute"/>.
/// </summary>
public enum GlAttribute
{
    /// <summary>The minimum number of bits for the red channel of the color buffer; defaults to 8.</summary>
    RedSize = Native.SDL_GLAttr.SDL_GL_RED_SIZE,

    /// <summary>The minimum number of bits for the green channel of the color buffer; defaults to 8.</summary>
    GreenSize = Native.SDL_GLAttr.SDL_GL_GREEN_SIZE,

    /// <summary>The minimum number of bits for the blue channel of the color buffer; defaults to 8.</summary>
    BlueSize = Native.SDL_GLAttr.SDL_GL_BLUE_SIZE,

    /// <summary>The minimum number of bits for the alpha channel of the color buffer; defaults to 8.</summary>
    AlphaSize = Native.SDL_GLAttr.SDL_GL_ALPHA_SIZE,

    /// <summary>The minimum number of bits for frame buffer size; defaults to 0.</summary>
    BufferSize = Native.SDL_GLAttr.SDL_GL_BUFFER_SIZE,

    /// <summary>Whether the output is single or double buffered; defaults to double buffering on.</summary>
    DoubleBuffer = Native.SDL_GLAttr.SDL_GL_DOUBLEBUFFER,

    /// <summary>The minimum number of bits in the depth buffer; defaults to 16.</summary>
    DepthSize = Native.SDL_GLAttr.SDL_GL_DEPTH_SIZE,

    /// <summary>The minimum number of bits in the stencil buffer; defaults to 0.</summary>
    StencilSize = Native.SDL_GLAttr.SDL_GL_STENCIL_SIZE,

    /// <summary>The minimum number of bits for the red channel of the accumulation buffer; defaults to 0.</summary>
    AccumRedSize = Native.SDL_GLAttr.SDL_GL_ACCUM_RED_SIZE,

    /// <summary>The minimum number of bits for the green channel of the accumulation buffer; defaults to 0.</summary>
    AccumGreenSize = Native.SDL_GLAttr.SDL_GL_ACCUM_GREEN_SIZE,

    /// <summary>The minimum number of bits for the blue channel of the accumulation buffer; defaults to 0.</summary>
    AccumBlueSize = Native.SDL_GLAttr.SDL_GL_ACCUM_BLUE_SIZE,

    /// <summary>The minimum number of bits for the alpha channel of the accumulation buffer; defaults to 0.</summary>
    AccumAlphaSize = Native.SDL_GLAttr.SDL_GL_ACCUM_ALPHA_SIZE,

    /// <summary>Whether the output is stereo 3D; defaults to off.</summary>
    Stereo = Native.SDL_GLAttr.SDL_GL_STEREO,

    /// <summary>The number of buffers used for multisample anti-aliasing; defaults to 0.</summary>
    MultisampleBuffers = Native.SDL_GLAttr.SDL_GL_MULTISAMPLEBUFFERS,

    /// <summary>The number of samples used around the current pixel used for multisample anti-aliasing.</summary>
    MultisampleSamples = Native.SDL_GLAttr.SDL_GL_MULTISAMPLESAMPLES,

    /// <summary>Set to 1 to require hardware acceleration, set to 0 to force software rendering; defaults to allow either.</summary>
    AcceleratedVisual = Native.SDL_GLAttr.SDL_GL_ACCELERATED_VISUAL,

    /// <summary>Not used (deprecated).</summary>
    RetainedBacking = Native.SDL_GLAttr.SDL_GL_RETAINED_BACKING,

    /// <summary>OpenGL context major version.</summary>
    ContextMajorVersion = Native.SDL_GLAttr.SDL_GL_CONTEXT_MAJOR_VERSION,

    /// <summary>OpenGL context minor version.</summary>
    ContextMinorVersion = Native.SDL_GLAttr.SDL_GL_CONTEXT_MINOR_VERSION,

    /// <summary>Some combination of 0 or more of elements of the <see cref="GlContextFlags"/> enumeration; defaults to 0.</summary>
    ContextFlags = Native.SDL_GLAttr.SDL_GL_CONTEXT_FLAGS,

    /// <summary>Type of GL context (Core, Compatibility, ES). See <see cref="GlProfile"/>; default value depends on platform.</summary>
    ContextProfileMask = Native.SDL_GLAttr.SDL_GL_CONTEXT_PROFILE_MASK,

    /// <summary>OpenGL context sharing; defaults to 0.</summary>
    ShareWithCurrentContext = Native.SDL_GLAttr.SDL_GL_SHARE_WITH_CURRENT_CONTEXT,

    /// <summary>Requests sRGB-capable visual if 1. Defaults to -1 ("don't care"). This is a request; GL drivers might not comply.</summary>
    FramebufferSrgbCapable = Native.SDL_GLAttr.SDL_GL_FRAMEBUFFER_SRGB_CAPABLE,

    /// <summary>Sets the context release behavior. See <see cref="GlContextReleaseBehavior"/>; defaults to Flush.</summary>
    ContextReleaseBehavior = Native.SDL_GLAttr.SDL_GL_CONTEXT_RELEASE_BEHAVIOR,

    /// <summary>Set context reset notification. See <see cref="GlContextResetNotification"/>; defaults to NoNotification.</summary>
    ContextResetNotification = Native.SDL_GLAttr.SDL_GL_CONTEXT_RESET_NOTIFICATION,

    /// <summary>Disable error checking for the context.</summary>
    ContextNoError = Native.SDL_GLAttr.SDL_GL_CONTEXT_NO_ERROR,

    /// <summary>Whether the color buffer should use floating point values; defaults to 0.</summary>
    FloatBuffers = Native.SDL_GLAttr.SDL_GL_FLOATBUFFERS,

    /// <summary>The EGL platform to use.</summary>
    EglPlatform = Native.SDL_GLAttr.SDL_GL_EGL_PLATFORM,
}

/// <summary>
/// Possible values to be set for the <see cref="GlAttribute.ContextProfileMask"/> attribute.
/// </summary>
public enum GlProfile : uint
{
    /// <summary>OpenGL Core Profile context.</summary>
    Core = (uint)Native.SDL_GLProfile.SDL_GL_CONTEXT_PROFILE_CORE,

    /// <summary>OpenGL Compatibility Profile context.</summary>
    Compatibility = (uint)Native.SDL_GLProfile.SDL_GL_CONTEXT_PROFILE_COMPATIBILITY,

    /// <summary>OpenGL ES profile context.</summary>
    Es = (uint)Native.SDL_GLProfile.SDL_GL_CONTEXT_PROFILE_ES,
}

/// <summary>
/// Possible flags to be set for the <see cref="GlAttribute.ContextFlags"/> attribute.
/// </summary>
[Flags]
public enum GlContextFlags : uint
{
    /// <summary>Create a debug context.</summary>
    Debug = (uint)Native.SDL_GLContextFlag.SDL_GL_CONTEXT_DEBUG_FLAG,

    /// <summary>Create a forward-compatible context.</summary>
    ForwardCompatible = (uint)Native.SDL_GLContextFlag.SDL_GL_CONTEXT_FORWARD_COMPATIBLE_FLAG,

    /// <summary>Create a robust-access context.</summary>
    RobustAccess = (uint)Native.SDL_GLContextFlag.SDL_GL_CONTEXT_ROBUST_ACCESS_FLAG,

    /// <summary>Create a context with reset isolation.</summary>
    ResetIsolation = (uint)Native.SDL_GLContextFlag.SDL_GL_CONTEXT_RESET_ISOLATION_FLAG,
}

/// <summary>
/// Possible values to be set for the <see cref="GlAttribute.ContextReleaseBehavior"/> attribute.
/// </summary>
public enum GlContextReleaseBehavior : uint
{
    /// <summary>The context is not flushed on release.</summary>
    None = (uint)Native.SDL_GLContextReleaseFlag.SDL_GL_CONTEXT_RELEASE_BEHAVIOR_NONE,

    /// <summary>The context is flushed on release.</summary>
    Flush = (uint)Native.SDL_GLContextReleaseFlag.SDL_GL_CONTEXT_RELEASE_BEHAVIOR_FLUSH,
}

/// <summary>
/// Possible values to be set for the <see cref="GlAttribute.ContextResetNotification"/> attribute.
/// </summary>
public enum GlContextResetNotification : uint
{
    /// <summary>No reset notification.</summary>
    NoNotification = (uint)Native.SDL_GLContextResetNotification.SDL_GL_CONTEXT_RESET_NO_NOTIFICATION,

    /// <summary>The context is lost on a GPU reset.</summary>
    LoseContext = (uint)Native.SDL_GLContextResetNotification.SDL_GL_CONTEXT_RESET_LOSE_CONTEXT,
}
