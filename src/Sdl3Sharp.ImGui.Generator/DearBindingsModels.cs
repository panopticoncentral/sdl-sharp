using System.Text.Json.Serialization;

namespace Sdl3Sharp.ImGui.Generator;

/// <summary>
/// Root model for Dear Bindings JSON output.
/// </summary>
public sealed class DearBindingsRoot
{
    [JsonPropertyName("defines")]
    public List<DefineInfo> Defines { get; set; } = [];

    [JsonPropertyName("enums")]
    public List<EnumInfo> Enums { get; set; } = [];

    [JsonPropertyName("typedefs")]
    public List<TypedefInfo> Typedefs { get; set; } = [];

    [JsonPropertyName("structs")]
    public List<StructInfo> Structs { get; set; } = [];

    [JsonPropertyName("functions")]
    public List<FunctionInfo> Functions { get; set; } = [];
}

public sealed class SourceLocation
{
    [JsonPropertyName("filename")]
    public string? Filename { get; set; }

    [JsonPropertyName("line")]
    public int? Line { get; set; }
}

public sealed class Comments
{
    [JsonPropertyName("preceding")]
    public List<string>? Preceding { get; set; }

    [JsonPropertyName("attached")]
    public string? Attached { get; set; }
}

public sealed class Conditional
{
    [JsonPropertyName("condition")]
    public string? Condition { get; set; }

    [JsonPropertyName("expression")]
    public string? Expression { get; set; }

    /// <summary>
    /// Checks if a list of conditionals indicates an obsolete item.
    /// An item is obsolete if it has an ifndef IMGUI_DISABLE_OBSOLETE_FUNCTIONS conditional.
    /// </summary>
    public static bool IsObsolete(List<Conditional>? conditionals)
    {
        return conditionals != null && conditionals.Any(c =>
            c.Condition == "ifndef" &&
            c.Expression == "IMGUI_DISABLE_OBSOLETE_FUNCTIONS");
    }
}

// ============ Defines ============

public sealed class DefineInfo
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("content")]
    public string? Content { get; set; }

    [JsonPropertyName("is_internal")]
    public bool IsInternal { get; set; }

    [JsonPropertyName("comments")]
    public Comments? Comments { get; set; }

    [JsonPropertyName("conditionals")]
    public List<Conditional>? Conditionals { get; set; }

    [JsonPropertyName("source_location")]
    public SourceLocation? SourceLocation { get; set; }
}

// ============ Enums ============

public sealed class EnumInfo
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("original_fully_qualified_name")]
    public string? OriginalFullyQualifiedName { get; set; }

    [JsonPropertyName("storage_type")]
    public TypeDescription? StorageType { get; set; }

    [JsonPropertyName("is_flags_enum")]
    public bool IsFlagsEnum { get; set; }

    [JsonPropertyName("elements")]
    public List<EnumElement> Elements { get; set; } = [];

    [JsonPropertyName("comments")]
    public Comments? Comments { get; set; }

    [JsonPropertyName("is_internal")]
    public bool IsInternal { get; set; }

    [JsonPropertyName("source_location")]
    public SourceLocation? SourceLocation { get; set; }
}

public sealed class EnumElement
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("value_expression")]
    public string? ValueExpression { get; set; }

    [JsonPropertyName("value")]
    public long Value { get; set; }

    [JsonPropertyName("is_count")]
    public bool IsCount { get; set; }

    [JsonPropertyName("is_internal")]
    public bool IsInternal { get; set; }

    [JsonPropertyName("comments")]
    public Comments? Comments { get; set; }

    [JsonPropertyName("conditionals")]
    public List<Conditional>? Conditionals { get; set; }

    [JsonPropertyName("source_location")]
    public SourceLocation? SourceLocation { get; set; }
}

// ============ Typedefs ============

public sealed class TypedefInfo
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("type")]
    public TypeDescription? Type { get; set; }

    [JsonPropertyName("comments")]
    public Comments? Comments { get; set; }

    [JsonPropertyName("conditionals")]
    public List<Conditional>? Conditionals { get; set; }

    [JsonPropertyName("is_internal")]
    public bool IsInternal { get; set; }

    [JsonPropertyName("source_location")]
    public SourceLocation? SourceLocation { get; set; }
}

// ============ Type Description ============

public sealed class TypeDescription
{
    [JsonPropertyName("declaration")]
    public string Declaration { get; set; } = "";

    [JsonPropertyName("description")]
    public TypeDescriptionDetail? Description { get; set; }

    [JsonPropertyName("type_details")]
    public FunctionPointerDetails? TypeDetails { get; set; }
}

