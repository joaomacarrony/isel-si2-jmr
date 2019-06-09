using SI2.Functions;
using System;
using System.Linq;
using System.Data.Entity;

namespace SI2
{
    class Teste
    {
        public static void Main()
        {
            //var ctx = new SI2Entities();
            //Franqueado franqueado = ctx.Franqueados.Find(1);

            //Console.WriteLine(franqueado.nome);
            //Console.ReadLine();

          //  FranqueadoUtils.Insert(111222333, "EF Teste 1", "Microsoft Avenue");

            using (var ctx = new SI2Entities())
            {
                /*
                var franq = ctx.Franqueados
                                .Where(f => f.nome == "EF Teste 1")
                                .FirstOrDefault<Franqueado>();

                Console.WriteLine(franq.nif);
                Console.WriteLine(franq.nome);
                Console.WriteLine(franq.morada);

                FranqueadoUtils.Update(franq.fid, (int)franq.nif, franq.nome, "Google Avenue");

                Console.WriteLine(franq.nif);
                Console.WriteLine(franq.nome);
                Console.WriteLine(franq.morada);

                FranqueadoUtils.Remove(franq.fid);


                ProcessoVenda proc = new ProcessoVenda(10, "João Dias");
                proc.FecharVenda(1, 1);
                */
               double y=MediaVendas.mediavendas(1);
                Console.WriteLine(y);
                var x=Console.ReadLine();
            }
        }
    }
}
