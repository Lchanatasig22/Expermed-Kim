using Expermed.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;

namespace Expermed.Servicios
{
    public class ConsultaService
    {
        private readonly Base_ExpermedContext _context;

        private readonly PacienteService _pacienteService;
        public ConsultaService(Base_ExpermedContext context, PacienteService pacienteService)
        {
            _context = context;
            _pacienteService = pacienteService;
        }

        public async Task<Paciente> BuscarPacientePorNombreAsync(string nombre, int ci)
        {
            return await _context.Pacientes
                .Where(p => p.PrimernombrePacientes.Contains(nombre) ||
                            p.SegundonombrePacientes.Contains(nombre) ||
                            p.PrimerapellidoPacientes.Contains(nombre) ||
                            p.SegundoapellidoPacientes.Contains(nombre) ||
                            p.CiPacientes == ci) 
                .FirstOrDefaultAsync();
        }


        public async Task InsertarConsultaAsync(Consultum consulta)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_context.Database.GetConnectionString()))
                {
                    SqlCommand command = new SqlCommand("sp_InsertarConsulta", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    // Parámetros de la consulta
                    command.Parameters.AddWithValue("@fechacreacion_consulta", DateTime.Now);
                    command.Parameters.AddWithValue("@usuariocreacion_consulta", consulta.UsuariocreacionConsulta ?? "sin especificar");
                    command.Parameters.AddWithValue("@historial_consulta", consulta.HistorialConsulta ?? "sin especificar");
                    command.Parameters.AddWithValue("@secuencial_consulta", consulta.SecuencialConsulta);
                    command.Parameters.AddWithValue("@paciente_consulta_p", consulta.PacienteConsultaP);
                    command.Parameters.AddWithValue("@motivo_consulta", consulta.MotivoConsulta ?? "sin especificar");
                    command.Parameters.AddWithValue("@enfermedad_consulta", consulta.EnfermedadConsulta ?? "sin especificar");
                    command.Parameters.AddWithValue("@nombrepariente_consulta", consulta.NombreparienteConsulta ?? "sin especificar");
                    command.Parameters.AddWithValue("@tipopariente_consulta", consulta.TipoparienteConsulta);
                    command.Parameters.AddWithValue("@telefono_consulta", consulta.TelefonoConsulta ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@temperatura_consulta", consulta.TemperaturaConsulta ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@frecuenciarespiratoria_consulta", consulta.FrecuenciarespiratoriaConsulta ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@presionarterialsistolica_consulta", consulta.PresionarterialsistolicaConsulta ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@presionarterialdiastolica_consulta", consulta.PresionarterialdiastolicaConsulta ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@pulso_consulta", consulta.PulsoConsulta ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@peso_consulta", consulta.PesoConsulta ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@talla_consulta", consulta.TallaConsulta ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@plantratamiento_consulta", consulta.PlantratamientoConsulta ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@observacion_consulta", consulta.ObservacionConsulta ?? "sin especificar");
                    command.Parameters.AddWithValue("@antecedentespersonales_consulta", consulta.AntecedentespersonalesConsulta ?? "sin especificar");
                    command.Parameters.AddWithValue("@diasincapacidad_consulta", consulta.DiasincapacidadConsulta ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@medico_consulta_d", consulta.MedicoConsultaD);
                    command.Parameters.AddWithValue("@especialidad_consulta_c", consulta.EspecialidadConsultaC);
                    command.Parameters.AddWithValue("@estado_consulta_c", consulta.EstadoConsultaC);
                    command.Parameters.AddWithValue("@tipo_consulta_c", consulta.TipoConsultaC);
                    command.Parameters.AddWithValue("@notasevolucion_consulta", consulta.NotasevolucionConsulta ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@consultaprincipal_consulta", consulta.ConsultaprincipalConsulta ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@medicamento_consulta_m", consulta.MedicamentoConsultaM ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@documento_consulta_d", consulta.DocumentoConsultaD ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@detalle_consulta_d", consulta.DetalleConsultaD ?? (object)DBNull.Value);

                    // Antecedentes personales
                    command.Parameters.AddWithValue("@Cardiopatia", consulta.Cardiopatia ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Obser_Cardiopatia", consulta.ObserCardiopatia ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Diabetes", consulta.Diabetes ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Obser_Diabetes", consulta.ObserDiabetes ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Enf_Cardiovascular", consulta.EnfCardiovascular ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Obser_Enf_Cardiovascular", consulta.ObserEnfCardiovascular ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Hipertension", consulta.Hipertension ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Obser_Hipertensión", consulta.ObserHipertensión ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Cancer", consulta.Cancer ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Obser_Cancer", consulta.ObserCancer ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Tuberculosis", consulta.Tuberculosis ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Obser_Tuberculosis", consulta.ObserTuberculosis ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Enf_Mental", consulta.EnfMental ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Obser_Enf_Mental", consulta.ObserEnfMental ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Enf_Infecciosa", consulta.EnfInfecciosa ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Obser_Enf_Infecciosa", consulta.ObserEnfInfecciosa ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Mal_Formacion", consulta.MalFormacion ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Obser_Mal_Formacion", consulta.ObserMalFormacion ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Otro", consulta.Otro ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Obser_Otro", consulta.ObserOtro ?? (object)DBNull.Value);

                    // Revisiones actuales de órganos y sistemas
                    command.Parameters.AddWithValue("@Org_Sentidos", consulta.OrgSentidos ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Obser_Org_Sentidos", consulta.ObserOrgSentidos ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Respiratorio", consulta.Respiratorio ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Obser_Respiratorio", consulta.ObserRespiratorio ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Cardio_Vascular", consulta.CardioVascular ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Obser_Cardio_Vascular", consulta.ObserCardioVascular ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Digestivo", consulta.Digestivo ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Obser_Digestivo", consulta.ObserDigestivo ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Genital", consulta.Genital ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Obser_Genital", consulta.ObserGenital ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Urinario", consulta.Urinario ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Obser_Urinario", consulta.ObserUrinario ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@M_Esqueletico", consulta.MEsqueletico ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Obser_M_Esqueletico", consulta.ObserMEsqueletico ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Endocrino", consulta.Endocrino ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Obser_Endocrino", consulta.ObserEndocrino ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Linfatico", consulta.Linfatico ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Obser_Linfatico", consulta.ObserLinfatico ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Nervioso", consulta.Nervioso ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Obser_Nervioso", consulta.ObserNervioso ?? (object)DBNull.Value);

                    // Examen físico regional
                    command.Parameters.AddWithValue("@Cabeza", consulta.Cabeza ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Obser_Cabeza", consulta.ObserCabeza ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Cuello", consulta.Cuello ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Obser_Cuello", consulta.ObserCuello ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Torax", consulta.Torax ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Obser_Torax", consulta.ObserTorax ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Abdomen", consulta.Abdomen ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Obser_Abdomen", consulta.ObserAbdomen ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Pelvis", consulta.Pelvis ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Obser_Pelvis", consulta.ObserPelvis ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Extremidades", consulta.Extremidades ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Obser_Extremidades", consulta.ObserExtremidades ?? (object)DBNull.Value);

                    // Diagnósticos
                    command.Parameters.AddWithValue("@diagnostico_consulta_di", consulta.DiagnosticoConsultaDi ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@imagen_consulta_i", consulta.ImagenConsultaI ?? (object)DBNull.Value);

                    // Log de parámetros para depuración
                    foreach (SqlParameter param in command.Parameters)
                    {
                        Console.WriteLine($"{param.ParameterName}: {param.Value}");
                    }

                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                // Log del mensaje de excepción
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }


        //public async Task ImprimirConsulta(int idConsulta)
        //{


        //    Consultum modelo =  _context.Consulta.Include (co => co.)  )
        //}
    }
}
