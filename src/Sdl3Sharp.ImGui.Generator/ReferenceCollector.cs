namespace Sdl3Sharp.ImGui.Generator;

public static class ReferenceCollector
{
    private static void AddUsage(Dictionary<string, bool> referencedTypes, string typeName, bool isPointer)
    {
        var hasValue = !isPointer;
        if (referencedTypes.TryGetValue(typeName, out var existingValue))
        {
            if (hasValue != existingValue)
            {
                hasValue = existingValue || hasValue;
            }
        }

        referencedTypes[typeName] = hasValue;
    }

    private static bool CollectUserType(DearBindingsRoot root, Dictionary<string, bool> referencedTypes, string? typeName, bool isPointer)
    {
        if (typeName == null
            || TypeMapper.UnsupportedTypes.Contains(typeName)
            || TypeMapper.KnownTypedefs.ContainsKey(typeName)
            || TypeMapper.SdlTypeToModule.ContainsKey(typeName))
        {
            return false;
        }

        foreach (DefineInfo defineInfo in root.Defines)
        {
            if (defineInfo.Name == typeName)
            {
                if (!defineInfo.IsInternal)
                {
                    AddUsage(referencedTypes, typeName, isPointer);
                }

                return !defineInfo.IsInternal;
            }
        }

        foreach (EnumInfo enumInfo in root.Enums)
        {
            if (enumInfo.Name == typeName)
            {
                if (!enumInfo.IsInternal)
                {
                    AddUsage(referencedTypes, typeName, isPointer);
                }

                return !enumInfo.IsInternal;
            }
        }

        foreach (TypedefInfo typedefInfo in root.Typedefs)
        {
            if (typedefInfo.Name == typeName)
            {
                if (!typedefInfo.IsInternal)
                {
                    AddUsage(referencedTypes, typeName, isPointer);
                }

                return !typedefInfo.IsInternal;
            }
        }

        foreach (StructInfo structInfo in root.Structs)
        {
            if (structInfo.Name == typeName)
            {
                if (!structInfo.IsInternal)
                {
                    if (typeName.StartsWith("ImVector_"))
                    {
                        FieldInfo dataField = structInfo.Fields.Single(f => f.Name == "Data");
                        TypeDescriptionDetail? elementType = dataField.Type?.Description?.InnerType;
                        if (elementType != null
                            && elementType.Kind == "User"
                            && elementType.Name != null
                            && !TypeMapper.KnownTypedefs.ContainsKey(elementType.Name)
                            && !CollectUserType(root, referencedTypes, elementType.Name, true))
                        {
                            return false;
                        }
                    }

                    AddUsage(referencedTypes, typeName, isPointer);
                }

                return !structInfo.IsInternal;
            }
        }

        return false;
    }

    private static void CollectType(DearBindingsRoot root, Dictionary<string, bool> referencedTypes, TypeDescriptionDetail? description, FunctionPointerDetails? functionPointerDetails, bool isPointer = false)
    {
        if (description == null)
        {
            return;
        }

        switch (description.Kind)
        {
            case "User":
                _ = CollectUserType(root, referencedTypes, description.Name, isPointer);
                break;
            case "Pointer":
            case "Array":
                CollectType(root, referencedTypes, description.InnerType, null, true);
                break;
            case "Type":
                if (functionPointerDetails == null || functionPointerDetails.Flavour != "function_pointer")
                {
                    return;
                }

                CollectType(root, referencedTypes, functionPointerDetails.ReturnType?.Description, functionPointerDetails.ReturnType?.TypeDetails);
                foreach (ArgumentInfo arg in functionPointerDetails.Arguments)
                {
                    CollectType(root, referencedTypes, arg.Type?.Description, arg.Type?.TypeDetails);
                }

                break;

            default:
                break;
        }
    }

    public static Dictionary<string, bool> CollectReferencedTypes(DearBindingsRoot root)
    {
        var referencedTypes = new Dictionary<string, bool>();

        // Collect types from functions
        foreach (FunctionInfo function in root.Functions)
        {
            if (function.IsInternal || (function.OriginalClass != null && TypeMapper.UnsupportedTypes.Contains(function.OriginalClass)))
            {
                continue;
            }

            CollectType(root, referencedTypes, function.ReturnType?.Description, function.ReturnType?.TypeDetails);
            foreach (ArgumentInfo arg in function.Arguments)
            {
                if (arg.Name == "self")
                {
                    continue;
                }

                CollectType(root, referencedTypes, arg.Type?.Description, arg.Type?.TypeDetails);
            }
        }

        // Collect types from structs
        foreach (StructInfo structInfo in root.Structs)
        {
            if (structInfo.IsInternal || TypeMapper.UnsupportedTypes.Contains(structInfo.Name))
            {
                continue;
            }

            foreach (FieldInfo field in structInfo.Fields)
            {
                if (field.IsInternal)
                {
                    continue;
                }

                CollectType(root, referencedTypes, field.Type?.Description, field.Type?.TypeDetails);
            }
        }

        // Collect types from typedefs
        foreach (TypedefInfo typedefInfo in root.Typedefs)
        {
            if (typedefInfo.IsInternal || TypeMapper.UnsupportedTypes.Contains(typedefInfo.Name))
            {
                continue;
            }

            CollectType(root, referencedTypes, typedefInfo.Type?.Description, typedefInfo.Type?.TypeDetails);
        }

        return referencedTypes;
    }
}
