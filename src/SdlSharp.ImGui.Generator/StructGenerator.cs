namespace SdlSharp.ImGui.Generator;

/// <summary>
/// Generates C# struct definitions from Dear Bindings struct data.
/// </summary>
public sealed class StructGenerator(TypeMapper typeMapper)
{
    private readonly TypeMapper _typeMapper = typeMapper;

    public string GenerateSingleStruct(StructInfo structInfo, string namespaceName, IEnumerable<FunctionInfo>? methods = null, string? cleanName = null)
    {
        List<FunctionInfo> methodList = methods?.ToList() ?? [];
        var structName = cleanName ?? structInfo.Name;

        var writer = new CodeWriter();
        writer.WriteFileHeader();

        writer.AppendLine("using System.Runtime.InteropServices;");

        // Add static using directives for SDL modules (types are nested in module classes)
        HashSet<string> sdlModules = TypeMapper.GetSdlModulesUsedByStruct(structInfo);
        foreach (FunctionInfo method in methodList)
        {
            foreach (var module in TypeMapper.GetSdlModulesUsedByFunctions([method]))
            {
                _ = sdlModules.Add(module);
            }
        }

        foreach (var module in sdlModules.OrderBy(m => m))
        {
            writer.AppendLine($"using static Sdl3Sharp.Native.{module};");
        }

        writer.AppendLine();
        writer.AppendLine($"namespace {namespaceName};");
        writer.AppendLine();

        GenerateStruct(writer, structInfo, methodList, structName);

        return writer.ToString();
    }

    private void GenerateStruct(CodeWriter writer, StructInfo structInfo, List<FunctionInfo> methods, string? structName = null)
    {
        structName ??= structInfo.Name;

        // Write documentation
        writer.WriteDocComment(structInfo.Comments);

        // Write struct layout attribute
        writer.AppendLine("[StructLayout(LayoutKind.Sequential)]");

        // Determine if we need unsafe context (fields with pointers, fixed buffers, or methods)
        var needsUnsafe = NeedsUnsafeContext(structInfo) || methods.Count > 0;
        var unsafeModifier = needsUnsafe ? "unsafe " : "";

        writer.AppendLine($"public {unsafeModifier}partial struct {structName}");
        writer.OpenBrace();

        var fields = structInfo.Fields
            .Where(f => !f.IsInternal && !f.IsAnonymous)
            .ToList();

        for (var i = 0; i < fields.Count; i++)
        {
            FieldInfo field = fields[i];
            GenerateField(writer, field, structInfo);

            if (i < fields.Count - 1 || methods.Count > 0)
            {
                writer.AppendLine();
            }
        }

        // Generate methods
        foreach (FunctionInfo method in methods)
        {
            GenerateMethod(writer, method, structName);
            writer.AppendLine();
        }

        writer.CloseBrace();
    }

    private void GenerateMethod(CodeWriter writer, FunctionInfo func, string structName)
    {
        // Write documentation
        writer.WriteDocComment(func.Comments);

        // Get the short method name (e.g., "GetTexID" from "ImDrawCmd_GetTexID")
        var methodName = func.OriginalFullyQualifiedName ?? func.Name;
        if (methodName.StartsWith(structName + "_"))
        {
            methodName = methodName[(structName.Length + 1)..];
        }

        // Generate LibraryImport attribute
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
        var parameters = GenerateMethodParameters(func.Arguments, structName);

        writer.AppendLine($"public static partial {returnType} {methodName}({parameters});");
    }

    private string GenerateMethodParameters(List<ArgumentInfo> arguments, string structName)
    {
        var parts = new List<string>();

        foreach (ArgumentInfo arg in arguments)
        {
            if (arg.IsVarargs)
            {
                continue;
            }

            string paramType;
            if (arg.IsInstancePointer)
            {
                // Use typed pointer for self parameter
                paramType = $"{structName}*";
            }
            else
            {
                paramType = _typeMapper.MapType(arg.Type, forParameter: true);
            }

            var paramName = NamingConventions.ToParameterName(arg.Name);

            // Get marshaling attribute
            var marshalAttr = TypeMapper.GetMarshalAsAttribute(arg.Type, forParameter: true);

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
        return desc.Kind == "Builtin" && desc.BuiltinType == "bool" ? "[return: MarshalAs(UnmanagedType.U1)]" : null;
    }

    private void GenerateField(CodeWriter writer, FieldInfo field, StructInfo structInfo)
    {
        // Write documentation
        writer.WriteDocComment(field.Comments);

        var fieldName = NamingConventions.ToPascalCase(field.Name);
        var fieldType = MapFieldType(field);

        // Handle fixed-size arrays
        if (field.IsArray && field.Type?.Description?.Kind == "Array")
        {
            var bounds = field.Type.Description.Bounds;
            var elementType = GetArrayElementType(field.Type.Description);

            if (bounds != null && CanBeFixedBuffer(elementType))
            {
                // Fixed buffer for primitive types
                writer.AppendLine($"public fixed {elementType} {fieldName}[{bounds}];");
            }
            else if (bounds != null)
            {
                // For non-primitive types, we need a different approach
                // Generate individual fields or use InlineArray (.NET 8+)
                writer.AppendLine($"// TODO: Fixed array of {elementType}[{bounds}]");
                writer.AppendLine($"private {elementType} _{fieldName}_0;");
            }
            else
            {
                // Unknown bounds - use pointer
                writer.AppendLine($"public nint {fieldName};");
            }
        }
        else
        {
            writer.AppendLine($"public {fieldType} {fieldName};");
        }
    }

    private string MapFieldType(FieldInfo field)
    {
        return field.Type == null ? "nint" : _typeMapper.MapType(field.Type);
    }

    private string GetArrayElementType(TypeDescriptionDetail desc)
    {
        if (desc.InnerType == null)
        {
            return "nint";
        }

        // Use the type mapper to properly resolve user types
        var innerType = new TypeDescription
        {
            Declaration = desc.InnerType.Name ?? "",
            Description = desc.InnerType
        };
        return _typeMapper.MapType(innerType);
    }

    private static bool CanBeFixedBuffer(string elementType)
    {
        // Only primitive types can be used with fixed buffers
        return elementType is "bool" or "byte" or "sbyte" or "short" or "ushort"
            or "int" or "uint" or "long" or "ulong" or "float" or "double" or "char";
    }

    private bool NeedsUnsafeContext(StructInfo structInfo)
    {
        foreach (FieldInfo field in structInfo.Fields)
        {
            // Fixed-size arrays require unsafe
            if (field.IsArray && field.Type?.Description?.Kind == "Array")
            {
                var bounds = field.Type.Description.Bounds;
                var elementType = GetArrayElementType(field.Type.Description);
                if (bounds != null && CanBeFixedBuffer(elementType))
                {
                    return true;
                }
            }

            // Pointer types require unsafe
            var fieldType = MapFieldType(field);
            if (fieldType.Contains('*'))
            {
                return true;
            }
        }

        return false;
    }
}
