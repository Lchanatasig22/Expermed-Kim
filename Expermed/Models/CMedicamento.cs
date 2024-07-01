using System;
using System.Collections.Generic;

namespace Expermed.Models
{
    public partial class CMedicamento
    {
        public CMedicamento()
        {
            Consulta = new HashSet<Consultum>();
        }

        public int IdMedicamento { get; set; }
        public DateTime? FechacreacionMedicamento { get; set; }
        public string? UsuariocreacionMedicamento { get; set; }
        public int? ConsultaMedicamentoC { get; set; }
        public int? CantidadMedicamentoC { get; set; }
        public string? ObservacionMedicamento { get; set; }
        public int? IdMedicamentosMedicamentoM { get; set; }

        public virtual Medicamento IdMedicamentoNavigation { get; set; } = null!;
        public virtual ICollection<Consultum> Consulta { get; set; }
    }
}
