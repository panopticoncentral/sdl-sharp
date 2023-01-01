namespace SdlSharp.Graphics
{
    /// <summary>
    /// A font.
    /// </summary>
    public sealed unsafe class Font : IDisposable
    {
        private readonly Native.TTF_Font* _font;

        /// <summary>
        /// The native byte order mark.
        /// </summary>
        public const int UnicodeBomNative = Native.UNICODE_BOM_NATIVE;

        /// <summary>
        /// The swapped byte order mark.
        /// </summary>
        public const int UnicodeBomSwapped = Native.UNICODE_BOM_SWAPPED;

        /// <summary>
        /// The style of the font.
        /// </summary>
        public FontStyle Style
        {
            get => (FontStyle)Native.TTF_GetFontStyle(_font);
            set => Native.TTF_SetFontStyle(_font, (uint)value);
        }

        /// <summary>
        /// Whether the font is outlined.
        /// </summary>
        public bool Outline
        {
            get => Native.TTF_GetFontOutline(_font) != 0;
            set => Native.TTF_SetFontOutline(_font, Native.BoolToInt(value));
        }

        /// <summary>
        /// The font hinting.
        /// </summary>
        public FontHinting Hinting
        {
            get => (FontHinting)Native.TTF_GetFontHinting(_font);
            set => Native.TTF_SetFontHinting(_font, (int)value);
        }

        /// <summary>
        /// The alignment of wrapped text.
        /// </summary>
        public FontWrappingAlignment WrappingAlignment
        {
            get => (FontWrappingAlignment)Native.TTF_GetFontWrappedAlign(_font);
            set => Native.TTF_SetFontWrappedAlign(_font, (int)value);
        }

        /// <summary>
        /// The font height.
        /// </summary>
        public int Height => Native.TTF_FontHeight(_font);

        /// <summary>
        /// The font ascent.
        /// </summary>
        public int Ascent => Native.TTF_FontAscent(_font);

        /// <summary>
        /// The font descent.
        /// </summary>
        public int Descent => Native.TTF_FontDescent(_font);

        /// <summary>
        /// The font's line skip.
        /// </summary>
        public int LineSkip => Native.TTF_FontLineSkip(_font);

        /// <summary>
        /// Whether the font has kerning.
        /// </summary>
        public bool Kerning
        {
            get => Native.TTF_GetFontKerning(_font) != 0;
            set => Native.TTF_SetFontKerning(_font, Native.BoolToInt(value));
        }

        /// <summary>
        /// The number of faces in the font.
        /// </summary>
        public long Faces => Native.TTF_FontFaces(_font);

        /// <summary>
        /// Whether the face is fixed width;
        /// </summary>
        public bool FaceIsFixedWidth => Native.TTF_FontFaceIsFixedWidth(_font) != 0;

        /// <summary>
        /// The face's family name.
        /// </summary>
        public string FaceFamilyName => Native.Utf8ToString(Native.TTF_FontFaceFamilyName(_font))!;

        /// <summary>
        /// The face's style name.
        /// </summary>
        public string FaceStyleName => Native.Utf8ToString(Native.TTF_FontFaceStyleName(_font))!;

        /// <summary>
        /// Determines whether Signed Distance Field (SDF) rendering will be used.
        /// </summary>
        public bool SignedDistanceFieldRendering
        {
            get => Native.TTF_GetFontSDF(_font);
            set => _ = Native.CheckError(Native.TTF_SetFontSDF(_font, value));
        }

        internal Font(Native.TTF_Font* font)
        {
            _font = font;
        }

        /// <summary>
        /// Creates a font from a file.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="pointSize">The point size.</param>
        /// <returns>The font.</returns>
        public static Font Create(string filename, int pointSize)
        {
            fixed (byte* ptr = Native.StringToUtf8(filename))
            {
                return new(Native.TTF_OpenFont(ptr, pointSize));
            }
        }

        /// <summary>
        /// Creates a font from a file.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="pointSize">The point size.</param>
        /// <param name="hdpi">The horizontal DPI.</param>
        /// <param name="vdpi">The vertical DPI.</param>
        /// <returns>The font.</returns>
        public static Font CreateDpi(string filename, int pointSize, uint hdpi, uint vdpi)
        {
            fixed (byte* ptr = Native.StringToUtf8(filename))
            {
                return new(Native.TTF_OpenFontDPI(ptr, pointSize, hdpi, vdpi));
            }
        }

        /// <summary>
        /// Creates a specific font face from a file.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="pointSize">The point size.</param>
        /// <param name="index">The font face index.</param>
        /// <returns>The font.</returns>
        public static Font Create(string filename, int pointSize, int index)
        {
            fixed (byte* ptr = Native.StringToUtf8(filename))
            {
                return new(Native.TTF_OpenFontIndex(ptr, pointSize, index));
            }
        }

        /// <summary>
        /// Creates a specific font face from a file.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="pointSize">The point size.</param>
        /// <param name="index">The font face index.</param>
        /// <param name="hdpi">The horizontal DPI.</param>
        /// <param name="vdpi">The vertical DPI.</param>
        /// <returns>The font.</returns>
        public static Font CreateDpi(string filename, int pointSize, int index, uint hdpi, uint vdpi)
        {
            fixed (byte* ptr = Native.StringToUtf8(filename))
            {
                return new(Native.TTF_OpenFontIndexDPI(ptr, pointSize, index, hdpi, vdpi));
            }
        }

        /// <summary>
        /// Creates a font from a storage.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <param name="shouldDispose">Whether the storage should be disposed after the load.</param>
        /// <param name="pointSize">The point size.</param>
        /// <returns>The font.</returns>
        public static Font Create(RWOps rwops, bool shouldDispose, int pointSize) =>
            new(Native.TTF_OpenFontRW(rwops.ToNative(), Native.BoolToInt(shouldDispose), pointSize));

        /// <summary>
        /// Creates a font from a storage.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <param name="shouldDispose">Whether the storage should be disposed after the load.</param>
        /// <param name="pointSize">The point size.</param>
        /// <param name="hdpi">The horizontal DPI.</param>
        /// <param name="vdpi">The vertical DPI.</param>
        /// <returns>The font.</returns>
        public static Font CreateDpi(RWOps rwops, bool shouldDispose, int pointSize, uint hdpi, uint vdpi) =>
            new(Native.TTF_OpenFontDPIRW(rwops.ToNative(), Native.BoolToInt(shouldDispose), pointSize, hdpi, vdpi));

        /// <summary>
        /// Creates a specific font face from a storage.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <param name="shouldDispose">Whether the storage should be disposed after the load.</param>
        /// <param name="pointSize">The point size.</param>
        /// <param name="index">The font face index.</param>
        /// <returns>The font.</returns>
        public static Font Create(RWOps rwops, bool shouldDispose, int pointSize, int index) =>
            new(Native.TTF_OpenFontIndexRW(rwops.ToNative(), Native.BoolToInt(shouldDispose), pointSize, index));

        /// <summary>
        /// Creates a specific font face from a storage.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <param name="shouldDispose">Whether the storage should be disposed after the load.</param>
        /// <param name="pointSize">The point size.</param>
        /// <param name="index">The font face index.</param>
        /// <param name="hdpi">The horizontal DPI.</param>
        /// <param name="vdpi">The vertical DPI.</param>
        /// <returns>The font.</returns>
        public static Font CreateDpi(RWOps rwops, bool shouldDispose, int pointSize, int index, uint hdpi, uint vdpi) =>
            new(Native.TTF_OpenFontIndexDPIRW(rwops.ToNative(), Native.BoolToInt(shouldDispose), pointSize, index, hdpi, vdpi));

        /// <summary>
        /// Sets whether the bytes are swapped relative to the system's endedness.
        /// </summary>
        /// <param name="swapped">Whether the bytes are swapped.</param>
        public static void SetByteSwappedUnicode(bool swapped) =>
            Native.TTF_ByteSwappedUNICODE(swapped);

        /// <inheritdoc/>
        public void Dispose() => Native.TTF_CloseFont(_font);

        /// <summary>
        /// Sets the font size.
        /// </summary>
        /// <param name="pointSize">The point size.</param>
        public void SetFontSize(int pointSize) =>
            Native.CheckError(Native.TTF_SetFontSize(_font, pointSize));

        /// <summary>
        /// Sets the font size.
        /// </summary>
        /// <param name="pointSize">The point size.</param>
        /// <param name="hdpi">The horizontal DPI.</param>
        /// <param name="vdpi">The vertical DPI.</param>
        public void SetFontSizeDpi(int pointSize, uint hdpi, uint vdpi) =>
            Native.CheckError(Native.TTF_SetFontSizeDPI(_font, pointSize, hdpi, vdpi));

        /// <summary>
        /// Sets the font direction.
        /// </summary>
        /// <param name="direction">The direction.</param>
        public void SetFontDirection(FontDirection direction) =>
            _ = Native.CheckError(Native.TTF_SetFontDirection(_font, (Native.TTF_Direction)direction));

        /// <summary>
        /// Sets the script to be used by text shaping.
        /// </summary>
        /// <param name="script">The script.</param>
        public void SetFontScriptName(string script)
        {
            fixed (byte* ptr = Native.StringToUtf8(script))
            {
                _ = Native.CheckError(Native.TTF_SetFontScriptName(_font, ptr));
            }
        }

        /// <summary>
        /// Does the font provide a glyph for this UTF-16 character?
        /// </summary>
        /// <param name="ch">The character.</param>
        /// <returns>The glyph index, zero otherwise.</returns>
        public int GlyphIsProvided(char ch) =>
            Native.TTF_GlyphIsProvided(_font, ch);

        /// <summary>
        /// Does the font provide a glyph for this UTF-32 character?
        /// </summary>
        /// <param name="ch">The character.</param>
        /// <returns>The glyph index, zero otherwise.</returns>
        public int GlyphIsProvided(ushort ch) =>
            Native.TTF_GlyphIsProvided32(_font, ch);

        /// <summary>
        /// Gets metrics about a UTF-16 character's glyph.
        /// </summary>
        /// <param name="ch">The character.</param>
        /// <param name="minimum">The minimum offset.</param>
        /// <param name="maximum">The maximum offset.</param>
        /// <param name="advance">The advance amount.</param>
        public void GlyphMetrics(char ch, out Point minimum, out Point maximum, out int advance)
        {
            int minx, maxx, miny, maxy, advanceLocal;
            _ = Native.CheckError(Native.TTF_GlyphMetrics(_font, ch, &minx, &maxx, &miny, &maxy, &advanceLocal));
            advance = advanceLocal;
            minimum = (minx, miny);
            maximum = (maxx, maxy);
        }

        /// <summary>
        /// Gets metrics about a UTF-32 character's glyph.
        /// </summary>
        /// <param name="ch">The character.</param>
        /// <param name="minimum">The minimum offset.</param>
        /// <param name="maximum">The maximum offset.</param>
        /// <param name="advance">The advance amount.</param>
        public void GlyphMetrics(uint ch, out Point minimum, out Point maximum, out int advance)
        {
            int minx, maxx, miny, maxy, advanceLocal;
            _ = Native.CheckError(Native.TTF_GlyphMetrics32(_font, ch, &minx, &maxx, &miny, &maxy, &advanceLocal));
            advance = advanceLocal;
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
            fixed (char* ptr = text)
            {
                int w, h;
                _ = Native.CheckError(Native.TTF_SizeUNICODE(_font, (ushort*)ptr, &w, &h));
                return (w, h);
            }
        }

        /// <summary>
        /// Determines the amount of a string that will fit into a space.
        /// </summary>
        /// <param name="text">The string.</param>
        /// <param name="measureWidth">The width of the area to measure.</param>
        /// <returns>The size.</returns>
        public (int Extent, int Count) MeasureText(string text, int measureWidth)
        {
            fixed (char* ptr = text)
            {
                int extent, count;
                _ = Native.CheckError(Native.TTF_MeasureUNICODE(_font, (ushort*)ptr, measureWidth, &extent, &count));
                return (extent, count);
            }
        }

        /// <summary>
        /// Renders a string in solid text.
        /// </summary>
        /// <param name="s">The string.</param>
        /// <param name="foreground">The foreground color.</param>
        /// <returns>The rendered surface.</returns>
        public Surface RenderSolid(string s, Color foreground)
        {
            fixed (char* ptr = s)
            {
                return new(Native.TTF_RenderUNICODE_Solid(_font, (ushort*)ptr, foreground.ToNative()));
            }
        }

        /// <summary>
        /// Renders a string in solid text wrapped.
        /// </summary>
        /// <param name="s">The string.</param>
        /// <param name="foreground">The foreground color.</param>
        /// <param name="wrapLength">The length to wrap at.</param>
        /// <returns>The rendered surface.</returns>
        public Surface RenderSolid(string s, Color foreground, uint wrapLength)
        {
            fixed (char* ptr = s)
            {
                return new(Native.TTF_RenderUNICODE_Solid_Wrapped(_font, (ushort*)ptr, foreground.ToNative(), wrapLength));
            }
        }

        /// <summary>
        /// Renders a string in solid text.
        /// </summary>
        /// <param name="s">The string.</param>
        /// <param name="foreground">The foreground color.</param>
        /// <param name="renderer">The renderer to use.</param>
        /// <returns>The rendered texture.</returns>
        public Texture RenderSolid(string s, Color foreground, Renderer renderer)
        {
            using var surface = RenderSolid(s, foreground);
            return renderer.CreateTexture(surface);
        }

        /// <summary>
        /// Renders a string in solid text, wrapped.
        /// </summary>
        /// <param name="s">The string.</param>
        /// <param name="foreground">The foreground color.</param>
        /// <param name="renderer">The renderer to use.</param>
        /// <param name="wrapLength">The length to wrap at.</param>
        /// <returns>The rendered texture.</returns>
        public Texture RenderSolid(string s, Color foreground, Renderer renderer, uint wrapLength)
        {
            using var surface = RenderSolid(s, foreground, wrapLength);
            return renderer.CreateTexture(surface);
        }

        /// <summary>
        /// Renders a UTF-16 character in solid text.
        /// </summary>
        /// <param name="ch">The character.</param>
        /// <param name="foreground">The foreground color.</param>
        /// <returns>The rendered surface.</returns>
        public Surface RenderSolid(char ch, Color foreground) =>
            new(Native.TTF_RenderGlyph_Solid(_font, ch, foreground.ToNative()));

        /// <summary>
        /// Renders a UTF-16 character in solid text.
        /// </summary>
        /// <param name="ch">The character.</param>
        /// <param name="foreground">The foreground color.</param>
        /// <param name="renderer">The renderer to use.</param>
        /// <returns>The rendered texture.</returns>
        public Texture RenderSolid(char ch, Color foreground, Renderer renderer)
        {
            using var surface = RenderSolid(ch, foreground);
            return renderer.CreateTexture(surface);
        }

        /// <summary>
        /// Renders a UTF-32 character in solid text.
        /// </summary>
        /// <param name="ch">The character.</param>
        /// <param name="foreground">The foreground color.</param>
        /// <returns>The rendered surface.</returns>
        public Surface RenderSolid(uint ch, Color foreground) =>
            new(Native.TTF_RenderGlyph32_Solid(_font, ch, foreground.ToNative()));

        /// <summary>
        /// Renders a UTF-32 character in solid text.
        /// </summary>
        /// <param name="ch">The character.</param>
        /// <param name="foreground">The foreground color.</param>
        /// <param name="renderer">The renderer to use.</param>
        /// <returns>The rendered texture.</returns>
        public Texture RenderSolid(uint ch, Color foreground, Renderer renderer)
        {
            using var surface = RenderSolid(ch, foreground);
            return renderer.CreateTexture(surface);
        }

        /// <summary>
        /// Renders a string in shaded text.
        /// </summary>
        /// <param name="s">The string.</param>
        /// <param name="foreground">The foreground color.</param>
        /// <param name="background">The background color.</param>
        /// <returns>The rendered surface.</returns>
        public Surface RenderShaded(string s, Color foreground, Color background)
        {
            fixed (char* ptr = s)
            {
                return new(Native.TTF_RenderUNICODE_Shaded(_font, (ushort*)ptr, foreground.ToNative(), background.ToNative()));
            }
        }

        /// <summary>
        /// Renders a string in shaded text.
        /// </summary>
        /// <param name="s">The string.</param>
        /// <param name="foreground">The foreground color.</param>
        /// <param name="background">The background color.</param>
        /// <param name="wrapLength">The length to wrap at.</param>
        /// <returns>The rendered surface.</returns>
        public Surface RenderShaded(string s, Color foreground, Color background, uint wrapLength)
        {
            fixed (char* ptr = s)
            {
                return new(Native.TTF_RenderUNICODE_Shaded_Wrapped(_font, (ushort*)ptr, foreground.ToNative(), background.ToNative(), wrapLength));
            }
        }

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
            using var surface = RenderShaded(s, foreground, background);
            return renderer.CreateTexture(surface);
        }

        /// <summary>
        /// Renders a string in shaded text.
        /// </summary>
        /// <param name="s">The string.</param>
        /// <param name="foreground">The foreground color.</param>
        /// <param name="background">The background color.</param>
        /// <param name="renderer">The renderer to use.</param>
        /// <param name="wrapLength">The length to wrap at.</param>
        /// <returns>The rendered texture.</returns>
        public Texture RenderShaded(string s, Color foreground, Color background, Renderer renderer, uint wrapLength)
        {
            using var surface = RenderShaded(s, foreground, background, wrapLength);
            return renderer.CreateTexture(surface);
        }

        /// <summary>
        /// Renders a UTF-16 character in shaded text.
        /// </summary>
        /// <param name="ch">The character.</param>
        /// <param name="foreground">The foreground color.</param>
        /// <param name="background">The background color.</param>
        /// <returns>The rendered surface.</returns>
        public Surface RenderShaded(char ch, Color foreground, Color background) =>
            new(Native.TTF_RenderGlyph_Shaded(_font, ch, foreground.ToNative(), background.ToNative()));

        /// <summary>
        /// Renders a UTF-16 character in shaded text.
        /// </summary>
        /// <param name="ch">The character.</param>
        /// <param name="foreground">The foreground color.</param>
        /// <param name="background">The background color.</param>
        /// <param name="renderer">The renderer to use.</param>
        /// <returns>The rendered texture.</returns>
        public Texture RenderShaded(char ch, Color foreground, Color background, Renderer renderer)
        {
            using var surface = RenderShaded(ch, foreground, background);
            return renderer.CreateTexture(surface);
        }

        /// <summary>
        /// Renders a UTF-32 character in shaded text.
        /// </summary>
        /// <param name="ch">The character.</param>
        /// <param name="foreground">The foreground color.</param>
        /// <param name="background">The background color.</param>
        /// <returns>The rendered surface.</returns>
        public Surface RenderShaded(uint ch, Color foreground, Color background) =>
            new(Native.TTF_RenderGlyph32_Shaded(_font, ch, foreground.ToNative(), background.ToNative()));

        /// <summary>
        /// Renders a UTF-32 character in shaded text.
        /// </summary>
        /// <param name="ch">The character.</param>
        /// <param name="foreground">The foreground color.</param>
        /// <param name="background">The background color.</param>
        /// <param name="renderer">The renderer to use.</param>
        /// <returns>The rendered texture.</returns>
        public Texture RenderShaded(uint ch, Color foreground, Color background, Renderer renderer)
        {
            using var surface = RenderShaded(ch, foreground, background);
            return renderer.CreateTexture(surface);
        }

        /// <summary>
        /// Renders a string in blended text.
        /// </summary>
        /// <param name="s">The string.</param>
        /// <param name="foreground">The foreground color.</param>
        /// <returns>The rendered surface.</returns>
        public Surface RenderBlended(string s, Color foreground)
        {
            fixed (char* ptr = s)
            {
                return new(Native.TTF_RenderUNICODE_Blended(_font, (ushort*)ptr, foreground.ToNative()));
            }
        }

        /// <summary>
        /// Renders a string in blended text wrapped.
        /// </summary>
        /// <param name="s">The string.</param>
        /// <param name="foreground">The foreground color.</param>
        /// <param name="wrapLength">The length to wrap at.</param>
        /// <returns>The rendered surface.</returns>
        public Surface RenderBlended(string s, Color foreground, uint wrapLength)
        {
            fixed (char* ptr = s)
            {
                return new(Native.TTF_RenderUNICODE_Blended_Wrapped(_font, (ushort*)ptr, foreground.ToNative(), wrapLength));
            }
        }

        /// <summary>
        /// Renders a string in blended text.
        /// </summary>
        /// <param name="s">The string.</param>
        /// <param name="foreground">The foreground color.</param>
        /// <param name="renderer">The renderer to use.</param>
        /// <returns>The rendered texture.</returns>
        public Texture RenderBlended(string s, Color foreground, Renderer renderer)
        {
            using var surface = RenderBlended(s, foreground);
            return renderer.CreateTexture(surface);
        }

        /// <summary>
        /// Renders a string in blended text, wrapped.
        /// </summary>
        /// <param name="s">The string.</param>
        /// <param name="foreground">The foreground color.</param>
        /// <param name="renderer">The renderer to use.</param>
        /// <param name="wrapLength">The length to wrap at.</param>
        /// <returns>The rendered texture.</returns>
        public Texture RenderBlended(string s, Color foreground, Renderer renderer, uint wrapLength)
        {
            using var surface = RenderBlended(s, foreground, wrapLength);
            return renderer.CreateTexture(surface);
        }

        /// <summary>
        /// Renders a UTF-16 character in blended text.
        /// </summary>
        /// <param name="ch">The character.</param>
        /// <param name="foreground">The foreground color.</param>
        /// <returns>The rendered surface.</returns>
        public Surface RenderBlended(char ch, Color foreground) =>
            new(Native.TTF_RenderGlyph_Blended(_font, ch, foreground.ToNative()));

        /// <summary>
        /// Renders a UTF-16 character in blended text.
        /// </summary>
        /// <param name="ch">The character.</param>
        /// <param name="foreground">The foreground color.</param>
        /// <param name="renderer">The renderer to use.</param>
        /// <returns>The rendered texture.</returns>
        public Texture RenderBlended(char ch, Color foreground, Renderer renderer)
        {
            using var surface = RenderBlended(ch, foreground);
            return renderer.CreateTexture(surface);
        }

        /// <summary>
        /// Renders a UTF-32 character in blended text.
        /// </summary>
        /// <param name="ch">The character.</param>
        /// <param name="foreground">The foreground color.</param>
        /// <returns>The rendered surface.</returns>
        public Surface RenderBlended(uint ch, Color foreground) =>
            new(Native.TTF_RenderGlyph32_Blended(_font, ch, foreground.ToNative()));

        /// <summary>
        /// Renders a UTF-32 character in blended text.
        /// </summary>
        /// <param name="ch">The character.</param>
        /// <param name="foreground">The foreground color.</param>
        /// <param name="renderer">The renderer to use.</param>
        /// <returns>The rendered texture.</returns>
        public Texture RenderBlended(uint ch, Color foreground, Renderer renderer)
        {
            using var surface = RenderBlended(ch, foreground);
            return renderer.CreateTexture(surface);
        }

        /// <summary>
        /// Renders a string in LCD shaded text.
        /// </summary>
        /// <param name="s">The string.</param>
        /// <param name="foreground">The foreground color.</param>
        /// <param name="background">The background color.</param>
        /// <returns>The rendered surface.</returns>
        public Surface RenderLcd(string s, Color foreground, Color background)
        {
            fixed (char* ptr = s)
            {
                return new(Native.TTF_RenderUNICODE_LCD(_font, (ushort*)ptr, foreground.ToNative(), background.ToNative()));
            }
        }

        /// <summary>
        /// Renders a string in LCD shaded text.
        /// </summary>
        /// <param name="s">The string.</param>
        /// <param name="foreground">The foreground color.</param>
        /// <param name="background">The background color.</param>
        /// <param name="wrapLength">The length to wrap at.</param>
        /// <returns>The rendered surface.</returns>
        public Surface RenderLcd(string s, Color foreground, Color background, uint wrapLength)
        {
            fixed (char* ptr = s)
            {
                return new(Native.TTF_RenderUNICODE_LCD_Wrapped(_font, (ushort*)ptr, foreground.ToNative(), background.ToNative(), wrapLength));
            }
        }

        /// <summary>
        /// Renders a string in LCD shaded text.
        /// </summary>
        /// <param name="s">The string.</param>
        /// <param name="foreground">The foreground color.</param>
        /// <param name="background">The background color.</param>
        /// <param name="renderer">The renderer to use.</param>
        /// <returns>The rendered texture.</returns>
        public Texture RenderLcd(string s, Color foreground, Color background, Renderer renderer)
        {
            using var surface = RenderLcd(s, foreground, background);
            return renderer.CreateTexture(surface);
        }

        /// <summary>
        /// Renders a string in LCD shaded text.
        /// </summary>
        /// <param name="s">The string.</param>
        /// <param name="foreground">The foreground color.</param>
        /// <param name="background">The background color.</param>
        /// <param name="renderer">The renderer to use.</param>
        /// <param name="wrapLength">The length to wrap at.</param>
        /// <returns>The rendered texture.</returns>
        public Texture RenderLcd(string s, Color foreground, Color background, Renderer renderer, uint wrapLength)
        {
            using var surface = RenderLcd(s, foreground, background, wrapLength);
            return renderer.CreateTexture(surface);
        }

        /// <summary>
        /// Renders a UTF-16 character in LCD shaded text.
        /// </summary>
        /// <param name="ch">The character.</param>
        /// <param name="foreground">The foreground color.</param>
        /// <param name="background">The background color.</param>
        /// <returns>The rendered surface.</returns>
        public Surface RenderLcd(char ch, Color foreground, Color background) =>
            new(Native.TTF_RenderGlyph_LCD(_font, ch, foreground.ToNative(), background.ToNative()));

        /// <summary>
        /// Renders a UTF-16 character in LCD shaded text.
        /// </summary>
        /// <param name="ch">The character.</param>
        /// <param name="foreground">The foreground color.</param>
        /// <param name="background">The background color.</param>
        /// <param name="renderer">The renderer to use.</param>
        /// <returns>The rendered texture.</returns>
        public Texture RenderLcd(char ch, Color foreground, Color background, Renderer renderer)
        {
            using var surface = RenderLcd(ch, foreground, background);
            return renderer.CreateTexture(surface);
        }

        /// <summary>
        /// Renders a UTF-32 character in LCD shaded text.
        /// </summary>
        /// <param name="ch">The character.</param>
        /// <param name="foreground">The foreground color.</param>
        /// <param name="background">The background color.</param>
        /// <returns>The rendered surface.</returns>
        public Surface RenderLcd(uint ch, Color foreground, Color background) =>
            new(Native.TTF_RenderGlyph32_LCD(_font, ch, foreground.ToNative(), background.ToNative()));

        /// <summary>
        /// Renders a UTF-32 character in LCD shaded text.
        /// </summary>
        /// <param name="ch">The character.</param>
        /// <param name="foreground">The foreground color.</param>
        /// <param name="background">The background color.</param>
        /// <param name="renderer">The renderer to use.</param>
        /// <returns>The rendered texture.</returns>
        public Texture RenderLcd(uint ch, Color foreground, Color background, Renderer renderer)
        {
            using var surface = RenderLcd(ch, foreground, background);
            return renderer.CreateTexture(surface);
        }

        /// <summary>
        /// Gets the kerning size for a character.
        /// </summary>
        /// <param name="previous">The previous character.</param>
        /// <param name="ch">The character.</param>
        /// <returns>The kerning size.</returns>
        public int GetFontKerningSizeGlyphs(char previous, char ch) =>
            Native.TTF_GetFontKerningSizeGlyphs(_font, previous, ch);

        /// <summary>
        /// Gets the kerning size for a character.
        /// </summary>
        /// <param name="previous">The previous character.</param>
        /// <param name="ch">The character.</param>
        /// <returns>The kerning size.</returns>
        public int GetFontKerningSizeGlyphs(uint previous, char ch) =>
            Native.TTF_GetFontKerningSizeGlyphs32(_font, previous, ch);
    }
}
