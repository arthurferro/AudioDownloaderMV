# MV Audio Downloader

MV Audio Downloader is a simple console application written in C# that allows you to download audio files from a given URL. It utilizes the HtmlAgilityPack library to parse HTML content and extract audio links.

## How to Use

1. **Search Audios**: Enter option 1 to search for audio files from a provided URL. Paste the URL when prompted, and the program will extract audio links from the HTML content.

2. **Exit**: Enter option 2 to exit the program.

## Dependencies

The program uses the HtmlAgilityPack library for HTML parsing. Make sure to install this library before running the application.

```bash
dotnet add package HtmlAgilityPack
```
Note: Ensure that you have the necessary permissions to write to the specified path for downloading audio files.

Feel free to customize and enhance the program according to your needs!
