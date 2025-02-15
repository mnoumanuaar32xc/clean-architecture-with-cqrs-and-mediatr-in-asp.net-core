using Demo.Api.ApiFramework.Tools;
using DemoApplication.App.Authentication.Commands.LoginCommand;
using DemoApplication.App.Authentication.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Demo.Api.Controllers.v1.Authentication
{
    [ApiVersion("1")]
    [ApiExplorerSettings(GroupName = "[v1] => Authentication")]
    [Route("api/v{version:apiVersion}/v1/Authentication")]

    [Route("api/[controller]")]
    public class AuthenticationController : BaseController
    {
        public AuthenticationController()
        {
        }
        [HttpPost("Login")]
        [SwaggerOperation("Login by username and password")]
        [ProducesResponseType(typeof(ApiResult<LoginResponseDTO>), 200)]
        [AllowAnonymous]
        public virtual async Task<ApiResult<LoginResponseDTO>> LoginAsync([FromBody] LoginCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return new ApiResult<LoginResponseDTO>(result);
        }

    }
}
