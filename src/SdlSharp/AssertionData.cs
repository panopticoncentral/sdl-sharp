namespace SdlSharp;

/// <summary>
/// Information about an SDL assertion failure.
/// </summary>
/// <param name="AlwaysIgnore">Whether the app should always continue when this assertion is triggered.</param>
/// <param name="TriggerCount">Number of times this assertion has been triggered.</param>
/// <param name="Condition">The assertion's test code as a string.</param>
/// <param name="Filename">The source file where the assertion lives.</param>
/// <param name="LineNumber">The line number where the assertion lives.</param>
/// <param name="Function">The function name where the assertion lives.</param>
public readonly record struct AssertionData(
    bool AlwaysIgnore,
    uint TriggerCount,
    string? Condition,
    string? Filename,
    int LineNumber,
    string? Function);
