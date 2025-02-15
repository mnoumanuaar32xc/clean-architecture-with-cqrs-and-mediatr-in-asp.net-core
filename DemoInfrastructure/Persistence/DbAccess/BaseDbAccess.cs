using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoInfrastructure.Persistence.DbAccess
{
    public abstract class BaseDbAccess<TDBConnection> : IDisposable where TDBConnection : IDbConnection, new()
        // new() this constraint allow you to create instances of TDBConnection using new TDBConnection() 
    {
        public IDbTransaction? Transaction { get; private set; }
        public IDbConnection? Connection { get; private set; }

        public string? ConnectionString { get; set; }
        public string? ConnectionError { get; set; }
        public bool IsReadOnly { get; set; } = false;

        protected bool _disposed;

        public bool OpenConnection(IDbTransaction? transaction = null, bool isReadOnly = false)
        {
            return OpenConnectionAsync(transaction, isReadOnly).Result;
        }

        public async Task<bool> OpenConnectionAsync_(IDbTransaction? transaction = null, bool isReadOnly = false, CancellationToken cancellationToken = default)
        {
            var result = true;
            try
            {
                TDBConnection? _conn;
                if (transaction == null)
                {

                    var newConn = new TDBConnection
                    {
                        ConnectionString = ConnectionString
                    };

                    newConn.Open();
                    IsReadOnly = isReadOnly;
                    _conn = newConn;
                }
                else
                {
                    ConnectionString = transaction.Connection.ConnectionString;
                    Transaction = transaction;
                    IsReadOnly = false;
                    _conn = (TDBConnection?)transaction.Connection;
                }

                Connection = _conn;
            }
            catch (Exception e)
            {
                ConnectionError = e.Message;
                result = false;
            }

            return await Task.FromResult(result);
        }


        public async Task<bool> OpenConnectionAsync(IDbTransaction? transaction = null, bool isReadOnly = false, CancellationToken cancellationToken = default)
        {
            try
            {
                if (transaction == null)
                {
                    Connection = new TDBConnection
                    {
                        ConnectionString = ConnectionString
                    };

                    if (Connection is SqlConnection dbConnection)
                    {
                        await dbConnection.OpenAsync(cancellationToken);
                    }
                    else
                    if (Connection is OracleConnection oracleConnection)
                    {

                        await oracleConnection.OpenAsync(cancellationToken);
                    }
                    else
                    {
                        Connection.Open();
                    }
                    IsReadOnly = isReadOnly;
                    return true;
                }
                else
                {
                    Connection = transaction.Connection; // Assume it's already open if part of an existing transaction
                    Transaction = transaction;
                    IsReadOnly = false;
                    return true;
                }
            }
            catch (Exception e)
            {
                ConnectionError = e.Message;
                Connection?.Close();
                Connection?.Dispose();
                Connection = null;
                return false;
            }
        }



        public bool IsConnected()
        {
            return Connection != null && Connection.State != ConnectionState.Closed;
        }

        public bool SetDbTransaction(IDbTransaction? transaction)
        {
            Transaction = transaction;

            return true;
        }

        private void CheckDbConnectionValidity(bool checkIsValidForTransaction = false)
        {
            if (Connection == null)
                throw new InvalidOperationException("The DbAccess connection is not initiated.");

            if (checkIsValidForTransaction && IsReadOnly)
                throw new InvalidOperationException("The DbAccess connection is in read only mode, cannot use Transaction.");
        }

        public IDbTransaction BeginTransaction()
        {
            CheckDbConnectionValidity(true);

            if (Transaction != null)
            {
                throw new InvalidOperationException("A transaction is already in progress.");
            }

            var _transaction = Connection.BeginTransaction();

            SetDbTransaction(_transaction);

            return _transaction;
        }

        public void CommitTransaction()
        {
            CheckDbConnectionValidity(true);

            if (Transaction == null)
            {
                throw new InvalidOperationException("No transaction is currently in progress.");
            }

            try
            {
                Transaction.Commit();
            }
            catch
            {
                Transaction.Rollback();
                throw;
            }
            finally
            {
                Transaction.Dispose();
                SetDbTransaction(null);
            }
        }

        public void RollbackTransaction()
        {
            CheckDbConnectionValidity();

            if (Transaction == null)
            {
                throw new InvalidOperationException("No transaction is currently in progress.");
            }

            try
            {
                Transaction.Rollback();
            }
            finally
            {
                Transaction.Dispose();
                SetDbTransaction(null);
            }
        }

        protected void ResetDbAccess()
        {
            ConnectionError = null;
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
                    if (Transaction != null)
                    {
                        Transaction.Dispose();
                        Transaction = default;
                    }
                    if (Connection != null)
                    {
                        Connection.Close();
                        Connection.Dispose();
                        Connection = default;
                    }

                    ResetDbAccess();
                }
                _disposed = true;
            }
        }


        //The tilde character(~) in C# is used to denote a destructor for a class.
        //A destructor is a special method that is called automatically when an instance of the class is no longer in use or being destroyed.
        ~BaseDbAccess()
        {
            Dispose(false);
        }

    }
}
