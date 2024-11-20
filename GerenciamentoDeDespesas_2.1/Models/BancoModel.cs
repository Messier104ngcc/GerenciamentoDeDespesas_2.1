using System.ComponentModel.DataAnnotations;

namespace GerenciamentoDeDespesas_2._1.Models
{
    public class BancoModel
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public DateTime DataCadastro { get; set; }
    }
}
