using Expermed.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Paciente> BuscarPacientePorNombreAsync(string nombre)
        {
            return await _context.Pacientes
                .Where(p => p.PrimernombrePacientes.Contains(nombre) || p.SegundonombrePacientes.Contains(nombre) || p.PrimerapellidoPacientes.Contains(nombre) || p.SegundoapellidoPacientes.Contains(nombre))
                .FirstOrDefaultAsync();
        }

        public async Task InsertarConsultaAsync(Consultum consulta)
        {
            using (SqlConnection connection = new SqlConnection(_context.Database.GetConnectionString()))
            {
                SqlCommand command = new SqlCommand("sp_InsertarConsulta", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@fechacreacion_consulta", DateTime.Now);
                command.Parameters.AddWithValue("@usuariocreacion_consulta", consulta.UsuariocreacionConsulta ?? "sin especificar");
                command.Parameters.AddWithValue("@historial_consulta", consulta.HistorialConsulta ?? "sin especificar");
                command.Parameters.AddWithValue("@secuencial_consulta", consulta.SecuencialConsulta);
                command.Parameters.AddWithValue("@paciente_consulta_p", consulta.PacienteConsultaP);
                command.Parameters.AddWithValue("@motivo_consulta", consulta.MotivoConsulta ?? "sin especificar");
                command.Parameters.AddWithValue("@enfermedad_consulta", consulta.EnfermedadConsulta ?? "sin especificar");
                command.Parameters.AddWithValue("@nombrepariente_consulta", consulta.NombreparienteConsulta ?? "sin especificar");
                command.Parameters.AddWithValue("@tipopariente_consulta", consulta.TipoparienteConsulta);
                command.Parameters.AddWithValue("@telefono_consulta", consulta.TelefonoConsulta ?? 0);
                command.Parameters.AddWithValue("@temperatura_consulta", consulta.TemperaturaConsulta ?? "sin especificar");
                command.Parameters.AddWithValue("@frecuenciarespiratoria_consulta", consulta.FrecuenciarespiratoriaConsulta);
                command.Parameters.AddWithValue("@presionarterialsistolica_consulta", consulta.PresionarterialsistolicaConsulta);
                command.Parameters.AddWithValue("@presionarterialdiastolica_consulta", consulta.PresionarterialdiastolicaConsulta);
                command.Parameters.AddWithValue("@pulso_consulta", consulta.PulsoConsulta);
                command.Parameters.AddWithValue("@peso_consulta", consulta.PesoConsulta);
                command.Parameters.AddWithValue("@talla_consulta", consulta.TallaConsulta);
                command.Parameters.AddWithValue("@plantratamiento_consulta", consulta.PlantratamientoConsulta);
                command.Parameters.AddWithValue("@observacion_consulta", consulta.ObservacionConsulta ?? "sin especificar");
                command.Parameters.AddWithValue("@antecedentespersonales_consulta", consulta.AntecedentespersonalesConsulta ?? "sin especificar");
                command.Parameters.AddWithValue("@diasincapacidad_consulta", consulta.DiasincapacidadConsulta ?? 0);
                command.Parameters.AddWithValue("@medico_consulta_d", consulta.MedicoConsultaD  );
                command.Parameters.AddWithValue("@especialidad_consulta_c", consulta.EspecialidadConsultaC  );
                command.Parameters.AddWithValue("@estado_consulta_c", consulta.EstadoConsultaC );
                command.Parameters.AddWithValue("@tipo_consulta_c", consulta.TipoConsultaC);
                command.Parameters.AddWithValue("@notasevolucion_consulta", consulta.NotasevolucionConsulta);
                command.Parameters.AddWithValue("@consultaprincipal_consulta", consulta.ConsultaprincipalConsulta);
                command.Parameters.AddWithValue("@medicamento_consulta_m", consulta.MedicamentoConsultaM);
                command.Parameters.AddWithValue("@documento_consulta_d", consulta.DocumentoConsultaD );
                command.Parameters.AddWithValue("@detalle_consulta_d", consulta.DetalleConsultaD );
                command.Parameters.AddWithValue("@Cardiopatia", consulta.Cardiopatia ?? "sin especificar");
                command.Parameters.AddWithValue("@Obser_Cardiopatia", consulta.ObserCardiopatia ?? "sin especificar");
                command.Parameters.AddWithValue("@Diabetes", consulta.Diabetes ?? "sin especificar");
                command.Parameters.AddWithValue("@Obser_Diabetes", consulta.ObserDiabetes ?? "sin especificar");
                command.Parameters.AddWithValue("@Enf_Cardiovascular", consulta.EnfCardiovascular ?? "sin especificar");
                command.Parameters.AddWithValue("@Obser_Enf_Cardiovascular", consulta.ObserEnfCardiovascular ?? "sin especificar");
                command.Parameters.AddWithValue("@Hipertension", consulta.Hipertension ?? "sin especificar");
                command.Parameters.AddWithValue("@Obser_Hipertensión", consulta.ObserHipertensión ?? "sin especificar");
                command.Parameters.AddWithValue("@Cancer", consulta.Cancer ?? "sin especificar");
                command.Parameters.AddWithValue("@Obser_Cancer", consulta.ObserCancer ?? "sin especificar");
                command.Parameters.AddWithValue("@Tuberculosis", consulta.Tuberculosis ?? "sin especificar");
                command.Parameters.AddWithValue("@Obser_Tuberculosis", consulta.ObserTuberculosis ?? "sin especificar");
                command.Parameters.AddWithValue("@Enf_Mental", consulta.EnfMental ?? "sin especificar");
                command.Parameters.AddWithValue("@Obser_Enf_Mental", consulta.ObserEnfMental ?? "sin especificar");
                command.Parameters.AddWithValue("@Enf_Infecciosa", consulta.EnfInfecciosa ?? "sin especificar");
                command.Parameters.AddWithValue("@Obser_Enf_Infecciosa", consulta.ObserEnfInfecciosa ?? "sin especificar");
                command.Parameters.AddWithValue("@Mal_Formacion", consulta.MalFormacion ?? "sin especificar");
                command.Parameters.AddWithValue("@Obser_Mal_Formacion", consulta.ObserMalFormacion ?? "sin especificar");
                command.Parameters.AddWithValue("@Otro", consulta.Otro ?? "sin especificar");
                command.Parameters.AddWithValue("@Obser_Otro", consulta.ObserOtro ?? "sin especificar");
                command.Parameters.AddWithValue("@Alergias", consulta.Alergias ?? "sin especificar");
                command.Parameters.AddWithValue("@Obser_Alergias", consulta.ObserAlergias ?? "sin especificar");
                command.Parameters.AddWithValue("@Cirugias", consulta.Cirugias ?? "sin especificar");
                command.Parameters.AddWithValue("@Obser_Cirugias", consulta.ObserCirugias ?? "sin especificar");
                command.Parameters.AddWithValue("@Org_Sentidos", consulta.OrgSentidos ?? "sin especificar");
                command.Parameters.AddWithValue("@Obser_Org_Sentidos", consulta.ObserOrgSentidos ?? "sin especificar");
                command.Parameters.AddWithValue("@Respiratorio", consulta.Respiratorio ?? "sin especificar");
                command.Parameters.AddWithValue("@Obser_Respiratorio", consulta.ObserRespiratorio ?? "sin especificar");
                command.Parameters.AddWithValue("@Cardio_Vascular", consulta.CardioVascular ?? "sin especificar");
                command.Parameters.AddWithValue("@Obser_Cardio_Vascular", consulta.ObserCardioVascular ?? "sin especificar");
                command.Parameters.AddWithValue("@Digestivo", consulta.Digestivo ?? "sin especificar");
                command.Parameters.AddWithValue("@Obser_Digestivo", consulta.ObserDigestivo ?? "sin especificar");
                command.Parameters.AddWithValue("@Genital", consulta.Genital ?? "sin especificar");
                command.Parameters.AddWithValue("@Obser_Genital", consulta.ObserGenital ?? "sin especificar");
                command.Parameters.AddWithValue("@Urinario", consulta.Urinario ?? "sin especificar");
                command.Parameters.AddWithValue("@Obser_Urinario", consulta.ObserUrinario ?? "sin especificar");
                command.Parameters.AddWithValue("@M_Esqueletico", consulta.MEsqueletico ?? "sin especificar");
                command.Parameters.AddWithValue("@Obser_M_Esqueletico", consulta.ObserMEsqueletico ?? "sin especificar");
                command.Parameters.AddWithValue("@Endocrino", consulta.Endocrino ?? "sin especificar");
                command.Parameters.AddWithValue("@Obser_Endocrino", consulta.ObserEndocrino ?? "sin especificar");
                command.Parameters.AddWithValue("@Linfatico", consulta.Linfatico ?? "sin especificar");
                command.Parameters.AddWithValue("@Obser_Linfatico", consulta.ObserLinfatico ?? "sin especificar");
                command.Parameters.AddWithValue("@Nervioso", consulta.Nervioso ?? "sin especificar");
                command.Parameters.AddWithValue("@Obser_Nervioso", consulta.ObserNervioso ?? "sin especificar");
                command.Parameters.AddWithValue("@Cabeza", consulta.Cabeza ?? "sin especificar");
                command.Parameters.AddWithValue("@Obser_Cabeza", consulta.ObserCabeza ?? "sin especificar");
                command.Parameters.AddWithValue("@Cuello", consulta.Cuello ?? "sin especificar");
                command.Parameters.AddWithValue("@Obser_Cuello", consulta.ObserCuello ?? "sin especificar");
                command.Parameters.AddWithValue("@Torax", consulta.Torax ?? "sin especificar");
                command.Parameters.AddWithValue("@Obser_Torax", consulta.ObserTorax ?? "sin especificar");
                command.Parameters.AddWithValue("@Abdomen", consulta.Abdomen ?? "sin especificar");
                command.Parameters.AddWithValue("@Obser_Abdomen", consulta.ObserAbdomen ?? "sin especificar");
                command.Parameters.AddWithValue("@Pelvis", consulta.Pelvis ?? "sin especificar");
                command.Parameters.AddWithValue("@Obser_Pelvis", consulta.ObserPelvis ?? "sin especificar");
                command.Parameters.AddWithValue("@Extremidades", consulta.Extremidades ?? "sin especificar");
                command.Parameters.AddWithValue("@Obser_Extremidades", consulta.ObserExtremidades ?? "sin especificar");
                command.Parameters.AddWithValue("@imagen_consulta_i", consulta.ImagenConsultaI );
                command.Parameters.AddWithValue("@diagnostico_consulta_di", consulta.DiagnosticoConsultaDi);

                connection.Open();
                await command.ExecuteNonQueryAsync();
            }
        }

    }
}
