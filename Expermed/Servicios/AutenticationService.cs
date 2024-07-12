using Expermed.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Expermed.Servicios
{
    public class AutenticationService
    {
        private readonly Base_ExpermedContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AutenticationService(Base_ExpermedContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Usuario> ValidateUser(string loginUsuario, string claveUsuario)
        {
            var parameterLoginUsuario = new SqlParameter("@login_usuario", loginUsuario);
            var parameterClaveUsuario = new SqlParameter("@clave_usuario", claveUsuario);

            var user = new Usuario();
            using (var connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("[dbo].[sp_ValidarCredenciales]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(parameterLoginUsuario);
                    command.Parameters.Add(parameterClaveUsuario);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            // Obtener el ID del usuario
                            if (!reader.IsDBNull(reader.GetOrdinal("id_usuario")))
                            {
                                user.IdUsuario = reader.GetInt32(reader.GetOrdinal("id_usuario"));
                            }

                            // Obtener el valor del perfil del usuario
                            if (!reader.IsDBNull(reader.GetOrdinal("perfil_usuario_p")))
                            {
                                user.PerfilUsuarioP = reader.GetInt32(reader.GetOrdinal("perfil_usuario_p"));
                            }

                            // Obtener el nombre de usuario
                            if (!reader.IsDBNull(reader.GetOrdinal("login_usuario")))
                            {
                                user.LoginUsuario = reader.GetString(reader.GetOrdinal("login_usuario"));
                            }

                            // Obtener la descripción del usuario
                            if (!reader.IsDBNull(reader.GetOrdinal("descripcion_usuario")))
                            {
                                user.DescripcionUsuario = reader.GetString(reader.GetOrdinal("descripcion_usuario"));
                            }

                            // Almacenar el nombre de usuario y el ID en la sesión
                            _httpContextAccessor.HttpContext.Session.SetString("UsuarioNombre", user.LoginUsuario);
                            _httpContextAccessor.HttpContext.Session.SetInt32("UsuarioId", user.IdUsuario);

                            // Check if DescripcionUsuario is not null before setting it in the session
                            if (!string.IsNullOrEmpty(user.DescripcionUsuario))
                            {
                                _httpContextAccessor.HttpContext.Session.SetString("UsuarioDescripcion", user.DescripcionUsuario);
                            }
                            else
                            {
                                // Optionally, handle the case where DescripcionUsuario is null or empty
                                _httpContextAccessor.HttpContext.Session.SetString("UsuarioDescripcion", "Default Description");
                            }
                        }
                    }
                }
            }

            return user;
        }
    }
}
