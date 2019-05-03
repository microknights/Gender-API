using System;
using Newtonsoft.Json;

namespace MicroKnights.Gender_API
{
    public class GenderApiEmailResponse : GenderApiFullnameResponse
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
        [JsonProperty("mail_provider")]
        public string MailProvider { get; set; }
    }
}