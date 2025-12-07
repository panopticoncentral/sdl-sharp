namespace Sdl3Sharp.ImGui.Generator;

/// <summary>
/// Generates C# P/Invoke declarations from Dear Bindings function data.
/// </summary>
public sealed class FunctionGenerator(TypeMapper typeMapper)
{
    private readonly TypeMapper _typeMapper = typeMapper;

    /// <summary>
    /// Generates a native methods class for a pre-filtered list of functions.
    /// </summary>
    public string GenerateForClass(IEnumerable<FunctionInfo> functions, string namespaceName, string className)
    {
        var functionList = functions.ToList();

        var writer = new CodeWriter();
        writer.WriteFileHeader();

        writer.AppendLine("using System.Runtime.InteropServices;");

        // Add static using directives for SDL modules (types are nested in module classes)
        HashSet<string> sdlModules = TypeMapper.GetSdlModulesUsedByFunctions(functionList);
        foreach (var module in sdlModules.OrderBy(m => m))
        {
            writer.AppendLine($"using static Sdl3Sharp.Native.{module};");
        }

        writer.AppendLine();
        writer.AppendLine($"namespace {namespaceName};");
        writer.AppendLine();

        writer.AppendLine($"internal static unsafe partial class {className}");
        writer.OpenBrace();

        // Group functions by category (from preceding comments) while preserving original order
        List<FunctionGroup> groupedFunctions = GroupFunctions(functionList);

        foreach (FunctionGroup group in groupedFunctions)
        {
            writer.AppendLine($"#region {group.Category}");
            writer.AppendLine();

            foreach (FunctionInfo func in group.Functions)
            {
                GenerateFunction(writer, func);
                writer.AppendLine();
            }

            writer.AppendLine("#endregion");
            writer.AppendLine();
        }

        writer.CloseBrace();

        return writer.ToString();
    }

    private void GenerateFunction(CodeWriter writer, FunctionInfo func)
    {
        // Write documentation
        writer.WriteDocComment(func.Comments);

        // Build method signature
        var methodName = NamingConventions.CleanFunctionName(func.Name);

        // Generate LibraryImport attribute (only include EntryPoint if it differs from method name)
        if (methodName == func.Name)
        {
            writer.AppendLine("[LibraryImport(Common.ImGuiNative)]");
        }
        else
        {
            writer.AppendLine($"[LibraryImport(Common.ImGuiNative, EntryPoint = \"{func.Name}\")]");
        }

        // Check if return type needs marshaling
        var returnMarshal = GetReturnMarshalAttribute(func.ReturnType);
        if (returnMarshal != null)
        {
            writer.AppendLine(returnMarshal);
        }

        var returnType = _typeMapper.MapType(func.ReturnType, forReturn: true);
        var parameters = GenerateParameters(func.Arguments);

        writer.AppendLine($"public static partial {returnType} {methodName}({parameters});");
    }

    private string GenerateParameters(List<ArgumentInfo> arguments)
    {
        var parts = new List<string>();

        foreach (ArgumentInfo arg in arguments)
        {
            if (arg.IsVarargs)
            {
                continue; // Skip varargs
            }

            var paramType = _typeMapper.MapType(arg.Type, forParameter: true);
            var paramName = NamingConventions.ToParameterName(arg.Name);

            // Get marshaling attribute
            var marshalAttr = TypeMapper.GetMarshalAsAttribute(arg.Type);

            if (marshalAttr != null)
            {
                parts.Add($"{marshalAttr} {paramType} {paramName}");
            }
            else
            {
                parts.Add($"{paramType} {paramName}");
            }
        }

        return string.Join(", ", parts);
    }

    private static string? GetReturnMarshalAttribute(TypeDescription? returnType)
    {
        if (returnType?.Description == null)
        {
            return null;
        }

        TypeDescriptionDetail desc = returnType.Description;

        // bool needs marshaling
        return desc.Kind == "Builtin" && desc.BuiltinType == "bool" ? "[return: MarshalAs(UnmanagedType.U1)]" : null;
    }

    /// <summary>
    /// Groups functions by category based on preceding comments, preserving original order.
    /// </summary>
    private static List<FunctionGroup> GroupFunctions(List<FunctionInfo> functions)
    {
        var groups = new List<FunctionGroup>();
        var currentCategory = "General";
        var currentFunctions = new List<FunctionInfo>();

        foreach (FunctionInfo func in functions)
        {
            // Check if the function has a preceding comment that indicates a new category
            var category = ExtractCategory(func.Comments?.Preceding);

            if (category != null && category != currentCategory)
            {
                // Save the current group if it has functions
                if (currentFunctions.Count > 0)
                {
                    groups.Add(new FunctionGroup(currentCategory, currentFunctions));
                    currentFunctions = [];
                }

                currentCategory = category;
            }

            currentFunctions.Add(func);
        }

        // Add the last group
        if (currentFunctions.Count > 0)
        {
            groups.Add(new FunctionGroup(currentCategory, currentFunctions));
        }

        return groups;
    }

    /// <summary>
    /// Extracts a category name from preceding comments.
    /// </summary>
    private static string? ExtractCategory(List<string>? precedingComments)
    {
        if (precedingComments == null || precedingComments.Count == 0)
        {
            return null;
        }

        // Look for patterns like "Context creation and access" or section headers
        // The preceding comments typically contain the category/section name
        foreach (var comment in precedingComments)
        {
            var trimmed = comment.Trim().TrimStart('-', ' ', '/');

            // Skip empty lines or lines that look like documentation rather than headers
            if (string.IsNullOrWhiteSpace(trimmed) ||
                trimmed.StartsWith("See", StringComparison.OrdinalIgnoreCase) ||
                trimmed.StartsWith("Note", StringComparison.OrdinalIgnoreCase) ||
                trimmed.StartsWith("Use", StringComparison.OrdinalIgnoreCase) ||
                trimmed.StartsWith("This", StringComparison.OrdinalIgnoreCase) ||
                trimmed.StartsWith("The", StringComparison.OrdinalIgnoreCase) ||
                trimmed.StartsWith("If", StringComparison.OrdinalIgnoreCase) ||
                trimmed.StartsWith("When", StringComparison.OrdinalIgnoreCase) ||
                trimmed.StartsWith("For", StringComparison.OrdinalIgnoreCase) ||
                trimmed.Length > 80) // Long lines are likely documentation, not headers
            {
                continue;
            }

            // Return the first line that looks like a category header
            return trimmed;
        }

        return null;
    }

    private sealed record FunctionGroup(string Category, List<FunctionInfo> Functions);
}
