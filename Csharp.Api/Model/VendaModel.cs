namespace Csharp.Api.Model
{
    public class VendaModel
    {
        public int IdVenda { get; set; }
        public List<ProdutoModel> Produto { get; set; }
        public int qtdVendida { get; set; }
        public DateTime dthVenda { get; set; }
    }
}
