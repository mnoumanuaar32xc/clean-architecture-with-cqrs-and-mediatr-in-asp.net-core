namespace DemoDomain.Exceptions
{
    public class DecryptionException : BaseException
    {
        public DecryptionException() : base()
        {
        }
        public DecryptionException(string message) : base(message)
        {
        }
        public DecryptionException(int apiErrorKey, string message) : base(apiErrorKey, message)
        {
        }
        public DecryptionException(int apiErrorKey) : base(apiErrorKey)
        {
        }
        public DecryptionException(List<int> apiErrorKeys) : base(apiErrorKeys)
        {
        }
    }
}
