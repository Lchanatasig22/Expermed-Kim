using Expermed.Models;
using Expermed.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Expermed.Controllers
{
    public class PacienteController : Controller
    {
        private readonly PacienteService _pacienteService;
        private readonly CatalogService _catalogoService;
        private readonly IHttpContextAccessor _httpContextAccessor;




        public PacienteController(PacienteService pacienteService, CatalogService catalogService, IHttpContextAccessor httpContextAccessor)
        {
            _pacienteService = pacienteService;
            _catalogoService = catalogService;
            _httpContextAccessor = httpContextAccessor;
        }
        //Listar pacientes
        [HttpGet]
        public async Task<IActionResult> ListarPacientes()
        {
            var pacientes = await _pacienteService.GetAllPacientesAsync();
            return View(pacientes);
        }

        // GET: Paciente/Create

        [HttpGet]
        public async Task<IActionResult> CrearPaciente()
        {
            var tiposDocumentos = await _catalogoService.ObtenerTiposDocumentosAsync();
            var tiposSangre = await _catalogoService.ObtenerTiposDeSangreAsync();
            var tiposFormacion = await _catalogoService.ObtenerTiposDeFormacionPAsync();
            var tiposEstadoCivil = await _catalogoService.ObtenerTiposDeEstadoCivilAsync();
            var tiposGenero = await _catalogoService.ObtenerTiposDeGeneroAsync();
            var tiposNacionalidad = await _catalogoService.ObtenerTiposDeNacionalidadPAsync();
            var tiposProvincia = await _catalogoService.ObtenerTiposDeProvinciaPAsync();
            var tiposSeguro = await _catalogoService.ObtenerTiposDeSeguroAsync();
            // Puedes crear una lista de SelectListItem para usar en tu select
            var tiposDocumentosSelectList = tiposDocumentos.Select(d => new SelectListItem
            {
                Value = d.UuidCatalogo.ToString(), // Aquí debes asignar el valor correcto
                Text = d.DescripcionCatalogo // Aquí debes asignar el texto que se mostrará en la opción
            }).ToList();
            var tiposSangreSelectList = tiposSangre.Select(s => new SelectListItem
            {
                Value = s.UuidCatalogo.ToString(), // Aquí debes asignar el valor correcto
                Text = s.DescripcionCatalogo // Aquí debes asignar el texto que se mostrará en la opción
            }).ToList();
            var tiposFormacionSelectList = tiposFormacion.Select(s => new SelectListItem
            {
                Value = s.UuidCatalogo.ToString(), // Aquí debes asignar el valor correcto
                Text = s.DescripcionCatalogo // Aquí debes asignar el texto que se mostrará en la opción
            }).ToList();
            var tiposEstadoCivilSelectList = tiposEstadoCivil.Select(s => new SelectListItem
            {
                Value = s.UuidCatalogo.ToString(), // Aquí debes asignar el valor correcto
                Text = s.DescripcionCatalogo // Aquí debes asignar el texto que se mostrará en la opción
            }).ToList();
            var tiposGeneroSelectList = tiposGenero.Select(s => new SelectListItem
            {
                Value = s.UuidCatalogo.ToString(), // Aquí debes asignar el valor correcto
                Text = s.DescripcionCatalogo // Aquí debes asignar el texto que se mostrará en la opción
            }).ToList();
            var tiposNacionalidadSelectList = tiposNacionalidad.Select(s => new SelectListItem
            {
                Value = s.IdLocalidad.ToString(), // Aquí debes asignar el valor correcto
                Text = s.GentilicioLocalidad // Aquí debes asignar el texto que se mostrará en la opción
            }).ToList();
            var tiposProvinciaSelectList = tiposProvincia.Select(s => new SelectListItem
            {
                Value = s.IdLocalidad.ToString(), // Aquí debes asignar el valor correcto
                Text = s.PrefijoLocalidad // Aquí debes asignar el texto que se mostrará en la opción
            }).ToList();
            var tipoSegurorSelectList = tiposSeguro.Select(s => new SelectListItem
            {
                Value = s.UuidCatalogo.ToString(), // Aquí debes asignar el valor correcto
                Text = s.DescripcionCatalogo // Aquí debes asignar el texto que se mostrará en la opción
            }).ToList();

            ViewBag.TiposDocumentos = tiposDocumentosSelectList;
            ViewBag.TiposSangre = tiposSangreSelectList;
            ViewBag.TiposFormacion = tiposFormacionSelectList;
            ViewBag.TiposEstadoCivil = tiposEstadoCivilSelectList;
            ViewBag.TiposGenero = tiposGeneroSelectList;
            ViewBag.TiposNacionalidad = tiposNacionalidadSelectList;
            ViewBag.TiposProvincia = tiposNacionalidadSelectList;
            ViewBag.TiposSeguro = tipoSegurorSelectList;

            // Obtener el nombre de usuario de la sesión

            var usuarioNombre = _httpContextAccessor.HttpContext.Session.GetString("UsuarioNombre");

            // Pasar el nombre de usuario a la vista
            ViewBag.UsuarioNombre = usuarioNombre;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CrearPaciente(Paciente paciente)
        {
            if (ModelState.IsValid)
            {

                await _pacienteService.CreatePacienteAsync(paciente);
                return RedirectToAction(nameof(ListarPacientes));
            }
            return View(paciente);
        }


        // GET: Usuario/Edit/5
        public async Task<IActionResult> EditarPaciente(int id)
        {
            var tiposDocumentos = await _catalogoService.ObtenerTiposDocumentosAsync();
            var tiposSangre = await _catalogoService.ObtenerTiposDeSangreAsync();
            var tiposFormacion = await _catalogoService.ObtenerTiposDeFormacionPAsync();
            var tiposEstadoCivil = await _catalogoService.ObtenerTiposDeEstadoCivilAsync();
            var tiposGenero = await _catalogoService.ObtenerTiposDeGeneroAsync();
            var tiposNacionalidad = await _catalogoService.ObtenerTiposDeNacionalidadPAsync();
            var tiposProvincia = await _catalogoService.ObtenerTiposDeProvinciaPAsync();
            var tiposSeguro = await _catalogoService.ObtenerTiposDeSeguroAsync();
            // Puedes crear una lista de SelectListItem para usar en tu select
            var tiposDocumentosSelectList = tiposDocumentos.Select(d => new SelectListItem
            {
                Value = d.UuidCatalogo.ToString(), // Aquí debes asignar el valor correcto
                Text = d.DescripcionCatalogo // Aquí debes asignar el texto que se mostrará en la opción
            }).ToList();
            var tiposSangreSelectList = tiposSangre.Select(s => new SelectListItem
            {
                Value = s.UuidCatalogo.ToString(), // Aquí debes asignar el valor correcto
                Text = s.DescripcionCatalogo // Aquí debes asignar el texto que se mostrará en la opción
            }).ToList();
            var tiposFormacionSelectList = tiposFormacion.Select(s => new SelectListItem
            {
                Value = s.UuidCatalogo.ToString(), // Aquí debes asignar el valor correcto
                Text = s.DescripcionCatalogo // Aquí debes asignar el texto que se mostrará en la opción
            }).ToList();
            var tiposEstadoCivilSelectList = tiposEstadoCivil.Select(s => new SelectListItem
            {
                Value = s.UuidCatalogo.ToString(), // Aquí debes asignar el valor correcto
                Text = s.DescripcionCatalogo // Aquí debes asignar el texto que se mostrará en la opción
            }).ToList();
            var tiposGeneroSelectList = tiposGenero.Select(s => new SelectListItem
            {
                Value = s.UuidCatalogo.ToString(), // Aquí debes asignar el valor correcto
                Text = s.DescripcionCatalogo // Aquí debes asignar el texto que se mostrará en la opción
            }).ToList();
            var tiposNacionalidadSelectList = tiposNacionalidad.Select(s => new SelectListItem
            {
                Value = s.IdLocalidad.ToString(), // Aquí debes asignar el valor correcto
                Text = s.GentilicioLocalidad // Aquí debes asignar el texto que se mostrará en la opción
            }).ToList();
            var tiposProvinciaSelectList = tiposProvincia.Select(s => new SelectListItem
            {
                Value = s.IdLocalidad.ToString(), // Aquí debes asignar el valor correcto
                Text = s.PrefijoLocalidad // Aquí debes asignar el texto que se mostrará en la opción
            }).ToList();
            var tipoSegurorSelectList = tiposSeguro.Select(s => new SelectListItem
            {
                Value = s.UuidCatalogo.ToString(), // Aquí debes asignar el valor correcto
                Text = s.DescripcionCatalogo // Aquí debes asignar el texto que se mostrará en la opción
            }).ToList();

            ViewBag.TiposDocumentos = tiposDocumentosSelectList;
            ViewBag.TiposSangre = tiposSangreSelectList;
            ViewBag.TiposFormacion = tiposFormacionSelectList;
            ViewBag.TiposEstadoCivil = tiposEstadoCivilSelectList;
            ViewBag.TiposGenero = tiposGeneroSelectList;
            ViewBag.TiposNacionalidad = tiposNacionalidadSelectList;
            ViewBag.TiposProvincia = tiposNacionalidadSelectList;
            ViewBag.TiposSeguro = tipoSegurorSelectList;

            // Obtener el nombre de usuario de la sesión

            var usuarioNombre = _httpContextAccessor.HttpContext.Session.GetString("UsuarioNombre");

            // Pasar el nombre de usuario a la vista
            ViewBag.UsuarioNombre = usuarioNombre;
            var paciente = await _pacienteService.GetPacienteByIdAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }
            return View(paciente);
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarPaciente(int id, [Bind("IdPacientes,FechacreacionPacientes,UsuariocreacionPacientes,FechamodificacionPacientes,UsuariomodificacionPacientes,ActivoPacientes,TipodocumentoPacientesC,CiPacientes,PrimernombrePacientes,SegundonombrePacientes,PrimerapellidoPacientes,SegundoapellidoPacientes,SexoPacientesC,FechanacimientoPacientes,Edad,TiposangrePacientesC,DonantePacientes,EstadocivilPacientesC,FormacionprofesionalPacientesC,TelefonofijoPacientes,TelefonocelularPacientes,EmailPacientes,NacionalidadPacientesL,ProvinciaPacientesL,DireccionPacientes,OcupacionPacientes,EmpresaPacientes,SegurosaludPacientesC")] Paciente paciente)
        {
            if (id != paciente.IdPacientes)
            {
                return BadRequest();
            }

            try
            {
                var result = await _pacienteService.UpdatePacienteAsync(paciente); // Reemplaza  por tu servicio que realiza la actualización

                if (result > 0)
                {
                    return RedirectToAction("ListarPacientes"); // Redirecciona a la página deseada después de la actualización
                }
                else
                {
                    return NotFound(); // Retorna NotFound si no se encontró el paciente para actualizar
                }
            }
            catch (Exception)
            {
                // Maneja cualquier excepción aquí
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
