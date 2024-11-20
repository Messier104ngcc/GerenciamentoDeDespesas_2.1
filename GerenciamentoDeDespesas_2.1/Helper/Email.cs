using GerenciamentoDeDespesas_2._1.Manutenção.Logs_System;
using System.Net;
using System.Net.Mail;

namespace GerenciamentoDeDespesas_2._1.Helper
{
    // classe resposanvel pelo envio do email com a redefinição de senha.
    public class Email : IEmail
    {
        private readonly IConfiguration _config;
        private readonly ILogs _logs;

        public Email(IConfiguration config, ILogs logs)
        {
            _config = config;
            _logs = logs;
        }

        public bool Enviar(string email, string assunto, string mensagem)
        {
            try
            {
                string host = _config.GetValue<string>("SMTP:Host");
                string nome = _config.GetValue<string>("SMTP:Nome");
                string username = _config.GetValue<string>("SMTP:UserName");
                string senha = _config.GetValue<string>("SMTP:Senha");
                int porta = _config.GetValue<int>("SMTP:Porta");

                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(username, nome)
                };

                mail.To.Add(email);
                mail.Subject = assunto;
                mail.Body = mensagem;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                using (SmtpClient smtp = new SmtpClient(host, porta))
                {
                    smtp.Credentials = new NetworkCredential(username, senha);
                    smtp.EnableSsl = true;


                    smtp.Send(mail);
                    return true;
                }
            }
            catch (Exception ex) 
            {
                // Chama o método para registrar a exceção no log
                _logs.RegistrarLogDeExcecao(ex);
                return false;
            }
        }
    }
}
