namespace MovieCollectionAPI.Exceptions
{
    public class BaseException
    {
        public BaseException(int statusCode, string message, string? details)
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string? Details { get; set; }
    }
}
