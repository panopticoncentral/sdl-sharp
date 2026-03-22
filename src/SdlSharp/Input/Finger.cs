// src/SdlSharp/Input/Finger.cs
namespace SdlSharp.Input;

/// <summary>
/// Data about a single touch finger.
/// </summary>
public readonly record struct Finger(long Id, float X, float Y, float Pressure);
