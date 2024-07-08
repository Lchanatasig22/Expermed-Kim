namespace Expermed.Models
{
    public class VerConsultaViewModel
    {
        public int IdConsulta { get; set; }
        public DateTime? FechacreacionConsulta { get; set; }
        public string? UsuariocreacionConsulta { get; set; }
        public string? HistorialConsulta { get; set; }
        public string? SecuencialConsulta { get; set; }
        public int? PacienteConsultaP { get; set; }
        public string? MotivoConsulta { get; set; }
        public string? EnfermedadConsulta { get; set; }
        public string? NombreparienteConsulta { get; set; }
        public int? TipoparienteConsulta { get; set; }
        public int? TelefonoConsulta { get; set; }
        public string? TemperaturaConsulta { get; set; }
        public string? FrecuenciarespiratoriaConsulta { get; set; }
        public string? PresionarterialsistolicaConsulta { get; set; }
        public string? PresionarterialdiastolicaConsulta { get; set; }
        public string? PulsoConsulta { get; set; }
        public string? PesoConsulta { get; set; }
        public string? TallaConsulta { get; set; }
        public string? PlantratamientoConsulta { get; set; }
        public string? ObservacionConsulta { get; set; }
        public string? AntecedentespersonalesConsulta { get; set; }
        public int? DiasincapacidadConsulta { get; set; }
        public int? MedicoConsultaD { get; set; }
        public int? EspecialidadConsultaC { get; set; }
        public int? EstadoConsultaC { get; set; }
        public int? TipoConsultaC { get; set; }
        public string? NotasevolucionConsulta { get; set; }
        public string? ConsultaprincipalConsulta { get; set; }
        public int? DocumentoConsultaD { get; set; }
        public int? DetalleConsultaD { get; set; }
        public string? Cardiopatia { get; set; }
        public string? ObserCardiopatia { get; set; }
        public string? Diabetes { get; set; }
        public string? ObserDiabetes { get; set; }
        public string? EnfCardiovascular { get; set; }
        public string? ObserEnfCardiovascular { get; set; }
        public string? Hipertension { get; set; }
        public string? ObserHipertensión { get; set; }
        public string? Cancer { get; set; }
        public string? ObserCancer { get; set; }
        public string? Tuberculosis { get; set; }
        public string? ObserTuberculosis { get; set; }
        public string? EnfMental { get; set; }
        public string? ObserEnfMental { get; set; }
        public string? EnfInfecciosa { get; set; }
        public string? ObserEnfInfecciosa { get; set; }
        public string? MalFormacion { get; set; }
        public string? ObserMalFormacion { get; set; }
        public string? Otro { get; set; }
        public string? ObserOtro { get; set; }
        public string? Alergias { get; set; }
        public string? ObserAlergias { get; set; }
        public string? Cirugias { get; set; }
        public string? ObserCirugias { get; set; }
        public string? OrgSentidos { get; set; }
        public string? ObserOrgSentidos { get; set; }
        public string? Respiratorio { get; set; }
        public string? ObserRespiratorio { get; set; }
        public string? CardioVascular { get; set; }
        public string? ObserCardioVascular { get; set; }
        public string? Digestivo { get; set; }
        public string? ObserDigestivo { get; set; }
        public string? Genital { get; set; }
        public string? ObserGenital { get; set; }
        public string? Urinario { get; set; }
        public string? ObserUrinario { get; set; }
        public string? MEsqueletico { get; set; }
        public string? ObserMEsqueletico { get; set; }
        public string? Endocrino { get; set; }
        public string? ObserEndocrino { get; set; }
        public string? Linfatico { get; set; }
        public string? ObserLinfatico { get; set; }
        public string? Nervioso { get; set; }
        public string? ObserNervioso { get; set; }
        public string? Cabeza { get; set; }
        public string? ObserCabeza { get; set; }
        public string? Cuello { get; set; }
        public string? ObserCuello { get; set; }
        public string? Torax { get; set; }
        public string? ObserTorax { get; set; }
        public string? Abdomen { get; set; }
        public string? ObserAbdomen { get; set; }
        public string? Pelvis { get; set; }
        public string? ObserPelvis { get; set; }
        public string? Extremidades { get; set; }
        public string? ObserExtremidades { get; set; }
        public int? ImagenConsultaI { get; set; }
        public int? LaboratorioConsultaLa { get; set; }
        public int? DiagnosticoConsultaDi { get; set; }
        public int? ActivoConsulta { get; set; }
        public int? MedicamentoConsultaM { get; set; }

        public string PacienteNombre { get; set; }
        public string PacienteSegundoNombre { get; set; }
        public string PacienteApellido { get; set; }
        public string PacienteSegundoApellido { get; set; }
        public string DiagnosticoDescripcion { get; set; }
        public string TiposDocumentos { get; set; }
        public int NumeroDocumento { get; set; }

        public string TiposSangre { get; set; }
        public string TipoDonante { get; set; }
        public DateTime? FechaNacimiento { get; set; }  // Agregamos la propiedad como DateTime?

        public int EdadPaciente { get; set; }

        public string TipoSexo { get; set; }
        public string TipoEstadoC { get; set; }
        public string TipoFormacionP { get; set; }
        public string TipoNacionalidadP { get; set; }
        public string DireccionPa { get; set; }
        public int TelefonoFiPa { get; set; }
        public int TelefonoCePa { get; set; }

        public string EmailPac { get; set; }

        public string OcupaciónPac { get; set; }
        public string EmpresaPac { get; set; }
        public string SeguroSaludPa { get; set; }
        public string MedicoConsulta { get; set; }
        public string DiagnosticoConsulta { get; set; }

        public string TipoDiagnostico { get; set; }
        public string TipoDiagnosticod { get; set; }
        public string MedicamentoConsulta { get; set; }

        public int MedicamentoCantidad { get; set; }
        public string MedicamentoObservacion { get; set; }

        public string ImagenConsulta { get; set; }
        public string ImagenObservacion { get; set; }
        public int ImagenCantidad { get; set; }

        public string LaboratorioConsulta { get; set; }
        public string LaboratorioObservacion { get; set; }

        public int LaboratorioCantidad { get;set; }

    }

}
