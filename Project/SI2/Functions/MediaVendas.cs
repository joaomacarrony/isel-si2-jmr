using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SI2.Functions
{
    class MediaVendas
    {

        public static double mediavendas(int productid)
        {
           var year1= DateTime.Today.Year.ToString();
           // System.DateTime year= new System.DateTime(2019);
            //var value= year.Year;
         
            Console.WriteLine(year1);
           // var year = DateTime.Now.Year;
            using (var ctx = new SI2Entities())
            {
               double x = 0;
                
              
                var query = from vendas in ctx.Vendas
                            where vendas.codigo_produto == productid    && vendas.data_venda.Value.Year==2019                           select new
                            {
                               data=vendas.data_venda.Value.Year,
                               franqueado=vendas.Franqueado.nome,
                               idfranqueado=vendas.Franqueado.fid,
                               codigoproduto=vendas.codigo_produto,
                              valorvenda=vendas.preco_venda
                            };

                foreach(var entity in query)
                {
                    x = x + (double)(entity.valorvenda);
                    Console.WriteLine(entity.data+" "+entity.franqueado+"codigo produto= "+entity.codigoproduto+"preço= "+entity.valorvenda);
                    

                }
                Console.WriteLine(x);

                return x;
                //  ctx.SaveChanges();
            }
        }

    }
}
