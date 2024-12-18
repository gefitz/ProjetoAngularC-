namespace Csharp.Api.DTO
{
    public class VendaDTO
    {
        public int idVenda { get; set; }
        public List<ProdutoDTO> idProduto { get; set; }
        public int qtdVendida { get; set; }
        public DateTime dthVenda { get; set; }
    }
}
