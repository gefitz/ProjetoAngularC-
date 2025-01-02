namespace Csharp.Api.DTO
{
    public class VendaDTO
    {
        public int idVenda { get; set; }
        public ProdutoDTO Produto { get; set; }
        public int qtdProdutoVendido { get; set; }
        public float vlrTotal { get; set; }
        public DateTime dthVenda { get; set; }
    }
}
