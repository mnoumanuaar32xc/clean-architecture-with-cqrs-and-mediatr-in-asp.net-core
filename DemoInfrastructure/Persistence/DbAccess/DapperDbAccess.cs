using CommonLibrary.Services.LoggerService;
using CommonLibrary.Services;
using SharedModels;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using Dapper;

namespace DemoInfrastructure.Persistence.DbAccess
{
    public class DapperDbAccess : DapperDbAccess<SqlConnection> { }
    public class DapperDbAccess<TDBConnection> : BaseDbAccess<TDBConnection>, IDbAccess where TDBConnection : IDbConnection, new()
    {
        public async Task<T?> ExecuteSPAsync<T>(string storedProcedureName, object? parameters = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
        {
            var dynamicParameters = GetParameters(parameters);

            try
            {
                CheckConnectionValidity();

                return await Connection.QueryFirstOrDefaultAsync<T>(
                    storedProcedureName,
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure,
                    transaction: transaction,
                    commandTimeout: 120 // Set a timeout if needed
                );
            }
            catch (Exception ex)
            {
                LogError(ex, storedProcedureName, dynamicParameters);
                throw ex;
            }
        }
        public async Task<IEnumerable<T>?> ExecuteSPWithTableAsync<T>(string storedProcedureName, object? parameters = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
        {
            var dynamicParameters = GetParameters(parameters);

            try
            {
                CheckConnectionValidity();

                return await Connection.QueryAsync<T>(
                    storedProcedureName,
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure,
                    transaction: transaction,
                    commandTimeout: 120 // Set a timeout if needed

                );
            }
            catch (Exception ex)
            {
                LogError(ex, storedProcedureName, dynamicParameters);
                throw ex;
            }
        }

        public async Task<dynamic?> ExecuteSPAsync(string storedProcedureName, object? parameters = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
        {

            var dynamicParameters = GetParameters(parameters);

            try
            {
                CheckConnectionValidity();

                return await Connection.QueryFirstOrDefaultAsync(
                    storedProcedureName,
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure,
                    transaction: transaction,
                    commandTimeout: 120 // Set a timeout if needed

                );
            }
            catch (Exception ex)
            {
                LogError(ex, storedProcedureName, dynamicParameters);

                throw;
            }
        }
        public async Task<T> ExecuteSPWithOutputAsync<T>(string storedProcedureName, string outputParameterName, SqlDbType outPutParamDbType = SqlDbType.NVarChar, int outPutParameterSize = 11, object? parameters = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
        {

            // Create DynamicParameters and add output parameter
            var dynamicParameters = GetParameters(parameters);
            dynamicParameters.Add(outputParameterName, dbType: MapSqlDbTypeToDbType(outPutParamDbType), size: outPutParameterSize, direction: ParameterDirection.Output);

            // this outside the try/catch as it has catching inside it, to not duplicate logging the same error.
            await ExecuteSPAsync<T>(storedProcedureName, dynamicParameters, transaction: transaction, cancellationToken: cancellationToken);

            try
            {
                // Retrieve the output parameter value
                if (dynamicParameters.ParameterNames.Contains(outputParameterName))
                {
                    return dynamicParameters.Get<T>(outputParameterName);
                }

                throw new InvalidOperationException($"Output parameter '{outputParameterName}' not found.");
            }
            catch (Exception ex)
            {
                LogError(ex, storedProcedureName, dynamicParameters);

                throw;
            }
        }
        public async Task<dynamic?> ExecuteSPWithUDTAsync(string storedProcedureName, IDictionary<string, DataTable> Udts, object? parameters = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
        {
            return await ExecuteSPWithUDTAsync<dynamic>(storedProcedureName, Udts, parameters, transaction, cancellationToken);
        }
        public async Task<T> ExecuteSPWithUDTAsync<T>(string storedProcedureName, IDictionary<string, DataTable> Udts, object? parameters = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
        {
            var dynamicParameters = GetParameters(parameters);
            try
            {
                CheckConnectionValidity();

                foreach (var item in Udts)
                {
                    dynamicParameters.Add("@" + item.Key, item.Value.AsTableValuedParameter(), DbType.Object);
                }

                var result = await Connection.QuerySingleOrDefaultAsync<T>(
                    storedProcedureName,
                    dynamicParameters,
                    transaction: transaction,
                    commandType: CommandType.StoredProcedure);

                return result;
            }
            catch (Exception ex)
            {
                LogError(ex, storedProcedureName, dynamicParameters);
                throw;
            }
        }
        public async Task<List<T>> ExecuteSPWithUDTWithTableAsync<T>(string storedProcedureName, IDictionary<string, DataTable> udts, object? parameters = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
        {
            // Get Dapper parameters from the provided object
            var dynamicParameters = GetParameters(parameters);
            try
            {
                // Validate database connection
                CheckConnectionValidity();
                // Validate date fields in DataTable
                // this function  just for testing 
                //ValidateDataTableForDates(udts);

                // Add UDTs to parameters
                foreach (var item in udts)
                {
                    dynamicParameters.Add("@" + item.Key, item.Value.AsTableValuedParameter(), DbType.Object);
                }

                // Execute the stored procedure expecting multiple results
                var result = await Connection.QueryAsync<T>(storedProcedureName, dynamicParameters, transaction: transaction, commandType: CommandType.StoredProcedure);

                // Convert the result to a List<T> and return it
                return result.ToList();
            }
            catch (Exception ex)
            {
                // Log the exception details for debugging purposes
                LogError(ex, storedProcedureName, dynamicParameters);
                throw;
            }
        }
        // this function  just for testing 
        //private void ValidateDataTableForDates(IDictionary<string, DataTable> udts)
        //{
        //    foreach (var table in udts.Values)
        //    {
        //        foreach (DataRow row in table.Rows)
        //        {
        //            foreach (DataColumn column in table.Columns)
        //            {
        //                // Log date values for debugging
        //                var dateValueStr = row[column]?.ToString();
        //                Console.WriteLine($"Column: {column.ColumnName}, Value: '{dateValueStr}'");

        //                if (column.DataType == typeof(DateTime) || column.DataType == typeof(DateTime?))
        //                {
        //                    if (row[column] != DBNull.Value)
        //                    {
        //                        DateTime dateValue;
        //                        if (!DateTime.TryParseExact(dateValueStr, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue))
        //                        {
        //                            throw new InvalidCastException($"Invalid date format in column '{column.ColumnName}'. Value: '{dateValueStr}'");
        //                        }
        //                        // Optionally reformat to ensure consistent format if needed
        //                        row[column] = dateValue.ToString("yyyy-MM-dd");
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
        public async Task<dynamic> ExecuteSPWithResultSetAsync(string storedProcedureName, object? parameters = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
        {
            var dynamicParameters = GetParameters(parameters);

            try
            {
                CheckConnectionValidity();

                using (var multi = await Connection.QueryMultipleAsync(
                    storedProcedureName,
                    dynamicParameters,
                    transaction: transaction,
                    commandType: CommandType.StoredProcedure))
                {
                    if (!multi.IsConsumed)
                    {
                        var result = new ExpandoObject() as IDictionary<string, object>;

                        int tableIndex = 1;
                        while (!multi.IsConsumed)
                        {
                            var tableName = $"Table{tableIndex}";
                            var listValue = await multi.ReadAsync();
                            result[tableName] = listValue;
                            tableIndex++;
                        }

                        return result;
                    }

                    throw new InvalidOperationException("No result found in the first result set.");
                }
            }
            catch (Exception ex)
            {
                LogError(ex, storedProcedureName, dynamicParameters);
                throw;
            }
        }
        public async Task<IEnumerable<T>?> QueryAsync<T>(string sql, object? parameters = null, IDbTransaction? transaction = null, int timeOutInSeconds = 120, CancellationToken cancellationToken = default)
        {

            var dynamicParameters = GetParameters(parameters);

            try
            {
                CheckConnectionValidity();

                return await Connection.QueryAsync<T>(
                    sql,
                    dynamicParameters,
                    transaction: transaction,
                    commandTimeout: timeOutInSeconds // Set a timeout if needed
                );
            }
            catch (Exception ex)
            {
                LogError(ex, sql, dynamicParameters);

                throw;
            }
        }
        public async Task<T?> QuerySingleOrDefaultAsync<T>(string sql, object? parameters = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
        {

            var dynamicParameters = GetParameters(parameters);

            try
            {
                CheckConnectionValidity();

                var queryTask = Task.Run(() =>
                {
                    return Connection.QuerySingleOrDefault<T>(
                        sql,
                        dynamicParameters,
                        transaction: transaction,
                        commandTimeout: 120 // Set a timeout if needed
                    );
                });

                var completedTask = await Task.WhenAny(queryTask, Task.Delay(-1, cancellationToken));

                if (completedTask == queryTask)
                {
                    return await queryTask;
                }
                else
                {
                    // Handle cancellation here if needed
                    throw new OperationCanceledException("QuerySingleOrDefaultAsync was canceled.");
                }
            }
            catch (Exception ex)
            {
                LogError(ex, sql, dynamicParameters);
                throw;
            }
        }
        public async Task<dynamic?> QuerySingleOrDefaultAsync(string sql, object? parameters = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
        {

            var dynamicParameters = GetParameters(parameters);

            try
            {
                CheckConnectionValidity();

                var queryTask = Task.Run(() =>
                {
                    return Connection.QuerySingleOrDefault(
                        sql,
                        dynamicParameters,
                        transaction: transaction,
                        commandTimeout: 120 // Set a timeout if needed
                    );
                });

                var completedTask = await Task.WhenAny(queryTask, Task.Delay(-1, cancellationToken));

                if (completedTask == queryTask)
                {
                    return await queryTask;
                }
                else
                {
                    // Handle cancellation here if needed
                    throw new OperationCanceledException("QuerySingleOrDefaultAsync was canceled.");
                }
            }
            catch (Exception ex)
            {
                LogError(ex, sql, dynamicParameters);
                throw;
            }
        }
        public async Task<int> ExecuteAsync(string sql, object? parameters = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
        {

            var dynamicParameters = GetParameters(parameters);

            try
            {
                CheckConnectionValidity();

                return await Connection.ExecuteAsync(
                    sql,
                    dynamicParameters,
                    transaction: transaction,
                    commandTimeout: 120 // Set a timeout if needed

                );
            }
            catch (Exception ex)
            {
                LogError(ex, sql, dynamicParameters);
                throw;
            }
        }
        private DynamicParameters GetParameters(object? parameters)
        {
            if (parameters != null && parameters is DynamicParameters) return (DynamicParameters)parameters;

            var dynamicParameters = new DynamicParameters();

            if (parameters != null)
            {

                if (parameters is ExpandoObject expandoObject)
                {
                    foreach (var property in expandoObject)
                    {
                        string paramName = "@" + property.Key;
                        var paramValue = property.Value;
                        dynamicParameters.Add(paramName, paramValue);
                    }
                }
                else
                {
                    Type type = parameters.GetType();
                    var props = type.GetProperties();

                    foreach (var prop in props)
                    {
                        string paramName = "@" + prop.Name;
                        var paramValue = prop.GetValue(parameters, null);
                        dynamicParameters.Add(paramName, paramValue);
                    }
                }
            }

            return dynamicParameters;
        }
        private void CheckConnectionValidity()
        {
            if (Connection == null)
            {
                throw new InvalidOperationException($"Connection for {GetType()} is not properly initialized.");
            }

            if (Connection.State != ConnectionState.Open)
            {
                try
                {
                    Connection.Open();
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Failed to open the database connection.", ex);
                }
            }
        }
        private static DbType MapSqlDbTypeToDbType(SqlDbType sqlDbType)
        {
            switch (sqlDbType)
            {
                case SqlDbType.Int:
                    return DbType.Int32;
                case SqlDbType.BigInt:
                    return DbType.Int64;
                case SqlDbType.SmallInt:
                    return DbType.Int16;
                case SqlDbType.TinyInt:
                    return DbType.Byte;
                case SqlDbType.Float:
                    return DbType.Double;
                case SqlDbType.Real:
                    return DbType.Single;
                case SqlDbType.Decimal:
                    return DbType.Decimal;
                case SqlDbType.DateTime:
                    return DbType.DateTime;
                case SqlDbType.NVarChar:
                    return DbType.String;
                case SqlDbType.VarChar:
                    return DbType.String;
                case SqlDbType.NChar:
                    return DbType.StringFixedLength;
                case SqlDbType.Char:
                    return DbType.AnsiStringFixedLength;
                case SqlDbType.UniqueIdentifier:
                    return DbType.Guid;
                // Add more mappings for other SqlDbType values as needed
                default:
                    throw new ArgumentException($"Mapping not found for SqlDbType: {sqlDbType}");
            }
        }
        private static void LogError(Exception ex, string sql, DynamicParameters? parameters = null)
        {
            try
            {
                var _logger = CommonServiceProvider.GetService<ILoggerService<DapperDbAccess>>();

                if (_logger != null)
                {
                    var _parametersDic = new Dictionary<string, object>();
                    if (parameters != null)
                    {
                        foreach (var parameterName in parameters.ParameterNames)
                        {
                            var parameterValue = parameters.Get<object>(parameterName);
                            _parametersDic.Add(parameterName, parameterValue);
                        }
                    }

                    var _errorInfo = new
                    {
                        QueryName = sql,
                        Paramters = _parametersDic
                    };

                    _logger.DBError(ex.Message, ex, _errorInfo.AsDictionary());
                }
            }
            catch (Exception exc1)
            {
                // Todo => will handle exception using log into file
            }
        }
    }
}
