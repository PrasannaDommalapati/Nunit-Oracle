using Dapper;
using System.Data;

namespace DbAutomationTests
{
    public class OracleDapperTests
    {
        [Test]
        public void Test_OracleConnection_ShouldBeSuccessful()
        {
            using var connection = DatabaseConnectionHelper.GetConnection();
            connection.Open();
            Assert.That(connection.State, Is.EqualTo(ConnectionState.Open), "Database connection should be open.");
        }

        [Test]
        public void Test_SelectQuery_ShouldReturnResults()
        {
            using var connection = DatabaseConnectionHelper.GetConnection();
            connection.Open();
            var result = connection.QueryFirstOrDefault<int>("SELECT COUNT(*) FROM USERS");
            Assert.That(result, Is.GreaterThan(0), "Users table should have at least one record.");
        }

        [Test]
        public void Test_DeleteQuery_ShouldRemoveRecords()
        {
            using var connection = DatabaseConnectionHelper.GetConnection();
            connection.Open();

            var idsToDelete = new int[] { 1, 2, 3 };

            var rowsAffected = connection.Execute("DELETE FROM USERS WHERE ID IN @Ids", new { Ids = idsToDelete });

            var remainingCount = connection.QueryFirstOrDefault<int>("SELECT COUNT(*) FROM USERS WHERE ID IN @Ids", new { Ids = idsToDelete });
            Assert.That(remainingCount, Is.EqualTo(0), "Records with specified IDs should be deleted.");
        }

        [Test]
        public void ReadEcel()
        {
            ReadExcelHelper.Read();
        }
    }
}
