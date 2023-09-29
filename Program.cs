using HtmlAgilityPack;

List<string> links = new();
string path = @"C:\Users\SoulRullex\AppData\Local\Temp\MV";

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
                    Console.Write($"Download error. \nMessage: {error.Message}\nStacktrace: {error.StackTrace}");
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
        Console.Write("Invalid option.");
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
                var result = await content.ReadAsStringAsync();
                var doc = new HtmlDocument();
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

    using (var client = new HttpClient())
    {
        Directory.CreateDirectory(path);
        foreach (var link in links)
        {
            var uri = new Uri(link);
            var fileName = uri.Segments.Last();
            var responseStream = await client.GetStreamAsync(link);
            using var fileStream = new FileStream(path + "\\" + fileName, FileMode.Create);
            responseStream.CopyTo(fileStream);
        }
    }
}