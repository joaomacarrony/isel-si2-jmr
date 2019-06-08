using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SI2.Functions
{
    class StockUtils
    {
        public static void Insert(int produto, float preco, int quant, int quantMinima, int quantMaxima, int fid)
        {
            using (var ctx = new SI2Entities())
            {
                ctx.Stocks.Add(new Stock()
                {
                    codigo_produto = produto,
                    preco = preco,
                    quantidade = quant,
                    quantidade_minima = quantMinima,
                    quantidade_maxima = quantMaxima,
                    fid = fid
                });

                ctx.SaveChanges();
            }
        }
    }
}
