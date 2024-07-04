using Expermed.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Expermed.Servicios
{ /// <summary>
/// Servicio para la autenticacion del usuario
/// </summary>
    public class AutenticationService
    {/// <summary>
     /// llamamod al db context que tiene los mapeos de la base de datos
     /// </summary>
        private readonly Base_ExpermedContext _context;
        /// <summary>
        /// mediante esto nos deja  acceder a la captura de los datos
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Controlador tanto para el uso de ambos servicios
        /// </summary>
        /// <param name="context"></param>
        /// <param name="httpContextAccessor"></param>
        public AutenticationService(Base_ExpermedContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        /// <summary>
        /// Servicio que permite la validacion de usuarios en base a dos variables, el loginname que sustituye al email y la clave
        /// </summary>
        /// <param name="loginUsuario"></param>
        /// <param name="claveUsuario"></param>
        /// <returns></returns>
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

                            // Almacenar el nombre de usuario y el ID en la sesión
                            _httpContextAccessor.HttpContext.Session.SetString("UsuarioNombre", user.LoginUsuario);

                            _httpContextAccessor.HttpContext.Session.SetInt32("UsuarioId", user.IdUsuario);
                        }
                    }
                }
            }

            return user;
        }


    }
}
