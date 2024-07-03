using Expermed.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
namespace Expermed.Servicios
{
    public class PacienteService
    {
        private readonly Base_ExpermedContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Siempre que se cree un servicio se tiene que instanciar el DbContext
        /// </summary>
        /// <param name="context"></param>
        public PacienteService(Base_ExpermedContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;

        }

        /// <summary>
        /// Manda a listar todos los pacientes incluyenddo la fk de localidad para manejo de campos relacionados, no tiene un sp, se creo con EF
        /// </summary>
        /// <returns></returns>
        public async Task<List<Paciente>> GetAllPacientesAsync()
        {
            // Obtener el nombre de usuario de la sesión
            var loginUsuario = _httpContextAccessor.HttpContext.Session.GetString("UsuarioNombre");

            if (string.IsNullOrEmpty(loginUsuario))
            {
                throw new Exception("El nombre de usuario no está disponible en la sesión.");
            }

            // Filtrar los pacientes por el usuario de creación
            var pacientes = await _context.Pacientes
                .Where(p => p.UsuariocreacionPacientes == loginUsuario)
                .Include(p => p.NacionalidadPacientesLNavigation)
                .ToListAsync();

            return pacientes;
        }
        

        /// <summary>
        /// Servicio de creacion de los pacientes, y haciendo la validacion de los campos nulos para evitarnos errores
        /// </summary>
        /// <param name="paciente"></param>
        /// <returns></returns>
        public async Task<int> CreatePacienteAsync(Paciente paciente)
        {
            var fechacreacionPacientesParam = new SqlParameter("@FechacreacionPacientes", DateTime.Now);
            var usuariocreacionPacientesParam = new SqlParameter("@UsuariocreacionPacientes", paciente.UsuariocreacionPacientes ?? (object)DBNull.Value);
            var fechamodificacionPacientesParam = new SqlParameter("@FechamodificacionPacientes", paciente.FechamodificacionPacientes ?? (object)DBNull.Value);
            var usuariomodificacionPacientesParam = new SqlParameter("@UsuariomodificacionPacientes", paciente.UsuariomodificacionPacientes ?? (object)DBNull.Value);
            var activoPacientesParam = new SqlParameter("@ActivoPacientes", paciente.ActivoPacientes ?? (object)DBNull.Value);
            var tipodocumentoPacientesCParam = new SqlParameter("@TipodocumentoPacientesC", paciente.TipodocumentoPacientesC ?? (object)DBNull.Value);
            var ciPacientesParam = new SqlParameter("@CiPacientes", paciente.CiPacientes ?? (object)DBNull.Value);
            var primernombrePacientesParam = new SqlParameter("@PrimernombrePacientes", paciente.PrimernombrePacientes ?? (object)DBNull.Value);
            var segundonombrePacientesParam = new SqlParameter("@SegundonombrePacientes", paciente.SegundonombrePacientes ?? (object)DBNull.Value);
            var primerapellidoPacientesParam = new SqlParameter("@PrimerapellidoPacientes", paciente.PrimerapellidoPacientes ?? (object)DBNull.Value);
            var segundoapellidoPacientesParam = new SqlParameter("@SegundoapellidoPacientes", paciente.SegundoapellidoPacientes ?? (object)DBNull.Value);
            var sexoPacientesCParam = new SqlParameter("@SexoPacientesC", paciente.SexoPacientesC ?? (object)DBNull.Value);
            var fechanacimientoPacientesParam = new SqlParameter("@FechanacimientoPacientes", paciente.FechanacimientoPacientes ?? (object)DBNull.Value);
            var edadParam = new SqlParameter("@Edad", paciente.Edad ?? (object)DBNull.Value);
            var tiposangrePacientesCParam = new SqlParameter("@TiposangrePacientesC", paciente.TiposangrePacientesC ?? (object)DBNull.Value);
            var donantePacientesParam = new SqlParameter("@DonantePacientes", paciente.DonantePacientes ?? (object)DBNull.Value);
            var estadocivilPacientesCParam = new SqlParameter("@EstadocivilPacientesC", paciente.EstadocivilPacientesC ?? (object)DBNull.Value);
            var formacionprofesionalPacientesCParam = new SqlParameter("@FormacionprofesionalPacientesC", paciente.FormacionprofesionalPacientesC ?? (object)DBNull.Value);
            var telefonofijoPacientesParam = new SqlParameter("@TelefonofijoPacientes", paciente.TelefonofijoPacientes ?? (object)DBNull.Value);
            var telefonocelularPacientesParam = new SqlParameter("@TelefonocelularPacientes", paciente.TelefonocelularPacientes ?? (object)DBNull.Value);
            var emailPacientesParam = new SqlParameter("@EmailPacientes", paciente.EmailPacientes ?? (object)DBNull.Value);
            var nacionalidadPacientesLParam = new SqlParameter("@NacionalidadPacientesL", paciente.NacionalidadPacientesL ?? (object)DBNull.Value);
            var provinciaPacientesLParam = new SqlParameter("@ProvinciaPacientesL", paciente.ProvinciaPacientesL ?? (object)DBNull.Value);
            var direccionPacientesParam = new SqlParameter("@DireccionPacientes", paciente.DireccionPacientes ?? (object)DBNull.Value);
            var ocupacionPacientesParam = new SqlParameter("@OcupacionPacientes", paciente.OcupacionPacientes ?? (object)DBNull.Value);
            var empresaPacientesParam = new SqlParameter("@EmpresaPacientes", paciente.EmpresaPacientes ?? (object)DBNull.Value);
            var segurosaludPacientesCParam = new SqlParameter("@SegurosaludPacientesC", paciente.SegurosaludPacientesC ?? (object)DBNull.Value);

            var result = await _context.Database.ExecuteSqlRawAsync("sp_CreatePaciente @FechacreacionPacientes, @UsuariocreacionPacientes, @FechamodificacionPacientes, @UsuariomodificacionPacientes, @ActivoPacientes, @TipodocumentoPacientesC, @CiPacientes, @PrimernombrePacientes, @SegundonombrePacientes, @PrimerapellidoPacientes, @SegundoapellidoPacientes, @SexoPacientesC, @FechanacimientoPacientes, @Edad, @TiposangrePacientesC, @DonantePacientes, @EstadocivilPacientesC, @FormacionprofesionalPacientesC, @TelefonofijoPacientes, @TelefonocelularPacientes, @EmailPacientes, @NacionalidadPacientesL, @ProvinciaPacientesL, @DireccionPacientes, @OcupacionPacientes, @EmpresaPacientes, @SegurosaludPacientesC",
                fechacreacionPacientesParam, usuariocreacionPacientesParam, fechamodificacionPacientesParam, usuariomodificacionPacientesParam, activoPacientesParam, tipodocumentoPacientesCParam, ciPacientesParam, primernombrePacientesParam, segundonombrePacientesParam, primerapellidoPacientesParam, segundoapellidoPacientesParam, sexoPacientesCParam, fechanacimientoPacientesParam, edadParam, tiposangrePacientesCParam, donantePacientesParam, estadocivilPacientesCParam, formacionprofesionalPacientesCParam, telefonofijoPacientesParam, telefonocelularPacientesParam, emailPacientesParam, nacionalidadPacientesLParam, provinciaPacientesLParam, direccionPacientesParam, ocupacionPacientesParam, empresaPacientesParam, segurosaludPacientesCParam);

            return result;
        }

