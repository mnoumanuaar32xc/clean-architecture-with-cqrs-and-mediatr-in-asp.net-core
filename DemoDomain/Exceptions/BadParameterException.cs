namespace DemoDomain.Exceptions
{
    public class BadParameterException : BaseException
    {
        public BadParameterException() : base()
        {
        }
        public BadParameterException(string message) : base(message)
        {
        }
        public BadParameterException(int apiErrorKey, string message) : base(apiErrorKey, message)
        {
        }
        public BadParameterException(int apiErrorKey) : base(apiErrorKey)
        {
        }
        public BadParameterException(List<int> apiErrorKeys) : base(apiErrorKeys)
        {
        }
    }
}
