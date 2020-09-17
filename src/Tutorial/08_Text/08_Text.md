# Tutorial #8: Text

In addition to drawing images on the windows, we often will need to display text as well. While it's possible to treat text as another kind of image (for example, creating a sprite sheet with individual letters drawn on it and then copying from that sprite sheet), there fortunately is an additional library, `sdl_ttf`, that supports rendering TrueType fonts (TTF). SDL# supports `sdl_ttf` by default, so with just a few changes, we can start rendering text!

## Step #1: Load a font

We initialize the font subsystem when we initialize SDL#, so change the `Application` constructor to:

```csharp
using var app = new Application(Subsystems.Video, fontSupport: true);
```

Now we need to load a font. Download the font from [here](SDS_8x8.ttf) and save into your project directory, calling it `SDS_8x8.ttf`. Then modify your project file to include it:

```xml
  <ItemGroup>
    <Content Include="SDS_8x8.ttf">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </Content>
  </ItemGroup>
```
Now, after the `Renderer.Create` call, create the font:

```csharp
using var font = Font.Create("SDS_8x8.ttf", 12);
```

The `12` in the call specifies the font size that we are going to render with this font.

(Note: The font in this tutorial comes from the [DawnLike - 16x16 Universal Rogue-like tileset v1.81](https://opengameart.org/content/dawnlike-16x16-universal-rogue-like-tileset-v181) license [CC-BY 4.0](https://creativecommons.org/licenses/by/4.0/), credit to DragonDePlatino and DawnBringer.)

## Step #2: Draw some text

To draw text on the screen, first we will render the text into a texture. So after the call to `Font.Create` we will:

```csharp
using var textTexture = font.RenderSolid("The quick brown fox jumped over the lazy dog", Colors.Black, renderer);
```

Then we can use the texture we created to copy it to the screen. After `renderer.Clear()` add:

```csharp
renderer.Copy(textTexture, null, (Point.Origin, textTexture.Size));
```

## Step #3: Input text

In addition to rendering text, SDL also supports several other 

## Step #4: Copy to/from the clipboard

