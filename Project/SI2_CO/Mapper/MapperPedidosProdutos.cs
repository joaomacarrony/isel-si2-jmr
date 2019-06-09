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
    class MapperPedidosProdutos
    {
        private string cs;

        public MapperPedidosProdutos()
        {
            cs = ConfigurationManager.ConnectionStrings["SI2"].ConnectionString;
        }

        public void Create(PedidosProdutos pedidosProdutos)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "insert into PedidosProdutos(codigo_produto,quantidade,data) output inserted.ppid values (@codigo_produto,@quantidade,@data)";

                SqlParameter codigo_produto = new SqlParameter();
                SqlParameter quantidade = new SqlParameter();
                SqlParameter data_pedido = new SqlParameter();


                cmd.Parameters.Add(codigo_produto);
                cmd.Parameters.Add(quantidade);
                cmd.Parameters.Add(data_pedido);


                codigo_produto.ParameterName = "@codigo_produto";
                codigo_produto.SqlDbType = SqlDbType.Int;

                quantidade.ParameterName = "@quantidade";
                quantidade.SqlDbType = SqlDbType.Int;

                data_pedido.ParameterName = "@data";
                data_pedido.SqlDbType = SqlDbType.DateTime;

                codigo_produto.Value = pedidosProdutos.codigo_produto;
                quantidade.Value = pedidosProdutos.quantidade;
                data_pedido.Value = pedidosProdutos.data;

                using (var cn = new SqlConnection(cs))
                {

                    cmd.Connection = cn;
                    cn.Open();
                    pedidosProdutos.ppid = (int)cmd.ExecuteScalar();

                }
                ts.Complete();
            }
        }

        public PedidosProdutos Read(int ppid1)
        {
            //PedidosProdutos pedidosProdutos = new PedidosProdutos();
            //using (var ts = new TransactionScope(TransactionScopeOption.Required))
            //{
            //    SqlCommand cmd = new SqlCommand();
            //    cmd.CommandText = "SELECT * FROM PedidosProdutos WHERE ppid = @ppid";

            //    SqlParameter ppid = new SqlParameter();
            //    cmd.Parameters.Add(ppid);
            //    ppid.ParameterName = "@ppid";
            //    ppid.SqlDbType = SqlDbType.Int;
            //    ppid.Value = ppid1;

            //    using (var cn = new SqlConnection(cs))
            //    {

            //        cmd.Connection = cn;
            //        cn.Open();
            //        using (SqlDataReader rdr = cmd.ExecuteReader())
            //        {
            //            while (rdr.Read())
            //            {
            //                                            // Do somthing with this rows string, for example to put them in to a list
            //                //string foid = rdr["foid"].ToString();
            //                string pid = rdr["pid"].ToString();
            //                string foid= rdr["foid"].ToString();
            //                string quantidade = rdr["quantidade"].ToString();
            //                string data_pedido = rdr["data_pedido"].ToString();

            //                pedidosProdutos.pid = Int32.Parse(pid);
            //                pedidosProdutos.quantidade = Int32.Parse(quantidade);
            //                pedidosProdutos.pid = Int32.Parse(pid);
            //                pedidosProdutos.foid = Int32.Parse(foid);
            //                pedidosProdutos.data_pedido = DateTime.Parse(data_pedido);
            //                pedidosProdutos.ppid = ppid1;

            //            }
            //        }

            //    }
            //    ts.Complete();
            //}

            //return pedidosProdutos;
            throw new NotImplementedException();
        }

        public void Update(PedidosProdutos pedidosProdutos)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "UPDATE PedidosProdutos SET codigo_produto = @codigo_produto, quantidade = @quantidade,data = @data WHERE ppid = @ppid";

                SqlParameter ppid = new SqlParameter();
                SqlParameter codigo_produto = new SqlParameter();
                SqlParameter quantidade = new SqlParameter();
                SqlParameter data_pedido = new SqlParameter();


                cmd.Parameters.Add(ppid);
                cmd.Parameters.Add(codigo_produto);
                cmd.Parameters.Add(quantidade);
                cmd.Parameters.Add(data_pedido);


                ppid.ParameterName = "@ppid";
                ppid.SqlDbType = SqlDbType.Int;

                codigo_produto.ParameterName = "@codigo_produto";
                codigo_produto.SqlDbType = SqlDbType.Int;

                quantidade.ParameterName = "@quantidade";
                quantidade.SqlDbType = SqlDbType.Int;

                data_pedido.ParameterName = "@data";
                data_pedido.SqlDbType = SqlDbType.DateTime;

                ppid.Value = pedidosProdutos.ppid;
                codigo_produto.Value = pedidosProdutos.codigo_produto;
                quantidade.Value = pedidosProdutos.quantidade;
                data_pedido.Value = pedidosProdutos.data;

                using (var cn = new SqlConnection(cs))
                {

                    cmd.Connection = cn;
                    cn.Open();

                    cmd.ExecuteNonQuery();

                }
                ts.Complete();
            }
        }
        public void Delete(PedidosProdutos pedidosProdutos)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "DELETE FROM PedidosProdutos WHERE ppid = @ppid";
           
                SqlParameter ppid = new SqlParameter();
  
                cmd.Parameters.Add(ppid);

                ppid.ParameterName = "@ppid";
                ppid.SqlDbType = SqlDbType.Int;

                ppid.Value = pedidosProdutos.ppid;

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


