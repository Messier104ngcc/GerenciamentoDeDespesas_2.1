using GerenciamentoDeDespesas_2._1.Helper;
using GerenciamentoDeDespesas_2._1.Manutenção.Logs_System;
using GerenciamentoDeDespesas_2._1.Models;
using GerenciamentoDeDespesas_2._1.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace GerenciamentoDeDespesas_2._1.Controllers
{
    public class AlterarSenhaController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;
        private readonly ILogs _logs;

        public AlterarSenhaController(ILogs logs, IUsuarioRepositorio usuarioRepositorio, ISessao sessao)
        {
            _logs = logs;
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Alterar(AlterarSenhaModel alterarSenhaModel)
        {
            try
            {
                Usuarios usuariosLogado = _sessao.BuscarSessaoDoUsuario();
                alterarSenhaModel.Id = usuariosLogado.UserId;

                if (ModelState.IsValid)
                {
                    _usuarioRepositorio.AlterarSenha(alterarSenhaModel);
                    TempData["SuccessMessage"] = "Senha alterada com Sucesso.";
                    return View("Home");                                                  
                }

                return View("Index", alterarSenhaModel);

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
        }
    }
}
