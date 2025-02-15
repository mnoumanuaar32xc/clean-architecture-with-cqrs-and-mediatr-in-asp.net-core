using DemoDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoDomain.Entites.DemoApp.AppUserEntites
{
    public class AppUserIdentifiers : IEntity
    {
        public long AppUserId { get; set; }
        public string AppUserGuid { get; set; }
        public int IdentifierId { get; set; }
        public string IdentifierGuid { get; set; }
        public string IdentifierNameEn { get; set; }
        public string IdentifierNameAr { get; set; }
        public string IdentifierValue { get; set; }
    }
}
