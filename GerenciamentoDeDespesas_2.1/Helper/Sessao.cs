using GerenciamentoDeDespesas_2._1.Models;
using Newtonsoft.Json;

namespace GerenciamentoDeDespesas_2._1.Helper
{
    public class Sessao : ISessao
    {
        private readonly IHttpContextAccessor _httpContext;

        public Sessao(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public Usuarios BuscarSessaoDoUsuario()
        {
            // busca um sessão da string sessão
            string sessaoUsuario = _httpContext.HttpContext.Session.GetString("sessaoUsuarioLogado");

            // condição para verificar 
            if (string.IsNullOrEmpty(sessaoUsuario))
            {
                return null;
            }
            else
            {
                return JsonConvert.DeserializeObject<Usuarios>(sessaoUsuario);
            }
        }

        public void CriarSessaoDoUsuario(Usuarios usuario)
        {
            // converte todas as informações do usuario em string
            string valor = JsonConvert.SerializeObject(usuario);

            // devolve essa informações comvertidas para criar um sessão do usuario.
            _httpContext.HttpContext.Session.SetString("sessaoUsuarioLogado", valor);
        }

        public void RemoversessaoUsuario()
        {
            _httpContext.HttpContext.Session.Remove("sessaoUsuarioLogado");
        }
    }
}
