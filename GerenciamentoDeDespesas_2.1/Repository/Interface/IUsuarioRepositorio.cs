using GerenciamentoDeDespesas_2._1.Models;

namespace GerenciamentoDeDespesas_2._1.Repository.Interface
{
    public interface IUsuarioRepositorio
    {
        Usuarios BuscarPorLogin(string username);

        Usuarios BuscarLoginEemail(string email, string username);

        List<Usuarios> BuscarUsuario();

        Usuarios BuscarPorID(int id);

        Usuarios CadastrarUsuario(Usuarios usuarios);

        Usuarios Atualizar(Usuarios usuario);

        Usuarios AlterarSenha(AlterarSenhaModel alterarSenhaModel);

        bool Excluir(int id);

        bool UsuarioExistePorNome(string userName);
        bool UsuarioExistePorEmail(string email);
    }
}
