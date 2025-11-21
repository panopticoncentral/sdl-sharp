using Sdl3Sharp.Graphics;

namespace Sdl3Sharp.Input;

/// <summary>
/// Represents data about a single finger in a multitouch event.
/// Each touch event is a collection of fingers that are simultaneously in
/// contact with the touch device.
/// </summary>
/// <param name="Id">The finger ID, valid for the lifetime of a single continuous touch.</param>
/// <param name="Position">The position of the touch event, normalized (0...1).</param>
/// <param name="Pressure">The quantity of pressure applied, normalized (0...1).</param>
public readonly record struct Finger(ulong Id, PointF Position, float Pressure);
