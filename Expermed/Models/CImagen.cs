using System;
using System.Collections.Generic;

namespace Expermed.Models
{
    public partial class CImagen
    {
        public CImagen()
        {
            Consulta = new HashSet<Consultum>();
        }

        public int IdImagen { get; set; }
        public DateTime? FechacreacionImagen { get; set; }
        public string? UsuariocreacionImagen { get; set; }
        public int? ConsultaImagenC { get; set; }
        public int? CantidadImagen { get; set; }
        public string? ObservacionImagen { get; set; }
        public int? IdImagenesImagenI { get; set; }

        public virtual Imagene? IdImagenesImagenINavigation { get; set; }
        public virtual ICollection<Consultum> Consulta { get; set; }
    }
}
