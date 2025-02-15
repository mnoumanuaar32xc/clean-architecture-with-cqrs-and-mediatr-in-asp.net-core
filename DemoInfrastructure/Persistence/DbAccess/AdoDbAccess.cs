using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoInfrastructure.Persistence.DbAccess
{
    public class AdoDbAccess : AdoDbAccess<SqlConnection> { }
    public class AdoDbAccess<TDBConnection> : BaseDbAccess<TDBConnection>/*, IDbAccess*/ where TDBConnection : IDbConnection, new()
    {

        public async Task<T?> ExecuteSPAsync<T>(string storedProcedureName, object? parameters = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
        {

            CheckConnectionValidity();

            using (var command = Connection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = transaction;
                command.CommandText = storedProcedureName;

                if (parameters != null)
                {
                    foreach (var param in GetParameters(parameters))
                    {
                        command.Parameters.Add(param);
                    }
                }

                return await Task.Run(() =>
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var value = reader[0];
                            return (T?)(value == DBNull.Value ? default(T) : value);
                        }
                        else
                        {
                            return default;
                        }
                    }
                });
            }
        }

        public async Task<dynamic?> ExecuteSPAsync(string storedProcedureName, object? parameters = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
        {

            CheckConnectionValidity();

            using (var command = Connection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = transaction;
                command.CommandText = storedProcedureName;

                if (parameters != null)
                {
                    foreach (var param in GetParameters(parameters))
                    {
                        command.Parameters.Add(param);
                    }
                }

                return await Task.Run(() =>
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var value = reader[0];
                            return (value == DBNull.Value ? default : value);
                        }
                        else
                        {
                            return default;
                        }
                    }
                });
            }
        }

        public async Task<dynamic?> ExecuteSPWithUDTAsync(string storedProcedureName, IDictionary<string, DataTable> Udts, object? parameters = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
        {
            return await ExecuteSPWithUDTAsync<dynamic>(storedProcedureName, Udts, parameters, transaction, cancellationToken);
        }

        public async Task<T> ExecuteSPWithUDTAsync<T>(string storedProcedureName, IDictionary<string, DataTable> Udts, object? parameters = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
        {
            using (var cmd = Connection.CreateCommand())
            {
                cmd.Transaction = transaction;
                cmd.CommandText = storedProcedureName;
                cmd.CommandType = CommandType.StoredProcedure;

                // Add UDT parameter
                foreach (var item in Udts)
                {
                    var udtParam = new SqlParameter(item.Key, SqlDbType.Structured);
                    udtParam.Value = item.Value;

                    cmd.Parameters.Add(udtParam);
                }

                // Add other parameters if needed
                if (parameters != null)
                {
                    foreach (var param in GetParameters(parameters))
                    {
                        cmd.Parameters.Add(param);
                    }
                }

                // Execute the stored procedure
                var result = cmd.ExecuteScalar();

                // Handle the result as needed and cast it to the appropriate type
                return (T)result;
            }
        }

        public async Task<dynamic> ExecuteSPWithResultSetAsync(string storedProcedureName, object? parameters = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
        {
            var dynamicParameters = GetParameters(parameters);

            try
            {
                CheckConnectionValidity();

                using (var command = Connection.CreateCommand())
                {
                    command.CommandText = storedProcedureName;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Transaction = transaction;
                    command.CommandTimeout = 60; // Set a timeout if needed

                    if (parameters != null)
                    {
                        foreach (var param in GetParameters(parameters))
                        {
                            command.Parameters.Add(param);
                        }
                    }

                    using (var adapter = new SqlDataAdapter((SqlCommand)command))
                    {
                        var dataSet = new DataSet();
                        adapter.Fill(dataSet);

                        var result = new ExpandoObject() as IDictionary<string, object>;

                        for (int i = 0; i < dataSet.Tables.Count; i++)
                        {
                            var tableName = $"Table{i + 1}";
                            var dataTable = dataSet.Tables[i];
                            var list = new List<dynamic>();

                            foreach (DataRow row in dataTable.Rows)
                            {
                                var item = new ExpandoObject() as IDictionary<string, object>;
                                foreach (DataColumn col in dataTable.Columns)
                                {
                                    item[col.ColumnName] = row[col];
                                }
                                list.Add(item);
                            }

                            result[tableName] = list;
                        }

                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<T>?> QueryAsync<T>(string sql, object? parameters = null, IDbTransaction? transaction = null, int timeOutInSeconds = 60, CancellationToken cancellationToken = default)
        {
            CheckConnectionValidity();

            using (var command = Connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.Transaction = transaction;
                command.CommandText = sql;
                command.CommandTimeout = timeOutInSeconds;

                if (parameters != null)
                {
                    foreach (var param in GetParameters(parameters))
                    {
                        command.Parameters.Add(param);
                    }
                }


                return await Task.Run(() =>
                {
                    var resultList = new List<T>();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var value = reader[0];

                            if (value != DBNull.Value)
                                resultList.Add((T)value);
                        }
                    }

                    return resultList;
                });
            }
        }

        public async Task<int> ExecuteAsync(string sql, object? parameters = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
        {
            CheckConnectionValidity();
            using (var command = Connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.Transaction = transaction;
                command.CommandText = sql;

                if (parameters != null)
                {
                    foreach (var param in GetParameters(parameters))
                    {
                        command.Parameters.Add(param);
                    }
                }

                return await Task.Run(() => command.ExecuteNonQuery());
            }
        }


        public async Task<T> ExecuteSPWithOutputAsync<T>(string storedProcedureName, string outputParamName, SqlDbType outPutParamDbType = SqlDbType.NVarChar, int outPutParamSize = 11, object? parameters = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
        {

            CheckConnectionValidity();

            using (var command = Connection.CreateCommand())
            {
                command.CommandText = storedProcedureName;
                command.Transaction = transaction;
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = transaction as SqlTransaction;

                if (parameters != null)
                {
                    foreach (var parameter in GetParameters(parameters))
                    {
                        command.Parameters.Add(parameter);
                    }
                }

                var outputParam = new SqlParameter(outputParamName, outPutParamDbType, outPutParamSize);
                outputParam.Direction = ParameterDirection.Output;
                command.Parameters.Add(outputParam);


                try
                {
                    command.ExecuteNonQuery();

                    if (outputParam.Value != DBNull.Value)
                    {
                        return (T)Convert.ChangeType(outputParam.Value, typeof(T));
                    }

                    throw new InvalidOperationException($"Output parameter '{outputParamName}' not found or is DBNull.");
                }
                catch (Exception ex)
                {
                    // Handle other exceptions here
                    throw ex;
                }
            }

        }

        public async Task<T?> QuerySingleOrDefaultAsync<T>(string sql, object? parameters = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
        {
            CheckConnectionValidity();

            using (var command = Connection.CreateCommand())
            {
                command.CommandText = sql;
                command.Transaction = transaction;
                command.CommandType = CommandType.Text;
                command.Transaction = transaction as SqlTransaction;

                if (parameters != null)
                {
                    foreach (var parameter in GetParameters(parameters))
                    {
                        command.Parameters.Add(parameter);
                    }
                }

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var value = reader[0];
                        return (T?)(value == DBNull.Value ? default(T) : value);
                    }
                    else
                    {
                        return default;
                    }
                }
            }
        }

        public async Task<dynamic?> QuerySingleOrDefaultAsync(string sql, object? parameters = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
        {

            using (var command = Connection.CreateCommand())
            {
                command.CommandText = sql;
                command.Transaction = transaction;
                command.CommandType = CommandType.Text;
                command.Transaction = transaction as SqlTransaction;

                if (parameters != null)
                {
                    foreach (var parameter in GetParameters(parameters))
                    {
                        command.Parameters.Add(parameter);
                    }
                }

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var value = reader[0];
                        return (value == DBNull.Value ? default : value);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        private IEnumerable<IDbDataParameter> GetParameters(object? parameters)
        {
            if (parameters != null && parameters is List<IDbDataParameter>) return (List<IDbDataParameter>)parameters;

            var parameterList = new List<IDbDataParameter>();
            if (parameters != null)
            {

                if (parameters is ExpandoObject expandoObject)
                {
                    foreach (var property in expandoObject)
                    {
                        var paramName = "@" + property.Key;
                        var paramValue = property.Value;
                        var sqlParam = new SqlParameter(paramName, paramValue ?? DBNull.Value);
                        parameterList.Add(sqlParam);
                    }
                }
                else
                {
                    Type type = parameters.GetType();
                    var props = type.GetProperties();

                    foreach (var prop in props)
                    {
                        var paramName = "@" + prop.Name;
                        var paramValue = prop.GetValue(parameters, null);
                        var sqlParam = new SqlParameter(paramName, paramValue ?? DBNull.Value);
                        parameterList.Add(sqlParam);
                    }
                }

            }
            return parameterList;
        }

        private void CheckConnectionValidity()
        {
            if (Connection == null)
            {
                throw new InvalidOperationException($"Connection for {GetType()} is not properly initialized.");
            }
        }
    }
}
