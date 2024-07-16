using Expermed.Models;
using Expermed.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Expermed.Controllers
{
    public class CitaController : Controller
    {/// <summary>
     /// Controlador general de las citas, si necesitas usar otros servicios asegurate de instanciarlos aqui tal como estan
     /// </summary>
        private readonly CitasService _citaService;
        private readonly CatalogService _catalogService;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public CitaController(CitasService citaService, IHttpContextAccessor httpContextAccessor, CatalogService catalogService)
        {
            _citaService = citaService;
            _httpContextAccessor = httpContextAccessor;
            _catalogService = catalogService;

        }

        /// <summary>
        /// Controlador que maneja tanto la vista del formulario como la visualizacion de la lista de las citas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ListarCitas()
        {
            var citas = await _citaService.GetAllCitasAsync();

            // Obtener el nombre de usuario de la sesión
            var usuarioDescripcion = _httpContextAccessor.HttpContext.Session.GetString("UsuarioDescripcion");

            // Pasar el nombre de usuario a la vista
            ViewBag.DescripcionUsuario = usuarioDescripcion ?? "No disponible";
            return View(citas);
        }

        /// <summary>
        /// Controla la visualizacion del formulario recibiendo como parametro el id del paciente
        /// </summary>
        /// <param name="idPaciente"></param>
        /// <returns></returns>
        public async Task<IActionResult> CrearCita(int idPaciente)
        {
            try
            {
                // Obtén el IdUsuario del usuario autenticado desde la sesión
                var idUsuario = _httpContextAccessor.HttpContext.Session.GetInt32("UsuarioId");
                if (idUsuario == null)
                {
                    return RedirectToAction("Login", "Account"); // Redirige al login si el usuario no está autenticado
                }

                // Obtén el nombre del usuario desde la sesión
                var usuarioNombre = _httpContextAccessor.HttpContext.Session.GetString("UsuarioNombre");
                ViewBag.UsuarioNombre = usuarioNombre;
                ViewBag.IdUsuario = idUsuario.Value;

                // Obtén la lista de médicos desde el servicio del catálogo (si es necesario)
                var medicos = await _catalogService.ObtenerMedicoAsync();
                var tiposMedicoSelectList = medicos.Select(d => new SelectListItem
                {
                    Value = d.IdUsuario.ToString(),
                    Text = $"{d.NombresUsuario} {d.ApellidosUsuario} {d.DescripcionUsuario}"
                }).ToList();
                ViewBag.TiposMedico = tiposMedicoSelectList;

                // Crea un objeto Cita con el id del paciente preseleccionado
                var cita = new Cita { PacienteCitasP = idPaciente };

                return View(cita);
            }
            catch (Exception ex)
            {
                // Manejo de errores
                ModelState.AddModelError("", "Error al cargar la página de creación de cita."+ex);
                return RedirectToAction(nameof(Index)); // Redirige a una acción adecuada en caso de error
            }
        }


        /// <summary>
        ///         ///USO PARA PRUEBAS UNITARIASSE PUEDE BORRAR
        /// </summary>
        /// <returns></returns>
        public IActionResult ObtenerHorasDisponibles()
        {
            // Aquí puedes cargar una vista para seleccionar la fecha y el médico
            return View();
        }


        /// <summary>
        /// METODO FILTRAR HORAS POR MEDICO pasando dos parametros 
        /// </summary>
        /// <param name="fechaCita"></param>
        /// <param name="medicoId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ObtenerHorasDisponibles(DateTime fechaCita, int medicoId)
        {
            try
            {
                List<TimeSpan> horasDisponibles = await _citaService.ObtenerHorasDisponiblesAsync(fechaCita, medicoId);

                // Convertir los TimeSpan a cadenas de hora en formato HH:mm
                var horasDisponiblesFormatted = horasDisponibles.Select(ts => ts.ToString(@"hh\:mm")).ToList();

                return Json(horasDisponiblesFormatted);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        /// <summary>
        /// CREACION DE CITAS validando campos nulos 
        /// </summary>
        /// <param name="cita"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        [HttpPost]
       
        public async Task<IActionResult> CrearCita(Cita cita)
        {
            try
            {//OBTIENE EL NOMBRE DEL USUARIO DESDE EL LOGIN
                var usuarioNombre = _httpContextAccessor.HttpContext.Session.GetString("UsuarioNombre");
                //VALIDACION DE DATOS EVITA PARAMETROS NULOS 
                if (ModelState.IsValid)
                {
                    int pacienteId = cita.PacienteCitasP ?? throw new InvalidOperationException("El ID del paciente no puede ser null");

                    // Usar fecha actual si la propiedad es nula
                    DateTime fechaCita = cita.FechadelacitaCitas ?? DateTime.Today;

                    // Convertir TimeOnly a DateTime
                    DateTime horaCitaDateTime = DateTime.Today.Add(cita.HoradelacitaCitas.Value);

                    int medicoId = cita.MedicoCitasU ?? 0;

                    var result = await _citaService.CrearCitaAsync(
                        DateTime.Now,
                        usuarioNombre,
                        fechaCita,
                        horaCitaDateTime, // Aquí pasamos el DateTime con la hora de la cita
                        medicoId,
                        pacienteId, // Asegúrate de pasar el parámetro pacienteId
                        estado: "En Curso"
                    );

                    if (result < 0)
                    {
                        return RedirectToAction(nameof(ListarCitas), new { pacienteId });
                    }
                    ModelState.AddModelError("", "Error al crear la cita.");
                }
                return View(cita);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al crear la cita." + ex);
                return View(cita);
            }
        }


        /// <summary>
        /// Acción para mostrar el formulario de edición ademas de traer los datos que se van a editar
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> EditarCita(int id)
        {
            var cita = await _citaService.GetCitaByIdAsync(id);
            if (cita == null)
            {
                return NotFound();
            }


            ViewBag.TiposMedico = await _citaService.ObtenerMedicoAsync();
            return View(cita);
        }

        /// <summary>
        /// Acción para manejar la solicitud de edición
        /// </summary>
        /// <param name="cita"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> EditarCita(Cita cita)
        {
            if (ModelState.IsValid)
            {
                _citaService.ActualizarCita(cita);
                return RedirectToAction("ListarCitas"); // Redirige a la lista de citas
            }

            ViewBag.TiposMedico = await _citaService.ObtenerMedicoAsync();
            return View(cita);
        }

        /// <summary>
        /// MISMA LOGICA DE EDITAR, SOLO HABRIA QUE BLOQUEAR CAMPOS
        /// </summary>
        /// <param name="idCitas"></param>
        /// <param name="nuevaFecha"></param>
        /// <param name="nuevaHora"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ReagendarCita(int idCitas, DateTime nuevaFecha, TimeSpan nuevaHora)
        {
            try
            {
                await _citaService.ReagendarCitaAsync(idCitas, nuevaFecha, nuevaHora);
                return RedirectToAction("Index");  // O a la vista que desees
            }
            catch (Exception ex)
            {
                // Manejo de errores
                ModelState.AddModelError(string.Empty, ex.Message);
                return View();  // O a la vista de error que desees
            }
        }

        /// <summary>
        /// Actualizar el estado de la cita, dejarlo como esta hasta ver si es mejor eliminarlo completo o no
        /// </summary>
        /// <param name="id"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult EliminarCita(int id)
        {
            _citaService.EliminarCita(id);
            return RedirectToAction("ListarCitas"); // Redirige a la lista de citas
        }






    }
}
