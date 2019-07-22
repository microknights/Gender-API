using System;
using System.Net.Http;
using System.Threading.Tasks;
using MicroKnights.Gender_API;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Gender_API_Test
{
    public class TestPlainConsole
    {
        [Fact]
        public async Task TestStatsUsingConsole()
        {
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets("306AA3EB-A26A-4B45-B629-0A56B5C3BEF9")
                .Build();
            var apiKey = configuration["Gender-API:ApiKey"];


            var client = new GenderApiClient(
                new HttpClient
                {
                    BaseAddress = new Uri("https://gender-api.com")
                }, 
                new GenderApiConfiguration
                {
                    ApiKey = apiKey
                });

            var response = await client.GetStatistics();
            Assert.True(response.IsSuccess, $"IsSuccess == false | {response.Exception.Message}");
            Assert.True(response.IsLimitReached == false, $"IsLimitReached == true");
        }
    }
}