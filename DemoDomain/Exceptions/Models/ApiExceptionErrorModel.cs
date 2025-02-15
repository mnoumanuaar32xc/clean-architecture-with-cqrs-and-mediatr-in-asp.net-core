namespace DemoDomain.Exceptions.Models
{
    public class ApiExceptionErrorModel
    {
        public int AppErrorKey { get; set; }
        public string PropertyName { get; set; }
        public List<ErrorsLang> Messages { get; set; }
    }
    public class ErrorsLang
    {
        public string LangCode { get; set; }
        public string Message { get; set; }
    }
    public class ErrorMessageTemplate
    {
        public string MessageTemplate { get; set; }
        public List<ErrorsLang> Messages { get; set; }
    }
}