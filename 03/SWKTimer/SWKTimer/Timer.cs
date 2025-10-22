namespace SWKTimer;

public delegate void ExpiredEventHandler();

public class Timer {
    private const int DEFAULT_INTERVAL_MS = 1000;
    
    private readonly Thread _timerThread;

    public int IntervalMs { get; set; } = DEFAULT_INTERVAL_MS;

    // add / remove Method could be overriden here
    public event ExpiredEventHandler TickHandler;
    
    public Timer() {
        _timerThread = new Thread(Schedule);
    }
    
    public void Start() => _timerThread.Start();
    
    private void Schedule() {
        while (true) {
            Thread.Sleep(IntervalMs);

            TickHandler?.Invoke();
        }
    }
}