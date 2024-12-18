using Microsoft.Data.SqlClient;

namespace Csharp.Api.Data
{
    public class CommandSql
    {
        private string _connectionString;
        private readonly IConfiguration _configuration;
        public CommandSql(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<SqlConnection> Open(SqlConnection connection)
        {
            try
            {

            if(connection.State == System.Data.ConnectionState.Closed)
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
            }
            }catch (Exception ex) { }
            return connection;
        }
        public async Task<SqlConnection> Close (SqlConnection connection)
        {
            if(connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
            return connection;
        }
    }
}
