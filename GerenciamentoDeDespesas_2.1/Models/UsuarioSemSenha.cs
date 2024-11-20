using GerenciamentoDeDespesas_2._1.Enum;
using System.ComponentModel.DataAnnotations;

namespace GerenciamentoDeDespesas_2._1.Models
{
    public class UsuarioSemSenha
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "⚠")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "⚠")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "⚠")]
        public string Email { get; set; }

        [Required(ErrorMessage = "⚠")]
        public PerfilEnum? Perfil { get; set; }
    }
}
