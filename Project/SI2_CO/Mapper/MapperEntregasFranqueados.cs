using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;


using SI2_CO.Entities;
using SI2_CO.Mapper_Interfaces;



using System.Configuration;
using System.Transactions;


namespace SI2_CO.Mapper
{
    class MapperEntregasFranqueados
    {
        private string cs;

        public MapperEntregasFranqueados()
        {
            cs = ConfigurationManager.ConnectionStrings["SI2"].ConnectionString;
        }

        public void Create(EntregasFranqueados entregasFranqueados)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "INSERT INTO EntregasFranqueados(quantidade,fid,pid) output inserted.efid VALUES(@quantidade,@fid,@pid)";

                SqlParameter quantidade = new SqlParameter();             
                SqlParameter fid = new SqlParameter();             
                SqlParameter pid = new SqlParameter();            


               // cmd.Parameters.Add(efid);
                cmd.Parameters.Add(quantidade);
                cmd.Parameters.Add(fid);
                cmd.Parameters.Add(pid);
                // tid.ParameterName = "@tid";
                //tid.SqlDbType = SqlDbType.Int;
               //efid.ParameterName = "@efid";
               //efid.SqlDbType = SqlDbType.Int;
                quantidade.ParameterName = "@quantidade";
                quantidade.SqlDbType = SqlDbType.Int;
                fid.ParameterName = "@fid";
                fid.SqlDbType = SqlDbType.Int;
                pid.ParameterName = "@pid";
                pid.SqlDbType = SqlDbType.Int;

                //efid.Value = entregasFranqueados.efid;
                quantidade.Value = entregasFranqueados.quantidade;
                fid.Value = entregasFranqueados.fid;
                pid.Value = entregasFranqueados.pid;


                using (var cn = new SqlConnection(cs))
                {

                    cmd.Connection = cn;
                    cn.Open();

                    entregasFranqueados.efid = (int)cmd.ExecuteScalar();

                }
                ts.Complete();
            }
        }

        public EntregasFranqueados Read(int efid1)
        {
            EntregasFranqueados entregasFranqueados = new EntregasFranqueados();
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT quantidade,pid,fid,efid FROM EntregasFranqueados WHERE efid=@efid";

                SqlParameter efid = new SqlParameter();
                SqlParameter quantidade = new SqlParameter();
                SqlParameter fid = new SqlParameter();
                SqlParameter pid = new SqlParameter();


                cmd.Parameters.Add(efid);
                cmd.Parameters.Add(quantidade);
                cmd.Parameters.Add(fid);
                cmd.Parameters.Add(pid);
            
                quantidade.ParameterName = "@quantidade";
                quantidade.SqlDbType = SqlDbType.Int;
                efid.ParameterName = "@efid";
                efid.SqlDbType = SqlDbType.Int;
                fid.ParameterName = "@fid";
                fid.SqlDbType = SqlDbType.Int;
                pid.ParameterName = "@pid";
                pid.SqlDbType = SqlDbType.Int;

                efid.Value = efid1;quantidade.Value=0;fid.Value = 0;pid.Value = 0;


                using (var cn = new SqlConnection(cs))
                {

                    cmd.Connection = cn;
                    cn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            string sefid = rdr["efid"].ToString();                               // Do somthing with this rows string, for example to put them in to a list
                            string sfid = rdr["fid"].ToString();
                            string spid = rdr["pid"].ToString();
                            string squantidade = rdr["quantidade"].ToString();
                            entregasFranqueados.efid = Int32.Parse(sefid);
                            entregasFranqueados.fid = Int32.Parse(sfid);
                            entregasFranqueados.pid = Int32.Parse(spid);
                            entregasFranqueados.quantidade = Int32.Parse(squantidade);

                        }
                    }
                }
                ts.Complete();
                return entregasFranqueados;
            }
            
        }

        public void Update(EntregasFranqueados entregasFranqueados)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "UPDATE EntregasFranqueados SET  quantidade=@quantidade,fid=@fid,pid=@pid WHERE efid=@efid";
                SqlParameter efid = new SqlParameter();
                SqlParameter quantidade = new SqlParameter();
                SqlParameter fid = new SqlParameter();
                SqlParameter pid = new SqlParameter();


                 cmd.Parameters.Add(efid);
                cmd.Parameters.Add(quantidade);
                cmd.Parameters.Add(fid);
                cmd.Parameters.Add(pid);
                // tid.ParameterName = "@tid";
                //tid.SqlDbType = SqlDbType.Int;
                efid.ParameterName = "@efid";
                efid.SqlDbType = SqlDbType.Int;
                quantidade.ParameterName = "@quantidade";
                quantidade.SqlDbType = SqlDbType.Int;
                fid.ParameterName = "@fid";
                fid.SqlDbType = SqlDbType.Int;
                pid.ParameterName = "@pid";
                pid.SqlDbType = SqlDbType.Int;

                efid.Value = entregasFranqueados.efid;
                quantidade.Value = entregasFranqueados.quantidade;
                fid.Value = entregasFranqueados.fid;
                pid.Value = entregasFranqueados.pid;


                using (var cn = new SqlConnection(cs))
                {

                    cmd.Connection = cn;
                    cn.Open();

                    cmd.ExecuteNonQuery();

                }
                ts.Complete();
            }
        }
        public void Delete(int efid1)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "DELETE FROM EntregasFranqueados WHERE efid=@efid";
                SqlParameter efid = new SqlParameter();
                cmd.Parameters.Add(efid);
              
              
                efid.ParameterName = "@efid";
                efid.SqlDbType = SqlDbType.Int;
                efid.Value = efid1;
              

                using (var cn = new SqlConnection(cs))
                {

                    cmd.Connection = cn;
                    cn.Open();

                    cmd.ExecuteNonQuery();

                }
                ts.Complete();
            }
        }
    }
}
