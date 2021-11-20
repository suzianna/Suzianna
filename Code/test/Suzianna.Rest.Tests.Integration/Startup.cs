using Microsoft.Extensions.DependencyInjection;

namespace Suzianna.Rest.Tests.Integration
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();

            services.AddSingleton<IHttpRequestSender, DefaultHttpRequestSender>();
        }
    }
}


