# Gender-API
C# impl. of https://gender-api.com

So far following methods are implemented:

```
Task<GenderApiNameResponse> GetByNameAndCountry2Alpha(string name, string country2AlphaCode)
Task<GenderApiNameResponse> GetByNameAndCountryType(string name, CountryType countryType)
Task<GenderApiNameResponse> GetByNameAndIp(string name, string ipAddresss)
Task<GenderApiNameResponse> GetByNameAndBrowserLanguageLocale(string name, string browserLanguageLocale)

Task<GenderApiMultipleNamesResponse> GetByMultipleNames(IEnumerable<string> names)
Task<GenderApiMultipleNamesResponse> GetByMultipleNamesAndCountries2Alpha(IEnumerable<string> names, IEnumerable<string> country2AlphaCodes)
Task<GenderApiMultipleNamesResponse> GetByMultipleNamesAndCountryTypes(IEnumerable<string> names, IEnumerable<CountryType> countryTypes)

Task<GenderApiEmailResponse> GetByEmail(string email)
Task<GenderApiEmailResponse> GetByEmailAndCountryType(string email, CountryType countryType)
Task<GenderApiEmailResponse> GetByEmailAndCountry2Alpha(string email, string country2AlphaCode)

Task<GenderApiNameResponse> GetByName(string name)
Task<GenderApiFullnameResponse> GetByFullname(string name, bool strict = false)
Task<GenderApiFullnameResponse> GetByFullnameAndCountry2Alpha(string name, string country2AlphaCode, bool strict = false)
Task<GenderApiFullnameResponse> GetByFullnameAndCountryType(string name, CountryType countryType, bool strict = false)

Task<GenderApiStatisticsResponse> GetStatistics()
```

For running, register at https://gender-api.com to get a `ApiKey` (free to some point).

The Test are using a UserSecret for `ApiKey`, under `Gender-API:ApiKey`.

# Initialize
At startup with `ServiceCollection`:
```
    services.UseGenderAPI("<ApiKey>");
```

and your code

```
    var client = ServiceProvider.GetRequiredService<GenderApiClient>();
    var response = await client.GetByNameAndCountryType(name, CountryType.Denmark);
```

# NuGet available
```
PM> Install-Package MicroKnights.Gender-API
```
