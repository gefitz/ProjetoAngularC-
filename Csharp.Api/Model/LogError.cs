using System.ComponentModel.DataAnnotations;

namespace Csharp.Api.Model
{
    public class LogError
    {
        [Key]
        public int IdLog { get; set; }
        public string Messagem { get; set; }
        public DateTime dthErro { get; set; }
    }
}
