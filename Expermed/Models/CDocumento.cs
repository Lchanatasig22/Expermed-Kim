using System;
using System.Collections.Generic;

namespace Expermed.Models
{
    public partial class CDocumento
    {
        public CDocumento()
        {
            Consulta = new HashSet<Consultum>();
        }

        public int IdDocumento { get; set; }
        public DateTime? FechacreacionDocumento { get; set; }
        public string? UsuariocreacionDocumento { get; set; }
        public int? MedicoDocumento { get; set; }
        public string? TipodocumentoDocumento { get; set; }
        public string? SecuencialDocumento { get; set; }
        public string? SignoalarmaDocumento { get; set; }
        public string? RecomendacionDocumento { get; set; }

        public virtual ICollection<Consultum> Consulta { get; set; }
    }
}
