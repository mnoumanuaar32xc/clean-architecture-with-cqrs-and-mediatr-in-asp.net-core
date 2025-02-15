using DemoDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoDomain.Entites.DemoApp.AppUserEntites
{
    public class AppUserPermissions : IEntity
    {
        public long AppUserId { get; set; }
        public string AppUserGuid { get; set; }
        public int PermissionId { get; set; }
        public string PermissionGuid { get; set; }
        public string PermissionCode { get; set; }
        public string PermissionNameEn { get; set; }
        public string PermissionNameAr { get; set; }
        public int PermissionGroupId { get; set; }
        public string PermissionGroupGuid { get; set; }
        public string PermissionGroupEn { get; set; }
        public string PermissionGroupAr { get; set; }
        public int ModuleId { get; set; }
        public string ModuleGuid { get; set; }
        public string ModuleNameEn { get; set; }
        public string ModuleNameAr { get; set; }
        public bool UserHasPermission { get; set; }
    }
}
