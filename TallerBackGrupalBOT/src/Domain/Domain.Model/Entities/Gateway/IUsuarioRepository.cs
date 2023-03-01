using Domain.Model.Entities.Usuarios;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Model.Entities.Gateway
{
    /// <summary>
    /// Interfaz de repositorio de entidad <see cref="Usuario"/>
    /// </summary>
    public interface IUsuarioRepository
    {
        /// <summary>
        /// Método para obtener todos los usuarios
        /// </summary>
        /// <returns></returns>
        Task<List<Usuario>> ObtenerTodosAsync();

        /// <summary>
        /// Método para obtener un usuario por Id
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        Task<Usuario> ObtenerPorIdAsync(string idUsuario);

        /// <summary>
        /// Método para crear un usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        Task<Usuario> CrearAsync(Usuario usuario);
    }
}