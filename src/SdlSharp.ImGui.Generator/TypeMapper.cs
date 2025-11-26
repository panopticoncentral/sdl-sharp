namespace SdlSharp.ImGui.Generator;

/// <summary>
/// Maps C types from Dear Bindings to C# types.
/// </summary>
public sealed class TypeMapper
{
    private readonly HashSet<string> _byValueStructs = [];
    private readonly HashSet<string> _opaqueStructs = [];
    private readonly HashSet<string> _enumTypes = [];
    private readonly Dictionary<string, string> _typedefs = new();

    /// <summary>
    /// Builtin C type to C# type mapping.
    /// </summary>
    private static readonly Dictionary<string, string> BuiltinTypeMap = new()
    {
        ["void"] = "void",
        ["bool"] = "bool",
        ["char"] = "byte",
        ["signed_char"] = "sbyte",
        ["unsigned_char"] = "byte",
        ["short"] = "short",
        ["unsigned_short"] = "ushort",
        ["int"] = "int",
        ["unsigned_int"] = "uint",
        ["long"] = "int",           // C long is 32-bit on Windows
        ["unsigned_long"] = "uint",
        ["long_long"] = "long",
        ["unsigned_long_long"] = "ulong",
        ["float"] = "float",
        ["double"] = "double",
        ["size_t"] = "nuint",
        ["ptrdiff_t"] = "nint",
    };

    /// <summary>
    /// Known ImGui typedefs that map to specific C# types.
    /// </summary>
    private static readonly Dictionary<string, string> KnownTypedefs = new()
    {
        ["ImU8"] = "byte",
        ["ImU16"] = "ushort",
        ["ImU32"] = "uint",
        ["ImU64"] = "ulong",
        ["ImS8"] = "sbyte",
        ["ImS16"] = "short",
        ["ImS32"] = "int",
        ["ImS64"] = "long",
        ["ImWchar16"] = "ushort",
        ["ImWchar32"] = "uint",
        ["ImWchar"] = "ushort",     // Default is 16-bit
        ["ImDrawIdx"] = "ushort",
        ["ImGuiID"] = "uint",
        ["ImTextureID"] = "nint",
        ["ImGuiKeyChord"] = "int",
        ["ImPoolIdx"] = "int",
        ["ImFileHandle"] = "nint",
        ["ImGuiMemAllocFunc"] = "nint",
        ["ImGuiMemFreeFunc"] = "nint",
        // Additional typedefs
        ["ImFontAtlasRectId"] = "int",
        ["ImDrawCallback"] = "nint",
        ["ImGuiInputTextCallback"] = "nint",
        ["ImGuiSizeCallback"] = "nint",
        ["ImGuiContextHookCallback"] = "nint",
        ["ImGuiErrorCallback"] = "nint",
        ["size_t"] = "nuint",
        ["va_list"] = "nint", // Will be filtered out at function level
        ["ImStr"] = "nint",   // Will be filtered out at function level
        // Additional missing typedefs
        ["ImGuiSelectionUserData"] = "long",  // typedef ImS64
        ["ImGuiKeyData"] = "nint",  // Opaque struct
    };

    /// <summary>
    /// Types that should cause functions to be skipped (not marshallable).
    /// </summary>
    public static readonly HashSet<string> UnsupportedTypes =
    [
        "va_list",
        "ImStr",
    ];

    /// <summary>
    /// Types that should be treated as opaque handles (pointers).
    /// </summary>
    private static readonly HashSet<string> OpaqueHandleTypes =
    [
        "ImGuiContext",
        "ImFontAtlas",
        "ImFont",
        "ImDrawList",
        "ImDrawListSharedData",
        "ImFontAtlasBuilder",
        "ImFontLoader",
        "ImGuiStorage",
        "ImGuiTextBuffer",
        "ImGuiListClipper",
        "ImGuiInputTextCallbackData",
        "ImGuiPayload",
        "ImGuiViewport",
        "ImGuiPlatformIO",
        "ImGuiPlatformImeData",
        "ImGuiMultiSelectIO",
        "ImGuiSelectionRequest",
        "ImGuiSelectionBasicStorage",
        "ImGuiSelectionExternalStorage",
    ];

