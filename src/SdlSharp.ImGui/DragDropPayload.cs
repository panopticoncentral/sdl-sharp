using System.Runtime.InteropServices;
using static SdlSharp.Native.Common;
using static SdlSharp.ImGui.Native;

namespace SdlSharp.Gui;

/// <summary>
/// A handle to an in-flight drag-and-drop payload. Obtain via
/// <see cref="Gui.AcceptDragDropPayload(string, DragDropFlags)"/> or <see cref="Gui.GetDragDropPayload"/>.
/// Data pointers returned by this handle are only valid until the next frame.
/// </summary>
public readonly unsafe struct DragDropPayload
{
    internal readonly void* Handle;

    internal DragDropPayload(void* handle) => Handle = handle;

    /// <summary>True if this handle refers to an active payload.</summary>
    public bool IsValid => Handle != null;

    /// <summary>Raw payload bytes (valid only for the current frame).</summary>
    public ReadOnlySpan<byte> Data
    {
        get
        {
            if (Handle == null) return default;
            var ptr = IGSharp_Payload_GetData(Handle);
            var size = IGSharp_Payload_GetDataSize(Handle);
            return ptr == null ? default : new ReadOnlySpan<byte>(ptr, size);
        }
    }

    /// <summary>The payload type identifier set by the source.</summary>
    public string? DataType => Handle == null ? null : Marshal.PtrToStringUTF8((nint)IGSharp_Payload_GetDataType(Handle));

    /// <summary>Checks whether the payload's type matches <paramref name="type"/>.</summary>
    public bool IsDataType(string type) => Handle != null && IGSharp_Payload_IsDataType(Handle, ToUtf8(type));

    /// <summary>True while the user is hovering without releasing the mouse button yet.</summary>
    public bool IsPreview => Handle != null && IGSharp_Payload_IsPreview(Handle);

    /// <summary>True on the frame the user released the mouse button (delivery frame).</summary>
    public bool IsDelivery => Handle != null && IGSharp_Payload_IsDelivery(Handle);

    /// <summary>Reinterprets the payload data as a value of type <typeparamref name="T"/>. Returns false if the payload is invalid or has the wrong size.</summary>
    public bool TryGetValue<T>(out T value) where T : unmanaged
    {
        if (Handle == null)
        {
            value = default;
            return false;
        }
        var ptr = IGSharp_Payload_GetData(Handle);
        var size = IGSharp_Payload_GetDataSize(Handle);
        if (ptr == null || size != sizeof(T))
        {
            value = default;
            return false;
        }
        value = *(T*)ptr;
        return true;
    }
}
