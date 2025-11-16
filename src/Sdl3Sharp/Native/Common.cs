namespace Sdl3Sharp.Native;

/// <summary>
/// Common routines and helpers for SDL bindings.
/// </summary>
public static class Common
{
    /// <summary>
    /// The name of the SDL3 native library.
    /// </summary>
    public const string Sdl3 = "SDL3";

    /// <summary>
    /// Check that the return of a method is not an error (i.e. false).
    /// </summary>
    /// <param name="returnValue">The return value of the API.</param>
    /// <returns>The return value.</returns>
    /// <exception cref="SdlException">Thrown if method returned an error.</exception>
    public static bool CheckError(bool returnValue)
    {
        return !returnValue ? throw new SdlException() : returnValue;
    }
}