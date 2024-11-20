using GerenciamentoDeDespesas_2._1.Models;

namespace GerenciamentoDeDespesas_2._1.Helper
{
    // classe responsavel por alteticar usuario logado, e desconectar usuarioa logado quando clicar no botão sair.
    public interface ISessao
    {
        void CriarSessaoDoUsuario(Usuarios usuario);
        void RemoversessaoUsuario();
        Usuarios BuscarSessaoDoUsuario();
    }
}
