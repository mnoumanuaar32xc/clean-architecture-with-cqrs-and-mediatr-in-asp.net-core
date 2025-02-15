namespace DemoDomain.Exceptions
{
    public class ProcessValidationException : BaseException
    {
        public ProcessValidationException() : base()
        {
        }
        public ProcessValidationException(string message) : base(message)
        {
        }
        public ProcessValidationException(int apiErrorKey, string message) : base(apiErrorKey, message)
        {
        }
        public ProcessValidationException(int apiErrorKey) : base(apiErrorKey)
        {
        }
        public ProcessValidationException(List<int> apiErrorKeys) : base(apiErrorKeys)
        {
        }
    }
}
