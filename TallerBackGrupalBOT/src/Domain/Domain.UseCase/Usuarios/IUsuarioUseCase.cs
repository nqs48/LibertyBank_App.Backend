using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model.Entities.Usuarios;

namespace Domain.UseCase.Usuarios
{
    /// <summary>
    /// Interfaz de caso de uso de entidad <see cref="Usuario"/>
    /// </summary>
    public interface IUsuarioUseCase
    {
        /// <summary>
        /// Método para obtener todos los usuarios
        /// </summary>
        /// <returns></returns>
        Task<List<Usuario>> ObtenerTodos();

        /// <summary>
        /// Método para obtener un usuario por Id
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        Task<Usuario> ObtenerPorId(string idUsuario);

        /// <summary>
        /// Método para crear un usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        Task<Usuario> Crear(Usuario usuario);
    }
}