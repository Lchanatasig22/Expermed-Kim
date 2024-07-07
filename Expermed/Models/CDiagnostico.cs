using System;
using System.Collections.Generic;

namespace Expermed.Models
{
    public partial class CDiagnostico
    {
        public CDiagnostico()
        {
            Consulta = new HashSet<Consultum>();
            CantidadDiagnostico = 0;
        }

        public int IdDiagnostico { get; set; }
        public DateTime? FechacreacionDiagnostico { get; set; }
        public string? UsuariocreacionDiagnostico { get; set; }
        public int? ConsultaDiagnosticoC { get; set; }
        public int? CantidadDiagnostico { get; set; }
        public string? ObservacionDiagnostico { get; set; }
        public string? PresuntivoDiagnosticos { get; set; }
        public string? DefinitivoDiagnosticos { get; set; }
        public int? IdDiagnosticosDiagnosticoD { get; set; }

        public virtual Diagnostico? IdDiagnosticosDiagnosticoDNavigation { get; set; }
        public virtual ICollection<Consultum> Consulta { get; set; }
    }
}
