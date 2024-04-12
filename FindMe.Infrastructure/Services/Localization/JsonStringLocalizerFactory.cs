using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;

namespace FindMe.Infrastructure.Services.Localization
{
    public class JsonStringLocalizerFactory : IStringLocalizerFactory
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _host;
        public JsonStringLocalizerFactory(
            IConfiguration configuration,
            IWebHostEnvironment host)
        {
            _configuration = configuration;
            _host = host;
        }

        public IStringLocalizer Create(Type resourceSource)
        {
            return new JsonStringLocalizer(_configuration, _host);
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            return new JsonStringLocalizer(_configuration, _host);

        }
    }
}
