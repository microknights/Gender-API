using System;
using MicroKnights.Enumerations.Country;
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
        public string Name { get; set; }

        /// <summary>
        /// The name after we applied our normalizer to it
        /// </summary>
        [JsonProperty("Name_Sanitized")]
        public string NameSanitized { get; set; }

        /// <summary>
        /// Submitted country code
        /// </summary>
        public string Country { get; set; }

        public CountryType CountryType => CountryType.GetByAlpha2CodeOrDefault(Country, CountryType.Unknown);

        /// <summary>
        /// Possible values: male, female, unknown
        /// </summary>
        public string Gender { get; set; }

        [JsonIgnore]
        public GenderType GenderType => GenderType.ParseOrDefault(Gender, GenderType.Unknown);

        /// <summary>
        /// Number of records found in our database which match your request
        /// </summary>
        [Obsolete("Use Samples instead",false)]
        public int Sample
        {
            set => Samples = value;
            get => Samples; 
        }

        public int Samples { get; set; }

        /// <summary>
        /// This value determines the reliability of our database. A value of 100 means that the results on your gender request are 100% accurate
        /// </summary>
        public int Accuracy { get; set; }

        /// <summary>
        /// Time the server needed to process the request
        /// </summary>
        public string Duration { get; set; }

        /// <summary>
        /// The amount of credits used for this query
        /// </summary>
        [JsonProperty("Credits_Used")]
        public int CreditsUsed { get; set; }
    }
}