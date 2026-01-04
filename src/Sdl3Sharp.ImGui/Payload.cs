using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Wrapper for ImGui drag and drop payload data.
/// </summary>
/// <remarks>
/// This struct provides access to the payload data during drag and drop operations.
/// Use <see cref="Widgets.AcceptDragDropPayload"/> or <see cref="Widgets.GetDragDropPayload"/> to obtain a payload.
/// </remarks>
public unsafe readonly ref struct Payload
{
    private readonly ImGuiPayload* _payload;

    /// <summary>
    /// Gets the identifier for the 3-component floating-point color type as a read-only span of UTF-8 bytes.
    /// </summary>
    public static ReadOnlySpan<byte> TypeColor3F => "_COL3F"u8;

    /// <summary>
    /// Gets the identifier for the 4-component floating-point color type as a read-only span of UTF-8 bytes.
    /// </summary>
    public static ReadOnlySpan<byte> TypeColor4F => "_COL4F"u8;

    internal Payload(ImGuiPayload* payload)
    {
        _payload = payload;
    }

    /// <summary>
    /// Gets a value indicating whether the payload is valid (non-null).
    /// </summary>
    public bool IsValid => _payload != null;

    /// <summary>
    /// Gets the payload data as a span of bytes.
    /// </summary>
    public ReadOnlySpan<byte> Data => new(_payload->Data, _payload->DataSize);

    /// <summary>
    /// Gets the payload data as a value of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The unmanaged type to interpret the data as.</typeparam>
    /// <returns>The payload data interpreted as type <typeparamref name="T"/>.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the payload data size does not match the size of <typeparamref name="T"/>.</exception>
    public T GetData<T>() where T : unmanaged
    {
        return _payload->DataSize != sizeof(T)
            ? throw new InvalidOperationException($"Payload data size ({_payload->DataSize}) does not match requested type size ({sizeof(T)}).")
            : *(T*)_payload->Data;
    }

    /// <summary>
    /// Checks if the payload is a preview (mouse button is still being held).
    /// </summary>
    /// <returns>True if this is a preview, false if the payload has been delivered.</returns>
    /// <remarks>
    /// When using <see cref="DragDropFlags.AcceptBeforeDelivery"/>, you can use this to differentiate
    /// between preview and actual delivery of the payload.
    /// </remarks>
    public bool IsPreview()
    {
        return ImGuiPayload.IsPreview(_payload);
    }

    /// <summary>
    /// Checks if the payload is being delivered (mouse button was released).
    /// </summary>
    /// <returns>True if the payload is being delivered, false if this is just a preview.</returns>
    /// <remarks>
    /// When using <see cref="DragDropFlags.AcceptBeforeDelivery"/>, you can use this to differentiate
    /// between preview and actual delivery of the payload.
    /// </remarks>
    public bool IsDelivery()
    {
        return ImGuiPayload.IsDelivery(_payload);
    }

    /// <summary>
    /// Checks if the payload is of a specific type.
    /// </summary>
    /// <param name="type">The type string to check against (max 32 characters).</param>
    /// <returns>True if the payload matches the specified type.</returns>
    public bool IsDataType(ReadOnlySpan<byte> type)
    {
        fixed (byte* typePtr = type)
        {
            return ImGuiPayload.IsDataType(_payload, typePtr);
        }
    }

    /// <summary>
    /// Clears the payload.
    /// </summary>
    public void Clear()
    {
        ImGuiPayload.Clear(_payload);
    }
}
