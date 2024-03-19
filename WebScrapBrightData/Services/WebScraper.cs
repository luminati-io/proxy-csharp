using HtmlAgilityPack;

namespace WebScrapeBrightData.Services
{
    internal class WebScraper : IWebScraper
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public WebScraper(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private async Task<string> GetHTML(Uri uri)
        {
            var client = _httpClientFactory.CreateClient("ScrapingClient");
            var response = await client.GetAsync(uri);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<bool> Scrape(Uri uri)
        {
            var content = await GetHTML(uri);
            HtmlDocument doc = new();
            doc.LoadHtml(content);
            var nodes = doc.DocumentNode.SelectNodes("//li/a[@href] | //p/a[@href] | //td/a[@href] | //span/a[@href]");

            if (nodes != null)
            {
                foreach (var node in nodes)
                {
                    string hrefValue = node.GetAttributeValue("href", string.Empty);
                    string title = node.InnerText;
                    Uri fullUri = new(uri, hrefValue);

                    Console.WriteLine($"Title: {title}, Link: {fullUri.AbsoluteUri}");
                }
            }
            else
            {
                Console.WriteLine("No article links found on the page.");
                return false;
            }
            return true;
        }

        public Task<bool> Scrape(string url)
        {
            return Scrape(new Uri(url));
        }
    }
}
