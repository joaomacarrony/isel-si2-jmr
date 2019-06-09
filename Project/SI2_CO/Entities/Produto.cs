using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SI2_CO.Entities
{
    public class Produto
    {
        public int codigo { get; set; }
        public string tipo { get; set; }
        public string descricao { get; set; }
		public int quantidade { get; set; }
		public int quantidade_minima { get; set; }
		public int quantidade_maxima { get; set; }
    }
}
