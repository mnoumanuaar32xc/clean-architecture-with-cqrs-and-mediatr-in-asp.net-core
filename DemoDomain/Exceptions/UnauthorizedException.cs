namespace DemoDomain.Exceptions
{
    public class UnauthorizedException : BaseException
    {
        public UnauthorizedException() : base()
        {
        }
        public UnauthorizedException(string message) : base(message)
        {
        }
        public UnauthorizedException(int apiErrorKey, string message) : base(apiErrorKey, message)
        {
        }
        public UnauthorizedException(int apiErrorKey) : base(apiErrorKey)
        {
        }
        public UnauthorizedException(List<int> apiErrorKeys) : base(apiErrorKeys)
        {
        }
    }
}
