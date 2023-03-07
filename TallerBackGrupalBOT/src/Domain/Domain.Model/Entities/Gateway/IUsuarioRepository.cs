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
        /// Método para obtener entidades de tipo <see cref="Usuario"/>
        /// </summary>
        /// <returns></returns>
        Task<List<Usuario>> ObtenerTodosAsync();

        /// <summary>
        /// Método para obtener una entidad de tipo <see cref="Usuario"/> por su Id
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        Task<Usuario> ObtenerPorIdAsync(string idUsuario);

        /// <summary>
        /// Método para crear una entidad de tipo <see cref="Usuario"/>
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        Task<Usuario> CrearAsync(Usuario usuario);
    }
}