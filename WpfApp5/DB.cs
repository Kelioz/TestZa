using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp5
{
    internal class DB
    {
        SqlConnection SqlConnection  = new SqlConnection("Data Source = DESKTOP-Q09V9UI; Initial Catalog = TestZavtra; Integrated Security = True");
        
        public void openConnection()
        {
            SqlConnection.Open();
        }
        public void closeConnection()
        {
            SqlConnection.Close();
        }

        public SqlConnection GetSqlConnection() { return SqlConnection; }

    }
}
