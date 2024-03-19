using Microsoft.Extensions.Configuration;
namespace WebScrapeBrightData.Services
{
    public class BrightDataConfigurationService : IBrightDataConfigurationService
    {
        private readonly IConfiguration _configuration;
        public BrightDataConfigurationService(IConfiguration configuration) {
            _configuration = configuration;
        }

        public BrightDataConfiguration GetConfiguration()
        {
            var conf = _configuration.GetRequiredSection("BrightDataConnection").Get<BrightDataConfiguration>();
            if (string.IsNullOrEmpty(conf.UserName) || string.IsNullOrEmpty(conf.Password) || string.IsNullOrEmpty(conf.Host))
            { 
                throw new ArgumentException("Invalid BrightDataConnection configuration: some of the fields are null or empty");
            }
            return conf;
        }
    }
}
