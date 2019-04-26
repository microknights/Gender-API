using System;
using System.Collections.ObjectModel;

namespace MicroKnights.Gender_API
{
    public class GenderApiMultipleNamesResponse : GenderApiResponse
    {
        public GenderApiMultipleNamesResponse()
        {}

        public GenderApiMultipleNamesResponse(Exception exception) 
            : base(exception)
        {}

        /// <summary>
        /// Submitted name in lower case
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Collection of names
        /// </summary>
        public Collection<GenderApiEmailResponse> Result { get; set; }
    }
}