using System.Data;
using System.Data.SqlClient;

namespace ClientNetAPI.Repositories
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string ConnectionString { get; set; }
    }

    public interface IDatabaseSettings
    {
        string ConnectionString { get; set; }
    }

    public class SqlServerConnectionProvider
    {
        private readonly string _connectionString;
        public SqlServerConnectionProvider(IDatabaseSettings settings)
        {
            _connectionString = settings.ConnectionString;
        }

        public IDbConnection GetDbConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}