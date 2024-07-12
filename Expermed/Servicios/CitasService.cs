using Expermed.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Expermed.Servicios
{ /// <summary>
/// Este tipo de servicios siempre se tienen que levantar en el el Program.cs
/// </summary>
    public class CitasService
    {
        private readonly Base_ExpermedContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CitasService(Base_ExpermedContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        /// <summary>
        /// Trae todos los medicos, basandose en el iddelperfil
        /// </summary>
        /// <returns></returns>
        public async Task<List<SelectListItem>> ObtenerMedicoAsync()
        {
            var medicos = await _context.Usuarios
                .Include(u => u.PerfilUsuarioPNavigation)
                .Where(u => u.PerfilUsuarioPNavigation.IdPerfil == 2)
                .ToListAsync();

            return medicos.Select(m => new SelectListItem
            {
                Value = m.IdUsuario.ToString(),
                Text = m.NombresUsuario
            }).ToList();
        }

    /// <summary>
    /// Funcionamiento de reagendar lo mismo que el editar sin emabargo solo modifica la fecha
    /// </summary>
    /// <param name="idCitas"></param>
    /// <param name="nuevaFecha"></param>
    /// <param name="nuevaHora"></param>
    /// <returns></returns>

        public async Task ReagendarCitaAsync(int idCitas, DateTime nuevaFecha, TimeSpan nuevaHora)
        {
            var parameters = new[]
            {
        new SqlParameter("@id_citas", idCitas),
        new SqlParameter("@fechadelacita_citas", nuevaFecha),
        new SqlParameter("@horadelacita_citas", nuevaHora)
    };

            await _context.Database.ExecuteSqlRawAsync("EXEC ReagendarCita @id_citas, @fechadelacita_citas, @horadelacita_citas", parameters);
        }





        /// <summary>
        /// Método para obtener todos los perfiles
        /// </summary>
        /// <returns></returns>
        public async Task<List<Citum>> GetAllCitasAsync()
        {
            var loginUsuario = _httpContextAccessor.HttpContext.Session.GetString("UsuarioNombre");

            if (string.IsNullOrEmpty(loginUsuario))
            {
                throw new Exception("El nombre de usuario no está disponible en la sesión.");
            }

            // Filtrar las citas por el usuario de creación y ordenarlas por fecha
            var citas = await _context.Cita
                .Where(c => c.UsuariocreacionCitas == loginUsuario)
                .Include(c => c.MedicoCitasUNavigation)
                .ThenInclude(m => m.PerfilUsuarioPNavigation) // Incluye el perfil del usuario si es necesario
                .Include(c => c.PacienteCitasPNavigation)
                .Include(c => c.ConsultaCitaCNavigation)
                .OrderBy(c => c.FechadelacitaCitas) // Ordenar por fecha de la cita
                .ToListAsync();

            return citas;
        }



        /// <summary>
        /// Creacion de cita, si necesita modificar la tabla, cambia el sp y el modelo
        /// </summary>
        /// <param name="fechacreacionCitas"></param>
        /// <param name="usuariocreacionCitas"></param>
        /// <param name="fechadelacitaCitas"></param>
        /// <param name="horadelacitaCitas"></param>
        /// <param name="medicoCitasU"></param>
        /// <param name="pacienteCitasP"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        public async Task<int> CrearCitaAsync(DateTime fechacreacionCitas, string usuariocreacionCitas, DateTime fechadelacitaCitas, DateTime horadelacitaCitas, int medicoCitasU, int pacienteCitasP,string estado)
        {
            var fechacreacionCitasParam = new SqlParameter("@FechacreacionCitas", fechacreacionCitas);
            var usuariocreacionCitasParam = new SqlParameter("@UsuariocreacionCitas", usuariocreacionCitas);
            var fechadelacitaCitasParam = new SqlParameter("@FechadelacitaCitas", fechadelacitaCitas);
            var horadelacitaCitasParam = new SqlParameter("@HoradelacitaCitas", horadelacitaCitas);
            var medicoCitasUParam = new SqlParameter("@MedicoCitasU", medicoCitasU);
            var pacienteCitasPParam = new SqlParameter("@PacienteCitasP", pacienteCitasP);
            var estadoCitasParam = new SqlParameter("@Estado", estado);

            var result = await _context.Database.ExecuteSqlRawAsync(
                "EXEC CreateCitaMedica @FechacreacionCitas, @UsuariocreacionCitas, @FechadelacitaCitas, @HoradelacitaCitas, @MedicoCitasU, @PacienteCitasP,@Estado",
                fechacreacionCitasParam, usuariocreacionCitasParam, fechadelacitaCitasParam, horadelacitaCitasParam, medicoCitasUParam, pacienteCitasPParam,estadoCitasParam);

            return result;
        }


        public async Task<IEnumerable<Citum>> ObtenerCitasPorPacienteAsync(int pacienteId)
        {
            var pacienteIdParam = new SqlParameter("@PacienteCitasP", SqlDbType.Int) { Value = pacienteId };

            return await _context.Cita
                .FromSqlRaw("EXEC sp_GetCitaByPaciente @PacienteCitasP", pacienteIdParam)
                .ToListAsync();
        }
       /// <summary>
       /// Este es el metodo qe funciona
       /// </summary>
       /// <param name="fechaCita"></param>
       /// <param name="medicoId"></param>
       /// <returns></returns>
        public async Task<List<TimeSpan>> ObtenerHorasDisponiblesAsync(DateTime fechaCita, int medicoId)
        {
            List<TimeSpan> horasDisponibles = new List<TimeSpan>();

            using (SqlConnection connection = new SqlConnection(_context.Database.GetConnectionString()))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("ObtenerHorasDisponibles", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Parámetros del procedimiento almacenado
                    command.Parameters.AddWithValue("@fecha_cita", fechaCita);
                    command.Parameters.AddWithValue("@medico_citas_u", medicoId);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            TimeSpan hora = reader.GetTimeSpan(0); // Asegúrate de que el índice coincida con la columna
                            horasDisponibles.Add(hora);
                        }
                    }
                }
            }

            return horasDisponibles;
        }

        /// <summary>
        /// Editar cita lo mismo que crear, si modificas la tabla, modifica el sp y el modelo, ademas actualzia aqui
        /// </summary>
        /// <param name="cita"></param>
        public void ActualizarCita(Cita cita)
        {
            using (SqlConnection connection = new SqlConnection(_context.Database.GetConnectionString()))
            {
                connection.Open();
                var citaIdParam = new SqlParameter("@id_cita", cita.IdCitas);
                var fechaCitaParam = new SqlParameter("@FechaCita", cita.FechadelacitaCitas);
                var horaCitaParam = new SqlParameter("@HoraCita", cita.HoradelacitaCitas);
                var medicoIdParam = new SqlParameter("@MedicoId", cita.MedicoCitasU);
                var pacienteIdParam = new SqlParameter("@PacienteId", cita.PacienteCitasP);

                _context.Database.ExecuteSqlRaw(
                    "EXEC sp_UpdateCita @id_cita, @FechaCita, @HoraCita, @MedicoId, @PacienteId",
                    citaIdParam, fechaCitaParam, horaCitaParam, medicoIdParam, pacienteIdParam);
            }
        }
        /// <summary>
        ///  Listado de todas las citas, no tiene sp para listar, uso funciones del mismo EF
        /// </summary>
        /// <param name="id"></param>z
        /// <returns></returns>
        public async Task<Citum> GetCitaByIdAsync(int id)
        {
            return await _context.Cita.FirstOrDefaultAsync(u => u.IdCitas == id);
        }
        /// <summary>
        /// Actualizacion de las citas
        /// </summary>
        /// <param name="citaId"></param>
        /// <param name="estado"></param>
        public void EliminarCita(int citaId)
        {
            using (SqlConnection connection = new SqlConnection(_context.Database.GetConnectionString()))
            {
                var citaIdParam = new SqlParameter("@id_cita", citaId);

                _context.Database.ExecuteSqlRaw("EXEC sp_DeleteCita @id_cita", citaIdParam);
            }
        }



    }
}
