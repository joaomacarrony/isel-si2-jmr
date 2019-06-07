using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace SI2_CO.Entities
{
    [Table(Name = "Fornecedor")]
    public class Fornecedor
    {
        public int Foid { get; set; }
        public int Nif { get; set; }
        public string Nome { get; set; }
    }
}
