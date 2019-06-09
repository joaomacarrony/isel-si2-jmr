using SI2_CO.Entities;
using SI2_CO.Mapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SI2_CO.Functions
{
    class ProdutoUtils
    {
        private string cs;

        public ProdutoUtils()
        {
            cs = ConfigurationManager.ConnectionStrings["SI2"].ConnectionString;
        }
        public void Insert(Produto produto)
        {
            MapperProduto mapper = new MapperProduto();
            mapper.Create(produto);
        }

        public void Update(Produto produto)
        {
            MapperProduto mapper = new MapperProduto();
            mapper.Update(produto);
        }

        public void Remove(Produto produto)
        {
            MapperProduto mapper = new MapperProduto();
            mapper.Delete(produto);
        }

        public void ForçarRemoçãoProduto(Produto produto)
        {
            //          delete from Stock where codigo_produto = @codigo_produto;
            // 			delete from FornecedoresProdutos where codigo_produto = @codigo_produto;
            // 			delete from PedidosFranqueados where codigo_produto = @codigo_produto;
            // 			delete from PedidosProdutos where codigo_produto = @codigo_produto;
            // 			delete from EntregasFranqueados where codigo_produto = @codigo_produto;
            // 			delete from Vendas where codigo_produto = @codigo_produto;
            // 			delete from HistoricoVendas where codigo_produto = @codigo_produto;
            // 			delete from Produto where codigo = @codigo_produto;

            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();
                SqlParameter codigoParam = new SqlParameter();

                codigoParam.ParameterName = "@codigo_produto";
                codigoParam.SqlDbType = SqlDbType.Int;
                codigoParam.Value = produto.codigo;

                cmd.Parameters.Add(codigoParam);

                /* Remover Stock */

                cmd.CommandText = "delete from Stock where codigo_produto = @codigo_produto";

                using (var cn = new SqlConnection(cs))
                {
                    cmd.Connection = cn;
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }

                /* Remover FornecedoresProdutos */

                cmd.CommandText = "delete from FornecedoresProdutos where codigo_produto = @codigo_produto";

                using (var cn = new SqlConnection(cs))
                {
                    cmd.Connection = cn;
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }

                /* Remover PedidosFranqueados */

                cmd.CommandText = "delete from PedidosFranqueados where codigo_produto = @codigo_produto";

                using (var cn = new SqlConnection(cs))
                {
                    cmd.Connection = cn;
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }

                /* Remover PedidosProdutos */

                cmd.CommandText = "delete from PedidosProdutos where codigo_produto = @codigo_produto";

                using (var cn = new SqlConnection(cs))
                {
                    cmd.Connection = cn;
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }

                /* Remover EntregasFranqueados */

                cmd.CommandText = "delete from EntregasFranqueados where codigo_produto = @codigo_produto";

                using (var cn = new SqlConnection(cs))
                {
                    cmd.Connection = cn;
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }

                /* Remover Vendas */

                cmd.CommandText = "delete from Vendas where codigo_produto = @codigo_produto";

                using (var cn = new SqlConnection(cs))
                {
                    cmd.Connection = cn;
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }

                /* Remover HistoricoVendas */

                cmd.CommandText = "delete from HistoricoVendas where codigo_produto = @codigo_produto";

                using (var cn = new SqlConnection(cs))
                {
                    cmd.Connection = cn;
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }

                /* Remover Produto */

                cmd.CommandText = "delete from Produto where codigo = @codigo_produto";

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
