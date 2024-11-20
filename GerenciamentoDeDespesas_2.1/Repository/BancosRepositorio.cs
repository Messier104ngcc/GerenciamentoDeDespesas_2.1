using GerenciamentoDeDespesas_2._1.Date;
using GerenciamentoDeDespesas_2._1.Models;
using GerenciamentoDeDespesas_2._1.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoDeDespesas_2._1.Repository
{
    public class BancosRepositorio : IBancos
    {
        private readonly DBContextApp _db;

        public BancosRepositorio(DBContextApp _db)
        {
            this._db = _db;
        }

        public List<Conta_Bancaria> BuscarConta(int usuarioId)
        {
            return _db.Conta_Bancaria.Where(x => x.UsuariosId == usuarioId).ToList();
        }

        public Conta_Bancaria BuscarPorID(int id)
        {
            return _db.Conta_Bancaria.FirstOrDefault(x => x.Id == id);
        }

        public bool ContaExistePorNome(string banco)
        {
            throw new NotImplementedException();
        }

        //public bool ContaExistePorNome(string banco)
        //{
        //    return _db.Conta_Bancaria.Any(t => t.Banco == banco);
        //}

        public Conta_Bancaria Depositar(Conta_Bancaria conta)
        {
            conta.DataHora = DateTime.Now;
            conta.Deposito += conta.Deposito;
            _db.SaveChanges();
            return conta;
        }
    }
}
