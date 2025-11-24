using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Haptic;

namespace Sdl3Sharp.Input;

/// <summary>
/// Describes a haptic device that is connected to the system but not yet opened.
/// Use <see cref="Open"/> to open the haptic device for use.
/// </summary>
/// <param name="id">The underlying SDL haptic ID.</param>
public readonly unsafe struct HapticDescriptor(SDL_HapticID id)
{
    /// <summary>
    /// Gets the underlying SDL haptic ID.
    /// </summary>
    public SDL_HapticID Id { get; } = id;

    /// <summary>
    /// Gets the implementation dependent name of this haptic device.
    /// </summary>
    public string Name => CheckErrorNull(SDL_GetHapticNameForID(Id));

    /// <summary>
    /// Opens this haptic device for use.
    /// </summary>
    /// <returns>A new Haptic instance.</returns>
    public Haptic Open()
    {
        return new(CheckErrorPointer(SDL_OpenHaptic(Id)), ownsHandle: true);
    }

    /// <summary>
    /// Gets the Haptic associated with this descriptor, if it has been opened.
    /// </summary>
    /// <returns>A Haptic instance if opened, or throws if not opened.</returns>
    public Haptic GetOpened()
    {
        return new(CheckErrorPointer(SDL_GetHapticFromID(Id)), ownsHandle: false);
    }

    /// <summary>
    /// Implicitly converts an SDL_HapticID to a HapticDescriptor.
    /// </summary>
    /// <param name="id">The haptic ID to convert.</param>
    public static implicit operator HapticDescriptor(SDL_HapticID id)
    {
        return new(id);
    }

    /// <summary>
    /// Implicitly converts a HapticDescriptor to an SDL_HapticID.
    /// </summary>
    /// <param name="descriptor">The descriptor to convert.</param>
    public static implicit operator SDL_HapticID(HapticDescriptor descriptor)
    {
        return descriptor.Id;
    }
}
