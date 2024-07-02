using System;
using System.Collections.Generic;

namespace Expermed.Models
{
    public partial class Imagene
    {
        public Imagene()
        {
            CImagens = new HashSet<CImagen>();
        }

        public int IdImagenes { get; set; }
        public DateTime? FechacreacionImagenes { get; set; }
        public string? UsuariocreacionImagenes { get; set; }
        public DateTime? FechamodificacionImagenes { get; set; }
        public string? UsuariomodificacionImagenes { get; set; }
        public int? ActivoImagenes { get; set; }
        public string? CategoriaImagenes { get; set; }
        public string? DescripcionImagenes { get; set; }
        public string? CodigoImagenes { get; set; }

        public virtual ICollection<CImagen> CImagens { get; set; }
    }
}
