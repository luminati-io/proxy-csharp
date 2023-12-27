using HtmlAgilityPack;

namespace WebScrapBrightData
{
    public class WebContentScraper
    {
        public static async Task ScrapeContent(string url, HttpClient client)
        {
            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            HtmlDocument doc = new();
            doc.LoadHtml(content);
            var nodes = doc.DocumentNode.SelectNodes("//li/a[@href] | //p/a[@href] | //td/a[@href]");

            if (nodes != null)
            {
                foreach (var node in nodes)
                {
                    string hrefValue = node.GetAttributeValue("href", string.Empty);
                    string title = node.InnerText;

                    Uri baseUri = new(url);
                    Uri fullUri = new(baseUri, hrefValue);

                    Console.WriteLine($"Title: {title}, Link: {fullUri.AbsoluteUri}");
                }
            }
            else
            {
                Console.WriteLine("No article links found on the page.");
            }
        }
    }
}