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

        protected virtual async Task<TResponse> ExecuteRequest<TResponse>(string method, IDictionary<string,object> parameters) where TResponse : GenderApiResponse
        {
            try
            {
                using (var client = _httpClientFactory.CreateClient(ConfigurationExtension.ServiceName))
                {
                    parameters.Add("key", client.DefaultRequestHeaders.Authorization.Parameter);
                    var urlParams = string.Join("&", parameters.Select(p => $"{p.Key}={GetUrlFormatted(p.Value.ToString())}"));
                    var jsonResult = await client.GetStringAsync($"{method}?{urlParams}");
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

        protected virtual string GetUrlFormatted(string str) => WebUtility.UrlEncode(str);

        public Task<GenderApiNameResponse> GetByNameAndCountry2Alpha(string name, string country2AlphaCode)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
            if (string.IsNullOrWhiteSpace(country2AlphaCode)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(country2AlphaCode));
            if (country2AlphaCode.Length != 2) throw new ArgumentOutOfRangeException(nameof(country2AlphaCode),"Value must have length of 2.");
            return ExecuteRequest<GenderApiNameResponse>("get", new Dictionary<string, object>
            {
                { "name", GetUrlFormatted(name) },
                { "country", country2AlphaCode }
            });
        }

        public Task<GenderApiNameResponse> GetByNameAndCountryType(string name, CountryType countryType)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
            if (countryType == null) throw new ArgumentNullException(nameof(countryType), "Value cannot be null.");
            return ExecuteRequest<GenderApiNameResponse>("get", new Dictionary<string, object>
            {
                { "name", GetUrlFormatted(name) },
                { "country", countryType.Alpha2Code }
            });
        }

        public Task<GenderApiMultipleNamesResponse> GetByMultipleNames(IEnumerable<string> names)
        {
            var namesArray = names as string[] ?? names?.ToArray();
            if (namesArray == null || namesArray.Any(string.IsNullOrWhiteSpace)) throw new ArgumentException("Value(s) cannot be null or whitespace.", nameof(names));
            return ExecuteRequest<GenderApiMultipleNamesResponse>("get", new Dictionary<string, object>
            {
                { "name", string.Join(";", namesArray.Select(GetUrlFormatted)) },
                { "multi", true },
            });
        }

        public Task<GenderApiMultipleNamesResponse> GetByMultipleNamesAndCountries2Alpha(IEnumerable<string> names, IEnumerable<string> country2AlphaCodes)
        {
            var namesArray = names as string[] ?? names?.ToArray();
            if (namesArray == null || namesArray.Any(string.IsNullOrWhiteSpace)) throw new ArgumentException("Value(s) cannot be null or whitespace.", nameof(names));
            var alphaCodesArray = country2AlphaCodes as string[] ?? country2AlphaCodes.ToArray();
            if (country2AlphaCodes == null || alphaCodesArray.Any(string.IsNullOrWhiteSpace)) throw new ArgumentException("Value(s) cannot be null or whitespace.", nameof(country2AlphaCodes));
            if( namesArray.Length != alphaCodesArray.Length) throw new ArgumentOutOfRangeException($"Misaligned length between paramters; {nameof(names)} and {nameof(country2AlphaCodes)}");
            return ExecuteRequest<GenderApiMultipleNamesResponse>("get", new Dictionary<string, object>
            {
                { "name", string.Join(";", namesArray.Select(GetUrlFormatted)) },
                { "country", string.Join(";", alphaCodesArray) },
                { "multi", true },
            });
        }

        public Task<GenderApiMultipleNamesResponse> GetByMultipleNamesAndCountryTypes(IEnumerable<string> names, IEnumerable<CountryType> countryTypes)
        {
            var namesArray = names as string[] ?? names?.ToArray();
            if (namesArray == null || namesArray.Any(string.IsNullOrWhiteSpace)) throw new ArgumentException("Value(s) cannot be null or whitespace.", nameof(names));
            var countryTypesArray = countryTypes as CountryType[] ?? countryTypes?.ToArray();
            if (countryTypesArray == null || countryTypesArray.Any(ct=>ct == null)) throw new ArgumentException("Value(s) cannot be null or whitespace.", nameof(countryTypes));
            if (namesArray.Length != countryTypesArray.Length) throw new ArgumentOutOfRangeException($"Misaligned length between paramters; {nameof(names)} and {nameof(countryTypes)}");
            return ExecuteRequest<GenderApiMultipleNamesResponse>("get", new Dictionary<string, object>
            {
                { "name", string.Join(";", namesArray.Select(GetUrlFormatted)) },
                { "country", string.Join(";", countryTypesArray.Select(ct=>ct.Alpha2Code)) },
                { "multi", true },
            });
        }

        public Task<GenderApiNameResponse> GetByNameAndIp(string name, string ipAddresss)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
            if (string.IsNullOrWhiteSpace(ipAddresss)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(ipAddresss));
            return ExecuteRequest<GenderApiNameResponse>("get", new Dictionary<string, object>
            {
                { "name", GetUrlFormatted(name) },
                { "ip", ipAddresss },
            });
        }

        public Task<GenderApiNameResponse> GetByNameAndBrowserLanguageLocale(string name, string browserLanguageLocale)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
            if (string.IsNullOrWhiteSpace(browserLanguageLocale)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(browserLanguageLocale));
            return ExecuteRequest<GenderApiNameResponse>("get", new Dictionary<string, object>
            {
                { "name", GetUrlFormatted(name) },
                { "country", browserLanguageLocale },
            });
        }

        public Task<GenderApiEmailResponse> GetByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(email));
            return ExecuteRequest<GenderApiEmailResponse>("get", new Dictionary<string, object>
            {
                { "email", GetUrlFormatted(email) },
            });
        }

        public Task<GenderApiEmailResponse> GetByEmailAndCountryType(string email, CountryType countryType)
        {
            if (countryType == null) throw new ArgumentNullException(nameof(countryType));
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(email));
            return ExecuteRequest<GenderApiEmailResponse>("get", new Dictionary<string, object>
            {
                { "email", GetUrlFormatted(email) },
                { "country", countryType.Alpha2Code },
            });
        }

        public Task<GenderApiEmailResponse> GetByEmailAndCountry2Alpha(string email, string country2AlphaCode)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(email));
            if (string.IsNullOrWhiteSpace(country2AlphaCode)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(country2AlphaCode));
            return ExecuteRequest<GenderApiEmailResponse>("get", new Dictionary<string, object>
            {
                { "email", GetUrlFormatted(email) },
                { "country", country2AlphaCode },
            });
        }

        public Task<GenderApiFullnameResponse> GetByFullname(string name, bool strict = false)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
            return ExecuteRequest<GenderApiFullnameResponse>("get", new Dictionary<string, object>
            {
                { "split", GetUrlFormatted(name) },
                { "strict", strict },
            });
        }

        public Task<GenderApiFullnameResponse> GetByFullnameAndCountry2Alpha(string name, string country2AlphaCode, bool strict = false)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
            return ExecuteRequest<GenderApiFullnameResponse>("get", new Dictionary<string, object>
            {
                { "split", GetUrlFormatted(name) },
                { "country", country2AlphaCode },
                { "strict", strict },
            });
        }

        public Task<GenderApiFullnameResponse> GetByFullnameAndCountryType(string name, CountryType countryType, bool strict = false)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
            return ExecuteRequest<GenderApiFullnameResponse>("get", new Dictionary<string, object>
            {
                { "split", GetUrlFormatted(name) },
                { "country", countryType.Alpha2Code },
                { "strict", strict },
            });
        }

        public Task<GenderApiStatisticsResponse> GetStatistics()
        {
            return ExecuteRequest<GenderApiStatisticsResponse>($"get-stats",new Dictionary<string, object>());
        }

    }
}