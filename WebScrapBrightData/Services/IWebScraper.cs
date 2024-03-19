namespace WebScrapeBrightData.Services
{
    public interface IWebScraper
    {
        Task<bool> Scrape(Uri uri);
        Task<bool> Scrape(string url);
    }
}
