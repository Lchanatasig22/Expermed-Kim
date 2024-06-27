namespace Expermed.Models
{
    public partial class Localidad
    {
        public Localidad()
        {
            PacienteNacionalidadPacientesLNavigations = new HashSet<Paciente>();
            PacienteProvinciaPacientesLNavigations = new HashSet<Paciente>();
        }

        public int IdLocalidad { get; set; }
        public DateTime? FechacreacionLocalidad { get; set; }
        public string? UsuariocreacionLocalidad { get; set; }
        public DateTime? FechamodificacionLocalidad { get; set; }
        public string? UsuariomodificacionLocalidad { get; set; }
        public int? ActivoLocalidad { get; set; }
        public string? NombreLocalidad { get; set; }
        public string? GentilicioLocalidad { get; set; }
        public string? PrefijoLocalidad { get; set; }
        public string? CodigoLocalidad { get; set; }
        public string? IsoLocalidad { get; set; }
        public string? IsoadLocalidad { get; set; }
        public string? CiaLocalidad { get; set; }

        public virtual ICollection<Paciente> PacienteNacionalidadPacientesLNavigations { get; set; }
        public virtual ICollection<Paciente> PacienteProvinciaPacientesLNavigations { get; set; }
    }
}
