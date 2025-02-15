namespace DemoDomain.Exceptions
{
    public class InvalidCredentialException : BaseException
    {
        public InvalidCredentialException() : base()
        {
        }
        public InvalidCredentialException(string message) : base(message)
        {
        }
        public InvalidCredentialException(int apiErrorKey, string message) : base(apiErrorKey, message)
        {
        }
        public InvalidCredentialException(int apiErrorKey) : base(apiErrorKey)
        {
        }
        public InvalidCredentialException(List<int> apiErrorKeys) : base(apiErrorKeys)
        {
        }
    }
}