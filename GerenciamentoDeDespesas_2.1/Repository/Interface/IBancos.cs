using GerenciamentoDeDespesas_2._1.Models;

namespace GerenciamentoDeDespesas_2._1.Repository.Interface
{
    public interface IBancos
    {
        List<Conta_Bancaria> BuscarConta(int usuarioId);
        Conta_Bancaria BuscarPorID(int id);
        Conta_Bancaria Depositar(Conta_Bancaria conta);
        bool ContaExistePorNome(string banco);
    }
}
