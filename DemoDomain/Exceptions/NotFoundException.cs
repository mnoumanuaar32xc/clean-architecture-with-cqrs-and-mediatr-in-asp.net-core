namespace DemoDomain.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException() : base()
        {
        }
        public NotFoundException(string message) : base(message)
        {
        }
        public NotFoundException(int apiErrorKey, string message) : base(apiErrorKey, message)
        {
        }
        public NotFoundException(int apiErrorKey) : base(apiErrorKey)
        {
        }
        public NotFoundException(List<int> apiErrorKeys) : base(apiErrorKeys)
        {
        }
    }
}
