# Tutorial #5: Hardware Acceleration

Up until now, we have been issuing drawing commands directly on a window's surface. Although this _works_, it is not the most efficient method.

Remember that all of the drawing on a `Surface` object, is actually batched - that is, you issue all of the drawing commands that you want to do and then you call `Window.UpdateSurface` to execute all those commands at once on the window. This process of executing batched drawing commands is called _rendering_. When you call `UpdateSurface`, the SDL library takes the draw commands and renders them to the pixels of the window.

By default, the renderer that SDL uses to process drawing commands makes no particular assumptions about the hardware that it is running on. As such, it does not take advantage of the fact that virtually all modern computers have specialized graphics chips that are designed to make various drawing operations (such as blting an image) much, much faster than can be achieved through normal code.

So what we want to do is replace the use of SDL's _software renderer_ with a _hardware renderer_ that takes advantage of whatever graphics chip is present in the system. SDL enables this through the use of the `Renderer` class, which allows you to choose what kind of renderer you'd like to utilize when executing drawing commands.

# Step 1: Create a hardware renderer

Go back to the previous tutorial's code. After the call to `Window.Create`, add the following line:

```csharp
using var renderer = Renderer.Create(window, -1, RendererFlags.Accelerated);
```

This will create a renderer that is bound to the window and is hardware accelerated (that's what the `RendererFlags` is for). The `-1` parameter just means that we want the first renderer that is hardware accelerated - a machine may support multiple hardware renderers, depending on what graphics cards are installed, but we don't particularly care which one we get at this point.

Now change the next line of code to optimize the image load for the renderer instead of the window itself:

```csharp
using var sunflowers = Image.Load("Sunflowers.jpg", renderer);
```

One thing to note here is that the type of the `sunflowers` variable changes. When we were working with the software renderer, we could just copy one surface onto another. However, when working with hardware rendering, surfaces exist only at the very end of the rendering process, when the result of the rendering is presented on the screen. Before that, renderers use _textures_ to hold images that can be drawn during the rendering process. Textures are just like surfaces, but they are directly managed by the graphics hardware, and so are much more efficient than regular surfaces. (If you look at the type of the `sunflowers` variable, it has changed from `Surface` to `Texture`.)

# Step 2: Utilize the hardware renderer

Now replace all of the code in the `DispatchEvent` loop with the following code:

```csharp
renderer.Copy(sunflowers, null, (Point.Origin, stretch ? windowSize : sunflowers.Size));
renderer.Present();
```

The methods on a `Renderer` are slightly different than a `Surface`. Here, instead of calling a blit method, we're calling a `Copy` method which copies the `sunflowers` texture onto the final result of the renderer. Then we call `Present` to do the render and copy the result to the window's surface.

# Step 3: Clear the window

We'll take one further step here. As you've noticed, if you switch from stretched to non-stretched, you'll still see part of the stretched image. This is because we don't clear the renderer between each render. Let's do that now by adding the following code to the start of the loop:

```csharp
renderer.DrawColor = Colors.White;
renderer.Clear();
```

The first line changes the default drawing color for the renderer to white. Then the second line clears the entire renderer with the default color. Now when you do `dotnet run` and switch between stretched and non-stretched, you'll see only one image!