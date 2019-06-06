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
            InserConsumidor();
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

// var query = database.Posts    // your starting point - table in the "from" statement
//    .Join(database.Post_Metas, // the source table of the inner join
//       post => post.ID,        // Select the primary key (the first part of the "on" clause in an sql "join" statement)
//       meta => meta.Post_ID,   // Select the foreign key (the second part of the "on" clause)
//       (post, meta) => new { Post = post, Meta = meta }) // selection
//    .Where(postAndMeta => postAndMeta.Post.ID == id);    // where statement

		// select Vendas.preco_venda, Vendas.quantidade, Produto.descricao
		// 	from Vendas
		// 	join Produto
		// 	on (Vendas.codigo_produto = Produto.codigo)
		// 	where cid = @cid and tid = @tid;
        public void FecharVenda(int cid, int tid)
        {
            using(var ctx = new SI2Entities())
            {
                float total = 0, total = 0, totalParcial = 0;
                int count = 0;
                // var query = ctx.Vendas
                //             .Join(ctx.Produtos,
                //             venda => venda.codigo_produto,
                //             produto => produto.codigo,
                //             (venda, produto) => new {
                //                 Vendas = venda, Produto = produto
                //             } )
                //             .Where(cid => cid.Venda.cid == cid && )

                var query = from venda in ctx.Vendas
                            join produto in ctx.Produtos
                            on new {venda.codigo_produto} equals
                                new {produto.codigo}
                            where venda.cid = cid && venda.tid = tid
                            select new {venda.preco_venda, venda.quantidade, produto.descricao };

                while(true){
                    //var entity = query.GetFirst();
                    if (entity == null) break;
                    totalParcial = entity.preco_venda * entity.quantidade;
                    total += totalParcial;
                    Console.WriteLine(entity.descricao + " : " + totalParcial + '€');
                }

                Console.WriteLine("Total: " + total);
            }

        }
    }
}