        /// <summary>
        /// manda a traer el paciente por el Id lo usamos para las ediciones y para cualquier cosa que necesite la informcion de un paciente especifico
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Paciente> GetPacienteByIdAsync(int id)
        {
            return await _context.Pacientes.FirstOrDefaultAsync(u => u.IdPacientes == id);
        }

        /// <summary>
        /// Actualizacion de los pacientes
        /// </summary>
        /// <param name="paciente"></param>
        /// <returns></returns>
        public async Task<int> UpdatePacienteAsync(Paciente paciente)
        {
            var idPacientesParam = new SqlParameter("@IdPacientes", paciente.IdPacientes);
            var fechacreacionPacientesParam = new SqlParameter("@FechacreacionPacientes", paciente.FechacreacionPacientes ?? (object)DBNull.Value);
            var usuariocreacionPacientesParam = new SqlParameter("@UsuariocreacionPacientes", paciente.UsuariocreacionPacientes ?? (object)DBNull.Value);
            var fechamodificacionPacientesParam = new SqlParameter("@FechamodificacionPacientes", paciente.FechamodificacionPacientes ?? (object)DBNull.Value);
            var usuariomodificacionPacientesParam = new SqlParameter("@UsuariomodificacionPacientes", paciente.UsuariomodificacionPacientes ?? (object)DBNull.Value);
            var activoPacientesParam = new SqlParameter("@ActivoPacientes", paciente.ActivoPacientes ?? (object)DBNull.Value);
            var tipodocumentoPacientesCParam = new SqlParameter("@TipodocumentoPacientesC", paciente.TipodocumentoPacientesC ?? (object)DBNull.Value);
            var ciPacientesParam = new SqlParameter("@CiPacientes", paciente.CiPacientes ?? (object)DBNull.Value);
            var primernombrePacientesParam = new SqlParameter("@PrimernombrePacientes", paciente.PrimernombrePacientes ?? (object)DBNull.Value);
            var segundonombrePacientesParam = new SqlParameter("@SegundonombrePacientes", paciente.SegundonombrePacientes ?? (object)DBNull.Value);
            var primerapellidoPacientesParam = new SqlParameter("@PrimerapellidoPacientes", paciente.PrimerapellidoPacientes ?? (object)DBNull.Value);
            var segundoapellidoPacientesParam = new SqlParameter("@SegundoapellidoPacientes", paciente.SegundoapellidoPacientes ?? (object)DBNull.Value);
            var sexoPacientesCParam = new SqlParameter("@SexoPacientesC", paciente.SexoPacientesC ?? (object)DBNull.Value);
            var fechanacimientoPacientesParam = new SqlParameter("@FechanacimientoPacientes", paciente.FechanacimientoPacientes ?? (object)DBNull.Value);
            var edadParam = new SqlParameter("@Edad", paciente.Edad ?? (object)DBNull.Value);
            var tiposangrePacientesCParam = new SqlParameter("@TiposangrePacientesC", paciente.TiposangrePacientesC ?? (object)DBNull.Value);
            var donantePacientesParam = new SqlParameter("@DonantePacientes", paciente.DonantePacientes ?? (object)DBNull.Value);
            var estadocivilPacientesCParam = new SqlParameter("@EstadocivilPacientesC", paciente.EstadocivilPacientesC ?? (object)DBNull.Value);
            var formacionprofesionalPacientesCParam = new SqlParameter("@FormacionprofesionalPacientesC", paciente.FormacionprofesionalPacientesC ?? (object)DBNull.Value);
            var telefonofijoPacientesParam = new SqlParameter("@TelefonofijoPacientes", paciente.TelefonofijoPacientes ?? (object)DBNull.Value);
            var telefonocelularPacientesParam = new SqlParameter("@TelefonocelularPacientes", paciente.TelefonocelularPacientes ?? (object)DBNull.Value);
            var emailPacientesParam = new SqlParameter("@EmailPacientes", paciente.EmailPacientes ?? (object)DBNull.Value);
            var nacionalidadPacientesLParam = new SqlParameter("@NacionalidadPacientesL", paciente.NacionalidadPacientesL ?? (object)DBNull.Value);
            var provinciaPacientesLParam = new SqlParameter("@ProvinciaPacientesL", paciente.ProvinciaPacientesL ?? (object)DBNull.Value);
            var direccionPacientesParam = new SqlParameter("@DireccionPacientes", paciente.DireccionPacientes ?? (object)DBNull.Value);
            var ocupacionPacientesParam = new SqlParameter("@OcupacionPacientes", paciente.OcupacionPacientes ?? (object)DBNull.Value);
            var empresaPacientesParam = new SqlParameter("@EmpresaPacientes", paciente.EmpresaPacientes ?? (object)DBNull.Value);
            var segurosaludPacientesCParam = new SqlParameter("@SegurosaludPacientesC", paciente.SegurosaludPacientesC ?? (object)DBNull.Value);

            var result = await _context.Database.ExecuteSqlRawAsync("sp_UpdatePaciente @IdPacientes, @FechacreacionPacientes, @UsuariocreacionPacientes, @FechamodificacionPacientes, @UsuariomodificacionPacientes, @ActivoPacientes, @TipodocumentoPacientesC, @CiPacientes, @PrimernombrePacientes, @SegundonombrePacientes, @PrimerapellidoPacientes, @SegundoapellidoPacientes, @SexoPacientesC, @FechanacimientoPacientes, @Edad, @TiposangrePacientesC, @DonantePacientes, @EstadocivilPacientesC, @FormacionprofesionalPacientesC, @TelefonofijoPacientes, @TelefonocelularPacientes, @EmailPacientes, @NacionalidadPacientesL, @ProvinciaPacientesL, @DireccionPacientes, @OcupacionPacientes, @EmpresaPacientes, @SegurosaludPacientesC",
                idPacientesParam, fechacreacionPacientesParam, usuariocreacionPacientesParam, fechamodificacionPacientesParam, usuariomodificacionPacientesParam, activoPacientesParam, tipodocumentoPacientesCParam, ciPacientesParam, primernombrePacientesParam, segundonombrePacientesParam, primerapellidoPacientesParam, segundoapellidoPacientesParam, sexoPacientesCParam, fechanacimientoPacientesParam, edadParam, tiposangrePacientesCParam, donantePacientesParam, estadocivilPacientesCParam, formacionprofesionalPacientesCParam, telefonofijoPacientesParam, telefonocelularPacientesParam, emailPacientesParam, nacionalidadPacientesLParam, provinciaPacientesLParam, direccionPacientesParam, ocupacionPacientesParam, empresaPacientesParam, segurosaludPacientesCParam);

            return result;
        }



        public async Task CambiarEstadoPacienteAsync(int id, bool activo)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente != null)
            {
                paciente.ActivoPacientes = activo ? 1 : 0;
                await _context.SaveChangesAsync();
            }
        }

    }
}