    /// <summary>
    /// SDL types mapped to their containing module class in Sdl3Sharp.Native.
    /// The types are nested within module classes (e.g., SDL_Window is Video.SDL_Window).
    /// </summary>
    private static readonly Dictionary<string, string> SdlTypeToModule = new()
    {
        // Video module types
        ["SDL_Window"] = "Video",
        // Render module types
        ["SDL_Renderer"] = "Render",
        ["SDL_Texture"] = "Render",
        // Events module types
        ["SDL_Event"] = "Events",
        // Gamepad module types
        ["SDL_Gamepad"] = "Gamepad",
        // GPU module types
        ["SDL_GPUDevice"] = "Gpu",
        ["SDL_GPUTexture"] = "Gpu",
        ["SDL_GPUSampler"] = "Gpu",
        ["SDL_GPUCommandBuffer"] = "Gpu",
        ["SDL_GPURenderPass"] = "Gpu",
        ["SDL_GPUGraphicsPipeline"] = "Gpu",
        // GPU enum types
        ["SDL_GPUTextureFormat"] = "Gpu",
        ["SDL_GPUSampleCount"] = "Gpu",
        ["SDL_GPUSwapchainComposition"] = "Gpu",
        ["SDL_GPUPresentMode"] = "Gpu",
    };

    /// <summary>
    /// Checks if a type name is an SDL type from Sdl3Sharp.Native.
    /// </summary>
    public static bool IsSdlType(string typeName) => SdlTypeToModule.ContainsKey(typeName);

    /// <summary>
    /// Gets the module class name for an SDL type.
    /// </summary>
    public static string? GetSdlTypeModule(string typeName) =>
        SdlTypeToModule.TryGetValue(typeName, out var module) ? module : null;

    public void Initialize(DearBindingsRoot root)
    {
        // Collect by-value structs
        foreach (StructInfo? s in root.Structs.Where(s => !s.ForwardDeclaration && s.ByValue))
        {
            _byValueStructs.Add(s.Name);
        }

        // Collect opaque/reference structs
        foreach (StructInfo? s in root.Structs.Where(s => !s.ForwardDeclaration && !s.ByValue))
        {
            _opaqueStructs.Add(s.Name);
        }

        // Add known opaque types
        foreach (var t in OpaqueHandleTypes)
        {
            _opaqueStructs.Add(t);
        }

        // Collect enums
        foreach (EnumInfo e in root.Enums)
        {
            _enumTypes.Add(e.Name);
            // Also add without trailing underscore (ImGuiWindowFlags_ -> ImGuiWindowFlags)
            if (e.Name.EndsWith('_'))
            {
                _enumTypes.Add(e.Name[..^1]);
            }
        }

        // Collect typedefs
        foreach (TypedefInfo t in root.Typedefs)
        {
            if (t.Type?.Description != null)
            {
                _typedefs[t.Name] = t.Type.Declaration;
            }
        }
    }

    public bool IsByValueStruct(string typeName) => _byValueStructs.Contains(typeName);
    public bool IsOpaqueStruct(string typeName) => _opaqueStructs.Contains(typeName) || OpaqueHandleTypes.Contains(typeName);
    public bool IsEnumType(string typeName) => _enumTypes.Contains(typeName) || _enumTypes.Contains(typeName + "_");

    /// <summary>
    /// Checks if a type is an SDL native type from Sdl3Sharp.Native.
    /// </summary>
    public static bool IsSdlNativeType(string typeName) => SdlTypeToModule.ContainsKey(typeName);

    /// <summary>
    /// Checks if a type description uses any SDL native types.
    /// </summary>
    public static bool UsesSdlNativeTypes(TypeDescription? type)
    {
        if (type?.Description == null)
            return false;

        return UsesSdlNativeTypesDetail(type.Description);
    }

    private static bool UsesSdlNativeTypesDetail(TypeDescriptionDetail desc)
    {
        // Check direct user type
        if (desc.Kind == "User" && desc.Name != null)
        {
            if (SdlTypeToModule.ContainsKey(desc.Name))
                return true;
        }

        // Check inner type for pointers/arrays
        if (desc.InnerType != null)
        {
            return UsesSdlNativeTypesDetail(desc.InnerType);
        }

        return false;
    }

    /// <summary>
    /// Checks if a function uses any SDL native types in its signature.
    /// </summary>
    public static bool FunctionUsesSdlNativeTypes(FunctionInfo func)
    {
        // Check return type
        if (UsesSdlNativeTypes(func.ReturnType))
            return true;

        // Check arguments
        foreach (ArgumentInfo arg in func.Arguments)
        {
            if (UsesSdlNativeTypes(arg.Type))
                return true;
        }

        return false;
    }

    /// <summary>
    /// Checks if a struct uses any SDL native types in its fields.
    /// </summary>
    public static bool StructUsesSdlNativeTypes(StructInfo structInfo)
    {
        foreach (FieldInfo field in structInfo.Fields)
        {
            if (UsesSdlNativeTypes(field.Type))
                return true;
        }

        return false;
    }

