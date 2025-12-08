namespace Sdl3Sharp.ImGui.Generator;

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
        // Reset counters for each struct
        _bitfieldCounter = 0;
        _internalCounter = 0;

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

        // Filter out obsolete fields
        var fields = structInfo.Fields
            .Where(f => !Conditional.IsObsolete(f.Conditionals))
            .ToList();

        // Group fields into regular fields and bitfield groups
        List<FieldGroup> fieldGroups = GroupFields(fields);

        for (var groupIndex = 0; groupIndex < fieldGroups.Count; groupIndex++)
        {
            FieldGroup group = fieldGroups[groupIndex];

            if (group.IsBitfieldGroup)
            {
                GenerateBitfieldGroup(writer, group.Fields);
            }
            else
            {
                foreach (FieldInfo field in group.Fields)
                {
                    GenerateField(writer, field);
                }
            }

            if (groupIndex < fieldGroups.Count - 1 || methods.Count > 0)
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

    /// <summary>
    /// Represents a group of fields (either a single regular field or consecutive bitfields).
    /// </summary>
    private sealed class FieldGroup
    {
        public List<FieldInfo> Fields { get; } = [];
        public bool IsBitfieldGroup { get; set; }
    }

    /// <summary>
    /// Groups fields into regular fields and consecutive bitfield groups.
    /// </summary>
    private static List<FieldGroup> GroupFields(List<FieldInfo> fields)
    {
        var groups = new List<FieldGroup>();
        FieldGroup? currentBitfieldGroup = null;

        foreach (FieldInfo field in fields)
        {
            if (field.Width.HasValue)
            {
                // This is a bitfield
                currentBitfieldGroup ??= new FieldGroup { IsBitfieldGroup = true };
                currentBitfieldGroup.Fields.Add(field);
            }
            else
            {
                // This is a regular field
                if (currentBitfieldGroup != null)
                {
                    groups.Add(currentBitfieldGroup);
                    currentBitfieldGroup = null;
                }

                groups.Add(new FieldGroup { IsBitfieldGroup = false, Fields = { field } });
            }
        }

        // Don't forget the last bitfield group
        if (currentBitfieldGroup != null)
        {
            groups.Add(currentBitfieldGroup);
        }

        return groups;
    }

    /// <summary>
    /// Generates a packed bitfield group with a backing field and accessor properties.
    /// </summary>
    private void GenerateBitfieldGroup(CodeWriter writer, List<FieldInfo> bitfields)
    {
        // Calculate total bits and determine storage type
        var totalBits = bitfields.Sum(f => f.Width!.Value);
        var storageType = GetStorageType(totalBits);

        // Generate private backing field
        var backingFieldName = $"_bitfield{GetBitfieldIndex()}";
        writer.AppendLine($"private {storageType} {backingFieldName};");
        writer.AppendLine();

        // Generate accessor properties for each bitfield
        var bitOffset = 0;
        for (var i = 0; i < bitfields.Count; i++)
        {
            FieldInfo field = bitfields[i];
            var width = field.Width!.Value;
            var isInternal = field.IsInternal || field.IsAnonymous;
            var visibility = isInternal ? "private" : "public";
            var propertyType = GetBitfieldPropertyType(width);

            // For internal/anonymous fields, use generated name with original name in comment
            string fieldName;
            string? inlineComment = null;
            if (isInternal)
            {
                fieldName = $"_internal{GetInternalIndex()}";
                inlineComment = field.Name;
            }
            else
            {
                // Write documentation for public fields
                writer.WriteDocComment(field.Comments);
                fieldName = NamingConventions.ToPascalCase(field.Name);
            }

            // Generate the mask for this bitfield
            var mask = (1UL << width) - 1;
            var shiftedMask = mask << bitOffset;

            // Generate property with getter and setter
            var commentSuffix = inlineComment != null ? $" // {inlineComment}" : "";
            writer.AppendLine($"{visibility} {propertyType} {fieldName}{commentSuffix}");
            writer.OpenBrace();

            // Getter: extract bits from backing field
            if (propertyType == "bool")
            {
                writer.AppendLine($"readonly get => ({backingFieldName} & 0x{shiftedMask:X}U) != 0;");
                writer.AppendLine($"set => {backingFieldName} = ({storageType})(({backingFieldName} & ~0x{shiftedMask:X}U) | (value ? 0x{shiftedMask:X}U : 0));");
            }
            else
            {
                if (bitOffset == 0)
                {
                    writer.AppendLine($"readonly get => ({propertyType})({backingFieldName} & 0x{mask:X}U);");
                }
                else
                {
                    writer.AppendLine($"readonly get => ({propertyType})(({backingFieldName} >> {bitOffset}) & 0x{mask:X}U);");
                }

                if (bitOffset == 0)
                {
                    writer.AppendLine($"set => {backingFieldName} = ({storageType})(({backingFieldName} & ~0x{mask:X}U) | (({storageType})value & 0x{mask:X}U));");
                }
                else
                {
                    writer.AppendLine($"set => {backingFieldName} = ({storageType})(({backingFieldName} & ~0x{shiftedMask:X}U) | ((({storageType})value & 0x{mask:X}U) << {bitOffset}));");
                }
            }

            writer.CloseBrace();

            bitOffset += width;

            if (i < bitfields.Count - 1)
            {
                writer.AppendLine();
            }
        }
    }

    private int _bitfieldCounter;
    private int _internalCounter;

    private int GetBitfieldIndex()
    {
        return _bitfieldCounter++;
    }

    private int GetInternalIndex()
    {
        return _internalCounter++;
    }

    /// <summary>
    /// Determines the storage type needed for a bitfield group based on total bits.
    /// </summary>
    private static string GetStorageType(int totalBits)
    {
        return totalBits switch
        {
            <= 8 => "byte",
            <= 16 => "ushort",
            <= 32 => "uint",
            _ => "ulong"
        };
    }

    /// <summary>
    /// Determines the appropriate property type for a bitfield.
    /// </summary>
    private static string GetBitfieldPropertyType(int width)
    {
        // For 1-bit fields, use bool
        if (width == 1)
        {
            return "bool";
        }

        // For larger fields, use the smallest unsigned type that fits
        return width <= 8 ? "byte" : width <= 16 ? "ushort" : width <= 32 ? "uint" : "ulong";
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

        var returnType = _typeMapper.MapType(func.ReturnType);
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
                paramType = _typeMapper.MapType(arg.Type);
            }

            var paramName = NamingConventions.ToParameterName(arg.Name);

            // Get marshaling attribute
            var marshalAttr = TypeMapper.GetMarshalAsAttribute(arg.Type);

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

    private void GenerateField(CodeWriter writer, FieldInfo field)
    {
        var isInternal = field.IsInternal || field.IsAnonymous;
        var visibility = isInternal ? "private" : "public";

        // For internal/anonymous fields, use generated name with original name in comment
        string fieldName;
        string? inlineComment = null;
        if (isInternal)
        {
            fieldName = $"_internal{GetInternalIndex()}";
            inlineComment = field.Name;
        }
        else
        {
            // Write documentation for public fields
            writer.WriteDocComment(field.Comments);
            fieldName = NamingConventions.ToPascalCase(field.Name);
        }

        var fieldType = MapFieldType(field);
        var commentSuffix = inlineComment != null ? $" // {inlineComment}" : "";

        // Handle fixed-size arrays
        if (field.IsArray && field.Type?.Description?.Kind == "Array")
        {
            var bounds = field.Type.Description.Bounds;
            var elementType = GetArrayElementType(field.Type.Description);

            // Evaluate bounds expression if it contains known macros
            var evaluatedBounds = EvaluateBoundsExpression(bounds);

            if (evaluatedBounds != null && CanBeFixedBuffer(elementType))
            {
                // Fixed buffer for primitive types
                writer.AppendLine($"{visibility} fixed {elementType} {fieldName}[{evaluatedBounds}];{commentSuffix}");
            }
            else if (evaluatedBounds != null)
            {
                // For non-primitive types, we need a different approach
                // Generate individual fields or use InlineArray (.NET 8+)
                writer.AppendLine($"// TODO: Fixed array of {elementType}[{bounds}]");
                writer.AppendLine($"private {elementType} _{fieldName}_0;{commentSuffix}");
            }
            else
            {
                // Unknown bounds - use pointer
                writer.AppendLine($"{visibility} nint {fieldName};{commentSuffix}");
            }
        }
        else
        {
            writer.AppendLine($"{visibility} {fieldType} {fieldName};{commentSuffix}");
        }
    }

    private string MapFieldType(FieldInfo field)
    {
        // If the field's type is null or refers to an internal struct, use nint
        return field.Type == null || _typeMapper.IsInternalType(field.Type)
            ? "nint"
            : _typeMapper.MapType(field.Type);
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

        // If the element type refers to an internal struct, use nint
        return _typeMapper.IsInternalType(innerType)
            ? "nint"
            : _typeMapper.MapType(innerType);
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
                var evaluatedBounds = EvaluateBoundsExpression(bounds);
                if (evaluatedBounds != null && CanBeFixedBuffer(elementType))
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

    /// <summary>
    /// Evaluates a C bounds expression, replacing known macros with their values.
    /// </summary>
    /// <param name="bounds">The bounds expression from the JSON.</param>
    /// <returns>The evaluated bounds as a string, or null if the expression cannot be evaluated.</returns>
    private static string? EvaluateBoundsExpression(string? bounds)
    {
        if (bounds == null)
        {
            return null;
        }

        // Dictionary of known C macros and their values
        // Using the WCHAR32 variant (0x10FFFF) as it's what the json appears to use
        var knownMacros = new Dictionary<string, long>
        {
            ["IM_UNICODE_CODEPOINT_MAX"] = 0x10FFFF,
            ["IM_DRAWLIST_TEX_LINES_WIDTH_MAX"] = 63
        };

        // Try to replace macros and evaluate the expression
        var expression = bounds;
        foreach ((var macro, var value) in knownMacros)
        {
            expression = expression.Replace(macro, value.ToString());
        }

        // If the expression is just a number, return it directly
        if (int.TryParse(expression.Trim(), out _))
        {
            return expression.Trim();
        }

        // Try to evaluate simple arithmetic expressions like "(0x10FFFF +1)/8192/8"
        try
        {
            // Remove spaces and handle hex numbers
            expression = expression.Replace(" ", "");

            // Convert hex numbers to decimal
            while (expression.Contains("0x", StringComparison.OrdinalIgnoreCase))
            {
                var idx = expression.IndexOf("0x", StringComparison.OrdinalIgnoreCase);
                var endIdx = idx + 2;
                while (endIdx < expression.Length && Uri.IsHexDigit(expression[endIdx]))
                {
                    endIdx++;
                }

                var hexStr = expression.Substring(idx + 2, endIdx - idx - 2);
                var decValue = Convert.ToInt64(hexStr, 16);
                expression = string.Concat(expression.AsSpan(0, idx), decValue.ToString(), expression.AsSpan(endIdx));
            }

            // Simple evaluation using DataTable for basic arithmetic
            var dt = new System.Data.DataTable();
            var result = dt.Compute(expression, null);
            if (result is int intResult)
            {
                return intResult.ToString();
            }
            else if (result is long longResult)
            {
                return longResult.ToString();
            }
            else if (result is double doubleResult)
            {
                return ((int)doubleResult).ToString();
            }
            else if (result is decimal decimalResult)
            {
                return ((int)decimalResult).ToString();
            }
        }
        catch
        {
            // If evaluation fails, return the original expression
            // This might still fail at compile time if it contains unknown macros
        }

        return bounds;
    }
}
