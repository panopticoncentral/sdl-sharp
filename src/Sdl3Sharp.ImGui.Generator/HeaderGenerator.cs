using System.Text.Json;

namespace Sdl3Sharp.ImGui.Generator;

public static class HeaderGenerator
{
    private static readonly JsonSerializerOptions jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        ReadCommentHandling = JsonCommentHandling.Skip
    };

    public static TypeMapper Generate(string path, string ns, string outputDir, string className, HashSet<string> excludedFunctions, TypeMapper? mainTypes)
    {
        // Load and parse JSON file
        Console.WriteLine($"\nLoading {path}...");
        DearBindingsRoot root = JsonSerializer.Deserialize<DearBindingsRoot>(
            File.ReadAllText(path), jsonOptions)!;

        // Initialize type mapper with all type information
        var typeMapper = new TypeMapper(mainTypes);
        typeMapper.Initialize(root);

        // Generate code
        var enumGenerator = new EnumGenerator();
        var structGenerator = new StructGenerator(typeMapper);
        var functionGenerator = new FunctionGenerator(typeMapper);
        var typedefGenerator = new TypedefGenerator(typeMapper);

        // === Generate Enums (one file per enum) ===
        Console.WriteLine("\nGenerating enums...");
        var enumCount = 0;
        foreach (EnumInfo? enumInfo in root.Enums.Where(e => !e.IsInternal))
        {
            var cleanName = NamingConventions.CleanEnumName(enumInfo.Name);
            var content = EnumGenerator.GenerateSingleEnum(enumInfo, ns);
            var filePath = Path.Combine(outputDir, $"{cleanName}.cs");
            File.WriteAllText(filePath, content);
            enumCount++;
        }

        Console.WriteLine($"  Generated {enumCount} enum files");

        // === Generate Typedef Wrapper Structs (one file per typedef) ===
        Console.WriteLine("\nGenerating typedef wrapper structs...");
        var typedefCount = 0;
        foreach (TypedefInfo typedefInfo in root.Typedefs
            .Where(t => !t.IsInternal)
            .Where(t => mainTypes == null || !mainTypes.TypeDefs.Contains(t.Name)))
        {
            if (TypedefGenerator.WrapperTypedefs.TryGetValue(typedefInfo.Name, out var underlyingType))
            {
                var content = TypedefGenerator.GenerateSingleTypedef(typedefInfo, underlyingType, ns);
                var filePath = Path.Combine(outputDir, $"{typedefInfo.Name}.cs");
                File.WriteAllText(filePath, content);
                typedefCount++;
            }
        }

        Console.WriteLine($"  Generated {typedefCount} typedef wrapper struct files");

        // === Generate Callback Typedef Wrapper Structs (one file per callback) ===
        Console.WriteLine("\nGenerating callback typedef wrapper structs...");
        var callbackCount = 0;
        foreach (TypedefInfo typedefInfo in root.Typedefs.Where(t => !t.IsInternal))
        {
            if (TypedefGenerator.CallbackTypedefs.Contains(typedefInfo.Name))
            {
                var content = typedefGenerator.GenerateCallbackTypedef(typedefInfo, ns);
                var filePath = Path.Combine(outputDir, $"{typedefInfo.Name}.cs");
                File.WriteAllText(filePath, content);
                callbackCount++;
            }
        }

        Console.WriteLine($"  Generated {callbackCount} callback typedef wrapper struct files");

        // === Generate Opaque Handle Wrapper Structs (one file per type) ===
        Console.WriteLine("\nGenerating opaque handle wrapper structs...");
        var opaqueHandleCount = 0;
        foreach (var handleName in typeMapper.OpaqueStructs.Where(s => mainTypes == null || !mainTypes.Structs.Contains(s)))
        {
            var content = TypedefGenerator.GenerateOpaqueHandleWrapper(handleName, ns);
            var filePath = Path.Combine(outputDir, $"{handleName}.cs");
            File.WriteAllText(filePath, content);
            opaqueHandleCount++;
        }

        Console.WriteLine($"  Generated {opaqueHandleCount} opaque handle wrapper struct files");

        // === Collect and filter all functions ===
        // Get all valid functions (filtered by common criteria)
        List<FunctionInfo> allFunctions = [.. root.Functions
            .Where(f => !f.IsInternal)
            .Where(f => !f.IsDefaultArgumentHelper)
            .Where(f => !f.IsImstrHelper)
            .Where(f => !f.Name.Contains("__"))
            .Where(f => !f.Arguments.Any(a => a.IsVarargs))
            .Where(f => !TypeMapper.FunctionHasUnsupportedTypes(f))
            .Where(f => !Conditional.IsObsolete(f.Conditionals))
            .Where(f => !excludedFunctions.Contains(f.Name))];

        // Group struct methods by their original class
        var structMethodsByClass = allFunctions
            .Where(f => !string.IsNullOrEmpty(f.OriginalClass) && f.Arguments.Any(a => a.IsInstancePointer))
            .GroupBy(f => f.OriginalClass!)
            .ToDictionary(g => g.Key, g => g.ToList());

        // Get struct names for filtering
        var structNames = root.Structs
            .Where(s => !s.ForwardDeclaration && !s.IsInternal && !s.IsAnonymous && s.Fields.Count > 0)
            .Where(s => !TypeMapper.IsUnsupportedType(s))
            .Select(s => s.Name)
            .ToHashSet();

        // === Generate Structs (one file per struct) ===
        Console.WriteLine("\nGenerating structs...");
        var structCount = 0;
        var structMethodCount = 0;
        foreach (StructInfo? structInfo in root.Structs
            .Where(s => !s.ForwardDeclaration && !s.IsInternal && !s.IsAnonymous && s.Fields.Count > 0)
            .Where(s => !TypeMapper.IsUnsupportedType(s)))
        {
            // Get methods for this struct
            _ = structMethodsByClass.TryGetValue(structInfo.Name, out List<FunctionInfo>? methods);
            var name = NamingConventions.CleanBackendStructName(structInfo.Name);
            var content = structGenerator.GenerateSingleStruct(structInfo, ns, methods, name);
            var filePath = Path.Combine(outputDir, $"{name}.cs");
            File.WriteAllText(filePath, content);
            structCount++;
            structMethodCount += methods?.Count ?? 0;
        }

        Console.WriteLine($"  Generated {structCount} struct files with {structMethodCount} methods");

        // === Generate Native Methods (one file per class grouping) ===
        // Filter out struct methods that were already generated in their structs
        var nonStructFunctions = allFunctions
            .Where(f => string.IsNullOrEmpty(f.OriginalClass) || !structNames.Contains(f.OriginalClass) || !f.Arguments.Any(a => a.IsInstancePointer))
            .ToList();

        Console.WriteLine("\nGenerating native methods...");
        if (nonStructFunctions.Count != 0)
        {
            var content = functionGenerator.GenerateForClass(nonStructFunctions, ns, className);
            var filePath = Path.Combine(outputDir, $"{className}.cs");
            File.WriteAllText(filePath, content);

            Console.WriteLine($"  Generated {nonStructFunctions.Count} {className} functions");
        }

        return typeMapper;
    }
}
