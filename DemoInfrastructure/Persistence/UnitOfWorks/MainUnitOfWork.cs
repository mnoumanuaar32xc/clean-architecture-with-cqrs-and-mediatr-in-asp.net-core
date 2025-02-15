using DemoApplication.Common.Helpers;
using DemoApplication.Common.Interfaces.IRepositories;
using DemoApplication.Common.Interfaces.IRepositories.User;
using DemoApplication.Common.Interfaces.UnitOfWorks;
using DemoInfrastructure.Persistence.DbAccess;
using DemoInfrastructure.Persistence.Repositories;
using DemoInfrastructure.Persistence.Repositories.User;
using SharedModels;
using System.Collections.Concurrent;
using System.Data;
using System.Reflection;

namespace DemoInfrastructure.Persistence.UnitOfWorks
{
    public class MainUnitOfWork : BaseUnitOfWork, IMainUnitOfWork
    {
        public MainUnitOfWork(AppConnectionStringSettings appConnectionStr)
        {
            OpenDbConnections(appConnectionStr.DefaultConnection, appConnectionStr.DefaultReadOnlyConnection);

        }

        IUserRepository? _userRepository;
        public IUserRepository UserRepository
        {
            get
            {
                return CreateUnitOfWorkRepository<UserRepository>(_userRepository, DefaultDbAccess, ReadOnlyDbAccess);
            }
            set { _userRepository = value; }
        }

         
    }

}
