namespace Expermed.Models
{
    public class FormularioPago
    {
        public int ConsultaId { get; set; }
        public DateTime FechaEmision { get; set; }
        public double TotalSinImpuestos { get; set; }
        public double ImporteTotal { get; set; }
        public int TipoPago { get; set; }
        public List<Impuesto> Impuestos { get; set; }
        public List<Pago> Pagos { get; set; }
    }

}
