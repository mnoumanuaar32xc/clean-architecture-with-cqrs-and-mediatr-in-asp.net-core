namespace DemoDomain.Exceptions
{
    public class UploadException : BaseException
    {
        public UploadException() : base()
        {
        }
        public UploadException(string message) : base(message)
        {
        }
        public UploadException(int apiErrorKey, string message) : base(apiErrorKey, message)
        {
        }
        public UploadException(int apiErrorKey) : base(apiErrorKey)
        {
        }
        public UploadException(List<int> apiErrorKeys) : base(apiErrorKeys)
        {
        }
    }
}
