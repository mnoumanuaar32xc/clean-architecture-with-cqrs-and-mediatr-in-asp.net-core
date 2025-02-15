using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoInfrastructure.Persistence.Repositories.Sql.Queries.User
{
    [ExcludeFromCodeCoverage]
    public static class UsersQueries
    {
        public static string ValidateLoginUser => "[eTables].[dbo].[spValidateTwafuoqLoginUser]";
    }
}
