using System.Threading.Tasks;
using MicroKnights.Enumerations.Country;
using MicroKnights.Gender_API;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Gender_API_Test
{
    public class TestByNames : TestBootstrap
    {
        [Theory]
        [InlineData("Frank")]
        [InlineData("Thomas")]
        [InlineData("Jens")]
        [InlineData("Kim")]
        [InlineData("Eddie")]
        [InlineData("Theis")]
        [InlineData("Huxi")]
        [InlineData("Bille")]
        [InlineData("Anders")]
        [InlineData("Norr")]
        [InlineData("Elmer")]
        [InlineData("Linus")]
        [InlineData("Henry")]
        [InlineData("Atlas")]
        [InlineData("Asbjørn")]
        [InlineData("Valde")]
        [InlineData("Kian")]
        [InlineData("Marko")]
        [InlineData("Alvin")]
        [InlineData("Silas")]
        [InlineData("Lasse")]
        [InlineData("Herman")]
        [InlineData("Ask")]
        [InlineData("Niels")]
        [InlineData("Julian")]
        [InlineData("Alex")]
        [InlineData("Hans")]
        [InlineData("Bertil")]
        [InlineData("Buster")]
        [InlineData("Mason")]
        [InlineData("Rasmus")]
        [InlineData("Matti")]
        [InlineData("Jamie")]
        [InlineData("Gustav")]
        [InlineData("Leo")]
        [InlineData("Harald")]
        [InlineData("Samuel")]
        [InlineData("Anton")]
        [InlineData("Malik")]
        [InlineData("Hjalte")]
        [InlineData("Peter")]
        [InlineData("Adrian")]
        [InlineData("Eskild")]
        [InlineData("Milan")]
        [InlineData("Aske")]
        [InlineData("Frej")]
        [InlineData("Ebbe")]
        [InlineData("Kasper")]
        [InlineData("Svend")]
        [InlineData("Bastian")]
        [InlineData("Vitus")]
        [InlineData("Matheo")]
        public async Task TestMaleNamesFromDenmark(string name)
        {
            var client = ServiceProvider.GetRequiredService<GenderApiClient>();
            var response = await client.GetByNameAndCountryType(name, CountryType.Denmark);
            Assert.True(response.IsSuccess, $"IsSuccess == false | {response.Exception.Message}");
            Assert.True(response.GenderType == GenderType.Male, $"GenderType != Male ({response.GenderType.DisplayName})");
        }

        [Theory]
        [InlineData("Nynne")]
        [InlineData("Petra")]
        [InlineData("Miriam")]
        [InlineData("Fie")]
        [InlineData("Ebba")]
        [InlineData("Katrine")]
        [InlineData("Lara")]
        [InlineData("Elisa")]
        [InlineData("Vanessa")]
        [InlineData("Lucca")]
        [InlineData("Sonja")]
        [InlineData("Malia")]
        [InlineData("Elva")]
        [InlineData("Elina")]
        [InlineData("Julia")]
        [InlineData("Uma")]
        [InlineData("Ane")]
        [InlineData("Johanna")]
        [InlineData("Jasmin")]
        [InlineData("Ingeborg")]
        [InlineData("Dicte")]
        [InlineData("Amelia")]
        [InlineData("Vilja")]
        [InlineData("Stella")]
        [InlineData("Anne")]
        [InlineData("Bella")]
        [InlineData("Flora")]
        [InlineData("Aura")]
        [InlineData("Amanda")]
        [InlineData("Lina")]
        [InlineData("Mie")]
//        [InlineData("Lilje")] // reponse: unknown
        [InlineData("My")]
//        [InlineData("Bjørk")] // reponse: male
        [InlineData("Alice")]
        [InlineData("Cecilie")]
        [InlineData("Ava")]
        [InlineData("Martha")]
        [InlineData("Maggie")]
        [InlineData("Melina")]
        [InlineData("Iben")]
        [InlineData("Frederikke")]
        [InlineData("Rose")]
        [InlineData("Mia")]
        [InlineData("Melanie")]
        [InlineData("Solveig")]
        [InlineData("Sally")]
        public async Task TestFemaleNamesFromDenmark(string name)
        {
            var client = ServiceProvider.GetRequiredService<GenderApiClient>();
            var response = await client.GetByNameAndCountryType(name, CountryType.Denmark);
            Assert.True(response.IsSuccess, $"IsSuccess == false | {response.Exception.Message}");
            Assert.True(response.GenderType == GenderType.Female, $"GenderType != Female ({response.GenderType.DisplayName})");
        }

    }
}


