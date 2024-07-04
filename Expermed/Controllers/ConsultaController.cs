using Expermed.Models;
using Expermed.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;

namespace Expermed.Controllers
{

    public class ConsultaController : Controller
    {
        private readonly CatalogService _catalogoService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly PacienteService _pacienteService;
        private readonly ConsultaService _consultaService;

        public ConsultaController(PacienteService pacienteService, CatalogService catalogService, IHttpContextAccessor httpContextAccessor, ConsultaService consultaService)
        {
            _pacienteService = pacienteService;
            _catalogoService = catalogService;
            _httpContextAccessor = httpContextAccessor;
            _consultaService = consultaService;
        }

        public IActionResult ListarConsultas()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CrearConsultas()
        {
            var tiposDocumentos = await _catalogoService.ObtenerTiposDocumentosAsync();
            var tiposSangre = await _catalogoService.ObtenerTiposDeSangreAsync();
            var tiposFormacion = await _catalogoService.ObtenerTiposDeFormacionPAsync();
            var tiposEstadoCivil = await _catalogoService.ObtenerTiposDeEstadoCivilAsync();
            var tiposGenero = await _catalogoService.ObtenerTiposDeGeneroAsync();
            var tiposNacionalidad = await _catalogoService.ObtenerTiposDeNacionalidadPAsync();
            var tiposProvincia = await _catalogoService.ObtenerTiposDeProvinciaPAsync();
            var tiposSeguro = await _catalogoService.ObtenerTiposDeSeguroAsync();
            var tiposPariente = await _catalogoService.ObtenerParienteAsync();
            var tiposLaboratorio = await _catalogoService.ObtenerLaboratoriosAsync();
            var tiposImagen = await _catalogoService.ObtenerImagenAsync();

            ViewBag.TiposDocumentos = tiposDocumentos.Select(d => new SelectListItem
            {
                Value = d.UuidCatalogo.ToString(),
                Text = d.DescripcionCatalogo
            }).ToList();
            ViewBag.TiposSangre = tiposSangre.Select(s => new SelectListItem
            {
                Value = s.UuidCatalogo.ToString(),
                Text = s.DescripcionCatalogo
            }).ToList();
            ViewBag.TiposFormacion = tiposFormacion.Select(s => new SelectListItem
            {
                Value = s.UuidCatalogo.ToString(),
                Text = s.DescripcionCatalogo
            }).ToList();
            ViewBag.TiposEstadoCivil = tiposEstadoCivil.Select(s => new SelectListItem
            {
                Value = s.UuidCatalogo.ToString(),
                Text = s.DescripcionCatalogo
            }).ToList();
            ViewBag.TiposGenero = tiposGenero.Select(s => new SelectListItem
            {
                Value = s.UuidCatalogo.ToString(),
                Text = s.DescripcionCatalogo
            }).ToList();
            ViewBag.TiposNacionalidad = tiposNacionalidad.Select(s => new SelectListItem
            {
                Value = s.IdLocalidad.ToString(),
                Text = s.GentilicioLocalidad
            }).ToList();
            ViewBag.TiposProvincia = tiposProvincia.Select(s => new SelectListItem
            {
                Value = s.IdLocalidad.ToString(),
                Text = s.PrefijoLocalidad
            }).ToList();
            ViewBag.TiposSeguro = tiposSeguro.Select(s => new SelectListItem
            {
                Value = s.UuidCatalogo.ToString(),
                Text = s.DescripcionCatalogo
            }).ToList();
            ViewBag.TiposPariente = tiposPariente.Select(s => new SelectListItem
            {
                Value = s.UuidCatalogo.ToString(),
                Text = s.DescripcionCatalogo
            }).ToList();
            ViewBag.TiposLaboratorio = tiposLaboratorio.Select(s => new SelectListItem
            {
                Value = s.CodigoLaboratorios.ToString(),
                Text = s.DescripcionLaboratorios
            }).ToList();
            ViewBag.TiposImagen = tiposImagen.Select(s => new SelectListItem
            {
                Value = s.CodigoImagenes.ToString(),
                Text = s.DescripcionImagenes
            }).ToList();

            var usuarioNombre = _httpContextAccessor.HttpContext.Session.GetString("UsuarioNombre");
            ViewBag.UsuarioNombre = usuarioNombre;
            var usuarioId = _httpContextAccessor.HttpContext.Session.GetInt32("UsuarioId");
            ViewBag.IdUsuario = usuarioId;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> InsertarConsulta(Consultum model)
        {
            if (ModelState.IsValid)
            {
                await _consultaService.InsertarConsultaAsync(model);

                // Verifica si el IdConsulta no es 0 después de la inserción
                if (model.IdConsulta == 0)
                {
                    // Manejo del error, en caso de que no se haya generado un ID
                    ModelState.AddModelError("", "Error inserting the consultation. Please try again.");
                    return View(model);
                }

                TempData["ConsultaReciente"] = JsonConvert.SerializeObject(model);
                return RedirectToAction("CrearConsultaDoc", new { id = model.IdConsulta });
            }

            return View(model);
        }



        [HttpGet]
        public async Task<IActionResult> CrearConsultaDoc(int id)
        {
            Consultum model;

            if (TempData["ConsultaReciente"] != null)
            {
                model = JsonConvert.DeserializeObject<Consultum>(TempData["ConsultaReciente"].ToString());
            }
            else
            {
                model = await _consultaService.ObtenerConsultaPorIdAsync(id);
            }

            var usuarioNombre = _httpContextAccessor.HttpContext.Session.GetString("UsuarioNombre");
            ViewBag.UsuarioNombre = usuarioNombre;

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> BuscarPacientePorNombre(int ci)
        {
            var paciente = await _consultaService.BuscarPacientePorNombreAsync(ci);
            if (paciente != null)
            {
                return Json(new
                {
                    idPaciente = paciente.IdPacientes,
                    primerApellido = paciente.PrimerapellidoPacientes,
                    segundoApellido = paciente.SegundoapellidoPacientes,
                    primerNombre = paciente.PrimernombrePacientes,
                    segundoNombre = paciente.SegundonombrePacientes,
                    tipoDocumento = paciente.TipodocumentoPacientesC,
                    numeroDocumento = paciente.CiPacientes,
                    tipoSangre = paciente.TiposangrePacientesC,
                    esDonante = paciente.DonantePacientes,
                    fechaNacimiento = paciente.FechanacimientoPacientes.HasValue ? paciente.FechanacimientoPacientes.Value.ToString("yyyy-MM-dd") : string.Empty,
                    edad = paciente.Edad,
                    sexo = paciente.SexoPacientesC,
                    estadoCivil = paciente.EstadocivilPacientesC,
                    formacionProfesional = paciente.FormacionprofesionalPacientesC,
                    nacionalidad = paciente.NacionalidadPacientesL,
                    direccion = paciente.DireccionPacientes,
                    telefono = paciente.TelefonocelularPacientes,
                    telefonoCelular = paciente.TelefonocelularPacientes,
                    email = paciente.EmailPacientes,
                    ocupacion = paciente.OcupacionPacientes,
                    empresa = paciente.EmpresaPacientes,
                    seguroSalud = paciente.SegurosaludPacientesC

                });
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarConsulta(Consultum consulta)
        {
            if (ModelState.IsValid)
            {
                await _consultaService.ActualizarConsultaAsync(consulta);
                return RedirectToAction("ListarConsultas"); // Redirigir a una vista que confirme la actualización
            }

            return View(consulta);
        }

    }
}
