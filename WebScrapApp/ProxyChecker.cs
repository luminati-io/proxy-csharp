using WebScrapApp;

public class ProxyChecker
{
    private static SemaphoreSlim consoleSemaphore = new SemaphoreSlim(1, 1);
    private static int currentProxyNumber = 0;

    public static async Task<List<string>> GetWorkingProxies(List<string> proxies)
    {
        var tasks = new List<Task<Tuple<string, bool>>>();

        foreach (var proxyUrl in proxies)
        {
            tasks.Add(CheckProxy(proxyUrl, proxies.Count));
        }

        var results = await Task.WhenAll(tasks);

        var workingProxies = new List<string>();
        foreach (var result in results)
        {
            if (result.Item2)
            {
                workingProxies.Add(result.Item1);
            }
        }

        return workingProxies;
    }

    private static async Task<Tuple<string, bool>> CheckProxy(string proxyUrl, int totalProxies)
    {
        var client = ProxyHttpClient.CreateClient(proxyUrl);
        bool isWorking = await IsProxyWorking(client);

        await consoleSemaphore.WaitAsync();
        try
        {
            currentProxyNumber++;
            Console.WriteLine($"Proxy: {currentProxyNumber} de {totalProxies}");
        }
        finally
        {
            consoleSemaphore.Release();
        }

        return new Tuple<string, bool>(proxyUrl, isWorking);
    }

    private static async Task<bool> IsProxyWorking(HttpClient client)
    {
        try
        {
            var testUrl = "http://www.google.com";
            var response = await client.GetAsync(testUrl);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}