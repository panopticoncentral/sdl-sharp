namespace SdlSharp.Graphics
{
    /// <summary>
    /// Image handling from SDL_image
    /// </summary>
    public static unsafe class Image
    {
        /// <summary>
        /// Loads an image from a file.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns>The image.</returns>
        public static Surface Load(string filename)
        {
            fixed (byte* ptr = Native.StringToUtf8(filename))
            {
                return new(Native.IMG_Load(ptr));
            }
        }

        /// <summary>
        /// Loads an image from a file compatible with a target surface.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="targetSurface">The target surface.</param>
        /// <returns>The image.</returns>
        public static Surface Load(string filename, Surface targetSurface)
        {
            using var loadedSurface = Load(filename);
            return loadedSurface.Convert(targetSurface.PixelFormat);
        }

        /// <summary>
        /// Loads an image from a file into a texture.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="renderer">The renderer.</param>
        /// <param name="colorKey">The color key for the image.</param>
        /// <returns>The image.</returns>
        public static Texture Load(string filename, Renderer renderer, Color? colorKey = default)
        {
            using var loadedSurface = Load(filename);
            if (colorKey != null)
            {
                loadedSurface.ColorKey = loadedSurface.PixelFormat.Map(colorKey.Value.Red, colorKey.Value.Green, colorKey.Value.Blue, colorKey.Value.Alpha);
            }
            return renderer.CreateTexture(loadedSurface);
        }

        /// <summary>
        /// Loads an image from a storage.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <param name="shouldDispose">Whether the storage should be disposed when loading is finished.</param>
        /// <param name="type">The type of the image.</param>
        /// <returns>The image.</returns>
        public static Surface Load(RWOps rwops, bool shouldDispose, string type)
        {
            fixed (byte* ptr = Native.StringToUtf8(type))
            {
                return new(Native.IMG_LoadTyped_RW(rwops.ToNative(), Native.BoolToInt(shouldDispose), ptr));
            }
        }

        /// <summary>
        /// Loads an image from a storage into a texture.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <param name="shouldDispose">Whether the storage should be disposed when loading is finished.</param>
        /// <param name="type">The type of the image.</param>
        /// <param name="renderer">The renderer.</param>
        /// <param name="colorKey">The color key for the image.</param>
        /// <returns>The image.</returns>
        public static Texture Load(RWOps rwops, bool shouldDispose, string type, Renderer renderer, Color? colorKey = default)
        {
            using var loadedSurface = Load(rwops, shouldDispose, type);
            if (colorKey != null)
            {
                loadedSurface.ColorKey = loadedSurface.PixelFormat.Map(colorKey.Value.Red, colorKey.Value.Green, colorKey.Value.Blue, colorKey.Value.Alpha);
            }
            return renderer.CreateTexture(loadedSurface);
        }

        /// <summary>
        /// Loads an image from a storage.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <param name="shouldDispose">Whether the storage should be disposed when loading is finished.</param>
        /// <returns>The image.</returns>
        public static Surface Load(RWOps rwops, bool shouldDispose) =>
            new(Native.IMG_Load_RW(rwops.ToNative(), Native.BoolToInt(shouldDispose)));

        /// <summary>
        /// Loads an image as a texture from a file.
        /// </summary>
        /// <param name="renderer">The renderer.</param>
        /// <param name="filename">The filename.</param>
        /// <returns>The texture.</returns>
        public static Texture LoadTexture(Renderer renderer, string filename)
        {
            fixed (byte* ptr = Native.StringToUtf8(filename))
            {
                return new(Native.IMG_LoadTexture(renderer.ToNative(), ptr));
            }
        }

        /// <summary>
        /// Loads an image as a texture from a storage.
        /// </summary>
        /// <param name="renderer">The renderer.</param>
        /// <param name="rwops">The storage.</param>
        /// <param name="shouldDispose">Whether the storage should be disposed when loading is finished.</param>
        /// <param name="type">The type of the image.</param>
        /// <returns>The texture.</returns>
        public static Texture LoadTexture(Renderer renderer, RWOps rwops, bool shouldDispose, string type)
        {
            fixed (byte* ptr = Native.StringToUtf8(type))
            {
                return new(Native.IMG_LoadTextureTyped_RW(renderer.ToNative(), rwops.ToNative(), Native.BoolToInt(shouldDispose), ptr));
            }
        }

        /// <summary>
        /// Loads an image as a texture from a storage.
        /// </summary>
        /// <param name="renderer">The renderer.</param>
        /// <param name="rwops">The storage.</param>
        /// <param name="shouldDispose">Whether the storage should be disposed when loading is finished.</param>
        /// <returns>The texture.</returns>
        public static Texture LoadTexture(Renderer renderer, RWOps rwops, bool shouldDispose) =>
            new(Native.IMG_LoadTexture_RW(renderer.ToNative(), rwops.ToNative(), Native.BoolToInt(shouldDispose)));

        /// <summary>
        /// Whether the storage is an AVIF image.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns><c>true</c> if it is, <c>false</c> otherwise.</returns>
        public static bool IsAvif(RWOps rwops) =>
            Native.IMG_isAVIF(rwops.ToNative());

        /// <summary>
        /// Whether the storage is an ICO image.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns><c>true</c> if it is, <c>false</c> otherwise.</returns>
        public static bool IsIco(RWOps rwops) =>
            Native.IMG_isICO(rwops.ToNative());

        /// <summary>
        /// Whether the storage is an CUR image.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns><c>true</c> if it is, <c>false</c> otherwise.</returns>
        public static bool IsCur(RWOps rwops) =>
            Native.IMG_isCUR(rwops.ToNative());

        /// <summary>
        /// Whether the storage is an BMP image.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns><c>true</c> if it is, <c>false</c> otherwise.</returns>
        public static bool IsBmp(RWOps rwops) =>
            Native.IMG_isBMP(rwops.ToNative());

        /// <summary>
        /// Whether the storage is an GIF image.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns><c>true</c> if it is, <c>false</c> otherwise.</returns>
        public static bool IsGif(RWOps rwops) =>
            Native.IMG_isGIF(rwops.ToNative());

        /// <summary>
        /// Whether the storage is an JPG image.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns><c>true</c> if it is, <c>false</c> otherwise.</returns>
        public static bool IsJpg(RWOps rwops) =>
            Native.IMG_isJPG(rwops.ToNative());

        /// <summary>
        /// Whether the storage is an JXL image.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns><c>true</c> if it is, <c>false</c> otherwise.</returns>
        public static bool IsJxl(RWOps rwops) =>
            Native.IMG_isJXL(rwops.ToNative());

        /// <summary>
        /// Whether the storage is an LBM image.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns><c>true</c> if it is, <c>false</c> otherwise.</returns>
        public static bool IsLbm(RWOps rwops) =>
            Native.IMG_isLBM(rwops.ToNative());

        /// <summary>
        /// Whether the storage is an PCX image.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns><c>true</c> if it is, <c>false</c> otherwise.</returns>
        public static bool IsPcx(RWOps rwops) =>
            Native.IMG_isPCX(rwops.ToNative());

        /// <summary>
        /// Whether the storage is an PNG image.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns><c>true</c> if it is, <c>false</c> otherwise.</returns>
        public static bool IsPng(RWOps rwops) =>
            Native.IMG_isPNG(rwops.ToNative());

        /// <summary>
        /// Whether the storage is an PNM image.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns><c>true</c> if it is, <c>false</c> otherwise.</returns>
        public static bool IsPnm(RWOps rwops) =>
            Native.IMG_isPNM(rwops.ToNative());

        /// <summary>
        /// Whether the storage is an SVG image.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns><c>true</c> if it is, <c>false</c> otherwise.</returns>
        public static bool IsSvg(RWOps rwops) =>
            Native.IMG_isSVG(rwops.ToNative());

        /// <summary>
        /// Whether the storage is a QOI image.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns><c>true</c> if it is, <c>false</c> otherwise.</returns>
        public static bool IsQoi(RWOps rwops) =>
            Native.IMG_isQOI(rwops.ToNative());

        /// <summary>
        /// Whether the storage is an TIFF image.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns><c>true</c> if it is, <c>false</c> otherwise.</returns>
        public static bool IsTif(RWOps rwops) =>
            Native.IMG_isTIF(rwops.ToNative());

        /// <summary>
        /// Whether the storage is an XCF image.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns><c>true</c> if it is, <c>false</c> otherwise.</returns>
        public static bool IsXcf(RWOps rwops) =>
            Native.IMG_isXCF(rwops.ToNative());

        /// <summary>
        /// Whether the storage is an XPM image.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns><c>true</c> if it is, <c>false</c> otherwise.</returns>
        public static bool IsXpm(RWOps rwops) =>
            Native.IMG_isXPM(rwops.ToNative());

        /// <summary>
        /// Whether the storage is an XV image.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns><c>true</c> if it is, <c>false</c> otherwise.</returns>
        public static bool IsXv(RWOps rwops) =>
            Native.IMG_isXV(rwops.ToNative());

        /// <summary>
        /// Whether the storage is an WEBP image.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns><c>true</c> if it is, <c>false</c> otherwise.</returns>
        public static bool IsWebp(RWOps rwops) =>
            Native.IMG_isWEBP(rwops.ToNative());

        /// <summary>
        /// Load an AVIF image from a storage.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns>The image.</returns>
        public static Surface LoadAvif(RWOps rwops) =>
                    new(Native.IMG_LoadAVIF_RW(rwops.ToNative()));

        /// <summary>
        /// Load a ICO image from a storage.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns>The image.</returns>
        public static Surface LoadIco(RWOps rwops) =>
                    new(Native.IMG_LoadICO_RW(rwops.ToNative()));

        /// <summary>
        /// Load a CUR image from a storage.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns>The image.</returns>
        public static Surface LoadCur(RWOps rwops) =>
                    new(Native.IMG_LoadCUR_RW(rwops.ToNative()));

        /// <summary>
        /// Load a BMP image from a storage.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns>The image.</returns>
        public static Surface LoadBmp(RWOps rwops) =>
                    new(Native.IMG_LoadBMP_RW(rwops.ToNative()));

        /// <summary>
        /// Load a GIF image from a storage.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns>The image.</returns>
        public static Surface LoadGif(RWOps rwops) =>
                    new(Native.IMG_LoadGIF_RW(rwops.ToNative()));

        /// <summary>
        /// Load a JPG image from a storage.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns>The image.</returns>
        public static Surface LoadJpg(RWOps rwops) =>
                    new(Native.IMG_LoadJPG_RW(rwops.ToNative()));

        /// <summary>
        /// Load a JXL image from a storage.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns>The image.</returns>
        public static Surface LoadJxl(RWOps rwops) =>
                    new(Native.IMG_LoadJXL_RW(rwops.ToNative()));

        /// <summary>
        /// Load a LBM image from a storage.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns>The image.</returns>
        public static Surface LoadLbm(RWOps rwops) =>
                    new(Native.IMG_LoadLBM_RW(rwops.ToNative()));

        /// <summary>
        /// Load a PCX image from a storage.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns>The image.</returns>
        public static Surface LoadPcx(RWOps rwops) =>
                    new(Native.IMG_LoadPCX_RW(rwops.ToNative()));

        /// <summary>
        /// Load a PNG image from a storage.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns>The image.</returns>
        public static Surface LoadPng(RWOps rwops) =>
                    new(Native.IMG_LoadPNG_RW(rwops.ToNative()));

        /// <summary>
        /// Load a PNM image from a storage.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns>The image.</returns>
        public static Surface LoadPnm(RWOps rwops) =>
                    new(Native.IMG_LoadPNM_RW(rwops.ToNative()));

        /// <summary>
        /// Load a SVG image from a storage.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns>The image.</returns>
        public static Surface LoadSvg(RWOps rwops) =>
                    new(Native.IMG_LoadSVG_RW(rwops.ToNative()));

        /// <summary>
        /// Load a SVG image from a storage and scale it.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <param name="size">The scaled size.</param>
        /// <returns>The image.</returns>
        public static Surface LoadSvgSized(RWOps rwops, Size size) =>
                    new(Native.IMG_LoadSizedSVG_RW(rwops.ToNative(), size.Width, size.Height));

        /// <summary>
        /// Load a QOI image from a storage.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns>The image.</returns>
        public static Surface LoadQoi(RWOps rwops) =>
                    new(Native.IMG_LoadQOI_RW(rwops.ToNative()));

        /// <summary>
        /// Load a TGA image from a storage.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns>The image.</returns>
        public static Surface LoadTga(RWOps rwops) =>
                    new(Native.IMG_LoadTGA_RW(rwops.ToNative()));

        /// <summary>
        /// Load a TIF image from a storage.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns>The image.</returns>
        public static Surface LoadTif(RWOps rwops) =>
                    new(Native.IMG_LoadTIF_RW(rwops.ToNative()));

        /// <summary>
        /// Load a XCF image from a storage.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns>The image.</returns>
        public static Surface LoadXcf(RWOps rwops) =>
                    new(Native.IMG_LoadXCF_RW(rwops.ToNative()));

        /// <summary>
        /// Load a XPM image from a storage.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns>The image.</returns>
        public static Surface LoadXpm(RWOps rwops) =>
                    new(Native.IMG_LoadXPM_RW(rwops.ToNative()));

        /// <summary>
        /// Load a XV image from a storage.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns>The image.</returns>
        public static Surface LoadXv(RWOps rwops) =>
                    new(Native.IMG_LoadXV_RW(rwops.ToNative()));

        /// <summary>
        /// Load a WEBP image from a storage.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns>The image.</returns>
        public static Surface LoadWebp(RWOps rwops) =>
                    new(Native.IMG_LoadWEBP_RW(rwops.ToNative()));

        /// <summary>
        /// Saves a surface as a PNG image.
        /// </summary>
        /// <param name="surface">The surface.</param>
        /// <param name="filename">The filename.</param>
        public static void SavePng(Surface surface, string filename)
        {
            fixed (byte* ptr = Native.StringToUtf8(filename))
            {
                _ = Native.CheckError(Native.IMG_SavePNG(surface.ToNative(), ptr));
            }
        }

        /// <summary>
        /// Saves a surface as a PNG image.
        /// </summary>
        /// <param name="surface">The surface.</param>
        /// <param name="rwops">The storage.</param>
        /// <param name="shouldDispose">Whether the storage should be disposed when loading is finished.</param>
        public static void SavePng(Surface surface, RWOps rwops, bool shouldDispose) =>
            Native.CheckError(Native.IMG_SavePNG_RW(surface.ToNative(), rwops.ToNative(), Native.BoolToInt(shouldDispose)));

        /// <summary>
        /// Saves a surface as a JPG image.
        /// </summary>
        /// <param name="surface">The surface.</param>
        /// <param name="filename">The filename.</param>
        /// <param name="quality">The quality to save as.</param>
        public static void SaveJpg(Surface surface, string filename, int quality)
        {
            fixed (byte* ptr = Native.StringToUtf8(filename))
            {
                _ = Native.CheckError(Native.IMG_SaveJPG(surface.ToNative(), ptr, quality));
            }
        }

        /// <summary>
        /// Saves a surface as a JPG image.
        /// </summary>
        /// <param name="surface">The surface.</param>
        /// <param name="rwops">The storage.</param>
        /// <param name="shouldDispose">Whether the storage should be disposed when loading is finished.</param>
        /// <param name="quality">The quality to save as.</param>
        public static void SaveJpg(Surface surface, RWOps rwops, bool shouldDispose, int quality) =>
            Native.CheckError(Native.IMG_SaveJPG_RW(surface.ToNative(), rwops.ToNative(), Native.BoolToInt(shouldDispose), quality));

        /// <summary>
        /// Loads an animated image.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns>The animation.</returns>
        public static Animation LoadAnimation(string filename)
        {
            fixed (byte* ptr = Native.StringToUtf8(filename))
            {
                return new(Native.CheckPointer(Native.IMG_LoadAnimation(ptr)));
            }
        }

        /// <summary>
        /// Loads an animated image.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <param name="shouldDispose">Whether the storage should be disposed when loading is finished.</param>
        /// <returns>The animation.</returns>
        public static Animation LoadAnimation(RWOps rwops, bool shouldDispose) =>
            new(Native.CheckPointer(Native.IMG_LoadAnimation_RW(rwops.ToNative(), Native.BoolToInt(shouldDispose))));

        /// <summary>
        /// Loads an animated image.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <param name="shouldDispose">Whether the storage should be disposed when loading is finished.</param>
        /// <param name="type">The type of the image.</param>
        /// <returns>The animation.</returns>
        public static Animation LoadAnimation(RWOps rwops, bool shouldDispose, string type)
        {
            fixed (byte* ptr = Native.StringToUtf8(type))
            {
                return new(Native.IMG_LoadAnimationTyped_RW(rwops.ToNative(), Native.BoolToInt(shouldDispose), ptr));
            }
        }

        /// <summary>
        /// Loads an animated image.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <returns>The animation.</returns>
        public static Animation LoadGIFAnimation(RWOps rwops) =>
            new(Native.CheckPointer(Native.IMG_LoadGIFAnimation_RW(rwops.ToNative())));
    }
}
