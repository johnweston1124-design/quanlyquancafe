using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace quanlyquancafe.DAL
{
    public sealed class DataProvider
    {
        private static readonly Lazy<DataProvider> _instance =
            new Lazy<DataProvider>(() => new DataProvider());

        private readonly string _connectionString;

        public static DataProvider Instance
        {
            get { return _instance.Value; }
        }

        private DataProvider()
        {
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["CoffeeShopDB"];

            if (settings == null || string.IsNullOrWhiteSpace(settings.ConnectionString))
            {
                throw new InvalidOperationException(
                    "Connection string 'CoffeeShopDB' was not found in App.config.");
            }

            _connectionString = settings.ConnectionString;
        }

        public SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public DataTable ExecuteQuery(
            string query,
            CommandType commandType = CommandType.Text,
            SqlTransaction transaction = null,
            params SqlParameter[] parameters)
        {
            SqlConnection connection = transaction != null
                ? transaction.Connection
                : CreateConnection();

            bool shouldCloseConnection = transaction == null;

            try
            {
                using (SqlCommand command = CreateCommand(query, connection, commandType, transaction, parameters))
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Database ExecuteQuery failed.", ex);
            }
            finally
            {
                if (shouldCloseConnection && connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }

        public int ExecuteNonQuery(
            string query,
            CommandType commandType = CommandType.Text,
            SqlTransaction transaction = null,
            params SqlParameter[] parameters)
        {
            SqlConnection connection = transaction != null
                ? transaction.Connection
                : CreateConnection();

            bool shouldCloseConnection = transaction == null;

            try
            {
                using (SqlCommand command = CreateCommand(query, connection, commandType, transaction, parameters))
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }

                    return command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Database ExecuteNonQuery failed.", ex);
            }
            finally
            {
                if (shouldCloseConnection && connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }

        public object ExecuteScalar(
            string query,
            CommandType commandType = CommandType.Text,
            SqlTransaction transaction = null,
            params SqlParameter[] parameters)
        {
            SqlConnection connection = transaction != null
                ? transaction.Connection
                : CreateConnection();

            bool shouldCloseConnection = transaction == null;

            try
            {
                using (SqlCommand command = CreateCommand(query, connection, commandType, transaction, parameters))
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }

                    return command.ExecuteScalar();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Database ExecuteScalar failed.", ex);
            }
            finally
            {
                if (shouldCloseConnection && connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }

        public void ExecuteInTransaction(Action<SqlTransaction> action)
        {
            using (SqlConnection connection = CreateConnection())
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        action(transaction);
                        transaction.Commit();
                    }
                    catch
                    {
                        try
                        {
                            transaction.Rollback();
                        }
                        catch
                        {
                            // Ignore rollback exception.
                        }

                        throw;
                    }
                }
            }
        }

        public T ExecuteInTransaction<T>(Func<SqlTransaction, T> action)
        {
            using (SqlConnection connection = CreateConnection())
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        T result = action(transaction);
                        transaction.Commit();
                        return result;
                    }
                    catch
                    {
                        try
                        {
                            transaction.Rollback();
                        }
                        catch
                        {
                            // Ignore rollback exception.
                        }

                        throw;
                    }
                }
            }
        }

        private SqlCommand CreateCommand(
            string query,
            SqlConnection connection,
            CommandType commandType,
            SqlTransaction transaction,
            SqlParameter[] parameters)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentException("Query must not be empty.", "query");
            }

            SqlCommand command = new SqlCommand(query, connection);
            command.CommandType = commandType;
            command.CommandTimeout = 30;

            if (transaction != null)
            {
                command.Transaction = transaction;
            }

            AddParameters(command, parameters);
            return command;
        }

        private void AddParameters(SqlCommand command, SqlParameter[] parameters)
        {
            if (parameters == null || parameters.Length == 0)
            {
                return;
            }

            foreach (SqlParameter parameter in parameters)
            {
                if (parameter == null)
                {
                    continue;
                }

                if (parameter.Value == null)
                {
                    parameter.Value = DBNull.Value;
                }

                command.Parameters.Add(parameter);
            }
        }
    }
}
