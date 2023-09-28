using HtmlAgilityPack;
using System.Net;

List<string> links = new();
string path = @"C:\tmp\";

Dictionary<int, string> options = new()
    {
        {1,"Search Audios"},
        {2,"Exit"}
    };

void ShowMenu()
{
    Console.Clear();
    Console.WriteLine("---Audio Downloader---");
    try
    {
        foreach (var option in options)
        {
            Console.WriteLine($"{option.Key} - {option.Value}");
        }
        Console.Write("Type option: ");
        var selectedOption = int.Parse(Console.ReadLine()!);
        if (options.ContainsKey(selectedOption))
        {
            if (selectedOption == 1)
            {
                Console.Clear();
                Console.Write("Paste URL: ");
                string url = Console.ReadLine()!;
                try
                {
                    SearchAudios(url).GetAwaiter().GetResult();
                    ShowMenu();
                }
                catch (Exception error)
                {
                    Console.Clear();
                    Console.WriteLine($"Download error. \nMessage: {error.Message}\nStacktrace: {error.StackTrace}");
                    Thread.Sleep(TimeSpan.FromSeconds(3));
                    ShowMenu();
                }
            }
            else if (selectedOption == 2)
            {
                Environment.Exit(0);
            }
        }
        else
        {
            ShowMenu();
        }
    }
    catch
    {
        Console.Clear();
        Console.WriteLine("Invalid option.");
        Thread.Sleep(TimeSpan.FromSeconds(3));
        ShowMenu();
    }
}

ShowMenu();
async Task SearchAudios(string url)
{
    using (HttpClient client = new())
    {
        using (HttpResponseMessage response = await client.GetAsync(url))
        {
            using (HttpContent content = response.Content)
            {
                string result = await content.ReadAsStringAsync();
                HtmlDocument doc = new();
                doc.LoadHtml(result);
                var audioTags = doc.DocumentNode.Descendants("audio").ToList();
                foreach (var audioTag in audioTags)
                {
                    string linkAudio = audioTag.GetAttributeValue("src", null);
                    links.Add(linkAudio);
                }
            }
        }
    }

    using (WebClient client = new())
    {
        foreach (var link in links)
        {
            Uri uri = new Uri(link);
            var fileName = uri.Segments.Last();
            client.DownloadFile(link, (path + fileName));
        }
    }
}