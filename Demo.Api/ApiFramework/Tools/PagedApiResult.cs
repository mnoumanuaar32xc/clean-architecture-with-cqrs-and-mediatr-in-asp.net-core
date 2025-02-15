using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Api.ApiFramework.Tools
{
    public class PagedApiResult : PagedApiResult<object>
    {
        public PagedApiResult(object data, int? total = null, int statusCode = StatusCodes.Status200OK, string[] messages = null, string[] errors = null) : base(data, total, statusCode, messages, errors)
        {

        }
    }

    public class PagedApiResult<T> : IActionResult, IDisposable, IStatusCodeActionResult
    {
        public string[] Errors { get; set; }

        public PagedApiData<T> Data { get; set; }

        public string[] Messages { get; set; }

        public int? StatusCode { get; set; }

        public PagedApiResult(T data, int? total = null, int statusCode = StatusCodes.Status200OK, string[] messages = null, string[] errors = null)
        {
            Data = new PagedApiData<T> { List = data, Total = total };
            StatusCode = statusCode;
            Messages = messages;
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

    public class PagedApiData<T>
    {
        public T List { get; set; }
        public int? Total { get; set; }
    }
}
