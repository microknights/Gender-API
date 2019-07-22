using System;
using Microsoft.Extensions.DependencyInjection;

namespace MicroKnights.Gender_API
{
    public static class ConfigurationExtension
    {
        // ReSharper disable once InconsistentNaming
        public static IServiceCollection UseGenderAPI(this IServiceCollection services, string apiKey)
        {
            services.AddHttpClient<GenderApiClient>(c =>
            {
                c.BaseAddress = new Uri("https://gender-api.com");
            });

            services.AddSingleton(new GenderApiConfiguration
            {
                ApiKey = apiKey
            });

            return services;
        }
    }
}