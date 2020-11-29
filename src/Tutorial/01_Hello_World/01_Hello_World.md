# Tutorial #1: Hello, World!

The first thing we'll do is go through the process of getting our development environment set up, and then putting up a simple blank SDL window.

**NOTE:** SDL# is only available for Windows at the moment, even though Linux and Mac SDL libraries exist and can be added at a later time.

## Step #1: Install your build tools.

Although you could use the full-blown [Visual Studio](https://visualstudio.microsoft.com/) IDE to develop using SDL#, for the purposes of this tutorial, we'll stick with the simplest setup possible. So the first step is going to be to go to the [.NET](https://dotnet.microsoft.com/) site and download the latest .NET Core installer. Make sure you choose to download the full SDK rather than just the runtime, since the SDK is the thing that contains the actual compilers. Then run the installer and get everything set up.

## Step #2: Install an editor

For the purpose of this tutorial, we'll use [Visual Studio Code](https://code.visualstudio.com/), although any editor will work for you. Install the latest stable build, and then run the editor. Click on the bottom icon along the left side of the window, which will open the Extensions panel. Seach for the "C#" extension and install it. This will allow you to get things like Intellisense for the tutorial projects.

## Step #3: Create a project

Create a directory somewhere on your machine named `Tutorial` and open a command prompt in that directory. Type `dotnet new console`, which will create a new console application in that directory. You'll see `Tutorial.csproj`, which is the file that describes what the project is and how .NET should build it, and `Program.cs` which is where your code is going to go.

## Step #4: Hello, world!

Now open Visual Studio Code in that directory by typing `code .` and then click on the `Program.cs` file. You should see:

```csharp
using System;

namespace Tutorial
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
```

A prompt will open in the lower right hand corner asking if you want to add assets required to build and debug. Click on `Yes` if you would like to run and debug your code from within Visual Studio Code directly. But for the moment, let's run this program from the command-line. So go back to your command window and type `dotnet run`. This should produce the output:

```Hello, World!```

## Step #4: Update your project

Now go back to Visual Studio Code and click on `Tutorial.csproj`. You should see:

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net5.0</TargetFramework>
  </PropertyGroup>

</Project>
```

You can learn more about `.csproj` files by reading about [MSBuild](https://docs.microsoft.com/en-us/visualstudio/msbuild/msbuild), but we need to make three changes here.

First, the `OutputType` property needs to be changed from `Exe` to `WinExe`. An `Exe` project produces a console application that only runs within a command window (like we did in the previous step). Since a SDL project is going to create its own window, we need to change it to `WinExe`, which is a full Windows application that has its own window.

Second, you need to add a `PlatformTarget` property that indicates whether we are building against the `x86` or `x64` versions of SDL2. Assuming you're on `x64`, you would add below `TargetFramework`:

```xml
<PlatformTarget>x64</PlatformTarget>
```

## Step #5: Add your reference

Finally, go back to the command-line and run the command `dotnet add package SdlSharp --version 0.9.3-alpha`. This will add a reference to the SDL# package so you can use it in your code.

(**NOTE:** The `--version` argument will not be required once SDL# is more stable.)

## Step #6: Bring up a window

Now go back to the `Program.cs` file in Visual Studio Code. Delete everything in the file and add the following lines:

```csharp
using SdlSharp;
using SdlSharp.Graphics;
```

This will allow us to refer to the SDL# types without having to qualify them with the SDL# namespaces. Now add the following lines:

```csharp
using var app = new Application(Subsystems.Video);
```

This creates a new SDL# `Application` object which represents the whole application. As we'll see later, this object will be used to respond to system events and give the window a chance to paint itself. The `Subsystems.Video` indicates that we will be using the display subsystem of SDL. In later tutorials, we will use other subsystems of SDL but for now this is all we need. Now add:

```csharp
Size windowSize = (640, 480);
Rectangle windowRectangle = (Window.UndefinedWindowLocation, windowSize);
```

This line defines the size of the window that we're going to create. A `Size` object holds a width/height pair, while a `Rectangle` holds both a location on the screen plus a size. `Window.UndefinedWindowLocation` is a location that means "I don't care where you put this on the screen." Now add:

```csharp
using var window = Window.Create("Hello, World!", windowRectangle, WindowFlags.Shown);
```

This creates a SDL window at the location we defined above. We specify `WindowFlags.Shown` to indicate that we want the window to be visible right away (rather than creating it hidden and then showing it later). Now add:

```csharp
while (app.DispatchEvent())
{
}
```

This is this application's _message loop_, where it processes incoming messages (via `DispatchEvent`). Right now, it doesn't do anything, and when you do `dotnet run` it you'll see something like:

<img src="window.png" title="Hello, World!" width="320" height="240"/>
