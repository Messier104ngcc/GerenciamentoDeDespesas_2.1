using GerenciamentoDeDespesas_2._1.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GerenciamentoDeDespesas_2._1.ViewComponents
{
    public class Menu : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string sessaoUsuario = HttpContext.Session.GetString("sessaoUsuarioLogado");

            if (string.IsNullOrEmpty(sessaoUsuario)) return null;

            Usuarios usuarios = JsonConvert.DeserializeObject<Usuarios>(sessaoUsuario);

            return View(usuarios);
        }
    }
}
