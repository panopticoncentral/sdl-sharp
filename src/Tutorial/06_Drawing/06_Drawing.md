# Tutorial #6: Drawing

All of the tutorials so far have been centered on drawing pre-made images on the screen, but it's important to note that it's not the only kind of drawing that you can do with SDL. This tutorial covers some of the other operations that you can do.

# Step #1: Drawing and filling rectangles

Let's go back to the previous tutorial and remove all the image drawing: delete the `sunflowers` local variable, the `KeyDown` handler, and the `Copy` call in the event loop. Now let's do two drawing operations instead. After the call to `Clear` add:

```csharp
renderer.DrawColor = Colors.Red;
renderer.FillRectangle(((windowSize.Width / 4, windowSize.Height / 4), (windowSize.Width / 2, windowSize.Height / 2)));
```

This will set the drawing color to red and then fill a rectange with red on the screen. You can also just draw the outline of a rectangle:

```csharp
renderer.DrawColor = Colors.Green;
renderer.DrawRectangle(((windowSize.Width / 6, windowSize.Height / 6), (windowSize.Width * 2 / 3, windowSize.Height * 2 / 3)));
```

# Step #2: Drawing lines

Instead of drawing a whole rectangle, you can also just draw a line:

```csharp
renderer.DrawColor = Colors.Blue;
renderer.DrawLine((0, windowSize.Height / 2), (windowSize.Width, windowSize.Height / 2));
```

# Step #3: Drawing points

Or you can just draw points:

```csharp
renderer.DrawColor = Colors.Yellow;
for (var i = 0; i < windowSize.Height; i += 4)
{
    renderer.DrawPoint((windowSize.Width / 2, i));
}
```

If you do a `dotnet run` after adding all of these drawing commands, you should see:

<img src="window.png" title="Hello, World!" width="320" height="240"/>