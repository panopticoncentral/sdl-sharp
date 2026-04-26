namespace SdlSharp.ImGui;

/// <summary>
/// Delegate for supplying plot values on demand. Returns the value at the given
/// zero-based index.
/// </summary>
public delegate float PlotValuesGetter(int index);
