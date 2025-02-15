namespace DemoDomain.Exceptions
{
    public class SmsException : BaseException
    {
        public SmsException() : base()
        {
        }
        public SmsException(string message) : base(message)
        {
        }
        public SmsException(int apiErrorKey, string message) : base(apiErrorKey, message)
        {
        }
        public SmsException(int apiErrorKey) : base(apiErrorKey)
        {
        }
        public SmsException(List<int> apiErrorKeys) : base(apiErrorKeys)
        {
        }
    }
}
