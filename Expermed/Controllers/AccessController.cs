using Expermed.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace Expermed.Controllers
{
    public class AccessController : Controller
    {
        private readonly AutenticationService _authService;

        public AccessController(AutenticationService authService)
        {
            _authService = authService;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string loginUsuario, string claveUsuario)
        {
            var user = await _authService.ValidateUser(loginUsuario, claveUsuario);
            if (user != null && user.PerfilUsuarioP.HasValue && user.PerfilUsuarioP.Value != 0)
            {
                // Autenticación exitosa, almacenar detalles del usuario en la sesión

                HttpContext.Session.SetInt32("PerfilUsuarioP", user.PerfilUsuarioP.Value); // Usar .Value para obtener el valor int


                return RedirectToAction("Index", "Home");
            }

            // Autenticación fallida, mostrar mensaje de error
            ViewBag.ErrorMessage = "Nombre de usuario o contraseña incorrectos";
            return View();
        }

    }
}
