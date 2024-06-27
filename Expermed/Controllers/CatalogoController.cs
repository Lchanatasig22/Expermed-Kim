using Microsoft.AspNetCore.Mvc;

namespace Expermed.Controllers
{
    public class CatalogoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
