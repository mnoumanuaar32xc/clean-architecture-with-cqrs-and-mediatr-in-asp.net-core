using DemoApplication.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoInfrastructure.Persistence.Repositories.Main
{
    public class MainRepository : Repository
    {

        #region ===[ Private Members ]=============================================================


        #endregion

        #region ===[ Constructor ]==================================================
        public MainRepository(AppConnectionStringSettings appConnectionStr) : base(appConnectionStr)
        {
            SetConnections(appConnectionStr.DefaultConnection, appConnectionStr.DefaultReadOnlyConnection);
        }

        public MainRepository(IDbAccess? defaultDbAccess, IDbAccess? readOnlyDbAccess) : base(defaultDbAccess, readOnlyDbAccess)
        {
        }

        #endregion

        #region ===[ Repository Methods ]==================================================

        #endregion
    }
}
