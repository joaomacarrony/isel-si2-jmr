using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace SI2_CO.Entities
{
    [Table(Name = "Franqueado")]
    public class Franqueado
    {
        public Franqueado()
        {
            this.EntregasFranqueados = new List<EntregasFranqueado>();
            this.HistoricoVendas = new List<HistoricoVenda>();
            this.PedidosFranqueados = new List<PedidosFranqueado>();
            this.Stocks = new List<Stock>();
            this.Vendas = new List<Venda>();
        }

        public int fid { get; set; }
        public int nif { get; set; }
        public string nome { get; set; }
        public string morada { get; set; }

        public virtual List<EntregasFranqueado> EntregasFranqueados { get; set; }
        public virtual List<HistoricoVenda> HistoricoVendas { get; set; }
        public virtual List<PedidosFranqueado> PedidosFranqueados { get; set; }
        public virtual List<Stock> Stocks { get; set; }
        public virtual List<Venda> Vendas { get; set; }
    }
}
