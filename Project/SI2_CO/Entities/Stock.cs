namespace SI2_CO.Entities
{
    public class Stock
    {
        public int codigo_produto { get; set; }
        public int preco { get; set; }
        public int quantidade { get; set; }
        public int quantidade_minima { get; set; }
        public int quantidade_maxima { get; set; }
        public int fid { get; set; }

        public virtual Franqueado Franqueado { get; set; }
        public virtual Produto Produto { get; set; }
    }
}
