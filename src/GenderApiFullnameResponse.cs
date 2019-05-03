using System;
using Newtonsoft.Json;

namespace MicroKnights.Gender_API
{
    public class GenderApiFullnameResponse : GenderApiNameResponse
    {
        public GenderApiFullnameResponse()
        {}

        public GenderApiFullnameResponse(Exception exception) 
            : base(exception)
        {}

        /// <summary>
        /// The last name found
        /// </summary>
        [JsonProperty("last_name")]
        public string LastName { get; set; }

        /// <summary>
        /// The last name found
        /// </summary>
        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        public Boolean Strict { get; set; }
    }
}