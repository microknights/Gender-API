using System.Threading.Tasks;
using MicroKnights.Enumerations.Country;
using MicroKnights.Gender_API;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Gender_API_Test
{
    public class TestByFullnames : TestBootstrap
    {
        [Theory]
        [InlineData("Frank Løvendahl Nielsen")]
        [InlineData("Thomas Hansen")]
        [InlineData("Jens Jensen")]
        [InlineData("Kim Kofoed Nexø")]
        [InlineData("Eddie Skoller")]
        [InlineData("Theis Martinsen")]
        [InlineData("Huxi Back")]
        public async Task TestMaleFullnamesFromDenmark(string name)
        {
            var client = ServiceProvider.GetRequiredService<GenderApiClient>();
            var response = await client.GetByFullnameAndCountryType(name, CountryType.Denmark);
            Assert.True(response.IsSuccess, "IsSuccess == false");
            Assert.True(response.GenderType == GenderType.Male, $"GenderType != Male ({response.GenderType.DisplayName})");
        }

        [Theory]
        [InlineData("Nynne Dagbog")]
        [InlineData("Petra Nielsen")]
        [InlineData("Miriam Hansen")]
        [InlineData("Fie Ibsen")]
        public async Task TestFemaleNamesFromDenmark(string name)
        {
            var client = ServiceProvider.GetRequiredService<GenderApiClient>();
            var response = await client.GetByFullnameAndCountryType(name, CountryType.Denmark);
            Assert.True(response.IsSuccess, "IsSuccess == false");
            Assert.True(response.GenderType == GenderType.Female, $"GenderType != Female ({response.GenderType.DisplayName})");
        }

    }
}