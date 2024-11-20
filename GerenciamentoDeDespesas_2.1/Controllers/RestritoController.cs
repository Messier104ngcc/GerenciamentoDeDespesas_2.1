using GerenciamentoDeDespesas_2._1.Filters;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoDeDespesas_2._1.Controllers
{
    [PaginaUsuarioLogado]
    public class RestritoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
