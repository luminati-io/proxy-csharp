using Microsoft.Extensions.Hosting;
using System.CommandLine.NamingConventionBinder;
using System.CommandLine;
using WebScrapeBrightData.Services;
using Microsoft.Extensions.DependencyInjection;

namespace WebScrapeBrightData
{
    public class RootCommandHandler
    {
        private static readonly string[] UrlOptionAliases = new string[] { "--url" };
        public async Task Run(string[] args, IHostBuilder builder)
        {
            var rootCommand = new RootCommand
            {
                new Option<Uri>(UrlOptionAliases, description: "URL to scrape"),
            };

            rootCommand.Handler = CommandHandler.Create((Uri URL) =>
            {
                var host = builder.Build();
                var argumentService = host.Services.GetRequiredService<IArgumentService>();
                argumentService.URL = URL;

                host.Run();
            });

            await rootCommand.InvokeAsync(args);
        }
    }
}
