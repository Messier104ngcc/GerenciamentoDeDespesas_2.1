using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace GerenciamentoDeDespesas_2._1.Models
{
    public class Despesas
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "⚠")] // mensagem de erro caso o usuario tente adicionar os campos sem valores.
        public string Despesa { get; set; }

        public string? Observacao { get; set; }

        [Required(ErrorMessage = "⚠")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "⚠")]
        public DateTime Vencimento { get; set; }

        public string? Paga { get; set; }

        public int? UsuariosId { get; set; }

        public Usuarios? Usuarios { get; set; }

    }
}