/// <summary>
/// Details for function pointer typedefs.
/// </summary>
public sealed class FunctionPointerDetails
{
    [JsonPropertyName("flavour")]
    public string Flavour { get; set; } = "";

    [JsonPropertyName("return_type")]
    public TypeDescription? ReturnType { get; set; }

    [JsonPropertyName("arguments")]
    public List<ArgumentInfo> Arguments { get; set; } = [];
}

public sealed class TypeDescriptionDetail
{
    [JsonPropertyName("kind")]
    public string Kind { get; set; } = "";

    [JsonPropertyName("builtin_type")]
    public string? BuiltinType { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("inner_type")]
    public TypeDescriptionDetail? InnerType { get; set; }

    [JsonPropertyName("storage_classes")]
    public List<string>? StorageClasses { get; set; }

    [JsonPropertyName("is_nullable")]
    public bool? IsNullable { get; set; }

    [JsonPropertyName("is_reference")]
    public bool? IsReference { get; set; }

    [JsonPropertyName("bounds")]
    public string? Bounds { get; set; }
}

// ============ Structs ============

public sealed class StructInfo
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("original_fully_qualified_name")]
    public string? OriginalFullyQualifiedName { get; set; }

    [JsonPropertyName("kind")]
    public string Kind { get; set; } = "struct";

    [JsonPropertyName("by_value")]
    public bool ByValue { get; set; }

    [JsonPropertyName("forward_declaration")]
    public bool ForwardDeclaration { get; set; }

    [JsonPropertyName("is_anonymous")]
    public bool IsAnonymous { get; set; }

    [JsonPropertyName("fields")]
    public List<FieldInfo> Fields { get; set; } = [];

    [JsonPropertyName("comments")]
    public Comments? Comments { get; set; }

    [JsonPropertyName("is_internal")]
    public bool IsInternal { get; set; }

    [JsonPropertyName("source_location")]
    public SourceLocation? SourceLocation { get; set; }
}

public sealed class FieldInfo
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("is_array")]
    public bool IsArray { get; set; }

    [JsonPropertyName("width")]
    public int? Width { get; set; }

    [JsonPropertyName("is_anonymous")]
    public bool IsAnonymous { get; set; }

    [JsonPropertyName("type")]
    public TypeDescription? Type { get; set; }

    [JsonPropertyName("comments")]
    public Comments? Comments { get; set; }

    [JsonPropertyName("conditionals")]
    public List<Conditional>? Conditionals { get; set; }

    [JsonPropertyName("is_internal")]
    public bool IsInternal { get; set; }

    [JsonPropertyName("source_location")]
    public SourceLocation? SourceLocation { get; set; }
}

// ============ Functions ============

public sealed class FunctionInfo
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("original_fully_qualified_name")]
    public string? OriginalFullyQualifiedName { get; set; }

    [JsonPropertyName("return_type")]
    public TypeDescription? ReturnType { get; set; }

    [JsonPropertyName("arguments")]
    public List<ArgumentInfo> Arguments { get; set; } = [];

    [JsonPropertyName("is_default_argument_helper")]
    public bool IsDefaultArgumentHelper { get; set; }

    [JsonPropertyName("is_manual_helper")]
    public bool IsManualHelper { get; set; }

    [JsonPropertyName("is_imstr_helper")]
    public bool IsImstrHelper { get; set; }

    [JsonPropertyName("has_imstr_helper")]
    public bool HasImstrHelper { get; set; }

    [JsonPropertyName("is_unformatted_helper")]
    public bool IsUnformattedHelper { get; set; }

    [JsonPropertyName("is_static")]
    public bool IsStatic { get; set; }

    [JsonPropertyName("original_class")]
    public string? OriginalClass { get; set; }

    [JsonPropertyName("comments")]
    public Comments? Comments { get; set; }

    [JsonPropertyName("conditionals")]
    public List<Conditional>? Conditionals { get; set; }

    [JsonPropertyName("is_internal")]
    public bool IsInternal { get; set; }

    [JsonPropertyName("source_location")]
    public SourceLocation? SourceLocation { get; set; }
}

public sealed class ArgumentInfo
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("type")]
    public TypeDescription? Type { get; set; }

    [JsonPropertyName("is_array")]
    public bool IsArray { get; set; }

    [JsonPropertyName("is_varargs")]
    public bool IsVarargs { get; set; }

    [JsonPropertyName("is_instance_pointer")]
    public bool IsInstancePointer { get; set; }

    [JsonPropertyName("default_value")]
    public string? DefaultValue { get; set; }
}
