using System;
using System.Data;
using System.Data.SQLite;

namespace IRSACA.src {
    public class DataManager {
        public static string GetConnection (string path) {
            var connectionString = "Data Source="+path+";Version=3; FailIfMissing=True; Foreign Keys=True;";
            return connectionString;
        }

        public static DataTable GetData(string Sql, string constr )
        {
            try
            { 
                SQLiteConnection conn = new SQLiteConnection(constr); 
                SQLiteDataAdapter da = new SQLiteDataAdapter(Sql,conn);  
                DataTable dt = new DataTable();
                da.Fill(dt);
                da.SelectCommand.Dispose();
                da.SelectCommand = null;
                CloseConnection(conn);
                return dt;
            }
            catch (Exception ex)
            {
               throw ex;
            }
        }

        public static void CloseConnection(SQLiteConnection Connection)
        {
            if (Connection.State == ConnectionState.Open)
                Connection.Close();
               Connection.Dispose();
               Connection = null;
        }

        
    }
}