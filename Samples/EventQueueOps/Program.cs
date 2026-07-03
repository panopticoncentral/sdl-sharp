using SdlSharp;
using SdlSharp.Input;

using var app = new Application(InitFlags.Events);

var failures = 0;

void Check(string name, bool ok)
{
    Console.WriteLine($"{(ok ? "PASS" : "FAIL")}  {name}");
    if (!ok) failures++;
}

// Register a custom event type.
var myEvent = Application.RegisterEvents(1);
Check("registered type >= User", myEvent >= EventType.User);

// Push one and confirm it queues.
var pushed = Application.PushUserEvent(myEvent, code: 42, data1: 7, data2: 9);
Check("push succeeded", pushed);
Check("HasEvent true after push", Application.HasEvent(myEvent));

// WaitDispatchEvent should fire the typed UserEvent handler.
UserEventArgs? received = null;
Application.UserEvent += args => received = args;
var dispatched = Application.WaitDispatchEvent(1000);
Check("wait dispatched an event", dispatched);
Check("user event received", received is { Code: 42, Data1: 7, Data2: 9 });
Check("queue empty after dispatch", !Application.HasEvent(myEvent));

// A filter that drops our event type prevents queueing.
Application.SetEventFilter((in RawEvent e) => e.Type != myEvent);
var pushedWhileFiltered = Application.PushUserEvent(myEvent, code: 1);
Check("filtered push not queued", !pushedWhileFiltered || !Application.HasEvent(myEvent));
Application.SetEventFilter(null);

// A watch observes a pushed event.
var watched = false;
void Watch(in RawEvent e)
{
    if (e.Type == myEvent) watched = true;
}
Application.AddEventWatch(Watch);
Application.PushUserEvent(myEvent, code: 2);
Check("watch observed push", watched);
Application.RemoveEventWatch(Watch);

// Flush clears remaining events of the type.
Application.PushUserEvent(myEvent, code: 3);
Application.FlushEvent(myEvent);
Check("flush cleared queue", !Application.HasEvent(myEvent));

// Enable/disable round-trips.
Application.SetEventEnabled(myEvent, false);
Check("event disabled", !Application.IsEventEnabled(myEvent));
Application.SetEventEnabled(myEvent, true);
Check("event enabled", Application.IsEventEnabled(myEvent));

Console.WriteLine(failures == 0 ? "ALL PASS" : $"{failures} FAILURES");
return failures == 0 ? 0 : 1;
