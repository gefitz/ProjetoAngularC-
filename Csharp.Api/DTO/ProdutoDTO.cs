using System.ComponentModel.DataAnnotations;

namespace Csharp.Api.DTO
{
    public class ProdutoDTO
    {
        public int idProduto { get; set; }
        public string? nome { get; set; }
        
        public string? descricao { get; set; }
        public string? tpProduto { get; set; }
        public int qtdProduto { get; set; }
        public float vlrProduto { get; set; }
        public DateTime dthAlteracaoProduto { get; set; }
        public DateTime dthCriacaoProduto { get; set; }
    }
}
