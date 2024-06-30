using Expermed.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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





            return View();

        }




        [HttpGet]
        public async Task<IActionResult> BuscarPacientePorNombre(string nombre)
        {
            var paciente = await _consultaService.BuscarPacientePorNombreAsync(nombre);
            if (paciente != null)
            {
                return Json(new
                {
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



    }
}
