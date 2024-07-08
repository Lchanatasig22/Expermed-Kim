using Expermed.Models;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ConsultaService(Base_ExpermedContext context, PacienteService pacienteService, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _pacienteService = pacienteService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Paciente> BuscarPacientePorNombreAsync(int ci)
        {
            return await _context.Pacientes
                .Where(p => p.CiPacientes == ci)
                .FirstOrDefaultAsync();
        }



        public async Task<int> InsertarConsultaAsync(Consultum consulta)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_context.Database.GetConnectionString()))
                {
                    SqlCommand command = new SqlCommand("InsertConsultaMod", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    // Parámetros de la consulta
                    command.Parameters.AddWithValue("@fechacreacion_consulta", DateTime.Now);
                    command.Parameters.AddWithValue("@usuariocreacion_consulta", consulta.UsuariocreacionConsulta ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@historial_consulta", consulta.HistorialConsulta ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@secuencial_consulta", consulta.SecuencialConsulta ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@paciente_consulta_p", consulta.PacienteConsultaP);
                    command.Parameters.AddWithValue("@motivo_consulta", consulta.MotivoConsulta ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@enfermedad_consulta", consulta.EnfermedadConsulta ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@nombrepariente_consulta", consulta.NombreparienteConsulta ?? (object)DBNull.Value);
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
                    command.Parameters.AddWithValue("@observacion_consulta", consulta.ObservacionConsulta ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@antecedentespersonales_consulta", consulta.AntecedentespersonalesConsulta ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@diasincapacidad_consulta", consulta.DiasincapacidadConsulta ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@medico_consulta_d", consulta.MedicoConsultaD);
                    command.Parameters.AddWithValue("@especialidad_consulta_c", consulta.EspecialidadConsultaC ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@estado_consulta_c", 0);
                    command.Parameters.AddWithValue("@tipo_consulta_c", consulta.TipoConsultaC ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@notasevolucion_consulta", consulta.NotasevolucionConsulta ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@consultaprincipal_consulta", consulta.ConsultaprincipalConsulta ?? (object)DBNull.Value);
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
                    command.Parameters.AddWithValue("@Alergias", consulta.Alergias ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Obser_Alergias", consulta.ObserAlergias ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Cirugias", consulta.Cirugias ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Obser_Cirugias", consulta.ObserCirugias ?? (object)DBNull.Value);

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

                    // Medicamento
                    command.Parameters.AddWithValue("@medicamento_fechacreacion", DateTime.Now);
                    command.Parameters.AddWithValue("@medicamento_usuariocreacion", consulta.MedicamentoConsultaMNavigation.UsuariocreacionMedicamento ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@medicamento_cantidad", consulta.MedicamentoConsultaMNavigation.CantidadMedicamentoC);
                    command.Parameters.AddWithValue("@medicamento_observacion", consulta.MedicamentoConsultaMNavigation.ObservacionMedicamento ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@medicamento_id_medicamentos", consulta.MedicamentoConsultaMNavigation.IdMedicamentosMedicamentoM);

                    // Imagen
                    command.Parameters.AddWithValue("@imagen_fechacreacion", DateTime.Now);
                    command.Parameters.AddWithValue("@imagen_usuariocreacion", consulta.ImagenConsultaINavigation.UsuariocreacionImagen ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@imagen_cantidad", consulta.ImagenConsultaINavigation.CantidadImagen);
                    command.Parameters.AddWithValue("@imagen_observacion", consulta.ImagenConsultaINavigation.ObservacionImagen ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@imagen_id_imagenes", consulta.ImagenConsultaINavigation.IdImagenesImagenI);

                    // Laboratorio
                    command.Parameters.AddWithValue("@laboratorio_fechacreacion", DateTime.Now);
                    command.Parameters.AddWithValue("@laboratorio_usuariocreacion", consulta.LaboratorioConsultaLaNavigation.UsuariocreacionLaboratorio ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@laboratorio_cantidad", consulta.LaboratorioConsultaLaNavigation.CantidadLaboratorio);
                    command.Parameters.AddWithValue("@laboratorio_observacion", consulta.LaboratorioConsultaLaNavigation.ObservacionLaboratorio ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@laboratorio_id_laboratorios", consulta.LaboratorioConsultaLaNavigation.IdLaboratoriosLaboratorioL);

                    // Diagnóstico
                    command.Parameters.AddWithValue("@diagnostico_fechacreacion", DateTime.Now);
                    command.Parameters.AddWithValue("@diagnostico_usuariocreacion", consulta.DiagnosticoConsultaDiNavigation?.UsuariocreacionDiagnostico ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@diagnostico_cantidad", consulta.DiagnosticoConsultaDiNavigation?.CantidadDiagnostico);
                    command.Parameters.AddWithValue("@diagnostico_observacion", consulta.DiagnosticoConsultaDiNavigation?.ObservacionDiagnostico ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@diagnostico_presuntivo", consulta.DiagnosticoConsultaDiNavigation?.PresuntivoDiagnosticos ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@diagnostico_definitivo", consulta.DiagnosticoConsultaDiNavigation?.DefinitivoDiagnosticos ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@diagnostico_id_diagnosticos", consulta.DiagnosticoConsultaDiNavigation?.IdDiagnosticosDiagnosticoD);

                    connection.Open();
                    await command.ExecuteNonQueryAsync();

                    // No se necesita el parámetro de salida si no es necesario devolver el ID de la consulta generada
                    return 1; // Devuelve 1 si la inserción fue exitosa
                }
            }
            catch (Exception ex)
            {
                // Log del mensaje de excepción
                Console.WriteLine($"An error occurred: {ex.Message}");
                return 0;
            }
        }





        // Método para obtener una consulta por su ID
        public async Task<Consultum> ObtenerConsultaPorIdAsync(int idConsulta)
        {
            return await _context.Consulta
                .Include(c => c.PacienteConsultaPNavigation) // Incluye la navegación al paciente
                .Include(c => c.DiagnosticoConsultaDiNavigation) // Reemplaza 'OtraNavegacion1' con la propiedad de navegación real
                .Include(c => c.ImagenConsultaINavigation) // Reemplaza 'OtraNavegacion2' con la propiedad de navegación real
                .Include(c => c.ImagenConsultaINavigation) // Reemplaza 'OtraNavegacion3' con la propiedad de navegación real
                .Include(c => c.LaboratorioConsultaLaNavigation) // Reemplaza 'OtraNavegacion3' con la propiedad de navegación real
                .Include(c => c.MedicamentoConsultaMNavigation)
                                                 // Añade más 'Include' según sea necesario
                .FirstOrDefaultAsync(c => c.IdConsulta == idConsulta);
        }

        public async Task ActualizarConsultaAsync(Consultum consulta)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_ActualizarConsulta @id_consulta, @usuariocreacion_consulta, @historial_consulta, @secuencial_consulta, @paciente_consulta_p, @motivo_consulta, @enfermedad_consulta, @nombrepariente_consulta, @tipopariente_consulta, @telefono_consulta, @temperatura_consulta, @frecuenciarespiratoria_consulta, @presionarterialsistolica_consulta, @presionarterialdiastolica_consulta, @pulso_consulta, @peso_consulta, @talla_consulta, @plantratamiento_consulta, @observacion_consulta, @antecedentespersonales_consulta, @diasincapacidad_consulta, @medico_consulta_d, @especialidad_consulta_c, @estado_consulta_c, @tipo_consulta_c, @notasevolucion_consulta, @consultaprincipal_consulta, @medicamento_usuariocreacion, @medicamento_cantidad, @medicamento_observacion, @medicamento_id_medicamentos, @documento_consulta_d, @detalle_consulta_d, @Cardiopatia, @Obser_Cardiopatia, @Diabetes, @Obser_Diabetes, @Enf_Cardiovascular, @Obser_Enf_Cardiovascular, @Hipertension, @Obser_Hipertensión, @Cancer, @Obser_Cancer, @Tuberculosis, @Obser_Tuberculosis, @Enf_Mental, @Obser_Enf_Mental, @Enf_Infecciosa, @Obser_Enf_Infecciosa, @Mal_Formacion, @Obser_Mal_Formacion, @Otro, @Obser_Otro, @Alergias, @Obser_Alergias, @Cirugias, @Obser_Cirugias, @Org_Sentidos, @Obser_Org_Sentidos, @Respiratorio, @Obser_Respiratorio, @Cardio_Vascular, @Obser_Cardio_Vascular, @Digestivo, @Obser_Digestivo, @Genital, @Obser_Genital, @Urinario, @Obser_Urinario, @M_Esqueletico, @Obser_M_Esqueletico, @Endocrino, @Obser_Endocrino, @Linfatico, @Obser_Linfatico, @Nervioso, @Obser_Nervioso, @Cabeza, @Obser_Cabeza, @Cuello, @Obser_Cuello, @Torax, @Obser_Torax, @Abdomen, @Obser_Abdomen, @Pelvis, @Obser_Pelvis, @Extremidades, @Obser_Extremidades, @imagen_usuariocreacion, @imagen_cantidad, @imagen_observacion, @imagen_id_imagenes, @laboratorio_usuariocreacion, @laboratorio_cantidad, @laboratorio_observacion, @laboratorio_id_laboratorios, @diagnostico_usuariocreacion, @diagnostico_cantidad, @diagnostico_observacion, @diagnostico_presuntivo, @diagnostico_definitivo, @diagnostico_id_diagnosticos",
                new SqlParameter("@id_consulta", consulta.IdConsulta),
                new SqlParameter("@usuariocreacion_consulta", consulta.UsuariocreacionConsulta ?? "sin especificar"),
                new SqlParameter("@historial_consulta", consulta.HistorialConsulta ?? "sin especificar"),
                new SqlParameter("@secuencial_consulta", consulta.SecuencialConsulta ?? (object)DBNull.Value),
                new SqlParameter("@paciente_consulta_p", consulta.PacienteConsultaP),
                new SqlParameter("@motivo_consulta", consulta.MotivoConsulta ?? "sin especificar"),
                new SqlParameter("@enfermedad_consulta", consulta.EnfermedadConsulta ?? "sin especificar"),
                new SqlParameter("@nombrepariente_consulta", consulta.NombreparienteConsulta ?? "sin especificar"),
                new SqlParameter("@tipopariente_consulta", consulta.TipoparienteConsulta),
                new SqlParameter("@telefono_consulta", consulta.TelefonoConsulta ?? (object)DBNull.Value),
                new SqlParameter("@temperatura_consulta", consulta.TemperaturaConsulta ?? "sin especificar"),
                new SqlParameter("@frecuenciarespiratoria_consulta", consulta.FrecuenciarespiratoriaConsulta ?? "sin especificar"),
                new SqlParameter("@presionarterialsistolica_consulta", consulta.PresionarterialsistolicaConsulta ?? "sin especificar"),
                new SqlParameter("@presionarterialdiastolica_consulta", consulta.PresionarterialdiastolicaConsulta ?? "sin especificar"),
                new SqlParameter("@pulso_consulta", consulta.PulsoConsulta ?? "sin especificar"),
                new SqlParameter("@peso_consulta", consulta.PesoConsulta ?? "sin especificar"),
                new SqlParameter("@talla_consulta", consulta.TallaConsulta ?? "sin especificar"),
                new SqlParameter("@plantratamiento_consulta", consulta.PlantratamientoConsulta ?? "sin especificar"),
                new SqlParameter("@observacion_consulta", consulta.ObservacionConsulta ?? "sin especificar"),
                new SqlParameter("@antecedentespersonales_consulta", consulta.AntecedentespersonalesConsulta ?? "sin especificar"),
                new SqlParameter("@diasincapacidad_consulta", consulta.DiasincapacidadConsulta ?? (object)DBNull.Value),
                new SqlParameter("@medico_consulta_d", consulta.MedicoConsultaD),
                new SqlParameter("@especialidad_consulta_c", consulta.EspecialidadConsultaC ?? (object)DBNull.Value),
                new SqlParameter("@estado_consulta_c", consulta.EstadoConsultaC ?? 1),
                new SqlParameter("@tipo_consulta_c", consulta.TipoConsultaC ?? (object)DBNull.Value),
                new SqlParameter("@notasevolucion_consulta", consulta.NotasevolucionConsulta ?? "sin especificar"),
                new SqlParameter("@consultaprincipal_consulta", consulta.ConsultaprincipalConsulta ?? "sin especificar"),
                new SqlParameter("@medicamento_usuariocreacion", consulta.MedicamentoConsultaMNavigation.UsuariocreacionMedicamento ?? "sin especificar"),
                new SqlParameter("@medicamento_cantidad", consulta.MedicamentoConsultaMNavigation.CantidadMedicamentoC),
                new SqlParameter("@medicamento_observacion", consulta.MedicamentoConsultaMNavigation.ObservacionMedicamento ?? "sin especificar"),
                new SqlParameter("@medicamento_id_medicamentos", consulta.MedicamentoConsultaMNavigation.IdMedicamentosMedicamentoM),
                new SqlParameter("@documento_consulta_d", consulta.DocumentoConsultaD ?? (object)DBNull.Value),
                new SqlParameter("@detalle_consulta_d", consulta.DetalleConsultaD ?? (object)DBNull.Value),
                new SqlParameter("@Cardiopatia", consulta.Cardiopatia ?? "sin especificar"),
                new SqlParameter("@Obser_Cardiopatia", consulta.ObserCardiopatia ?? "sin especificar"),
                new SqlParameter("@Diabetes", consulta.Diabetes ?? "sin especificar"),
                new SqlParameter("@Obser_Diabetes", consulta.ObserDiabetes ?? "sin especificar"),
                new SqlParameter("@Enf_Cardiovascular", consulta.EnfCardiovascular ?? "sin especificar"),
                new SqlParameter("@Obser_Enf_Cardiovascular", consulta.ObserEnfCardiovascular ?? "sin especificar"),
                new SqlParameter("@Hipertension", consulta.Hipertension ?? "sin especificar"),
                new SqlParameter("@Obser_Hipertensión", consulta.ObserHipertensión ?? "sin especificar"),
                new SqlParameter("@Cancer", consulta.Cancer ?? "sin especificar"),
                new SqlParameter("@Obser_Cancer", consulta.ObserCancer ?? "sin especificar"),
                new SqlParameter("@Tuberculosis", consulta.Tuberculosis ?? "sin especificar"),
                new SqlParameter("@Obser_Tuberculosis", consulta.ObserTuberculosis ?? "sin especificar"),
                new SqlParameter("@Enf_Mental", consulta.EnfMental ?? "sin especificar"),
                new SqlParameter("@Obser_Enf_Mental", consulta.ObserEnfMental ?? "sin especificar"),
                new SqlParameter("@Enf_Infecciosa", consulta.EnfInfecciosa ?? "sin especificar"),
                new SqlParameter("@Obser_Enf_Infecciosa", consulta.ObserEnfInfecciosa ?? "sin especificar"),
                new SqlParameter("@Mal_Formacion", consulta.MalFormacion ?? "sin especificar"),
                new SqlParameter("@Obser_Mal_Formacion", consulta.ObserMalFormacion ?? "sin especificar"),
                new SqlParameter("@Otro", consulta.Otro ?? "sin especificar"),
                new SqlParameter("@Obser_Otro", consulta.ObserOtro ?? "sin especificar"),
                new SqlParameter("@Alergias", consulta.Alergias ?? "sin especificar"),
                new SqlParameter("@Obser_Alergias", consulta.ObserAlergias ?? "sin especificar"),
                new SqlParameter("@Cirugias", consulta.Cirugias ?? "sin especificar"),
                new SqlParameter("@Obser_Cirugias", consulta.ObserCirugias ?? "sin especificar"),
                new SqlParameter("@Org_Sentidos", consulta.OrgSentidos ?? "sin especificar"),
                new SqlParameter("@Obser_Org_Sentidos", consulta.ObserOrgSentidos ?? "sin especificar"),
                new SqlParameter("@Respiratorio", consulta.Respiratorio ?? "sin especificar"),
                new SqlParameter("@Obser_Respiratorio", consulta.ObserRespiratorio ?? "sin especificar"),
                new SqlParameter("@Cardio_Vascular", consulta.CardioVascular ?? "sin especificar"),
                new SqlParameter("@Obser_Cardio_Vascular", consulta.ObserCardioVascular ?? "sin especificar"),
                new SqlParameter("@Digestivo", consulta.Digestivo ?? "sin especificar"),
                new SqlParameter("@Obser_Digestivo", consulta.ObserDigestivo ?? "sin especificar"),
                new SqlParameter("@Genital", consulta.Genital ?? "sin especificar"),
                new SqlParameter("@Obser_Genital", consulta.ObserGenital ?? "sin especificar"),
                new SqlParameter("@Urinario", consulta.Urinario ?? "sin especificar"),
                new SqlParameter("@Obser_Urinario", consulta.ObserUrinario ?? "sin especificar"),
                new SqlParameter("@M_Esqueletico", consulta.MEsqueletico ?? "sin especificar"),
                new SqlParameter("@Obser_M_Esqueletico", consulta.ObserMEsqueletico ?? "sin especificar"),
                new SqlParameter("@Endocrino", consulta.Endocrino ?? "sin especificar"),
                new SqlParameter("@Obser_Endocrino", consulta.ObserEndocrino ?? "sin especificar"),
                new SqlParameter("@Linfatico", consulta.Linfatico ?? "sin especificar"),
                new SqlParameter("@Obser_Linfatico", consulta.ObserLinfatico ?? "sin especificar"),
                new SqlParameter("@Nervioso", consulta.Nervioso ?? "sin especificar"),
                new SqlParameter("@Obser_Nervioso", consulta.ObserNervioso ?? "sin especificar"),
                new SqlParameter("@Cabeza", consulta.Cabeza ?? "sin especificar"),
                new SqlParameter("@Obser_Cabeza", consulta.ObserCabeza ?? "sin especificar"),
                new SqlParameter("@Cuello", consulta.Cuello ?? "sin especificar"),
                new SqlParameter("@Obser_Cuello", consulta.ObserCuello ?? "sin especificar"),
                new SqlParameter("@Torax", consulta.Torax ?? "sin especificar"),
                new SqlParameter("@Obser_Torax", consulta.ObserTorax ?? "sin especificar"),
                new SqlParameter("@Abdomen", consulta.Abdomen ?? "sin especificar"),
                new SqlParameter("@Obser_Abdomen", consulta.ObserAbdomen ?? "sin especificar"),
                new SqlParameter("@Pelvis", consulta.Pelvis ?? "sin especificar"),
                new SqlParameter("@Obser_Pelvis", consulta.ObserPelvis ?? "sin especificar"),
                new SqlParameter("@Extremidades", consulta.Extremidades ?? "sin especificar"),
                new SqlParameter("@Obser_Extremidades", consulta.ObserExtremidades ?? "sin especificar"),
                new SqlParameter("@imagen_usuariocreacion", consulta.ImagenConsultaINavigation.UsuariocreacionImagen ?? "sin especificar"),
                new SqlParameter("@imagen_cantidad", consulta.ImagenConsultaINavigation.CantidadImagen),
                new SqlParameter("@imagen_observacion", consulta.ImagenConsultaINavigation.ObservacionImagen ?? "sin especificar"),
                new SqlParameter("@imagen_id_imagenes", consulta.ImagenConsultaINavigation.IdImagenesImagenI),
                new SqlParameter("@laboratorio_usuariocreacion", consulta.LaboratorioConsultaLaNavigation.UsuariocreacionLaboratorio ?? "sin especificar"),
                new SqlParameter("@laboratorio_cantidad", consulta.LaboratorioConsultaLaNavigation.CantidadLaboratorio),
                new SqlParameter("@laboratorio_observacion", consulta.LaboratorioConsultaLaNavigation.ObservacionLaboratorio ?? "sin especificar"),
                new SqlParameter("@laboratorio_id_laboratorios", consulta.LaboratorioConsultaLaNavigation.IdLaboratoriosLaboratorioL),
                new SqlParameter("@diagnostico_usuariocreacion", consulta.DiagnosticoConsultaDiNavigation.UsuariocreacionDiagnostico ?? "sin especificar"),
                new SqlParameter("@diagnostico_cantidad", consulta.DiagnosticoConsultaDiNavigation.CantidadDiagnostico),
                new SqlParameter("@diagnostico_observacion", consulta.DiagnosticoConsultaDiNavigation.ObservacionDiagnostico ?? "sin especificar"),
                new SqlParameter("@diagnostico_presuntivo", consulta.DiagnosticoConsultaDiNavigation.PresuntivoDiagnosticos ?? "sin especificar"),
                new SqlParameter("@diagnostico_definitivo", consulta.DiagnosticoConsultaDiNavigation.DefinitivoDiagnosticos ?? "sin especificar"),
                new SqlParameter("@diagnostico_id_diagnosticos", consulta.DiagnosticoConsultaDiNavigation.IdDiagnosticosDiagnosticoD)
            );
        }





        public async Task<List<Consultum>> GetAllConsultasAsync()
        {
            // Obtener el nombre de usuario de la sesión
            var loginUsuario = _httpContextAccessor.HttpContext.Session.GetString("UsuarioNombre");

            if (string.IsNullOrEmpty(loginUsuario))
            {
                throw new Exception("El nombre de usuario no está disponible en la sesión.");
            }

            // Filtrar las consultas por el usuario de creación y el estado igual a 0
            var consultas = await _context.Consulta
                .Where(c => c.UsuariocreacionConsulta == loginUsuario && c.EstadoConsultaC == 0)
                .Include(c => c.DiagnosticoConsultaDiNavigation)
                .Include(c => c.ImagenConsultaINavigation)
                .Include(c => c.LaboratorioConsultaLaNavigation)
                .Include(c => c.MedicamentoConsultaMNavigation)
                .Include(c => c.PacienteConsultaPNavigation)
                .ToListAsync();

            return consultas;
        }


        public async Task<Consultum> ObtenerDatosConsultaAsync(int id)
        {
            var consulta = await _context.Consulta
                .Include(c => c.DiagnosticoConsultaDiNavigation)
                .Include(c => c.ImagenConsultaINavigation)
                .Include(c => c.LaboratorioConsultaLaNavigation)
                .Include(c => c.MedicamentoConsultaMNavigation)
                .Include(c => c.PacienteConsultaPNavigation)
                .FirstOrDefaultAsync(c => c.IdConsulta == id);

            return consulta;
        }

    }
}
