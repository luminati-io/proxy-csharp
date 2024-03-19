This project demonstrates setting up proxy servers in [C# for web scraping](https://brightdata.com/blog/how-tos/web-scraping-with-c-sharp) with Visual Studio and .NET 7, leveraging HtmlAgilityPack for HTML parsing. Proxies protect your digital identity during web scraping by using their IP address, circumventing IP bans and geoblocking.

### Prerequisites
-   [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) or [Visual Studio Code](https://code.visualstudio.com/)
-   [.NET 7 or newer](https://dotnet.microsoft.com/en-us/download)
-   [HtmlAgilityPack NuGet package](https://www.nuget.org/packages/HtmlAgilityPack/)
    

### Configuring a Local Proxy

-   Download and install mitmproxy.
    
-   Launch mitmproxy with the mitmproxy command.
    

### Web Scraping Setup

**ProxyHttpClient** - is designed to configure an HttpClient instance to route requests via a specified proxy server.

**ProxyRotator** - manages a list of proxies, providing a method to select a proxy for each web request randomly. This randomization is key to reducing the risk of detection and potential IP bans during web scraping.

When the `isLocal` is set to True, it takes the local proxy from mitmproxy. If it’s set to False, it takes the public IPs of the proxies.

**ProxyChecker** - is used to validate the list of proxy servers. When you use the `GetWorkingProxies` method with a list of proxy URLs, it checks each proxy’s status asynchronously via the `CheckProxy` method, collecting operational proxies in a `workingProxies` list. Within `CheckProxy`, you establish an `HttpClient` with the proxy URL, make a test request to http://www.google.com, and record progress safely using a semaphore.

The `IsProxyWorking` method confirms the proxy’s functionality by examining the response status code, returning true for operational proxies. This class aids in identifying working proxies from a given list.

**WebScraper** - encapsulates web scraping functionality. When you call the `ScrapeData` method, you provide it with a `ProxyRotator` instance and a target URL. Inside this method, you use an `HttpClient` to make an asynchronous GET request to the URL, retrieve the HTML content, and parse it using the `HtmlAgilityPack` library. Then you employ XPath queries to locate and extract links and corresponding titles from specific HTML elements. If any article links are found, you print their titles and absolute URLs; otherwise, you print a message indicating no links were found.

### Using Bright Data Proxy

-   Sign up for Bright Data and create [a residential proxy](https://brightdata.com/proxy-types/residential-proxies).    
-   Update **appsettings.json** file with your credentials in **WebScrapeBrightdata** project
    
### Running Your Application

-   Use `dotnet build` and `dotnet run -- --url https://www.wikipedia.org/` to compile and execute the application, and program will show extracted Wikipedia article titles and links. 

[Bright Data’s proxy services](https://brightdata.com/proxy-types) facilitates anonymous and efficient web scraping in C#, providing a scalable solution for bypassing IP bans. This tutorial offers a foundation for integrating proxy servers into your web scraping projects, following best practices to ensure data collection reliability.