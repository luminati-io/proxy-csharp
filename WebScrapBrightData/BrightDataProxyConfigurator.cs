using System.Net;

namespace WebScrapBrightData
{
    public class BrightDataProxyConfigurator
    {
        public static HttpClient ConfigureHttpClient(string proxyHost, string proxyUsername, string proxyPassword)
        {

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "YourUserAgent");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            var proxy = new WebProxy(proxyHost);
            proxy.Credentials = new NetworkCredential(proxyUsername, proxyPassword);
            client.DefaultRequestHeaders.TryAddWithoutValidation("Proxy-Authorization", Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{proxyUsername}:{proxyPassword}")));
            return client;
        }
    }
}

