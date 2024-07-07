using System;
using System.Collections.Generic;

namespace Expermed.Models
{
    public partial class Diagnostico
    {
        public Diagnostico()
        {
            CDiagnosticos = new HashSet<CDiagnostico>();
        
        }

        public int IdDiagnosticos { get; set; }
        public DateTime? FechacreacionDiagnosticos { get; set; }
        public string? UsuariocreacionDiagnosticos { get; set; }
        public DateTime? FechamodificacionDiagnosticos { get; set; }
        public string? UsuariomodificacionDiagnosticos { get; set; }
        public int? ActivoDiagnosticso { get; set; }
        public string? CategoriaDiagnosticos { get; set; }
        public string? DescripcionDiagnosticos { get; set; }
        public string? CodigoDiagnosticos { get; set; }

        public virtual ICollection<CDiagnostico> CDiagnosticos { get; set; }
    }
}
