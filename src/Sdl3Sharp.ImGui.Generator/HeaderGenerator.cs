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

        Dictionary<string, bool> referencedTypes = ReferenceCollector.CollectReferencedTypes(root);

        var enums = root.Enums
            .Where(e => !e.IsInternal
                && !TypeMapper.UnsupportedTypes.Contains(e.Name))
            .Select(e => NamingConventions.CleanEnumName(e.Name))
            .ToHashSet();

        var structs = root.Structs
            .Where(s => !s.IsInternal
                && !s.IsAnonymous
                && !TypeMapper.UnsupportedTypes.Contains(s.Name)
                && !TypeMapper.SdlTypeToModule.ContainsKey(s.Name)
                && (mainTypes == null || !mainTypes.Structs.Contains(s.Name))
                && referencedTypes.ContainsKey(s.Name))
            .ToHashSet();

        var functions = root.Functions
            .Where(f => !f.IsInternal
                && !f.IsImstrHelper
                && !f.Name.Contains("__")
                && !TypeMapper.FunctionHasUnsupportedTypes(f)
                && !Conditional.IsObsolete(f.Conditionals)
                && !excludedFunctions.Contains(f.Name))
            .ToHashSet();

        // Group struct methods by their original class
        var structFunctions = functions
            .Where(f => !string.IsNullOrEmpty(f.OriginalClass) && f.Arguments.Any(a => a.IsInstancePointer))
            .GroupBy(f => f.OriginalClass!)
            .ToDictionary(g => g.Key, g => g.ToList());

        // Filter out struct methods that were already generated in their structs
        var nonStructFunctions = functions
            .Where(f => string.IsNullOrEmpty(f.OriginalClass) || !f.Arguments.Any(a => a.IsInstancePointer))
            .ToList();

        // === Generate Enums (one file per enum) ===
        Console.WriteLine("\nGenerating enums...");
        var enumCount = 0;
        foreach (EnumInfo? enumInfo in root.Enums
            .Where(e => !e.IsInternal 
                && !TypeMapper.UnsupportedTypes.Contains(NamingConventions.CleanEnumName(e.Name))
                && referencedTypes.ContainsKey(NamingConventions.CleanEnumName(e.Name))))
        {
            var cleanName = NamingConventions.CleanEnumName(enumInfo.Name);
            var content = EnumGenerator.GenerateSingleEnum(enumInfo, ns);
            var filePath = Path.Combine(outputDir, $"{cleanName}.cs");
            File.WriteAllText(filePath, content);
            enumCount++;
        }

        Console.WriteLine($"  Generated {enumCount} enum files");

        // === Generate Typedefs (one file per typedef) ===
        Console.WriteLine("\nGenerating typedefs...");
        var typeDefCount = 0;
        foreach (TypedefInfo typedefInfo in root.Typedefs
            .Where(t => !t.IsInternal
                && !enums.Contains(t.Name)
                && !TypeMapper.KnownTypedefs.ContainsKey(t.Name)
                && !TypeMapper.UnsupportedTypes.Contains(t.Name)
                && !TypeMapper.SdlTypeToModule.ContainsKey(t.Name)
                && t.Type?.TypeDetails?.Flavour != "function_pointer"
                && (mainTypes == null || !mainTypes.TypeDefs.Contains(t.Name))
                && referencedTypes.ContainsKey(t.Name)))
        {
            var content = TypedefGenerator.GenerateSingleTypedef(typedefInfo, typeMapper.MapType(typedefInfo.Type), ns);
            var filePath = Path.Combine(outputDir, $"{typedefInfo.Name}.cs");
            File.WriteAllText(filePath, content);

            typeDefCount++;
        }

        Console.WriteLine($"  Generated {typeDefCount} typedef files");

        // === Generate Structs (one file per struct) ===
        Console.WriteLine("\nGenerating structs...");
        var valueStructCount = 0;
        foreach (StructInfo? structInfo in structs)
        {
            _ = structFunctions.TryGetValue(structInfo.Name, out List<FunctionInfo>? methods);
            var content = StructGenerator.GenerateValueStruct(typeMapper, structInfo, ns, methods, referencedTypes[structInfo.Name]);
            var filePath = Path.Combine(outputDir, $"{structInfo.Name}.cs");
            File.WriteAllText(filePath, content);
            valueStructCount++;
        }

        Console.WriteLine($"  Generated {valueStructCount} value struct files");

        // === Generate Native Methods (one file per class grouping) ===
        Console.WriteLine("\nGenerating native methods...");
        if (nonStructFunctions.Count != 0)
        {
            var content = FunctionGenerator.GenerateForClass(typeMapper, nonStructFunctions, ns, className);
            var filePath = Path.Combine(outputDir, $"{className}.cs");
            File.WriteAllText(filePath, content);

            Console.WriteLine($"  Generated {nonStructFunctions.Count} {className} functions");
        }

        return typeMapper;
    }
}
