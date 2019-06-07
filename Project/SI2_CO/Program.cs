using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data.SqlClient;

namespace SI2_CO
{

    class Program
    {
        static void Main(string[] args)
        {
            SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder();
            csb.DataSource = "localhost";
            csb.InitialCatalog = "SI2"; 
            csb.UserID = "sa";
            csb.Password = "1234";

            using (SqlConnection cn = new SqlConnection(csb.ConnectionString))
            {
                string strSQL = "UPDATE Franqueado SET morada = 'Av. do Brasil Nº 7' WHERE fid = 1";
                cn.Open();
                SqlCommand cmd = new SqlCommand(strSQL, cn);
                Console.WriteLine(cmd.ExecuteNonQuery() + " rows affected");
                cn.Close();
            }
        }
    }
}
