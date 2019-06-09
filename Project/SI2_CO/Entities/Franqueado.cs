using SI2;
using System.Collections.Generic;


namespace SI2_CO.Entities
{
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
