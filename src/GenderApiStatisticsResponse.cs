using System;
using Newtonsoft.Json;

namespace MicroKnights.Gender_API
{
    public class GenderApiStatisticsResponse : GenderApiResponse
    {
        public GenderApiStatisticsResponse()
        {}

        public GenderApiStatisticsResponse(Exception exception) 
            : base(exception)
        {}

        public string Key { get; set; }

        [JsonProperty("is_limit_reached")]
        public bool IsLimitReached { get; set; }

        [JsonProperty("remaining_requests")]
        public int RemaningRequests { get; set; }

        [JsonProperty("amount_month_start")]
        public int AmountMonthStart { get; set; }

        [JsonProperty("amount_month_bought")]
        public int AmountMonthBought { get; set; }

        public string Duration { get; set; }
    }
}