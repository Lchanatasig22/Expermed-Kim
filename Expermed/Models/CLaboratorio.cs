using System;
using System.Collections.Generic;

namespace Expermed.Models
{
    public partial class CLaboratorio
    {
        public CLaboratorio()
        {
            Consulta = new HashSet<Consultum>();
        }

        public int IdLaboratorio { get; set; }
        public DateTime? FechacreacionLaboratorio { get; set; }
        public string? UsuariocreacionLaboratorio { get; set; }
        public int? ConsultaLaboratorioC { get; set; }
        public int? CantidadLaboratorio { get; set; }
        public string? ObservacionLaboratorio { get; set; }
        public int? IdLaboratoriosLaboratorioL { get; set; }

        public virtual Laboratorio? IdLaboratoriosLaboratorioLNavigation { get; set; }
        public virtual ICollection<Consultum> Consulta { get; set; }
    }
}
