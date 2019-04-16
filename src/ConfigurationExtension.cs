using System;
using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;

namespace MicroKnights.Gender_API
{
    public static class ConfigurationExtension
    {
        public static readonly string ServiceName = "Gender-API";

        // ReSharper disable once InconsistentNaming
        public static IServiceCollection UseGenderAPI(this IServiceCollection services, string apiKey)
        {
            services.AddHttpClient(ServiceName, c =>
            {
                c.BaseAddress = new Uri("https://gender-api.com");
                c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            });

            services.AddSingleton<GenderApiClient>();

            return services;
        }
    }
}