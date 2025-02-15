using CommonLibrary.Models;
using DemoApplication.App.Employees.DTOs;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApplication.App.Employees.Queries
{
    public class GetEmployeeCompanyDetailsQuery : PagedRequest, IRequest<PaginatedDTO<EmployeeCompanyDetailsDTO>>
    {
        public required long EployeeCardNumber { get; set; }
    }
    public class GetEmployeeCompanyDetailsQueryValidator : AbstractValidator<GetEmployeeCompanyDetailsQuery>
    {
        public GetEmployeeCompanyDetailsQueryValidator()
        {
            RuleFor(e => e.EployeeCardNumber).NotNull().NotEmpty();
        }
    }
    public class GetEmployeeCompaniesWorkPermitQueryHandler : IRequestHandler<GetEmployeeCompanyDetailsQuery, PaginatedDTO<EmployeeCompanyDetailsDTO>>
    {
        public Task<PaginatedDTO<EmployeeCompanyDetailsDTO>> Handle(GetEmployeeCompanyDetailsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
