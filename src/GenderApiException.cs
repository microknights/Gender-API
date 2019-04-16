using System;

namespace MicroKnights.Gender_API
{
    public class GenderApiException : Exception
    {
        public GenderApiException(Exception innerException) 
            : base(innerException.Message, innerException)
        {}

        public GenderApiException(int code, string message) 
            : base(message)
        {
            ErrorCode = code;
        }

        public int ErrorCode { get; }
    }
}