namespace QCimiss
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class sql
    {
        public static int bulkDBtoDB(string cnnnew, string cnnold, string select, string targettable, Dictionary<string, string> ZiDuanDuiYing)
        {
            using (SqlConnection connection = new SqlConnection(cnnold))
            {
                connection.Open();
                SqlDataReader reader = new SqlCommand(select, connection).ExecuteReader();
                using (SqlConnection connection2 = new SqlConnection(cnnnew))
                {
                    connection2.Open();
                    using (SqlBulkCopy copy = new SqlBulkCopy(connection2))
                    {
                        foreach (string str in ZiDuanDuiYing.Keys)
                        {
                            copy.ColumnMappings.Add(str, ZiDuanDuiYing[str].ToString());
                        }
                        copy.DestinationTableName = targettable;
                        try
                        {
                            copy.WriteToServer((IDataReader) reader);
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception.Message);
                            return -1;
                        }
                        finally
                        {
                            reader.Close();
                        }
                    }
                }
            }
            return 0;
        }

        public static int bulkDTtoDB(string cnnnew, DataTable data, string targettable, Dictionary<string, string> ZiDuanDuiYing)
        {
            using (SqlConnection connection = new SqlConnection(cnnnew))
            {
                connection.Open();
                using (SqlBulkCopy copy = new SqlBulkCopy(connection))
                {
                    foreach (string str in ZiDuanDuiYing.Keys)
                    {
                        copy.ColumnMappings.Add(str, ZiDuanDuiYing[str].ToString());
                    }
                    copy.DestinationTableName = targettable;
                    try
                    {
                        try
                        {
                            copy.WriteToServer(data);
                        }
                        catch
                        {
                            return -1;
                        }
                    }
                    finally
                    {
                    }
                }
            }
            return 0;
        }

        public static int insertsql(string cnntstr, string strinsert)
        {
            int num2;
            using (SqlConnection connection = new SqlConnection(cnntstr))
            {
                SqlCommand command = new SqlCommand(strinsert, connection);
                try
                {
                    connection.Open();
                    return command.ExecuteNonQuery();
                }
                catch (SqlException exception)
                {
                    connection.Close();
                    throw new Exception(exception.Message);
                }
                finally
                {
                    if (command != null)
                    {
                        command.Dispose();
                    }
                }
            }
            return num2;
        }

        public static DataTable readsql(string cnntstr, string sltstr)
        {
            DataTable table = new DataTable();
            using (SqlConnection connection = new SqlConnection(cnntstr))
            {
                connection.Open();
                SqlDataReader reader = new SqlCommand(sltstr, connection).ExecuteReader(CommandBehavior.CloseConnection);
                table.Load(reader);
                reader.Close();
            }
            return table;
        }
    }
}

