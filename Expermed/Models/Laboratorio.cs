using System;
using System.Collections.Generic;

namespace Expermed.Models
{
    public partial class Laboratorio
    {
        public Laboratorio()
        {
            CLaboratorios = new HashSet<CLaboratorio>();
        }

        public int IdLaboratorios { get; set; }
        public DateTime? FechacreacionLaboratorios { get; set; }
        public string? UsuariocreacionLaboratorios { get; set; }
        public DateTime? FechamodificacionLaboratorios { get; set; }
        public string? UsuariomodificacionLaboratorios { get; set; }
        public int? ActivoLaboratorios { get; set; }
        public string? CategoriaLaboratorios { get; set; }
        public string? DescripcionLaboratorios { get; set; }
        public string? CodigoLaboratorios { get; set; }

        public virtual ICollection<CLaboratorio> CLaboratorios { get; set; }
    }
}
