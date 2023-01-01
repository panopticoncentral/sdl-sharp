namespace SdlSharp
{
    /// <summary>
    /// A version number for a component.
    /// </summary>
    public readonly unsafe record struct Version(int Major, int Minor, int Patch)
    {
        internal Version(Native.SDL_version* version) : this(version->major, version->minor, version->patch)
        {
        }

        /// <summary>
        /// Tests to see if a version is at least a particular version.
        /// </summary>
        /// <param name="targetVersion">The version to test against.</param>
        /// <param name="version">The version to test.</param>
        /// <returns><c>true</c> if the version is at least the target, <c>false</c> otherwise.</returns>
        public static bool VersionAtLeast(Version targetVersion, Version version) =>
            Native.SDL_VersionNumber(targetVersion.ToNative()) >= Native.SDL_VersionNumber(version.ToNative());

        internal Native.SDL_version ToNative() => new((byte)Major, (byte)Minor, (byte)Patch);
    }
}
