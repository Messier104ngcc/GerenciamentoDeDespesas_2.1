using GerenciamentoDeDespesas_2._1.Helper;
using GerenciamentoDeDespesas_2._1.Manutenção.Logs_System;
using GerenciamentoDeDespesas_2._1.Models;
using GerenciamentoDeDespesas_2._1.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace GerenciamentoDeDespesas_2._1.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _db;
        private readonly ISessao _sessao;
        private readonly IEmail _email;
        private readonly ILogs _logs;

        public LoginController(IUsuarioRepositorio db, ISessao sessao, IEmail email, ILogs logs)
        {
            _db = db;
            _sessao = sessao;
            _email = email;
            _logs = logs;
        }

        // Exibir a página de login
        [HttpGet]
        public IActionResult Index()
        {          
            try
            {
                // Caso o usuario sair do sistem e entre novamente, verificar se o usuario já tem uma
                // sessão, dai apenas redireciona para tela home, sem nessecitar logar novamente.
                if (_sessao.BuscarSessaoDoUsuario() != null)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View("Index");
                }
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

        public IActionResult RedefinirSenha()
        {
            return View();
        }

        // Processa o login do usuário
        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    Usuarios usuario = _db.BuscarPorLogin(loginModel.UserName);

                    if (usuario != null)
                    {

                        if (usuario.SenhaValida(loginModel.Senha))
                        {
                            // criando um sessão para o usuario antes de logar no sistema.
                            _sessao.CriarSessaoDoUsuario(usuario);

                            // Redirecionar para a página inicial do sistema após login
                            return RedirectToAction("Index", "Home");
                        }

                        // Exibe a mensagem de erro se o login for inválido
                        TempData["ErrorMessage"] = "Usuário ou senha inválidos!";
                        return View("Index");

                    }
                }

                // Se houver erros, reexibir o formulário
                TempData["ErrorMessage"] = "Digite um Usuario e Senha.";
                return View("Index");

            }
            catch(SqlException ex)
            {
                _logs.RegistrarLogDeExcecao(ex);
                TempData["WarningMessage"] = "Erro ao conectar ao banco de dados. Verifique a conexão e tente novamente.";
                return View("Index");
            }
            catch (Exception ex)
            {
                _logs.RegistrarLogDeExcecao(ex);
                TempData["WarningMessage"] = "Erro inesperado. Contate o suporte.";
                return View("Index");
            }
        }

        [HttpPost] 
        public IActionResult EnviarLinkPsenha(RedefinirSenhaModel redefinirSenhaModel)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    Usuarios usuario = _db.BuscarLoginEemail(redefinirSenhaModel.Email, redefinirSenhaModel.UserName);

                    if (usuario != null)
                    {
                        string novaSenha = usuario.GerarNovaSenha();                      
                        string mensagem = $"Sua nova senha é : {novaSenha}";

                        bool emailEnviado =_email.Enviar(usuario.Email, "Sistema de Despesas - Nova Senha", mensagem);

                        if (emailEnviado)
                        {
                            _db.Atualizar(usuario);
                            TempData["SuccessMessage"] = " Nova senha enviada para seu E-mail cadastrado.";
                        }
                        else
                        {
                            TempData["ErrorMessage"] = " Sem sucesso no envio do email, tente novamente.";
                        }
                       
                        return RedirectToAction("Index","Login");

                    }
                }

                TempData["ErrorMessage"] = " Sem sucesso na redefinição de senha, verifique os dados informados.";
                return View("RedefinirSenha");

            }
            catch (Exception ex)
            {
                _logs.RegistrarLogDeExcecao(ex);
                TempData["WarningMessage"] = " Erro ao redefinir senha. Contate o suporte.";
                return View("RedefinirSenha");
            }
        }

        // Logout do usuário
        public IActionResult Sair()
        {
            _sessao.RemoversessaoUsuario();

            return RedirectToAction("Index", "Login");
        }

    }
}
