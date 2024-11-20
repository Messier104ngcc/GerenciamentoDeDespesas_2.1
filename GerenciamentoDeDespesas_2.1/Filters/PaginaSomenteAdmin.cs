using GerenciamentoDeDespesas_2._1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace GerenciamentoDeDespesas_2._1.Filters
{
    public class PaginaSomenteAdmin : ActionFilterAttribute
    {
        // metodo que verfica se o usuario está logado como Admin.
        public override void OnActionExecuting(ActionExecutingContext context)
		{
            string sessaoUsuario = context.HttpContext.Session.GetString("sessaoUsuarioLogado");

            if(string.IsNullOrEmpty(sessaoUsuario) ) 
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary { {"controller" , "Login"}, { "action", "Index" } });
            }
            else
            {
                Usuarios usuarios = JsonConvert.DeserializeObject<Usuarios>(sessaoUsuario);

                if(usuarios == null)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
                }

                if(usuarios.Perfil != Enum.PerfilEnum.Admin)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { {"controller", "Restrito" }, { "action", "Index" } });
                }
            }

            base.OnActionExecuting(context);
        }
    }
}
