namespace SdlSharp
{
    /// <summary>
    /// A version number for a component.
    /// </summary>
    public readonly struct Version
    {
        /// <summary>
        /// The major version number.
        /// </summary>
        public byte Major { get; }

        /// <summary>
        /// The minor version number.
        /// </summary>
        public byte Minor { get; }

        /// <summary>
        /// The patch number.
        /// </summary>
        public byte Patch { get; }

        /// <summary>
        /// Constructs a version.
        /// </summary>
        /// <param name="major">The major version number.</param>
        /// <param name="minor">The minor version number.</param>
        /// <param name="patch">The patch number.</param>
        public Version(byte major, byte minor, byte patch)
        {
            Major = major;
            Minor = minor;
            Patch = patch;
        }

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
