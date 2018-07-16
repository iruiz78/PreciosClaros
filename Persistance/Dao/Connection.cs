using System.Data.SqlClient;

namespace Persistance.Dao
{
    public static class Connection
    {
        private static string _sConnectionString = string.Empty;
        public static void SetConnectionString(string sConnectionString)
        {
            _sConnectionString = sConnectionString;
        }
        public static SqlConnection GetConnection()
        {
            SqlConnection oSqlConnection = new SqlConnection(_sConnectionString);
            return oSqlConnection;
        }
    }
}
