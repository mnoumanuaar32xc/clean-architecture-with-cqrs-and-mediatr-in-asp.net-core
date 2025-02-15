using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApplication.App.Employees.DTOs
{
    public class EmployeeCompanyDetailsDTO
    {
        public long? CompanyCode { get; set; }
        public string? CompanyNameEn { get; set; }
        public string? CompanyNameAr { get; set; }
        public string? PersonCode { get; set; }
        public long? CardNumber { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CardIssueDate { get; set; }
        public DateTime? CardExpiryDate { get; set; }
        public int? CardType { get; set; }
        public string? CardTypeNameEn { get; set; }
        public string? CardTypeNameAr { get; set; }
    }
}
