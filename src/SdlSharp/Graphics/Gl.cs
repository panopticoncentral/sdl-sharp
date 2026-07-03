using static SdlSharp.Native.Common;
using static SdlSharp.Native.Video;

namespace SdlSharp.Graphics;

/// <summary>
/// Static functions for managing the OpenGL library and contexts.
/// </summary>
public static unsafe class Gl
{
    /// <summary>
    /// Dynamically loads an OpenGL library, or the platform default when <paramref name="path"/>
    /// is <c>null</c>. This should be called before creating any OpenGL windows.
    /// </summary>
    /// <param name="path">The platform-dependent library name, or <c>null</c> for the default.</param>
    public static void LoadLibrary(string? path) => Check(SDL_GL_LoadLibrary(ToUtf8(path)));

    /// <summary>
    /// Unloads the OpenGL library previously loaded by <see cref="LoadLibrary"/>.
    /// </summary>
    public static void UnloadLibrary() => SDL_GL_UnloadLibrary();

    /// <summary>
    /// Gets an OpenGL function by name. Returns <see cref="nint.Zero"/> if the function is not found.
    /// </summary>
    /// <param name="name">The name of the OpenGL function.</param>
    /// <returns>A pointer to the function, or <see cref="nint.Zero"/> if not found.</returns>
    public static nint GetProcAddress(string name) => SDL_GL_GetProcAddress(ToUtf8(name));

    /// <summary>
    /// Gets whether an OpenGL extension is supported by the current context.
    /// </summary>
    /// <param name="extension">The name of the extension.</param>
    /// <returns>true if the extension is supported.</returns>
    public static bool ExtensionSupported(string extension) => SDL_GL_ExtensionSupported(ToUtf8(extension));

    /// <summary>
    /// Resets all previously set OpenGL context attributes to their default values.
    /// </summary>
    public static void ResetAttributes() => SDL_GL_ResetAttributes();

    /// <summary>
    /// Sets an OpenGL window attribute before window creation.
    /// </summary>
    /// <param name="attribute">The attribute to set.</param>
    /// <param name="value">The desired value for the attribute.</param>
    public static void SetAttribute(GlAttribute attribute, int value) =>
        Check(SDL_GL_SetAttribute((Native.SDL_GLAttr)attribute, value));

    /// <summary>
    /// Gets the actual value for an OpenGL attribute from the current context.
    /// </summary>
    /// <param name="attribute">The attribute to query.</param>
    /// <returns>The current value of the attribute.</returns>
    public static int GetAttribute(GlAttribute attribute)
    {
        int value;
        Check(SDL_GL_GetAttribute((Native.SDL_GLAttr)attribute, &value));
        return value;
    }

    /// <summary>
    /// Gets or sets the swap interval for the current OpenGL context. A value of 0 disables
    /// vsync (immediate updates), 1 enables vsync, and -1 requests adaptive vsync (updates
    /// synchronized with the vertical retrace, but the swap happens immediately if a vertical
    /// retrace was already missed). Setting -1 throws <see cref="SdlException"/> when adaptive
    /// vsync is not supported by the platform.
    /// </summary>
    public static int SwapInterval
    {
        get
        {
            int interval;
            Check(SDL_GL_GetSwapInterval(&interval));
            return interval;
        }
        set => Check(SDL_GL_SetSwapInterval(value));
    }
}
