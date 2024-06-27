namespace Expermed.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Cita = new HashSet<Cita>();
        }

        public int IdUsuario { get; set; }
        public int? CiUsuario { get; set; }
        public string? NombresUsuario { get; set; }
        public string? ApellidosUsuario { get; set; }
        public string? TelefonoUsuario { get; set; }
        public string? EmailUsuario { get; set; }
        public string? EstablecimientoUsuario { get; set; }
        public string? DirecccionestableUsuario { get; set; }
        public string? CiudadUsuario { get; set; }
        public string? ProvinciaUsuario { get; set; }
        public DateTime? FechacreacionUsuario { get; set; }
        public DateTime? FechamodificacionUsuario { get; set; }
        public string? LoginUsuario { get; set; }
        public string? ClaveUsuario { get; set; }
        public int? ActivoUsuario { get; set; }
        public int? PerfilUsuarioP { get; set; }
        public string? DescripcionUsuario { get; set; }
        public string? CodigoUsuario { get; set; }

        public virtual Perfil? PerfilUsuarioPNavigation { get; set; }
        public virtual ICollection<Cita> Cita { get; set; }
    }
}
