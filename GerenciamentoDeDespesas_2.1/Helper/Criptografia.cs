using System.Security.Cryptography;
using System.Text;

namespace GerenciamentoDeDespesas_2._1.Helper
{
    // classe de camada de segurança (Senha).
    public static class Criptografia
    {
        // metodo para gerar o has, para criptografar a senha do usuario no BD.
        public static string GerarHash(this string valor)
        {
            var hash = SHA1.Create();
            var encoding = new ASCIIEncoding();
            var array = encoding.GetBytes(valor);

            var strHexa = new StringBuilder();

            foreach (var item in array)
            {
                strHexa.Append(item.ToString("x2"));
            }

            return strHexa.ToString();
        }
    }
}
