using Expermed.Models;
using Expermed.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace Expermed.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UserService _usuarioService;
        private readonly PerfilesService _perfilService;
        //private readonly CatalogService _catalogService;
        public UsuarioController(UserService usuarioService, PerfilesService perfilService/*, CatalogService catalogService*/)
        {
            _usuarioService = usuarioService;
            _perfilService = perfilService;
            //_catalogService = catalogService;
        }


        [HttpGet]
        public async Task<IActionResult> ListarUsuario()
        {


            var usuarios = await _usuarioService.GetAllUsuariosAsync();
            return View(usuarios);
        }

        [HttpGet]
        public async Task<IActionResult> CrearUsuario()
        {
            var perfiles = await _perfilService.GetAllPerfilesAsync();
            ViewBag.Perfiles = perfiles;
            //var tipodocumento = await _catalogService.GetAllCatalogosAsync();
            //ViewBag.Perfiles = tipodocumento;

            return View();
        }



        // Ejemplo de método para crear un usuario
        [HttpPost]
        public async Task<IActionResult> CrearUsuario(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                await _usuarioService.CreateUsuarioAsync(usuario);
                return RedirectToAction("ListarUsuario"); // Redirige a la página de listado de usuarios o a donde corresponda
            }

            var perfiles = await _perfilService.GetAllPerfilesAsync();
            ViewBag.Perfiles = perfiles;


            // Manejo de errores si el modelo no es válido
            return View(usuario);
        }

        // GET: Usuario/Edit/5
        public async Task<IActionResult> EditarUsuario(int id)
        {
            var usuario = await _usuarioService.GetUsuarioByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarUsuario(int id, [Bind("IdUsuario,CiUsuario,NombresUsuario,ApellidosUsuario,TelefonoUsuario,EmailUsuario,EstablecimientoUsuario,DirecccionestableUsuario,CiudadUsuario,ProvinciaUsuario,LoginUsuario,ClaveUsuario,ActivoUsuario,PerfilUsuarioP,CodigoUsuario")] Usuario usuario)
        {
            if (id != usuario.IdUsuario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _usuarioService.UpdateUsuarioAsync(usuario);
                }
                catch (Exception)
                {
                    // Manejar excepciones aquí si es necesario
                    throw;
                }
                return RedirectToAction(nameof(ListarUsuario)); // Redirigir a la lista de usuarios o a donde corresponda
            }
            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> CambiarEstadoUsuario(int id, bool activo)
        {
            if (id == 0)
            {
                return NotFound();
            }

            await _usuarioService.CambiarEstadoUsuarioAsync(id, activo);
            return RedirectToAction("Listar"); // Redirige a la lista de usuarios después de cambiar el estado
        }


        // Método para listar usuarios
        public async Task<IActionResult> Listar()
        {
            var usuarios = await _usuarioService.GetAllUsuariosAsync();
            return View(usuarios);
        }


    }
}
