[![Promo](https://brightdata.com/static/github_promo_15.png?md5=105367-daeb786e)](https://brightdata.com/?promo=github15) 

本项目演示了如何在 Visual Studio 和 .NET 7 中使用 C#（参考链接：https://www.bright.cn/blog/how-tos/web-scraping-with-c-sharp）配置代理服务器进行网页抓取，并使用 HtmlAgilityPack 进行 HTML 解析。通过使用代理服务器的 IP 地址，代理可以在网页抓取时保护您的数字身份，从而绕过 IP 封禁和地域限制。
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
