namespace Sdl3Sharp.ImGui.Generator;

/// <summary>
/// Generates C# wrapper structs for ImGui typedefs.
/// </summary>
public static class TypedefGenerator
{
    /// <summary>
    /// Typedefs that should be generated as wrapper structs.
    /// Maps typedef name to underlying C# type.
    /// </summary>
    public static readonly Dictionary<string, string> WrapperTypedefs = new()
    {
        ["ImGuiID"] = "uint",
        ["ImTextureID"] = "nint",
        ["ImDrawIdx"] = "ushort",
        ["ImGuiKeyChord"] = "int",
        ["ImGuiSelectionUserData"] = "long",
        ["ImPoolIdx"] = "int",
        ["ImFileHandle"] = "nint",
        ["ImFontAtlasRectId"] = "int",
    };

    /// <summary>
    /// Callback typedefs that should be generated as function pointer wrapper structs.
    /// </summary>
    public static readonly HashSet<string> CallbackTypedefs =
    [
        "ImGuiInputTextCallback",
        "ImGuiSizeCallback",
        "ImGuiMemAllocFunc",
        "ImGuiMemFreeFunc",
        "ImDrawCallback",
    ];

    /// <summary>
    /// Generates a wrapper struct for an opaque handle type.
    /// </summary>
    public static string GenerateOpaqueHandleWrapper(string name, string namespaceName)
    {
        var writer = new CodeWriter();
        writer.WriteFileHeader();
        writer.AppendLine($"namespace {namespaceName};");
        writer.AppendLine();

        // Write documentation - empty struct, pointer wrapping happens at managed level
        writer.AppendLine($"/// <summary>Opaque handle to an internal ImGui {name} structure.</summary>");
        writer.AppendLine($"public readonly struct {name};");

        return writer.ToString();
    }

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

    /// <summary>
    /// Generates a function pointer wrapper struct for a callback typedef.
    /// </summary>
    public static string GenerateCallbackTypedef(TypeMapper typeMapper, TypedefInfo typedefInfo, string namespaceName)
    {
        var writer = new CodeWriter();
        writer.WriteFileHeader();
        writer.AppendLine($"namespace {namespaceName};");
        writer.AppendLine();

        FunctionPointerDetails? details = typedefInfo.Type?.TypeDetails;
        if (details == null || details.Flavour != "function_pointer")
        {
            throw new InvalidOperationException();
        }

        var name = typedefInfo.Name;
        var fpType = BuildFunctionPointerSignature(typeMapper, details);

        // Write documentation
        writer.WriteDocComment(typedefInfo.Comments);

        // Write param doc for primary constructor
        writer.AppendLine($"/// <param name=\"value\">The underlying function pointer.</param>");

        // Write struct with primary constructor - needs unsafe
        writer.AppendLine($"public readonly unsafe struct {name}({fpType} value)");
        writer.OpenBrace();

        // Value property
        writer.AppendLine($"/// <summary>The underlying function pointer.</summary>");
        writer.AppendLine($"public readonly {fpType} Value = value;");
        writer.AppendLine();

        // Implicit conversion to function pointer
        writer.AppendLine($"/// <summary>Implicitly converts a {name} to its underlying function pointer.</summary>");
        writer.AppendLine($"/// <param name=\"callback\">The {name} to convert.</param>");
        writer.AppendLine($"public static implicit operator {fpType}({name} callback) => callback.Value;");
        writer.AppendLine();

        // Implicit conversion from function pointer
        writer.AppendLine($"/// <summary>Implicitly converts a function pointer to {name}.</summary>");
        writer.AppendLine($"/// <param name=\"value\">The function pointer to convert.</param>");
        writer.AppendLine($"public static implicit operator {name}({fpType} value) => new(value);");

        writer.CloseBrace();

        return writer.ToString();
    }

    private static string BuildFunctionPointerSignature(TypeMapper typeMapper, FunctionPointerDetails details)
    {
        var returnType = typeMapper.MapType(details.ReturnType);
        var paramTypes = new List<string>();

        foreach (ArgumentInfo arg in details.Arguments)
        {
            var paramType = typeMapper.MapType(arg.Type);
            paramTypes.Add(paramType);
        }

        // Add return type at the end for delegate* syntax
        paramTypes.Add(returnType);

        var signature = string.Join(", ", paramTypes);
        return $"delegate* unmanaged[Cdecl]<{signature}>";
    }
}
