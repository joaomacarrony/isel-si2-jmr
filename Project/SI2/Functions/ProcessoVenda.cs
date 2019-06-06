using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SI2.Functions
{
    class ProcessoVenda
    {
        Consumidor consumidor;
        public ProcessoVenda(Consumidor consumidor){
            this.consumidor = consumidor;
            InsertConsumidor();
        }

        public ProcessoVenda(int cid, string nome){
            this.consumidor = new Consumidor(){cid = cid,nome = nome};
            InsertConsumidor();
        }

        private void InsertConsumidor(){
            using (var ctx = new SI2Entities())
            {
                ctx.Consumidores.Add(consumidor);
                ctx.SaveChanges();
            }
        }

        public void InserirVenda(int fid, int tid, int codigoProduto, float precoVenda, int quant){
            using (var ctx = new SI2Entities())
            {
                ctx.Vendas.Add(new Venda()
                {
                    fid = fid,
                    cid = consumidor.cid,
                    tid = tid,
                    codigo_produto = codigoProduto,
                    preco_venda = precoVenda,
                    quantidade = quant
                });
                ctx.SaveChanges();
            }
        }

		// select Vendas.preco_venda, Vendas.quantidade, Produto.descricao
		// 	from Vendas
		// 	join Produto
		// 	on (Vendas.codigo_produto = Produto.codigo)
		// 	where cid = @cid and tid = @tid;
        public void FecharVenda(int cid, int tid)
        {
            using(var ctx = new SI2Entities())
            {
                float total = 0;
                double totalParcial = 0;

                var query = from venda in ctx.Vendas
                            join produto in ctx.Produtos
                            on venda.codigo_produto equals
                               produto.codigo
                            where venda.cid == cid && venda.tid == tid
                            select new { venda.preco_venda, venda.quantidade, produto.descricao };

                foreach (var entity in query)
                {
                    totalParcial = (double)(entity.preco_venda * entity.quantidade);
                    total += (float)totalParcial;
                    Console.WriteLine(entity.descricao + " : " + totalParcial + '€');
                }

                Console.WriteLine("Total: " + total);
            }

        }
    }
}
