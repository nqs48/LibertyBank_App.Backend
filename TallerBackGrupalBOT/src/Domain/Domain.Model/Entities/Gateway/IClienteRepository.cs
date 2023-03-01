using Domain.Model.Entities.Clientes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Model.Entities.Gateway
{
    /// <summary>
    /// Repositorio del cliente
    /// </summary>
    public interface IClienteRepository
    {
        /// <summary>
        /// Obtiene todos los clientes
        /// </summary>
        /// <returns></returns>
        Task<List<Cliente>> ObtenerTodosAsync();

        /// <summary>
        /// Obtiene cliente por Id
        /// </summary>
        /// <param name="IdCliente"></param>
        /// <returns></returns>
        Task<Cliente> ObtenerPorIdAsync(string IdCliente);

        /// <summary>
        /// Obtiene cliente por numero de identificación
        /// </summary>
        /// <param name="numeroIdentificacion"></param>
        /// <returns></returns>
        Task<Cliente> ObtenerPorNumeroIdentificacion(string numeroIdentificacion);

        /// <summary>
        /// Crea un nuevo cliente
        /// </summary>
        /// <param name="idUsuarioCreacion"></param>
        /// <param name="cliente"></param>
        /// <returns></returns>
        Task<Cliente> CrearAsync(string idUsuarioCreacion, Cliente cliente);

        /// <summary>
        /// Actualiza datos del cliente
        /// </summary>
        /// <param name="IdCliente"></param>
        /// <param name="cliente"></param>
        /// <returns></returns>

        Task<Cliente> ActualizarAsync(string IdCliente, Cliente cliente);
    }
}