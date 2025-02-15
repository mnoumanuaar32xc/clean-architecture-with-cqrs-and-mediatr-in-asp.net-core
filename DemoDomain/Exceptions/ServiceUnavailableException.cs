namespace DemoDomain.Exceptions
{
    public class ServiceUnavailableException : BaseException
    {
        public ServiceUnavailableException() : base()
        {
        }
        public ServiceUnavailableException(string message) : base(message)
        {
        }
        public ServiceUnavailableException(int apiErrorKey, string message) : base(apiErrorKey, message)
        {
        }
        public ServiceUnavailableException(int apiErrorKey) : base(apiErrorKey)
        {
        }
        public ServiceUnavailableException(List<int> apiErrorKeys) : base(apiErrorKeys)
        {
        }
    }
}
