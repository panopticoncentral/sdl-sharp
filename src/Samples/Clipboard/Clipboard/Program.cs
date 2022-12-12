using SdlSharp;

using var applications = new Application(Subsystems.Video);

var exit = false;
while (!exit)
{
    var command = Console.ReadLine();

    switch (command)
    {
        case "q":
            exit = true;
            break;

        case "c":
            Clipboard.Text = Console.ReadLine();
            Console.WriteLine("Set clipboard text.");
            break;

        case "p":
            Console.WriteLine(Clipboard.Text);
            Console.WriteLine("Read clipboard text.");
            break;
    }
}
