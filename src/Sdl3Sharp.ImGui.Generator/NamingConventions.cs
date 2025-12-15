
namespace Sdl3Sharp.ImGui.Generator;

/// <summary>
/// Utilities for converting C names to C# conventions.
/// </summary>
public static class NamingConventions
{
    /// <summary>
    /// Backend prefixes to strip from enum names and elements.
    /// </summary>
    private static readonly Dictionary<string, string> BackendPrefixes = new()
    {
        { "ImGui_ImplSDL3_", "ImGuiSdl3" },
        { "ImGui_ImplSDLRenderer3_", "ImGuiSdl3Renderer" },
        { "ImGui_ImplSDLGPU3_", "ImGuiSdl3Gpu" },
    };

    /// <summary>
    /// Converts an ImGui enum name (ImGuiWindowFlags_) to C# style (ImGuiWindowFlags).
    /// Also handles backend enums (ImGui_ImplSDL3_GamepadMode -> ImGuiSdl3GamepadMode).
    /// </summary>
    public static string CleanEnumName(string name)
    {
        // Remove trailing underscore
        return name.EndsWith('_') ? name[..^1] : name;
    }

    /// <summary>
    /// Cleans a backend struct name by replacing backend prefixes with shorter identifiers.
    /// For example: ImGui_ImplSDLGPU3_InitInfo -> GpuInitInfo
    ///              ImGui_ImplSDLRenderer3_RenderState -> RendererRenderState
    /// </summary>
    public static string CleanBackendStructName(string name)
    {
        // Map backend prefixes to short identifiers
        foreach (KeyValuePair<string, string> prefix in BackendPrefixes)
        {
            if (name.StartsWith(prefix.Key))
            {
                name = prefix.Value + name[prefix.Key.Length..];
                break;
            }
        }

        return name;
    }

    /// <summary>
    /// Generates a cleaned-up function name by removing common library or backend-specific prefixes from the specified
    /// function's name.
    /// </summary>
    public static string CleanFunctionName(string name)
    {
        if (name.StartsWith("cImGui"))
        {
            // Remove 'c' prefix for C API functions (e.g., cImGui_CreateContext -> CreateContext)
            name = name[1..];
        }

        // Remove backend prefixes (e.g., ImplSDL3_InitForOpenGL -> InitForOpenGL)
        foreach (KeyValuePair<string, string> prefix in BackendPrefixes)
        {
            if (name.StartsWith(prefix.Key))
            {
                return name[prefix.Key.Length..];
            }
        }

        // For member functions like ImVec2_Add, keep the full name
        return name;
    }

    /// <summary>
    /// Converts an enum element name to C# style.
    /// For example: ImGuiWindowFlags_NoTitleBar -> NoTitleBar
    /// Also handles backend enums: ImGui_ImplSDL3_GamepadMode_AutoFirst -> AutoFirst
    /// </summary>
    public static string CleanEnumElementName(string enumName, string elementName)
    {
        var result = elementName;

        if (result.StartsWith(enumName))
        {
            result = result[enumName.Length..];
        }

        // Remove leading underscore if present (e.g., _AutoFirst -> AutoFirst)
        if (result.StartsWith('_'))
        {
            result = result[1..];
        }

        // C# identifiers cannot start with a digit; use word form for single digits
        if (result.Length == 1 && char.IsDigit(result[0]))
        {
            result = result[0] switch
            {
                '0' => "Zero",
                '1' => "One",
                '2' => "Two",
                '3' => "Three",
                '4' => "Four",
                '5' => "Five",
                '6' => "Six",
                '7' => "Seven",
                '8' => "Eight",
                '9' => "Nine",
                _ => "_" + result
            };
        }
        else if (result.Length > 0 && char.IsDigit(result[0]))
        {
            // For multi-character names starting with digit, prefix with underscore
            result = "_" + result;
        }

        return result;
    }

    /// <summary>
    /// Converts a field name to PascalCase.
    /// </summary>
    public static string ToPascalCase(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return name;
        }

        // Handle leading underscore (private field indicator)
        if (name.StartsWith('_'))
        {
            var rest = name[1..];
            if (rest.Length > 0)
            {
                return char.ToUpper(rest[0]) + rest[1..];
            }
        }

        // Simple PascalCase
        return char.ToUpper(name[0]) + name[1..];
    }

    /// <summary>
    /// Converts a parameter name to camelCase, avoiding C# keywords.
    /// </summary>
    public static string ToParameterName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return name;
        }

        // Convert to camelCase
        var result = char.ToLower(name[0]) + name[1..];

        // Check for C# keywords
        return IsKeyword(result) ? "@" + result : result;
    }

    private static readonly HashSet<string> CSharpKeywords =
    [
        "abstract", "as", "base", "bool", "break", "byte", "case", "catch",
        "char", "checked", "class", "const", "continue", "decimal", "default",
        "delegate", "do", "double", "else", "enum", "event", "explicit",
        "extern", "false", "finally", "fixed", "float", "for", "foreach",
        "goto", "if", "implicit", "in", "int", "interface", "internal",
        "is", "lock", "long", "namespace", "new", "null", "object",
        "operator", "out", "override", "params", "private", "protected",
        "public", "readonly", "ref", "return", "sbyte", "sealed", "short",
        "sizeof", "stackalloc", "static", "string", "struct", "switch",
        "this", "throw", "true", "try", "typeof", "uint", "ulong",
        "unchecked", "unsafe", "ushort", "using", "virtual", "void",
        "volatile", "while"
    ];

    public static bool IsKeyword(string name)
    {
        return CSharpKeywords.Contains(name);
    }
}
