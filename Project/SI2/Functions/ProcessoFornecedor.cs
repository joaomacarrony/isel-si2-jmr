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
            float precoMedio = 0, precoMinimoDescontado;

            using (var ctx = new SI2Entities())
            {
                precoMedio = (float)(from stock in ctx.Stocks
                                     where stock.codigo_produto == pid
                                     select stock.preco).Average();


                precoMinimoDescontado = (float)(precoMedio * 1.3);

                var query = from pedidos in ctx.PedidosProdutos
                            join resposta in ctx.RespostaPedidos
                            on pedidos.ppid equals
                               resposta.ppid
                            where pedidos.codigo_produto == pid
                            orderby resposta.preco ascending
                            select new
                            {
                                RID = resposta.rid,
                                Preco = resposta.preco,
                                QuantidadeResposta = resposta.quantidade,
                                QuantidadePretendida = pedidos.quantidade,
                                PPID = pedidos.ppid,
                                RespostaFornecedor = resposta.resposta
                            };

                foreach (var ent in query)
                {
                    RespostaPedido resposta = ctx.RespostaPedidos.Find(ent.RID);

                    if (ent.Preco < precoMinimoDescontado)
                    {

                        if (ent.Preco >= ent.QuantidadePretendida)
                        {
                            resposta.resposta = true;
                            PedidosProduto pedido = ctx.PedidosProdutos.Find(ent.PPID);
                            //ctx.PedidosProdutos.Remove(pedido);
                            break;
                        }
                        else
                        {
                            resposta.resposta = false;
                        }
                    }
                    else
                    {
                        resposta.resposta = false;
                    }
                }
                ctx.SaveChanges();
            }
        }
    }
}
