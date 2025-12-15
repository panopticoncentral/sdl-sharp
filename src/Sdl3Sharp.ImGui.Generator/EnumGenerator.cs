namespace Sdl3Sharp.ImGui.Generator;

/// <summary>
/// Generates C# enum definitions from Dear Bindings enum data.
/// </summary>
public static class EnumGenerator
{
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
        // Try to map the declaration directly (e.g., "int", "ImU8")
        return (storageType == null)
            ? null
            : TypeMapper.BuiltinTypeMap.TryGetValue(storageType.Declaration, out var mappedType)
                ? mappedType
                : TypeMapper.KnownTypedefs.TryGetValue(storageType.Declaration, out mappedType)
                    ? mappedType
                    : throw new InvalidOperationException();
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
