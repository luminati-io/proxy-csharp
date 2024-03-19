using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.CommandLine;
using System.Net;
using WebScrapeBrightData;
using WebScrapeBrightData.Services;

var builder = Host.CreateDefaultBuilder(args);
builder.ConfigureAppConfiguration((hostingContext, config) => {
    var env = hostingContext.HostingEnvironment.EnvironmentName;
    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
          .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true);
});
builder.ConfigureServices((hostContext, services) => {
    services.AddHostedService<Worker>();
    services.AddTransient<IWebScraper, WebScraper>();
    services.AddTransient<IBrightDataConfigurationService, BrightDataConfigurationService>();
    services.AddSingleton<IArgumentService, ArgumentService>();
    services.AddHttpClient();
    services.AddHttpClient("ScrapingClient", (serviceProvider, client) => {
        //var configurationService = serviceProvider.GetRequiredService<IBrightDataConfigurationService>();
        //var configuration = configurationService.GetConfiguration();
        client.DefaultRequestHeaders.Add("Accept", "application/json");
        //client.DefaultRequestHeaders.TryAddWithoutValidation("Proxy-Authorization", Convert.ToBase64String(
        //    System.Text.Encoding.UTF8.GetBytes($"{configuration.UserName}:{configuration.Password}")));
    }).ConfigurePrimaryHttpMessageHandler((serviceProvider) => {
        var configurationService = serviceProvider.GetRequiredService<IBrightDataConfigurationService>();
        var configuration = configurationService.GetConfiguration();
        var proxy = new WebProxy(configuration.Host)
        {
            Credentials = new NetworkCredential(configuration.UserName, configuration.Password),
        };
        return new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true,
            Proxy = proxy,
            UseProxy = true,
        };
    });
});
Console.OutputEncoding = System.Text.Encoding.UTF8;
var commandHandler = new RootCommandHandler();
await commandHandler.Run(args, builder);