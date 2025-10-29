using System.Net;
using static System.Console;

public class Downloader
{
    public void DownloadSync(string url, string filePath) {
        // outdated but interesting still, should not be used in practice though, HttpClient preferrable
        using var client = new WebClient();
        byte[] bytes = client.DownloadData(url);
        WriteLine($"{nameof(DownloadSync)}: Downloaded '{url}'");
        
        File.WriteAllBytes(filePath, bytes);
        WriteLine($"{nameof(DownloadSync)}: Saved '{filePath}'");
    }

    public Task DownloadAsync_Task(string url, string filePath) {
        using var client = new WebClient();

        // ContinueWith = Callback when finished, comparable to .then in JS
        return client.DownloadDataTaskAsync(url)
            .ContinueWith(t => {
                WriteLine($"{nameof(DownloadAsync_Task)}: Downloaded '{url}'");
                return t.Result;
            })
            .ContinueWith(t => File.WriteAllBytes(filePath, t.Result))
            .ContinueWith(t => WriteLine($"{nameof(DownloadAsync_Task)}: Saved '{filePath}'"));
    }

    // async not absolutely necessary, but await only allowed from compiler to be used in async methods for C# backwards compatibility
    public async Task DownloadAsync_Await(string url, string filePath)
    {
        // basically identical to synchronous, just with awaits and as async function with Task ReturnType
        using var client = new WebClient();

        // await task
        byte[] bytes = await client.DownloadDataTaskAsync(url);
        WriteLine($"{nameof(DownloadAsync_Await)}: Downloaded '{url}'");

        await File.WriteAllBytesAsync(filePath, bytes);
        WriteLine($"{nameof(DownloadAsync_Await)}: Saved '{filePath}'");
    }

    // Possible without return type, bad practice though, we no longer know when finished and cannot even catch exceptions
    public async Task DownloadMultipleAsync(string url1, string filePath1, string url2, string filePath2) {
        Task t1 = DownloadAsync_Await(url1, filePath1);
        WriteLine($"{nameof(DownloadMultipleAsync)}: {nameof(DownloadAsync_Await)} of '{url1}' started");

        Task t2 = DownloadAsync_Await(url2, filePath2);
        WriteLine($"{nameof(DownloadMultipleAsync)}: {nameof(DownloadAsync_Await)} of '{url2}' started");

        // returns task, can then be awaited aswell
        await Task.WhenAll(t1, t2);
        WriteLine($"{nameof(DownloadMultipleAsync)}: {nameof(DownloadAsync_Await)} of all files completed");
    }
}
