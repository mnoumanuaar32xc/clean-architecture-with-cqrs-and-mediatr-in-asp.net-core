namespace DemoDomain.Exceptions
{
    public class NoContentException : BaseException
    {
        public NoContentException() : base()
        {
        }
        public NoContentException(string message) : base(message)
        {
        }
        public NoContentException(int apiErrorKey, string message) : base(apiErrorKey, message)
        {
        }
        public NoContentException(int apiErrorKey) : base(apiErrorKey)
        {
        }
        public NoContentException(List<int> apiErrorKeys) : base(apiErrorKeys)
        {
        }
    }
}
