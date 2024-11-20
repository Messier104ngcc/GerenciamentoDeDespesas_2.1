using GerenciamentoDeDespesas_2._1.Date;
using GerenciamentoDeDespesas_2._1.Filters;
using GerenciamentoDeDespesas_2._1.Helper;
using GerenciamentoDeDespesas_2._1.Models;
using GerenciamentoDeDespesas_2._1.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Diagnostics;

namespace GerenciamentoDeDespesas_2._1.Controllers
{
    [PaginaUsuarioLogado]
    public class HomeController : Controller
    {
        private readonly IDespesasRepositorio _dbContextApp;
        private readonly ISessao _sessao;

        public HomeController(IDespesasRepositorio dBContextApp, ISessao sessao)
        {
            _dbContextApp = dBContextApp;
            _sessao = sessao;
        }
        public IActionResult Index()
        {
            try
            {
                Usuarios usuariosLogado = _sessao.BuscarSessaoDoUsuario();
                var home = _dbContextApp.BuscarDespesas(usuariosLogado.UserId);
                return View(home);
            }
            catch (SqlException)
            {
                return StatusCode(500, "Ocorreu um erro ao processar sua solicitação. Tente novamente mais tarde.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro inesperado. Contate o suporte.");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
