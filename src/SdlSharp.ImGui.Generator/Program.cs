using System.Text.Json;
using SdlSharp.ImGui.Generator;

const string Namespace = "SdlSharp.ImGui.Native";
const string BackendsNamespace = "SdlSharp.ImGui.Native.Backends";

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
Directory.CreateDirectory(Path.Combine(outputDir, "Backends"));

// Load and parse JSON files
var jsonOptions = new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true,
    ReadCommentHandling = JsonCommentHandling.Skip
};

Console.WriteLine("\nLoading dcimgui.json...");
var mainJsonPath = Path.Combine(dearBindingsDir, "dcimgui.json");
DearBindingsRoot mainRoot = JsonSerializer.Deserialize<DearBindingsRoot>(
    File.ReadAllText(mainJsonPath), jsonOptions)!;

// Load backend JSONs
var backendFiles = new Dictionary<string, string>
{
    ["SDL3"] = Path.Combine(dearBindingsDir, "backends", "dcimgui_impl_sdl3.json"),
    ["SDLGpu3"] = Path.Combine(dearBindingsDir, "backends", "dcimgui_impl_sdlgpu3.json"),
    ["SDLRenderer3"] = Path.Combine(dearBindingsDir, "backends", "dcimgui_impl_sdlrenderer3.json"),
};

var backends = new Dictionary<string, DearBindingsRoot>();
foreach ((var name, var path) in backendFiles)
{
    if (!File.Exists(path))
    {
        continue;
    }

    Console.WriteLine($"Loading {name} backend...");
    backends[name] = JsonSerializer.Deserialize<DearBindingsRoot>(
        File.ReadAllText(path), jsonOptions)!;
}

// Initialize type mapper with all type information
var typeMapper = new TypeMapper();
typeMapper.Initialize(mainRoot);
foreach (DearBindingsRoot backend in backends.Values)
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
foreach (EnumInfo? enumInfo in mainRoot.Enums.Where(e => !e.IsInternal))
{
    var cleanName = NamingConventions.CleanEnumName(enumInfo.Name);
    var content = EnumGenerator.GenerateSingleEnum(enumInfo, Namespace);
    var filePath = Path.Combine(outputDir, $"{cleanName}.cs");
    File.WriteAllText(filePath, content);
    enumCount++;
}

Console.WriteLine($"  Generated {enumCount} enum files");

// Generate backend enums (one file per enum)
foreach ((var name, DearBindingsRoot? backend) in backends)
{
    foreach (EnumInfo? enumInfo in backend.Enums.Where(e => !e.IsInternal))
    {
        var cleanName = NamingConventions.CleanEnumName(enumInfo.Name);
        var content = EnumGenerator.GenerateSingleEnum(enumInfo, BackendsNamespace);
        var filePath = Path.Combine(outputDir, "Backends", $"{cleanName}.cs");
        File.WriteAllText(filePath, content);
        enumCount++;
    }
}

Console.WriteLine($"  Generated {backends.Sum(b => b.Value.Enums.Count(e => !e.IsInternal))} backend enum files");

// === Generate Structs (one file per struct) ===
Console.WriteLine("\nGenerating structs...");
var structCount = 0;
foreach (StructInfo? structInfo in mainRoot.Structs
    .Where(s => !s.ForwardDeclaration && !s.IsInternal && !s.IsAnonymous && s.Fields.Count > 0)
    .Where(s => !TypeMapper.IsUnsupportedType(s)))
{
    var content = structGenerator.GenerateSingleStruct(structInfo, Namespace);
    var filePath = Path.Combine(outputDir, $"{structInfo.Name}.cs");
    File.WriteAllText(filePath, content);
    structCount++;
}

Console.WriteLine($"  Generated {structCount} struct files");

// === Generate Native Methods (one file per class grouping) ===
Console.WriteLine("\nGenerating native methods...");
GenerateFunctions(mainRoot, typeMapper, functionGenerator, outputDir, Namespace, "ImGui");

// Generate backend functions (one file per backend)
foreach ((var name, DearBindingsRoot? backend) in backends)
{
    GenerateFunctions(backend, typeMapper, functionGenerator, outputDir, BackendsNamespace, name);
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

static void GenerateFunctions(DearBindingsRoot root, TypeMapper typeMapper, FunctionGenerator functionGenerator, string outputDir, string ns, string name)
{
    List<FunctionInfo> functions = [.. root.Functions
    .Where(f => !f.IsInternal)
    .Where(f => !f.IsDefaultArgumentHelper)
    .Where(f => !f.IsImstrHelper)
    .Where(f => !f.Arguments.Any(a => a.IsVarargs))
    .Where(f => !TypeMapper.FunctionHasUnsupportedTypes(f))];

    if (functions.Count == 0)
    {
        return;
    }

    var content = functionGenerator.GenerateForClass(functions, ns, name);
    var filePath = Path.Combine(outputDir, $"{name}.cs");
    File.WriteAllText(filePath, content);

    Console.WriteLine($"  Generated {functions.Count} {name} functions");
}

static Dictionary<string, List<FunctionInfo>> GroupFunctionsByClass(IEnumerable<FunctionInfo> functions)
{
    var groups = new Dictionary<string, List<FunctionInfo>>();

    foreach (FunctionInfo func in functions)
    {
        var className = GetFunctionClass(func);

        if (!groups.ContainsKey(className))
        {
            groups[className] = [];
        }

        groups[className].Add(func);
    }

    return groups;
}

static string GetFunctionClass(FunctionInfo func)
{
    // If it has an original class, use that
    if (!string.IsNullOrEmpty(func.OriginalClass))
    {
        return func.OriginalClass;
    }

    var name = func.Name;

    // ImGui_ functions -> ImGui
    if (name.StartsWith("ImGui_"))
    {
        return "ImGui";
    }

    // cImGui_ functions -> ImGui
    if (name.StartsWith("cImGui_"))
    {
        return "ImGui";
    }

    // Extract class from pattern like ImDrawList_AddLine -> ImDrawList
    var underscoreIndex = name.IndexOf('_');
    if (underscoreIndex > 0)
    {
        return name[..underscoreIndex];
    }

    // Default to ImGui
    return "ImGui";
}