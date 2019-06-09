using SI2_CO.Entities;
using SI2_CO.Mapper;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;

namespace SI2_CO.Functions
{
    public class FranqueadoUtils
    {
        private string cs;

        public FranqueadoUtils()
        {
            cs = ConfigurationManager.ConnectionStrings["SI2"].ConnectionString;
        }

        public FranqueadoUtils(SqlConnectionStringBuilder csb)
        {
            cs = csb.ConnectionString;
        }

        public void Insert(Franqueado franq)
        {
            MapperFranqueado mapper = new MapperFranqueado();
            mapper.Create(franq);
        }

        public void Update(Franqueado franq)
        {
            MapperFranqueado mapper = new MapperFranqueado();
            mapper.Update(franq);
        }

        public void Remove(Franqueado franq)
        {
            MapperFranqueado mapper = new MapperFranqueado();
            mapper.Delete(franq);
        }

        public void ForçarRemoçãoFranqueado(Franqueado franq)
        {

            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();
                SqlParameter fidParam = new SqlParameter();

                fidParam.ParameterName = "@fid";
                fidParam.SqlDbType = SqlDbType.Int;
                fidParam.Value = franq.fid;

                cmd.Parameters.Add(fidParam);

                /* Remover EntregasFranqueados */

                cmd.CommandText = "delete from EntregasFranqueados where fid = @fid";
                
                using (var cn = new SqlConnection(cs))
                {
                    cmd.Connection = cn;
                    cn.Open(); 
                    cmd.ExecuteNonQuery();
                }

                /* Remover Stock */

                cmd.CommandText = "delete from Stocks where fid = @fid";

                using (var cn = new SqlConnection(cs))
                {
                    cmd.Connection = cn;
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }

                /* Remover Vendas */

                cmd.CommandText = "delete from Vendas where fid = @fid";

                using (var cn = new SqlConnection(cs))
                {
                    cmd.Connection = cn;
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }

                /* Remover HistoricoVenda */

                cmd.CommandText = "delete from HistoricoVenda where fid = @fid";

                using (var cn = new SqlConnection(cs))
                {
                    cmd.Connection = cn;
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }

                /* Remover PedidosFranqueado */

                cmd.CommandText = "delete from PedidosFranqueado where fid = @fid";

                using (var cn = new SqlConnection(cs))
                {
                    cmd.Connection = cn;
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }

                /* Remover Franqueado */

                cmd.CommandText = "delete from Franqueado where fid = @fid";

                using (var cn = new SqlConnection(cs))
                {
                    cmd.Connection = cn;
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
                ts.Complete();
            }
        }

        public double TotalVendasFranqueado(Franqueado franq)
        {
            double total = 0;

            int year = DateTime.Now.Year;

            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "select preco_venda, quantidade from [Vendas]" +
                                  "where fid = @fid and year(data_venda) = @year";

                SqlParameter fidParam = new SqlParameter();
                SqlParameter yearParam = new SqlParameter();

                cmd.Parameters.Add(fidParam);
                cmd.Parameters.Add(yearParam);

                fidParam.ParameterName = "@fid";
                fidParam.SqlDbType = SqlDbType.Int;

                yearParam.ParameterName = "@year";
                yearParam.SqlDbType = SqlDbType.Int;

                yearParam.Value = year;
                fidParam.Value = franq.fid;


                using (var cn = new SqlConnection(cs))
                {

                    cmd.Connection = cn;
                    cn.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        object preco = rdr["preco_venda"];
                        int quantidade = (int)rdr["quantidade"];
                        total += (double)preco * quantidade;
                    }

                }
                ts.Complete();

                return total;
            }
        }
    }
}
