namespace Expermed.Models
{
    public partial class CDetalle
    {
        public CDetalle()
        {
            Consulta = new HashSet<Consultum>();
        }

        public int IdDetalle { get; set; }
        public DateTime? FechacreacionDetalle { get; set; }
        public string? UsuariocreacionDetalle { get; set; }
        public int? TipoDetalle { get; set; }
        public int? CantidadDetalle { get; set; }
        public string? ObservacionDetalle { get; set; }

        public virtual ICollection<Consultum> Consulta { get; set; }
    }
}
