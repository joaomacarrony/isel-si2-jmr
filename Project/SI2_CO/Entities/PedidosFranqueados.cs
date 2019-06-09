namespace SI2_CO.Entities
{
    public class PedidosFranqueados
    {
        public int pfid { get; set; }
        public int fid { get; set; }
        public int codigo_produto { get; set; }
        public int quantidade { get; set; }

        public virtual Franqueado Franqueado { get; set; }
        public virtual Produto Produto { get; set; }
    }
}
