using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SI2_CO.Entities
{
    public class Vendas
    {
        public int vid { get; set; }
        public int fid                      { get; set; }
	public int cid                      { get; set; }
	public int tid                      { get; set; }
	public int codigo_produto           { get; set; }
	public DateTime data_venda          { get; set; }
	public float preco_venda            { get; set; }
	public int quantidade               { get; set; }
        }
}
