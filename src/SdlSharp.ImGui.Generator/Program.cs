using System.Text.Json;
using SdlSharp.ImGui.Generator;

const string Namespace = "SdlSharp.ImGui.Native";

// Determine paths
var baseDir = FindSolutionRoot();
var nativeDir = Path.Combine(baseDir, "src", "SdlSharp.ImGui.Native");
var dearBindingsDir = Path.Combine(nativeDir, "dear_bindings");
var outputDir = Path.Combine(baseDir, "src", "SdlSharp.ImGui", "Native");

Console.WriteLine($"Solution root: {baseDir}");
Console.WriteLine($"Dear Bindings dir: {dearBindingsDir}");
Console.WriteLine($"Output dir: {outputDir}");

// Clean and recreate output directories
if (Directory.Exists(outputDir))
{
    Directory.Delete(outputDir, recursive: true);
}
Directory.CreateDirectory(outputDir);
Directory.CreateDirectory(Path.Combine(outputDir, "Enums"));
Directory.CreateDirectory(Path.Combine(outputDir, "Structs"));
Directory.CreateDirectory(Path.Combine(outputDir, "NativeMethods"));

// Load and parse JSON files
var jsonOptions = new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true,
    ReadCommentHandling = JsonCommentHandling.Skip
};

Console.WriteLine("\nLoading dcimgui.json...");
var mainJsonPath = Path.Combine(dearBindingsDir, "dcimgui.json");
var mainRoot = JsonSerializer.Deserialize<DearBindingsRoot>(
    File.ReadAllText(mainJsonPath), jsonOptions)!;

// Load backend JSONs
var backendFiles = new Dictionary<string, string>
{
    ["SDL3"] = Path.Combine(dearBindingsDir, "backends", "dcimgui_impl_sdl3.json"),
    ["SDLGpu3"] = Path.Combine(dearBindingsDir, "backends", "dcimgui_impl_sdlgpu3.json"),
    ["SDLRenderer3"] = Path.Combine(dearBindingsDir, "backends", "dcimgui_impl_sdlrenderer3.json"),
};

var backends = new Dictionary<string, DearBindingsRoot>();
foreach (var (name, path) in backendFiles)
{
    if (File.Exists(path))
    {
        Console.WriteLine($"Loading {name} backend...");
        backends[name] = JsonSerializer.Deserialize<DearBindingsRoot>(
            File.ReadAllText(path), jsonOptions)!;
    }
}

// Initialize type mapper with all type information
var typeMapper = new TypeMapper();
typeMapper.Initialize(mainRoot);
foreach (var backend in backends.Values)
{
    typeMapper.Initialize(backend);
}

// Generate code
var enumGenerator = new EnumGenerator();
var structGenerator = new StructGenerator(typeMapper);
var functionGenerator = new FunctionGenerator(typeMapper);

// === Generate Enums (one file per enum) ===
Console.WriteLine("\nGenerating enums...");
var enumCount = 0;
foreach (var enumInfo in mainRoot.Enums.Where(e => !e.IsInternal))
{
    var cleanName = NamingConventions.CleanEnumName(enumInfo.Name);
    var content = EnumGenerator.GenerateSingleEnum(enumInfo, Namespace);
    var filePath = Path.Combine(outputDir, "Enums", $"{cleanName}.cs");
    File.WriteAllText(filePath, content);
    enumCount++;
}
Console.WriteLine($"  Generated {enumCount} enum files");

// Generate backend enums (one file per enum)
foreach (var (name, backend) in backends)
{
    foreach (var enumInfo in backend.Enums.Where(e => !e.IsInternal))
    {
        var cleanName = NamingConventions.CleanEnumName(enumInfo.Name);
        var content = EnumGenerator.GenerateSingleEnum(enumInfo, Namespace);
        var filePath = Path.Combine(outputDir, "Enums", $"{cleanName}.cs");
        File.WriteAllText(filePath, content);
        enumCount++;
    }
}
Console.WriteLine($"  Generated {backends.Sum(b => b.Value.Enums.Count(e => !e.IsInternal))} backend enum files");

