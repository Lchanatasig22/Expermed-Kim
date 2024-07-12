using Expermed.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
namespace Expermed.Servicios
{
    public class UserService
    {
        private readonly Base_ExpermedContext _context;

        public UserService(Base_ExpermedContext context)
        {
            _context = context;
        }
     

        /// <summary>
        /// Método para listar todos los usuarios con propiedades relacionadas
        /// </summary>
        /// <returns></returns>
        public async Task<List<Usuario>> GetAllUsuariosAsync()
        {
            var usuarios = await _context.Usuarios
      .Include(u => u.PerfilUsuarioPNavigation)
      .ToListAsync();
            return usuarios;
        }

        /// <summary>
        /// creacion de usuarios usando sp
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public async Task CreateUsuarioAsync(Usuario usuario)
        {
            var ciUsuarioParam = new SqlParameter("@ci_usuario", usuario.CiUsuario);
            var nombresUsuarioParam = new SqlParameter("@nombres_usuario", usuario.NombresUsuario);
            var apellidosUsuarioParam = new SqlParameter("@apellidos_usuario", usuario.ApellidosUsuario);
            var telefonoUsuarioParam = new SqlParameter("@telefono_usuario", usuario.TelefonoUsuario);
            var emailUsuarioParam = new SqlParameter("@email_usuario", usuario.EmailUsuario);
            var establecimientoUsuarioParam = new SqlParameter("@establecimiento_usuario", usuario.EstablecimientoUsuario);
            var direccionEstablecimientoUsuarioParam = new SqlParameter("@direccionestable_usuario", usuario.DirecccionestableUsuario);
            var ciudadUsuarioParam = new SqlParameter("@ciudad_usuario", usuario.CiudadUsuario);
            var provinciaUsuarioParam = new SqlParameter("@provincia_usuario", usuario.ProvinciaUsuario);
            var fechaCreacionUsuarioParam = new SqlParameter("@fechacreacion_usuario", DateTime.Now);
            var loginUsuarioParam = new SqlParameter("@login_usuario", usuario.LoginUsuario);
            var claveUsuarioParam = new SqlParameter("@clave_usuario", usuario.ClaveUsuario);
            var activoUsuarioParam = new SqlParameter("@activo_usuario", usuario.ActivoUsuario);
            var perfilUsuarioPParam = new SqlParameter("@perfil_usuario_p", usuario.PerfilUsuarioP);
            var codigoUsuarioParam = new SqlParameter("@codigo_usuario", usuario.CodigoUsuario);

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_CreateUsuario @ci_usuario, @nombres_usuario, @apellidos_usuario, @telefono_usuario, @email_usuario, " +
                "@establecimiento_usuario, @direccionestable_usuario, @ciudad_usuario, @provincia_usuario, @fechacreacion_usuario, " +
                "@login_usuario, @clave_usuario, @activo_usuario, @perfil_usuario_p, @codigo_usuario",
                ciUsuarioParam, nombresUsuarioParam, apellidosUsuarioParam, telefonoUsuarioParam, emailUsuarioParam,
                establecimientoUsuarioParam, direccionEstablecimientoUsuarioParam, ciudadUsuarioParam, provinciaUsuarioParam,
                fechaCreacionUsuarioParam, loginUsuarioParam, claveUsuarioParam, activoUsuarioParam, perfilUsuarioPParam,
                codigoUsuarioParam);
        }

        /// <summary>
        /// Trae los usuarios especificos dependiendo del id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Usuario> GetUsuarioByIdAsync(int id)
        {
            return await _context.Usuarios
                .Include(u => u.PerfilUsuarioPNavigation)
                .FirstOrDefaultAsync(u => u.IdUsuario == id);
        }

        /// <summary>
        /// Actualizacion de usuario 
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public async Task UpdateUsuarioAsync(Usuario usuario)
        {
            var idUsuarioParam = new SqlParameter("@id_usuario", usuario.IdUsuario);
            var ciUsuarioParam = new SqlParameter("@ci_usuario", usuario.CiUsuario);
            var nombresUsuarioParam = new SqlParameter("@nombres_usuario", usuario.NombresUsuario);
            var apellidosUsuarioParam = new SqlParameter("@apellidos_usuario", usuario.ApellidosUsuario);
            var telefonoUsuarioParam = new SqlParameter("@telefono_usuario", usuario.TelefonoUsuario);
            var emailUsuarioParam = new SqlParameter("@email_usuario", usuario.EmailUsuario);
            var establecimientoUsuarioParam = new SqlParameter("@establecimiento_usuario", usuario.EstablecimientoUsuario);
            var direccionEstablecimientoUsuarioParam = new SqlParameter("@direccionestable_usuario", usuario.DirecccionestableUsuario);
            var ciudadUsuarioParam = new SqlParameter("@ciudad_usuario", usuario.CiudadUsuario);
            var provinciaUsuarioParam = new SqlParameter("@provincia_usuario", usuario.ProvinciaUsuario);
            var fechaModificacionUsuarioParam = new SqlParameter("@fechamodificacion_usuario", DateTime.Now);
            var loginUsuarioParam = new SqlParameter("@login_usuario", usuario.LoginUsuario);
            var claveUsuarioParam = new SqlParameter("@clave_usuario", usuario.ClaveUsuario);
            var activoUsuarioParam = new SqlParameter("@activo_usuario", usuario.ActivoUsuario);
            var perfilUsuarioPParam = new SqlParameter("@perfil_usuario_p", usuario.PerfilUsuarioP);
            var codigoUsuarioParam = new SqlParameter("@codigo_usuario", usuario.CodigoUsuario);

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_UpdateUsuario @id_usuario, @ci_usuario, @nombres_usuario, @apellidos_usuario, @telefono_usuario, @email_usuario, " +
                "@establecimiento_usuario, @direccionestable_usuario, @ciudad_usuario, @provincia_usuario, @fechamodificacion_usuario, " +
                "@login_usuario, @clave_usuario, @activo_usuario, @perfil_usuario_p, @codigo_usuario",
                idUsuarioParam, ciUsuarioParam, nombresUsuarioParam, apellidosUsuarioParam, telefonoUsuarioParam, emailUsuarioParam,
                establecimientoUsuarioParam, direccionEstablecimientoUsuarioParam, ciudadUsuarioParam, provinciaUsuarioParam,
                fechaModificacionUsuarioParam, loginUsuarioParam, claveUsuarioParam, activoUsuarioParam, perfilUsuarioPParam,
                codigoUsuarioParam);
        }

        /// <summary>
        /// Cambio del estado del usuario este mismo se puede usar para los formularios que requieran un cambio en el estado
        /// </summary>
        /// <param name="id"></param>
        /// <param name="activo"></param>
        /// <returns></returns>
        public async Task CambiarEstadoUsuarioAsync(int id, bool activo)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                usuario.ActivoUsuario = activo ? 1 : 0;
                await _context.SaveChangesAsync();
            }
        }





    }
}
