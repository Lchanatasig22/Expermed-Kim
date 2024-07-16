namespace Expermed.Models
{
    public class Establecimientos
    {
        public string PuntoEmision { get; set; }
        public string Codigo { get; set; }
        public string Direccion { get; set; }
    }

    public class Emisor
    {
        public string Ruc { get; set; }
        public bool ObligadoContabilidad { get; set; }
        public string ContribuyenteEspecial { get; set; }
        public string NombreComercial { get; set; }
        public string RazonSocial { get; set; }
        public string Direccion { get; set; }
        public Establecimiento Establecimiento { get; set; }
    }

    public class InformacionAdicional
    {
        public string Referencia { get; set; }
    }

    public class Impuesto
    {
        public double BaseImponible { get; set; }
        public double Valor { get; set; }
        public string Codigo { get; set; }
        public string CodigoPorcentaje { get; set; }

        public Double Tarifa { get; set; }

    }

    public class Totales
    {
        public double TotalSinImpuestos { get; set; }
        public List<Impuesto> Impuestos { get; set; }
        public double ImporteTotal { get; set; }
        public double Propina { get; set; }
        public double Descuento { get; set; }
    }

    public class Comprador
    {
        public string Email { get; set; }
        public int? Identificacion { get; set; }
        public int TipoIdentificacion { get; set; }
        public string RazonSocial { get; set; }
        public string Direccion { get; set; }
        public int? Telefono { get; set; }
    }

    public class Item
    {
        public int Cantidad { get; set; }
        public string CodigoPrincipal { get; set; }
        public string CodigoAuxiliar { get; set; }
        public double PrecioUnitario { get; set; }
        public double Descuento { get; set; }
        public string Descripcion { get; set; }
        public double PrecioTotalSinImpuestos { get; set; }
        public List<Impuesto> Impuestos { get; set; }
        public string UnidadMedida { get; set; }
    }

    public class Pago
    {
        public string Medio { get; set; }
        public double Total { get; set; }
        public string Notas { get; set; }
    }

    public class Documento
    {
        public int Ambiente { get; set; }
        public int TipoEmision { get; set; }
        public int Secuencial { get; set; }
        public DateTime FechaEmision { get; set; }
        public Emisor Emisor { get; set; }
        public string Moneda { get; set; }
        public InformacionAdicional InformacionAdicional { get; set; }
        public Totales Totales { get; set; }
        public Comprador Comprador { get; set; }
        public List<Item> Items { get; set; }
        public List<Pago> Pagos { get; set; }
    }
}
