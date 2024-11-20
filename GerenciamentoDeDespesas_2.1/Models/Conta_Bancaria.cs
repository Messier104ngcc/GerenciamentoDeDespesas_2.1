using System.ComponentModel.DataAnnotations;

namespace GerenciamentoDeDespesas_2._1.Models
{
    public class Conta_Bancaria
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Sobrenome { get; set; }

        [Required]
        public string Email { get; set; }

        public string? Observacao { get; set; }

        [Required]
        public decimal Deposito { get; set; }

        public decimal Saque { get; set; }

        public DateTime DataHora { get; set; } = DateTime.Now;

        public int? UsuariosId { get; set; }
        public int? BancoId { get; set; }

        public Usuarios? Usuarios { get; set; }
    }
}
