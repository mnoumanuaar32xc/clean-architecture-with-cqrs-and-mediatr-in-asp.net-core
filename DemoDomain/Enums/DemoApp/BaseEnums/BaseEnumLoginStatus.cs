using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoDomain.Enums.DemoApp.BaseEnums
{
    public enum BaseEnumLoginStatus
    {
        WrongUsernameOrUserIsNotActive = -1,
        WrongPassword = -2,
        ValidUserLogin = 1
    }
}
