using static System.Console;

public class Downloader
{
    public void DownloadSync(string url, string filePath)
    {
        // TODO
        WriteLine($"{nameof(DownloadSync)}: Downloaded '{url}'");

        // TODO
        WriteLine($"{nameof(DownloadSync)}: Saved '{filePath}'");
    }

    public void DownloadAsync_Task(string url, string filePath)
    {
        // TODO
        WriteLine($"{nameof(DownloadAsync_Task)}: Downloaded '{url}'");

        // TODO
        WriteLine($"{nameof(DownloadAsync_Task)}: Saved '{filePath}'");
    }

    public void DownloadAsync_Await(string url, string filePath)
    {
        // TODO
        WriteLine($"{nameof(DownloadAsync_Await)}: Downloaded '{url}'");

        // TODO
        WriteLine($"{nameof(DownloadAsync_Await)}: Saved '{filePath}'");
    }

    public void DownloadMultipleAsync(string url1, string filePath1, string url2, string filePath2)
    {
        // TODO
        WriteLine($"{nameof(DownloadMultipleAsync)}: {nameof(DownloadAsync_Await)} of '{url1}' started");

        // TODO
        WriteLine($"{nameof(DownloadMultipleAsync)}: {nameof(DownloadAsync_Await)} of '{url2}' started");

        // TODO
        WriteLine($"{nameof(DownloadMultipleAsync)}: {nameof(DownloadAsync_Await)} of all files completed");
    }
}
