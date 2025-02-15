using DemoApplication.Common.Interfaces.IRepositories.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApplication.Common.Interfaces.UnitOfWorks
{
    public interface IMainUnitOfWork : IUnitOfWork
    {
        IUserRepository UserRepository { get; }
    }
}
