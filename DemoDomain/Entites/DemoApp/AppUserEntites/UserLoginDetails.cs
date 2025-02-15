using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoDomain.Entites.DemoApp.AppUserEntites
{
    public class UserLoginDetails
    {
        public string UserId { get; set; }
        public string UserRole { get; set; }
        public int EmirateId { get; set; }
        public int ServiceCenterId { get; set; }
        public string UserCode { get; set; }
        public string UserFullName { get; set; }
        public string Response { get; set; } 
        public int UserLabourOfficeCode { get; set; }
        public bool? IsParentCenter { get; set; }
    }
}
