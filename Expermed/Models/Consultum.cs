namespace Expermed.Models
{
    public partial class Consultum
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
        public int? MedicamentoConsultaM { get; set; }
        public int? DocumentoConsultaD { get; set; }
        public int? DetalleConsultaD { get; set; }

        public virtual CDetalle? DetalleConsultaDNavigation { get; set; }
        public virtual CDocumento? DocumentoConsultaDNavigation { get; set; }
        public virtual CMedicamento? MedicamentoConsultaMNavigation { get; set; }
        public virtual Paciente? PacienteConsultaPNavigation { get; set; }
    }
}
