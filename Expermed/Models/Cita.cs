namespace Expermed.Models
{
    public partial class Cita
    {
        public int IdCitas { get; set; }
        public DateTime? FechacreacionCitas { get; set; }
        public string? UsuariocreacionCitas { get; set; }
        public DateTime? FechadelacitaCitas { get; set; }

        public TimeSpan? HoradelacitaCitas { get; set; }

        public string Estado { get; set; } = "En Curso";
        public int? MedicoCitasU { get; set; }
        public int? PacienteCitasP { get; set; }

        public virtual Usuario? MedicoCitasUNavigation { get; set; }
        public virtual Paciente? PacienteCitasPNavigation { get; set; }

        public Cita()

        {
            FechacreacionCitas = DateTime.Now;
        }



    }
}
