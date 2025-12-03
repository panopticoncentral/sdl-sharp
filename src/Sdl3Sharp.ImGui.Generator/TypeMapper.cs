namespace Sdl3Sharp.ImGui.Generator;

/// <summary>
/// Maps C types from Dear Bindings to C# types.
/// </summary>
public sealed class TypeMapper
{
    private readonly HashSet<string> _structs = [];
    private readonly HashSet<string> _internalStructs = [];
    private readonly HashSet<string> _opaqueStructs = [];
    private readonly HashSet<string> _enumTypes = [];
    private readonly Dictionary<string, string> _typedefs = [];
    private readonly Dictionary<string, string> _backendStructRenames = [];

    /// <summary>
    /// Gets the set of opaque struct names (forward-declared structs with no exposed fields).
    /// </summary>
    public IReadOnlySet<string> OpaqueStructs => _opaqueStructs;

    /// <summary>
    /// Gets the set of internal struct names (not exposed publicly).
    /// </summary>
    public IReadOnlySet<string> InternalStructs => _internalStructs;

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
    /// Typedefs that should become wrapper structs are not included here;
    /// they are handled by <see cref="TypedefGenerator.WrapperTypedefs"/> and
    /// <see cref="TypedefGenerator.CallbackTypedefs"/>.
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
        ["size_t"] = "nuint",
        ["va_list"] = "nint", // Will be filtered out at function level
        ["ImStr"] = "nint",   // Will be filtered out at function level
    };

    /// <summary>
    /// Types that should cause functions to be skipped (not marshallable).
    /// </summary>
    public static readonly HashSet<string> UnsupportedTypes =
    [
        "va_list",
        "ImColor",
        "ImStr",
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

    public void Initialize(DearBindingsRoot root)
    {
        // Collect public structs (internal/anonymous structs are filtered out at generation sites)
        foreach (StructInfo? s in root.Structs.Where(s => !s.ForwardDeclaration && !s.IsInternal && !s.IsAnonymous))
        {
            _ = _structs.Add(s.Name);

            // Track backend struct renames (ImGui_ImplSDLGPU3_InitInfo -> GpuInitInfo)
            var cleanName = NamingConventions.CleanBackendStructName(s.Name);
            if (cleanName != s.Name)
            {
                _backendStructRenames[s.Name] = cleanName;
            }
        }

        // Collect internal/anonymous structs (for mapping their pointers to nint)
        foreach (StructInfo? s in root.Structs.Where(s => !s.ForwardDeclaration && (s.IsInternal || s.IsAnonymous)))
        {
            _ = _internalStructs.Add(s.Name);
        }

        // Collect opaque types (forward-declared structs with no exposed fields)
        foreach (StructInfo? s in root.Structs.Where(s => s.ForwardDeclaration && !SdlTypeToModule.ContainsKey(s.Name)))
        {
            _ = _opaqueStructs.Add(s.Name);
        }

        // Collect enums
        foreach (EnumInfo e in root.Enums)
        {
            _ = _enumTypes.Add(e.Name);
            // Also add without trailing underscore (ImGuiWindowFlags_ -> ImGuiWindowFlags)
            if (e.Name.EndsWith('_'))
            {
                _ = _enumTypes.Add(e.Name[..^1]);
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

    private static bool UsesSdlNativeTypesDetail(TypeDescriptionDetail desc)
    {
        // Check direct user type
        if (desc.Kind == "User" && desc.Name != null)
        {
            if (SdlTypeToModule.ContainsKey(desc.Name))
            {
                return true;
            }
        }

        // Check inner type for pointers/arrays
        return desc.InnerType != null && UsesSdlNativeTypesDetail(desc.InnerType);
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
        {
            return;
        }

        CollectSdlModulesFromTypeDetail(type.Description, modules);
    }

    private static void CollectSdlModulesFromTypeDetail(TypeDescriptionDetail desc, HashSet<string> modules)
    {
        if (desc.Kind == "User" && desc.Name != null)
        {
            if (SdlTypeToModule.TryGetValue(desc.Name, out var module))
            {
                _ = modules.Add(module);
            }
        }

        if (desc.InnerType != null)
        {
            CollectSdlModulesFromTypeDetail(desc.InnerType, modules);
        }
    }

    /// <summary>
    /// Checks if a type is an unsupported type.
    /// </summary>
    public static bool IsUnsupportedType(StructInfo structInfo)
    {
        return UnsupportedTypes.Contains(structInfo.Name);
    }

    /// <summary>
    /// Checks if a type refers to an internal struct (should be mapped to nint).
    /// </summary>
    public bool IsInternalType(TypeDescription? type)
    {
        return type?.Description != null && IsInternalTypeDetail(type.Description);
    }

    private bool IsInternalTypeDetail(TypeDescriptionDetail desc)
    {
        // Check direct user type
        if (desc.Kind == "User" && desc.Name != null)
        {
            return _internalStructs.Contains(desc.Name);
        }

        // Check inner type for pointers/arrays
        return desc.InnerType != null && IsInternalTypeDetail(desc.InnerType);
    }

    /// <summary>
    /// Checks if a type contains unsupported types (va_list, ImStr, etc.)
    /// </summary>
    public static bool HasUnsupportedType(TypeDescription? type)
    {
        return (type?.Description) != null && HasUnsupportedTypeDetail(type.Description);
    }

    private static bool HasUnsupportedTypeDetail(TypeDescriptionDetail desc)
    {
        // Check direct user type
        if (desc.Kind == "User" && desc.Name != null)
        {
            if (UnsupportedTypes.Contains(desc.Name))
            {
                return true;
            }
        }

        // Check inner type for pointers/arrays
        return desc.InnerType != null && HasUnsupportedTypeDetail(desc.InnerType);
    }

    /// <summary>
    /// Checks if a function has any unsupported types in its signature.
    /// </summary>
    public static bool FunctionHasUnsupportedTypes(FunctionInfo func)
    {
        // Check return type
        if (HasUnsupportedType(func.ReturnType))
        {
            return true;
        }

        // Check arguments
        foreach (ArgumentInfo arg in func.Arguments)
        {
            if (HasUnsupportedType(arg.Type))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Maps a type description to a C# type string.
    /// </summary>
    public string MapType(TypeDescription? type, bool forParameter = false, bool forReturn = false)
    {
        return type?.Description == null ? "void" : MapTypeDescription(type.Description, forParameter, forReturn);
    }

    private string MapTypeDescription(TypeDescriptionDetail desc, bool forParameter, bool forReturn)
    {
        return desc.Kind switch
        {
            "Builtin" => MapBuiltinType(desc.BuiltinType ?? "void"),
            "User" => MapUserType(desc.Name ?? "void"),
            "Pointer" => MapPointerType(desc),
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
        // Check if it's a wrapper typedef (these become type-safe wrapper structs)
        if (TypedefGenerator.WrapperTypedefs.ContainsKey(name))
        {
            return name;
        }

        // Check if it's a callback typedef (these become wrapper structs)
        if (TypedefGenerator.CallbackTypedefs.Contains(name))
        {
            return name;
        }

        // Check known typedefs that map directly to primitives
        if (KnownTypedefs.TryGetValue(name, out var known))
        {
            return known;
        }

        // Check if it's an enum (use CleanEnumName for consistent naming)
        if (_enumTypes.Contains(name))
        {
            return NamingConventions.CleanEnumName(name);
        }

        // Check if it's a struct (use cleaned name for backend structs)
        if (_structs.Contains(name))
        {
            return _backendStructRenames.TryGetValue(name, out var cleanName) ? cleanName : name;
        }

        // Check if it's an SDL type from Sdl3Sharp.Native - keep the type name
        if (SdlTypeToModule.ContainsKey(name))
        {
            return name;
        }

        // Check if it's an opaque struct - return wrapper struct name
        if (_opaqueStructs.Contains(name))
        {
            return name;
        }

        // Default: assume it's a struct type
        return name;
    }

    private string MapPointerType(TypeDescriptionDetail desc)
    {
        TypeDescriptionDetail? innerType = desc.InnerType;
        if (innerType == null)
        {
            return "nint";
        }

        // void* -> nint
        if (innerType.Kind == "Builtin" && innerType.BuiltinType == "void")
        {
            return "nint";
        }

        // char* -> byte* (marshalling handled at managed wrapper level)
        if (innerType.Kind == "Builtin" && innerType.BuiltinType == "char")
        {
            return "byte*";
        }

        // Pointer to builtin type
        if (innerType.Kind == "Builtin")
        {
            var baseType = MapBuiltinType(innerType.BuiltinType ?? "void");
            return baseType == "void" ? "nint" : $"{baseType}*";
        }

        // Pointer to user type
        if (innerType.Kind == "User")
        {
            var userName = innerType.Name ?? "";

            // Wrapper typedef pointers
            if (TypedefGenerator.WrapperTypedefs.ContainsKey(userName))
            {
                return $"{userName}*";
            }

            // Known typedefs that map to primitive pointers
            if (KnownTypedefs.TryGetValue(userName, out var known))
            {
                return $"{known}*";
            }

            // Enum pointer
            if (_enumTypes.Contains(userName))
            {
                var enumName = NamingConventions.CleanEnumName(userName);
                return $"{enumName}*";
            }

            // Struct -> typed pointer (use cleaned name for backend structs)
            if (_structs.Contains(userName))
            {
                var structName = _backendStructRenames.TryGetValue(userName, out var cleanName) ? cleanName : userName;
                return $"{structName}*";
            }

            // SDL type pointer -> use the actual SDL type pointer for type safety
            if (SdlTypeToModule.ContainsKey(userName))
            {
                return $"{userName}*";
            }

            // Opaque struct pointer -> return wrapper type (these are already pointers conceptually)
            if (_opaqueStructs.Contains(userName))
            {
                return userName;
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
        {
            return "nint";
        }

        var elementType = MapTypeDescription(innerType, false, false);

        // For fixed buffers, we return the element type and handle bounds elsewhere
        return elementType;
    }

    /// <summary>
    /// Gets the appropriate marshaling attribute for a parameter type.
    /// </summary>
    public static string? GetMarshalAsAttribute(TypeDescription? type)
    {
        if (type?.Description == null)
        {
            return null;
        }

        TypeDescriptionDetail desc = type.Description;

        // bool needs MarshalAs for LibraryImport
        return desc.Kind == "Builtin" && desc.BuiltinType == "bool" ? "[MarshalAs(UnmanagedType.U1)]" : null;
    }
}
