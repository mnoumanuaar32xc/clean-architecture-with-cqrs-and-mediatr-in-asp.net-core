namespace DemoDomain.Exceptions
{
    public class BussinessValidationException : BaseException
    {
        public BussinessValidationException() : base()
        {
        }
        public BussinessValidationException(string message) : base(message)
        {
        }
        public BussinessValidationException(int apiErrorKey, string message) : base(apiErrorKey, message)
        {
        }
        public BussinessValidationException(int apiErrorKey) : base(apiErrorKey)
        {
        }
        public BussinessValidationException(List<int> apiErrorKeys) : base(apiErrorKeys)
        {
        }

    }
}
