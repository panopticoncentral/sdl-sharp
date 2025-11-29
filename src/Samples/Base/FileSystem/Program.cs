using Sdl3Sharp;

Console.WriteLine("SDL3 Filesystem API Demo");
Console.WriteLine("========================\n");

// Get base path (where the application is installed)
Console.WriteLine("Application Paths:");
Console.WriteLine($"  Base Path: {FileSystem.GetBasePath()}");

// Get pref path (where user data can be saved)
var prefPath = FileSystem.GetPrefPath("SDL3Sharp", "FileSystemSample");
Console.WriteLine($"  Pref Path: {prefPath}");

// Get current working directory
Console.WriteLine($"  Current Directory: {FileSystem.GetCurrentDirectory()}\n");

// Get user folders
Console.WriteLine("User Folders:");
try
{
    Console.WriteLine($"  Home: {FileSystem.GetUserFolder(Folder.Home)}");
    Console.WriteLine($"  Desktop: {FileSystem.GetUserFolder(Folder.Desktop)}");
    Console.WriteLine($"  Documents: {FileSystem.GetUserFolder(Folder.Documents)}");
    Console.WriteLine($"  Downloads: {FileSystem.GetUserFolder(Folder.Downloads)}");
    Console.WriteLine($"  Music: {FileSystem.GetUserFolder(Folder.Music)}");
    Console.WriteLine($"  Pictures: {FileSystem.GetUserFolder(Folder.Pictures)}");
    Console.WriteLine($"  Videos: {FileSystem.GetUserFolder(Folder.Videos)}");
}
catch (Exception ex)
{
    Console.WriteLine($"  Error getting user folders: {ex.Message}");
}

Console.WriteLine();

// Test file operations
Console.WriteLine("File Operations:");
var testFile = Path.Combine(prefPath, "test.txt");
var testFile2 = Path.Combine(prefPath, "test_renamed.txt");

try
{
    // Write a test file using .NET
    File.WriteAllText(testFile, "Hello from SDL3Sharp!");
    Console.WriteLine($"  Created: {testFile}");

    // Check if file exists
    if (FileSystem.PathExists(testFile))
    {
        Console.WriteLine($"  File exists: yes");

        // Get file info
        var info = FileSystem.GetPathInfo(testFile);
        Console.WriteLine($"  File type: {info.Type}");
        Console.WriteLine($"  File size: {info.Size} bytes");
        Console.WriteLine($"  Created: {info.CreateTime}");
        Console.WriteLine($"  Modified: {info.ModifyTime}");
        Console.WriteLine($"  Accessed: {info.AccessTime}");
    }

    // Copy file
    FileSystem.CopyFile(testFile, testFile2);
    Console.WriteLine($"  Copied to: {testFile2}");

    // Remove original
    FileSystem.RemovePath(testFile);
    Console.WriteLine($"  Removed: {testFile}");

    // Rename copy back
    FileSystem.RenamePath(testFile2, testFile);
    Console.WriteLine($"  Renamed back to: {testFile}");
}
catch (Exception ex)
{
    Console.WriteLine($"  Error during file operations: {ex.Message}");
}

Console.WriteLine();

// Enumerate directory
Console.WriteLine("Directory Contents (Pref Path):");
try
{
    FileSystem.EnumerateDirectory(prefPath, (dir, file) =>
    {
        Console.WriteLine($"  {file}");
        return true; // Continue enumeration
    });
}
catch (Exception ex)
{
    Console.WriteLine($"  Error enumerating directory: {ex.Message}");
}

Console.WriteLine();

// Glob directory
Console.WriteLine("Glob Directory (*.txt files):");
try
{
    var txtFiles = FileSystem.GlobDirectory(prefPath, "*.txt");
    foreach (var file in txtFiles)
    {
        Console.WriteLine($"  {file}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"  Error globbing directory: {ex.Message}");
}

Console.WriteLine();

// Cleanup
try
{
    FileSystem.RemovePath(testFile);
    Console.WriteLine("Cleanup: Removed test file");
}
catch
{
    // Ignore cleanup errors
}

Console.WriteLine("\nDone!");
