using System;
using Newtonsoft.Json;

namespace MicroKnights.Gender_API
{
    public class GenderApiEmailResponse : GenderApiNameResponse
    {
        public GenderApiEmailResponse()
        {}

        public GenderApiEmailResponse(Exception exception) 
            : base(exception)
        {}

        /// <summary>
        /// Submitted email address
        /// </summary>
        public string Email { get; set; }

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

        /// <summary>
        /// The last name found
        /// </summary>
        [JsonProperty("mail_provider")]
        public string MailProvider { get; set; }
    }
}