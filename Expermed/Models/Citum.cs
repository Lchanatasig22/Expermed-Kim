using System;
using System.Collections.Generic;

namespace Expermed.Models
{
    public partial class Citum
    {
        public int IdCitas { get; set; }
        public DateTime? FechacreacionCitas { get; set; }
        public string? UsuariocreacionCitas { get; set; }
        public DateTime? FechadelacitaCitas { get; set; }
        public TimeSpan? HoradelacitaCitas { get; set; }
        public int? MedicoCitasU { get; set; }
        public int? PacienteCitasP { get; set; }
        public string? Estado { get; set; }

        public virtual Usuario? MedicoCitasUNavigation { get; set; }
        public virtual Paciente? PacienteCitasPNavigation { get; set; }
    }
}
