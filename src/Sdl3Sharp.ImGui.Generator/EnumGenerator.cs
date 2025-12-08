namespace Sdl3Sharp.ImGui.Generator;

/// <summary>
/// Generates C# enum definitions from Dear Bindings enum data.
/// </summary>
public static class EnumGenerator
{
    /// <summary>
    /// Maps C type declarations and user type names to C# types for enum underlying types.
    /// </summary>
    private static readonly Dictionary<string, string> TypeMappings = new(StringComparer.OrdinalIgnoreCase)
    {
        // Builtin types
        ["int"] = "int",
        ["signed int"] = "int",
        ["unsigned int"] = "uint",
        ["short"] = "short",
        ["signed short"] = "short",
        ["unsigned short"] = "ushort",
        ["char"] = "sbyte",
        ["signed char"] = "sbyte",
        ["unsigned char"] = "byte",
        ["long"] = "long",
        ["signed long"] = "long",
        ["unsigned long"] = "ulong",
        ["long long"] = "long",
        ["signed long long"] = "long",
        ["unsigned long long"] = "ulong",

        // ImGui user types
        ["ImS8"] = "sbyte",
        ["ImU8"] = "byte",
        ["ImS16"] = "short",
        ["ImU16"] = "ushort",
        ["ImS32"] = "int",
        ["ImU32"] = "uint",
        ["ImS64"] = "long",
        ["ImU64"] = "ulong",
    };

    public static string GenerateSingleEnum(EnumInfo enumInfo, string namespaceName)
    {
        var writer = new CodeWriter();
        writer.WriteFileHeader();

        writer.AppendLine($"namespace {namespaceName};");
        writer.AppendLine();

        var cleanName = NamingConventions.CleanEnumName(enumInfo.Name);

        // Write documentation
        writer.WriteDocComment(enumInfo.Comments);

        // Write flags attribute if applicable
        if (enumInfo.IsFlagsEnum)
        {
            writer.AppendLine("[Flags]");
        }

        // Determine the underlying type from storage_type or fall back to value-based detection
        var underlyingType = DetermineUnderlyingTypeFromStorageType(enumInfo.StorageType)
            ?? DetermineUnderlyingType(enumInfo.Elements);
        var typeDeclaration = underlyingType != "int" ? $" : {underlyingType}" : "";

        writer.AppendLine($"public enum {cleanName}{typeDeclaration}");
        writer.OpenBrace();

        var elements = enumInfo.Elements
            .Where(e => !e.IsInternal && !e.IsCount && !Conditional.IsObsolete(e.Conditionals))
            .ToList();

        for (var i = 0; i < elements.Count; i++)
        {
            EnumElement element = elements[i];
            var elementName = NamingConventions.CleanEnumElementName(enumInfo.Name, element.Name);

            // Write element comment inline
            var comment = "";
            if (element.Comments?.Attached != null)
            {
                var cleanComment = element.Comments.Attached.Trim();
                if (cleanComment.StartsWith("//"))
                {
                    cleanComment = cleanComment[2..].Trim();
                }

                comment = $" // {cleanComment}";
            }

            var comma = i < elements.Count - 1 ? "," : "";
            var valueStr = FormatValue(element.Value, enumInfo.IsFlagsEnum, element.ValueExpression);

            writer.AppendLine($"{elementName} = {valueStr}{comma}{comment}");
        }

        writer.CloseBrace();

        return writer.ToString();
    }

    /// <summary>
    /// Determines the underlying type from the enum's storage_type property.
    /// </summary>
    /// <param name="storageType">The storage type from the JSON.</param>
    /// <returns>The C# type name, or null if the storage type cannot be mapped.</returns>
    private static string? DetermineUnderlyingTypeFromStorageType(TypeDescription? storageType)
    {
        if (storageType == null)
        {
            return null;
        }

        // Try to map the declaration directly (e.g., "int", "ImU8")
        if (TypeMappings.TryGetValue(storageType.Declaration, out var mappedType))
        {
            return mappedType;
        }

        // Try to map from the user type name if present
        if (storageType.Description?.Kind == "User" &&
            storageType.Description.Name != null &&
            TypeMappings.TryGetValue(storageType.Description.Name, out mappedType))
        {
            return mappedType;
        }

        // Try to map from the builtin type if present
        if (storageType.Description?.Kind == "Builtin" &&
            storageType.Description.BuiltinType != null)
        {
            // Convert underscore format (e.g., "unsigned_int") to space format
            var builtinType = storageType.Description.BuiltinType.Replace('_', ' ');
            if (TypeMappings.TryGetValue(builtinType, out mappedType))
            {
                return mappedType;
            }
        }

        return null;
    }

    private static string DetermineUnderlyingType(List<EnumElement> elements)
    {
        if (elements.Count == 0)
        {
            return "int";
        }

        var maxValue = elements.Max(e => e.Value);
        var minValue = elements.Min(e => e.Value);

        // Check if values fit in int (most common case)
        if (minValue >= int.MinValue && maxValue <= int.MaxValue)
        {
            return "int";
        }

        // Need long for larger values
        if (minValue >= long.MinValue && maxValue <= long.MaxValue)
        {
            return "long";
        }

        return "int"; // Fallback
    }

    private static string FormatValue(long value, bool isFlags, string? expression)
    {
        // For flags, try to use bit shift notation for powers of 2
        if (isFlags && value > 0 && IsPowerOfTwo(value))
        {
            var shift = (int)Math.Log2(value);
            return $"1 << {shift}";
        }

        // Use the original expression if it's a simple bit shift
        if (expression != null && expression.StartsWith("1<<"))
        {
            return expression.Replace("<<", " << ");
        }

        // Default to decimal value
        return value.ToString();
    }

    private static bool IsPowerOfTwo(long value)
    {
        return value > 0 && (value & (value - 1)) == 0;
    }
}
