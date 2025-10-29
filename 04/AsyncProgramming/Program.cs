using static System.Console;

const string URL1 = "https://github.com/progit/progit2/releases/download/2.1.360/progit.pdf";
const string URL2 = "https://github.com/SoftUni/Programming-Basics-Book-CSharp-EN/raw/b153f06082ede1d0e4c8e7994c4d6a628886c22e/resources/Programming-Basics-CSharp-Book-and-Video-Lessons-Nakov-v2019.pdf";

var downloader = new Downloader();

WriteLine($"====================== {nameof(Downloader.DownloadSync)} ======================");
downloader.DownloadSync(URL1, "download1.pdf");
WriteLine($"{nameof(Downloader.DownloadSync)} gave control back to caller");
WriteLine($"{nameof(Downloader.DownloadSync)} completed work");
WriteLine();

WriteLine($"====================== {nameof(Downloader.DownloadAsync_Task)} ======================");
var task = downloader.DownloadAsync_Task(URL1, "download2.pdf");
WriteLine($"{nameof(Downloader.DownloadAsync_Task)} gave control back to caller");
task = task.ContinueWith(_ => {
        WriteLine($"{nameof(Downloader.DownloadAsync_Task)} completed work");
        WriteLine();
    });

// Not absolutely necessary, would continue with other methods mixing all the console outputs up though
task.Wait(); // blocks the thread, therefore not allowing any handling of user inputs and such

WriteLine($"====================== {nameof(Downloader.DownloadAsync_Await)} ======================");
task = downloader.DownloadAsync_Await(URL1, "download3.pdf");
WriteLine($"{nameof(Downloader.DownloadAsync_Await)} gave control back to caller");

await task; // does not block the thread, as the one above, therefore can freely still handle userinputs
WriteLine($"{nameof(Downloader.DownloadAsync_Await)} completed work");
WriteLine();

WriteLine($"======================= {nameof(Downloader.DownloadMultipleAsync)} =======================");
task = downloader.DownloadMultipleAsync(URL1, "download_mult1.pdf", URL2, "download_mult2.pdf");
WriteLine($"{nameof(Downloader.DownloadMultipleAsync)} gave control back to caller");

await task;
WriteLine($"{nameof(Downloader.DownloadMultipleAsync)} completed work");
WriteLine();
