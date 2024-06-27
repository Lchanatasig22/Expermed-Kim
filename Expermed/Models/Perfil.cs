namespace Expermed.Models
{
    public partial class Perfil
    {
        public Perfil()
        {
            Usuarios = new HashSet<Usuario>();
        }

        public int IdPerfil { get; set; }
        public string? DescripcionPerfil { get; set; }
        public DateTime? FechacreacionPerfil { get; set; }
        public string? EspecialidadPerfil { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
