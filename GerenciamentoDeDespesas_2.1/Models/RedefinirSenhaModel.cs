using System.ComponentModel.DataAnnotations;

namespace GerenciamentoDeDespesas_2._1.Models
{
    public class RedefinirSenhaModel
    {
        [Required(ErrorMessage = "⚠")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "⚠")]
        public string Email { get; set; }
    }
}
