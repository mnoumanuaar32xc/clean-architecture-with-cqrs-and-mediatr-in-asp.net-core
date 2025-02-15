namespace DemoDomain.Exceptions
{
    public class InvalidNullInputException : BaseException
    {
        public string PropertyName { get; set; }
        public InvalidNullInputException() : base()
        {
        }
        public InvalidNullInputException(string propertyName = null)  
        {
            PropertyName = propertyName;
        }
        public InvalidNullInputException(int apiErrorKey, string message) : base(apiErrorKey, message)
        {
        }
        public InvalidNullInputException(int apiErrorKey) : base(apiErrorKey)
        {
        }
        public InvalidNullInputException(List<int> apiErrorKeys) : base(apiErrorKeys)
        {
        }

    }
}
