using GerenciamentoDeDespesas_2._1.Date;
using GerenciamentoDeDespesas_2._1.Models;
using GerenciamentoDeDespesas_2._1.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoDeDespesas_2._1.Repository
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly DBContextApp _db;

        public UsuarioRepositorio(DBContextApp db)
        {
            this._db = db;
        }

        public Usuarios BuscarPorLogin(string userName)
        {
            return _db.Usuarios.FirstOrDefault(t => t.UserName.ToUpper() == userName.ToUpper()); //ToUpper converter uma string (cadeia de caracteres) para letras maiúsculas. Quando você chama esse método em uma string, ele retorna uma nova string onde todos os caracteres alfabéticos foram convertidos para sua forma maiúscula.
        }

        public Usuarios BuscarLoginEemail(string email, string username)
        {
            return _db.Usuarios.FirstOrDefault(t => t.Email.ToUpper() == email.ToUpper() && t.UserName.ToUpper() == username.ToUpper());
        }

        // comando para buscar pelo Id no dados do banco.
        public Usuarios BuscarPorID(int id)
        {

            return _db.Usuarios.FirstOrDefault(t => t.UserId == id);

        }

        // comando para buscar os dados do banco.
        public List<Usuarios> BuscarUsuario()
        {
            return _db.Usuarios
                .Include(x => x.Despesas) // comando que inclui o total de despesas para cada usuario.
                .ToList();
        }

        // verificações no cadastro do Usuário
        public bool UsuarioExistePorNome(string userName)
        {
            return _db.Usuarios.Any(t => t.UserName == userName);
        }

        public bool UsuarioExistePorEmail(string email)
        {
            return _db.Usuarios.Any(t => t.Email == email);
        }

        //responsavel por inserir os dados no banco
        public Usuarios CadastrarUsuario(Usuarios usuario)
        {
            usuario.DataCadastro = DateTime.Now;
            usuario.SetSenhaHash(); // chamando o metodo para gerar o HASH antes de salva no BD.
            _db.Usuarios.Add(usuario); //inseri os dados no banco.
            _db.SaveChanges(); //salva os dados no bando.
            return usuario;
        }

        public Usuarios Atualizar(Usuarios usuario)
        {
            Usuarios usuarioDb = BuscarPorID(usuario.UserId);

            if (usuarioDb == null) throw new Exception("Houve um erro na Atualização do Usuario!");

            usuarioDb.Nome = usuario.Nome;
            usuarioDb.Email = usuario.Email;
            usuarioDb.UserName = usuario.UserName;
            usuarioDb.Perfil = usuario.Perfil;
            usuarioDb.DataAtualizacao = DateTime.Now;

            _db.Usuarios.Update(usuarioDb);
            _db.SaveChanges();

            return usuarioDb;
        }

        public Usuarios AlterarSenha(AlterarSenhaModel alterarSenhaModel)
        {
            Usuarios usuarioDb = BuscarPorID(alterarSenhaModel.Id);

            if(usuarioDb == null) throw new Exception("Houve um erro na atualização da senha, usuário não encontrado!");

            if (!usuarioDb.SenhaValida(alterarSenhaModel.SenhaAtual)) throw new Exception("Senha atual não confere!");

            if (usuarioDb.SenhaValida(alterarSenhaModel.NovaSenha)) throw new Exception("Nova senha deve ser diferente a senha atual!");

            usuarioDb.SetNovaSenha(alterarSenhaModel.NovaSenha);
            usuarioDb.DataAtualizacao = DateTime.Now;

            _db.Usuarios.Update(usuarioDb);
            _db.SaveChanges();

            return usuarioDb;
        }

        public bool Excluir(int id)
        {
            Usuarios usuarioDb = BuscarPorID(id);

            _db.Usuarios.Remove(usuarioDb);
            _db.SaveChanges();

            return true;
        }
    }
}
