using System.Net;

namespace WebScrapApp
{
    public class ProxyHttpClient
    {
        public static HttpClient CreateClient(string proxyUrl)
        {
            var httpClientHandler = new HttpClientHandler()
            {
                Proxy = new WebProxy(proxyUrl),
                UseProxy = true
            };
            return new HttpClient(httpClientHandler);
        }
    }
}
