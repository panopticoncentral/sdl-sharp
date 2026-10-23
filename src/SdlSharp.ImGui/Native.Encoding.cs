using System.Text;
namespace SdlSharp.ImGui;
internal static unsafe partial class Native
{
    internal static byte[] ToUtf8Range(string? text) => text is null ? [] : Encoding.UTF8.GetBytes(text);
}
