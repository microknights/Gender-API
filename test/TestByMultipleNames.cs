using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroKnights.Enumerations.Country;
using MicroKnights.Gender_API;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Gender_API_Test
{
    public class TestByMultipleNames : TestBootstrap
    {
        [Theory]
        [InlineData("Frank", "Thomas", "Jens"/*, "Kim"*/)] // response: Kim
        [InlineData("Eddie", "Theis", "Huxi", "Bille")]
        [InlineData("Anders", "Norr", "Elmer")]
        [InlineData("Linus", "Henry", "Atlas")]
        [InlineData("Asbjørn", "Valde", "Kian")]
        [InlineData("Marko", "Alvin", "Silas")]
        [InlineData("Lasse", "Herman", "Ask", "Niels", "Julian", "Alex", "Hans")]
        [InlineData("Bertil", "Buster", "Mason", "Rasmus", "Matti")]
        [InlineData("Jamie", "Gustav", "Leo", "Harald", "Samuel")]
        [InlineData("Anton")]
        [InlineData("Malik", "Hjalte", "Peter", "Adrian")]
        [InlineData("Eskild", "Milan", "Aske", "Frej", "Ebbe")]
        [InlineData("Kasper", "Svend", "Bastian", "Vitus", "Matheo")]
        public async Task TestMultipleMaleNames(params string[] names)
        {
            var client = ServiceProvider.GetRequiredService<GenderApiClient>();
            var response = await client.GetByMultipleNames(names);
            Assert.True(response.IsSuccess, $"IsSuccess == false | {response.Exception?.Message}");
            Assert.True(response.Result.All(r => r.GenderType == GenderType.Male), $"GenderTypes != Male ({string.Join(",",response.Result.Where(r => r.GenderType != GenderType.Male).Select(r => r.Name))})");
        }

        [Theory]
        [InlineData("Frank", "Thomas", "Jens", "Kim")]
        [InlineData("Eddie", "Theis", "Huxi", "Bille")]
        [InlineData("Anders", "Norr", "Elmer")]
        [InlineData("Linus", "Henry", "Atlas")]
        [InlineData("Asbjørn", "Valde", "Kian")]
        [InlineData("Marko", "Alvin", "Silas")]
        [InlineData("Lasse", "Herman", "Ask", "Niels", "Julian", "Alex", "Hans")]
        [InlineData("Bertil", "Buster", "Mason", "Rasmus", "Matti")]
        [InlineData("Jamie", "Gustav", "Leo", "Harald", "Samuel")]
        [InlineData("Anton")]
        [InlineData("Malik", "Hjalte", "Peter", "Adrian")]
        [InlineData("Eskild", "Milan", "Aske", "Frej", "Ebbe")]
        [InlineData("Kasper", "Svend", "Bastian", "Vitus", "Matheo")]
        public async Task TestMultipleMaleNamesFromDenmark(params string[] names)
        {
            var client = ServiceProvider.GetRequiredService<GenderApiClient>();
            var response = await client.GetByMultipleNamesAndCountryTypes(names, Enumerable.Repeat(CountryType.Denmark, names.Length));
            Assert.True(response.IsSuccess, $"IsSuccess == false | {response.Exception?.Message}");
            Assert.True(response.Result.All(r=>r.GenderType == GenderType.Male), $"GenderTypes != Male ({string.Join(",", response.Result.Where(r=>r.GenderType != GenderType.Male).Select(r=>r.Name))})");
        }

        [Theory]
        [InlineData("Nynne","Petra","Miriam","Fie")]
        [InlineData("Ebba","Katrine","Lara","Elisa","Vanessa")]
        [InlineData("Lucca","Sonja","Malia","Elva")]
        [InlineData("Elina","Julia","Uma","Ane","Johanna")]
        [InlineData("Jasmin","Ingeborg","Dicte","Amelia","Vilja")]
        [InlineData("Stella","Anne","Bella","Flora","Aura")]
        [InlineData("Amanda")]
        [InlineData("Lina","Mie")]
//        [InlineData("Lilje")] // reponse: unknown
        [InlineData("My")]
//        [InlineData("Bjørk")] // reponse: male
        [InlineData("Alice","Cecilie","Ava","Martha","Maggie")]
        [InlineData("Melina","Iben","Frederikke","Rose")]
        [InlineData("Mia","Melanie","Solveig","Sally")]
        public async Task TestMultipleFemaleNamesFromDenmark(params string[] names)
        {
            var client = ServiceProvider.GetRequiredService<GenderApiClient>();
            var response = await client.GetByMultipleNamesAndCountryTypes(names, Enumerable.Repeat(CountryType.Denmark, names.Length));
            Assert.True(response.IsSuccess, $"IsSuccess == false | {response.Exception?.Message}");
            Assert.True(response.Result.All(r => r.GenderType == GenderType.Female), $"GenderTypes != Female ({string.Join(",", response.Result.Where(r => r.GenderType != GenderType.Female).Select(r => r.Name))})");
        }

    }
}