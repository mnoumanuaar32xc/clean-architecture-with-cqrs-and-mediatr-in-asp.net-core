using DemoApplication.Common.Helpers;
using DemoApplication.Common.Interfaces.IRepositories.User;
using DemoDomain.Entites.DemoApp.AppUserEntites;
using DemoInfrastructure.Persistence.Repositories.Main;
using DemoInfrastructure.Persistence.Repositories.Sql.Queries.User;
using SharedModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoInfrastructure.Persistence.Repositories.User
{
    internal class UserRepository : MainRepository, IUserRepository, IScopedDependency
    {
        public UserRepository(AppConnectionStringSettings appConnectionStr) : base(appConnectionStr)
        {
        }
        public UserRepository(IDbAccess? defaultDbAccess, IDbAccess? readOnlyDbAccess) : base(defaultDbAccess, readOnlyDbAccess)
        {
        }
        public async Task<UserLoginDetails?> ValidateLoginUserAsync(string userName, string password, CancellationToken cancellationToken = default)
        {
            return await ExecWithReadOnlyConnectionAsync(async dbAccess =>
            {
                object _params = new
                {
                    UserName = userName,
                    Password = password
                };

                var _result = await dbAccess.ExecuteSPAsync<UserLoginDetails?>(UsersQueries.ValidateLoginUser, _params, transaction: dbAccess.Transaction, cancellationToken: cancellationToken);

                return _result;

            }, cancellationToken);
        }
    }
}
