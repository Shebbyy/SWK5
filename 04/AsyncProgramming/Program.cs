using static System.Console;

const string URL1 = "https://github.com/progit/progit2/releases/download/2.1.360/progit.pdf";
const string URL2 = "https://github.com/SoftUni/Programming-Basics-Book-CSharp-EN/raw/b153f06082ede1d0e4c8e7994c4d6a628886c22e/resources/Programming-Basics-CSharp-Book-and-Video-Lessons-Nakov-v2019.pdf";

var downloader = new Downloader();

WriteLine($"====================== {nameof(Downloader.DownloadSync)} ======================");
// TODO
WriteLine($"{nameof(Downloader.DownloadSync)} gave control back to caller");
WriteLine($"{nameof(Downloader.DownloadSync)} completed work");
WriteLine();

WriteLine($"====================== {nameof(Downloader.DownloadAsync_Task)} ======================");
// TODO
WriteLine($"{nameof(Downloader.DownloadAsync_Task)} gave control back to caller");
// TODO
WriteLine($"{nameof(Downloader.DownloadAsync_Task)} completed work");
WriteLine();

WriteLine($"====================== {nameof(Downloader.DownloadAsync_Await)} ======================");
// TODO
WriteLine($"{nameof(Downloader.DownloadAsync_Await)} gave control back to caller");
// TODO
WriteLine($"{nameof(Downloader.DownloadAsync_Await)} completed work");
WriteLine();

WriteLine($"======================= {nameof(Downloader.DownloadMultipleAsync)} =======================");
// TODO
WriteLine($"{nameof(Downloader.DownloadMultipleAsync)} gave control back to caller");
// TODO
WriteLine($"{nameof(Downloader.DownloadMultipleAsync)} completed work");
WriteLine();

ReadLine(); 
