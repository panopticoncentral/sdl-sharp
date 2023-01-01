namespace SdlSharp
{
    /// <summary>
    /// A version number for a component.
    /// </summary>
    public readonly record struct Version(byte Major, byte Minor, byte Patch)
    {
        /// <summary>
        /// Tests to see if a version is at least a particular version.
        /// </summary>
        /// <param name="targetVersion">The version to test against.</param>
        /// <param name="version">The version to test.</param>
        /// <returns><c>true</c> if the version is at least the target, <c>false</c> otherwise.</returns>
        public static bool VersionAtLeast(Version targetVersion, Version version) =>
            Native.SDL_VersionNumber(targetVersion) >= Native.SDL_VersionNumber(version);
    }
}
