using SI2_CO.Entities;
using SI2_CO.Mapper;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using System.Transactions;

namespace SI2_CO.Functions
{
    class ProcessoVenda
    {
        Consumidor consumidor;
        private string cs;

        public ProcessoVenda(Consumidor consumidor)
        {
            cs = ConfigurationManager.ConnectionStrings["SI2"].ConnectionString;
            this.consumidor = consumidor;
            MapperConsumidor mapper = new MapperConsumidor();
            mapper.Create(consumidor);
        }

        public ProcessoVenda(int cid, string nome)
        {
            this.consumidor = new Consumidor() { cid = cid, nome = nome };
            cs = ConfigurationManager.ConnectionStrings["SI2"].ConnectionString;
            MapperConsumidor mapper = new MapperConsumidor();
            mapper.Create(consumidor);
        }

        public void InserirVenda(int fid, int tid, int codigoProduto, float precoVenda, int quant)
        {
            Vendas venda = new Vendas()
                {
                    fid = fid,
                    cid = consumidor.cid,
                    tid = tid,
                    codigo_produto = codigoProduto,
                    preco_venda = precoVenda,
                    quantidade = quant
                };

            MapperVendas mapper = new MapperVendas();
            mapper.Create(venda);
        }

        public void FecharVenda(int cid, int tid)
        {
            float total = 0;
            double totalParcial = 0;

            string query = "Select preco_venda, quantidade, descricao" +
                           "from [Venda] join [Produto] on Venda.codigo_produto = Produto.codigo" +
                           "where venda.cid = @cid and venda.tid = @tid";

            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();
                SqlParameter cidParam = new SqlParameter();
                SqlParameter tidParam = new SqlParameter();

                cidParam.ParameterName = "@cid";
                cidParam.SqlDbType = SqlDbType.Int;
                cidParam.Value = cid;

                cidParam.ParameterName = "@tid";
                cidParam.SqlDbType = SqlDbType.Int;
                cidParam.Value = tid;

                cmd.Parameters.Add(cidParam);
                cmd.Parameters.Add(tidParam);

                using (var cn = new SqlConnection(cs))
                {
                    cmd.Connection = cn;
                    cn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {

                        totalParcial = (double)rdr["preco_venda"] * (double)rdr["quantidade"];
                        total += (float)totalParcial;
                        Console.WriteLine((string)rdr["descricao"] + " : " + totalParcial + "€");
                    }
                    Console.WriteLine("Total: " + total);
                }
                ts.Complete();

            }
        

        }
    }
}