    /// <summary>
    /// Gets all SDL module names used by the functions.
    /// </summary>
    public static HashSet<string> GetSdlModulesUsedByFunctions(IEnumerable<FunctionInfo> functions)
    {
        var modules = new HashSet<string>();

        foreach (FunctionInfo func in functions)
        {
            CollectSdlModulesFromType(func.ReturnType, modules);
            foreach (ArgumentInfo arg in func.Arguments)
            {
                CollectSdlModulesFromType(arg.Type, modules);
            }
        }

        return modules;
    }

    /// <summary>
    /// Gets all SDL module names used by a struct.
    /// </summary>
    public static HashSet<string> GetSdlModulesUsedByStruct(StructInfo structInfo)
    {
        var modules = new HashSet<string>();

        foreach (FieldInfo field in structInfo.Fields)
        {
            CollectSdlModulesFromType(field.Type, modules);
        }

        return modules;
    }

    private static void CollectSdlModulesFromType(TypeDescription? type, HashSet<string> modules)
    {
        if (type?.Description == null)
            return;

        CollectSdlModulesFromTypeDetail(type.Description, modules);
    }

    private static void CollectSdlModulesFromTypeDetail(TypeDescriptionDetail desc, HashSet<string> modules)
    {
        if (desc.Kind == "User" && desc.Name != null)
        {
            if (SdlTypeToModule.TryGetValue(desc.Name, out var module))
            {
                modules.Add(module);
            }
        }

        if (desc.InnerType != null)
        {
            CollectSdlModulesFromTypeDetail(desc.InnerType, modules);
        }
    }

    /// <summary>
    /// Checks if a type contains unsupported types (va_list, ImStr, etc.)
    /// </summary>
    public static bool HasUnsupportedType(TypeDescription? type)
    {
        if (type?.Description == null)
            return false;

        return HasUnsupportedTypeDetail(type.Description);
    }

    private static bool HasUnsupportedTypeDetail(TypeDescriptionDetail desc)
    {
        // Check direct user type
        if (desc.Kind == "User" && desc.Name != null)
        {
            if (UnsupportedTypes.Contains(desc.Name))
                return true;
        }

        // Check inner type for pointers/arrays
        if (desc.InnerType != null)
        {
            return HasUnsupportedTypeDetail(desc.InnerType);
        }

        return false;
    }

    /// <summary>
    /// Checks if a function has any unsupported types in its signature.
    /// </summary>
    public bool FunctionHasUnsupportedTypes(FunctionInfo func)
    {
        // Check return type
        if (HasUnsupportedType(func.ReturnType))
            return true;

        // Check arguments
        foreach (ArgumentInfo arg in func.Arguments)
        {
            if (HasUnsupportedType(arg.Type))
                return true;
        }

        return false;
    }

    /// <summary>
    /// Maps a type description to a C# type string.
    /// </summary>
    public string MapType(TypeDescription? type, bool forParameter = false, bool forReturn = false)
    {
        if (type?.Description == null)
            return "void";

        return MapTypeDescription(type.Description, forParameter, forReturn);
    }

    private string MapTypeDescription(TypeDescriptionDetail desc, bool forParameter, bool forReturn)
    {
        return desc.Kind switch
        {
            "Builtin" => MapBuiltinType(desc.BuiltinType ?? "void"),
            "User" => MapUserType(desc.Name ?? "void"),
            "Pointer" => MapPointerType(desc, forParameter, forReturn),
            "Array" => MapArrayType(desc),
            "Type" => MapTypeDescription(desc.InnerType!, forParameter, forReturn),
            _ => "nint" // Unknown types default to native int
        };
    }

    private static string MapBuiltinType(string builtinType)
    {
        return BuiltinTypeMap.TryGetValue(builtinType, out var mapped) ? mapped : "nint";
    }

    private string MapUserType(string name)
    {
        // Check known typedefs first
        if (KnownTypedefs.TryGetValue(name, out var known))
            return known;

        // Check if it's an enum (remove trailing underscore for C# name)
        if (_enumTypes.Contains(name))
        {
            return name.EndsWith('_') ? name[..^1] : name;
        }

        // Check if it's a by-value struct
        if (_byValueStructs.Contains(name))
            return name;

        // Check if it's an SDL type from Sdl3Sharp.Native - keep the type name
        if (SdlTypeToModule.ContainsKey(name))
            return name;

        // Check if it's an opaque struct - use nint as handle
        if (_opaqueStructs.Contains(name) || OpaqueHandleTypes.Contains(name))
            return "nint";

        // Default: assume it's a struct type
        return name;
    }

