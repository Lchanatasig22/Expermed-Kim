namespace Expermed.Models
{
    public partial class Establecimiento
    {
        public int IdEstablecimiento { get; set; }
        public DateTime? FechacreacionEstablecimiento { get; set; }
        public string? UsuariocreacionEstablecimiento { get; set; }
        public DateTime? FechamodificacionEstablecimiento { get; set; }
        public string? DescripcionEstablecimiento { get; set; }
        public int? ActivoEstablecimiento { get; set; }
        public string? DireccionEstablecimiento { get; set; }
        public string? CiudadEstablecimiento { get; set; }
        public string? ProvinciaEstablecimiento { get; set; }
    }
}
