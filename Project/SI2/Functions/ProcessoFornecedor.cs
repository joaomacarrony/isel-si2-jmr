using System;
using System.Collections.Generic;
using System.Linq;

namespace SI2.Functions
{
    class ProcessoFornecedor
    {
        public static void PedidosProdutoFornecedor(int codigoProduto, int quantidade, DateTime date)
        {
            using (var ctx = new SI2Entities())
            {
                ctx.PedidosProdutos.Add(new PedidosProduto {
                    codigo_produto = codigoProduto,
                    quantidade = quantidade,
                    data = date,
                });
                ctx.SaveChanges();
            }
        }

        public static void RespostaPedidoFornecedor(int codigoProduto)
        {
            using (var ctx = new SI2Entities())
            {
                var query = from pedidos in ctx.PedidosProdutos
                            join resposta in ctx.RespostaPedidos
                            on pedidos.ppid equals
                               resposta.ppid
                            where pedidos.codigo_produto == codigoProduto
                            select new { Produto = pedidos.codigo_produto,
                                         Preco = resposta.preco,
                                         QuantidadeResposta = resposta.quantidade,
                                         QuantidadePretendida = pedidos.quantidade,
                                         Resposta = resposta.resposta
                            };
            }
        }

        public static void VerificaOfertas(int pid)
        {
            // TODO
        }
    }
}
