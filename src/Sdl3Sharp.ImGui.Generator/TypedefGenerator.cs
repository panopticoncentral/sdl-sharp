namespace Sdl3Sharp.ImGui.Generator;

/// <summary>
/// Generates C# wrapper structs for ImGui typedefs.
/// </summary>
public static class TypedefGenerator
{
    /// <summary>
    /// Generates a single typedef wrapper struct.
    /// </summary>
    public static string GenerateSingleTypedef(TypedefInfo typedefInfo, string underlyingType, string namespaceName)
    {
        var writer = new CodeWriter();
        writer.WriteFileHeader();
        writer.AppendLine($"namespace {namespaceName};");
        writer.AppendLine();

        var name = typedefInfo.Name;

        // Write documentation
        writer.WriteDocComment(typedefInfo.Comments);

        // Write param doc for primary constructor
        writer.AppendLine($"/// <param name=\"value\">The underlying {name} value.</param>");

        // Write struct with primary constructor
        writer.AppendLine($"public readonly struct {name}({underlyingType} value)");
        writer.OpenBrace();

        // Value property
        writer.AppendLine($"/// <summary>The underlying {name} value.</summary>");
        writer.AppendLine($"public readonly {underlyingType} Value = value;");
        writer.AppendLine();

        // Implicit conversion to underlying type
        writer.AppendLine($"/// <summary>Implicitly converts a {name} to {underlyingType}.</summary>");
        writer.AppendLine($"/// <param name=\"id\">The {name} to convert.</param>");
        writer.AppendLine($"public static implicit operator {underlyingType}({name} id) => id.Value;");
        writer.AppendLine();

        // Implicit conversion from underlying type
        writer.AppendLine($"/// <summary>Implicitly converts a {underlyingType} to {name}.</summary>");
        writer.AppendLine($"/// <param name=\"value\">The value to convert.</param>");
        writer.AppendLine($"public static implicit operator {name}({underlyingType} value) => new(value);");

        writer.CloseBrace();

        return writer.ToString();
    }
}
