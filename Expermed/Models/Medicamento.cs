namespace Expermed.Models
{
    public partial class Medicamento
    {
        public int IdMedicamentos { get; set; }
        public DateTime? FechacreacionMedicamentos { get; set; }
        public string? UsuariocreacionMedicamentos { get; set; }
        public DateTime? FechamodificacionMedicamentos { get; set; }
        public string? UsuariomodificacionMedicamentos { get; set; }
        public int? ActivoMedicamentos { get; set; }
        public string? CategoriaMedicamentos { get; set; }
        public string? DescripcionMedicamentos { get; set; }
        public string? ConcentracionMedicamentos { get; set; }
    }
}
