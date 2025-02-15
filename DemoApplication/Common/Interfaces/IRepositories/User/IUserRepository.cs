using DemoDomain.Entites.DemoApp.AppUserEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApplication.Common.Interfaces.IRepositories.User
{
    public interface IUserRepository : IRepository
    {
        Task<UserLoginDetails?> ValidateLoginUserAsync(string userName, string password, CancellationToken cancellationToken = default);

    }
}
