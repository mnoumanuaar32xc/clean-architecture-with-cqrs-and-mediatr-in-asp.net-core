using DemoDomain.Interfaces;

namespace DemoDomain.Entites.DemoApp.AppUserEntites
{
    public class AppUserProfile : IEntity
    {
        public long AppUserId { get; set; }
        public string AppUserGuid { get; set; }
        public string AppUserNameAr { get; set; }
        public string AppUserNameEn { get; set; }
        public string AppUserPassword { get; set; }
        public string AppUserPasswordSalt { get; set; }
        public bool IsNeedToChangePassword { get; set; }
        public string AppUserTypes { get; set; }
        public string AppUserIdentifiers { get; set; }
        public string AppUserPermissions { get; set; }
        public string AppUserEmiratesAccess { get; set; }
    }
}
