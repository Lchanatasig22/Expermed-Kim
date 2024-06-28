using System;
using System.Collections.Generic;

namespace Expermed.Models
{
    public partial class Paciente
    {
        public Paciente()
        {
            Cita = new HashSet<Citum>();
            Consulta = new HashSet<Consultum>();
        }

        public int IdPacientes { get; set; }
        public DateTime? FechacreacionPacientes { get; set; }
        public string? UsuariocreacionPacientes { get; set; }
        public DateTime? FechamodificacionPacientes { get; set; }
        public string? UsuariomodificacionPacientes { get; set; }
        public int? ActivoPacientes { get; set; }
        public int? TipodocumentoPacientesC { get; set; }
        public int? CiPacientes { get; set; }
        public string? PrimernombrePacientes { get; set; }
        public string? SegundonombrePacientes { get; set; }
        public string? PrimerapellidoPacientes { get; set; }
        public string? SegundoapellidoPacientes { get; set; }
        public int? SexoPacientesC { get; set; }
        public DateTime? FechanacimientoPacientes { get; set; }
        public int? Edad { get; set; }
        public int? TiposangrePacientesC { get; set; }
        public string? DonantePacientes { get; set; }
        public int? EstadocivilPacientesC { get; set; }
        public int? FormacionprofesionalPacientesC { get; set; }
        public int? TelefonofijoPacientes { get; set; }
        public int? TelefonocelularPacientes { get; set; }
        public string? EmailPacientes { get; set; }
        public int? NacionalidadPacientesL { get; set; }
        public int? ProvinciaPacientesL { get; set; }
        public string? DireccionPacientes { get; set; }
        public string? OcupacionPacientes { get; set; }
        public string? EmpresaPacientes { get; set; }
        public string? SegurosaludPacientesC { get; set; }

        public virtual Catalogo? EstadocivilPacientesCNavigation { get; set; }
        public virtual Catalogo? FormacionprofesionalPacientesCNavigation { get; set; }
        public virtual Localidad? NacionalidadPacientesLNavigation { get; set; }
        public virtual Localidad? ProvinciaPacientesLNavigation { get; set; }
        public virtual Catalogo? SexoPacientesCNavigation { get; set; }
        public virtual Catalogo? TipodocumentoPacientesCNavigation { get; set; }
        public virtual Catalogo? TiposangrePacientesCNavigation { get; set; }
        public virtual ICollection<Citum> Cita { get; set; }
        public virtual ICollection<Consultum> Consulta { get; set; }
    }
}
