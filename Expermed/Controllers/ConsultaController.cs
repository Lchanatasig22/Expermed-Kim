﻿using Expermed.Models;
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

        // GET: api/consultas
        [HttpGet]
        public async Task<ActionResult<List<Consultum>>> GetAllConsultas()
        {
            try
            {
                var consultas = await _consultaService.GetAllConsultasAsync();
                return Ok(consultas);
            }
            catch (Exception ex)
            {
                // Manejo de errores, por ejemplo, si el nombre de usuario no está disponible en la sesión
                return BadRequest(new { message = ex.Message });
            }
        }

        // Acción para la vista
        [HttpGet("/Consulta/ListarConsultas")]
        public async Task<IActionResult> ListarConsultas()
        {
            try
            {
                var consultas = await _consultaService.GetAllConsultasAsync();
                return View(consultas);
            }
            catch (Exception ex)
            {
                // Manejo de errores, por ejemplo, si el nombre de usuario no está disponible en la sesión
                return BadRequest(new { message = ex.Message });
            }
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
            var tiposMedicamento = await _catalogoService.ObtenerMedicamentosAsync();
            var tiposDiagnostico = await _catalogoService.ObtenerDiagnosticoAsync();


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
                Value = s.IdLaboratorios.ToString(),
                Text = s.DescripcionLaboratorios
            }).ToList();
            ViewBag.TiposImagen = tiposImagen.Select(s => new SelectListItem
            {
                Value = s.IdImagenes.ToString(),
                Text = s.DescripcionImagenes
            }).ToList();
            ViewBag.TiposMedicamento = tiposMedicamento.Select(s => new SelectListItem
            {
                Value = s.IdMedicamentos.ToString(),
                Text = s.DescripcionMedicamentos
            }).ToList();
            ViewBag.TiposDiagnostico = tiposDiagnostico.Select(s => new SelectListItem
            {
                Value = s.IdDiagnosticos.ToString(),
                Text = s.DescripcionDiagnosticos
            }).ToList();

            var usuarioNombre = _httpContextAccessor.HttpContext.Session.GetString("UsuarioNombre");
            ViewBag.UsuarioNombre = usuarioNombre;
            var usuarioId = _httpContextAccessor.HttpContext.Session.GetInt32("UsuarioId");
            ViewBag.IdUsuario = usuarioId;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CrearConsultas(Consultum model)
        {
            if (ModelState.IsValid)
            {
                int consultaId = await _consultaService.InsertarConsultaAsync(model);

                // Verifica si el IdConsulta no es 0 después de la inserción
                if (consultaId == 0)
                {
                    // Manejo del error, en caso de que no se haya generado un ID
                    ModelState.AddModelError("", "Error inserting the consultation. Please try again.");
                    return View(model);
                }

                // Almacena la consulta reciente en TempData
                model.IdConsulta = consultaId;
                TempData["ConsultaReciente"] = JsonConvert.SerializeObject(model);

                // Redirige a la acción CrearConsultaDoc con el ID de la consulta recién insertada
                return RedirectToAction("CrearConsultaDoc", new { id = consultaId });
            }

            // Agrega los errores del ModelState a ViewData
            ViewData["ModelStateErrors"] = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();

            // Si el modelo no es válido, devuelve la vista con el modelo actual y muestra los errores
            return View(model);
        }





        [HttpGet]
        public async Task<IActionResult> CrearConsultaDoc(int? id)
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
            var tiposMedicamento = await _catalogoService.ObtenerMedicamentosAsync();
            var tiposDiagnostico = await _catalogoService.ObtenerDiagnosticoAsync();

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
                Value = s.IdLaboratorios.ToString(),
                Text = s.DescripcionLaboratorios
            }).ToList();
            ViewBag.TiposImagen = tiposImagen.Select(s => new SelectListItem
            {
                Value = s.IdImagenes.ToString(),
                Text = s.DescripcionImagenes
            }).ToList();
            ViewBag.TiposMedicamento = tiposMedicamento.Select(s => new SelectListItem
            {
                Value = s.IdMedicamentos.ToString(),
                Text = s.DescripcionMedicamentos
            }).ToList();
            ViewBag.TiposDiagnostico = tiposDiagnostico.Select(s => new SelectListItem
            {
                Value = s.IdDiagnosticos.ToString(),
                Text = s.DescripcionDiagnosticos
            }).ToList();

            var usuarioId = _httpContextAccessor.HttpContext.Session.GetInt32("UsuarioId");
            ViewBag.IdUsuario = usuarioId;

            Consultum model;

            if (TempData["ConsultaReciente"] != null)
            {
                // Deserializar TempData["ConsultaReciente"] a un objeto Consultum
                model = JsonConvert.DeserializeObject<Consultum>(TempData["ConsultaReciente"].ToString());
            }
            else
            {
                if (id.HasValue)
                {
                    // Obtener la consulta por ID si TempData["ConsultaReciente"] es nulo
                    model = await _consultaService.ObtenerConsultaPorIdAsync(id.Value);
                }
                else
                {
                    // Inicializar un modelo vacío si no hay un ID proporcionado
                    model = new Consultum();
                }
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
            if (consulta == null)
            {
                return BadRequest("La consulta no puede ser nula.");
            }

            try
            {
                await _consultaService.ActualizarConsultaAsync(consulta);
                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                // Log del error
                Console.WriteLine($"Error al actualizar la consulta: {ex.Message}");
                return StatusCode(500, "Hubo un problema al actualizar la consulta.");
            }
        }


        public async Task<IActionResult> ObtenerDatosConsulta(int id)
        {
            var consulta = await _consultaService.ObtenerDatosConsultaAsync(id);

            if (consulta == null)
            {
                return NotFound();
            }

            return Json(consulta);
        }
    }
}
