using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApplication.Common.Interfaces.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {

        /// <summary>
        /// Performs a non-transactional save operation to persist changes.
        /// This method should be used when you don't require a transaction for saving changes.
        /// This is commonly used when you implement a unitOfWork that use an ORM requires calling another method (ex:"SaveChanges") to execute the query.
        /// </summary>
        void Complete();

        /// <summary>
        /// Performs a non-transactional save operation to persist changes.
        /// This method should be used when you don't require a transaction for saving changes.
        /// This is commonly used when you implement a unitOfWork that use an ORM requires calling another method (ex:"SaveChangesAsync") to execute the query.
        /// </summary>
        Task CompleteAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Begins a new transaction for the unit of work.
        /// Throws an exception if a transaction is already in progress.
        /// </summary>
        /// <returns>The newly created transaction.</returns>
        IDbTransaction BeginTransaction();

        /// <summary>
        /// Commits the current transaction and persists the changes to the underlying data source.
        /// Throws an exception if no transaction is currently in progress.
        /// </summary>
        void CommitTransaction();


        /// <summary>
        /// Rolls back the current transaction, discarding any changes made within the transaction.
        /// Throws an exception if no transaction is currently in progress.
        /// </summary>
        void RollbackTransaction();
    }

}