    private string MapPointerType(TypeDescriptionDetail desc, bool forParameter, bool forReturn)
    {
        TypeDescriptionDetail? innerType = desc.InnerType;
        if (innerType == null)
            return "nint";

        // Check for const
        var isConst = innerType.StorageClasses?.Contains("const") ?? false;

        // void* -> nint
        if (innerType.Kind == "Builtin" && innerType.BuiltinType == "void")
            return "nint";

        // char* -> string for parameters, nint for return
        if (innerType.Kind == "Builtin" && innerType.BuiltinType == "char")
        {
            if (forParameter && isConst)
                return "string"; // Will need [MarshalAs(UnmanagedType.LPUTF8Str)]
            return "nint"; // byte* essentially
        }

        // Pointer to builtin type
        if (innerType.Kind == "Builtin")
        {
            var baseType = MapBuiltinType(innerType.BuiltinType ?? "void");
            if (baseType == "void")
                return "nint";
            return $"{baseType}*";
        }

        // Pointer to user type
        if (innerType.Kind == "User")
        {
            var userName = innerType.Name ?? "";

            // Known typedefs that are pointers
            if (KnownTypedefs.TryGetValue(userName, out var known))
            {
                return $"{known}*";
            }

            // Enum pointer
            if (_enumTypes.Contains(userName))
            {
                var enumName = userName.EndsWith('_') ? userName[..^1] : userName;
                return $"{enumName}*";
            }

            // By-value struct pointer -> ref or pointer
            if (_byValueStructs.Contains(userName))
            {
                return $"{userName}*";
            }

            // SDL type pointer -> use the actual SDL type pointer for type safety
            if (SdlTypeToModule.ContainsKey(userName))
            {
                return $"{userName}*";
            }

            // Opaque struct pointer -> nint (it's already a pointer conceptually)
            if (_opaqueStructs.Contains(userName) || OpaqueHandleTypes.Contains(userName))
            {
                return "nint";
            }

            // Unknown user type - treat as opaque pointer
            return "nint";
        }

        // Pointer to pointer
        if (innerType.Kind == "Pointer")
        {
            return "nint"; // Double pointers become nint
        }

        return "nint";
    }

    private string MapArrayType(TypeDescriptionDetail desc)
    {
        // Fixed-size arrays in structs - we'll handle these specially
        TypeDescriptionDetail? innerType = desc.InnerType;
        if (innerType == null)
            return "nint";

        var bounds = desc.Bounds;
        var elementType = MapTypeDescription(innerType, false, false);

        // For fixed buffers, we return the element type and handle bounds elsewhere
        return elementType;
    }

    /// <summary>
    /// Gets the appropriate marshaling attribute for a parameter type.
    /// </summary>
    public static string? GetMarshalAsAttribute(TypeDescription? type, bool forParameter)
    {
        if (type?.Description == null)
            return null;

        TypeDescriptionDetail desc = type.Description;

        // bool needs MarshalAs for LibraryImport
        if (desc.Kind == "Builtin" && desc.BuiltinType == "bool")
        {
            return "[MarshalAs(UnmanagedType.U1)]";
        }

        // bool* (pointer to bool) also needs MarshalAs when used as ref bool
        if (desc.Kind == "Pointer" && desc.InnerType?.Kind == "Builtin" && desc.InnerType.BuiltinType == "bool")
        {
            return "[MarshalAs(UnmanagedType.U1)]";
        }

        // const char* for parameters -> LPUTF8Str
        if (desc.Kind == "Pointer" && desc.InnerType?.Kind == "Builtin" && desc.InnerType.BuiltinType == "char")
        {
            var isConst = desc.InnerType.StorageClasses?.Contains("const") ?? false;
            if (forParameter && isConst)
            {
                return "[MarshalAs(UnmanagedType.LPUTF8Str)]";
            }
        }

        return null;
    }

    /// <summary>
    /// Determines if a type should use 'ref' modifier.
    /// </summary>
    public bool ShouldUseRef(TypeDescription? type, ArgumentInfo arg)
    {
        if (type?.Description == null)
            return false;

        TypeDescriptionDetail desc = type.Description;

        // Instance pointers are passed as-is
        if (arg.IsInstancePointer)
            return false;

        // Pointer to by-value struct (non-const) should be ref
        if (desc.Kind == "Pointer" && desc.InnerType?.Kind == "User")
        {
            var userName = desc.InnerType.Name ?? "";
            var isConst = desc.InnerType.StorageClasses?.Contains("const") ?? false;

            if (_byValueStructs.Contains(userName) && !isConst)
            {
                return true;
            }
        }

        // Pointer to builtin (non-const, non-void) for out parameters
        if (desc.Kind == "Pointer" && desc.InnerType?.Kind == "Builtin")
        {
            var builtinType = desc.InnerType.BuiltinType;
            var isConst = desc.InnerType.StorageClasses?.Contains("const") ?? false;

            if (!isConst && builtinType != "void" && builtinType != "char")
            {
                return true;
            }
        }

        return false;
    }
}
