using DemoDomain.Exceptions.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Api.ApiFramework.Tools
{
    public class ApiResult : ApiResult<object>
    {
        public ApiResult(object data, int statusCode = StatusCodes.Status200OK, List<ApiExceptionErrorModel>? errors = null) : base(data, statusCode, errors)
        {

        }
    }

    public class ApiResult<T> : IActionResult, IDisposable, IStatusCodeActionResult
    {
        public List<ApiExceptionErrorModel>? Errors { get; set; }
        public T Data { get; set; }
        public int? StatusCode { get; set; }
        public ApiResult(T data, int statusCode = StatusCodes.Status200OK, List<ApiExceptionErrorModel>? errors = null)
        {
            Data = data;
            StatusCode = statusCode;
            Errors = errors;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            await new OkObjectResult(this).ExecuteResultAsync(context);
        }

        public void Dispose()
        {
            if (Data != null && typeof(T).GetInterfaces().Contains(typeof(IDisposable)))
            {
                ((IDisposable)Data).Dispose();
            }
        }
    }
}
