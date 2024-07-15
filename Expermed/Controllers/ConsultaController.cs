using Expermed.Models;
using Expermed.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Rotativa.AspNetCore;
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
        public async Task<IActionResult> VerConsulta(int id)
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

            var usuarioId = _httpContextAccessor.HttpContext?.Session.GetInt32("UsuarioId");
            ViewBag.IdUsuario = usuarioId;
            var usuarioNombre = _httpContextAccessor.HttpContext?.Session.GetString("UsuarioNombre");
            ViewBag.UsuarioNombre = usuarioNombre;

            var consulta = await _consultaService.ObtenerConsultaPorIdAsync(id);
            if (consulta == null)
            {
                return NotFound();
            }

            var viewModel = new VerConsultaViewModel
            {
                IdConsulta = consulta.IdConsulta,
                FechacreacionConsulta = consulta.FechacreacionConsulta,
                UsuariocreacionConsulta = consulta.UsuariocreacionConsulta,
                HistorialConsulta = consulta.HistorialConsulta,
                SecuencialConsulta = consulta.SecuencialConsulta,
                PacienteConsultaP = consulta.PacienteConsultaPNavigation?.IdPacientes ?? 0,
                MotivoConsulta = consulta.MotivoConsulta,
                EnfermedadConsulta = consulta.EnfermedadConsulta,
                NombreparienteConsulta = consulta.NombreparienteConsulta,
                TipoparienteConsulta = consulta.TipoparienteConsulta,
                TelefonoConsulta = consulta.TelefonoConsulta,
                TemperaturaConsulta = consulta.TemperaturaConsulta,
                FrecuenciarespiratoriaConsulta = consulta.FrecuenciarespiratoriaConsulta,
                PresionarterialsistolicaConsulta = consulta.PresionarterialsistolicaConsulta,
                PresionarterialdiastolicaConsulta = consulta.PresionarterialdiastolicaConsulta,
                PulsoConsulta = consulta.PulsoConsulta,
                PesoConsulta = consulta.PesoConsulta,
                TallaConsulta = consulta.TallaConsulta,
                PlantratamientoConsulta = consulta.PlantratamientoConsulta,
                ObservacionConsulta = consulta.ObservacionConsulta,
                AntecedentespersonalesConsulta = consulta.AntecedentespersonalesConsulta,
                DiasincapacidadConsulta = consulta.DiasincapacidadConsulta,
                MedicoConsultaD = consulta.MedicoConsultaD,
                EspecialidadConsultaC = consulta.EspecialidadConsultaC,
                EstadoConsultaC = consulta.EstadoConsultaC,
                TipoConsultaC = consulta.TipoConsultaC,
                NotasevolucionConsulta = consulta.NotasevolucionConsulta,
                ConsultaprincipalConsulta = consulta.ConsultaprincipalConsulta,
                DocumentoConsultaD = consulta.DocumentoConsultaD,
                DetalleConsultaD = consulta.DetalleConsultaD,
                Cardiopatia = consulta.Cardiopatia,
                ObserCardiopatia = consulta.ObserCardiopatia,
                Diabetes = consulta.Diabetes,
                ObserDiabetes = consulta.ObserDiabetes,
                EnfCardiovascular = consulta.EnfCardiovascular,
                ObserEnfCardiovascular = consulta.ObserEnfCardiovascular,
                Hipertension = consulta.Hipertension,
                ObserHipertensión = consulta.ObserHipertensión,
                Cancer = consulta.Cancer,
                ObserCancer = consulta.ObserCancer,
                Tuberculosis = consulta.Tuberculosis,
                ObserTuberculosis = consulta.ObserTuberculosis,
                EnfMental = consulta.EnfMental,
                ObserEnfMental = consulta.ObserEnfMental,
                EnfInfecciosa = consulta.EnfInfecciosa,
                ObserEnfInfecciosa = consulta.ObserEnfInfecciosa,
                MalFormacion = consulta.MalFormacion,
                ObserMalFormacion = consulta.ObserMalFormacion,
                Otro = consulta.Otro,
                ObserOtro = consulta.ObserOtro,
                Alergias = consulta.Alergias,
                ObserAlergias = consulta.ObserAlergias,
                Cirugias = consulta.Cirugias,
                ObserCirugias = consulta.ObserCirugias,
                OrgSentidos = consulta.OrgSentidos,
                ObserOrgSentidos = consulta.ObserOrgSentidos,
                Respiratorio = consulta.Respiratorio,
                ObserRespiratorio = consulta.ObserRespiratorio,
                CardioVascular = consulta.CardioVascular,
                ObserCardioVascular = consulta.ObserCardioVascular,
                Digestivo = consulta.Digestivo,
                ObserDigestivo = consulta.ObserDigestivo,
                Genital = consulta.Genital,
                ObserGenital = consulta.ObserGenital,
                Urinario = consulta.Urinario,
                ObserUrinario = consulta.ObserUrinario,
                MEsqueletico = consulta.MEsqueletico,
                ObserMEsqueletico = consulta.ObserMEsqueletico,
                Endocrino = consulta.Endocrino,
                ObserEndocrino = consulta.ObserEndocrino,
                Linfatico = consulta.Linfatico,
                ObserLinfatico = consulta.ObserLinfatico,
                Nervioso = consulta.Nervioso,
                ObserNervioso = consulta.ObserNervioso,
                Cabeza = consulta.Cabeza,
                ObserCabeza = consulta.ObserCabeza,
                Cuello = consulta.Cuello,
                ObserCuello = consulta.ObserCuello,
                Torax = consulta.Torax,
                ObserTorax = consulta.ObserTorax,
                Abdomen = consulta.Abdomen,
                ObserAbdomen = consulta.ObserAbdomen,
                Pelvis = consulta.Pelvis,
                ObserPelvis = consulta.ObserPelvis,
                Extremidades = consulta.Extremidades,
                ObserExtremidades = consulta.ObserExtremidades,
                ImagenConsultaI = consulta.ImagenConsultaI,
                LaboratorioConsultaLa = consulta.LaboratorioConsultaLa,
                DiagnosticoConsultaDi = consulta.DiagnosticoConsultaDi,
                ActivoConsulta = consulta.ActivoConsulta,
                MedicamentoConsultaM = consulta.MedicamentoConsultaM,
                PacienteNombre = consulta.PacienteConsultaPNavigation?.PrimernombrePacientes ?? "No asignado",
                PacienteSegundoNombre = consulta.PacienteConsultaPNavigation?.SegundonombrePacientes ?? "No asignado",
                PacienteApellido = consulta.PacienteConsultaPNavigation?.PrimerapellidoPacientes ?? "No asignado",
                PacienteSegundoApellido = consulta.PacienteConsultaPNavigation?.SegundoapellidoPacientes ?? "No asignado",
                NumeroDocumento = consulta.PacienteConsultaPNavigation?.CiPacientes ?? 0,
                TiposDocumentos = consulta.PacienteConsultaPNavigation?.TipodocumentoPacientesCNavigation?.DescripcionCatalogo ?? "No asignado",
                TiposSangre = consulta.PacienteConsultaPNavigation?.TiposangrePacientesCNavigation?.DescripcionCatalogo ?? "No asignado",
                TipoDonante = consulta.PacienteConsultaPNavigation?.DonantePacientes ?? "No asignado",
                FechaNacimiento = consulta.PacienteConsultaPNavigation?.FechanacimientoPacientes,
                EdadPaciente = consulta.PacienteConsultaPNavigation?.Edad ?? 0,
                TipoSexo = consulta.PacienteConsultaPNavigation?.SexoPacientesCNavigation.DescripcionCatalogo ?? "No asignado",
                TipoEstadoC = consulta.PacienteConsultaPNavigation?.EstadocivilPacientesCNavigation.DescripcionCatalogo ?? "No asignado",
                TipoFormacionP = consulta.PacienteConsultaPNavigation?.FormacionprofesionalPacientesCNavigation.DescripcionCatalogo ?? "No asignado",
                TipoNacionalidadP = consulta.PacienteConsultaPNavigation?.NacionalidadPacientesLNavigation.GentilicioLocalidad ?? "No asignado",
                DireccionPa = consulta.PacienteConsultaPNavigation?.DireccionPacientes ?? "No asignado",
                EmailPac = consulta.PacienteConsultaPNavigation?.EmailPacientes ?? "No asignado",
                OcupaciónPac = consulta.PacienteConsultaPNavigation?.OcupacionPacientes ?? "No asignado",
                EmpresaPac = consulta.PacienteConsultaPNavigation?.EmpresaPacientes ?? "No asignado",
                SeguroSaludPa = consulta.PacienteConsultaPNavigation?.SegurosaludPacientesC ?? "No asignado",
                TelefonoFiPa = consulta.PacienteConsultaPNavigation?.TelefonofijoPacientes ?? 0,
                TelefonoCePa = consulta.PacienteConsultaPNavigation?.TelefonocelularPacientes ?? 0,
                MedicamentoCantidad = consulta.MedicamentoConsultaMNavigation?.CantidadMedicamentoC ?? 0,
                TipoDiagnostico = consulta.DiagnosticoConsultaDiNavigation?.PresuntivoDiagnosticos ?? "No asignado",
                TipoDiagnosticod = consulta.DiagnosticoConsultaDiNavigation?.DefinitivoDiagnosticos ?? "No asignado",
                MedicamentoConsulta = consulta.MedicamentoConsultaMNavigation?.IdMedicamentosMedicamentoMNavigation?.DescripcionMedicamentos ?? "No asignado",

                MedicamentoObservacion = consulta.MedicamentoConsultaMNavigation?.ObservacionMedicamento ?? "No asignado",
                ImagenConsulta = consulta.ImagenConsultaINavigation?.IdImagenesImagenINavigation?.DescripcionImagenes ?? "No asignado",
                ImagenCantidad = consulta.ImagenConsultaINavigation?.CantidadImagen ?? 0,
                ImagenObservacion = consulta.ImagenConsultaINavigation?.ObservacionImagen ?? "No asignado",
                DiagnosticoConsulta = consulta.DiagnosticoConsultaDiNavigation?.IdDiagnosticosDiagnosticoDNavigation?.DescripcionDiagnosticos ?? "No asignado",

                LaboratorioConsulta = consulta.LaboratorioConsultaLaNavigation?.IdLaboratoriosLaboratorioLNavigation?.DescripcionLaboratorios ?? "No asignado",
                LaboratorioCantidad = consulta.LaboratorioConsultaLaNavigation?.CantidadLaboratorio ?? 0,
                LaboratorioObservacion = consulta.LaboratorioConsultaLaNavigation?.ObservacionLaboratorio ?? "No asignado"
            };

            return View(viewModel);
        }


        public async Task<IActionResult> GeneratePdf(int id, string tipoDocumento)
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

            var usuarioId = _httpContextAccessor.HttpContext?.Session.GetInt32("UsuarioId");
            ViewBag.IdUsuario = usuarioId;
            var usuarioNombre = _httpContextAccessor.HttpContext?.Session.GetString("UsuarioNombre");
            ViewBag.UsuarioNombre = usuarioNombre;
            var consulta = await _consultaService.ObtenerConsultaPorIdAsync(id);

            if (consulta == null)
            {
                return NotFound();
            }

            // Asegúrate de que los valores en `consulta` no sean nulos antes de asignarlos
            var pdfViewModel = new VerConsultaViewModel
            {
                IdConsulta = consulta.IdConsulta,
                FechacreacionConsulta = consulta.FechacreacionConsulta,
                UsuariocreacionConsulta = consulta.UsuariocreacionConsulta,
                HistorialConsulta = consulta.HistorialConsulta,
                SecuencialConsulta = consulta.SecuencialConsulta,
                PacienteConsultaP = consulta.PacienteConsultaPNavigation?.IdPacientes ?? 0,
                MotivoConsulta = consulta.MotivoConsulta,
                EnfermedadConsulta = consulta.EnfermedadConsulta,
                NombreparienteConsulta = consulta.NombreparienteConsulta,
                TipoparienteConsulta = consulta.TipoparienteConsulta,
                TelefonoConsulta = consulta.TelefonoConsulta,
                TemperaturaConsulta = consulta.TemperaturaConsulta,
                FrecuenciarespiratoriaConsulta = consulta.FrecuenciarespiratoriaConsulta,
                PresionarterialsistolicaConsulta = consulta.PresionarterialsistolicaConsulta,
                PresionarterialdiastolicaConsulta = consulta.PresionarterialdiastolicaConsulta,
                PulsoConsulta = consulta.PulsoConsulta,
                PesoConsulta = consulta.PesoConsulta,
                TallaConsulta = consulta.TallaConsulta,
                PlantratamientoConsulta = consulta.PlantratamientoConsulta,
                ObservacionConsulta = consulta.ObservacionConsulta,
                AntecedentespersonalesConsulta = consulta.AntecedentespersonalesConsulta,
                DiasincapacidadConsulta = consulta.DiasincapacidadConsulta,
                MedicoConsultaD = consulta.MedicoConsultaD,
                EspecialidadConsultaC = consulta.EspecialidadConsultaC,
                EstadoConsultaC = consulta.EstadoConsultaC,
                TipoConsultaC = consulta.TipoConsultaC,
                NotasevolucionConsulta = consulta.NotasevolucionConsulta,
                ConsultaprincipalConsulta = consulta.ConsultaprincipalConsulta,
                DocumentoConsultaD = consulta.DocumentoConsultaD,
                DetalleConsultaD = consulta.DetalleConsultaD,
                Cardiopatia = consulta.Cardiopatia,
                ObserCardiopatia = consulta.ObserCardiopatia,
                Diabetes = consulta.Diabetes,
                ObserDiabetes = consulta.ObserDiabetes,
                EnfCardiovascular = consulta.EnfCardiovascular,
                ObserEnfCardiovascular = consulta.ObserEnfCardiovascular,
                Hipertension = consulta.Hipertension,
                ObserHipertensión = consulta.ObserHipertensión,
                Cancer = consulta.Cancer,
                ObserCancer = consulta.ObserCancer,
                Tuberculosis = consulta.Tuberculosis,
                ObserTuberculosis = consulta.ObserTuberculosis,
                EnfMental = consulta.EnfMental,
                ObserEnfMental = consulta.ObserEnfMental,
                EnfInfecciosa = consulta.EnfInfecciosa,
                ObserEnfInfecciosa = consulta.ObserEnfInfecciosa,
                MalFormacion = consulta.MalFormacion,
                ObserMalFormacion = consulta.ObserMalFormacion,
                Otro = consulta.Otro,
                ObserOtro = consulta.ObserOtro,
                Alergias = consulta.Alergias,
                ObserAlergias = consulta.ObserAlergias,
                Cirugias = consulta.Cirugias,
                ObserCirugias = consulta.ObserCirugias,
                OrgSentidos = consulta.OrgSentidos,
                ObserOrgSentidos = consulta.ObserOrgSentidos,
                Respiratorio = consulta.Respiratorio,
                ObserRespiratorio = consulta.ObserRespiratorio,
                CardioVascular = consulta.CardioVascular,
                ObserCardioVascular = consulta.ObserCardioVascular,
                Digestivo = consulta.Digestivo,
                ObserDigestivo = consulta.ObserDigestivo,
                Genital = consulta.Genital,
                ObserGenital = consulta.ObserGenital,
                Urinario = consulta.Urinario,
                ObserUrinario = consulta.ObserUrinario,
                MEsqueletico = consulta.MEsqueletico,
                ObserMEsqueletico = consulta.ObserMEsqueletico,
                Endocrino = consulta.Endocrino,
                ObserEndocrino = consulta.ObserEndocrino,
                Linfatico = consulta.Linfatico,
                ObserLinfatico = consulta.ObserLinfatico,
                Nervioso = consulta.Nervioso,
                ObserNervioso = consulta.ObserNervioso,
                Cabeza = consulta.Cabeza,
                ObserCabeza = consulta.ObserCabeza,
                Cuello = consulta.Cuello,
                ObserCuello = consulta.ObserCuello,
                Torax = consulta.Torax,
                ObserTorax = consulta.ObserTorax,
                Abdomen = consulta.Abdomen,
                ObserAbdomen = consulta.ObserAbdomen,
                Pelvis = consulta.Pelvis,
                ObserPelvis = consulta.ObserPelvis,
                Extremidades = consulta.Extremidades,
                ObserExtremidades = consulta.ObserExtremidades,
                ImagenConsultaI = consulta.ImagenConsultaI,
                LaboratorioConsultaLa = consulta.LaboratorioConsultaLa,
                DiagnosticoConsultaDi = consulta.DiagnosticoConsultaDi,
                ActivoConsulta = consulta.ActivoConsulta,
                MedicamentoConsultaM = consulta.MedicamentoConsultaM,
                PacienteNombre = consulta.PacienteConsultaPNavigation?.PrimernombrePacientes ?? "No asignado",
                PacienteSegundoNombre = consulta.PacienteConsultaPNavigation?.SegundonombrePacientes ?? "No asignado",
                PacienteApellido = consulta.PacienteConsultaPNavigation?.PrimerapellidoPacientes ?? "No asignado",
                PacienteSegundoApellido = consulta.PacienteConsultaPNavigation?.SegundoapellidoPacientes ?? "No asignado",
                NumeroDocumento = consulta.PacienteConsultaPNavigation?.CiPacientes ?? 0,
                TiposDocumentos = consulta.PacienteConsultaPNavigation?.TipodocumentoPacientesCNavigation?.DescripcionCatalogo ?? "No asignado",
                TiposSangre = consulta.PacienteConsultaPNavigation?.TiposangrePacientesCNavigation?.DescripcionCatalogo ?? "No asignado",
                TipoDonante = consulta.PacienteConsultaPNavigation?.DonantePacientes ?? "No asignado",
                FechaNacimiento = consulta.PacienteConsultaPNavigation?.FechanacimientoPacientes,
                EdadPaciente = consulta.PacienteConsultaPNavigation?.Edad ?? 0,
                TipoSexo = consulta.PacienteConsultaPNavigation?.SexoPacientesCNavigation.DescripcionCatalogo ?? "No asignado",
                TipoEstadoC = consulta.PacienteConsultaPNavigation?.EstadocivilPacientesCNavigation.DescripcionCatalogo ?? "No asignado",
                TipoFormacionP = consulta.PacienteConsultaPNavigation?.FormacionprofesionalPacientesCNavigation.DescripcionCatalogo ?? "No asignado",
                TipoNacionalidadP = consulta.PacienteConsultaPNavigation?.NacionalidadPacientesLNavigation.GentilicioLocalidad ?? "No asignado",
                DireccionPa = consulta.PacienteConsultaPNavigation?.DireccionPacientes ?? "No asignado",
                EmailPac = consulta.PacienteConsultaPNavigation?.EmailPacientes ?? "No asignado",
                OcupaciónPac = consulta.PacienteConsultaPNavigation?.OcupacionPacientes ?? "No asignado",
                EmpresaPac = consulta.PacienteConsultaPNavigation?.EmpresaPacientes ?? "No asignado",
                SeguroSaludPa = consulta.PacienteConsultaPNavigation?.SegurosaludPacientesC ?? "No asignado",
                TelefonoFiPa = consulta.PacienteConsultaPNavigation?.TelefonofijoPacientes ?? 0,
                TelefonoCePa = consulta.PacienteConsultaPNavigation?.TelefonocelularPacientes ?? 0,
                MedicamentoCantidad = consulta.MedicamentoConsultaMNavigation?.CantidadMedicamentoC ?? 0,
                TipoDiagnostico = consulta.DiagnosticoConsultaDiNavigation?.PresuntivoDiagnosticos ?? "No asignado",
                TipoDiagnosticod = consulta.DiagnosticoConsultaDiNavigation?.DefinitivoDiagnosticos ?? "No asignado",
                MedicamentoConsulta = consulta.MedicamentoConsultaMNavigation?.IdMedicamentosMedicamentoMNavigation?.DescripcionMedicamentos ?? "No asignado",

                MedicamentoObservacion = consulta.MedicamentoConsultaMNavigation?.ObservacionMedicamento ?? "No asignado",
                ImagenConsulta = consulta.ImagenConsultaINavigation?.IdImagenesImagenINavigation?.DescripcionImagenes ?? "No asignado",
                ImagenCantidad = consulta.ImagenConsultaINavigation?.CantidadImagen ?? 0,
                ImagenObservacion = consulta.ImagenConsultaINavigation?.ObservacionImagen ?? "No asignado",
                DiagnosticoConsulta = consulta.DiagnosticoConsultaDiNavigation?.IdDiagnosticosDiagnosticoDNavigation?.DescripcionDiagnosticos ?? "No asignado",

                LaboratorioConsulta = consulta.LaboratorioConsultaLaNavigation?.IdLaboratoriosLaboratorioLNavigation?.DescripcionLaboratorios ?? "No asignado",
                LaboratorioCantidad = consulta.LaboratorioConsultaLaNavigation?.CantidadLaboratorio ?? 0,
                LaboratorioObservacion = consulta.LaboratorioConsultaLaNavigation?.ObservacionLaboratorio ?? "No asignado",
                ProfesionalNombre = consulta.PacienteConsultaPNavigation?.UsuariocreacionPacientes ?? "No asignado"


            };
            string viewName = tipoDocumento switch
            {
                "receta" => "PdfRecetaView",
                "justificacion" => "PdfJustificacionView",
                "formatoConsulta" => "PdfFormatoConsultaView",
                "laboratorio" => "PdfLaboratorioView",
                "imagen" => "PdfImagenView",
                _ => "PdfRecetaView"
            };


            return new ViewAsPdf(viewName, pdfViewModel);
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
