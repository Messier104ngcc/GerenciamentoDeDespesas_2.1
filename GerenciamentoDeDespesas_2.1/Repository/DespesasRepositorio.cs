using GerenciamentoDeDespesas_2._1.Date;
using GerenciamentoDeDespesas_2._1.Models;
using GerenciamentoDeDespesas_2._1.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoDeDespesas_2._1.Repository
{
    public class DespesasRepositorio : IDespesasRepositorio
    {
        private readonly DBContextApp _dbContextApp;

        public DespesasRepositorio(DBContextApp dBContextApp) 
        {
            _dbContextApp = dBContextApp;
        }
        public Despesas Atualizar(Despesas despesa)
        {
            _dbContextApp.Despesas.Update(despesa);
            _dbContextApp.SaveChanges();

            return despesa;
        }

        public List<Despesas> BuscarDespesas(int usuarioId)
        {
            return _dbContextApp.Despesas.Where(x => x.UsuariosId == usuarioId).ToList();
        }

        public Despesas BuscarPorID(int id)
        {
            return _dbContextApp.Despesas.FirstOrDefault(x => x.Id == id);
        }

        public Despesas CriarDespesa(Despesas despesa)
        {
            _dbContextApp.Despesas.Add(despesa);
            _dbContextApp.SaveChanges();
            return despesa;
        }

        public bool Excluir(int id)
        {
            Despesas despesaDB = BuscarPorID(id);

            _dbContextApp.Despesas.Remove(despesaDB);
            _dbContextApp.SaveChanges();

            return true;
        }

        public bool Paga(int id)
        {
            Despesas despesa = BuscarPorID(id);

            if (despesa != null)
            {
                // Define o valor de 'Paga' como "SIM" antes de atualizar no banco
                despesa.Paga = "Sim";
            }

            _dbContextApp.Despesas.Update(despesa);
            _dbContextApp.SaveChanges();

            return true;
        }
    }
}
