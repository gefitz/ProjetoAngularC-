using System.ComponentModel.DataAnnotations;

namespace Csharp.Api.DTO
{
    public class ProdutoDTO
    {
        public int idProduto { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Required]
        public TipoProdutoDTO tpProduto { get; set; }
        [Required]
        public int qtdProduto { get; set; }
        [Required]
        public float vlrProduto { get; set; }
        public DateTime dthCriadoProduto { get; set; }
        public DateTime dthAlteracaoProduto { get; set; }
    }
}
