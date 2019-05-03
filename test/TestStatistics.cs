using System.Threading.Tasks;
using MicroKnights.Gender_API;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Gender_API_Test
{
    public class TestStatistics : TestBootstrap
    {
        [Fact]
        public async Task TestStats()
        {
            var client = ServiceProvider.GetRequiredService<GenderApiClient>();
            var response = await client.GetStatistics();
            Assert.True(response.IsSuccess, "IsSuccess == false");
            Assert.True(response.IsLimitReached == false, $"IsLimitReached == true");
        }
    }
}