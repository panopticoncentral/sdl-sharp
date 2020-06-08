# Tutorial #4: Working with Images

In the previous tutorial, the image we drew on the window was in the [Windows bitmap](https://en.wikipedia.org/wiki/BMP_file_format) (or Bmp) format. By default, SDL only supports the BMP format for images, but fortunately there is an additional library, `sdl_image`, that supports a number of other really common image formats (Jpeg, Png, Tiff, Webp). SDL# supports `sdl_image` by default, so with just a few changes, we can start using these image formats!

## Step 1: Get the new image

What we'd like to do is use the same image as before, but this time in its native Jpeg format:

<img src="Sunflowers.jpg" title="Sunflowers" width="197" height="256"/>

Save the image to the file `Sunflowers.jpg` and go back now to the program we created in the previous tutorial. Open the project file and change `Sunflowers.bmp` to `Sunflowers.jpg` to have the new image copied on build.

## Step 2: Load the new image

To use the additional image formats `sdl_image` supports, we have to specify that we're going to use them when we initialize SDL. So change the `new Application` line to:

```csharp
using var app = new Application(Subsystems.Video, ImageFormats.Jpg);
```

This specifies that we're going to working with Jpeg images. You can `||` in any other formats you want to use in your program as well.

Now change the `Surface.LoadBmp` line to:

```csharp
using var sunflowers = Image.Load("Sunflowers.jpg", window.Surface);
```

You'll notice two changes here. First, we're now using the `Image` class (which wraps the `sdl_image` library) to load the image. It figures out that we're loading a Jpeg by the file's extension, but you can also say explicitly what format the file is in if you want to/need to.

The other change is that we specify the surface of the window we're going to draw this image on. We do this as an optimization, as different `Surface` objects can have different pixel formats (for example, one surface may have a 24-bit pixel format, while another has a 32-bit pixel format). SDL will convert from one pixel format to another if needed when drawing, but if you know the surface you're going to draw on, it's easiest to just tell `Image.Load` to load the image into a surface that has the same pixel format as the destination surface.

You can now `dotnet run` to verify that everything works exactly like it did before!