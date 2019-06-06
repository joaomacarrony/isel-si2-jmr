using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SI2.Functions
{
    class ProdutoUtils
    {
        public static void Insert(int codigo, string tipo, string descricao, int quant, int quantMinima, int quantMaxima, int armazem)
        {
            using (var ctx = new SI2Entities())
            {
                ctx.Produtos.Add(new Produto()
                {
                    codigo = codigo,
                    tipo = tipo,
                    descricao = descricao,
                    quantidade = quant,
                    quantidade_minima = quantMinima,
                    quantidade_maxima = quantMaxima,
                    armazem = armazem
                });

                ctx.SaveChanges();
            }
        }

        public static void Update(int pid, int codigo, string tipo, string descricao, int quant, int quantMinima, int quantMaxima, int armazem)
        {
            using (var ctx = new SI2Entities())
            {
                Produto produto = ctx.produto.Find(pid);
                    produto.codigo = codigo;
                    produto.tipo = tipo;
                    produto.descricao = descricao;
                    produto.quantidade = quant;
                    produto.quantidade_minima = quantMinima;
                    produto.quantidade_maxima = quantMaxima;
                    produto.armazem = armazem;
                ctx.SaveChanges();
            }
        }

        public static void Remove(int pid)
        {
            using (var ctx = new SI2Entities())
            {
                var stock = ctx.Stock
                    .Where(s => s.codigo_produto == pid)
                    .FirstOrDefault<Stock>();
                if (stock != null){
                    Produto produto = ctx.Produtos.Find(pid);
                    ctx.Produtos.Remove(produto);
                    ctx.SaveChanges();
                }else{
                    Console.WriteLine("Product can't be removed because it's being used");
                }
            }
        }

//          delete from Stock where codigo_produto = @codigo_produto;
// 			delete from FornecedoresProdutos where codigo_produto = @codigo_produto;
// 			delete from PedidosFranqueados where codigo_produto = @codigo_produto;
// 			delete from PedidosProdutos where codigo_produto = @codigo_produto;
// 			delete from EntregasFranqueados where codigo_produto = @codigo_produto;
// 			delete from Vendas where codigo_produto = @codigo_produto;
// 			delete from HistoricoVendas where codigo_produto = @codigo_produto;
// 			delete from Produto where codigo = @codigo_produto;

        public static void ForçarRemoçãoProduto(int codigoProduto)
        {
            using (var ctx = new SI2Entities())
            {
                // Produto produto =                       ctx.Produtos.Find(codigoProduto);
                // HistoricoVenda historicoVendas =        ctx.HistoricoVendas.Find(codigoProduto);
                // Venda venda =                           ctx.Vendas.Find(codigoProduto);
                // EntregasFranqueado entregaFranqueado =  ctx.EntregasFranqueados.Find(codigoProduto);
                // PedidosProduto pedidoProduto =          ctx.PedidosProdutos.Find(codigoProduto);
                // PedidosFranqueado pedidoFranqueado =    ctx.PedidosFranqueados.Find(codigoProduto);
                // FornecedoresProduto fornecedorProduto = ctx.FornecedoresProdutos.Find(codigoProduto);
                // Stock stock =                           ctx.Stock.Find(codigoProduto)               ;

                ctx.Produtos.Remove(codigoProduto);
                ctx.HistoricoVendas.Remove(codigoProduto);
                ctx.Vendas.Remove(codigoProduto);
                ctx.EntregasFranqueados.Remove(codigoProduto);
                ctx.PedidosProdutos.Remove(codigoProduto);
                ctx.PedidosFranqueados.Remove(codigoProduto);
                ctx.FornecedoresProdutos.Remove(codigoProduto);
                ctx.Stock.Remove(codigoProduto);

                ctx.SaveChanges();
            }
        }
    }
}
