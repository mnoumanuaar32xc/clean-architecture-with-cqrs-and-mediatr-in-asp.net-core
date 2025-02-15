using Demo.Api.ApiFramework.Tools;
using DemoApplication.App.Employees.DTOs;
using DemoApplication.App.Employees.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Demo.Api.Controllers.v1.Employees
{
    [ApiVersion("1")]
    [ApiExplorerSettings(GroupName = "[v1] => Employees")]
    [Route("api/v{version:apiVersion}/v1/Employees")]

    public class EmployeesController : BaseController
    {
        [HttpGet("GetEmployeeCompanyDetails")]
        [SwaggerOperation("Get Employee Company Details")]
        [ProducesResponseType(typeof(PagedApiResult<List<EmployeeCompanyDetailsDTO>>), 200)]
        public virtual async Task<PagedApiResult<List<EmployeeCompanyDetailsDTO>>> GetEmployeeCompanyDetailsAsync(GetEmployeeCompanyDetailsQuery query, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(query, cancellationToken).ConfigureAwait(false);

            return new PagedApiResult<List<EmployeeCompanyDetailsDTO>>(result.Items, result.TotalRecords);
        }
    }
}
