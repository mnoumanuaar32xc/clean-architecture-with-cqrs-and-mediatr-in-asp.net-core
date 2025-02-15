namespace DemoDomain.Exceptions
{
    public class BaseException : Exception
    {
        public int? ApiErrorKey { get; set; }
        public List<int> ApiErrorKeys { get; set; }
        public BaseException() : base()
        {
        }
        public BaseException(string message) : base(message)
        {
        }
        public BaseException(int apiErrorKey, string message)
        {
            ApiErrorKey = apiErrorKey;
        }
        public BaseException(int apiErrorKey)
        {
            ApiErrorKey = apiErrorKey;
        }
        public BaseException(List<int> apiErrorKeys)
        {
            ApiErrorKeys = apiErrorKeys;
        }
    }
}