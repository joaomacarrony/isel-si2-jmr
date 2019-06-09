using SI2_CO.Entities;
using SI2_CO.Functions;
using System;
using System.Data.SqlClient;

namespace SI2_CO
{

    class Program
    {
        static void Main(string[] args)
        {
            //SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder();
            //csb.DataSource = "localhost";
            //csb.InitialCatalog = "SI2";
            //csb.UserID = "sa";
            //csb.Password = "1234";

            //using (SqlConnection cn = new SqlConnection(csb.ConnectionString))
            //{
            //    string strSQL = "UPDATE Franqueado SET morada = 'Av. do Brasil Nº 7' WHERE fid = 1";
            //    cn.Open();
            //    SqlCommand cmd = new SqlCommand(strSQL, cn);
            //    Console.WriteLine(cmd.ExecuteNonQuery() + " rows affected");
            //    cn.Close();
            //}

            /* Teste FranqueadoUtils*/

            Franqueado franq = new Franqueado();
            FranqueadoUtils franqUtils = new FranqueadoUtils();

            franq.nome = "Teste Franqueado CO";
            franq.nif = 200300400;
            franq.morada = "Teste Morada CO";

            franqUtils.Insert(franq);
            franqUtils.Remove(franq);

            Franqueado franq2 = new Franqueado();
            franq2.fid = 1;

            Console.WriteLine(franqUtils.TotalVendasFranqueado(franq2));
            Console.ReadLine();
        }
    }
}
