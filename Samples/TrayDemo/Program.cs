using SdlSharp;
using SdlSharp.Graphics;

// A real tray icon appears in the menu bar for ~3 seconds during this run.
using var app = new Application(InitFlags.Video);

var failures = 0;

void Check(string name, bool ok)
{
    Console.WriteLine($"{(ok ? "PASS" : "FAIL")}  {name}");
    if (!ok) failures++;
}

// Small solid icon.
using var icon = Surface.Create(16, 16, PixelFormat.Rgba8888);
icon.FillRect(null, icon.MapColor(new Color(200, 60, 60, 255)));

using (var tray = Tray.Create(icon, "SdlSharp TrayDemo"))
{
    var menu = tray.CreateMenu();
    Check("root menu created", tray.Menu is not null);

    var clicked = false;
    var button = menu.InsertEntry(-1, "Do Thing", TrayEntryFlags.Button);
    button.SetCallback(_ => clicked = true);

    var checkbox = menu.InsertEntry(-1, "Enabled Option", TrayEntryFlags.Checkbox | TrayEntryFlags.Checked);
    _ = menu.InsertEntry(-1, null); // separator

    var parent = menu.InsertEntry(-1, "More", TrayEntryFlags.Submenu);
    var submenu = parent.CreateSubmenu();
    var subItem = submenu.InsertEntry(-1, "Nested", TrayEntryFlags.Button);

    Check("entries snapshot", menu.Entries.Length == 4);
    Check("label get", button.Label == "Do Thing");
    button.Label = "Do Thing!";
    Check("label set round-trip", button.Label == "Do Thing!");
    Check("checkbox starts checked", checkbox.IsChecked);
    checkbox.IsChecked = false;
    Check("checkbox toggle", !checkbox.IsChecked);
    checkbox.IsEnabled = false;
    Check("enabled toggle", !checkbox.IsEnabled);
    Check("submenu reachable", parent.Submenu is not null);
    Check("submenu parent entry", submenu.ParentEntry is not null);
    Check("nested label", subItem.Label == "Nested");

    button.Click();
    Application.PumpEvents();
    Tray.Update();
    Check("click fired callback", clicked);

    subItem.Remove();
    Check("entry removed", submenu.Entries.Length == 0);

    // Keep the icon visible briefly so the smoke is observable.
    for (var i = 0; i < 30; i++)
    {
        Application.DispatchEvents();
        Tray.Update();
        Thread.Sleep(100);
    }
}

Console.WriteLine(failures == 0 ? "ALL PASS" : $"{failures} FAILURES");
return failures == 0 ? 0 : 1;
