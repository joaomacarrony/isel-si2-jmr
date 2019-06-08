using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SI2.Functions
{
    class FranqueadoUtils
    {
        public static void Insert(int nif, string nome, string morada)
        {
            using (var ctx = new SI2Entities())
            {
                ctx.Franqueados.Add(new Franqueado()
                {
                    nif = nif,
                    nome = nome,
                    morada = morada
                });

                ctx.SaveChanges();
            }
        }

        public static void Update(int fid, int nif, string nome, string morada)
        {
            using (var ctx = new SI2Entities())
            {
                Franqueado franq = ctx.Franqueados.Find(fid);
                franq.nif = nif;
                franq.nome = nome;
                franq.morada = morada;
                ctx.SaveChanges();
            }
        }

        public static void Remove(int fid)
        {
            using (var ctx = new SI2Entities())
            {
                Franqueado franq = ctx.Franqueados.Find(fid);
                ctx.Franqueados.Remove(franq);
                ctx.SaveChanges();
            }
        }

        public static void ForçarRemoçãoFranqueado(int fid)
        {
            using (var ctx = new SI2Entities())
            {
                EntregasFranqueado entregas = ctx.EntregasFranqueados.Find(fid);
                Stock stock = ctx.Stocks.Find(fid);
                Venda venda = ctx.Vendas.Find(fid);
                HistoricoVenda historicoVenda = ctx.HistoricoVendas.Find(fid);
                PedidosFranqueado pedidosFranqueados = ctx.PedidosFranqueados.Find(fid);
                Franqueado franqueado = ctx.Franqueados.Find(fid);

                ctx.EntregasFranqueados.Remove(entregas);
                ctx.Stocks.Remove(stock);
                ctx.Vendas.Remove(venda);
                ctx.HistoricoVendas.Remove(historicoVenda);
                ctx.PedidosFranqueados.Remove(pedidosFranqueados);
                ctx.Franqueados.Remove(franqueado);

                ctx.SaveChanges();
            }
        }
    }
}
