using GerenciamentoDeDespesas_2._1.Date;
using GerenciamentoDeDespesas_2._1.Models;
using GerenciamentoDeDespesas_2._1.Repository.Interface;

namespace GerenciamentoDeDespesas_2._1.Repository
{
    public class NewBancoRepositorio : INewBanco
    {
        private readonly DBContextApp _db;

        public NewBancoRepositorio(DBContextApp _db)
        {
            this._db = _db;
        }

        public bool BancoExistePorNome(string nome)
        {
            return _db.BancoModels.Any(t => t.Nome == nome);
        }

        public BancoModel Cadastrar(BancoModel banco)
        {
            _db.Bancos.Add(banco);
            _db.SaveChanges();
            return banco;
        }
    }
}
