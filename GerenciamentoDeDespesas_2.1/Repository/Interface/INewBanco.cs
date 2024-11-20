using GerenciamentoDeDespesas_2._1.Models;

namespace GerenciamentoDeDespesas_2._1.Repository.Interface
{
    public interface INewBanco
    {
        BancoModel Cadastrar(BancoModel banco);

        bool BancoExistePorNome(string nome);
    }
}
