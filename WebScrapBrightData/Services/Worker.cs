using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace WebScrapeBrightData.Services
{
    internal class Worker : IHostedService
    {
        private readonly IArgumentService _argumentService;
        private readonly IWebScraper _webScraper;
        public Worker(IArgumentService argumentService, IWebScraper webScraper)
        {
            _argumentService = argumentService;
            _webScraper = webScraper;   
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var result = await _webScraper.Scrape(_argumentService.URL);
            if (!result)
            {
                throw new Exception("Scraping failed");
            }
            Console.WriteLine("SUCCESS");
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
