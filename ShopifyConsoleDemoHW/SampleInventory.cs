using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopifyConsoleDemoHW
{
    using System.Data.SqlClient;

    class SampleInventory
    {
        Public int TotalInventory1(string ItemNumber)
        {
            int count = 0;
            string sql = "SELECT SUM(Quantity) AS Qty FROM Items WHERE ItemNumber = '" + ItemNumber + "'";
            System.Data.SqlClient.SqlConnection conn = createNewConnection();
            conn.Open();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sql, conn);
            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                count = (int)reader["Qty"];
            }
            return count;
        }
        //Assume that: createNewConnection(); returns a un-opened but correctly constructed SqlConnection object.

        /*
         Q3a: How many potential or actual problems can you find
    with this method (and why are they potential problems)?
    -The connection is never closed
    -you should not concatenate user strings like that (risk of SQL injection attacks)
    -no connection string
    -Should use "using" to close the resource after it is done.
         */

        public int TotalInventory(string ItemNumber)
        {
            int count = 0;
            string template = "SELECT SUM(Quantity) AS Qty FROM Items WHERE ItemNumber = {0}";
            string sql = string.Format(template, ItemNumber);
            System.Data.SqlClient.SqlConnection conn = createNewConnection();
            string connectionString = "Server=SomeServer;Database=Somedatabase;User Id=JohnDoe;Password=SomePassword;";

            //open connection, get the sum, then close when done
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        count = (int)reader["Qty"];
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
                        return count;
            }

        }

    }
}