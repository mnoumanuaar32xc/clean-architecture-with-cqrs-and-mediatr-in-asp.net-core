namespace DemoDomain.Exceptions
{
    public class ForbiddenException : BaseException
    {
        public ForbiddenException() : base()
        {
        }
        public ForbiddenException(string message) : base(message)
        {
        }
        public ForbiddenException(int apiErrorKey, string message) : base(apiErrorKey, message)
        {
        }
        public ForbiddenException(int apiErrorKey) : base(apiErrorKey)
        {
        }
        public ForbiddenException(List<int> apiErrorKeys) : base(apiErrorKeys)
        {
        }
    }
}
