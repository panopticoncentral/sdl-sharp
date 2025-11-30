using System.Runtime.InteropServices;
using System.Text;
using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Init;

namespace Sdl3Sharp;

/// <summary>
/// Provides access to SDL application metadata properties.
/// </summary>
public unsafe static class AppMetadata
{
    /// <summary>
    /// Gets or sets the human-readable name of the application, like "My Game 2: Bad Guy's Revenge!".
    /// </summary>
    public static string? Name
    {
        get => GetProperty(SDL_PROP_APP_METADATA_NAME_STRING);
        set => SetProperty(SDL_PROP_APP_METADATA_NAME_STRING, value);
    }

    /// <summary>
    /// Gets or sets the version of the application. There are no rules on format.
    /// </summary>
    public static string? Version
    {
        get => GetProperty(SDL_PROP_APP_METADATA_VERSION_STRING);
        set => SetProperty(SDL_PROP_APP_METADATA_VERSION_STRING, value);
    }

    /// <summary>
    /// Gets or sets a unique string in reverse-domain format that identifies this app, like "com.example.mygame2".
    /// </summary>
    public static string? Identifier
    {
        get => GetProperty(SDL_PROP_APP_METADATA_IDENTIFIER_STRING);
        set => SetProperty(SDL_PROP_APP_METADATA_IDENTIFIER_STRING, value);
    }

    /// <summary>
    /// Gets or sets the human-readable name of the creator/developer/maker of this app, like "MojoWorkshop, LLC".
    /// </summary>
    public static string? Creator
    {
        get => GetProperty(SDL_PROP_APP_METADATA_CREATOR_STRING);
        set => SetProperty(SDL_PROP_APP_METADATA_CREATOR_STRING, value);
    }

    /// <summary>
    /// Gets or sets the human-readable copyright notice, like "Copyright (c) 2024 MojoWorkshop, LLC".
    /// </summary>
    public static string? Copyright
    {
        get => GetProperty(SDL_PROP_APP_METADATA_COPYRIGHT_STRING);
        set => SetProperty(SDL_PROP_APP_METADATA_COPYRIGHT_STRING, value);
    }

    /// <summary>
    /// Gets or sets a URL to the app on the web. Maybe a product page, or a storefront, or even a GitHub repository.
    /// </summary>
    public static string? Url
    {
        get => GetProperty(SDL_PROP_APP_METADATA_URL_STRING);
        set => SetProperty(SDL_PROP_APP_METADATA_URL_STRING, value);
    }

    /// <summary>
    /// Gets or sets the type of application. Currently can be "game", "mediaplayer", or "application".
    /// </summary>
    public static string? Type
    {
        get => GetProperty(SDL_PROP_APP_METADATA_TYPE_STRING);
        set => SetProperty(SDL_PROP_APP_METADATA_TYPE_STRING, value);
    }

    /// <summary>
    /// Gets the value of a metadata property by name.
    /// </summary>
    public static string? GetProperty(string propertyName)
    {
        fixed (byte* namePtr = Encoding.UTF8.GetBytes(propertyName + '\0'))
        {
            return Marshal.PtrToStringUTF8((nint)SDL_GetAppMetadataProperty(namePtr));
        }
    }

    /// <summary>
    /// Sets the value of a metadata property by name.
    /// </summary>
    public static void SetProperty(string propertyName, string? value)
    {
        fixed (byte* namePtr = Encoding.UTF8.GetBytes(propertyName + '\0'))
        fixed (byte* valuePtr = value != null ? Encoding.UTF8.GetBytes(value + '\0') : null)
        {
            _ = CheckErrorBool(SDL_SetAppMetadataProperty(namePtr, valuePtr));
        }
    }
}
