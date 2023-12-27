using WebScrapApp;

public class Program
{
    static async Task Main(string[] args)
    {
        string[] proxies = {
            "http://60.174.1.240:8089",
            "http://183.165.224.146:8089",
            "http://183.164.242.245:8089",
            "http://72.10.160.173:25759",
            "http://122.3.41.154:8090"
        };

        var proxyRotator = new ProxyRotator(proxies, false);
        string urlToScrape = "https://www.wikipedia.org/";
        await WebScraper.ScrapeData(proxyRotator, urlToScrape);
    }
}
