using DemoApplication.Common.Helpers;
using DemoInfrastructure.Persistence.DbAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoInfrastructure.Persistence.Repositories
{
    public class Repository : Repository<DapperDbAccess>
    {
        protected Repository(AppConnectionStringSettings appConnectionStr) : base(appConnectionStr)
        {
        }

        protected Repository(IDbAccess? defaultDbAccess, IDbAccess? readOnlyDbAccess) : base(defaultDbAccess, readOnlyDbAccess)
        {
        }
    }

    public class Repository<TDbAccess> where TDbAccess : IDbAccess, new()
    {

        #region ===[ Private Members ]=============================================================

        public IDbAccess? DefaultDbAccess { get; set; } = null;
        public IDbAccess? ReadOnlyDbAccess { get; set; } = null;

        // this mean that the repository will use one connection for all queries executions.
        protected bool UseOneConnection = false;

        protected AppConnectionStringSettings? _appConnectionStr = null;

        #endregion


        #region ===[ Constructor ]==================================================
        protected Repository(AppConnectionStringSettings appConnectionStr)
        {
            _appConnectionStr = appConnectionStr;
        }


        protected Repository(IDbAccess? defaultDbAccess, IDbAccess? readOnlyDbAccess)
        {
            SetConnections(defaultDbAccess, readOnlyDbAccess, true);
        }
        #endregion

        #region ===[ Repository Methods ]==================================================


        public void SetConnections(string defaulConn, string readOnlyConn, bool useOneConnection = false)
        {


            var defaultDbAccess = new TDbAccess()
            {
                ConnectionString = defaulConn
            };

            var readOnlyDbAccess = new TDbAccess()
            {
                ConnectionString = readOnlyConn,
                IsReadOnly = true
            };

            try
            {
                defaultDbAccess.OpenConnection();
            }
            catch (Exception e)
            {
                defaultDbAccess.ConnectionError = e.Message;
            }

            try
            {
                readOnlyDbAccess.OpenConnection();
            }
            catch (Exception e)
            {
                readOnlyDbAccess.ConnectionError = e.Message;
            }

            DefaultDbAccess = defaultDbAccess;
            ReadOnlyDbAccess = readOnlyDbAccess;

            UseOneConnection = useOneConnection;
        }


        public void SetConnections(IDbAccess? defaultDbAccess, IDbAccess? readOnlyDbAccess, bool useOneConnection = false)
        {

            DefaultDbAccess = defaultDbAccess;
            ReadOnlyDbAccess = readOnlyDbAccess;

            //ConnectionString = defaultConnection?.ConnectionString;
            //ReadOnlyConnectionString = readOnlyConnection?.ConnectionString;
            UseOneConnection = useOneConnection;
        }

        public IDbAccess GetDefaultDbAccess()
        {
            ValidateDbAccesses();

            if (DefaultDbAccess != null && DefaultDbAccess.IsConnected() && UseOneConnection)
                return DefaultDbAccess;

            var defaultDbAccess = new TDbAccess()
            {
                ConnectionString = DefaultDbAccess?.ConnectionString
            };

            try
            {
                defaultDbAccess.OpenConnection();
            }
            catch (Exception e)
            {
                defaultDbAccess.ConnectionError = e.Message;
                throw;
            }

            return defaultDbAccess;

        }

        public IDbAccess GetReadOnlyDbAccess()
        {
            ValidateDbAccesses();

            if (ReadOnlyDbAccess != null && ReadOnlyDbAccess.IsConnected() && UseOneConnection)
                return ReadOnlyDbAccess;

            var readOnlyDbAccess = new TDbAccess()
            {
                ConnectionString = ReadOnlyDbAccess?.ConnectionString,
                IsReadOnly = true
            };

            try
            {
                readOnlyDbAccess.OpenConnection();
            }
            catch (Exception e)
            {
                readOnlyDbAccess.ConnectionError = e.Message;
                throw;
            }

            return readOnlyDbAccess;

        }


        private bool ValidateDbAccesses(bool throwError = true)
        {
            bool isValid = true;

            if (DefaultDbAccess == null)
            {
                isValid = false;

                if (throwError)
                    throw new Exception(GetType() + " default db access is not set.");
            }

            if (ReadOnlyDbAccess == null)
            {
                isValid = false;

                if (throwError)
                    throw new Exception(GetType() + " readonly db access is not set.");
            }

            return isValid;
        }


        /// <summary>
        /// Executes the specified action with a default database connection (can write), automatically handling connection creation and disposal.
        /// </summary>
        /// <param name="action">The action to execute with the connection.</param>
        public void ExecWithDefaultConnection(Action<IDbAccess> action)
        {
            var _dbAccess = GetDefaultDbAccess();
            bool disposeConnection = !UseOneConnection;

            try
            {
                action(_dbAccess);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (disposeConnection)
                {
                    _dbAccess?.Dispose();
                }
            }
        }


        /// <summary>
        /// Executes the specified action with a default database connection (can write), automatically handling connection creation and disposal.
        /// </summary>
        /// <typeparam name="T">The type of the action result.</typeparam>
        /// <param name="action">The action to execute with the connection.</param>
        /// <returns>The result of the action.</returns>
        public T ExecWithDefaultConnection<T>(Func<IDbAccess, T> action)
        {
            var _dbAccess = GetDefaultDbAccess();
            bool disposeConnection = !UseOneConnection;

            try
            {
                return action(_dbAccess);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (disposeConnection)
                {
                    _dbAccess?.Dispose();
                }
            }
        }

        /// <summary>
        /// Executes the specified action with a read-only database connection, automatically handling connection creation and disposal.
        /// </summary>
        /// <param name="action">The action to execute with the connection.</param>
        public void ExecWithReadOnlyConnection(Action<IDbAccess> action)
        {
            var _dbAccess = GetReadOnlyDbAccess();
            bool disposeConnection = !UseOneConnection;

            try
            {
                action(_dbAccess);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (disposeConnection)
                {
                    _dbAccess?.Dispose();
                }
            }
        }


        /// <summary>
        /// Executes the specified action with a read-only database connection, automatically handling connection creation and disposal.
        /// </summary>
        /// <typeparam name="T">The type of the action result.</typeparam>
        /// <param name="action">The action to execute with the connection.</param>
        /// <returns>The result of the action.</returns>
        public T ExecWithReadOnlyConnection<T>(Func<IDbAccess, T> action)
        {
            var _dbAccess = GetReadOnlyDbAccess();
            bool disposeConnection = !UseOneConnection;

            try
            {
                return action(_dbAccess);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (disposeConnection)
                {
                    _dbAccess?.Dispose();
                }
            }
        }



        /// <summary>
        /// Executes the specified async action with a default database connection (can write), automatically handling connection creation and disposal.
        /// </summary>
        /// <typeparam name="T">The type of the action result.</typeparam>
        /// <param name="action">The async action to execute with the connection.</param>
        /// <returns>A task representing the asynchronous operation, yielding the result of the action.</returns>
        public async Task<T> ExecWithDefaultConnectionAsync<T>(Func<IDbAccess, Task<T>> action, CancellationToken cancellationToken = default)
        {
            var _dbAccess = GetDefaultDbAccess();
            bool disposeConnection = !UseOneConnection;

            try
            {
                return await action(_dbAccess);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (disposeConnection)
                {
                    _dbAccess?.Dispose();
                }
            }
        }

        /// <summary>
        /// Executes the specified async action with a default database connection (can write), automatically handling connection creation and disposal.
        /// </summary>
        /// <param name="action">The async action to execute with the connection.</param>
        /// <returns>A task representing the asynchronous operation. The task will be completed when the action finishes.</returns>
        public async Task ExecWithDefaultConnectionAsync(Func<IDbAccess, Task> action, CancellationToken cancellationToken = default)
        {
            var _dbAccess = GetDefaultDbAccess();
            bool disposeConnection = !UseOneConnection;

            try
            {
                await action(_dbAccess);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (disposeConnection)
                {
                    _dbAccess?.Dispose();
                }
            }
        }

        /// <summary>
        /// Executes the specified async action with a read-only database connection, automatically handling connection creation and disposal.
        /// </summary>
        /// <typeparam name="T">The type of the action result.</typeparam>
        /// <param name="action">The async action to execute with the connection.</param>
        /// <returns>A task representing the asynchronous operation, yielding the result of the action.</returns>
        public async Task<T> ExecWithReadOnlyConnectionAsync<T>(Func<IDbAccess, Task<T>> action, CancellationToken cancellationToken = default)
        {
            var _dbAccess = GetReadOnlyDbAccess();
            bool disposeConnection = !UseOneConnection;

            try
            {
                return await action(_dbAccess);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (disposeConnection)
                {
                    _dbAccess?.Dispose();
                }
            }
        }

        /// <summary>
        /// Executes the specified async action with a read-only database connection, automatically handling connection creation and disposal.
        /// </summary>
        /// <param name="action">The async action to execute with the connection.</param>
        /// <returns>A task representing the asynchronous operation. The task will be completed when the action finishes.</returns>
        public async Task ExecWithReadOnlyConnectionAsync(Func<IDbAccess, Task> action, CancellationToken cancellationToken = default)
        {
            var _dbAccess = GetReadOnlyDbAccess();
            bool disposeConnection = !UseOneConnection;

            try
            {
                await action(_dbAccess);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (disposeConnection)
                {
                    _dbAccess?.Dispose();
                }
            }
        }

        #endregion


    }
}
