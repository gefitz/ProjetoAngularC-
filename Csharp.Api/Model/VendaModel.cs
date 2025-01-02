namespace Csharp.Api.Model
{
    public class VendaModel
    {
        public int IdVenda { get; set; }
        public ProdutoModel Produto { get; set; }
        public int qtdProdutoVendido { get; set; }
        public DateTime dthVenda { get; set; }
        public float vlrTotal { get; set; }
    }
}
