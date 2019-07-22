using System;
using MicroKnights.Gender_API;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gender_API_Test
{
    public class TestBootstrap
    {
        protected IConfiguration Configuration { get; private set; }
        protected IServiceProvider ServiceProvider { get; private set; }

        public TestBootstrap()
        {
            Configuration = new ConfigurationBuilder()
                .AddUserSecrets("306AA3EB-A26A-4B45-B629-0A56B5C3BEF9")
                .Build();

            ServiceProvider = GetConfigureServices(Configuration);
        }

        private IServiceProvider GetConfigureServices(IConfiguration configuration)
        {
            var services = new ServiceCollection();
//            services.AddHttpClient()
//                .UseGenderAPI(configuration["Gender-API:ApiKey"]);
            services.UseGenderAPI(configuration["Gender-API:ApiKey"]);
            return services.BuildServiceProvider();
        }
    }
}