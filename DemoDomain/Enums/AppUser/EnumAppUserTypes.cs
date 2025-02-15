using SharedModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoDomain.Enums.AppUser
{
    public class EnumAppUserTypes : Enumeration
    {
        public string AppUserTypeGuid { get; set; }
        public string AppUserTypeNameEn { get; }
        public string AppUserTypeNameAr { get; }
        private EnumAppUserTypes(int appUserTypeId, string appUserTypeName, string appUserTypeGuid, string appUserTypeNameEn, string appUserTypeNameAr) : base(appUserTypeId, appUserTypeName)
        {
            AppUserTypeGuid = appUserTypeGuid;
            AppUserTypeNameEn = appUserTypeNameEn;
            AppUserTypeNameAr = appUserTypeNameAr;
        }

        public static readonly EnumAppUserTypes SystemUser = new(1, "SystemUser", "0b41ebd0-7691-44de-93a0-07e8115ec986", "Ministry User", "مستخدم وزارة");
        public static readonly EnumAppUserTypes CallCenterAgent = new(2, "CallCenterAgent", "4c956fed-3385-4d1d-ab54-2396e0446d7d", "Call Center Agent", "موظف خدمة العملاء");
        public static readonly EnumAppUserTypes ShipCorpUser = new(3, "ShipCorpUser", "86ebad9d-3ac2-4e59-adcb-b24c068f9bb3", "Zones Corp User", "مستخدم المنطقة الحرة");
        public static readonly EnumAppUserTypes Supervisor = new(4, "Supervisor", "d40cfe7c-1cfd-4dd4-9b3e-a6f962228cce", "Supervisor", "مشرف");
        public static readonly EnumAppUserTypes Anonymous = new(5, "Anonymous", "41ae20d4-9d0c-423e-a634-92e31717d1e6", "Anonymous", "مجهول");

        public static IReadOnlyList<EnumAppUserTypes> GetList()
        {
            return GetAll<EnumAppUserTypes>().ToList();
        }
    }
}
