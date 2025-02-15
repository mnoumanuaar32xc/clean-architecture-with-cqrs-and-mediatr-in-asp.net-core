namespace DemoDomain.Exceptions
{
    public class SqlErrorException : BaseException
    {
        public SqlErrorException() : base()
        {
        }
        public SqlErrorException(string message) : base(message)
        {
        }
        public SqlErrorException(int apiErrorKey, string message) : base(apiErrorKey, message)
        {
        }
        public SqlErrorException(int apiErrorKey) : base(apiErrorKey)
        {
        }
        public SqlErrorException(List<int> apiErrorKeys) : base(apiErrorKeys)
        {
        }
    }
}
