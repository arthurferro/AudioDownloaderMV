﻿using HtmlAgilityPack;
using System.Diagnostics;

var links = new List<string>();
var path = Path.GetTempPath();

var options = new Dictionary<int, string>()
    {
        {1,"Search Audios"},
        {2,"Exit"}
    };

void ShowMenu()
{
    Console.Clear();
    Console.WriteLine("MV Audio Downloader");
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
                links = new List<string>();
                path = Path.GetTempPath();
                Console.Clear();
                Console.Write("Paste URL: ");
                string url = Console.ReadLine()!;
                try
                {
                    SearchAudios(url).GetAwaiter().GetResult();
                    Process.Start("explorer.exe", path);
                    ShowMenu();
                }
                catch (Exception error)
                {
                    Console.Clear();
                    Console.Write($"Download error. \nMessage: {error.Message}\nStacktrace: {error.StackTrace}");
                    Console.ReadKey();
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
        Task.Delay(TimeSpan.FromSeconds(3)).Wait();
        ShowMenu();
    }
}

ShowMenu();
async Task SearchAudios(string url)
{
    using (HttpClient client = new())
    {
        using HttpResponseMessage response = await client.GetAsync(url);
        using HttpContent content = response.Content;
        var result = await content.ReadAsStringAsync();
        var doc = new HtmlDocument();
        doc.LoadHtml(result);
        var audioTags = doc.DocumentNode.Descendants("audio").ToList();
        foreach (var audioTag in audioTags)
        {
            var linkAudio = audioTag.InnerText;
            links.Add(linkAudio);
        }
    }

    using (var client = new HttpClient())
    {
        path += "MV\\";
        Directory.CreateDirectory(path);
        foreach (var link in links)
        {
            var uri = new Uri(link);
            var fileName = uri.Segments.Last();
            var responseStream = await client.GetStreamAsync(link);
            using var fileStream = new FileStream(path + fileName, FileMode.Create);
            await responseStream.CopyToAsync(fileStream);
        }
    }
}