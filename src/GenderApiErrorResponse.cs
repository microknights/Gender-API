using Newtonsoft.Json;

namespace MicroKnights.Gender_API
{
    public class GenderApiErrorResponse 
    {
        [JsonProperty("errno")]
        public int ErrorCode { get; set; }

        [JsonProperty("errmsg")]
        public string ErrorMessage { get; set; }
    }
}