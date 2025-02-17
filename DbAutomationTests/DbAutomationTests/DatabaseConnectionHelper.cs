using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace DbAutomationTests
{
    public static class DatabaseConnectionHelper
    {
        private static string _connectionString;

        static DatabaseConnectionHelper()
        {
            var configuration = ConfigurationHelper.GetConfiguration();
            _connectionString = configuration.GetConnectionString("OracleDb");
        }

        public static OracleConnection GetConnection()
        {
            return new OracleConnection(_connectionString);
        }
    }
}
