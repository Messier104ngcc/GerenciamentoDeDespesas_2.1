
using GerenciamentoDeDespesas_2._1.Date;
using GerenciamentoDeDespesas_2._1.Filters;
using GerenciamentoDeDespesas_2._1.Helper;
using GerenciamentoDeDespesas_2._1.Manutenção.Logs_System;
using GerenciamentoDeDespesas_2._1.Models;
using GerenciamentoDeDespesas_2._1.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace GerenciamentoDeDespesas_2._1.Controllers
{
    [PaginaUsuarioLogado]
    public class DespesasController : Controller
    {
        private readonly IDespesasRepositorio _dbContextApp;
        private readonly ILogs _logs;
        private readonly ISessao _sessao;

        public DespesasController(IDespesasRepositorio dBContextApp, ILogs logs, ISessao sessao)
        {
            _dbContextApp = dBContextApp;
            _logs = logs;
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            Usuarios usuariosLogado = _sessao.BuscarSessaoDoUsuario();
            List<Despesas> despesas = _dbContextApp.BuscarDespesas(usuariosLogado.UserId);
            return View(despesas);
        }

        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Editar(int id)
        {
            Despesas despesas = _dbContextApp.BuscarPorID(id);
            return View(despesas);
        }

        public IActionResult Excluir(int id)
        {
            var despesa = _dbContextApp.BuscarPorID(id);
            return View(despesa);
        }

        [HttpPost] // tornando o metodo em post, assim será possivel que todas as informacçoes digitadas sejam salvas no banco de dados.
        public IActionResult Cadastrar(Despesas despesas)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Usuarios usuariosLogado = _sessao.BuscarSessaoDoUsuario();
                    despesas.UsuariosId = usuariosLogado.UserId;

                    _dbContextApp.CriarDespesa(despesas); // adicionado todas as informações no banco.
                    TempData["SuccessMessage"] = "Despesa cadastrada com sucesso!";
                    return RedirectToAction("Index"); // apos der tudo certo, retonara para tela Aluno/Index.
                }               
            }
            catch (Exception ex)
            {
                _logs.RegistrarLogDeExcecao(ex);
                TempData["WarningMessage"] = "Erro inesperado. Contate o suporte.";
                return View("Index", despesas);
            }

            TempData["ErrorMessage"] = "Ocorreu algum erro ao Cadastrar a despesa!";
            return View("Criar");
        }

        [HttpPost]
        public IActionResult Alterar(Despesas despesas) 
        {
            
            if (ModelState.IsValid)
            {
                Usuarios usuariosLogado = _sessao.BuscarSessaoDoUsuario();
                despesas.UsuariosId = usuariosLogado.UserId;

                _dbContextApp.Atualizar(despesas);

                TempData["SuccessMessage"] = "Alterações realizado com sucesso!"; // mensagem mostranado ao usuario que o cadastro deu certo.

                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Ocorreu algum erro de edição das informações!"; // mensagem de erro caso ocorra agum erro apois salvar as alterações.

            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Excluir")]
        public IActionResult Apagar(int id) 
        {
            try
            {
                bool apagado = _dbContextApp.Excluir(id);

                if (apagado)
                {
                    TempData["MensagemSucesso"] = " Registro excluido com sucesso!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Despesa não encontrada ou você não tem permissão para excluí-la.";
                }

                return View("Index");
            }
            catch(Exception) 
            {
                TempData["WarningMessage"] = "Erro inesperado. Contate o suporte.";
                return RedirectToAction("Index");
            }                       
        }

        public IActionResult Pagar(int id)
        {
            try
            {
                bool despesa = _dbContextApp.Paga(id);

                if (id == null) // condição para verificar se há informações salvas no banco
                {
                    TempData["WarningMessage"] = "Registro não encontrada ou você não tem permissão para excluí-lo.";
                    return NoContent();
                }

                TempData["SuccessMessage"] = "Pagamento realizado com sucesso!"; // mensagem mostranado ao usuario que o pagamento deu certo.

                return RedirectToAction("Index");

            }
            catch (SqlException ex)
            {
                _logs.RegistrarLogDeExcecao(ex);
                TempData["WarningMessage"] = "Erro ao conectar ao servidor de dados. Verifique a conexão e tente novamente.";
                return View();
            }
            catch (Exception ex)
            {
                _logs.RegistrarLogDeExcecao(ex);
                TempData["WarningMessage"] = "Erro inesperado. Contate o suporte.";
                return View();
            }           
        }
    }   

}
