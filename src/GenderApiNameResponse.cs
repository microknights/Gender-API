using System;
using Newtonsoft.Json;

namespace MicroKnights.Gender_API
{
    public class GenderApiNameResponse : GenderApiResponse
    {
        public GenderApiNameResponse()
        {}

        public GenderApiNameResponse(Exception exception) 
            : base(exception)
        {}

        /// <summary>
        /// Submitted name in lower case
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The name after we applied our normalizer to it
        /// </summary>
        [JsonProperty("Name_Sanitized")]
        public string NameSanitized { get; }

        /// <summary>
        /// Submitted country code
        /// </summary>
        public string Country { get; }

        public CountryType CountryType => CountryType.ParseOrDefault(Country, CountryType.Unknown);

        /// <summary>
        /// Possible values: male, female, unknown
        /// </summary>
        public string Gender { get; }

        [JsonIgnore]
        public GenderType GenderType => GenderType.ParseOrDefault(Gender, GenderType.Unkown);

        /// <summary>
        /// Number of records found in our database which match your request
        /// </summary>
        public int Sample { get; }

        /// <summary>
        /// This value determines the reliability of our database. A value of 100 means that the results on your gender request are 100% accurate
        /// </summary>
        public int Accuracy { get; }

        /// <summary>
        /// Time the server needed to process the request
        /// </summary>
        public string Duration { get; }

        /// <summary>
        /// The amount of credits used for this query
        /// </summary>
        [JsonProperty("Credits_Used")]
        public int CreditsUsed { get; }
    }
}