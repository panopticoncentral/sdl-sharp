// SystemInfo sample — queries system information, CPU features, power state,
// locale, filesystem paths, timer, clipboard, hints, and logging.
// Exercises the Phase 5a high-level wrappers without needing a window.

using SdlSharp;

using var app = new Application(InitFlags.Events | InitFlags.Video);

// --- CPU ---
Console.WriteLine("=== CPU ===");
Console.WriteLine($"  Logical cores: {SystemInfo.LogicalCpuCores}");
Console.WriteLine($"  Cache line size: {SystemInfo.CpuCacheLineSize} bytes");
Console.WriteLine($"  System RAM: {SystemInfo.SystemRam} MB");
Console.WriteLine($"  Page size: {SystemInfo.SystemPageSize} bytes");
Console.WriteLine($"  SIMD alignment: {SystemInfo.SimdAlignment} bytes");

Console.WriteLine("  SIMD features:");
if (SystemInfo.HasSSE) Console.WriteLine("    SSE");
if (SystemInfo.HasSSE2) Console.WriteLine("    SSE2");
if (SystemInfo.HasSSE3) Console.WriteLine("    SSE3");
if (SystemInfo.HasSSE41) Console.WriteLine("    SSE4.1");
if (SystemInfo.HasSSE42) Console.WriteLine("    SSE4.2");
if (SystemInfo.HasAVX) Console.WriteLine("    AVX");
if (SystemInfo.HasAVX2) Console.WriteLine("    AVX2");
if (SystemInfo.HasAVX512F) Console.WriteLine("    AVX-512F");
if (SystemInfo.HasAltiVec) Console.WriteLine("    AltiVec");
if (SystemInfo.HasARMSIMD) Console.WriteLine("    ARM SIMD");
if (SystemInfo.HasNEON) Console.WriteLine("    NEON");
if (SystemInfo.HasLSX) Console.WriteLine("    LSX");
if (SystemInfo.HasLASX) Console.WriteLine("    LASX");

// --- Power ---
Console.WriteLine();
Console.WriteLine("=== Power ===");
var power = PowerInfo.Get();
Console.WriteLine($"  State: {power.State}");
Console.WriteLine($"  Battery: {(power.BatteryPercent >= 0 ? $"{power.BatteryPercent}%" : "N/A")}");
Console.WriteLine($"  Time left: {(power.BatterySeconds >= 0 ? $"{power.BatterySeconds}s" : "N/A")}");

// --- Locale ---
Console.WriteLine();
Console.WriteLine("=== Locale ===");
var locales = LocaleInfo.GetPreferred();
if (locales.Length == 0)
{
    Console.WriteLine("  (no locale info)");
}
else
{
    foreach (var locale in locales)
        Console.WriteLine($"  {locale.Language}{(locale.Country != null ? $"-{locale.Country}" : "")}");
}

// --- Filesystem ---
Console.WriteLine();
Console.WriteLine("=== Filesystem ===");
Console.WriteLine($"  Base path: {SystemInfo.BasePath}");
Console.WriteLine($"  Pref path: {SystemInfo.GetPrefPath("SdlSharp", "SystemInfoSample")}");
Console.WriteLine($"  Current dir: {SystemInfo.GetCurrentDirectory()}");
Console.WriteLine($"  Home: {SystemInfo.GetUserFolder(SystemFolder.Home)}");
Console.WriteLine($"  Documents: {SystemInfo.GetUserFolder(SystemFolder.Documents)}");
Console.WriteLine($"  Downloads: {SystemInfo.GetUserFolder(SystemFolder.Downloads)}");

// --- Timer ---
Console.WriteLine();
Console.WriteLine("=== Timer ===");
Console.WriteLine($"  Ticks (ms): {SdlTimer.Ticks}");
Console.WriteLine($"  Ticks (ns): {SdlTimer.TicksNS}");
Console.WriteLine($"  Perf counter: {SdlTimer.PerformanceCounter}");
Console.WriteLine($"  Perf frequency: {SdlTimer.PerformanceFrequency}");
Console.Write("  Delaying 100ms... ");
var before = SdlTimer.Ticks;
SdlTimer.Delay(100);
Console.WriteLine($"elapsed: {SdlTimer.Ticks - before}ms");

// --- Clipboard ---
Console.WriteLine();
Console.WriteLine("=== Clipboard (text) ===");
Console.WriteLine($"  Has text: {Clipboard.HasText}");
var originalText = Clipboard.Text;
Console.WriteLine($"  Current text: {(originalText?.Length > 50 ? originalText[..50] + "..." : originalText ?? "(empty)")}");
Clipboard.Text = "Hello from SdlSharp!";
Console.WriteLine($"  After set: {Clipboard.Text}");
// Restore original
Clipboard.Text = originalText;

Console.WriteLine();
Console.WriteLine("=== Clipboard (MIME data) ===");
var testData = "SdlSharp clipboard data test"u8.ToArray();
Clipboard.SetData(mimeType =>
{
    if (mimeType == "text/plain")
        return testData;
    return ReadOnlySpan<byte>.Empty;
}, "text/plain");
Console.WriteLine($"  Has text/plain: {Clipboard.HasData("text/plain")}");
Console.WriteLine($"  Has image/png: {Clipboard.HasData("image/png")}");
var mimeTypes = Clipboard.GetMimeTypes();
Console.WriteLine($"  Available MIME types: [{string.Join(", ", mimeTypes)}]");
var data = Clipboard.GetData("text/plain");
Console.WriteLine($"  Got {data.Length} bytes: {System.Text.Encoding.UTF8.GetString(data)}");
Clipboard.ClearData();
Console.WriteLine($"  After clear, has text/plain: {Clipboard.HasData("text/plain")}");
// Restore original text
Clipboard.Text = originalText;

// --- Hints ---
Console.WriteLine();
Console.WriteLine("=== Hints ===");
SdlHints.Set("SDL_TEST_HINT", "42");
Console.WriteLine($"  Set SDL_TEST_HINT=42, Get: {SdlHints.Get("SDL_TEST_HINT")}");
Console.WriteLine($"  GetBoolean: {SdlHints.GetBoolean("SDL_TEST_HINT")}");
SdlHints.Reset("SDL_TEST_HINT");
Console.WriteLine($"  After reset: {SdlHints.Get("SDL_TEST_HINT") ?? "(null)"}");

// --- Logging ---
Console.WriteLine();
Console.WriteLine("=== Logging ===");
var logMessages = new List<string>();
SdlLog.SetOutputFunction((category, priority, message) =>
{
    logMessages.Add($"[{category}/{priority}] {message}");
});

SdlLog.SetPriority(LogCategory.Application, LogPriority.Trace);
Console.WriteLine($"  App priority: {SdlLog.GetPriority(LogCategory.Application)}");

// Reset to default so we don't capture internal SDL logs later
SdlLog.SetOutputFunction(null);
SdlLog.ResetPriorities();

if (logMessages.Count > 0)
{
    Console.WriteLine($"  Captured {logMessages.Count} log message(s):");
    foreach (var msg in logMessages)
        Console.WriteLine($"    {msg}");
}
else
{
    Console.WriteLine("  (no log messages captured during test)");
}

Console.WriteLine();
Console.WriteLine("Done.");
