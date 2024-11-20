using GerenciamentoDeDespesas_2._1.Enum;
using GerenciamentoDeDespesas_2._1.Helper;
using System.ComponentModel.DataAnnotations;

namespace GerenciamentoDeDespesas_2._1.Models
{
    public class Usuarios
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
        public PerfilEnum Perfil { get; set; }

        [Required(ErrorMessage = "⚠")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "⚠")]
        public string ConfSenha { get; set; }

        public DateTime DataCadastro { get; set; } = DateTime.Now;

        public DateTime? DataAtualizacao { get; set; } = DateTime.Now;

        public virtual List<Despesas>? Despesas { get; set; }


        // verifica se a senha está correta com a que está salva no banco de dados.
        public bool SenhaValida(string senha)
        {
            return Senha == senha.GerarHash();
        }

        // metodo para criar o Hash na senha do usuario
        public void SetSenhaHash()
        {
            Senha = Senha.GerarHash();
            ConfSenha = ConfSenha.GerarHash();
        }

        // metodo para atualizar senha e já gerar o Hash para nova senha.
        public void SetNovaSenha(string novaSenha)
        {
            Senha = novaSenha.GerarHash();
        }

        // metodo para gerar um nova senha para o usuario, quando ele redefinir.
        public string GerarNovaSenha()
        {
            string novaSenha = Guid.NewGuid().ToString().Substring(0, 8);
            Senha = novaSenha.GerarHash();
            return novaSenha;
        }
    }
}
