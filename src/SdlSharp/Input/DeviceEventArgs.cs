namespace SdlSharp.Input;

/// <summary>Event data for keyboard/mouse device add/remove events.</summary>
/// <param name="Which">The instance ID of the device.</param>
public readonly record struct InputDeviceEventArgs(uint Which);

/// <summary>Event data for text-editing (IME composition) events.</summary>
/// <param name="WindowId">The window with keyboard focus.</param>
/// <param name="Text">The current composition text.</param>
/// <param name="Start">The start cursor position, in UTF-8 bytes.</param>
/// <param name="Length">The selection length, in UTF-8 bytes.</param>
public readonly record struct TextEditingEventArgs(uint WindowId, string? Text, int Start, int Length);

/// <summary>Event data for IME candidate-list events.</summary>
/// <param name="WindowId">The window with keyboard focus.</param>
/// <param name="Candidates">The candidate strings.</param>
/// <param name="SelectedCandidate">The index of the selected candidate, or -1 if none.</param>
/// <param name="Horizontal">True if the candidate list is horizontal.</param>
public readonly record struct TextEditingCandidatesEventArgs(
    uint WindowId, string?[] Candidates, int SelectedCandidate, bool Horizontal);
