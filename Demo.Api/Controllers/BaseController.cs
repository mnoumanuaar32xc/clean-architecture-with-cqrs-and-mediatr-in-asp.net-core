using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Api.Controllers
{
    [ApiVersion("1")]
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {
        protected IServiceProvider Resolver => HttpContext.RequestServices;

        protected T GetService<T>()
        {
            return Resolver.GetService<T>();
        }

        protected IMapper Mapper => GetService<IMapper>();

        protected IMediator Mediator => GetService<IMediator>();

        protected ILogger Logger => GetService<ILogger>();
    }
}