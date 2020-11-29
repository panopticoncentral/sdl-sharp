# Tutorial #2: Event Driven Programming

Now that we've created a window and got it painting, let's talk briefly about how the message loop that we created in the previous tutorial works. As I mentioned, the call to `DispatchEvents` causes the SDL application to process any events incoming from the operating system. But how do you, as the programmer, actually respond to those events? By creating _event handlers_ that will take care of responding to specific events.

## Step 1: Respond to key presses

Go back now to the program we created. First add the following line at the top of the file below the other `using` statements:

```csharp
using SdlSharp.Input;
```

This will enable us to use the classes that deal with input from the user without qualification. Then add these lines _before_ the `DispatchEvents` loop:

```csharp
Keyboard.KeyDown += (s, e) => Application.ShowMessageBox(MessageBoxFlags.Information, "Key Press", e.Keycode switch
{
    Keycode.Up => "Up!",
    Keycode.Down => "Down!",
    Keycode.Left => "Left!",
    Keycode.Right => "Right!",
    _ => "Other!"
}, window);
```

What this code does is hook up a handler for the `KeyDown` events that are fired when the user presses a key on the keyboard. The parameter `s` is the "sender" of the event, but in this case there is no "sender" so this value will always be `null`. The parameter `e` are the event arguments, which contain information about the event. In this case, the `KeyDown` event will provide a `KeyboardEventArgs` object that contains information about the key that was involved in the event.

So in this case, all we're doing is putting up a message box when the users presses the up, down, left or right key. You can do `dotnet run` and see this in action.

## Step #2: Respond to mouse input

In addition to keyboard input, you can also respond to mouse events as well. Add these lines below the ones you added in the previous step:

```csharp
Mouse.ButtonDown += (s, e) => Application.ShowMessageBox(MessageBoxFlags.Information, "Mouse Button", "Down!", window);
Mouse.ButtonUp += (s, e) => Application.ShowMessageBox(MessageBoxFlags.Information, "Mouse Button", "Up!", window);
```

Now when you click on the mouse button when you're over the window, you should get a message box that says "Down!" and then one that says "Up!".

## Step #3: Explore!

`Keyboard` and `Mouse` are just two of the many objects that will raise events. Here is a list of the major objects that have events:

Type | Events
--- | ---
`Application` | Events related to application-level actions such as quitting, drag and drop, etc.
`Clipboard` | Events related to the clipboard
`Display` and `Window` | Events related to the screen
`GameController` and `Joystick` | Events related to game controllers
`Keyboard` | Events related to the keyboard
`Mouse` | Events related to the mouse
`Sensor` | Events related to system sensors (if any)
`AudioDevice` | Events related to sound
`Gesture` and `TouchDevice` | Events related to touch input