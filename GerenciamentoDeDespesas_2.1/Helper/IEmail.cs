namespace GerenciamentoDeDespesas_2._1.Helper
{
    public interface IEmail
    {
        bool Enviar(string email, string assunto, string mensagem);
    }
}
