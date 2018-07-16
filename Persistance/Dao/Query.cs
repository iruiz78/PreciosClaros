using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Persistance.Dao
{
    /// <summary>
    /// Summary description for Query
    /// </summary>
    /// 
    public static class Query
    {
        public static DataTable GetDataTable(string sQuery, Hashtable hParams)
        {
            DataTable oDataTable = new DataTable();
            SqlConnection oSqlConnection = Connection.GetConnection();
            using (SqlCommand oCommand = new SqlCommand(sQuery, oSqlConnection))
            {
                oCommand.CommandType = CommandType.Text;
                //oCommand.CommandText = sQuery;
                foreach (DictionaryEntry p in hParams)
                {
                    oCommand.Parameters.AddWithValue(p.Key.ToString(), p.Value);
                }
                using (SqlDataAdapter oAdapter = new SqlDataAdapter(oCommand))
                {
                    bool error = false;
                    try
                    {
                        oSqlConnection.Open();
                        oAdapter.Fill(oDataTable);
                    }
                    catch (Exception ex)
                    {
                        error = true;
                        Common.Helpers.Logs.LogFile("GetDataTable:" + ex.ToString());
                    }
                    finally
                    {
                        oSqlConnection.Close();
                    }
                    if (error)
                        throw new Exception("Error");

                }
            }
            return oDataTable;
        }
        public static bool Execute(string sQuery, Hashtable hParams)
        {
            SqlConnection oSqlConnection = Connection.GetConnection();
            using (SqlCommand oCommand = new SqlCommand(sQuery, oSqlConnection))
            {
                oCommand.CommandType = CommandType.Text;
                //oCommand.CommandText = sQuery;
                foreach (DictionaryEntry p in hParams)
                {
                    oCommand.Parameters.AddWithValue(p.Key.ToString(), p.Value);
                }
                bool error = false;
                try
                {
                    oSqlConnection.Open();
                    oCommand.ExecuteNonQuery();
                }
                catch(Exception ex)
                {
                    error = true;
                    Common.Helpers.Logs.LogFile("Execute:" + ex.ToString());
                }
                finally
                {
                    oSqlConnection.Close();
                }
                if (error)
                    throw new Exception("Error");
            }
            return true;
        }
        public static void ExecuteTransaction(string[] sQuery, Hashtable[] hParams)
        {
            SqlConnection oSqlConnection = Connection.GetConnection();
            oSqlConnection.Open();
            using (SqlTransaction oTransaction = oSqlConnection.BeginTransaction())
            {
                bool error = false;
                try
                {
                    SqlCommand oCommand = null;
                    for (int i = 0; i < sQuery.Length; i++)
                    {
                        oCommand = new SqlCommand(sQuery[i], oSqlConnection);
                        oCommand.CommandType = CommandType.Text;
                        //oCommand.CommandText = sQuery[i];
                        foreach (DictionaryEntry p in hParams[i])
                        {
                            oCommand.Parameters.AddWithValue(p.Key.ToString(), p.Value);
                        }
                        oCommand.ExecuteNonQuery();

                    }
                    oTransaction.Commit();
                    oSqlConnection.Close();
                    if (oCommand != null)
                    {
                        oCommand.Dispose();
                    }
                }
                catch(Exception ex)
                {
                    oTransaction.Rollback();
                    error = true;
                    Common.Helpers.Logs.LogFile("ExecuteTransaction:" + ex.ToString());
                }
                finally
                {
                    oSqlConnection.Close();
                }
                if (error)
                    throw new Exception("Error");

            }
        }
        public static void ExecuteEx(string sQuery, Hashtable hParams, CommandType type)
        {
            SqlConnection oSqlConnection = Connection.GetConnection();
            using (SqlCommand oCommand = new SqlCommand(sQuery, oSqlConnection))
            {
                oCommand.CommandType = type;
                //oCommand.CommandText = sQuery;
                foreach (DictionaryEntry p in hParams)
                {
                    oCommand.Parameters.AddWithValue(p.Key.ToString(), p.Value);
                }
                bool error = false;
                try
                {
                    oSqlConnection.Open();
                    oCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    error = true;
                    Common.Helpers.Logs.LogFile("ExecuteEx:" + ex.ToString());
                }
                finally
                {
                    oSqlConnection.Close();
                }
                if (error)
                { throw new Exception("Error"); }
            }
        }
        public static void ExecuteEx(string sQuery, Hashtable hParams)
        {
            SqlConnection oSqlConnection = Connection.GetConnection();
            using (SqlCommand oCommand = new SqlCommand(sQuery, oSqlConnection))
            {
                oCommand.CommandType = CommandType.Text;
                //oCommand.CommandText = sQuery;
                foreach (DictionaryEntry p in hParams)
                {
                    oCommand.Parameters.AddWithValue(p.Key.ToString(), p.Value);
                }
                bool error = false;
                try
                {
                    oSqlConnection.Open();
                    oCommand.ExecuteNonQuery();
                }
                catch(Exception ex)
                {
                    error = true;
                    Common.Helpers.Logs.LogFile("ExecuteEx:" + ex.ToString());
                }
                finally
                {
                    oSqlConnection.Close();
                }
                if (error)
                { throw new Exception("Error"); }
            }
        }
    }
}
