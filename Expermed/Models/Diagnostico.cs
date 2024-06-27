namespace Expermed.Models
{
    public partial class Diagnostico
    {
        public int IdDiagnostico { get; set; }
        public DateTime? FechacreacionDiagnostico { get; set; }
        public string? UsuariocreacionDiagnostico { get; set; }
        public DateTime? FechamodificacionDiagnostico { get; set; }
        public string? UsuariomodificacionDiagnostico { get; set; }
        public int? ActivoDiagnostico { get; set; }
        public string? CategoriaDiagnostico { get; set; }
        public string? DescripcionDiagnostico { get; set; }
        public string? CodigoDiagnostico { get; set; }
    }
}
