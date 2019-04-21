using System;
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
            return ExecuteRequest<GenderApiNameResponse>($"get?name={GetUrlFormatted(name)}&country={country2AlphaCode}");
        }

        public Task<GenderApiNameResponse> GetByNameAndCountryType(string name, CountryType countryType)
        {
            return ExecuteRequest<GenderApiNameResponse>($"get?name={GetUrlFormatted(name)}&country={countryType.Alpha2Code}");
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