namespace Sdl3Sharp.Native;

/// <summary>
/// Common routines and helpers for SDL bindings.
/// </summary>
public unsafe static class Common
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
    public static bool CheckErrorBool(bool returnValue)
    {
        return !returnValue ? throw new SdlException() : returnValue;
    }

    /// <summary>
    /// Check that the return of a method is not an error (i.e. zero).
    /// </summary>
    /// <param name="returnValue">The return value of the API.</param>
    /// <returns>The return value.</returns>
    /// <exception cref="SdlException">Thrown if method returned an error.</exception>
    public static int CheckErrorZero(int returnValue)
    {
        return (returnValue == 0) ? throw new SdlException() : returnValue;
    }

    /// <summary>
    /// Check that the return of a method is not an error (i.e. zero).
    /// </summary>
    /// <param name="returnValue">The return value of the API.</param>
    /// <returns>The return value.</returns>
    /// <exception cref="SdlException">Thrown if method returned an error.</exception>
    public static long CheckErrorZero(long returnValue)
    {
        return (returnValue == 0) ? throw new SdlException() : returnValue;
    }

    /// <summary>
    /// Check that the return of a method is not an error (i.e. zero).
    /// </summary>
    /// <param name="returnValue">The return value of the API.</param>
    /// <returns>The return value.</returns>
    /// <exception cref="SdlException">Thrown if method returned an error.</exception>
    public static uint CheckErrorZero(uint returnValue)
    {
        return (returnValue == 0) ? throw new SdlException() : returnValue;
    }

    /// <summary>
    /// Check that the return of a method is not an error (i.e. zero).
    /// </summary>
    /// <param name="returnValue">The return value of the API.</param>
    /// <returns>The return value.</returns>
    /// <exception cref="SdlException">Thrown if method returned an error.</exception>
    public static nuint CheckErrorZero(nuint returnValue)
    {
        return (returnValue == 0) ? throw new SdlException() : returnValue;
    }

    /// <summary>
    /// Check that the return of a method is not an error (i.e. zero).
    /// </summary>
    /// <param name="returnValue">The return value of the API.</param>
    /// <returns>The return value.</returns>
    /// <exception cref="SdlException">Thrown if method returned an error.</exception>
    public static float CheckErrorZero(float returnValue)
    {
        return (returnValue == 0.0f) ? throw new SdlException() : returnValue;
    }

    /// <summary>
    /// Check that the return of a method is not an error (i.e. -1.0f).
    /// </summary>
    /// <param name="returnValue">The return value of the API.</param>
    /// <returns>The return value.</returns>
    /// <exception cref="SdlException">Thrown if method returned an error.</exception>
    public static float CheckErrorNegativeOne(float returnValue)
    {
        return (returnValue == -1.0f) ? throw new SdlException() : returnValue;
    }

    /// <summary>
    /// Check that the return of a method is not an error (i.e. null).
    /// </summary>
    /// <param name="returnValue">The return value of the API.</param>
    /// <returns>The return value.</returns>
    /// <exception cref="SdlException">Thrown if method returned an error.</exception>
    public static T CheckErrorNull<T>(T? returnValue) where T : class
    {
        return returnValue ?? throw new SdlException();
    }

    /// <summary>
    /// Check that the pointer returned from a method is not null.
    /// </summary>
    /// <param name="returnValue">The return value of the API.</param>
    /// <returns>The return value.</returns>
    /// <exception cref="SdlException">Thrown if method returned an error.</exception>
    public static T** CheckErrorPointer<T>(T** returnValue) where T : unmanaged
    {
        return (returnValue == null) ? throw new SdlException() : returnValue;
    }

    /// <summary>
    /// Check that the pointer returned from a method is not null.
    /// </summary>
    /// <param name="returnValue">The return value of the API.</param>
    /// <returns>The return value.</returns>
    /// <exception cref="SdlException">Thrown if method returned an error.</exception>
    public static T* CheckErrorPointer<T>(T* returnValue) where T : unmanaged
    {
        return (returnValue == null) ? throw new SdlException() : returnValue;
    }

    /// <summary>
    /// Check that the pointer returned from a method is not null.
    /// </summary>
    /// <param name="returnValue">The return value of the API.</param>
    /// <returns>The return value.</returns>
    /// <exception cref="SdlException">Thrown if method returned an error.</exception>
    public static void* CheckErrorPointer(void* returnValue)
    {
        return (returnValue == null) ? throw new SdlException() : returnValue;
    }
}