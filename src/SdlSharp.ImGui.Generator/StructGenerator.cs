
namespace SdlSharp.ImGui.Generator;

/// <summary>
/// Generates C# struct definitions from Dear Bindings struct data.
/// </summary>
public sealed class StructGenerator
{
    private readonly TypeMapper _typeMapper;

    public StructGenerator(TypeMapper typeMapper)
    {
        _typeMapper = typeMapper;
    }

    public string GenerateSingleStruct(StructInfo structInfo, string namespaceName)
    {
        var writer = new CodeWriter();
        writer.WriteFileHeader();

        writer.AppendLine("using System.Runtime.InteropServices;");

        // Add static using directives for SDL modules (types are nested in module classes)
        HashSet<string> sdlModules = TypeMapper.GetSdlModulesUsedByStruct(structInfo);
        foreach (var module in sdlModules.OrderBy(m => m))
        {
            writer.AppendLine($"using static Sdl3Sharp.Native.{module};");
        }

        writer.AppendLine();
        writer.AppendLine($"namespace {namespaceName};");
        writer.AppendLine();

        GenerateStruct(writer, structInfo);

        return writer.ToString();
    }

    private void GenerateStruct(CodeWriter writer, StructInfo structInfo)
    {
        // Write documentation
        writer.WriteDocComment(structInfo.Comments);

        // Write struct layout attribute
        writer.AppendLine("[StructLayout(LayoutKind.Sequential)]");

        // Determine if we need unsafe context
        var needsUnsafe = NeedsUnsafeContext(structInfo);
        var unsafeModifier = needsUnsafe ? "unsafe " : "";

        writer.AppendLine($"public {unsafeModifier}partial struct {structInfo.Name}");
        writer.OpenBrace();

        var fields = structInfo.Fields
            .Where(f => !f.IsInternal && !f.IsAnonymous)
            .ToList();

        for (var i = 0; i < fields.Count; i++)
        {
            FieldInfo field = fields[i];
            GenerateField(writer, field, structInfo);

            if (i < fields.Count - 1)
            {
                writer.AppendLine();
            }
        }

        writer.CloseBrace();
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
