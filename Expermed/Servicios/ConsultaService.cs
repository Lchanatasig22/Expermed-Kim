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

        public async Task InsertarConsultaAsync(Consultum model)
        {
            using (SqlConnection connection = new SqlConnection(_context.Database.GetConnectionString()))
            {
                SqlCommand command = new SqlCommand("sp_InsertarConsulta", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@fechacreacion_consulta", DateTime.Now);
                command.Parameters.AddWithValue("@usuariocreacion_consulta", model.UsuariocreacionConsulta ?? "sin especificar");
                command.Parameters.AddWithValue("@historial_consulta", model.HistorialConsulta ?? "sin especificar");
                command.Parameters.AddWithValue("@secuencial_consulta", model.SecuencialConsulta);
                command.Parameters.AddWithValue("@paciente_consulta_p", model.PacienteConsultaP);
                command.Parameters.AddWithValue("@motivo_consulta", model.MotivoConsulta ?? "sin especificar");
                command.Parameters.AddWithValue("@enfermedad_consulta", model.EnfermedadConsulta ?? "sin especificar");
                command.Parameters.AddWithValue("@nombrepariente_consulta", model.NombreparienteConsulta ?? "sin especificar");
                command.Parameters.AddWithValue("@tipopariente_consulta", model.TipoparienteConsulta);
                command.Parameters.AddWithValue("@telefono_consulta", model.TelefonoConsulta ?? 0);
                command.Parameters.AddWithValue("@temperatura_consulta", model.TemperaturaConsulta ?? "sin especificar");
                command.Parameters.AddWithValue("@frecuenciarespiratoria_consulta", model.FrecuenciarespiratoriaConsulta );
                command.Parameters.AddWithValue("@presionarterialsistolica_consulta", model.PresionarterialsistolicaConsulta);
                command.Parameters.AddWithValue("@presionarterialdiastolica_consulta", model.PresionarterialdiastolicaConsulta);
                command.Parameters.AddWithValue("@pulso_consulta", model.PulsoConsulta);
                command.Parameters.AddWithValue("@peso_consulta", model.PesoConsulta );
                command.Parameters.AddWithValue("@talla_consulta", model.TallaConsulta);
                command.Parameters.AddWithValue("@plantratamiento_consulta", model.PlantratamientoConsulta ?? "sin especificar");
                command.Parameters.AddWithValue("@observacion_consulta", model.ObservacionConsulta ?? "sin especificar");
                command.Parameters.AddWithValue("@antecedentespersonales_consulta", model.AntecedentespersonalesConsulta ?? "sin especificar");
                command.Parameters.AddWithValue("@diasincapacidad_consulta", model.DiasincapacidadConsulta ?? 0);
                command.Parameters.AddWithValue("@medico_consulta_d", model.MedicoConsultaD  );
                command.Parameters.AddWithValue("@especialidad_consulta_c", model.EspecialidadConsultaC  );
                command.Parameters.AddWithValue("@estado_consulta_c", model.EstadoConsultaC );
                command.Parameters.AddWithValue("@tipo_consulta_c", model.TipoConsultaC);
                command.Parameters.AddWithValue("@notasevolucion_consulta", model.NotasevolucionConsulta);
                command.Parameters.AddWithValue("@consultaprincipal_consulta", model.ConsultaprincipalConsulta);
                command.Parameters.AddWithValue("@medicamento_consulta_m", model.MedicamentoConsultaM);
                command.Parameters.AddWithValue("@documento_consulta_d", model.DocumentoConsultaD );
                command.Parameters.AddWithValue("@detalle_consulta_d", model.DetalleConsultaD );
                command.Parameters.AddWithValue("@Cardiopatia", model.Cardiopatia ?? "sin especificar");
                command.Parameters.AddWithValue("@Obser_Cardiopatia", model.ObserCardiopatia ?? "sin especificar");
                command.Parameters.AddWithValue("@Diabetes", model.Diabetes ?? "sin especificar");
                command.Parameters.AddWithValue("@Obser_Diabetes", model.ObserDiabetes ?? "sin especificar");
                command.Parameters.AddWithValue("@Enf_Cardiovascular", model.EnfCardiovascular ?? "sin especificar");
                command.Parameters.AddWithValue("@Obser_Enf_Cardiovascular", model.ObserEnfCardiovascular ?? "sin especificar");
                command.Parameters.AddWithValue("@Hipertension", model.Hipertension ?? "sin especificar");
                command.Parameters.AddWithValue("@Obser_Hipertensión", model.ObserHipertensión ?? "sin especificar");
                command.Parameters.AddWithValue("@Cancer", model.Cancer ?? "sin especificar");
                command.Parameters.AddWithValue("@Obser_Cancer", model.ObserCancer ?? "sin especificar");
                command.Parameters.AddWithValue("@Tuberculosis", model.Tuberculosis ?? "sin especificar");
                command.Parameters.AddWithValue("@Obser_Tuberculosis", model.ObserTuberculosis ?? "sin especificar");
                command.Parameters.AddWithValue("@Enf_Mental", model.EnfMental ?? "sin especificar");
                command.Parameters.AddWithValue("@Obser_Enf_Mental", model.ObserEnfMental ?? "sin especificar");
                command.Parameters.AddWithValue("@Enf_Infecciosa", model.EnfInfecciosa ?? "sin especificar");
                command.Parameters.AddWithValue("@Obser_Enf_Infecciosa", model.ObserEnfInfecciosa ?? "sin especificar");
                command.Parameters.AddWithValue("@Mal_Formacion", model.MalFormacion ?? "sin especificar");
                command.Parameters.AddWithValue("@Obser_Mal_Formacion", model.ObserMalFormacion ?? "sin especificar");
                command.Parameters.AddWithValue("@Otro", model.Otro ?? "sin especificar");
                command.Parameters.AddWithValue("@Obser_Otro", model.ObserOtro ?? "sin especificar");
                command.Parameters.AddWithValue("@Alergias", model.Alergias ?? "sin especificar");
                command.Parameters.AddWithValue("@Obser_Alergias", model.ObserAlergias ?? "sin especificar");
                command.Parameters.AddWithValue("@Cirugias", model.Cirugias ?? "sin especificar");
                command.Parameters.AddWithValue("@Obser_Cirugias", model.ObserCirugias ?? "sin especificar");
                command.Parameters.AddWithValue("@Org_Sentidos", model.OrgSentidos ?? "sin especificar");
                command.Parameters.AddWithValue("@Obser_Org_Sentidos", model.ObserOrgSentidos ?? "sin especificar");
                command.Parameters.AddWithValue("@Respiratorio", model.Respiratorio ?? "sin especificar");
                command.Parameters.AddWithValue("@Obser_Respiratorio", model.ObserRespiratorio ?? "sin especificar");
                command.Parameters.AddWithValue("@Cardio_Vascular", model.CardioVascular ?? "sin especificar");
                command.Parameters.AddWithValue("@Obser_Cardio_Vascular", model.ObserCardioVascular ?? "sin especificar");
                command.Parameters.AddWithValue("@Digestivo", model.Digestivo ?? "sin especificar");
                command.Parameters.AddWithValue("@Obser_Digestivo", model.ObserDigestivo ?? "sin especificar");
                command.Parameters.AddWithValue("@Genital", model.Genital ?? "sin especificar");
                command.Parameters.AddWithValue("@Obser_Genital", model.ObserGenital ?? "sin especificar");
                command.Parameters.AddWithValue("@Urinario", model.Urinario ?? "sin especificar");
                command.Parameters.AddWithValue("@Obser_Urinario", model.ObserUrinario ?? "sin especificar");
                command.Parameters.AddWithValue("@M_Esqueletico", model.MEsqueletico ?? "sin especificar");
                command.Parameters.AddWithValue("@Obser_M_Esqueletico", model.ObserMEsqueletico ?? "sin especificar");
                command.Parameters.AddWithValue("@Endocrino", model.Endocrino ?? "sin especificar");
                command.Parameters.AddWithValue("@Obser_Endocrino", model.ObserEndocrino ?? "sin especificar");
                command.Parameters.AddWithValue("@Linfatico", model.Linfatico ?? "sin especificar");
                command.Parameters.AddWithValue("@Obser_Linfatico", model.ObserLinfatico ?? "sin especificar");
                command.Parameters.AddWithValue("@Nervioso", model.Nervioso ?? "sin especificar");
                command.Parameters.AddWithValue("@Obser_Nervioso", model.ObserNervioso ?? "sin especificar");
                command.Parameters.AddWithValue("@Cabeza", model.Cabeza ?? "sin especificar");
                command.Parameters.AddWithValue("@Obser_Cabeza", model.ObserCabeza ?? "sin especificar");
                command.Parameters.AddWithValue("@Cuello", model.Cuello ?? "sin especificar");
                command.Parameters.AddWithValue("@Obser_Cuello", model.ObserCuello ?? "sin especificar");
                command.Parameters.AddWithValue("@Torax", model.Torax ?? "sin especificar");
                command.Parameters.AddWithValue("@Obser_Torax", model.ObserTorax ?? "sin especificar");
                command.Parameters.AddWithValue("@Abdomen", model.Abdomen ?? "sin especificar");
                command.Parameters.AddWithValue("@Obser_Abdomen", model.ObserAbdomen ?? "sin especificar");
                command.Parameters.AddWithValue("@Pelvis", model.Pelvis ?? "sin especificar");
                command.Parameters.AddWithValue("@Obser_Pelvis", model.ObserPelvis ?? "sin especificar");
                command.Parameters.AddWithValue("@Extremidades", model.Extremidades ?? "sin especificar");
                command.Parameters.AddWithValue("@Obser_Extremidades", model.ObserExtremidades ?? "sin especificar");
                command.Parameters.AddWithValue("@imagen_consulta_i", model.ImagenConsultaI );
                command.Parameters.AddWithValue("@diagnostico_consulta_di", model.DiagnosticoConsultaDi);

                connection.Open();
                await command.ExecuteNonQueryAsync();
            }
        }

    }
}
