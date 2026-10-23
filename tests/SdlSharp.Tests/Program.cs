using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SdlSharp;

if (args.Length is < 1 or > 2) throw new ArgumentException("Pass the output of the native ABI probe compiled against SDL 3.4.18 headers, then an optional native SDL library path.");
var assembly = typeof(Application).Assembly;
int checks = 0, failures = 0;
void Check(bool condition, string message)
{
    Interlocked.Increment(ref checks);
    if (!condition) { Console.Error.WriteLine(message); Interlocked.Increment(ref failures); }
}
foreach (var line in File.ReadLines(args[0]))
{
    var fields = line.Split('|');
    if (fields[0] == "wchar-size")
    {
        Check(SdlSharp.Input.HidText.CharacterSize == int.Parse(fields[2]), "HID wchar_t width matches native compiler");
        continue;
    }
    var type = assembly.GetTypes().Single(t => t.Namespace == "SdlSharp.Native" && t.Name == fields[1]);
    ulong actual;
    if (fields[0] == "field-size")
        actual = type.GetField(fields[2])!.FieldType.IsPointer || type.GetField(fields[2])!.FieldType.IsFunctionPointer ? (ulong)IntPtr.Size : (ulong)(int)typeof(Unsafe).GetMethod(nameof(Unsafe.SizeOf))!.MakeGenericMethod(type.GetField(fields[2])!.FieldType).Invoke(null, null)!;
    else if (fields[0] == "alignment")
        actual = (ulong)(nint)typeof(AbiAlignment).GetMethod("Of")!.MakeGenericMethod(type).Invoke(null, null)!;
    else if (fields[0] == "size")
        actual = (ulong)(int)typeof(Unsafe).GetMethod(nameof(Unsafe.SizeOf))!.MakeGenericMethod(type).Invoke(null, null)!;
    else if (fields[0] == "offset") actual = (ulong)Marshal.OffsetOf(type, fields[2]);
    else
    {
        var value = Enum.Parse(type, fields[2]);
        actual = Enum.GetUnderlyingType(type) == typeof(uint) ? Convert.ToUInt32(value) : unchecked((ulong)Convert.ToInt64(value));
    }
    Check(actual == ulong.Parse(fields[^1]), $"ABI mismatch {line}: managed {actual}");
}
var architecture = RuntimeInformation.ProcessArchitecture.ToString().ToLowerInvariant();
var nativePath = args.Length == 2 ? args[1] : OperatingSystem.IsLinux() ? "libSDL3.so.0" : Path.Combine(AppContext.BaseDirectory, "runtimes", $"{(OperatingSystem.IsMacOS() ? "osx" : "win")}-{architecture}", "native", OperatingSystem.IsMacOS() ? "libSDL3.dylib" : "SDL3.dll");
var library = NativeLibrary.Load(nativePath);
NativeLibrary.SetDllImportResolver(assembly, (name, _, _) => name == "SDL3" ? library : 0);
var exports = new HashSet<string>();
foreach (var type in assembly.GetTypes().Where(t => t.Namespace == "SdlSharp.Native"))
foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.Static))
{
    var import = method.GetCustomAttribute<LibraryImportAttribute>();
    if (import != null && exports.Add(import.EntryPoint ?? method.Name)) Check(NativeLibrary.TryGetExport(library, import.EntryPoint ?? method.Name, out _), $"Missing export {import.EntryPoint}");
}
Check(Sdl.Version == new Version(3,4,18), $"Wrong SDL version: {Sdl.Version}");
Console.WriteLine($"SDL ABI/export checks: {checks}, failures: {failures} ({RuntimeInformation.ProcessArchitecture}).");
var previousChecks = checks;
HighLevelTests.Run(Check);
OwnershipTests.Run(Check);
HidStorageTests.Run(Check);
ReviewSafetyTests.Run(Check);
Console.WriteLine($"Managed checks: {checks - previousChecks}; total failures: {failures}.");
return failures == 0 ? 0 : 1;

internal static class AbiAlignment
{
    private struct Probe<T> where T : unmanaged { public byte First; public T Value; }
    public static nint Of<T>() where T : unmanaged
    {
        var probe = new Probe<T> { First = 0, Value = default };
        return Unsafe.ByteOffset(ref probe.First, ref Unsafe.As<T, byte>(ref probe.Value));
    }
}
