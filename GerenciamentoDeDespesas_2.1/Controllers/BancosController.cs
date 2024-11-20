using GerenciamentoDeDespesas_2._1.Helper;
using GerenciamentoDeDespesas_2._1.Manutenção.Logs_System;
using GerenciamentoDeDespesas_2._1.Models;
using GerenciamentoDeDespesas_2._1.Repository;
using GerenciamentoDeDespesas_2._1.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GerenciamentoDeDespesas_2._1.Controllers
{
    public class BancosController : Controller
    {
        private readonly IBancos _dbContextApp;
        private readonly ILogs _logs;
        private readonly ISessao _sessao;
        private readonly INewBanco _banco;
        public BancosController(IBancos dBContextApp, ILogs logs, ISessao sessao, INewBanco newbanco)
        {
            _dbContextApp = dBContextApp;
            _logs = logs;
            _sessao = sessao;
            _banco = newbanco;
        }
        public IActionResult Index()
        {
            try
            {
                ViewModel model = new ViewModel();
                Usuarios usuariosLogado = _sessao.BuscarSessaoDoUsuario();
                List<Conta_Bancaria> conta = _dbContextApp.BuscarConta(usuariosLogado.UserId);
                return View(model);
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

        //[HttpPost]
        //public IActionResult Deposito(Conta_Bancaria contasBancaria)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            if (_dbContextApp.ContaExistePorNome(contasBancaria.Banco))
        //            {
        //                Usuarios usuariosLogado = _sessao.BuscarSessaoDoUsuario();
        //                contasBancaria.UsuariosId = usuariosLogado.UserId;

        //                _dbContextApp.Depositar(contasBancaria); // adicionado todas as informações no banco.
        //                TempData["SuccessMessage"] = "Deposito realizado com sucesso!";
        //                return RedirectToAction("Index"); // apos der tudo certo, retonara para tela Aluno/Index.
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logs.RegistrarLogDeExcecao(ex);
        //        TempData["WarningMessage"] = "Erro inesperado. Contate o suporte.";
        //        return View("Index", contasBancaria);
        //    }

        //    TempData["ErrorMessage"] = "Ocorreu algum erro no deposito!";
        //    return View("Index");
        //}

        [HttpPost]
        public IActionResult Cadastrar(BancoModel nome)
        {           
            try
            {
                if (ModelState.IsValid)
                {
                    _banco.Cadastrar(nome);
                    TempData["SuccessMessage"] = "Cadastro realizado com sucesso!";
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                _logs.RegistrarLogDeExcecao(ex);
                TempData["WarningMessage"] = "Erro inesperado. Contate o suporte.";
                return View("Index", nome);
            }


            TempData["ErrorMessage"] = "Ocorreu algum erro no deposito!";
            return View("Index");
        }

    }
}