// === Generate Structs (one file per struct) ===
Console.WriteLine("\nGenerating structs...");
var structCount = 0;
foreach (var structInfo in mainRoot.Structs
    .Where(s => !s.ForwardDeclaration && !s.IsInternal && !s.IsAnonymous && s.Fields.Count > 0))
{
    var content = structGenerator.GenerateSingleStruct(structInfo, Namespace);
    var filePath = Path.Combine(outputDir, "Structs", $"{structInfo.Name}.cs");
    File.WriteAllText(filePath, content);
    structCount++;
}
Console.WriteLine($"  Generated {structCount} struct files");

// === Generate Native Methods (one file per class grouping) ===
Console.WriteLine("\nGenerating native methods...");

// Group functions by their class/prefix
var functionGroups = GroupFunctionsByClass(mainRoot.Functions
    .Where(f => !f.IsInternal)
    .Where(f => !f.IsDefaultArgumentHelper)
    .Where(f => !f.IsImstrHelper)
    .Where(f => !f.Arguments.Any(a => a.IsVarargs))
    .Where(f => !typeMapper.FunctionHasUnsupportedTypes(f)));

var totalFuncCount = 0;
foreach (var (className, functions) in functionGroups)
{
    var safeClassName = GetSafeClassName(className);
    var content = functionGenerator.GenerateForClass(functions, Namespace, $"NativeMethods{safeClassName}");
    var filePath = Path.Combine(outputDir, "NativeMethods", $"NativeMethods{safeClassName}.cs");
    File.WriteAllText(filePath, content);
    totalFuncCount += functions.Count;
}
Console.WriteLine($"  Generated {functionGroups.Count} native method files ({totalFuncCount} functions)");

// Generate backend functions (one file per backend)
foreach (var (name, backend) in backends)
{
    if (backend.Functions.Count > 0)
    {
        var backendFunctions = backend.Functions
            .Where(f => !f.IsInternal)
            .Where(f => !f.IsDefaultArgumentHelper)
            .Where(f => !f.IsImstrHelper)
            .Where(f => !f.Arguments.Any(a => a.IsVarargs))
            .Where(f => !typeMapper.FunctionHasUnsupportedTypes(f))
            .ToList();

        if (backendFunctions.Count > 0)
        {
            var content = functionGenerator.GenerateForClass(backendFunctions, Namespace, $"NativeMethods{name}");
            var filePath = Path.Combine(outputDir, "NativeMethods", $"NativeMethods{name}.cs");
            File.WriteAllText(filePath, content);
            Console.WriteLine($"  Generated {backendFunctions.Count} {name} backend functions");
        }
    }
}

Console.WriteLine("\nGeneration complete!");
Console.WriteLine($"Output written to: {outputDir}");

// === Helper Functions ===

static string FindSolutionRoot()
{
    var dir = Directory.GetCurrentDirectory();

    // Walk up until we find the .slnx file
    while (dir != null)
    {
        if (Directory.GetFiles(dir, "*.slnx").Length > 0 ||
            Directory.GetFiles(dir, "*.sln").Length > 0)
        {
            return dir;
        }
        dir = Directory.GetParent(dir)?.FullName;
    }

    throw new InvalidOperationException(
        "Could not find solution root. Please run from the solution directory or a project directory.");
}

static Dictionary<string, List<FunctionInfo>> GroupFunctionsByClass(IEnumerable<FunctionInfo> functions)
{
    var groups = new Dictionary<string, List<FunctionInfo>>();

    foreach (var func in functions)
    {
        var className = GetFunctionClass(func);

        if (!groups.ContainsKey(className))
            groups[className] = [];

        groups[className].Add(func);
    }

    return groups;
}

static string GetFunctionClass(FunctionInfo func)
{
    // If it has an original class, use that
    if (!string.IsNullOrEmpty(func.OriginalClass))
        return func.OriginalClass;

    var name = func.Name;

    // ImGui_ functions -> ImGui
    if (name.StartsWith("ImGui_"))
        return "ImGui";

    // cImGui_ functions -> ImGui
    if (name.StartsWith("cImGui_"))
        return "ImGui";

    // Extract class from pattern like ImDrawList_AddLine -> ImDrawList
    var underscoreIndex = name.IndexOf('_');
    if (underscoreIndex > 0)
    {
        return name[..underscoreIndex];
    }

    // Default to ImGui
    return "ImGui";
}

static string GetSafeClassName(string className)
{
    // Remove Im prefix for cleaner file names, but keep it for ImGui
    if (className == "ImGui")
        return "";

    return className;
}
