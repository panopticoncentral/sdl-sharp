namespace SdlSharp.ImGui.Generator;

/// <summary>
/// Generates C# P/Invoke declarations from Dear Bindings function data.
/// </summary>
public sealed class FunctionGenerator
{
    private readonly TypeMapper _typeMapper;
    private readonly string _dllName;

    public FunctionGenerator(TypeMapper typeMapper, string dllName = "SdlSharp.ImGui.Native")
    {
        _typeMapper = typeMapper;
        _dllName = dllName;
    }

    /// <summary>
    /// Generates a native methods class for a pre-filtered list of functions.
    /// </summary>
    public string GenerateForClass(IEnumerable<FunctionInfo> functions, string namespaceName, string className)
    {
        var writer = new CodeWriter();
        writer.WriteFileHeader();

        writer.AppendLine("using System.Runtime.InteropServices;");
        writer.AppendLine();
        writer.AppendLine($"namespace {namespaceName};");
        writer.AppendLine();

        writer.AppendLine($"internal static unsafe partial class {className}");
        writer.OpenBrace();

        writer.AppendLine($"private const string DllName = \"{_dllName}\";");
        writer.AppendLine();

        var functionList = functions.OrderBy(f => f.Name).ToList();

        // Group by category based on comments or naming
        var groups = GroupFunctions(functionList);

        foreach (var group in groups)
        {
            if (!string.IsNullOrEmpty(group.Key))
            {
                writer.AppendLine($"#region {group.Key}");
                writer.AppendLine();
            }

            foreach (var func in group.Value)
            {
                GenerateFunction(writer, func);
                writer.AppendLine();
            }

            if (!string.IsNullOrEmpty(group.Key))
            {
                writer.AppendLine("#endregion");
                writer.AppendLine();
            }
        }

        writer.CloseBrace();

        return writer.ToString();
    }

    private static Dictionary<string, List<FunctionInfo>> GroupFunctions(List<FunctionInfo> functions)
    {
        var groups = new Dictionary<string, List<FunctionInfo>>();
        string currentCategory = "";

        foreach (var func in functions)
        {
            // Check for category comment
            if (func.Comments?.Preceding != null)
            {
                foreach (var comment in func.Comments.Preceding)
                {
                    var trimmed = comment.Trim().TrimStart('/').Trim();
                    // Category comments are typically short headers
                    if (trimmed.Length > 0 && trimmed.Length < 60 && !trimmed.Contains('.'))
                    {
                        currentCategory = trimmed;
                        break;
                    }
                }
            }

            if (!groups.ContainsKey(currentCategory))
                groups[currentCategory] = [];

            groups[currentCategory].Add(func);
        }

        return groups;
    }

    private void GenerateFunction(CodeWriter writer, FunctionInfo func)
    {
        // Write documentation
        writer.WriteDocComment(func.Comments);

        // Generate LibraryImport attribute
        writer.AppendLine($"[LibraryImport(DllName, EntryPoint = \"{func.Name}\")]");

        // Check if return type needs marshaling
        var returnMarshal = GetReturnMarshalAttribute(func.ReturnType);
        if (returnMarshal != null)
        {
            writer.AppendLine(returnMarshal);
        }

        // Build method signature
        var returnType = _typeMapper.MapType(func.ReturnType, forReturn: true);
        var methodName = GetMethodName(func);
        var parameters = GenerateParameters(func.Arguments);

        writer.AppendLine($"public static partial {returnType} {methodName}({parameters});");
    }

    private static string GetMethodName(FunctionInfo func)
    {
        var name = func.Name;

        // Remove common prefixes for cleaner names
        if (name.StartsWith("ImGui_"))
            return name[6..]; // Remove "ImGui_"

        if (name.StartsWith("cImGui_"))
            return name[7..]; // Remove "cImGui_"

        // For member functions like ImVec2_Add, keep the full name
        return name;
    }

    private string GenerateParameters(List<ArgumentInfo> arguments)
    {
        var parts = new List<string>();

        foreach (var arg in arguments)
        {
            if (arg.IsVarargs)
                continue; // Skip varargs

            var paramType = _typeMapper.MapType(arg.Type, forParameter: true);
            var paramName = NamingConventions.ToParameterName(arg.Name);

            // Check if we need ref modifier
            var refModifier = "";
            if (_typeMapper.ShouldUseRef(arg.Type, arg))
            {
                refModifier = "ref ";
                // Remove the pointer from the type since we're using ref
                if (paramType.EndsWith('*'))
                    paramType = paramType[..^1];
            }

            // Get marshaling attribute
            var marshalAttr = TypeMapper.GetMarshalAsAttribute(arg.Type, forParameter: true);

            if (marshalAttr != null)
            {
                parts.Add($"{marshalAttr} {refModifier}{paramType} {paramName}");
            }
            else
            {
                parts.Add($"{refModifier}{paramType} {paramName}");
            }
        }

        return string.Join(", ", parts);
    }

    private static string? GetReturnMarshalAttribute(TypeDescription? returnType)
    {
        if (returnType?.Description == null)
            return null;

        var desc = returnType.Description;

        // bool needs marshaling
        if (desc.Kind == "Builtin" && desc.BuiltinType == "bool")
        {
            return "[return: MarshalAs(UnmanagedType.U1)]";
        }

        return null;
    }

    private static bool IsVarargs(FunctionInfo func)
    {
        return func.Arguments.Any(a => a.IsVarargs);
    }
}
