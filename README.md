# MV Audio Downloader

This is a simple console application written in C# for downloading audio files from a given URL using the HtmlAgilityPack and HttpClient libraries.

## How to Use

1. Clone the repository or download the source code files.
2. Compile the code using any C# compiler.
3. Run the compiled executable file (e.g., `MVAudioDownloader.exe`).
4. Follow the instructions provided by the application.

## Functionality

The application presents a menu with the following options:

1. **Search Audios**: Allows the user to paste a URL containing audio files to download.
2. **Exit**: Exits the application.

Upon selecting the "Search Audios" option, the application prompts the user to paste a URL. It then searches for `<audio>` tags in the HTML content of the provided URL, extracts the `src` attribute from each `<audio>` tag, and downloads the corresponding audio files to a temporary directory. After downloading, it opens the directory containing the downloaded files using the default file explorer.

## Dependencies

- [HtmlAgilityPack](https://html-agility-pack.net/)
- [System.Diagnostics](https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics?view=net-6.0)
- [System.IO](https://docs.microsoft.com/en-us/dotnet/api/system.io?view=net-6.0)
- [System.Net.Http](https://docs.microsoft.com/en-us/dotnet/api/system.net.http?view=net-6.0)

## Notes

- This application may not support all types of audio files or URLs.
- Error handling is implemented to catch and display any exceptions that may occur during the download process.

## Contribution

Feel free to contribute to this project by opening issues or pull requests.

## License

This project is licensed under the [MIT License](LICENSE).
