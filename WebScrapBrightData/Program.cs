namespace WebScrapBrightData
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Bright Data Proxy Configuration
            string host = "your_brightdata_proxy_host";
            string username = "your_brightdata_proxy_username";
            string password = "your_brightdata_proxy_password";
            var client = BrightDataProxyConfigurator.ConfigureHttpClient(host, username, password);

            // Scrape content from the target URL
            string urlToScrape = "https://www.wikipedia.org/";
            await WebContentScraper.ScrapeContent(urlToScrape, client);
        }
    }
}