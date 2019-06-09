using SI2_CO.Entities;
using SI2_CO.Mapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;

namespace SI2_CO.Functions
{
    class ProcessoFornecedor
    {
        private string cs;

        public ProcessoFornecedor()
        {
            cs = ConfigurationManager.ConnectionStrings["SI2"].ConnectionString;
        }
        public void PedidosProdutoFornecedor(Produto produto, int quantidade, DateTime date)
        {
            MapperPedidosProdutos mapperPedido = new MapperPedidosProdutos();
            PedidosProdutos pedido = new PedidosProdutos()
            {
                codigo_produto = produto.codigo,
                quantidade = quantidade,
                data = date
            };
            mapperPedido.Create(pedido);
        }

        public void RespostaPedidoFornecedor(Produto produto)
        {
            string query = "select codigo_produto," +
                           "preco," +
                           "RespostaPedido.quantidade," +
                           "PedidosProdutos.quantidade," +
                           "resposta" +
                           "from [PedidosProdutos]" +
                           "join [RespostaPedido] on (PedidosProdutos.ppid = RespostaPedido.ppid)" +
                            "where PedidosProdutos.codigo_produto = @codigo_produto; ";

            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();
                SqlParameter codigoParam = new SqlParameter();

                codigoParam.ParameterName = "@codigo_produto";
                codigoParam.SqlDbType = SqlDbType.Int;
                codigoParam.Value = produto.codigo;

                cmd.Parameters.Add(codigoParam);

                cmd.CommandText = query;

                using (var cn = new SqlConnection(cs))
                {
                    cmd.Connection = cn;
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }


            }

            public void VerificaOfertas(Produto produto){

            float precoMedio = 0, precoMinimoDescontado, preco = 0;

            string query = "select count(preco) from Stock where codigo_produto = @pid";

            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();
                SqlParameter codigoParam = new SqlParameter();

                codigoParam.ParameterName = "@pid";
                codigoParam.SqlDbType = SqlDbType.Int;
                codigoParam.Value = produto.codigo;

                cmd.Parameters.Add(codigoParam);

                cmd.CommandText = query;

                using (var cn = new SqlConnection(cs))
                {
                    cmd.Connection = cn;
                    cn.Open();
                    precoMedio = (float)cmd.ExecuteScalar();
                }

                precoMinimoDescontado = (float)(precoMedio * 1.3);

                query = "select RespostaPedido.rid," +
                        "RespostaPedido.preco," +
                         "RespostaPedido.quantidade as QuantidadeResposta," +
                         "PedidosProdutos.quantidade as QuantidadePretendida," +
                         "PedidosProdutos.ppid," +
                         "resposta" +
                        "from [RespostaPedido]" +
                        "join [PedidosProdutos]" +
                        "on PedidosProdutos.ppid = RespostaPedido.ppid" +
                        "+where PedidosProdutos.codigo_produto = @pid" +
                        "+order by RespostaPedido.preco asc;";

                cmd.CommandText = query;

                using (var cn = new SqlConnection(cs))
                {
                    cmd.Connection = cn;
                    cn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {

                        query = "select resposta from [RespostaPedido] where rid = @rid";
                        codigoParam.ParameterName = "@rid";
                        codigoParam.SqlDbType = SqlDbType.Int;
                        codigoParam.Value = (int)rdr["rid"];

                        cmd.Parameters.Add(codigoParam);

                        cmd.CommandText = query;

                        SqlDataReader rdr2 = cmd.ExecuteReader();

                        while (rdr2.Read())
                        {
                            preco = (float)rdr["preco"];
                            if (preco < precoMinimoDescontado)
                            {
                                if (preco >= (float)rdr["QuantidadePretendida"])
                                {
                                    //(bool)rdr.["resposta"] = true;
                                    //PedidosProduto pedido = ctx.PedidosProdutos.Find(ent.PPID);
                                    //ctx.PedidosProdutos.Remove(pedido);
                                    //break;
                                }
                            }
                        }

                    }
                }

                //foreach (var ent in query)
                //{
                //    RespostaPedido resposta = ctx.RespostaPedidos.Find(ent.RID);

                //    if (ent.Preco < precoMinimoDescontado)
                //    {

                //        if (ent.Preco >= ent.QuantidadePretendida)
                //        {
                //            resposta.resposta = true;
                //            PedidosProduto pedido = ctx.PedidosProdutos.Find(ent.PPID);
                //            ctx.PedidosProdutos.Remove(pedido);
                //            break;
                //        }
                //        else
                //        {
                //            resposta.resposta = false;
                //        }
                //    }
                //    else
                //    {
                //        resposta.resposta = false;
                //    }
                //}

            }
            }
        }
    }

