namespace Mercadinho.Prateleira.Domain
{
    public class Estoque
    {
        public int Id { get; set; }
        public Produto Produto { get; set; }
        public int ProdutoId { get; set; }
        public Qualitativo InfoCompra { get; set; }
    }
}
