namespace WebScrapApp
{
    public class ProxyRotator
    {
        private List<string> _validProxies = new List<string>();
        private readonly Random _random = new();

        public ProxyRotator(string[] proxies, bool isLocal)
        { 
            if (isLocal)
            {
                _validProxies.Add("http://localhost:8080/");
            }
            else
            {
                _validProxies = ProxyChecker.GetWorkingProxies(proxies.ToList()).Result;
            }
            if (_validProxies.Count == 0)
                throw new InvalidOperationException();
        }

        public HttpClient ScrapeDataWithRandomProxy(string url)
        {
            if (_validProxies.Count == 0)
                throw new InvalidOperationException();

            var proxyUrl = _validProxies[_random.Next(_validProxies.Count)];
            return ProxyHttpClient.CreateClient(proxyUrl);
        }
    }
}
