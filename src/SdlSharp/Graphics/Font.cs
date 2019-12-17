namespace SdlSharp.Graphics
{
    /// <summary>
    /// A font.
    /// </summary>
    public sealed unsafe class Font : NativePointerBase<Native.TTF_Font, Font>
    {
        /// <summary>
        /// The native byte order mark.
        /// </summary>
        public const int UnicodeBomNative = '\xFEFF';

        /// <summary>
        /// The swapped byte order mark.
        /// </summary>
        public const int UnicodeBomSwapped = '\xFFFE';

        /// <summary>
        /// The style of the font.
        /// </summary>
        public FontStyle Style
        {
            get => Native.TTF_GetFontStyle(Pointer);
            set => Native.TTF_SetFontStyle(Pointer, value);
        }

        /// <summary>
        /// Whether the font is outlined.
        /// </summary>
        public bool Outline
        {
            get => Native.TTF_GetFontOutline(Pointer);
            set => Native.TTF_SetFontOutline(Pointer, value);
        }

        /// <summary>
        /// The font hinting.
        /// </summary>
        public FontHinting Hinting
        {
            get => Native.TTF_GetFontHinting(Pointer);
            set => Native.TTF_SetFontHinting(Pointer, value);
        }

        /// <summary>
        /// The font height.
        /// </summary>
        public int Height => Native.TTF_FontHeight(Pointer);

        /// <summary>
        /// The font ascent.
        /// </summary>
        public int Ascent => Native.TTF_FontAscent(Pointer);

        /// <summary>
        /// The font descent.
        /// </summary>
        public int Descent => Native.TTF_FontDescent(Pointer);

        /// <summary>
        /// The font's line skip.
        /// </summary>
        public int LineSkip => Native.TTF_FontLineSkip(Pointer);

        /// <summary>
        /// Whether the font has kerning.
        /// </summary>
        public bool Kerning
        {
            get => Native.TTF_GetFontKerning(Pointer);
            set => Native.TTF_SetFontKerning(Pointer, value);
        }

        /// <summary>
        /// The number of faces in the font.
        /// </summary>
        public long Faces => Native.TTF_FontFaces(Pointer);

        /// <summary>
        /// Whether the face is fixed width;
        /// </summary>
        public bool FaceIsFixedWidth => Native.TTF_FontFaceIsFixedWidth(Pointer);

        /// <summary>
        /// The face's family name.
        /// </summary>
        public string FaceFamilyName => Native.TTF_FontFaceFamilyName(Pointer);

        /// <summary>
        /// The face's style name.
        /// </summary>
        public string FaceStyleName => Native.TTF_FontFaceStyleName(Pointer);

        /// <summary>
        /// Creates a font from a file.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="pointSize">The point size.</param>
        /// <returns>The font.</returns>
        public static Font Create(string filename, int pointSize) =>
            PointerToInstanceNotNull(Native.TTF_OpenFont(filename, pointSize));

        /// <summary>
        /// Creates a specific font face from a file.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="pointSize">The point size.</param>
        /// <param name="index">The font face index.</param>
        /// <returns>The font.</returns>
        public static Font Create(string filename, int pointSize, int index) =>
            PointerToInstanceNotNull(Native.TTF_OpenFontIndex(filename, pointSize, index));

        /// <summary>
        /// Creates a font from a storage.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <param name="shouldDispose">Whether the storage should be disposed after the load.</param>
        /// <param name="pointSize">The point size.</param>
        /// <returns>The font.</returns>
        public static Font Create(RWOps rwops, bool shouldDispose, int pointSize) =>
            PointerToInstanceNotNull(Native.TTF_OpenFontRW(rwops.Pointer, shouldDispose, pointSize));

        /// <summary>
        /// Creates a specific font face from a storage.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <param name="shouldDispose">Whether the storage should be disposed after the load.</param>
        /// <param name="pointSize">The point size.</param>
        /// <param name="index">The font face index.</param>
        /// <returns>The font.</returns>
        public static Font Create(RWOps rwops, bool shouldDispose, int pointSize, int index) =>
            PointerToInstanceNotNull(Native.TTF_OpenFontIndexRW(rwops.Pointer, shouldDispose, pointSize, index));

        /// <summary>
        /// Sets whether the bytes are swapped relative to the system's endedness.
        /// </summary>
        /// <param name="swapped">Whether the bytes are swapped.</param>
        public static void SetByteSwappedUnicode(bool swapped) =>
            Native.TTF_ByteSwappedUNICODE(swapped);

        /// <inheritdoc/>
        public override void Dispose()
        {
            Native.TTF_CloseFont(Pointer);
            base.Dispose();
        }

        /// <summary>
        /// Does the font provide a glyph for this character?
        /// </summary>
        /// <param name="ch">The character.</param>
        /// <returns>The glyph index, zero otherwise.</returns>
        public int GlyphIsProvided(char ch) =>
            Native.TTF_GlyphIsProvided(Pointer, ch);

        /// <summary>
        /// Gets metrics about a character's glyph.
        /// </summary>
        /// <param name="ch">The character.</param>
        /// <param name="minimum">The minimum offset.</param>
        /// <param name="maximum">The maximum offset.</param>
        /// <param name="advance">The advance amount.</param>
        public void GlyphMetrics(char ch, out Point minimum, out Point maximum, out int advance)
        {
            _ = Native.CheckError(Native.TTF_GlyphMetrics(Pointer, ch, out var minx, out var maxx, out var miny, out var maxy, out advance));
            minimum = (minx, miny);
            maximum = (maxx, maxy);
        }

        /// <summary>
        /// Gets the size of a string.
        /// </summary>
        /// <param name="text">The string.</param>
        /// <returns>The size.</returns>
        public Size SizeText(string text)
        {
            _ = Native.CheckError(Native.TTF_SizeUNICODE(Pointer, text, out var w, out var h));
            return (w, h);
        }

        /// <summary>
        /// Renders a string in solid text.
        /// </summary>
        /// <param name="s">The string.</param>
        /// <param name="foreground">The foreground color.</param>
        /// <returns>The rendered surface.</returns>
        public Surface RenderSolid(string s, Color foreground) =>
            Surface.PointerToInstanceNotNull(Native.TTF_RenderUNICODE_Solid(Pointer, s, foreground));

        /// <summary>
        /// Renders a string in solid text.
        /// </summary>
        /// <param name="s">The string.</param>
        /// <param name="foreground">The foreground color.</param>
        /// <param name="renderer">The renderer to use.</param>
        /// <returns>The rendered texture.</returns>
        public Texture RenderSolid(string s, Color foreground, Renderer renderer)
        {
            using var surface = Surface.PointerToInstanceNotNull(Native.TTF_RenderUNICODE_Solid(Pointer, s, foreground));
            return renderer.CreateTexture(surface);
        }

        /// <summary>
        /// Renders a character in solid text.
        /// </summary>
        /// <param name="ch">The character.</param>
        /// <param name="foreground">The foreground color.</param>
        /// <returns>The rendered surface.</returns>
        public Surface RenderSolid(char ch, Color foreground) =>
            Surface.PointerToInstanceNotNull(Native.TTF_RenderGlyph_Solid(Pointer, ch, foreground));

        /// <summary>
        /// Renders a character in solid text.
        /// </summary>
        /// <param name="ch">The character.</param>
        /// <param name="foreground">The foreground color.</param>
        /// <param name="renderer">The renderer to use.</param>
        /// <returns>The rendered texture.</returns>
        public Texture RenderSolid(char ch, Color foreground, Renderer renderer)
        {
            using var surface = Surface.PointerToInstanceNotNull(Native.TTF_RenderGlyph_Solid(Pointer, ch, foreground));
            return renderer.CreateTexture(surface);
        }

        /// <summary>
        /// Renders a string in shaded text.
        /// </summary>
        /// <param name="s">The string.</param>
        /// <param name="foreground">The foreground color.</param>
        /// <param name="background">The background color.</param>
        /// <returns>The rendered surface.</returns>
        public Surface RenderShaded(string s, Color foreground, Color background) =>
            Surface.PointerToInstanceNotNull(Native.TTF_RenderUNICODE_Shaded(Pointer, s, foreground, background));

        /// <summary>
        /// Renders a string in shaded text.
        /// </summary>
        /// <param name="s">The string.</param>
        /// <param name="foreground">The foreground color.</param>
        /// <param name="background">The background color.</param>
        /// <param name="renderer">The renderer to use.</param>
        /// <returns>The rendered texture.</returns>
        public Texture RenderShaded(string s, Color foreground, Color background, Renderer renderer)
        {
            using var surface = Surface.PointerToInstanceNotNull(Native.TTF_RenderUNICODE_Shaded(Pointer, s, foreground, background));
            return renderer.CreateTexture(surface);
        }

        /// <summary>
        /// Renders a character in shaded text.
        /// </summary>
        /// <param name="ch">The character.</param>
        /// <param name="foreground">The foreground color.</param>
        /// <param name="background">The background color.</param>
        /// <returns>The rendered surface.</returns>
        public Surface RenderShaded(char ch, Color foreground, Color background) =>
            Surface.PointerToInstanceNotNull(Native.TTF_RenderGlyph_Shaded(Pointer, ch, foreground, background));

        /// <summary>
        /// Renders a character in shaded text.
        /// </summary>
        /// <param name="ch">The character.</param>
        /// <param name="foreground">The foreground color.</param>
        /// <param name="background">The background color.</param>
        /// <param name="renderer">The renderer to use.</param>
        /// <returns>The rendered texture.</returns>
        public Texture RenderShaded(char ch, Color foreground, Color background, Renderer renderer)
        {
            using var surface = Surface.PointerToInstanceNotNull(Native.TTF_RenderGlyph_Shaded(Pointer, ch, foreground, background));
            return renderer.CreateTexture(surface);
        }

        /// <summary>
        /// Renders a string in blended text.
        /// </summary>
        /// <param name="s">The string.</param>
        /// <param name="foreground">The foreground color.</param>
        /// <returns>The rendered surface.</returns>
        public Surface RenderBlended(string s, Color foreground) =>
            Surface.PointerToInstanceNotNull(Native.TTF_RenderUNICODE_Blended(Pointer, s, foreground));

        /// <summary>
        /// Renders a string in blended text.
        /// </summary>
        /// <param name="s">The string.</param>
        /// <param name="foreground">The foreground color.</param>
        /// <param name="renderer">The renderer to use.</param>
        /// <returns>The rendered texture.</returns>
        public Texture RenderBlended(string s, Color foreground, Renderer renderer)
        {
            using var surface = Surface.PointerToInstanceNotNull(Native.TTF_RenderUNICODE_Blended(Pointer, s, foreground));
            return renderer.CreateTexture(surface);
        }

        /// <summary>
        /// Renders a string in blended text with wrap.
        /// </summary>
        /// <param name="s">The string.</param>
        /// <param name="foreground">The foreground color.</param>
        /// <param name="wrapLength">The wrap length.</param>
        /// <returns>The rendered surface.</returns>
        public Surface RenderBlended(string s, Color foreground, uint wrapLength) =>
            Surface.PointerToInstanceNotNull(Native.TTF_RenderUNICODE_Blended_Wrapped(Pointer, s, foreground, wrapLength));

        /// <summary>
        /// Renders a string in blended text with wrap.
        /// </summary>
        /// <param name="s">The string.</param>
        /// <param name="foreground">The foreground color.</param>
        /// <param name="wrapLength">The wrap length.</param>
        /// <param name="renderer">The renderer to use.</param>
        /// <returns>The rendered texture.</returns>
        public Texture RenderBlended(string s, Color foreground, uint wrapLength, Renderer renderer)
        {
            using var surface = Surface.PointerToInstanceNotNull(Native.TTF_RenderUNICODE_Blended_Wrapped(Pointer, s, foreground, wrapLength));
            return renderer.CreateTexture(surface);
        }

        /// <summary>
        /// Renders a character in blended text.
        /// </summary>
        /// <param name="ch">The character.</param>
        /// <param name="foreground">The foreground color.</param>
        /// <returns>The rendered surface.</returns>
        public Surface RenderBlended(char ch, Color foreground) =>
            Surface.PointerToInstanceNotNull(Native.TTF_RenderGlyph_Blended(Pointer, ch, foreground));

        /// <summary>
        /// Renders a character in blended text.
        /// </summary>
        /// <param name="ch">The character.</param>
        /// <param name="foreground">The foreground color.</param>
        /// <param name="renderer">The renderer to use.</param>
        /// <returns>The rendered texture.</returns>
        public Texture RenderBlended(char ch, Color foreground, Renderer renderer)
        {
            using var surface = Surface.PointerToInstanceNotNull(Native.TTF_RenderGlyph_Blended(Pointer, ch, foreground));
            return renderer.CreateTexture(surface);
        }

        /// <summary>
        /// Gets the kerning size for a character.
        /// </summary>
        /// <param name="previous">The previous character.</param>
        /// <param name="ch">The character.</param>
        /// <returns>The kerning size.</returns>
        public int GetFontKerningSizeGlyphs(char previous, char ch) =>
            Native.TTF_GetFontKerningSizeGlyphs(Pointer, previous, ch);
    }
}
