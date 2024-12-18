namespace Csharp.Api.Model
{
    public class ProdutoModel
    {
        public int IdProduto { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public TipoProdutoModel tpProduto { get; set; }
        public int qtdProduto { get; set; }
        public float vlrProduto { get; set; }
        public DateTime dthCriadoProduto { get; set; }
        public DateTime dthAlteracaoProduto { get; set; }
    }
}
