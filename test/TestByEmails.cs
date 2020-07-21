using System.Threading.Tasks;
using MicroKnights.Enumerations.Country;
using MicroKnights.Gender_API;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Gender_API_Test
{
    public class TestByEmails : TestBootstrap
    {
        [Theory]
        [InlineData("frank.nielsen@hotmail.com")]
        public async Task TestMaleEmailFromDenmark(string name)
        {
            var client = ServiceProvider.GetRequiredService<GenderApiClient>();
            var response = await client.GetByEmailAndCountryType(name, CountryType.Denmark);
            Assert.True(response.IsSuccess, $"IsSuccess == false | {response.Exception?.Message}");
            Assert.True(response.GenderType == GenderType.Male, $"GenderType != Male ({response.GenderType.DisplayName})");
        }

        [Theory]
        [InlineData("anne.pande@hotmail.com")]
        public async Task TestFemaleNamesFromDenmark(string name)
        {
            var client = ServiceProvider.GetRequiredService<GenderApiClient>();
            var response = await client.GetByEmailAndCountryType(name, CountryType.Denmark);
            Assert.True(response.IsSuccess, $"IsSuccess == false | {response.Exception?.Message}");
            Assert.True(response.GenderType == GenderType.Female, $"GenderType != Female ({response.GenderType.DisplayName})");
        }

    }
}