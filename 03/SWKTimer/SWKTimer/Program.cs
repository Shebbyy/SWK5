// Trial Project for Delegates

using SWKTimer;

var timer = new SWKTimer.Timer {
    IntervalMs = 700
};

// += Append Handler
timer.TickHandler += () => Console.WriteLine("Timer elapsed");
timer.TickHandler += () => Console.WriteLine("Timer elapsed");
timer.TickHandler += () => Console.WriteLine("Timer elapsed");

// would be allowed with delegate, would override the other handlers as expected though; event suppresses this
// timer.TickHandler = () => Console.WriteLine("Timer elapsed");

ExpiredEventHandler t3 = () => Console.WriteLine("Handler to be removed");
timer.TickHandler += t3;

timer.Start();

// sometime later 
timer.TickHandler -= t3; // Remove Handler