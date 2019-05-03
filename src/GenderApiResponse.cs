using System;

namespace MicroKnights.Gender_API
{
    public abstract class GenderApiResponse
    {
        protected GenderApiResponse()
        {
            IsSuccess = true;
        }

        protected GenderApiResponse(Exception exception)
        {
            Exception = exception;
            IsSuccess = exception == null;
        }

        public bool IsSuccess { get; }
        public Exception Exception { get; }
        public int ErrorCode => (Exception as GenderApiException)?.ErrorCode ?? 0;
    }
}