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
                    command.Parameters.AddWithValue("@especialidad_consulta_c", consulta.EspecialidadConsultaC ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@estado_consulta_c", consulta.EstadoConsultaC);
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
                    command.Parameters.AddWithValue("@medicamento_fechacreacion", consulta.MedicamentoConsultaMNavigation.FechacreacionMedicamento);
                    command.Parameters.AddWithValue("@medicamento_usuariocreacion", consulta.MedicamentoConsultaMNavigation.UsuariocreacionMedicamento ?? "sin especificar");
                    command.Parameters.AddWithValue("@medicamento_cantidad", consulta.MedicamentoConsultaMNavigation.CantidadMedicamentoC);
                    command.Parameters.AddWithValue("@medicamento_observacion", consulta.MedicamentoConsultaMNavigation.ObservacionMedicamento ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@medicamento_id_medicamentos", consulta.MedicamentoConsultaMNavigation.IdMedicamentosMedicamentoM);

                    // Imagen
                    command.Parameters.AddWithValue("@imagen_fechacreacion", consulta.ImagenConsultaINavigation.FechacreacionImagen);
                    command.Parameters.AddWithValue("@imagen_usuariocreacion", consulta.ImagenConsultaINavigation.UsuariocreacionImagen ?? "sin especificar");
                    command.Parameters.AddWithValue("@imagen_cantidad", consulta.ImagenConsultaINavigation.CantidadImagen);
                    command.Parameters.AddWithValue("@imagen_observacion", consulta.ImagenConsultaINavigation.ObservacionImagen ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@imagen_id_imagenes", consulta.ImagenConsultaINavigation.IdImagenesImagenI);

                    // Laboratorio
                    command.Parameters.AddWithValue("@laboratorio_fechacreacion", consulta.LaboratorioConsultaLaNavigation.FechacreacionLaboratorio);
                    command.Parameters.AddWithValue("@laboratorio_usuariocreacion", consulta.LaboratorioConsultaLaNavigation.UsuariocreacionLaboratorio ?? "sin especificar");
                    command.Parameters.AddWithValue("@laboratorio_cantidad", consulta.LaboratorioConsultaLaNavigation.CantidadLaboratorio);
                    command.Parameters.AddWithValue("@laboratorio_observacion", consulta.LaboratorioConsultaLaNavigation.ObservacionLaboratorio ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@laboratorio_id_laboratorios", consulta.LaboratorioConsultaLaNavigation.IdLaboratoriosLaboratorioL);

                    // Diagnóstico
                    command.Parameters.AddWithValue("@diagnostico_fechacreacion", consulta.DiagnosticoConsultaDiNavigation.FechacreacionDiagnostico);
                    command.Parameters.AddWithValue("@diagnostico_usuariocreacion", consulta.DiagnosticoConsultaDiNavigation.UsuariocreacionDiagnostico ?? "sin especificar");
                    command.Parameters.AddWithValue("@diagnostico_cantidad", consulta.DiagnosticoConsultaDiNavigation.CantidadDiagnostico);
                    command.Parameters.AddWithValue("@diagnostico_observacion", consulta.DiagnosticoConsultaDiNavigation.ObservacionDiagnostico ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@diagnostico_presuntivo", consulta.DiagnosticoConsultaDiNavigation.PresuntivoDiagnosticos ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@diagnostico_definitivo", consulta.DiagnosticoConsultaDiNavigation.DefinitivoDiagnosticos ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@diagnostico_id_diagnosticos", consulta.DiagnosticoConsultaDiNavigation.IdDiagnosticosDiagnosticoD);

                    // Parámetro de salida para el ID de la consulta
                    SqlParameter outputIdParam = new SqlParameter("@consulta_id", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputIdParam);

                    connection.Open();
                    await command.ExecuteNonQueryAsync();

                    // Obtener el ID de la consulta generada
                    int consultaId = (int)outputIdParam.Value;
                    return consultaId;
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
                .FirstOrDefaultAsync(c => c.IdConsulta == idConsulta);
        }

        public async Task ActualizarConsultaAsync(Consultum consulta)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_ActualizarConsulta @id_consulta, @fechacreacion_consulta, @usuariocreacion_consulta, @historial_consulta, @secuencial_consulta, @paciente_consulta_p, @motivo_consulta, @enfermedad_consulta, @nombrepariente_consulta, @tipopariente_consulta, @telefono_consulta, @temperatura_consulta, @frecuenciarespiratoria_consulta, @presionarterialsistolica_consulta, @presionarterialdiastolica_consulta, @pulso_consulta, @peso_consulta, @talla_consulta, @plantratamiento_consulta, @observacion_consulta, @antecedentespersonales_consulta, @diasincapacidad_consulta, @medico_consulta_d, @especialidad_consulta_c, @estado_consulta_c, @tipo_consulta_c, @notasevolucion_consulta, @consultaprincipal_consulta, @medicamento_consulta_m, @documento_consulta_d, @detalle_consulta_d, @Cardiopatia, @Obser_Cardiopatia, @Diabetes, @Obser_Diabetes, @Enf_Cardiovascular, @Obser_Enf_Cardiovascular, @Hipertension, @Obser_Hipertensión, @Cancer, @Obser_Cancer, @Tuberculosis, @Obser_Tuberculosis, @Enf_Mental, @Obser_Enf_Mental, @Enf_Infecciosa, @Obser_Enf_Infecciosa, @Mal_Formacion, @Obser_Mal_Formacion, @Otro, @Obser_Otro, @Alergias, @Obser_Alergias, @Cirugias, @Obser_Cirugias, @Org_Sentidos, @Obser_Org_Sentidos, @Respiratorio, @Obser_Respiratorio, @Cardio_Vascular, @Obser_Cardio_Vascular, @Digestivo, @Obser_Digestivo, @Genital, @Obser_Genital, @Urinario, @Obser_Urinario, @M_Esqueletico, @Obser_M_Esqueletico, @Endocrino, @Obser_Endocrino, @Linfatico, @Obser_Linfatico, @Nervioso, @Obser_Nervioso, @Cabeza, @Obser_Cabeza, @Cuello, @Obser_Cuello, @Torax, @Obser_Torax, @Abdomen, @Obser_Abdomen, @Pelvis, @Obser_Pelvis, @Extremidades, @Obser_Extremidades, @imagen_consulta_i, @laboratorio_consulta_la, @diagnostico_consulta_di, @activo_consulta",
                new SqlParameter("@id_consulta", consulta.IdConsulta),
                new SqlParameter("@fechacreacion_consulta", consulta.FechacreacionConsulta),
                new SqlParameter("@usuariocreacion_consulta", consulta.UsuariocreacionConsulta),
                new SqlParameter("@historial_consulta", consulta.HistorialConsulta),
                new SqlParameter("@secuencial_consulta", consulta.SecuencialConsulta),
                new SqlParameter("@paciente_consulta_p", consulta.PacienteConsultaP),
                new SqlParameter("@motivo_consulta", consulta.MotivoConsulta),
                new SqlParameter("@enfermedad_consulta", consulta.EnfermedadConsulta),
                new SqlParameter("@nombrepariente_consulta", consulta.NombreparienteConsulta),
                new SqlParameter("@tipopariente_consulta", consulta.TipoparienteConsulta),
                new SqlParameter("@telefono_consulta", consulta.TelefonoConsulta),
                new SqlParameter("@temperatura_consulta", consulta.TemperaturaConsulta),
                new SqlParameter("@frecuenciarespiratoria_consulta", consulta.FrecuenciarespiratoriaConsulta),
                new SqlParameter("@presionarterialsistolica_consulta", consulta.PresionarterialsistolicaConsulta),
                new SqlParameter("@presionarterialdiastolica_consulta", consulta.PresionarterialdiastolicaConsulta),
                new SqlParameter("@pulso_consulta", consulta.PulsoConsulta),
                new SqlParameter("@peso_consulta", consulta.PesoConsulta),
                new SqlParameter("@talla_consulta", consulta.TallaConsulta),
                new SqlParameter("@plantratamiento_consulta", consulta.PlantratamientoConsulta),
                new SqlParameter("@observacion_consulta", consulta.ObservacionConsulta),
                new SqlParameter("@antecedentespersonales_consulta", consulta.AntecedentespersonalesConsulta),
                new SqlParameter("@diasincapacidad_consulta", consulta.DiasincapacidadConsulta),
                new SqlParameter("@medico_consulta_d", consulta.MedicoConsultaD),
                new SqlParameter("@especialidad_consulta_c", consulta.EspecialidadConsultaC),
                new SqlParameter("@estado_consulta_c", consulta.EstadoConsultaC),
                new SqlParameter("@tipo_consulta_c", consulta.TipoConsultaC),
                new SqlParameter("@notasevolucion_consulta", consulta.NotasevolucionConsulta),
                new SqlParameter("@consultaprincipal_consulta", consulta.ConsultaprincipalConsulta),
                new SqlParameter("@medicamento_consulta_m", consulta.MedicamentoConsultaM),
                new SqlParameter("@documento_consulta_d", consulta.DocumentoConsultaD),
                new SqlParameter("@detalle_consulta_d", consulta.DetalleConsultaD),
                new SqlParameter("@Cardiopatia", consulta.Cardiopatia),
                new SqlParameter("@Obser_Cardiopatia", consulta.ObserCardiopatia),
                new SqlParameter("@Diabetes", consulta.Diabetes),
                new SqlParameter("@Obser_Diabetes", consulta.ObserDiabetes),
                new SqlParameter("@Enf_Cardiovascular", consulta.EnfCardiovascular),
                new SqlParameter("@Obser_Enf_Cardiovascular", consulta.ObserEnfCardiovascular),
                new SqlParameter("@Hipertension", consulta.Hipertension),
                new SqlParameter("@Obser_Hipertensión", consulta.ObserHipertensión),
                new SqlParameter("@Cancer", consulta.Cancer),
                new SqlParameter("@Obser_Cancer", consulta.ObserCancer),
                new SqlParameter("@Tuberculosis", consulta.Tuberculosis),
                new SqlParameter("@Obser_Tuberculosis", consulta.ObserTuberculosis),
                new SqlParameter("@Enf_Mental", consulta.EnfMental),
                new SqlParameter("@Obser_Enf_Mental", consulta.ObserEnfMental),
                new SqlParameter("@Enf_Infecciosa", consulta.EnfInfecciosa),
                new SqlParameter("@Obser_Enf_Infecciosa", consulta.ObserEnfInfecciosa),
                new SqlParameter("@Mal_Formacion", consulta.MalFormacion),
                new SqlParameter("@Obser_Mal_Formacion", consulta.ObserMalFormacion),
                new SqlParameter("@Otro", consulta.Otro),
                new SqlParameter("@Obser_Otro", consulta.ObserOtro),
                new SqlParameter("@Alergias", consulta.Alergias),
                new SqlParameter("@Obser_Alergias", consulta.ObserAlergias),
                new SqlParameter("@Cirugias", consulta.Cirugias),
                new SqlParameter("@Obser_Cirugias", consulta.ObserCirugias),
                new SqlParameter("@Org_Sentidos", consulta.OrgSentidos),
                new SqlParameter("@Obser_Org_Sentidos", consulta.ObserOrgSentidos),
                new SqlParameter("@Respiratorio", consulta.Respiratorio),
                new SqlParameter("@Obser_Respiratorio", consulta.ObserRespiratorio),
                new SqlParameter("@Cardio_Vascular", consulta.CardioVascular),
                new SqlParameter("@Obser_Cardio_Vascular", consulta.ObserCardioVascular),
                new SqlParameter("@Digestivo", consulta.Digestivo),
                new SqlParameter("@Obser_Digestivo", consulta.ObserDigestivo),
                new SqlParameter("@Genital", consulta.Genital),
                new SqlParameter("@Obser_Genital", consulta.ObserGenital),
                new SqlParameter("@Urinario", consulta.Urinario),
                new SqlParameter("@Obser_Urinario", consulta.ObserUrinario),
                new SqlParameter("@M_Esqueletico", consulta.MEsqueletico),
                new SqlParameter("@Obser_M_Esqueletico", consulta.ObserMEsqueletico),
                new SqlParameter("@Endocrino", consulta.Endocrino),
                new SqlParameter("@Obser_Endocrino", consulta.ObserEndocrino),
                new SqlParameter("@Linfatico", consulta.Linfatico),
                new SqlParameter("@Obser_Linfatico", consulta.ObserLinfatico),
                new SqlParameter("@Nervioso", consulta.Nervioso),
                new SqlParameter("@Obser_Nervioso", consulta.ObserNervioso),
                new SqlParameter("@Cabeza", consulta.Cabeza),
                new SqlParameter("@Obser_Cabeza", consulta.ObserCabeza),
                new SqlParameter("@Cuello", consulta.Cuello),
                new SqlParameter("@Obser_Cuello", consulta.ObserCuello),
                new SqlParameter("@Torax", consulta.Torax),
                new SqlParameter("@Obser_Torax", consulta.ObserTorax),
                new SqlParameter("@Abdomen", consulta.Abdomen),
                new SqlParameter("@Obser_Abdomen", consulta.ObserAbdomen),
                new SqlParameter("@Pelvis", consulta.Pelvis),
                new SqlParameter("@Obser_Pelvis", consulta.ObserPelvis),
                new SqlParameter("@Extremidades", consulta.Extremidades),
                new SqlParameter("@Obser_Extremidades", consulta.ObserExtremidades),
                new SqlParameter("@imagen_consulta_i", consulta.ImagenConsultaI),
                new SqlParameter("@laboratorio_consulta_la", consulta.LaboratorioConsultaLa),
                new SqlParameter("@diagnostico_consulta_di", consulta.DiagnosticoConsultaDi),
                new SqlParameter("@activo_consulta", consulta.ActivoConsulta)
            );
        }


        //public async Task<List<Consultum>> GetAllConsultasAsync()
        //{
        //    // Obtener el nombre de usuario de la sesión
        //    var loginUsuario = _httpContextAccessor.HttpContext.Session.GetString("UsuarioNombre");

        //    if (string.IsNullOrEmpty(loginUsuario))
        //    {
        //        throw new Exception("El nombre de usuario no está disponible en la sesión.");
        //    }

        //    // Filtrar los pacientes por el usuario de creación
        //    var consultas = await _context.Consulta
        //        .Where(p => p.activiUsuario == loginUsuario)
        //        .Include(p => p.NacionalidadPacientesLNavigation)
        //        .ToListAsync();

        //    return consultas;
        //}
    }
}
