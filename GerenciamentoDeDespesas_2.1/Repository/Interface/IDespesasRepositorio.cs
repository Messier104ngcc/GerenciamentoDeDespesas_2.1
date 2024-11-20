using GerenciamentoDeDespesas_2._1.Models;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoDeDespesas_2._1.Repository.Interface
{
    public interface IDespesasRepositorio
    {
        List<Despesas> BuscarDespesas(int usuarioId);

        Despesas BuscarPorID(int id);

        Despesas CriarDespesa(Despesas despesa);

        Despesas Atualizar(Despesas despesa);

        bool Excluir(int id);

        bool Paga(int id);
    }
}
