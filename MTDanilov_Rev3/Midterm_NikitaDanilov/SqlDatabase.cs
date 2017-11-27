using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midterm_NikitaDanilov
{
    public class SqlDatabase
    {
        private static SqlDatabase _instance;
        private SqlDatabase() { }

        // Initiate connection to the db
        public static SqlConnection sqlConnection =
            new SqlConnection(ConfigurationManager.ConnectionStrings["GreatCorporationDB"].ConnectionString);

        // Initiate singleton instance
        public static SqlDatabase Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SqlDatabase();
                }
                return _instance;
            }
        }

        // Open connection
        public void OpenConnection()
        {
            try
            {
                sqlConnection.Open();
                Console.WriteLine("Connection Opened...");
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error: " + ex.ToString());
            }
        }

        // Close Connection
        public void CloseConnection()
        {
            try
            {
                sqlConnection.Close();
                Console.WriteLine("\n");
                Console.WriteLine("Connection Closed...");
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error: " + ex.ToString());
            }
        }

        // Get data from database by query
        public List<object[]> SelectData(string query)
        {
            List<object[]> result = new List<object[]>();
            try
            {
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            object[] row = new object[dr.FieldCount];
                            for (int i = 0; i < dr.FieldCount; i++)
                                if (dr.GetValue(i) != DBNull.Value) row[i] = dr.GetValue(i);
                            result.Add(row);
                        }
                        dr.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                // Display error
                Console.WriteLine("Error: " + ex.ToString());
            }
            return result;
        }
    }
}
