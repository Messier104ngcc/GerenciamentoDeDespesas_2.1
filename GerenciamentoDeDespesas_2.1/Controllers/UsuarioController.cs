using GerenciamentoDeDespesas_2._1.Filters;
using GerenciamentoDeDespesas_2._1.Manutenção.Logs_System;
using GerenciamentoDeDespesas_2._1.Models;
using GerenciamentoDeDespesas_2._1.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace GerenciamentoDeDespesas_2._1.Controllers
{
    [PaginaSomenteAdmin]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _db;
        private readonly IDespesasRepositorio _despesas;
        private readonly ILogs _logs;

        // buscanco o registro do banco
        public UsuarioController(IUsuarioRepositorio db, ILogs logs, IDespesasRepositorio despesasRepositorio)
        {
            _db = db;
            _logs = logs;
            _despesas = despesasRepositorio;
        }
        public IActionResult Index()
        {
            List<Usuarios> usuarios = _db.BuscarUsuario();
            return View(usuarios);
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        public IActionResult Remover(int id)
        {
            Usuarios usuario = _db.BuscarPorID(id);
            return View(usuario);
        }

        public IActionResult ListarDespesasPorUsuarioId(int id)
        {
            List<Despesas> despesas = _despesas.BuscarDespesas(id);
            return PartialView("_DespesasUsuario", despesas);
        }

        [HttpPost]
        public IActionResult Cadastrar(Usuarios usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Verificar se o usuário já existe pelo nome de usuário e email usando métodos de verificação
                    if (_db.UsuarioExistePorNome(usuario.UserName))
                    {
                        TempData["ErrorMessage"] = "Nome de usuário já existente.";
                        return View("Cadastrar");
                    }
                    if (_db.UsuarioExistePorEmail(usuario.Email))
                    {
                        TempData["ErrorMessage"] = "Email já cadastrado, tente outro email.";
                        return View("Cadastrar");
                    }
                    if (usuario.Senha != usuario.ConfSenha)
                    {
                        TempData["ErrorMessage"] = "Senhas não coincidem!";
                        return View("Cadastrar");
                    }

                    // Chamando o método CadastrarUsuario com o parâmetro correto
                    _db.CadastrarUsuario(usuario);
                    TempData["SuccessMessage"] = "Usuario Cadastrado com Sucesso.";

                    return RedirectToAction("Index"); // Redireciona para a página de gerenciamento
                }

                // Se houver erros, reexibir o formulário
                TempData["ErrorMessage"] = "Faltam campos a serem preenchidos.";
                return View("Cadastrar");
            }
            catch (Exception)
            {
                TempData["WarningMessage"] = "Erro inesperado. Contate o suporte.";
                return View("Index");
            }
        }

        [HttpPost, ActionName("Excluir")]
        public IActionResult Excluir(int id)
        {
            try
            {
                if (id == null) // condição para verificar se há informações salvas no banco
                {
                    TempData["WarningMessage"] = "⚠ Usuário não encontrada ou você não tem permissão para excluí-lo.";
                    return NoContent();
                }

                bool apagado = _db.Excluir(id);
            }
            catch (SqlException ex)
            {
                _logs.RegistrarLogDeExcecao(ex);
                TempData["WarningMessage"] = "Erro ao conectar ao servidor de dados. Verifique a conexão e tente novamente.";
                return View("Index");
            }
            catch (Exception ex)
            {
                _logs.RegistrarLogDeExcecao(ex);
                TempData["WarningMessage"] = "Erro inesperado. Contate o suporte.";
                return View("Index");
            }

            TempData["MensagemSucesso"] = "Cadastro excluido com sucesso!";
            return RedirectToAction("Index");
        }

        public IActionResult Editar(int id)
        {
            Usuarios ususario = _db.BuscarPorID(id);
            return View(ususario);
        }

        [HttpPost]
        public IActionResult Editar(UsuarioSemSenha usuarioSemSenha)
        {
            Usuarios usuario = null;

            if (ModelState.IsValid)
            {
                usuario = new Usuarios();
                {
                    usuario.UserId = usuarioSemSenha.UserId;
                    usuario.Nome = usuarioSemSenha.Nome;
                    usuario.UserName = usuarioSemSenha.UserName;
                    usuario.Email = usuarioSemSenha.Email;
                    usuario.Perfil = (Enum.PerfilEnum)usuarioSemSenha.Perfil;
                }

                usuario = _db.Atualizar(usuario); // atualizando as informações do banco

                TempData["SuccessMessage"] = "Alterações realizado com sucesso!"; // mensagem mostranado ao usuario que o cadastro deu certo.

                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Ocorreu algum erro de edição das informações!"; // mensagem de erro caso ocorra agum erro apois salvar as alterações.

            return RedirectToAction("Index");
        }
    }
}

