using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using MicroKnights.Enumerations.Country;
using Newtonsoft.Json;

namespace MicroKnights.Gender_API
{
    public class GenderApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GenderApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;

        }

        protected virtual async Task<TResponse> ExecuteRequest<TResponse>(string url) where TResponse : GenderApiResponse
        {
            try
            {
                using (var client = _httpClientFactory.CreateClient(ConfigurationExtension.ServiceName))
                {

                    var key = client.DefaultRequestHeaders.Authorization.Parameter;
                    var jsonResult = await client.GetStringAsync($"{url}&key={key}");
                    if (jsonResult.IndexOf("errno", StringComparison.InvariantCultureIgnoreCase) > 0)
                    {
                        var error = JsonConvert.DeserializeObject<GenderApiErrorResponse>(jsonResult);
                        return (TResponse)Activator.CreateInstance(typeof(TResponse),new GenderApiException(error.ErrorCode,error.ErrorMessage));
                    }
                    return JsonConvert.DeserializeObject<TResponse>(jsonResult);
                }

            }
            catch (Exception ex)
            {
                return (TResponse)Activator.CreateInstance(typeof(TResponse), new GenderApiException(ex));
            }
        }

        protected virtual string GetUrlFormatted(string str)
        {
            return WebUtility.UrlEncode(str);
        }

        public Task<GenderApiNameResponse> GetByNameAndCountry2Alpha(string name, string country2AlphaCode)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
            if (string.IsNullOrWhiteSpace(country2AlphaCode)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(country2AlphaCode));
            if (country2AlphaCode.Length != 2) throw new ArgumentOutOfRangeException(nameof(country2AlphaCode),"Value must have length of 2.");

            return ExecuteRequest<GenderApiNameResponse>($"get?name={GetUrlFormatted(name)}&country={country2AlphaCode}");
        }

        public Task<GenderApiNameResponse> GetByNameAndCountryType(string name, CountryType countryType)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
            if (countryType == null) throw new ArgumentNullException(nameof(countryType), "Value cannot be null.");
            return ExecuteRequest<GenderApiNameResponse>($"get?name={GetUrlFormatted(name)}&country={countryType.Alpha2Code}");
        }

        public Task<GenderApiMultipleNamesResponse> GetByMultipleNamesAndCountries2Alpha(IEnumerable<string> names, IEnumerable<string> country2AlphaCodes)
        {
            var namesArray = names as string[] ?? names?.ToArray();
            if (namesArray == null || namesArray.Any(string.IsNullOrWhiteSpace)) throw new ArgumentException("Value(s) cannot be null or whitespace.", nameof(names));
            var alphaCodesArray = country2AlphaCodes as string[] ?? country2AlphaCodes.ToArray();
            if (country2AlphaCodes == null || alphaCodesArray.Any(string.IsNullOrWhiteSpace)) throw new ArgumentException("Value(s) cannot be null or whitespace.", nameof(country2AlphaCodes));
            if( namesArray.Length != alphaCodesArray.Length) throw new ArgumentOutOfRangeException($"Misaligned length between paramters; {nameof(names)} and {nameof(country2AlphaCodes)}");
            return ExecuteRequest<GenderApiMultipleNamesResponse>($"get?name={string.Join(";", namesArray.Select(GetUrlFormatted))}&country={string.Join(";", alphaCodesArray)}&multi=true");
        }

        public Task<GenderApiMultipleNamesResponse> GetByMultipleNamesAndCountryTypes(IEnumerable<string> names, IEnumerable<CountryType> countryTypes)
        {
            var namesArray = names as string[] ?? names?.ToArray();
            if (namesArray == null || namesArray.Any(string.IsNullOrWhiteSpace)) throw new ArgumentException("Value(s) cannot be null or whitespace.", nameof(names));
            var countryTypesArray = countryTypes as CountryType[] ?? countryTypes?.ToArray();
            if (countryTypesArray == null || countryTypesArray.Any(ct=>ct == null)) throw new ArgumentException("Value(s) cannot be null or whitespace.", nameof(countryTypes));
            if (namesArray.Length != countryTypesArray.Length) throw new ArgumentOutOfRangeException($"Misaligned length between paramters; {nameof(names)} and {nameof(countryTypes)}");
            return ExecuteRequest<GenderApiMultipleNamesResponse>($"get?name={string.Join(";", namesArray.Select(GetUrlFormatted))}&country={string.Join(";", countryTypesArray.Select(ct=>ct.Alpha2Code))}&multi=true");
        }

        public Task<GenderApiNameResponse> GetByNameAndIp(string name, string ipAddresss)
        {
            return ExecuteRequest<GenderApiNameResponse>($"get?name={GetUrlFormatted(name)}&ip={ipAddresss}");
        }

        public Task<GenderApiNameResponse> GetByNameAndBrowserLanguageLocale(string name, string browserLanguageLocale)
        {
            return ExecuteRequest<GenderApiNameResponse>($"get?name={GetUrlFormatted(name)}&locale={browserLanguageLocale}");
        }

        public Task<GenderApiEmailResponse> GetByEmail(string email)
        {
            return ExecuteRequest<GenderApiEmailResponse>($"get?email={GetUrlFormatted(email)}");
        }

        public Task<GenderApiEmailResponse> GetByEmailAndCountryType(string email, CountryType countryType)
        {
            return ExecuteRequest<GenderApiEmailResponse>($"get?email={GetUrlFormatted(email)}&country={countryType.Alpha2Code}");
        }

        public Task<GenderApiEmailResponse> GetByEmailAndCountry2Alpha(string email, string country2AlphaCode)
        {
            return ExecuteRequest<GenderApiEmailResponse>($"get?email={GetUrlFormatted(email)}&country={country2AlphaCode}");
        }
    }
}