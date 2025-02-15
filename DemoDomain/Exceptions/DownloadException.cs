namespace DemoDomain.Exceptions
{
    public class DownloadException : BaseException
    {
        public DownloadException() : base()
        {
        }
        public DownloadException(string message) : base(message)
        {
        }
        public DownloadException(int apiErrorKey, string message) : base(apiErrorKey, message)
        {
        }
        public DownloadException(int apiErrorKey) : base(apiErrorKey)
        {
        }
        public DownloadException(List<int> apiErrorKeys) : base(apiErrorKeys)
        {
        }
    }
}
