using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoInfrastructure.Persistence
{
    public interface IDbAccess : IDisposable
    {
        /// <summary>
        /// Gets the current database transaction, if one is in progress.
        /// </summary>
        IDbTransaction? Transaction { get; }

        /// <summary>
        /// Gets the current database connection, if it's open.
        /// </summary>
        IDbConnection? Connection { get; }

        /// <summary>
        /// Gets or sets the connection string used to connect to the database.
        /// </summary>
        string? ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets any error message related to the database connection.
        /// </summary>
        string? ConnectionError { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating whether the database access should be in read-only mode.
        /// </summary>
        bool IsReadOnly { get; set; }

        /// <summary>
        /// Opens a database connection asynchronously, optionally within a specified transaction and read-only mode.
        /// </summary>
        /// <param name="transaction">The transaction to use (default is null).</param>
        /// <param name="isReadOnly">Indicates whether the connection should be read-only (default is false).</param>
        /// <param name="cancellationToken">Cancellation token for async operation (default is default).</param>
        /// <returns>True if the connection was successfully opened; otherwise, false.</returns>
        Task<bool> OpenConnectionAsync(IDbTransaction? transaction = null, bool isReadOnly = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// Opens a database connection, optionally within a specified transaction and read-only mode.
        /// </summary>
        /// <param name="transaction">The transaction to use (default is null).</param>
        /// <param name="isReadOnly">Indicates whether the connection should be read-only (default is false).</param>
        /// <returns>True if the connection was successfully opened; otherwise, false.</returns>
        bool OpenConnection(IDbTransaction? transaction = null, bool isReadOnly = false);

        /// <summary>
        /// Checks if the database connection is open.
        /// </summary>
        /// <returns>True if the connection is open; otherwise, false.</returns>
        bool IsConnected();

        /// <summary>
        /// Sets the current database transaction.
        /// </summary>
        /// <param name="transaction">The transaction to set.</param>
        /// <returns>True if the transaction was successfully set; otherwise, false.</returns>
        bool SetDbTransaction(IDbTransaction? transaction);

        /// <summary>
        /// Begins a new transaction for the db access.
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

        /// <summary>
        /// Executes a stored procedure asynchronously and returns a result of type T.
        /// </summary>
        /// <typeparam name="T">The type of result to return.</typeparam>
        /// <param name="storedProcedureName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Optional parameters for the stored procedure.</param>
        /// <param name="transaction">The transaction to use (default is null).</param>
        /// <param name="cancellationToken">Cancellation token for async operation (default is default).</param>
        /// <returns>The result of type T from the stored procedure.</returns>
        Task<T?> ExecuteSPAsync<T>(string storedProcedureName, object? parameters = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Executes a stored procedure asynchronously and returns a dynamic result.
        /// </summary>
        /// <param name="storedProcedureName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Optional parameters for the stored procedure.</param>
        /// <param name="transaction">The transaction to use (default is null).</param>
        /// <param name="cancellationToken">Cancellation token for async operation (default is default).</param>
        /// <returns>A dynamic result from the stored procedure.</returns>
        Task<dynamic?> ExecuteSPAsync(string storedProcedureName, object? parameters = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Executes a stored procedure asynchronously and returns a dynamic result.
        /// </summary>
        /// <param name="storedProcedureName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Optional parameters for the stored procedure.</param>
        /// <param name="transaction">The transaction to use (default is null).</param>
        /// <param name="cancellationToken">Cancellation token for async operation (default is default).</param>
        /// <returns>A dynamic result from the stored procedure.</returns>
        Task<IEnumerable<T>?> ExecuteSPWithTableAsync<T>(string storedProcedureName, object? parameters = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Executes a stored procedure asynchronously with an output parameter and returns a result of type dynamic.
        /// </summary>
        /// <typeparam name="T">The type of result to return.</typeparam>
        /// <param name="storedProcedureName">The name of the stored procedure to execute.</param>
        /// <param name="outputParameterName">The name of the output parameter.</param>
        /// <param name="outPutParamDbType">The SqlDbType of the output parameter (default is SqlDbType.NVarChar).</param>
        /// <param name="outPutParameterSize">The size of the output parameter (default is 11).</param>
        /// <param name="parameters">Optional parameters for the stored procedure.</param>
        /// <param name="transaction">The transaction to use (default is null).</param>
        /// <param name="cancellationToken">Cancellation token for async operation (default is default).</param>
        /// <returns>The result of type T from the stored procedure.</returns>
        Task<T> ExecuteSPWithOutputAsync<T>(string storedProcedureName, string outputParameterName, SqlDbType outPutParamDbType = SqlDbType.NVarChar, int outPutParameterSize = 11, object? parameters = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Executes a stored procedure asynchronously with user-defined table types.
        /// </summary>
        /// <param name="storedProcedureName">The name of the stored procedure to execute.</param>
        /// <param name="Udts">A dictionary of user-defined table types and their corresponding DataTable objects.</param>
        /// <param name="parameters">Optional parameters for the stored procedure.</param>
        /// <param name="transaction">The transaction to use (default is null).</param>
        /// <param name="cancellationToken">Cancellation token for async operation (default is default).</param>
        /// <returns>A dynamic result from the stored procedure.</returns>
        Task<dynamic?> ExecuteSPWithUDTAsync(string storedProcedureName, IDictionary<string, DataTable> Udts, object? parameters = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Executes a stored procedure asynchronously with user-defined table types and returns a result of type T.
        /// </summary>
        /// <typeparam name="T">The type of result to return.</typeparam>
        /// <param name="storedProcedureName">The name of the stored procedure to execute.</param>
        /// <param name="Udts">A dictionary of user-defined table types and their corresponding DataTable objects.</param>
        /// <param name="parameters">Optional parameters for the stored procedure.</param>
        /// <param name="transaction">The transaction to use (default is null).</param>
        /// <param name="cancellationToken">Cancellation token for async operation (default is default).</param>
        /// <returns>The result of type T from the stored procedure.</returns>
        Task<T> ExecuteSPWithUDTAsync<T>(string storedProcedureName, IDictionary<string, DataTable> Udts, object? parameters = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default);
        /// <summary>
        /// Executes a stored procedure asynchronously that utilizes user-defined table types (UDTs) 
        /// and returns a list of results of type T.
        /// </summary>
        /// <typeparam name="T">The type of each element in the result list to return.</typeparam>
        /// <param name="storedProcedureName">The name of the stored procedure to execute. It should match exactly as defined in the database. </param>
        /// <param name="udts">
        /// A dictionary where the key is the name of the UDT parameter defined in the stored procedure,  and the value is a DataTable object containing the data to be passed to the stored procedure. </param>
        /// <param name="parameters"> Optional additional parameters required by the stored procedure. This should be an anonymous object  where properties correspond to parameter names in the stored procedure. </param>
        /// <param name="transaction"> An optional database transaction object. If provided, the command will be executed within the context  of this transaction.
        /// </param>
        /// <param name="cancellationToken">Cancellation token for async operation (default is default).</param>
        /// <returns> A task that represents the asynchronous operation. The task result contains a list of type <typeparamref name="T"/>  which includes the data retrieved by executing the stored procedure. </returns>

        Task<List<T>> ExecuteSPWithUDTWithTableAsync<T>(string storedProcedureName, IDictionary<string, DataTable> udts, object? parameters = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Executes a stored procedure using ADO.NET and returns a dynamic object containing multiple tables from the result dataset.
        /// </summary>
        /// <param name="storedProcedureName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Optional parameters to pass to the stored procedure.</param>
        /// <param name="transaction">An optional database transaction to use.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>A dynamic object containing tables with their respective data from the dataset.</returns>
        /// <remarks>
        /// The dynamic object contains properties like Table1, Table2, and so on, each representing a list of dynamic objects
        /// with the data from a table in the result dataset.
        /// </remarks>
        Task<dynamic> ExecuteSPWithResultSetAsync(string storedProcedureName, object? parameters = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Executes a SQL query asynchronously and returns a collection of results of type T.
        /// </summary>
        /// <typeparam name="T">The type of results to return.</typeparam>
        /// <param name="sql">The SQL query to execute.</param>
        /// <param name="parameters">Optional parameters for the SQL query.</param>
        /// <param name="transaction">The transaction to use (default is null).</param>
        /// <param name="cancellationToken">Cancellation token for async operation (default is default).</param>
        /// <returns>A collection of results of type T.</returns>
        Task<IEnumerable<T>?> QueryAsync<T>(string sql, object? parameters = null, IDbTransaction? transaction = null, int timeOutInSeconds = 60, CancellationToken cancellationToken = default);

        /// <summary>
        /// Executes a SQL query asynchronously and returns a single result of type T.
        /// </summary>
        /// <typeparam name="T">The type of result to return.</typeparam>
        /// <param name="sql">The SQL query to execute.</param>
        /// <param name="parameters">Optional parameters for the SQL query.</param>
        /// <param name="transaction">The transaction to use (default is null).</param>
        /// <param name="cancellationToken">Cancellation token for async operation (default is default).</param>
        /// <returns>A single result of type T.</returns>
        Task<T?> QuerySingleOrDefaultAsync<T>(string sql, object? parameters = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Executes a SQL query asynchronously and returns a single dynamic result.
        /// </summary>
        /// <param name="sql">The SQL query to execute.</param>
        /// <param name="parameters">Optional parameters for the SQL query.</param>
        /// <param name="transaction">The transaction to use (default is null).</param>
        /// <param name="cancellationToken">Cancellation token for async operation (default is default).</param>
        /// <returns>A single dynamic result.</returns>
        Task<dynamic?> QuerySingleOrDefaultAsync(string sql, object? parameters = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Executes a SQL command asynchronously and returns the number of affected rows.
        /// </summary>
        /// <param name="sql">The SQL command to execute.</param>
        /// <param name="parameters">Optional parameters for the SQL command.</param>
        /// <param name="transaction">The transaction to use (default is null).</param>
        /// <param name="cancellationToken">Cancellation token for async operation (default is default).</param>
        /// <returns>The number of affected rows.</returns>
        Task<int> ExecuteAsync(string sql, object? parameters = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default);
    }
}
