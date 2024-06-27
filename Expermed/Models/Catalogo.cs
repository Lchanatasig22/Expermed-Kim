namespace Expermed.Models
{
    public partial class Catalogo
    {
        public Catalogo()
        {
            PacienteEstadocivilPacientesCNavigations = new HashSet<Paciente>();
            PacienteFormacionprofesionalPacientesCNavigations = new HashSet<Paciente>();
            PacienteSexoPacientesCNavigations = new HashSet<Paciente>();
            PacienteTipodocumentoPacientesCNavigations = new HashSet<Paciente>();
            PacienteTiposangrePacientesCNavigations = new HashSet<Paciente>();
        }

        public int IdCatalogo { get; set; }
        public DateTime? FechacreacionCatalogo { get; set; }
        public string? UsuariocreacionCatalogo { get; set; }
        public DateTime? FechamodificacionCatalogo { get; set; }
        public string? UsuariomodificacionCatalogo { get; set; }
        public int? ActivoCatalogo { get; set; }
        public string? DescripcionCatalogo { get; set; }
        public string? CategoriaCatalogo { get; set; }
        public int UuidCatalogo { get; set; }

        public virtual ICollection<Paciente> PacienteEstadocivilPacientesCNavigations { get; set; }
        public virtual ICollection<Paciente> PacienteFormacionprofesionalPacientesCNavigations { get; set; }
        public virtual ICollection<Paciente> PacienteSexoPacientesCNavigations { get; set; }
        public virtual ICollection<Paciente> PacienteTipodocumentoPacientesCNavigations { get; set; }
        public virtual ICollection<Paciente> PacienteTiposangrePacientesCNavigations { get; set; }
    }
}
