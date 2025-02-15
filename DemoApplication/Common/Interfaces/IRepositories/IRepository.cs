using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApplication.Common.Interfaces.IRepositories
{
    public interface IRepository
    {

    }
    public interface IRepository<T> : IRepository where T : class
    {   
        //Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default);
        //Task<T> GetUserByIdAsync(long id, CancellationToken cancellationToken = default);
        //Task<string> AddAsync(T entity, CancellationToken cancellationToken = default);
        //Task<string> UpdateAsync(T entity, CancellationToken cancellationToken = default);
        //Task<string> DeleteAsync(long id, CancellationToken cancellationToken = default);
    }
}
