using DemoDomain.Exceptions.Models;

namespace DemoDomain.Exceptions
{
    public class ParametersValidationException : BaseException
    {
        public List<ApiExceptionErrorModel> ValidationExceptionErrors { get; set; }

        public ParametersValidationException() : base()
        {
        }
        public ParametersValidationException(string message) : base(message)
        {
        }
        public ParametersValidationException(int apiErrorKey, string message) : base(apiErrorKey, message)
        {
        }
        public ParametersValidationException(int apiErrorKey) : base(apiErrorKey)
        {
        }
        public ParametersValidationException(List<int> apiErrorKeys) : base(apiErrorKeys)
        {
        } 
        public ParametersValidationException(List<ApiExceptionErrorModel> validationExceptionErrors)
        {
            ValidationExceptionErrors = validationExceptionErrors;
        }
    }
}
