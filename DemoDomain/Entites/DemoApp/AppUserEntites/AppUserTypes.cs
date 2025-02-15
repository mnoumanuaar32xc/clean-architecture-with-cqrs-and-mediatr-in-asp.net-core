using DemoDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DemoDomain.Entites.DemoApp.AppUserEntites
{
    public class AppUserTypes : IEntity
    {
        public long AppUserId { get; set; }
        public string AppUserGuid { get; set; }
        public int AppUserTypeId { get; set; }
        public string AppUserTypeGuid { get; set; }
        public string AppUserTypeNameEn { get; set; }
        public string AppUserTypeNameAr { get; set; }
    }
}
