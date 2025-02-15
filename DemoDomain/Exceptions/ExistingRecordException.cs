namespace DemoDomain.Exceptions
{
    public class ExistingRecordException : BaseException
    {
        public ExistingRecordException() : base()
        {
        }
        public ExistingRecordException(string message) : base(message)
        {
        }
        public ExistingRecordException(int apiErrorKey, string message) : base(apiErrorKey, message)
        {
        }
        public ExistingRecordException(int apiErrorKey) : base(apiErrorKey)
        {
        }
        public ExistingRecordException(List<int> apiErrorKeys) : base(apiErrorKeys)
        {
        }
    }
}
