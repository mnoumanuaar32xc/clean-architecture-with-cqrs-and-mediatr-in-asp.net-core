using System.Collections.Concurrent;
using System.Data;
using System.Reflection;
using DemoApplication.Common.Interfaces.IRepositories;
using DemoApplication.Common.Interfaces.UnitOfWorks;
using DemoInfrastructure.Persistence.DbAccess;
using DemoInfrastructure.Persistence.Repositories;
using SharedModels;

namespace DemoInfrastructure.Persistence.UnitOfWorks
{
    public abstract class BaseUnitOfWork : BaseUnitOfWork<DapperDbAccess> { }

    public abstract class BaseUnitOfWork<TDbAccess> : IUnitOfWork where TDbAccess : IDbAccess, new()
    {
        public IDbAccess? DefaultDbAccess { get; set; } = null;
        public IDbAccess? ReadOnlyDbAccess { get; set; } = null;

        protected bool _disposed;

        protected ConcurrentDictionary<string, Repository<TDbAccess>> RepositoryList = new();



        protected void OpenDbConnections(string? defaultConnStr = null, string? readOnlyConnStr = null)
        {

            if (defaultConnStr.HasValue())
            {
                var defaultDbAccess = new TDbAccess()
                {
                    ConnectionString = defaultConnStr
                };

                try
                {
                    defaultDbAccess.OpenConnection();
                }
                catch (Exception e)
                {
                    defaultDbAccess.ConnectionError = e.Message;
                }

                DefaultDbAccess = defaultDbAccess;

            }

            if (readOnlyConnStr.HasValue())
            {
                var readOnlyDbAccess = new TDbAccess()
                {
                    ConnectionString = readOnlyConnStr,
                    IsReadOnly = true
                };

                try
                {
                    readOnlyDbAccess.OpenConnection();
                }
                catch (Exception e)
                {
                    readOnlyDbAccess.ConnectionError = e.Message;
                }

                ReadOnlyDbAccess = readOnlyDbAccess;
            }
        }


        protected void ResetRepositories()
        {

            PropertyInfo[] properties = GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (typeof(IRepository).IsAssignableFrom(property.PropertyType) || property.PropertyType.GetInterfaces().Any(i => i.IsAssignableFrom(typeof(IRepository))))
                {
                    property.SetValue(this, null);
                }
            }

            RepositoryList = new();
        }

        private void CheckDbAccessValidity(IDbAccess? dbAccess)
        {
            if (dbAccess?.Connection == null)
                throw new InvalidOperationException($"the dbAccess {GetType()} connection is not initiated.");
        }

        public void Complete()
        {
            CheckDbAccessValidity(DefaultDbAccess);

            if (DefaultDbAccess?.Transaction != null)
            {
                throw new InvalidOperationException("A transaction is in progress. Use CommitTransaction() instead.");
            }

            // Perform non-transactional save operation here
            // For example, in Entity Framework Core, you can use _dbContext.SaveChanges();
        }

        public async Task CompleteAsync(CancellationToken cancellationToken = default)
        {
            CheckDbAccessValidity(DefaultDbAccess);

            if (DefaultDbAccess?.Transaction != null)
            {
                throw new InvalidOperationException("A transaction is in progress. Use CommitTransaction() instead.");
            }

            // Perform non-transactional save operation here
            // For example, in Entity Framework Core, you can use _dbContext.SaveChanges();

            await Task.CompletedTask;
        }

        public IDbTransaction BeginTransaction()
        {
            CheckDbAccessValidity(DefaultDbAccess);

            if (DefaultDbAccess.Transaction != null)
            {
                throw new InvalidOperationException("A transaction is already in progress.");
            }

            var _transaction = DefaultDbAccess.BeginTransaction();

            foreach (var item in RepositoryList)
            {
                var _repo = item.Value;
                _repo.DefaultDbAccess?.SetDbTransaction(_transaction);
            }

            return _transaction;
        }

        public void CommitTransaction()
        {
            CheckDbAccessValidity(DefaultDbAccess);

            if (DefaultDbAccess.Transaction == null)
            {
                throw new InvalidOperationException("No transaction is currently in progress.");
            }

            try
            {
                DefaultDbAccess.CommitTransaction();
            }
            catch
            {
                DefaultDbAccess.RollbackTransaction();
                throw;
            }
            finally
            {
                ResetRepositories();
            }
        }

        public void RollbackTransaction()
        {
            CheckDbAccessValidity(DefaultDbAccess);

            if (DefaultDbAccess.Transaction == null)
            {
                throw new InvalidOperationException("No transaction is currently in progress.");
            }

            try
            {
                DefaultDbAccess.RollbackTransaction();
            }
            finally
            {
                ResetRepositories();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (DefaultDbAccess != null)
                    {
                        DefaultDbAccess.Dispose();
                        DefaultDbAccess = null;
                    }
                    if (ReadOnlyDbAccess != null)
                    {
                        ReadOnlyDbAccess.Dispose();
                        ReadOnlyDbAccess = null;
                    }

                    ResetRepositories();
                }
                _disposed = true;
            }
        }

        protected T CreateUnitOfWorkRepository<T>(IRepository? _currentRepo, IDbAccess? _defaultDbAccess, IDbAccess? _readOnlyDbAccess) where T : Repository<TDbAccess>
        {
            T _resultRepo;
            var _repoKey = typeof(T).Name.ToUpper();

            if (!RepositoryList.ContainsKey(_repoKey))
            {
                _resultRepo = (T)Activator.CreateInstance(typeof(T), _defaultDbAccess, _readOnlyDbAccess); //in case of non-transaction query
            }
            else
            {
                _resultRepo = (T)RepositoryList[_repoKey];
            }

            if (!RepositoryList.ContainsKey(_repoKey))
            {
                RepositoryList.TryAdd(_repoKey, _resultRepo);
            }

            return _resultRepo;
        }

        //The tilde character(~) in C# is used to denote a destructor for a class.
        //A destructor is a special method that is called automatically when an instance of the class is no longer in use or being destroyed.
        ~BaseUnitOfWork()
        {
            Dispose(false);
        }
    }
}
